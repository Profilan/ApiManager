using System;

namespace ApiManager.Web.Models
{
    public class RepetitionViewModel
    {
        public TimeSpan Duration { get; set; }

        public TimeSpan Interval { get; set; }

        public bool StopAtDurationEnd { get; set; }
    }
}