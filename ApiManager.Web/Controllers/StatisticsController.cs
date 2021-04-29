using ApiManager.Logic.Common;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly UserRepository userRepository = new UserRepository();

        // GET: Statistics
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult UrlVisits(int urlId, Period period = Period.Month)
        {
            var model = new UrlStatisticsViewModel()
            {
                UrlId = urlId,
                Urls = urlRepository.List(),
                Period = period
            };

            return View(model);
        }

        [Authorize]
        public ActionResult UserVisits(int userId, Period period)
        {
            var model = new UserStatisticsViewModel()
            {
                UserId = userId,
                Users = userRepository.List(),
                Period = period
            };

            return View(model);
        
        }
    }
}