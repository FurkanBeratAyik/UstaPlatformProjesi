using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;

namespace UstaPlatform.Infrastructure.Interfaces
{
    /// <summary>
    /// Generic repository arayüzü
    /// DIP (Dependency Inversion) - Üst katmanlar bu arayüze bağımlı
    /// </summary>

  //generic (her tür için geçerli) bir depo/arşiv arayüzü.
 //Yani üst katmanlar, veri nasıl saklanıyor ya da çekiliyor diye uğraşmak zorunda kalmıyor,sadece bu arayüzü kullanıyor.
    public interface IRepository<T> where T : class
    {
        T GetById(string id);   //ID’si verilen nesneyi getirir
        IEnumerable<T> GetAll();   //Hafızadaki tüm nesneleri listeler
        void Add(T entity);  //Yeni bir nesne ekler
        void Update(T entity); //Mevcut nesneyi günceller
        void Delete(string id); //ID’si verilen nesneyi siler
    }
}
