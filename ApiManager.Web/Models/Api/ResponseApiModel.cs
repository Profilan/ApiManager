using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace ApiManager.Web.Models.Api
{
    public class ResponseApiModel<T>
    {
        [JsonProperty("meta")]
        public ResponseMetaApiModel Meta { get; set; }

        [JsonProperty("data")]
        public IList<T> Data { get; set; }
    }
}