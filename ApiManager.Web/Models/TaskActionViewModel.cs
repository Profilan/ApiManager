
using ApiManager.Logic.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskActionViewModel
    {
        [Display(Name = "Type")]
        public ActionType Type { get; set; }

        // Authentication
        public TaskAuthenticationViewModel AuthenticationViewModel { get; set; }

        // API
        public TaskApiViewModel ApiViewModel { get; set; }

        // FTP
        public TaskFTPViewModel FTPViewModel { get; set; }

        // File
        public TaskFileViewModel FileViewModel { get; set; }

        // Mail
        public TaskMailViewModel MailViewModel { get; set; }
    }
}