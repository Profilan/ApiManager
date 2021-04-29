using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class ChartDataApiModel
    {
        [JsonProperty("datasets")]
        public IList<ChartDataset> Datasets { get; set; }

        [JsonProperty("labels")]
        public IList<string> Labels { get; set; }
    }
}