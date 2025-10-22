using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Infrastructure.Repositories;

namespace UstaPlatform.Infrastructure.Services
{
    public class EslestirmeServisi
    {
        private readonly UstaRepository _ustaRepository;

        public EslestirmeServisi(UstaRepository ustaRepository)
        {
            _ustaRepository = ustaRepository ?? throw new ArgumentNullException("ustaRepository");
        }

        public Usta TalepIcinUstaBul(Talep talep)
        {
            if (talep == null) throw new ArgumentNullException("talep");

            var uygunUstalar = _ustaRepository.GetByUzmanlikAlani(talep.UzmanlikAlani); 
            //ustaların uzmanlık alanı UstaRepository içindeki GetByUzmanlikAlani metodu ile çekiliyor.
            
            /* talep.UzmanlikAlani parametresi, talep edilen uzmanlık alanını temsil ediyor.

Repository, veritabanından veya mock datadan bu alana sahip ustaları getiriyor.

Sonra gelen listeye göre seçimi yapıyor: en az aktif iş yüküne sahip olan + puanı yüksek olan usta seçiliyor.
             */

            if (!uygunUstalar.Any())
            {
                Console.WriteLine($"⚠️  '{talep.UzmanlikAlani}' uzmanlık alanında usta bulunamadı!");
                return null;
            }

            var secilenUsta = uygunUstalar
                .OrderBy(u => u.AktifIsYuku)
                .ThenByDescending(u => u.Puan)
                .First();

            Console.WriteLine($"✓ Seçilen Usta: {secilenUsta.AdSoyad}");
            Console.WriteLine($"  Puan: {secilenUsta.Puan:F1}/5.0");
            Console.WriteLine($"  Aktif İş: {secilenUsta.AktifIsYuku} adet\n");

            return secilenUsta;
        }
    }
}
