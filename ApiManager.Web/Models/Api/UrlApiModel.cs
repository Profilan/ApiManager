using ApiManager.Logic.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class UrlApiModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("address")]
        public string Address { get; set; }
        [JsonProperty("inactivity_timeout")]
        public string InactivityTimeout { get; set; }
        [JsonProperty("external_url")]
        public string ExternalUrl { get; set; }
        [JsonProperty("monitor_activity")]
        public bool MonitorActivity { get; set; }
        [JsonProperty("hits")]
        public int Hits { get; set; }
        [JsonProperty("show_in_statistics")]
        public bool ShowInStatistics { get; set; }
        [JsonProperty("avg_duration")]
        public double AverageDuration { get; set; }
        [JsonProperty("latest_visit_date")]
        public string LatestVisitDate { get; set; }
        [JsonProperty("access_type")]
        public AccessType AccessType { get; set; }

    }
}