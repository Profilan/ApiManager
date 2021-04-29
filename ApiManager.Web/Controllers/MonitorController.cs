using ApiManager.Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize]
    public class MonitorController : Controller
    {
        private readonly TaskRepository taskRepository = new TaskRepository();

        public ActionResult APITaskScheduler()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult APIQueue(Guid taskId)
        {
            return View();
        }
    }
}