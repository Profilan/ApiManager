using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
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
    

    public class ServiceController : ApiController
    {
        private readonly ServiceRepository serviceRepository = new ServiceRepository();
        private readonly PasswordHashingRepository passwordHashingRepository = new PasswordHashingRepository();

        [Route("api/service")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            var items = serviceRepository.List();

            IList<ServiceApiModel> services = new List<ServiceApiModel>();
            foreach (var service in services)
            {
                services.Add(new ServiceApiModel()
                {
                    Code = service.Code,
                    Hashing = service.Hashing
                });
            }

            return Ok(services);
        }


        [Route("api/service")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = serviceRepository.List(sortOrder, searchString);

            List<ServiceApiModel> services = new List<ServiceApiModel>();
            foreach (var item in items)
            {
                var hashing = new PasswordHashingApiModel();
                if (item.PasswordHashing != null)
                {
                    var passwordHashing = passwordHashingRepository.GetById(item.PasswordHashing.Id);
                    hashing.Code = passwordHashing.Code;
                }

                services.Add(new ServiceApiModel()
                {
                    Id = item.Id,
                    Code = item.Code,
                    ExternalUrl = item.ExternalUrl,
                    Hashing = hashing,
                });
            }

            return Ok(services);
        }

        [Route("api/service/create")]
        [HttpPost]
        public IHttpActionResult Create(FormDataCollection data)
        {
            try
            {
                var passwordHashing = passwordHashingRepository.GetById(Convert.ToInt32(data["HashingId"]));
                var service = new Service(data["Code"])
                {
                    PasswordHashing = passwordHashing,
                    ExternalUrl = data["ExternalUrl"]
                };

                serviceRepository.Insert(service);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Route("api/service/edit")]
        [HttpPost]
        public IHttpActionResult Edit(FormDataCollection data)
        {
            try
            {
                
                var service = serviceRepository.GetById(Convert.ToInt32(data["Id"]));

                service.Code = data["Code"];

                var passwordHashing = passwordHashingRepository.GetById(Convert.ToInt32(data["HashingId"]));
                service.PasswordHashing = passwordHashing;
                service.ExternalUrl = data["ExternalUrl"];
                service.HTPasswordLocation = data["HtpasswdLocation"];
                serviceRepository.Update(service);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
