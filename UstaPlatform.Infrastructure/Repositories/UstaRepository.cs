using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Repositories
{
    public class UstaRepository : InMemoryRepository<Usta>
    {
        public UstaRepository() : base(u => u.Id) { }

        public IEnumerable<Usta> GetByUzmanlikAlani(string uzmanlikAlani)
        {
            return GetAll().Where(u => u.UzmanlikAlani.Equals(uzmanlikAlani, StringComparison.OrdinalIgnoreCase));
        }     // hafızadaki tüm ustaların uzmanlık alanını alıp karşılaştırma 

        public Usta GetEnAzYogunUsta(string uzmanlikAlani)
        {
            return GetByUzmanlikAlani(uzmanlikAlani)
                .OrderBy(u => u.AktifIsYuku)
                .ThenByDescending(u => u.Puan)
                .FirstOrDefault();
        }
    }
}
