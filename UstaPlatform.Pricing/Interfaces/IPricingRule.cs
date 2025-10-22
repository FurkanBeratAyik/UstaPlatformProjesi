using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Pricing.Interfaces
{
    /// <summary>
    /// Fiyatlandırma kuralları için temel arayüz.
    /// Tüm fiyat kuralları bu arayüzü uygulamalıdır (OCP - Open/Closed Principle).
    /// </summary>

    public interface IPricingRule
    {
        string Name { get; }
        string Description { get; }
        decimal Apply(decimal currentPrice, is_emri order);
        bool IsApplicable(is_emri order);
    }
}