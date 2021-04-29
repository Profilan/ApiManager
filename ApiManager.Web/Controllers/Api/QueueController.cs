using ApiManager.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class QueueController : ApiController
    {
        private readonly QueueRepository queueRepository = new QueueRepository();

        [Route("api/queue/delete/{id}")]
        [HttpPost]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                queueRepository.Delete(id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }

            return Content(HttpStatusCode.NoContent, "Queue Item deleted");
        }

    }
}
