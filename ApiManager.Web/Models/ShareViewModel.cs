using ApiManager.Logic.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class ShareViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        
        [Required]
        [Display(Name = "UNC Path")]
        public string UNCPath { get; set; }

        [Required]
        public int Amount { get; set; }

        [Required]
        public Unit Unit { get; set; }

        [Display(Name = "Monitor Inactivity")]
        public bool MonitorInactivity { get; set; }
    }
}