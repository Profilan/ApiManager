using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class UrlStatistics
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Total { get; set; }
    }
}
