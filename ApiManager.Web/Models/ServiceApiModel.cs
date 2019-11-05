using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class ServiceApiModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}