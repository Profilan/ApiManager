using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using ApiManager.Web.Models.Api;
using ApiManager.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class UrlController : ApiController
    {
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ServiceRepository serviceRepository = new ServiceRepository();
        private readonly LogRepository logRepository = new LogRepository();
        private readonly UserRepository userRepository = new UserRepository();

        [Route("api/url/{searchstring}")]
        [HttpGet]
        public IHttpActionResult Get(string searchstring)
        {
            var items = urlRepository.ListBySearchstring(searchstring);

            List<UrlApiModel> urls = new List<UrlApiModel>();
            foreach (var item in items)
            {
                urls.Add(new UrlApiModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Hits = item.Hits
                });
            }

            return Ok(urls);
        }

        [Route("api/url")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            IEnumerable<Url> items;
            
            if (!String.IsNullOrEmpty(data["query[AccessType]"]))
            {
                AccessType accessType = (AccessType) Enum.Parse(typeof(AccessType), data["query[AccessType]"], true);
                items = urlRepository.List(sortOrder, searchString, accessType);
            }
            else
            {
                items = urlRepository.List(sortOrder, searchString);
            }


            List<UrlApiModel> urls = new List<UrlApiModel>();
            foreach (var item in items)
            {
                urls.Add(new UrlApiModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Address = item.Address,
                    Hits = item.Hits,
                    AccessType = item.AccessType
                });
            }

            return Ok(urls);
        }

        [Route("api/url/edit")]
        [HttpPost]
        public IHttpActionResult Edit(FormDataCollection data)
        {
            try
            {

                var url = urlRepository.GetById(Convert.ToInt32(data["Id"]));

                var monitorInactivity = false;
                if (data["MonitorInactivity"] == "true,false")
                {
                    monitorInactivity = true;
                }
                var showInStatistics = false;
                if (data["ShowInStatistics"] == "true,false")
                {
                    showInStatistics = true;
                }

                url.Name = data["Name"];
                url.Address = data["Address"];
                url.ExternalUrl = data["ExternalUrl"];
                var inactivityTimeout = new Interval(Convert.ToInt32(data["Amount"]), (Unit)Enum.Parse(typeof(Unit), data["Unit"]));
                url.InactivityTimeout = inactivityTimeout;
                url.MonitorInactivity = monitorInactivity;
                url.ShowInStatistics = showInStatistics;
                url.AccessType = (AccessType)Enum.Parse(typeof(AccessType), data["AccessType"]);
                url.Service = serviceRepository.GetById(Convert.ToInt32(data["ServiceId"]));

                urlRepository.Update(url);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Route("api/url/create")]
        [HttpPost]
        public IHttpActionResult CreateUrl(FormDataCollection data)
        {
            try
            {

                var monitorInactivity = false;
                if (data["MonitorInactivity"] == "true,false")
                {
                    monitorInactivity = true;
                }
                var showInStatistics = false;
                if (data["ShowInStatistics"] == "true,false")
                {
                    showInStatistics = true;
                }

                var url = new Url();

                url.Name = data["Name"];
                url.Address = data["Address"];
                url.ExternalUrl = data["ExternalUrl"];
                var inactivityTimeout = new Interval(Convert.ToInt32(data["Amount"]), (Unit)Enum.Parse(typeof(Unit), data["Unit"]));
                url.InactivityTimeout = inactivityTimeout;
                url.MonitorInactivity = monitorInactivity;
                url.ShowInStatistics = showInStatistics;
                url.AccessType = (AccessType)Enum.Parse(typeof(AccessType), data["AccessType"]);
                url.Service = serviceRepository.GetById(Convert.ToInt32(data["ServiceId"]));

                urlRepository.Insert(url);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Route("api/url/visits")]
        [HttpPost]
        public IHttpActionResult Visits(FormDataCollection data)
        {
            var url = urlRepository.GetById(Convert.ToInt32(data["urlId"]));

            var visitors = new List<VisitorViewModel>();

            var users = userRepository.List();
            foreach (var user in users)
            {
                var logs = logRepository.ListByUserAndUrl(user, url, (Period)Enum.Parse(typeof(Period), data["period"], true));
                if (logs.Count() > 0)
                {
                    double duration = 0;
                    foreach (var log in logs)
                    {
                        duration += log.Duration;
                    }
                    visitors.Add(new VisitorViewModel()
                    {
                        UserId = user.Id,
                        Username = user.Username,
                        QuantityVisitedUrls = logs.Count(),
                        LatestVisitDate = logs.Count() > 0 ? logs.Last().TimeStamp.ToString() : "",
                        AverageDuration = duration / logs.Count()
                    });
                }
            }


            return Ok(visitors);
        }

        [Route("api/url/top5")]
        [HttpGet]
        public IHttpActionResult Top5(Period period)
        {
            var items = urlRepository.ListTopFive(period);

            return Ok(DashboardService.GetTopFiveUrlsData(items));
        }
    }
}
