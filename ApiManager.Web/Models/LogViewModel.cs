using ApiManager.Logic.Models;
using System.Collections.Generic;

namespace ApiManager.Web.Models
{
    public class LogViewModel
    {
        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public IEnumerable<SchedulerTask> Tasks { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}