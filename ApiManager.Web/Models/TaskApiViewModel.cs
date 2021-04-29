
using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskApiViewModel
    {
        public string Url { get; set; }

        [Display(Name = "API Type")]
        public ApiType ApiType { get; set; }
 
        public HttpMethod HttpMethod { get; set; }

        public GraphQLMethod GraphQLMethod { get; set; }

        [Display(Name = "Url")]
        public int UrlId { get; set; }
        public IEnumerable<Url> Urls { get; set; }

        public IEnumerable<HttpHeader> Headers { get; set; }

        public IEnumerable<ActionHttpHeader> ActionHeaders { get; set; }
    }
}