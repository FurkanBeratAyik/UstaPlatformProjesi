using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using UstaPlatform.Domain.Helpers;

namespace UstaPlatform.Domain.Collections
{

    // Bir uzmanın günlük ziyaret rotası.
    // IEnumerable<(int X, int Y)> uygular.
    public class Rota : IEnumerable<(int X, int Y)>
    {
        private readonly List<(int X, int Y)> _duruklar = new List<(int X, int Y)>();
        private string v;
        private DateTime dateTime;

        public Rota(string v, DateTime dateTime)
        {
            this.v = v;
            this.dateTime = dateTime;
        }

        public string UstaId { get; set; } = string.Empty;

        public DateTime Tarih { get; set; }

        public int DurakSayisi
        {
            get { return _duruklar.Count; }
        }

        public void Add(int x, int y)
        {
            _duruklar.Add((x, y));
        }

        public void DurakEkle((int X, int Y) konum)
        {
            _duruklar.Add(konum);
        }

        public decimal ToplamMesafe()
        {
            if (_duruklar.Count < 2) return 0;

            decimal toplam = 0;
            for (int i = 0; i < _duruklar.Count - 1; i++)
            {
                var d1 = _duruklar[i];
                var d2 = _duruklar[i + 1];
                toplam += KonumYardimcisi.Mesafe(d1, d2);
            }
            return toplam;
        }

        public IEnumerator<(int X, int Y)> GetEnumerator()
        {
            return _duruklar.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}