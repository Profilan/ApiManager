using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class ShareApiModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("unc_path")]
        public string UNCPath { get; set; }

        [JsonProperty("monitor_inactivity")]
        public bool MonitorInactivity { get; set; }
    }
}