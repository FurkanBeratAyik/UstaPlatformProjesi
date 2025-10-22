using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Repositories
{
    public class VatandasRepository : InMemoryRepository<Vatandas>
    {
        public VatandasRepository() : base(v => v.Id) { }
    }
}
