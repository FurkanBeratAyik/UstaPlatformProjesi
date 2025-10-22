using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Repositories
{   public class is_emri_Repository : InMemoryRepository<is_emri>
    {
        public is_emri_Repository() : base(wo => wo.Id) { }

        public IEnumerable<is_emri> GetByUstaId(string ustaId)
        {
            return GetAll().Where(wo => wo.UstaId == ustaId);
        }

        public IEnumerable<is_emri> GetByTarih(DateTime tarih)
        {
            return GetAll().Where(wo => wo.PlanlananTarih.Date == tarih.Date);
        }

        public IEnumerable<is_emri> GetByDurum(string durum)
        {
            return GetAll().Where(wo => wo.Durum.Equals(durum, StringComparison.OrdinalIgnoreCase));
        }
    }
}
