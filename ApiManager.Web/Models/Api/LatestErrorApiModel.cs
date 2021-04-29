using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class LatestErrorApiModel
    {
        [JsonProperty("url_name")]
        public string UrlName { get; set; }

        [JsonProperty("error_count")]
        public int ErrorCount { get; set; }
    }
}