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
    /// Yeni müşteri hoş geldin indirimi
    /// Yeni plugin örneği - Ana kod değişmeden eklenebilir!
    /// </summary>
    public class YeniMusteriWelcomeRule : IPricingRule
    {
        private const decimal INDIRIM = 50m;

        public string Name
        {
            get { return "Yeni Müşteri İndirimi"; }
        }

        public string Description
        {
            get { return "İlk iş için 50 TL hoş geldin indirimi"; }
        }

        public bool IsApplicable(is_emri order)
        {
            // Gerçek uygulamada müşteri geçmişine bakılır
            // Demo için rastgele uygulayalım
            return false; // Şimdilik kapalı
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice - INDIRIM;
        }
    }
}
