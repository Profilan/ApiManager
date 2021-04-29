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
    public class ShareController : ApiController
    {
        private readonly ShareRepository shareRepository = new ShareRepository();

        [Route("api/share")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = shareRepository.List(sortOrder, searchString);

            List<ShareApiModel> services = new List<ShareApiModel>();
            foreach (var item in items)
            {
                services.Add(new ShareApiModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    UNCPath = item.UNCPath,
                    MonitorInactivity = item.MonitorInactivity
                });
            }

            return Ok(services);
        }


        [Route("api/share/edit")]
        [HttpPost]
        public IHttpActionResult Edit(FormDataCollection data)
        {
            try
            {

                var share = shareRepository.GetById(Convert.ToInt32(data["Id"]));

                var monitorInactivity = false;
                if (data["MonitorInactivity"] == "true,false")
                {
                    monitorInactivity = true;
                }

                share.Name = data["Name"];
                share.UNCPath = data["UNCPath"];
                var inactivityTimeout = new Interval(Convert.ToInt32(data["Amount"]), (Unit)Enum.Parse(typeof(Unit), data["Unit"]));
                share.InactivityTimeout = inactivityTimeout;
                share.MonitorInactivity = monitorInactivity;

                shareRepository.Update(share);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
