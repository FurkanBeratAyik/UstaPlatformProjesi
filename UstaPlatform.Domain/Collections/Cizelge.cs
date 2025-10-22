using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Domain.Collections
{
    // Ustaların iş emri takvimi.
    // Indexer ile tarihe göre iş emirlerine erişim sağlar.
    public class Cizelge
    {
        // DateOnly yok, DateTime kullanıyoruz
        private readonly Dictionary<DateTime, List<is_emri>> _takvim = new Dictionary<DateTime, List<is_emri>>();

        public string UstaId { get; set; } = string.Empty;

        public Cizelge() { }

        public Cizelge(string ustaId)
        {
            UstaId = ustaId;
        }

        // Indexer - Cizelge[tarih] şeklinde kullanım
        public List<is_emri> this[DateTime tarih]
        {
            get
            {
                if (!_takvim.ContainsKey(tarih))
                {
                    _takvim[tarih] = new List<is_emri>();
                }
                return _takvim[tarih];
            }
        }

        public void IsEmriEkle(is_emri isEmri)
        {
            if (isEmri == null) throw new ArgumentNullException(nameof(isEmri));

            var tarih = isEmri.PlanlananTarih;
            if (!_takvim.ContainsKey(tarih))
            {
                _takvim[tarih] = new List<is_emri>();
            }
            _takvim[tarih].Add(isEmri);
        }

        public int ToplamIsEmriSayisi()
        {
            return _takvim.Values.Sum(liste => liste.Count);
        }

        public IEnumerable<DateTime> DoluGunler()
        {
            return _takvim.Keys.OrderBy(k => k);
        }

        public bool TarihteIsVarMi(DateTime tarih)
        {
            return _takvim.ContainsKey(tarih) && _takvim[tarih].Count > 0;
        }
    }
}

