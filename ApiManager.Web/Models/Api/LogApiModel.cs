using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiManager.Web.Models.Api
{
    public class LogApiModel
    {
        public int Id { get; set; }

        public string TimeStamp { get; set; }

        public int Priority { get; set; }

        public string Message { get; set; }

        public string PriorityName { get; set; }

        public string Url { get; set; }

        public string Detail { get; set; }

        public bool Acknowledged { get; set; }

        public UserApiModel User { get; set; }

        public string UserName { get; set; }

        public double Duration { get; set; }
    }
}