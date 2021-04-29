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

                string address;
                try
                {
                    address = url.Address;
                }
                catch
                {
                    address = "";
                }
                int amount;
                try
                {
                    amount = url.InactivityTimeout.Amount;
                }
                catch
                {
                    amount = 900;
                }
                AccessType accessType;
                try
                {
                    accessType = url.AccessType;
                }
                catch
                {
                    accessType = AccessType.Inbound;
                }
                string externalUrl;
                try
                {
                    externalUrl = url.ExternalUrl;
                }
                catch
                {
                    externalUrl = "";
                }

                var urlModel = new UrlViewModel()
                {
                    Id = url.Id,
                    Name = url.Name,
                    Address = address,
                    ExternalUrl = externalUrl,
                    Amount = amount,
                    Unit = Logic.Common.Unit.Seconds,
                    MonitorInactivity = url.MonitorInactivity,
                    Hits = url.Hits,
                    ShowInStatistics = url.ShowInStatistics,
                    ServiceId = url.Service.Id,
                    AccessType = url.AccessType
                };

                ViewBag.Services = serviceRepository.List();

                return View(urlModel);
            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}