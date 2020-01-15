using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class UserApiModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("password")]
        public string HashedPassword { get; set; }
        [JsonProperty("apikey")]
        public string Apikey { get; set; }
        [JsonProperty("role")]
        public string Role { get; set; }
        [JsonProperty("allowed_ip")]
        public string AllowedIP { get; set; }
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
        [JsonProperty("state")]
        public int State { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("service")]
        public ServiceApiModel Service { get; set; }
        [JsonProperty("created")]
        public string SysCreated { get; set; }
        [JsonProperty("creator")]
        public int SysCreator { get; set; }
        [JsonProperty("modified")]
        public string SysModified { get; set; }
        [JsonProperty("modifier")]
        public int SysModifier { get; set; }
    }
}