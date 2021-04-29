
using ApiManager.Logic.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskAuthenticationViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AuthenticationType AuthenticationType { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string OAuthUrl { get; set; }
        public string OAuthAudience { get; set; }

        [Display(Name = "Api Key")]
        public string ApiKey { get; set; }
    }
}