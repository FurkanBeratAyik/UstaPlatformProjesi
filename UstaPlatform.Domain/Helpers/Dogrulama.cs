using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UstaPlatform.Domain.Helpers
{
    // Doğrulama (validation) için statik yardımcı sınıf
    public static class Dogrulama
    {
        public static void NotNull(object value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void NotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"{paramName} boş olamaz.", paramName);
            }
        }

        public static void Positive(decimal value, string paramName)
        {
            if (value <= 0)
            {
                throw new ArgumentException($"{paramName} pozitif olmalıdır.", paramName);
            }
        }
    }
}
