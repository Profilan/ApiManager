using ApiManager.Logic.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.Logic.Mappings
{
    public class DebtorMap : ClassMap<Debtor>
    {
        public DebtorMap()
        {
            Table("EEK_API_vw_DEBTORS");

            Id(x => x.DEBITEURNR);

            Map(x => x.NAAM);
        }
    }
}
