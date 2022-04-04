using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using ApiManager.Logic.Repositories;
using ApiManager.Web.Helpers;
using ApiManager.Web.Models;
using ApiManager.Web.Models.Api;
using ApiManager.Web.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApiManager.Web.Controllers.Api
{
    public class UserController : ApiController
    {
        private readonly UserRepository userRepository = new UserRepository();
        private readonly UrlRepository urlRepository = new UrlRepository();
        private readonly ServiceRepository serviceRepository = new ServiceRepository();
        private readonly PasswordHashingRepository passwordHashingRepository = new PasswordHashingRepository();
        private readonly DebtorRepository debtorRepository = new DebtorRepository();
        private readonly LogRepository logRepository = new LogRepository();

        [Route("api/user")]
        [HttpGet]
        public IHttpActionResult Get()
        {

            var items = userRepository.List();

            List<UserApiModel> users = new List<UserApiModel>();
            foreach (var item in items)
            {
                var service = new ServiceApiModel()
                {
                    Code = item.Service.Code
                };

                users.Add(new UserApiModel()
                {
                    Id = item.Id,
                    Username = item.Username,
                    DisplayName = item.DisplayName,
                    Email = item.Email,
                    Role = item.Role,
                    Enabled = item.Enabled,
                    Service = service,
                    SysCreated = item.SysCreated.ToString("dd-MM-yyyy HH:mm:ss")
                });
            }

            return Ok(users);
        }

        [Route("api/user")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = userRepository.List(sortOrder, searchString);

            List<UserApiModel> users = new List<UserApiModel>();
            foreach (var item in items)
            {
                var service = new ServiceApiModel();
                try
                {
                    if (item.Service != null)
                    {
                        service.Code = item.Service.Code;
                    }
                }
                catch
                {

                }

                users.Add(new UserApiModel()
                {
                    Id = item.Id,
                    Username = item.Username,
                    DisplayName = item.DisplayName,
                    Email = item.Email,
                    Role = item.Role,
                    Enabled = item.Enabled,
                    Service = service,
                    SysCreated = item.SysCreated.ToString("dd-MM-yyyy HH:mm:ss")
                });
            }

            return Ok(users);
        }

        [Route("api/user/edit")]
        [HttpPost]
        public IHttpActionResult Edit(UserModel data)
        {
            try
            {
                var user = userRepository.GetById(data.Id);

                var service = serviceRepository.GetById(data.ServiceId);

                user.Username = data.Username;
                user.DisplayName = data.DisplayName;
                user.Email = data.Email;
                user.Apikey = data.Apikey;
                user.Role = data.Role;
                user.AllowedIP = data.AllowedIP;
                user.Enabled = data.Enabled;
                user.Service = service;

                user.Urls.Clear();
                foreach (var urlItem in data.Urls)
                {
                    if (!string.IsNullOrEmpty(urlItem.Name))
                    {
                        var url = urlRepository.GetByName(urlItem.Name);
                        user.Urls.Add(url);
                    }
                }

                user.Debtors.Clear();
                foreach (var debtor in data.Debtors)
                {
                    if (!string.IsNullOrEmpty(debtor.Name))
                    {
                        var id = debtor.Name.Split(new char[] { ' ' })[0];
                        user.Debtors.Add(debtorRepository.GetById(id));
                    }
                }


                var sysCreated = user.SysCreated.ToShortDateString();
                if (sysCreated == DateTime.MinValue.ToShortDateString())
                {
                    user.SysCreated = DateTime.Now;
                }
                user.SysModified = DateTime.Now;

                userRepository.Update(user);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [Route("api/user/create")]
        [HttpPost]
        public HttpResponseMessage Create(UserModel data)
        {
               
            var user = new User()
            {
                Username = data.Username,
                DisplayName = data.DisplayName,
                Email = data.Email,
                Apikey = data.Apikey,
                Role = data.Role,
                AllowedIP = data.AllowedIP,
                Enabled = data.Enabled,
            };

            var service = serviceRepository.GetById(data.ServiceId);
            user.Service = service;

            var passwordHashing = passwordHashingRepository.GetById(service.PasswordHashing.Id);

            IPasswordHasher hasher;
            switch (passwordHashing.Code)
            {
                case "MD5 (Apache)":
                    hasher = new MD5ApachePasswordHasher();
                    var htpasswdService = new HtpasswdService(service.HTPasswordLocation);
                    htpasswdService.GetUserList();
                    htpasswdService.AddUser(data.Username, data.Password);
                    break;
                case "SHA1 (ZF1)":
                    hasher = new SHA1ZF1PasswordHasher();
                    break;
                case "Bcrypt":
                default:
                    hasher = new BcryptHasher();
                    break;
            };

            user.HashedPassword = hasher.HashPassword(data.Password);
            user.RawPassword = data.Password;


            foreach (var urlItem in data.Urls)
            {
                if (!string.IsNullOrEmpty(urlItem.Name))
                {
                    var url = urlRepository.GetByName(urlItem.Name);
                    user.Urls.Add(url);
                }
            }

            foreach (var debtor in data.Debtors)
            {
                if (!string.IsNullOrEmpty(debtor.Name))
                {
                    var id = debtor.Name.Split(new char[] { ' ' })[0];
                    user.Debtors.Add(debtorRepository.GetById(id));
                }
            }

            user.SysCreated = DateTime.Now;
            user.SysModified = user.SysCreated;

            userRepository.Insert(user);
            

            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/user/changepassword")]
        [HttpPost]
        public HttpResponseMessage ChangePassword(PasswordViewModel data)
        {

            var user = userRepository.GetById(data.Id);
            var service = serviceRepository.GetById(user.Service.Id);
            var passwordHashing = passwordHashingRepository.GetById(service.PasswordHashing.Id);

            IPasswordHasher hasher;
            switch (passwordHashing.Code)
            {
                case "MD5 (Apache)":
                    hasher = new MD5ApachePasswordHasher();
                    var htpasswdService = new HtpasswdService(service.HTPasswordLocation);
                    htpasswdService.GetUserList();
                    htpasswdService.UpdateUser(user.Username, data.Password1);
                    break;
                case "SHA1 (ZF1)":
                    hasher = new SHA1ZF1PasswordHasher();
                    break;
                case "Bcrypt":
                default:
                    hasher = new BcryptHasher();
                    break;
            };

            user.HashedPassword = hasher.HashPassword(data.Password1);
            user.RawPassword = data.Password1;

            userRepository.Update(user);


            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [Route("api/user/visits")]
        [HttpPost]
        public IHttpActionResult Visits(FormDataCollection data)
        {
            var user = userRepository.GetById(Convert.ToInt32(data["userId"]));

            var urls = urlRepository.List().Where(x => x.ShowInStatistics == true);
            var visitedUrls = new List<UrlApiModel>();
            foreach (var url in urls)
            {
                var logs = logRepository.ListByUserAndUrl(user, url, (Period)Enum.Parse(typeof(Period), data["period"], true));
                if (logs.Count() > 0)
                {
                    double totalDuration = 0;
                    foreach (var log in logs)
                    {
                        totalDuration += log.Duration;
                    }

                    visitedUrls.Add(new UrlApiModel()
                    {
                        Id = url.Id,
                        Name = url.Name,
                        Hits = logs.Count(),
                        AverageDuration = totalDuration / logs.Count(),
                        LatestVisitDate = logs.Count() > 0 ? logs.Last().TimeStamp.ToString() : ""
                    });
                }
            }

            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];
            var sortedUrls = new List<UrlApiModel>();
            switch (sortOrder)
            {
                case "latest_visit_date.desc":
                    sortedUrls = visitedUrls.OrderByDescending(x => x.LatestVisitDate).ToList();
                    break;
                case "latest_visit_date.asc":
                    sortedUrls = visitedUrls.OrderBy(x => x.LatestVisitDate).ToList();
                    break;
                case "hits.asc":
                    sortedUrls = visitedUrls.OrderBy(x => x.Hits).ToList();
                    break;
                case "hits.desc":
                    sortedUrls = visitedUrls.OrderByDescending(x => x.Hits).ToList();
                    break;
                case "avg_duration.asc":
                    sortedUrls = visitedUrls.OrderBy(x => x.AverageDuration).ToList();
                    break;
                case "avg_duration.desc":
                    sortedUrls = visitedUrls.OrderByDescending(x => x.AverageDuration).ToList();
                    break;
                case "name.desc":
                    sortedUrls = visitedUrls.OrderByDescending(x => x.Name).ToList();
                    break;
                case "name.asc":
                default:
                    sortedUrls = visitedUrls.OrderBy(x => x.Name).ToList();
                    break;
            }

            return Ok(sortedUrls);
        }

        [Route("api/user/top5")]
        [HttpGet]
        public IHttpActionResult Top5(Period period)
        {
            var items = userRepository.ListTopFive(period);

            return Ok(DashboardService.GetTopFiveUsersData(items));
        }
    }
}
