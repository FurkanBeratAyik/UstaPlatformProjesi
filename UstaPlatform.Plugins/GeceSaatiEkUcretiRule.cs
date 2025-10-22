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
    /// Gece saati ek ücreti - Akşam tarifesi örneği
    /// </summary>
    public class GeceSaatiEkUcretiRule : IPricingRule
    {
        private const int GECE_BASLANGIC_SAAT = 20; // 20:00
        private const int GECE_BITIS_SAAT = 6;      // 06:00

        public string Name
        {
            get { return "Gece Saati Ek Ücreti"; }
        }

        public string Description
        {
            get { return "20:00 - 06:00 arası için ek ücret"; }
        }

        public bool IsApplicable(is_emri order)
        {
            var saat = order.PlanlananSaat.Hours;
            return saat >= GECE_BASLANGIC_SAAT || saat < GECE_BITIS_SAAT;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            // Gece için %30 ek ücret
            return currentPrice * 1.30m;
        }
    }
}
