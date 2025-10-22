using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
    public static class ParaFormatlayici
    {
        public static string Formatla(decimal tutar)
        {
            return $"{tutar:N2} TL";
        }

        public static string FormatlaDegisim(decimal eski, decimal yeni)
        {
            var fark = yeni - eski;
            var isaret = fark >= 0 ? "+" : "";
            return $"{isaret}{Formatla(fark)}";
        }
    }
}
