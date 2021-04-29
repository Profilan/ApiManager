using ApiManager.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class DebtorController : ApiController
    {
        private readonly DebtorRepository debtorRepository = new DebtorRepository();


        [Route("api/debtor/{searchstring}")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetDebtorsBySearchstring(string searchstring)
        {
            var debtors = debtorRepository.ListBySearchstring(searchstring);

            return Ok(debtors);
        }

    }
}
