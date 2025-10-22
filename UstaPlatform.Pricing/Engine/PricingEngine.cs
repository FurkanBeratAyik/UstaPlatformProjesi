using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UstaPlatform.Domain.Entities;
using UstaPlatform.Pricing.Interfaces;

namespace UstaPlatform.Pricing.Engine
{
    /// <summary>
    /// Fiyatlandırma motoru - Plugin mimarisinin kalbi.
    /// DLL'lerden dinamik olarak fiyat kurallarını yükler ve uygular.
    /// DIP (Dependency Inversion) - IPricingRule'a bağımlı, somut sınıflara değil.
    /// </summary>
    public class PricingEngine
    {
        private readonly List<IPricingRule> _rules = new List<IPricingRule>();

        public IReadOnlyList<IPricingRule> LoadedRules => _rules.AsReadOnly();

        /// <summary>
        /// Belirtilen klasördeki DLL'lerden fiyat kurallarını yükler
        /// </summary>
        public void LoadRulesFromDirectory(string pluginDirectory)
        {
            if (!Directory.Exists(pluginDirectory))
            {
                Console.WriteLine("⚠️  Plugin klasörü bulunamadı: {0}", pluginDirectory);
                return;
            }

            var dllFiles = Directory.GetFiles(pluginDirectory, "*.dll");
            Console.WriteLine("📁 {0} adet DLL dosyası bulundu.", dllFiles.Length);

            foreach (var dllPath in dllFiles)
            {
                try
                {
                    LoadRulesFromAssembly(dllPath);
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Console.WriteLine("  ❌ ReflectionTypeLoadException ({0}):", Path.GetFileName(dllPath));
                    foreach (var le in ex.LoaderExceptions)
                    {
                        if (le != null)
                            Console.WriteLine("    - {0}", le.Message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("  ❌ Hata ({0}): {1}", Path.GetFileName(dllPath), ex.Message);
                }
            }

            Console.WriteLine("✅ Toplam {0} adet fiyatlandırma kuralı yüklendi.\n", _rules.Count);
        }

        /// <summary>
        /// Tek bir assembly'den kuralları yükler
        /// </summary>
        private void LoadRulesFromAssembly(string assemblyPath)
        {
            Console.WriteLine("  🔍 Yükleniyor: {0}", Path.GetFileName(assemblyPath));

            var assembly = Assembly.LoadFrom(assemblyPath);
            Console.WriteLine("  ✓ Assembly yüklendi: {0}", assembly.FullName);

            var allTypes = assembly.GetTypes();
            Console.WriteLine("  📋 Toplam {0} tip bulundu", allTypes.Length);

            var ruleTypes = allTypes
                .Where(t => typeof(IPricingRule).IsAssignableFrom(t)
                         && !t.IsInterface
                         && !t.IsAbstract)
                .ToList();

            Console.WriteLine("  🎯 IPricingRule implement eden {0} tip bulundu", ruleTypes.Count);

            foreach (var type in ruleTypes)
            {
                Console.WriteLine("    → {0} instantiate ediliyor...", type.Name);
                var rule = (IPricingRule)Activator.CreateInstance(type);
                if (rule != null)
                {
                    _rules.Add(rule);
                    Console.WriteLine("    ✓ {0} yüklendi", rule.Name);
                }
            }
        }

        /// <summary>
        /// Kuralı manuel olarak ekler (test veya programatik ekleme için)
        /// </summary>
        public void AddRule(IPricingRule rule)
        {
            if (rule == null)
                throw new ArgumentNullException(nameof(rule));

            _rules.Add(rule);
        }

        /// <summary>
        /// Tüm kuralları uygulayarak fiyatı hesaplar
        /// </summary>
        public PricingResult Calculate(is_emri order, decimal basePrice)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var result = new PricingResult
            {
                BasePrice = basePrice,
                FinalPrice = basePrice
            };

            Console.WriteLine($"\n💰 Fiyat Hesaplaması Başlıyor...");
            Console.WriteLine($"   Temel Ücret: {basePrice:N2} TL\n");

            foreach (var rule in _rules)
            {
                if (!rule.IsApplicable(order))
                {
                    Console.WriteLine($"   ⊘ {rule.Name}: Uygulanamaz");
                    continue;
                }

                var beforePrice = result.FinalPrice;
                result.FinalPrice = rule.Apply(result.FinalPrice, order);
                var adjustment = result.FinalPrice - beforePrice;

                result.AppliedRules.Add(new RuleApplication
                {
                    RuleName = rule.Name,
                    Description = rule.Description,
                    PriceBefore = beforePrice,
                    PriceAfter = result.FinalPrice,
                    Adjustment = adjustment
                });

                var sign = adjustment >= 0 ? "+" : "";
                Console.WriteLine($"   ✓ {rule.Name}: {sign}{adjustment:N2} TL → Toplam: {result.FinalPrice:N2} TL");
            }

            Console.WriteLine($"\n   📊 Nihai Fiyat: {result.FinalPrice:N2} TL");
            Console.WriteLine($"   📈 Toplam Değişim: {(result.FinalPrice - basePrice):+0.00;-0.00;0} TL\n");

            return result;
        }

        /// <summary>
        /// Yüklü kuralları listeler
        /// </summary>
        public void PrintLoadedRules()
        {
            Console.WriteLine("📋 Yüklü Fiyatlandırma Kuralları:");
            foreach (var rule in _rules)
            {
                Console.WriteLine($"   • {rule.Name}");
                Console.WriteLine($"     └─ {rule.Description}");
            }
            Console.WriteLine();
        }
    }
}
