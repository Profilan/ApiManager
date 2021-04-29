
using ApiManager.Logic.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskFTPViewModel
    {
        [Display(Name = "Sync Method")]
        public FtpSyncMethod FtpSyncMethod { get; set; }

        [Display(Name = "Server address")]
        public string FTPServerAddress { get; set; }

        [Display(Name = "Connection Type")]
        public FtpConnectionType FtpConnectionType { get; set; }

        [Display(Name = "Port")]
        public int FtpPort { get; set; }

        [Display(Name = "Local Folder")]
        public string FtpLocalFolder { get; set; }

        [Display(Name = "Remote Folder")]
        public string FtpRemoteFolder { get; set; }
    }
}