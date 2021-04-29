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
    public class ShareController : Controller
    {
        private readonly ShareRepository shareRepository = new ShareRepository();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var item = shareRepository.GetById(id);

                var model = new ShareViewModel()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Amount = item.InactivityTimeout.Amount,
                    Unit = item.InactivityTimeout.Unit,
                    MonitorInactivity = item.MonitorInactivity,
                    UNCPath = item.UNCPath
                };

                return View(model);
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}