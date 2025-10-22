using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UstaPlatform.Domain;
using UstaPlatform.Domain.Collections;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Domain.Helpers;
using UstaPlatform.Infrastructure.Repositories;
using UstaPlatform.Infrastructure.Services;
using UstaPlatform.Pricing.Engine;

namespace UstaPlatform.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
            Console.WriteLine("║     UstaPlatform - Şehrin Uzmanlık Platformu          ║");
            Console.WriteLine("║     Yozgat Belediyesi                                 ║");
            Console.WriteLine("╚═══════════════════════════════════════════════════════╝\n");

            
                // 1. Bağımlılıkları hazırlama
                var ustaRepo = new UstaRepository();
                var vatandasRepo = new VatandasRepository();
                var talepRepo = new TalepRepository();
                var isEmriRepo = new is_emri_Repository();

                var eslestirmeServisi = new EslestirmeServisi(ustaRepo);
                var cizelgeYoneticisi = new CizelgeYoneticisi();
                var pricingEngine = new PricingEngine();

                // 2. Test verisini yükleme
                TestVerisiYukle(ustaRepo, vatandasRepo, talepRepo);

                // 3. Plugin sistemini başlatma
                PluginSisteminiBaslat(pricingEngine);

                // 4. Demo senaryosunu çalıştırma
                DemoSenaryosu(talepRepo, eslestirmeServisi, pricingEngine, cizelgeYoneticisi, isEmriRepo, ustaRepo);

                // 5. Çizelge ve rota
                CizelgeVeRotaOrnekleri(cizelgeYoneticisi);

                Console.WriteLine("\n✅ Demo başarıyla tamamlandı!");
            
            Console.WriteLine("\n\nDevam etmek için bir tuşa basın...");
            Console.ReadKey();
        }

        /// <summary>
        /// Test verisi oluşturur - Object Initializers kullanımı
        /// </summary>
        static void TestVerisiYukle(UstaRepository ustaRepo, VatandasRepository vatandasRepo, TalepRepository talepRepo)
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("📦 [1] TEST VERİSİ YÜKLEME");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Ustalar - Object Initializers ile
            var ustalar = new List<Usta>
            {
                new Usta("U001")
                {
                    AdSoyad = "Mehmet Yılmaz",
                    UzmanlikAlani = "Elektrikçi",
                    Puan = 4.8m,
                    AktifIsYuku = 1,
                    Konum = new Tuple<int, int>(10, 20)
                },
                new Usta("U002")
                {
                    AdSoyad = "Ayşe Demir",
                    UzmanlikAlani = "Elektrikçi",
                    Puan = 4.9m,
                    AktifIsYuku = 3,
                    Konum = new Tuple<int, int>(15, 25)
                },
                new Usta("U003")
                {
                    AdSoyad = "Ali Kaya",
                    UzmanlikAlani = "Tesisatçı",
                    Puan = 4.5m,
                    AktifIsYuku = 5,
                    Konum = new Tuple<int, int>(8, 18)
                },
                new Usta("U004")
                {
                    AdSoyad = "Zeynep Aksoy",
                    UzmanlikAlani = "Marangoz",
                    Puan = 4.7m,
                    AktifIsYuku = 3,
                    Konum = new Tuple<int, int>(20, 30)
                }
            };

            foreach (var usta in ustalar)
            {
                ustaRepo.Add(usta);
            }

            // Vatandaşlar
            var vatandaslar = new List<Vatandas>
            {
                new Vatandas("V001")
                {
                    AdSoyad = "Ahmet Yılmaz",
                    Telefon = "0555-111-2233",
                    Konum = new Tuple<int, int>(12, 22)
                },
                new Vatandas("V002")
                {
                    AdSoyad = "Fatma Özkan",
                    Telefon = "0555-444-5566",
                    Konum = new Tuple<int, int>(18, 28)
                }
            };

            foreach (var vatandas in vatandaslar)
            {
                vatandasRepo.Add(vatandas);
            }

            // Talepler
            var talepler = new List<Talep>
            {
                new Talep("T001", "V001")
                {
                    Baslik = "Lavabo sızıntısı",
                    Aciklama = "Mutfak lavabosunda ciddi sızıntı var",
                    UzmanlikAlani = "Tesisatçı",
                    Konum = new Tuple<int, int>(12, 22),
                    TalepTarihi = new DateTime(2025, 10, 21),
                    Acil = true
                },
                new Talep("T002", "V002")
                {
                    Baslik = "Elektrik panosu arızası",
                    Aciklama = "Ana elektrik panosunda arıza",
                    UzmanlikAlani = "Elektrikçi",
                    Konum = new Tuple<int, int>(18, 28),
                    TalepTarihi = new DateTime(2025, 10, 22),
                    Acil = false
                }
            };

            foreach (var talep in talepler)
            {
                talepRepo.Add(talep);
            }
 
            Console.WriteLine("✅ {0} usta yüklendi", ustalar.Count);
            Console.WriteLine("✅ {0} vatandaş yüklendi", vatandaslar.Count);
            Console.WriteLine("✅ {0} talep yüklendi\n", talepler.Count);

            /*
            var u = ustaRepo.GetAll().First();

            foreach (var usta in ustaRepo.GetAll())
            {
                Console.WriteLine($"Usta: {usta.AdSoyad}, Uzmanlık: {usta.UzmanlikAlani}");
            } 
           */

        }

        /// <summary>
        /// Plugin sistemini başlatır - OCP'nin kalbi
        /// </summary>
        static void PluginSisteminiBaslat(PricingEngine engine)
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("🔌 [2] PLUGIN SİSTEMİ BAŞLATILIYOR");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Plugins klasörünü hazırla
            var pluginPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Plugins");
            System.IO.Directory.CreateDirectory(pluginPath);

            Console.WriteLine("📁 Plugin Klasörü: {0}", pluginPath);

            // Plugin'leri yüklemeyi dene 
            try
            {
                engine.LoadRulesFromDirectory(pluginPath);
            }
            catch (ReflectionTypeLoadException ex)
            {
                Console.WriteLine(" LoaderExceptions yakalandı:");
                foreach (var le in ex.LoaderExceptions)
                {
                    Console.WriteLine(le.Message);
                }
            }

            // Yüklü kuralları göster
            if (engine.LoadedRules.Count > 0)
            {
                engine.PrintLoadedRules();
            }
            else
            {
                Console.WriteLine("⚠️  Hiç plugin yüklenmedi. \n");

                // Temel kuralları manuel ekle
                engine.AddRule(new UstaPlatform.Pricing.Rules.TemelUcretKurali());
                engine.AddRule(new UstaPlatform.Pricing.Rules.HaftaSonuEkUcretiKurali());
                engine.AddRule(new UstaPlatform.Pricing.Rules.AcilCagriUcretiKurali());

                Console.WriteLine("✅ 3 temel kural manuel eklendi\n");
            }
        }

        /// <summary>
        /// Ana demo senaryosu
        /// </summary>
        static void DemoSenaryosu(
            TalepRepository talepRepo,
            EslestirmeServisi eslestirme,
            PricingEngine pricing,
            CizelgeYoneticisi cizelge,
            is_emri_Repository isEmriRepo,
            UstaRepository ustaRepo)
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("🎬 [3] DEMO SENARYOSU BAŞLIYOR");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            var talepler = talepRepo.GetAll().ToList();

            for (int i = 0; i < talepler.Count; i++)
            {
                var talep = talepler[i];

                Console.WriteLine("───────────────────────────────────────────────────────");
                Console.WriteLine("📝 Talep #{0}: {1}", i + 1, talep.Baslik);
                Console.WriteLine("───────────────────────────────────────────────────────");
                Console.WriteLine("Vatandaş ID: {0}", talep.VatandasId);
                Console.WriteLine("Uzmanlık: {0}", talep.UzmanlikAlani);
                Console.WriteLine("Tarih: {0:dd MMMM yyyy}", talep.TalepTarihi);
                Console.WriteLine("Acil: {0}", talep.Acil ? "Evet ⚡" : "Hayır");
                Console.WriteLine("Açıklama: {0}\n", talep.Aciklama);

                // Usta eşleştirme servisi
                var usta = eslestirme.TalepIcinUstaBul(talep);
                if (usta == null)
                {
                    Console.WriteLine("❌ Uygun usta bulunamadı!\n");
                    continue;
                }

                // İş emri oluştur - Object Initializer
                var isEmri = new is_emri(
                    "WO-" + Guid.NewGuid().ToString().Substring(0, 8),
                    talep.Id)
                {
                    UstaId = usta.Id,
                    TemelUcret = 500m,
                    PlanlananTarih = talep.TalepTarihi.Date, // sadece tarih kısmı
                    PlanlananSaat = new TimeSpan(9, 0, 0),   // 09:00
                    Adres = talep.Konum,
                    Durum = talep.Acil ? "Acil" : "Normal"
                };

                // Fiyatlandırma hesaplama
                var pricingResult = pricing.Calculate(isEmri, isEmri.TemelUcret);
                isEmri.ToplamUcret = pricingResult.FinalPrice;

                // Fiyat detaylarını ekle
                foreach (var rule in pricingResult.AppliedRules)
                {
                    isEmri.FiyatDetaylari[rule.RuleName] = rule.Adjustment;
                }

                // Kaydet
                isEmriRepo.Add(isEmri);

                // Çizelgeye ekle
                cizelge.IsEmriEkle(isEmri);

                // Usta iş yükünü güncelleme
                usta.AktifIsYuku++;
                ustaRepo.Update(usta);

                Console.WriteLine("✅ İş emri oluşturuldu: {0}", isEmri.Id);
                Console.WriteLine("   Toplam Ücret: {0}", ParaFormatlayici.Formatla(isEmri.ToplamUcret));
                Console.WriteLine("   Durum: {0}\n", isEmri.Durum);
            }
        }

        /// <summary>
        /// Çizelge ve Rota örnekleri - İleri C# özellikleri
        /// </summary>
        static void CizelgeVeRotaOrnekleri(CizelgeYoneticisi yonetici)
        {
            Console.WriteLine("═══════════════════════════════════════════════════════");
            Console.WriteLine("📊 [4] ÇİZELGE ve ROTA ÖRNEKLERİ");
            Console.WriteLine("═══════════════════════════════════════════════════════\n");

            // Çizelge Indexer örneği
            Console.WriteLine("📅 Çizelge Indexer Kullanımı:");
            yonetici.TarihtekiIslerinListesiniYazdir("U001", new DateTime(2025, 10, 21));

            // Rota koleksiyon örneği - Collection Initializer
            Console.WriteLine("🗺️  Rota Koleksiyonu Oluşturma (Collection Initializer):\n");

            var rota = new Rota("U001", new DateTime(2025, 10, 21));

            // Collection initializer syntax
            rota.Add(10, 20);  // Başlangıç noktası
            rota.Add(12, 22);  // 1. durak
            rota.Add(15, 25);  // 2. durak
            rota.Add(18, 28);  // 3. durak
            rota.Add(10, 20);  // Dönüş

            Console.WriteLine("Usta: {0}", rota.UstaId);
            Console.WriteLine("Tarih: {0:dd MMMM yyyy}", rota.Tarih);
            Console.WriteLine("Durak Sayısı: {0}", rota.DurakSayisi);
            Console.WriteLine("Toplam Mesafe: {0:F2} km\n", rota.ToplamMesafe());

            Console.WriteLine("Duraklar:");
            int i = 0;
            // IEnumerable kullanımı
            foreach (var durak in rota)
            {
                Console.WriteLine("   {0}. ({1}, {2})", i++, durak.Item1, durak.Item2);
            }
            Console.WriteLine();

            // Static helper kullanımı
            Console.WriteLine("🔧 Static Helper Sınıfları:\n");

            var mesafe = KonumYardimcisi.Mesafe(new Tuple<int, int>(10, 20), new Tuple<int, int>(15, 25));
            Console.WriteLine("Mesafe Hesaplama: {0:F2} km", mesafe);

            var yakinMi = KonumYardimcisi.YakinMi(new Tuple<int, int>(10, 20), new Tuple<int, int>(12, 22));
            Console.WriteLine("Yakınlık Kontrolü: {0}", yakinMi ? "Yakın ✓" : "Uzak ✗");

            var formatli = ParaFormatlayici.Formatla(1234.56m);
            Console.WriteLine("Para Formatlama: {0}", formatli);

            var degisim = ParaFormatlayici.FormatlaDegisim(1000m, 1250m);
            Console.WriteLine("Değişim Formatlama: {0}\n", degisim);
        }
    }
}