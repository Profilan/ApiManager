using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class UrlStatisticsViewModel
    {
        public int UrlId { get; set; }

        public IEnumerable<Url> Urls { get; set; }

        public Period Period { get; set; }
    }
}