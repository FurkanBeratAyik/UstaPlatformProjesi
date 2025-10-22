using System;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Engine;
using UstaPlatform.Pricing.Interfaces;
using UstaPlatform.Tests;
using Xunit;

namespace UstaPlatform.Tests
{

    /// <summary>
    /// Test amaçlı basit fiyat kuralı
    /// </summary>
    public class TestPricingRule : IPricingRule
    {
        public string Name { get { return "Test Rule"; } }
        public string Description { get { return "Test için %10 artış"; } }

        public bool IsApplicable(is_emri order)
        {
            return true;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice * 1.10m;
        }
    }

    /// <summary>
    /// Test amaçlı indirim kuralı
    /// </summary>
    public class TestDiscountRule : IPricingRule
    {
        public string Name { get { return "Test Discount"; } }
        public string Description { get { return "Test için 20 TL indirim"; } }

        public bool IsApplicable(is_emri order)
        {
            return true;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice - 20m;
        }
    }

    /// <summary>
    /// Test amaçlı uygulanamaz kural
    /// </summary>
    public class NonApplicableRule : IPricingRule
    {
        public string Name { get { return "Non Applicable Rule"; } }
        public string Description { get { return "Hiçbir zaman uygulanmaz"; } }

        public bool IsApplicable(is_emri order)
        {
            return false;
        }

        public decimal Apply(decimal currentPrice, is_emri order)
        {
            return currentPrice * 2; // Uygulanmayacak
        }
    }
}
    /// PricingEngine için birim testleri
    /// Plugin yükleme ve fiyat hesaplama senaryolarını test eder
    /// </summary>
    public class PricingEngineTests
{
    [Fact]
    public void PricingEngine_ShouldAddRule_Successfully()
    {
        // Arrange
        var engine = new PricingEngine();
        var rule = new TestPricingRule();

        // Act
        engine.AddRule(rule);

        // Assert
        Assert.Single(engine.LoadedRules);
        Assert.Equal("Test Rule", engine.LoadedRules[0].Name);
    }

    [Fact]
    public void PricingEngine_ShouldCalculatePrice_WithSingleRule()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new TestPricingRule()); // %10 artış

        var isEmri = new is_emri("WO-TEST", "T-TEST")
        {
            UstaId = "U-TEST",
            PlanlananTarih = DateTime.Now,
            Durum = "Normal"
        };

        // Act
        var result = engine.Calculate(isEmri, 100m);

        // Assert
        Assert.Equal(100m, result.BasePrice);
        Assert.Equal(110m, result.FinalPrice); // 100 + 10%
        Assert.Single(result.AppliedRules);
    }

    [Fact]
    public void PricingEngine_ShouldCalculatePrice_WithMultipleRules()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new TestPricingRule()); // %10 artış
        engine.AddRule(new TestDiscountRule()); // 20 TL indirim

        var isEmri = new is_emri("WO-TEST", "T-TEST")
        {
            UstaId = "U-TEST",
            PlanlananTarih = DateTime.Now,
            Durum = "Normal"
        };

        // Act
        var result = engine.Calculate(isEmri, 100m);

        // Assert
        Assert.Equal(100m, result.BasePrice);
        Assert.Equal(90m, result.FinalPrice); // 100 + 10% - 20 = 90
        Assert.Equal(2, result.AppliedRules.Count);
    }

    [Fact]
    public void PricingEngine_ShouldNotApply_WhenRuleNotApplicable()
    {
        // Arrange
        var engine = new PricingEngine();
        engine.AddRule(new NonApplicableRule());

        var isEmri = new is_emri("WO-TEST", "T-TEST")
        {
            UstaId = "U-TEST",
            PlanlananTarih = DateTime.Now,
            Durum = "Normal"
        };

        // Act
        var result = engine.Calculate(isEmri, 100m);

        // Assert
        Assert.Equal(100m, result.FinalPrice); // Değişmedi
        Assert.Empty(result.AppliedRules); // Hiç uygulanmadı
    }

    [Fact]
    public void PricingEngine_ShouldThrow_WhenOrderIsNull()
    {
        // Arrange
        var engine = new PricingEngine();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => engine.Calculate(null, 100m));
    }
}