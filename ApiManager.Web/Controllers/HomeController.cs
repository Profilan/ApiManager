using ApiManager.Logic.Common;
using ApiManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize(Roles = "Domain Admins")]
    public class HomeController : Controller
    {
        public ActionResult Index(Period period = Period.Month)
        {
            var viewModel = new DashboardViewModel()
            {
                Period = period
            };

            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}