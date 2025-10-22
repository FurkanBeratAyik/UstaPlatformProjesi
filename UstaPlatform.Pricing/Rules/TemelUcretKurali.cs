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
    /// Temel ücret kuralı - her zaman uygulanır
    /// </summary>
    public class TemelUcretKurali : IPricingRule
    {
        public string Name => "Temel Ücret";
        public string Description => "İş türüne göre sabit başlangıç ücreti";

        public bool IsApplicable(is_emri order) => true;

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            // Temel ücret zaten var sadece döndür
            return currentPrice;
        }
    }
}
