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
    public class UrlController : ApiController
    {
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ServiceRepository serviceRepository = new ServiceRepository();

        [Route("api/url")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = urlRepository.List(sortOrder, searchString);

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
                var inactivityTimeout = new Interval(Convert.ToInt32(data["Amount"]), (Unit)Enum.Parse(typeof(Unit), data["Unit"]));
                url.InactivityTimeout = inactivityTimeout;
                url.MonitorInactivity = monitorInactivity;
                url.ShowInStatistics = showInStatistics;
                url.Service = serviceRepository.GetById(Convert.ToInt32(data["ServiceId"]));

                urlRepository.Update(url);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
