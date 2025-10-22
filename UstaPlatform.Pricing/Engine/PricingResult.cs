using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Pricing.Engine
{
    /// <summary>
    /// Fiyatlandırma sonucu
    /// </summary>
    public class PricingResult
    {
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }
        public List<RuleApplication> AppliedRules { get; set; } = new List<RuleApplication>();

        public decimal TotalAdjustment
        {
            get { return FinalPrice - BasePrice; }
        }
    }

    /// <summary>
    /// Uygulanan kural bilgisi
    /// </summary>
    public class RuleApplication
    {
        public string RuleName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal PriceBefore { get; set; }
        public decimal PriceAfter { get; set; }
        public decimal Adjustment { get; set; }
    }
}