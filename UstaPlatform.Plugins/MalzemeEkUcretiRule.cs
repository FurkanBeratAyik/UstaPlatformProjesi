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
    /// Malzeme ek ücreti - İş türüne göre malzeme bedeli
    /// </summary>
    public class MalzemeEkUcretiRule : IPricingRule
    {
        private const decimal MALZEME_BEDELI = 75m;

        public string Name
        {
            get { return "Malzeme Bedeli"; }
        }

        public string Description
        {
            get { return "İş için gereken malzeme bedeli (75 TL)"; }
        }

        public bool IsApplicable(is_emri order)
        {
            // Tüm işler için malzeme ücreti ekleme
            return true;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice + MALZEME_BEDELI;
        }
    }
}
