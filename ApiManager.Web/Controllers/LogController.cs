using ApiManager.Logic.Repositories;
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

        public ActionResult Index()
        {
            ViewBag.Users = userRepository.List();

            DateTime start, end;

            start = DateTime.Now.AddDays(-7).Date;
            end = DateTime.Now.AddDays(1).Date;

            ViewBag.StartDate = start.ToString("dd-MM-yyyy");
            ViewBag.EndDate = end.ToString("dd-MM-yyyy");

            return View();
        }
    }
}