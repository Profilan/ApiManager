using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Controllers
{
    [Authorize(Roles = "Domain Admins")]
    public class UrlController : Controller
    {
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ServiceRepository serviceRepository = new ServiceRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Services = serviceRepository.List();

            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var url = urlRepository.GetById(id);

                var urlModel = new UrlViewModel()
                {
                    Id = url.Id,
                    Name = url.Name,
                    Address = url.Address,
                    Amount = url.InactivityTimeout.Amount,
                    Unit = Logic.Common.Unit.Seconds,
                    MonitorInactivity = url.MonitorInactivity,
                    Hits = url.Hits,
                    ShowInStatistics = url.ShowInStatistics,
                    ServiceId = url.Service.Id
                };

                ViewBag.Services = serviceRepository.List();

                return View(urlModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}