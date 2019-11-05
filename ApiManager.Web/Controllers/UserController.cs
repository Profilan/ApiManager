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
    public class UserController : Controller
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly DebtorRepository debtorRepository = new DebtorRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            var item = userRepository.GetById(id);

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
                Urls = item.Urls
            };

            return View(user);
        }
    }
}