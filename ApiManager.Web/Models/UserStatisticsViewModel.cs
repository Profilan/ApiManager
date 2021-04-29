using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class UserStatisticsViewModel
    {
        public int UserId { get; set; }

        public IEnumerable<User> Users { get; set; }

        public Period Period { get; set; }
    }
}