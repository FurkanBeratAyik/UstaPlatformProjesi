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
    // <summary>
    /// Mahalle özel ücreti - Belirli bölgeler için özel fiyatlandırma
    /// Bu, PDF'de bahsedilen "Mahalle B için akşam tarifesi" örneğidir
    /// </summary>
    public class MahalleOzelUcretiRule : IPricingRule
    {
        public string Name
        {
            get { return "Mahalle Özel Ücreti"; }
        }

        public string Description
        {
            get { return "Uzak mahalleler için ek seyahat ücreti"; }
        }

        public bool IsApplicable(is_emri order)
        {
            // Koordinat (X, Y) > (50, 50) ise uzak mahalle kabul et
            return order.Adres.Item1 > 50 || order.Adres.Item2 > 50;
        }


        public decimal Apply(decimal currentPrice, is_emri order)
        {
            // Uzak mahalle için %15 ek ücret
            return currentPrice * 1.15m;
        }
    }
}
