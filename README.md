# 🧰 UstaPlatform - Şehrin Uzmanlık Platformu

**UstaPlatform**, Yozgat şehrindeki ustaları vatandaş talepleriyle eşleştiren, dinamik fiyatlama ve rota planlaması yapabilen bir programdır.  
Proje, modüler mimarisi ve plugin desteğiyle genişletilebilir bir yapıya sahiptir.

---

## 🚀 Kurulum ve Çalıştırma

### Gereksinimler
- **.NET Framework 4.8**
- **Visual Studio 2022**

### Adımlar
1. Projeyi indirin.  
2. 'UstaPlatform' klasöründe yer alan 'UstaPlatform.sln' dosyasını **restore** edin.  
3. Projeyi **derleyin**.  
4. `UstaPlatform.Plugins\bin\Debug` klasöründeki `.dll` dosyalarını  
   `UstaPlatform.App\bin\Debug\Plugins` klasörüne kopyalayın.  
5. Uygulamayı çalıştırın.


❗ eğer 'UstaPlatform' klasörü gözükmüyorsa Visual Studio ile 'Blank Solution' projesi oluşturun. ismini 'UstaPlatform' koyun. proje dosyalarını Solution'a sağ tıklayıp 'add>>existing project' ile ekleyin. sonra projelere sağ tıklayıp 'add>>reference' ile gerekli referansları ekleyin. programı çalıştıracak olan 'UstaPlatform.sln' dosyası oluştu. daha sonra 'UstaPlatformProjesi' klasöründe tüm proje klasörlerini toplayın, aşağıda yer alan 'Dosya Yapısı' gibi gözükecektir. artık uygulamayı çalıştırabilirsiniz❗

---

## 🏗️ Dosya Yapısı

Proje, **SOLID prensiplerine** uygun olarak çok katmanlı mimari ile tasarlanmıştır:

```
UstaPlatformProjesi/
├── UstaPlatform/                 # Ana Solution ('UstaPlatform.sln')
├── UstaPlatform.Domain/          # Domain modelleri ve temel varlıklar
├── UstaPlatform.Pricing/         # Fiyatlandırma motoru ve arayüzler
├── UstaPlatform.Infrastructure/  # Veri erişim katmanı (Repository)
├── UstaPlatform.App/             # Ana konsol uygulaması
├── UstaPlatform.Plugins/         # Örnek fiyatlandırma plugin'leri
└── UstaPlatform.Tests/           # Unit testler
```

---

## ⚙️ Temel Özellikler

### 1. Domain Varlıkları
- **Usta (Master):** Hizmet veren ustalar  
- **Vatandaş (Citizen):** Hizmet talep eden kişiler  
- **Talep (Request):** Vatandaş iş talepleri  
- **İş Emri (WorkOrder):** Onaylanmış ve planlanmış işler  
- **Rota (Route):** Günlük ziyaret rotaları  
- **Çizelge (Schedule):** İş emri takvimi  

### 2. İleri C# Özellikleri
- Nesne ve Koleksiyon Başlatıcılar  
- Indexer kullanımı  
- Custom `IEnumerable` (Rota koleksiyonu için)  
- Static Helper Classes (`Dogrulama`, `ParaFormatlayici`, `KonumYardimcisi`)  

### 3. SOLID Prensipleri

**S** - *Single Responsibility Principle (SRP)*  
Her sınıf tek bir sorumluluğa sahiptir:
- Domain nesneleri yalnızca veri tutar
- Repository’ler veri erişiminden sorumludur
- `PricingEngine` fiyat hesaplama yapar

**O** - *Open/Closed Principle (OCP)*  
Yeni fiyatlandırma kuralları ana kod değiştirilmeden eklenir.  
Yeni bir kural için sadece `IPricingRule` implement edilir.

**L** - *Liskov Substitution Principle (LSP)*  
Tüm fiyatlandırma kuralları `IPricingRule` ile değiştirilebilir.

**I** - *Interface Segregation Principle (ISP)*  
Arayüzler küçük ve spesifik tutulmuştur (`IPricingRule`, `IRepository`).

**D** - *Dependency Inversion Principle (DIP)*  
Üst katmanlar, somut sınıflara değil soyutlamalara bağımlıdır.

---

## 🔌 Plugin Mimarisi

### Nasıl Çalışır?
1. **IPricingRule Arayüzü:** Tüm fiyatlandırma kuralları bu arayüzü uygular.  
2. **Dynamic Loading:** `PricingEngine`, belirlenen klasördeki DLL’leri tarar.  
3. **Reflection:** `IPricingRule` implement eden sınıfları bulur ve yükler.  
4. **Composition:** Kurallar sıralı şekilde uygulanır.  
   *(Temel Ücret + Kural1 + Kural2 + ...)*

---

### Yeni Plugin Ekleme

#### 1. Yeni bir plugin projesi oluşturun:
```bash
dotnet new classlib -n UstaPlatform.Plugins.LoyaltyDiscount
```

#### 2. `IPricingRule` arayüzünü uygulayın:
```csharp
public class LoyaltyDiscountRule : IPricingRule
{
    public string Name => "Sadakat İndirimi";

    public decimal Apply(decimal currentPrice, IsEmri order)
    {
        // Örnek: %10 indirim
        return currentPrice * 0.9m;
    }
}
```

#### 3. DLL’i `Plugins` klasörüne kopyalayın.  
#### 4. Uygulamayı çalıştırın — yeni kural otomatik olarak yüklenecektir.

---

## 🧪 Testler

- `PricingEngine` dinamik yükleme testi  
- `Cizelge` indexer testi  
- `Rota` koleksiyon testi  
- Fiyat hesaplama senaryoları
