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
    public class LogController : Controller
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly TaskRepository taskRepository = new TaskRepository();

        public ActionResult Index()
        {
            DateTime start, end;

            start = DateTime.Now.AddDays(-3).Date;
            end = DateTime.Now.AddDays(1).Date;

            LogViewModel viewModel = new LogViewModel
            {
                StartDate = start.ToString("dd-MM-yyyy"),
                EndDate = end.ToString("dd-MM-yyyy"),
                Tasks = taskRepository.List(),
                Users = userRepository.List()
            };

            return View(viewModel);
        }
    }
}