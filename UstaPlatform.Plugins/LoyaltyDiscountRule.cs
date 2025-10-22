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
    /// Sadakat indirimi kuralı - Runtime'da yüklenebilir plugin örneği
    /// Bu DLL, Plugins klasörüne kopyalandığında otomatik yüklenir (OCP!)
    /// </summary>
    public class LoyaltyDiscountRule : IPricingRule
    {
        private const decimal INDIRIM_YUZDESI = 0.10m; // %10 indirim

        public string Name
        {
            get { return "Sadakat İndirimi"; }
        }

        public string Description
        {
            get { return "Düzenli müşteriler için %10 indirim"; }
        }

        public bool IsApplicable(is_emri order)
        {
            // Basit kontrol - gerçek uygulamada müşteri geçmişine bakılır
            // Şimdilik her WorkOrder için uygula (demo amaçlı)
            return true;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            // %10 indirim uygula
            return currentPrice * (1 - INDIRIM_YUZDESI);
        }
    }
}
