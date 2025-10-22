using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Collections;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Services
{
    public class CizelgeYoneticisi
    {
        private readonly Dictionary<string, Cizelge> _ustacizelgeleri = new Dictionary<string, Cizelge>();

        public Cizelge GetOrCreateSchedule(string ustaId)
        {
            if (!_ustacizelgeleri.ContainsKey(ustaId))
            {
                _ustacizelgeleri[ustaId] = new Cizelge { UstaId = ustaId };
            }
            return _ustacizelgeleri[ustaId];
        }

        public void IsEmriEkle(is_emri isEmri)
        {
            if (isEmri == null) throw new ArgumentNullException("isEmri");

            var cizelge = GetOrCreateSchedule(isEmri.UstaId);
            cizelge.IsEmriEkle(isEmri);

            Console.WriteLine($"📅 İş emri çizelgeye eklendi:");
            Console.WriteLine($"   Usta: {isEmri.UstaId}");
            Console.WriteLine($"   Tarih: {isEmri.PlanlananTarih:dd MMMM yyyy}");
            Console.WriteLine($"   Toplam İş: {cizelge[isEmri.PlanlananTarih].Count} adet\n");
        }

        public void TarihtekiIslerinListesiniYazdir(string ustaId, DateTime tarih)
        {
            var cizelge = GetOrCreateSchedule(ustaId);
            var isler = cizelge[tarih]; // Indexer kullanımı

            Console.WriteLine($"📋 {tarih:dd MMMM yyyy} tarihindeki işler:");
            if (isler.Count == 0)
                Console.WriteLine("   (Boş)");
            else
            {
                foreach (var is_ in isler)
                {
                    Console.WriteLine($"   • {is_.Id} - {is_.ToplamUcret:N2} TL - {is_.PlanlananSaat:hh\\:mm}");
                }
            }
            Console.WriteLine();
        }
    }
}
