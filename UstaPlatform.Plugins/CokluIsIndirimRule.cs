using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Plugins
{
    /// <summary>
    /// Çoklu iş indirimi - Aynı adreste birden fazla iş varsa indirim
    /// </summary>
    public class CokluIsIndirimRule : IPricingRule
    {
        public string Name
        {
            get { return "Çoklu İş İndirimi"; }
        }

        public string Description
        {
            get { return "Aynı adreste birden fazla iş için %5 indirim"; }
        }

        public bool IsApplicable(is_emri order)
        {
            // Demo amaçlı, her zaman false
            return false;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            // %5 indirim uygula
            return currentPrice * 0.95m;
        }
    }
}

