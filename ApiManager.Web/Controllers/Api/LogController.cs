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

        [Route("api/log")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];
            int pageNumber = Convert.ToInt32(data["pagination[page]"]);
            int pageSize = Convert.ToInt32(data["pagination[perpage]"]);
            

            DateTime startDate, endDate;
            if (String.IsNullOrEmpty(data["StartDate"]) || String.IsNullOrEmpty(data["EndDate"]))
            {
                startDate = DateTime.Now.AddDays(-7).Date;
                endDate = DateTime.Now.AddDays(1).Date;
            }
            else
            {
                startDate = Convert.ToDateTime(data["StartDate"]);
                endDate = Convert.ToDateTime(data["EndDate"]);
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
                var items = logRepository.List(sortOrder, searchString, pageNumber, pageSize, startDate, endDate, userId, (ErrorType)errorType);

                List<LogApiModel> logs = new List<LogApiModel>();
                foreach (var item in items)
                {
                    logs.Add(new LogApiModel()
                    {
                        Id = item.Id,
                        TimeStamp = item.TimeStamp.ToString("dd-MM-yyyy hh:mm:ss"),
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
    }
}
