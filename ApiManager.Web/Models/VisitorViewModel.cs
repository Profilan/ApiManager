using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class VisitorViewModel
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("quantity_visited_urls")]
        public int QuantityVisitedUrls { get; set; }

        [JsonProperty("latest_visit_date")]
        public string LatestVisitDate { get; set; }

        [JsonProperty("avg_duration")]
        public double AverageDuration { get; set; }
    }
}