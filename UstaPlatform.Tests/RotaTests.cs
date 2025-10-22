using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UstaPlatform.Tests
{
    /// <summary>
    /// Rota için birim testleri
    /// IEnumerable ve koleksiyon başlatıcıları test eder
    /// </summary>
    public class RotaTests
    {
        [Fact]
        public void Rota_ShouldAddStops_UsingCollectionInitializer()
        {
            // Arrange & Act
            var rota = new UstaPlatform.Domain.Collections.Rota("U001", new DateTime(2025, 10, 21));

            rota.Add(10, 20);
            rota.Add(15, 25);
            rota.Add(20, 30);

            // Assert
            Assert.Equal(3, rota.DurakSayisi);
        }

        [Fact]
        public void Rota_ShouldBeEnumerable()
        {
            // Arrange
            var rota = new UstaPlatform.Domain.Collections.Rota("U001", new DateTime(2025, 10, 21));

            rota.Add(10, 20);
            rota.Add(15, 25);
            rota.Add(20, 30);

            // Act
            var duraklar = new List<(int X, int Y)>();
            foreach (var durak in rota)
            {
                duraklar.Add(durak); // artık sorun yok
            }

            // Assert'ler
            Assert.Equal(10, duraklar[0].X);
            Assert.Equal(20, duraklar[0].Y);
            Assert.Equal(15, duraklar[1].X);
            Assert.Equal(25, duraklar[1].Y);

        }

        [Fact]
        public void Rota_ShouldCalculateTotalDistance()
        {
            // Arrange
            var rota = new UstaPlatform.Domain.Collections.Rota("U001", new DateTime(2025, 10, 21));

            rota.Add(0, 0);
            rota.Add(3, 4); // Mesafe: 5 (3-4-5 üçgeni)

            // Act
            var mesafe = rota.ToplamMesafe();

            // Assert
            Assert.Equal(5m, Math.Round(mesafe, 2));
        }

        [Fact]
        public void Rota_ShouldReturnZeroDistance_WhenSingleStop()
        {
            // Arrange
            var rota = new UstaPlatform.Domain.Collections.Rota("U001", new DateTime(2025, 10, 21));

            rota.Add(10, 20);

            // Act
            var mesafe = rota.ToplamMesafe();

            // Assert
            Assert.Equal(0m, mesafe);
        }
    }
}
