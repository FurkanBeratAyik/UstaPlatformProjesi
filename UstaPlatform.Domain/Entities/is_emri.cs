using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class is_emri
    {
        public string Id { get; set; } = string.Empty;
        public string TalepId { get; set; } = string.Empty;
        public string UstaId { get; set; } = string.Empty;
        public decimal TemelUcret { get; set; }
        public decimal ToplamUcret { get; set; }

        public DateTime PlanlananTarih { get; set; }

        public TimeSpan PlanlananSaat { get; set; }

        public Tuple<int, int> Adres { get; set; }

        public string Durum { get; set; } = "Beklemede";
        public DateTime KayitZamani { get; set; }

        // new() yerine klasik yazım
        public Dictionary<string, decimal> FiyatDetaylari { get; set; } = new Dictionary<string, decimal>();

        public is_emri(string idx)
        {
            Id = idx;
            KayitZamani = DateTime.Now;
            PlanlananTarih = DateTime.Now.Date; // sadece tarih kısmı
            PlanlananSaat = DateTime.Now.TimeOfDay; // sadece saat kısmı
        }

        public is_emri(string idx, string idy) : this(idx)
        {
            TalepId = idy;
        }
    }
}