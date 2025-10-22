using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Entities
{
    public class Talep
    {
        public string Id { get; set; } = string.Empty;
        public string VatandasId { get; set; } = string.Empty;
        public string Baslik { get; set; } = string.Empty;
        public string Aciklama { get; set; } = string.Empty;
        public string UzmanlikAlani { get; set; } = string.Empty;
        public Tuple<int, int> Konum { get; set; }

        public DateTime TalepTarihi { get; set; }
        public bool Acil { get; set; }
        public DateTime KayitZamani { get; set; }

        public Talep(string talepid, string vatandasid)
        {
            Id = talepid;
            VatandasId = vatandasid;
            KayitZamani = DateTime.Now;
            TalepTarihi = DateTime.Now;
        }
    }
}
