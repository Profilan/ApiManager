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
    public class ServiceController : Controller
    {
        private readonly ServiceRepository serviceRepository = new ServiceRepository();
        private readonly PasswordHashingRepository passwordHashingRepository = new PasswordHashingRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Hashings = passwordHashingRepository.List();

            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var item = serviceRepository.GetById(id);

                ViewBag.Hashings = passwordHashingRepository.List();
 
                var service = new ServiceViewModel()
                {
                    Id = item.Id,
                    Code = item.Code,
                    HashingId = item.PasswordHashing != null ? item.PasswordHashing.Id : 0,
                    ExternalUrl = item.ExternalUrl,
                    HTPasswdLocation = item.HTPasswordLocation
                };

                return View(service);
            }
            catch (Exception e)
            {
                Request.Flash("error", "Severe Error : " + e.Message);

                return RedirectToAction("Index");
            }
        }
    }
}