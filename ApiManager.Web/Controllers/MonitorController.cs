using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
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
        private readonly SchedulerRepository schedulerRepository = new SchedulerRepository();

        public ActionResult APITaskScheduler()
        {
            return View();
        }

        public ActionResult Index()
        {
            var schedulers = schedulerRepository.List();

            var viewModel = new MonitorViewModel
            {
                Schedulers = schedulers
            };

            return View(viewModel);
        }

        public ActionResult APIQueue(Guid taskId)
        {
            return View();
        }
    }
}