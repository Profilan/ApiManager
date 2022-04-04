using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
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
            if (sortOrder == ".") // default order
            {
                sortOrder = "TimeStamp.desc";
            }
            string dateRange = data["query[daterange]"];
            Guid taskId = Guid.Empty;
            if (!string.IsNullOrEmpty(data["query[taskId]"]))
            {
                taskId = new Guid(data["query[taskId]"]);
            }

            int page = Convert.ToInt32(data["pagination[page]"]);
            int pageSize = Convert.ToInt32(data["pagination[perpage]"]);

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
                var items = logRepository.List(searchString, page, pageSize, start, end, sortOrder, userId, (ErrorType)errorType, taskId);

                List<LogApiModel> logs = new List<LogApiModel>();
                foreach (var item in items)
                {

                    logs.Add(new LogApiModel()
                    {
                        Id = item.Id,
                        TimeStamp = item.TimeStamp.ToString("dd-MM-yyyy HH:mm:ss"),
                        Message = item.Message,
                        PriorityName = item.PriorityName,
                        Url = item.Url,
                        Duration = item.Duration,
                        UserName = item.User != null ? userRepository.GetById(item.User.Id).Username : ""
                    });
                }

                int total = logRepository.GetTotal(searchString, start, end, userId, (ErrorType)errorType, taskId);
                var sort = sortOrder.Split('.');

                return Ok(new ResponseApiModel<LogApiModel>
                {
                    Meta = new ResponseMetaApiModel
                    {
                        Page = page,
                        Pages = (total + pageSize - 1) / pageSize,
                        PerPage = pageSize,
                        Total = total,
                        Sort = sort[1],
                        Field = sort[0]
                    },
                    Data = logs
                });

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
