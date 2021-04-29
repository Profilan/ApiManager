using ApiManager.Logic.Models;
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
    public class UserController : Controller
    {
        private readonly UserRepository userRepository = new UserRepository();
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
            var item = userRepository.GetById(id);
 
            int serviceId = -1;
            if (item.Service !=  null)
            {
                serviceId = item.Service.Id;
            }
            
            var user = new UserViewModel()
            {
                Id = item.Id,
                Username = item.Username,
                DisplayName = item.DisplayName,
                Email = item.Email,
                Apikey = item.Apikey,
                Role = item.Role,
                AllowedIP = item.AllowedIP,
                Debtors = item.Debtors,
                Urls = item.Urls,
                Enabled = item.Enabled,
                ServiceId = serviceId,
                Services = serviceRepository.List()
            };

            return View(user);
        }
        public ActionResult ChangePassword(int id)
        {
                var item = userRepository.GetById(id);

                var model = new PasswordViewModel()
                {
                    Id = item.Id
                };

                return View(model);
        }
    }
}