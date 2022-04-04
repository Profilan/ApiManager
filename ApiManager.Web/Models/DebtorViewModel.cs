using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models
{
    public class DebtorViewModel
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}