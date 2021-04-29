
using ApiManager.Logic.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskMailViewModel
    {
        [Display(Name = "Mail From")]
        public string MailSender { get; set; }

        [Display(Name = "Mail To")]
        public string MailRecipient { get; set; }

        [Display(Name = "Http Headers")]
        public IEnumerable<HttpHeader> Headers { get; set; }
    }
}