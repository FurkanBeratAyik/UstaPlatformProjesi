using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
        public static class KonumYardimcisi
        {
            // Tuple<int,int> sürümü (eski)
            public static decimal Mesafe(Tuple<int, int> konum1, Tuple<int, int> konum2)
            {
                var dx = konum2.Item1 - konum1.Item1;
                var dy = konum2.Item2 - konum1.Item2;
                return (decimal)Math.Sqrt(dx * dx + dy * dy);
            }

            public static bool YakinMi(Tuple<int, int> konum1, Tuple<int, int> konum2, decimal esikMesafe = 10)
            {
                return Mesafe(konum1, konum2) <= esikMesafe;
            }

            // (int X,int Y) tuple sürümü (Rota için)
            internal static decimal Mesafe((int X, int Y) d1, (int X, int Y) d2)
            {
                var dx = d2.X - d1.X;
                var dy = d2.Y - d1.Y;
                return (decimal)Math.Sqrt(dx * dx + dy * dy);
            }

            internal static bool YakinMi((int X, int Y) d1, (int X, int Y) d2, decimal esikMesafe = 10)
            {
                return Mesafe(d1, d2) <= esikMesafe;
            }
        }

        /*public static decimal Mesafe(Tuple<int, int> konum1, Tuple<int, int> konum2)
        {
            var dx = konum2.Item1 - konum1.Item1;
            var dy = konum2.Item2 - konum1.Item2;
            return (decimal)Math.Sqrt(dx * dx + dy * dy);
        }

        public static bool YakinMi(Tuple<int, int> konum1, Tuple<int, int> konum2, decimal esikMesafe = 10)
        {
            return Mesafe(konum1, konum2) <= esikMesafe;
        }

        internal static decimal Mesafe((int X, int Y) d1, (int X, int Y) d2)
        {
            throw new NotImplementedException();
        }*/
}

