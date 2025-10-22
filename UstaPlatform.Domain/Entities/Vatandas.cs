using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class Vatandas
    {
        public string Id { get; set; } = string.Empty;
        public string AdSoyad { get; set; } = string.Empty;
        public string Telefon { get; set; } = string.Empty;
        public Tuple<int, int> Konum { get; set; }

        public DateTime KayitZamani { get; set; }

        public Vatandas(string id)
        {
            Id = id;
            KayitZamani = DateTime.Now;
        }
    }
}
