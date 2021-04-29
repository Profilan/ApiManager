using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Display(Name = "External Url")]
        public string ExternalUrl { get; set; }
        [Required]
        [Display(Name = "Password Hashing")]
        public int HashingId { get; set; }

        [Display(Name = "Htpasswd Lovation")]
        public string HTPasswdLocation { get; set; }
    }
}