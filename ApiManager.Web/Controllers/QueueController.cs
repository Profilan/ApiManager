using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize]
    public class QueueController : Controller
    {
        public ActionResult Index(Guid taskId)
        {
            return View();
        }
    }
}