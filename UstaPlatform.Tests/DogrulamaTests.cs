using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Engine;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Tests
{
    /// <summary>
    /// Guard (Doğrulama) helper testleri
    /// </summary>
    public class GuardTests
    {
        [Fact]
        public void Guard_NotNull_ShouldThrow_WhenNull()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => UstaPlatform.Domain.Helpers.Dogrulama.NotNull(null, "test"));
        }

        [Fact]
        public void Guard_NotNull_ShouldNotThrow_WhenNotNull()
        {
            // Arrange
            var obj = new object();

            // Act & Assert (no exception)
            UstaPlatform.Domain.Helpers.Dogrulama.NotNull(obj, "test");
        }

        [Fact]
        public void Guard_NotNullOrEmpty_ShouldThrow_WhenEmpty()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UstaPlatform.Domain.Helpers.Dogrulama.NotNullOrEmpty("", "test"));
            Assert.Throws<ArgumentException>(() => UstaPlatform.Domain.Helpers.Dogrulama.NotNullOrEmpty("   ", "test"));
        }

        [Fact]
        public void Guard_Positive_ShouldThrow_WhenNonPositive()
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => UstaPlatform.Domain.Helpers.Dogrulama.Positive(0m, "test"));
            Assert.Throws<ArgumentException>(() => UstaPlatform.Domain.Helpers.Dogrulama.Positive(-1m, "test"));
        }
    }

}
