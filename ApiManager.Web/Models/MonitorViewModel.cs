using ApiManager.Logic.Models;
using System.Collections.Generic;

namespace ApiManager.Web.Models
{
    public class MonitorViewModel
    {
        public IEnumerable<Scheduler> Schedulers { get; set; }
    }
}