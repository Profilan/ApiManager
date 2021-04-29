
using ApiManager.Logic.Common;
using System.ComponentModel.DataAnnotations;

namespace ApiManager.Web.Models
{
    public class TaskScheduleViewModel
    {

        [Required]
        public int Amount { get; set; }

        [Required]
        public Unit Unit { get; set; }

        public ScheduleType ScheduleType { get; set; }

        public string ScheduleStart { get; set; }

        public string ScheduleEnd { get; set; }

        public string ScheduleDays { get; set; }

        public int ScheduleRecurrence { get; set; }
    }
}