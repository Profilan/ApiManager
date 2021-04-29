using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class ChartDataset
    {

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("data")]
        public IList<int> Data { get; set; }

        [JsonProperty("backgroundColor")]
        public IList<string> Colors { get; set; }
    }
}