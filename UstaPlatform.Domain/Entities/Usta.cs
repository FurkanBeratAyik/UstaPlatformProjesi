using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class Usta
    {
        public string Id { get; set; } = string.Empty;
        public string AdSoyad { get; set; } = string.Empty;
        public string UzmanlikAlani { get; set; } = string.Empty;
        public decimal Puan { get; set; }
        public int AktifIsYuku { get; set; }
        public DateTime KayitZamani { get; set; }
        public Tuple<int, int> Konum { get; set; }


        public Usta(string id)
        {
            Id = id;
            KayitZamani = DateTime.Now;
        }
    }
}
