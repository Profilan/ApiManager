using ApiManager.Logic.Common;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    
    public class LogController : ApiController
    {
        private readonly LogRepository logRepository = new LogRepository();
        private readonly UserRepository userRepository = new UserRepository();
        private readonly UrlRepository urlRepository = new UrlRepository();

        [Route("api/log")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];
            string dateRange = data["query[daterange]"];

            DateTime start = DateTime.Today.AddDays(-3);
            DateTime end = DateTime.Today.AddDays(1).AddSeconds(-1);
            if (!string.IsNullOrEmpty(dateRange))
            {
                var dates = dateRange.Split(new char[] { ';' });
                start = DateTime.Parse(dates[0]);
                end = DateTime.Parse(dates[1]);
            }

            int userId = -1;
            if (!String.IsNullOrEmpty(data["query[UserId]"]))
            {
                userId = Convert.ToInt32(data["query[UserId]"]);
            }
            int errorType = 0;
            if (!String.IsNullOrEmpty(data["query[Type]"]))
            {
                errorType = Convert.ToInt32(data["query[Type]"]);
            }

            try
            {
                var items = logRepository.List(sortOrder, searchString, start, end, userId, (ErrorType)errorType);

                List<LogApiModel> logs = new List<LogApiModel>();
                foreach (var item in items)
                {
                    logs.Add(new LogApiModel()
                    {
                        Id = item.Id,
                        TimeStamp = item.TimeStamp.ToString("MM-dd-yyyy H:mm:ss"),
                        Message = item.Message,
                        PriorityName = item.PriorityName,
                        Url = item.Url,
                        Duration = item.Duration,
                    });
                }

                return Ok(logs);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
         }

        [Route("api/log/{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var item = logRepository.GetById(id);

            var username = "";
           
            if (item.User != null)
            {
                var user = userRepository.GetById(item.User.Id);
                username = user.Username;
            }

            var log = new LogApiModel()
            {
                Detail = item.Detail,
                User = new UserApiModel()
                {
                    Username = username
                }
            };


            return Ok(log);
        }

        [Route("api/log/latest-errors")]
        [HttpGet]
        public IHttpActionResult GetLatestErrors(Period period)
        {
            var urls = urlRepository.List().Where(x => x.ShowInStatistics == true);

            var logErrors = new List<LatestErrorApiModel>();
            foreach (var url in urls)
            {
                var logs = logRepository.ListByTypeAndUrl(period, ErrorType.ERR, url);
                if (logs.Count() > 0)
                {
                    logErrors.Add(new LatestErrorApiModel()
                    {
                        UrlName = url.Name,
                        ErrorCount = logs.Count()
                    });
                }
            }

            return Ok(logErrors);
        }
    }
}
