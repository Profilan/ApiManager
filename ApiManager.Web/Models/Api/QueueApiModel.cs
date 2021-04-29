using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class QueueApiModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("key")]
        public int Key { get; set; }
        [JsonProperty("try_count")]
        public int TryCount { get; set; }
        [JsonProperty("created")]
        public string Created { get; set; }
        [JsonProperty("title")]
        public string TaskTitle { get; set; }
    }
}