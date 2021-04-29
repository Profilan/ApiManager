using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class UrlViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "Destination")]
        public string Address { get; set; }
        [Display(Name = "External Hostname")]
        public string ExternalUrl { get; set; }
        public int Amount { get; set; }
        public Unit Unit { get; set; }
        [Display(Name = "Monitor Activity")]
        public bool MonitorInactivity { get; set; }
        public int Hits { get; set; }
        [Display(Name = "Show in statistics")]
        public bool ShowInStatistics { get; set; }
        [Display(Name = "Service")]
        public int ServiceId { get; set; }
        public AccessType AccessType { get; set; }
    }
}