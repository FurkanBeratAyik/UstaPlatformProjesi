using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Pricing.Rules
{
    /// <summary>
    /// Acil çağrılar için ek ücret kuralı
    /// </summary>
    public class AcilCagriUcretiKurali : IPricingRule
    {
        private const decimal ACIL_EK_UCRET = 150m;

        public string Name => "Acil Çağrı Ücreti";
        public string Description => "Acil durumlar için sabit 150 TL ek ücret";

        public bool IsApplicable(is_emri order)
        {
            // Basit kontrol
            return order.Durum == "Acil" || order.PlanlananSaat.Hours < 8 || order.PlanlananSaat.Hours > 18;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice + ACIL_EK_UCRET;
        }
    }
}
