using ApiManager.Logic.Common;
using ApiManager.Logic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApiManager.Web.Models
{
    public class TaskViewModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public TaskType TaskType { get; set; }

        [Display(Name = "Classname")]
        public string Classname { get; set; }

        public bool Enabled { get; set; }

        public bool Active { get; set; }

        public int Queued { get; set; }

        [Display(Name = "Stored Procedure Logger")]
        public string SPLogger { get; set; }

        [Display(Name = "Last Run Time")]
        public DateTime LastRunTime { get; set; }

        [Display(Name = "Last Run Details")]
        public string LastRunDetails { get; set; }

        [Display(Name = "Last Run Result")]
        public string LastRunResult { get; set; }

        [Display(Name = "Maximum Errors")]
        public int MaxErrors { get; set; }

        [Display(Name = "Total processed items")]
        public int TotalProcessedItems { get; set; }

        public bool IsEdit { get; set; }

        // Authentication
        public TaskAuthenticationViewModel AuthenticationViewModel { get; set; }

        // Schedule
        public TaskScheduleViewModel ScheduleViewModel { get; set; }

        // API fields
        public TaskApiViewModel ApiViewModel { get; set; }

        // FTP fields
        public TaskFTPViewModel FTPViewModel { get; set; }

        // File fields
        public TaskFileViewModel FileViewModel { get; set; }

        // Mail fields
        public TaskMailViewModel MailViewModel { get; set; }
    }
}