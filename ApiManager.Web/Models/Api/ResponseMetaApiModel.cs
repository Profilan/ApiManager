using Newtonsoft.Json;

namespace ApiManager.Web.Models.Api
{
    public class ResponseMetaApiModel
    {
        [JsonProperty("page")]
        public int Page { get; set; }

        [JsonProperty("pages")]
        public int Pages { get; set; }

        [JsonProperty("perpage")]
        public int PerPage { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("sort")]
        public string Sort { get; set; }

        [JsonProperty("field")]
        public string Field { get; set; }
    }
}