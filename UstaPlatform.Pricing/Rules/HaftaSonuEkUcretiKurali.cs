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
    /// Hafta sonu için ek ücret kuralı
    /// </summary>
    public class HaftaSonuEkUcretiKurali : IPricingRule
    {
        private const decimal EK_UCRET_YUZDESI = 0.20m; // %20

        public string Name => "Hafta Sonu Ek Ücreti";
        public string Description => "Cumartesi ve Pazar günleri için %20 ek ücret";

        public bool IsApplicable(is_emri order)
        {
            var gun = order.PlanlananTarih.DayOfWeek;
            return gun == DayOfWeek.Saturday || gun == DayOfWeek.Sunday;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice * (1 + EK_UCRET_YUZDESI);
        }
    }
}
