using APIManager.Logic.Enums;
using Newtonsoft.Json;
using System;

namespace ApiManager.Web.Models.Api
{
    public class ScheduleApiModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("taskId")]
        public Guid TaskId { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("start")]
        public string StartBoundery { get; set; }

        [JsonProperty("end")]
        public string EndBoundery { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

    }
}