using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class LogApiModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }

        [JsonProperty("priority")]
        public int Priority { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("priority_name")]
        public string PriorityName { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("acknowledged")]
        public bool Acknowledged { get; set; }

        [JsonProperty("user")]
        public UserApiModel User { get; set; }

        [JsonProperty("duration")]
        public double Duration { get; set; }
    }
}