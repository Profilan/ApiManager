using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class TaskApiModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("last_run_time")]
        public string LastRunTime { get; set; }
        [JsonProperty("last_run_result")]
        public string LastRunResult { get; set; }
        [JsonProperty("last_run_details")]
        public string LastRunDetails { get; set; }
        [JsonProperty("errors")]
        public int Errors { get; set; }

        [JsonProperty("queued")]
        public int Queued { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }
    }
}