using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Collections;
using Xunit;

namespace UstaPlatform.Tests
{
    /// <summary>
    /// Cizelge için birim testleri
    /// Indexer kullanımını ve iş emri yönetimini test eder
    /// </summary>
    public class CizelgeTests
    {
        [Fact]
        public void Cizelge_Indexer_ShouldReturnEmptyList_ForNewDate()
        {
            // Arrange
            var cizelge = new Cizelge("U001");
            var tarih = new DateTime(2025, 10, 21);

            // Act
            var isler = cizelge[tarih]; // Indexer kullanımı

            // Assert
            Assert.NotNull(isler);
            Assert.Empty(isler);
        }

        [Fact]
        public void Cizelge_Indexer_ShouldReturnWorkOrders_AfterAdding()
        {
            // Arrange
            var cizelge = new Cizelge("U001");
            var tarih = new DateTime(2025, 10, 21);

            var isEmri = new is_emri("WO-001", "T-001")
            {
                UstaId = "U001",
                PlanlananTarih = tarih,
                TemelUcret = 500m
            };

            // Act
            cizelge.IsEmriEkle(isEmri);
            var isler = cizelge[tarih]; // Indexer kullanımı

            // Assert
            Assert.Single(isler);
            Assert.Equal("WO-001", isler[0].Id);
        }

        [Fact]
        public void Cizelge_ShouldAddMultipleWorkOrders_ToSameDate()
        {
            // Arrange
            var cizelge = new UstaPlatform.Domain.Collections.Cizelge("U001");
            var tarih = new DateTime(2025, 10, 21);

            // Act
            for (int i = 1; i <= 3; i++)
            {
                cizelge.IsEmriEkle(new is_emri(
                    string.Format("WO-{0:D3}", i),
                    string.Format("T-{0:D3}", i))
                {
                    UstaId = "U001",
                    PlanlananTarih = tarih,
                    TemelUcret = 500m
                });
            }

            // Assert
            Assert.Equal(3, cizelge[tarih].Count);
            Assert.Equal(3, cizelge.ToplamIsEmriSayisi());
        }

        [Fact]
        public void Cizelge_ShouldSeparateWorkOrders_ByDate()
        {
            // Arrange
            var cizelge = new UstaPlatform.Domain.Collections.Cizelge("U001");
            var tarih1 = new DateTime(2025, 10, 21);
            var tarih2 = new DateTime(2025, 10, 22);

            // Act
            cizelge.IsEmriEkle(new is_emri("WO-001", "T-001")
            {
                UstaId = "U001",
                PlanlananTarih = tarih1,
                TemelUcret = 500m
            });

            cizelge.IsEmriEkle(new is_emri("WO-002", "T-002")
            {
                UstaId = "U001",
                PlanlananTarih = tarih2,
                TemelUcret = 600m
            });

            // Assert
            Assert.Single(cizelge[tarih1]);
            Assert.Single(cizelge[tarih2]);
            Assert.Equal(2, cizelge.ToplamIsEmriSayisi());
        }

        [Fact]
        public void Cizelge_TarihteIsVarMi_ShouldReturnCorrectly()
        {
            // Arrange
            var cizelge = new UstaPlatform.Domain.Collections.Cizelge("U001");
            var tarih = new DateTime(2025, 10, 21);
            var bosTarih = new DateTime(2025, 10, 22);

            cizelge.IsEmriEkle(new is_emri("WO-001", "T-001")
            {
                UstaId = "U001",
                PlanlananTarih = tarih,
                TemelUcret = 500m
            });

            // Act & Assert
            Assert.True(cizelge.TarihteIsVarMi(tarih));
            Assert.False(cizelge.TarihteIsVarMi(bosTarih));
        }
    }
}
