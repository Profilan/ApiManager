using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class PasswordHashingApiModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }
    }
}