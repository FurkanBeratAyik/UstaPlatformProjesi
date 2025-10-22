using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Repositories
{
    public class TalepRepository : InMemoryRepository<Talep>
    {
        public TalepRepository() : base(t => t.Id) { }

        public IEnumerable<Talep> GetByVatandasId(string vatandasId)
        {
            return GetAll().Where(t => t.VatandasId == vatandasId);
        }

        public IEnumerable<Talep> GetAcilTalepler()
        {
            return GetAll().Where(t => t.Acil);
        }
    }
}
