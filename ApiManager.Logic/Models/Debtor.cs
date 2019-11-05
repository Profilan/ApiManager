using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Models
{
    public class Debtor
    {
        public virtual string NAAM { get; set; }
        public virtual string DEBITEURNR { get; set; }
        public virtual ISet<User> Users { get; set; }

        public Debtor()
        {
            Users = new HashSet<User>();
        }
    }
}
