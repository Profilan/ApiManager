using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Controllers
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Apikey { get; set; }
        public string Role { get; set; }
        public IEnumerable<Debtor> Debtors { get; set; }
        public IEnumerable<Url> Urls { get; set; }
        public bool Enabled { get; set; }
        public string AllowedIP { get; set; }
        public int ServiceId { get; set; }
    }
}