using ApiManager.Logic.Repositories;
using ApiManager.Web.Models;
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

        [Route("api/users")]
        [HttpGet]
        public IHttpActionResult Get()
        {

            var items = userRepository.List();

            List<UserApiModel> users = new List<UserApiModel>();
            foreach (var item in items)
            {
                var passwordHashing = new PasswordHashingApiModel()
                {
                    Code = item.PasswordHashing.Code
                };

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
                    PasswordHashing = passwordHashing,
                    Enabled = item.Enabled,
                    Service = service
                });
            }

            return Ok(users);
        }

        [Route("api/users")]
        [HttpPost]
        public IHttpActionResult Get(FormDataCollection data)
        {
            string searchString = data["query[generalsearch]"];
            string sortOrder = data["sort[field]"] + "." + data["sort[sort]"];

            var items = userRepository.List(sortOrder, searchString);

            List<UserApiModel> users = new List<UserApiModel>();
            foreach (var item in items)
            {
                var passwordHashing = new PasswordHashingApiModel();
                try
                {
                    passwordHashing.Code = item.PasswordHashing.Code;
                }
                catch
                {

                }
                var service = new ServiceApiModel();
                try
                {
                    service.Code = item.Service.Code;
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
                    PasswordHashing = passwordHashing,
                    Enabled = item.Enabled,
                    Service = service
                });
            }

            return Ok(users);
        }

        [Route("api/users/edit")]
        [HttpPost]
        public IHttpActionResult Edit(UserViewModel data)
        {
            try
            {
                var user = userRepository.GetById(data.Id);

                user.Username = data.Username;
                user.DisplayName = data.DisplayName;
                user.Email = data.Email;
                user.Apikey = data.Apikey;
                user.Role = data.Role;
                user.AllowedIP = data.AllowedIP;
                user.Enabled = data.Enabled;
                user.Urls.Clear();
                foreach (var urlItem in data.Urls)
                {
                    var url = urlRepository.GetByName(urlItem.Name);
                    user.Urls.Add(url);
                }

                userRepository.Update(user);

                return Ok();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
