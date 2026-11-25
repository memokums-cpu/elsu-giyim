using ElsuGiyim.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElsuGiyim.Infrastructure.Persistence.Seed;

/// <summary>
/// DEMO VERİDİR. Gerçek ürünler Aşama 4'teki yönetim paneli üzerinden
/// eklenecek. Sabit Guid'ler kullanıyoruz çünkü HasData, migration'lar
/// arasında deterministik (hep aynı) ID bekliyor.
/// </summary>
public static class DemoDataSeeder
{
    private static readonly DateTime SeedDate = new(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    // --- Kategoriler ---
    private static readonly Guid CatKadin = Guid.Parse("11111111-0000-0000-0000-000000000001");
    private static readonly Guid CatErkek = Guid.Parse("11111111-0000-0000-0000-000000000002");
    private static readonly Guid CatAyakkabi = Guid.Parse("11111111-0000-0000-0000-000000000003");
    private static readonly Guid CatAksesuar = Guid.Parse("11111111-0000-0000-0000-000000000004");

    // --- Ürünler ---
    private static readonly Guid PKetenGomlek = Guid.Parse("22222222-0000-0000-0000-000000000001");
    private static readonly Guid POversizeBlazer = Guid.Parse("22222222-0000-0000-0000-000000000002");
    private static readonly Guid PPamukluTisort = Guid.Parse("22222222-0000-0000-0000-000000000003");
    private static readonly Guid PDeriCanta = Guid.Parse("22222222-0000-0000-0000-000000000004");
    private static readonly Guid PYunKazak = Guid.Parse("22222222-0000-0000-0000-000000000005");
    private static readonly Guid PKotPantolon = Guid.Parse("22222222-0000-0000-0000-000000000006");
    private static readonly Guid PTrenckot = Guid.Parse("22222222-0000-0000-0000-000000000007");
    private static readonly Guid PSneaker = Guid.Parse("22222222-0000-0000-0000-000000000008");
    private static readonly Guid PMiniElbise = Guid.Parse("22222222-0000-0000-0000-000000000009");

    // --- Roller ve kullanıcı ---
    private static readonly Guid RoleAdminId = Guid.Parse("55555555-0000-0000-0000-000000000001");
    private static readonly Guid RoleUrunYoneticisiId = Guid.Parse("55555555-0000-0000-0000-000000000002");
    private static readonly Guid RoleSiparisYoneticisiId = Guid.Parse("55555555-0000-0000-0000-000000000003");
    private static readonly Guid AdminUserId = Guid.Parse("66666666-0000-0000-0000-000000000001");

    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(
            new Role { Id = RoleAdminId, Name = RoleNames.Admin, CreatedAtUtc = SeedDate },
            new Role { Id = RoleUrunYoneticisiId, Name = RoleNames.UrunYoneticisi, CreatedAtUtc = SeedDate },
            new Role { Id = RoleSiparisYoneticisiId, Name = RoleNames.SiparisYoneticisi, CreatedAtUtc = SeedDate }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = AdminUserId,
                Email = "admin@elsugiyim.com",
                FullName = "Mağaza Sahibi (Demo)",
                // DEMO ŞİFRE: Elsu2026! — sabit salt ile önceden hesaplanmış hash.
                // İlk girişten sonra MUTLAKA değiştirilmeli (Aşama 4'te "şifre
                // değiştir" ekranı eklenecek).
                PasswordHash = "210000.AAECAwQFBgcICQoLDA0ODw==.VMUd18nJtIldSv6u5JUCqVab5MaFcvk+Cu5FxnTXuzQ=",
                CreatedAtUtc = SeedDate,
            }
        );

        modelBuilder.Entity<UserRole>().HasData(
            new UserRole { UserId = AdminUserId, RoleId = RoleAdminId }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = CatKadin, Name = "Kadın", Slug = "kadin", DisplayOrder = 1, CreatedAtUtc = SeedDate },
            new Category { Id = CatErkek, Name = "Erkek", Slug = "erkek", DisplayOrder = 2, CreatedAtUtc = SeedDate },
            new Category { Id = CatAyakkabi, Name = "Ayakkabı", Slug = "ayakkabi", DisplayOrder = 3, CreatedAtUtc = SeedDate },
            new Category { Id = CatAksesuar, Name = "Aksesuar", Slug = "aksesuar", DisplayOrder = 4, CreatedAtUtc = SeedDate }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = PKetenGomlek, Name = "Keten Gömlek", Slug = "keten-gomlek", SubCategory = "Gömlek",
                CategoryId = CatErkek, Price = 1490m,
                Description = "Nefes alabilen %100 keten kumaştan üretilmiş, yaz aylarına uygun rahat kesim gömlek.",
                Fabric = "%100 Keten", CareInstructions = "30°C'de yıkayın, düşük ısıda ütüleyin.",
                Fit = FitType.RegularFit, ModelInfo = "Manken 186 cm, 76 kg, M beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"S\",\"Chest\":\"96-100 cm\",\"Length\":\"72 cm\"},{\"Size\":\"M\",\"Chest\":\"100-104 cm\",\"Length\":\"74 cm\"},{\"Size\":\"L\",\"Chest\":\"104-108 cm\",\"Length\":\"76 cm\"},{\"Size\":\"XL\",\"Chest\":\"108-112 cm\",\"Length\":\"78 cm\"}]",
                IsNew = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = POversizeBlazer, Name = "Oversize Blazer", Slug = "oversize-blazer", SubCategory = "Ceket",
                CategoryId = CatKadin, Price = 2390m, SalePrice = 1990m,
                Description = "Yapılandırılmamış omuzlara sahip, oversize blazer.",
                Fabric = "%70 Yün, %30 Polyester", CareInstructions = "Kuru temizlemeye verin.",
                Fit = FitType.Oversize, ModelInfo = "Manken 178 cm, 58 kg, S beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"XS\",\"Chest\":\"88-92 cm\"},{\"Size\":\"S\",\"Chest\":\"92-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-100 cm\"},{\"Size\":\"L\",\"Chest\":\"100-104 cm\"}]",
                IsNew = true, IsBestSeller = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PPamukluTisort, Name = "Pamuklu Tişört", Slug = "pamuklu-tisort", SubCategory = "Tişört",
                CategoryId = CatErkek, Price = 690m,
                Description = "Günlük kullanıma uygun, %100 organik pamuktan temel tişört.",
                Fabric = "%100 Organik Pamuk", CareInstructions = "40°C'de yıkayın.",
                Fit = FitType.RegularFit, ModelInfo = "Manken 184 cm, 74 kg, M beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"S\",\"Chest\":\"94-98 cm\"},{\"Size\":\"M\",\"Chest\":\"98-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-106 cm\"},{\"Size\":\"XL\",\"Chest\":\"106-110 cm\"}]",
                IsNew = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PDeriCanta, Name = "Deri Çanta", Slug = "deri-canta", SubCategory = "Çanta",
                CategoryId = CatAksesuar, Price = 3190m,
                Description = "El yapımı hakiki deri, günlük kullanıma uygun omuz çantası.",
                Fabric = "Hakiki Deri", CareInstructions = "Nemden koruyun, deri bakım kremi ile temizleyin.",
                IsNew = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PYunKazak, Name = "Yün Kazak", Slug = "yun-kazak", SubCategory = "Sweatshirt",
                CategoryId = CatKadin, Price = 1890m,
                Description = "Yumuşak merinos yünden örülmüş, bol kesim kazak.",
                Fabric = "%100 Merinos Yün", CareInstructions = "Elde, soğuk suda yıkayın.",
                Fit = FitType.GenisKalip, ModelInfo = "Manken 175 cm, 55 kg, S beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"S\",\"Chest\":\"90-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-108 cm\"}]",
                CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PKotPantolon, Name = "Klasik Kot Pantolon", Slug = "klasik-kot-pantolon", SubCategory = "Pantolon",
                CategoryId = CatErkek, Price = 1190m,
                Description = "Zamansız kesim, orta bel, düz paça kot pantolon.",
                Fabric = "%98 Pamuk, %2 Elastan", CareInstructions = "30°C'de ters çevirerek yıkayın.",
                Fit = FitType.RegularFit, ModelInfo = "Manken 186 cm, 76 kg, 32 beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"30\",\"Waist\":\"76-80 cm\",\"Length\":\"102 cm\"},{\"Size\":\"32\",\"Waist\":\"81-85 cm\",\"Length\":\"104 cm\"},{\"Size\":\"34\",\"Waist\":\"86-90 cm\",\"Length\":\"106 cm\"},{\"Size\":\"36\",\"Waist\":\"91-95 cm\",\"Length\":\"108 cm\"}]",
                IsBestSeller = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PTrenckot, Name = "Trençkot", Slug = "trenckot", SubCategory = "Mont ve Ceket",
                CategoryId = CatKadin, Price = 3490m,
                Description = "Su iticiliği olan kumaştan üretilmiş, kemerli klasik trençkot.",
                Fabric = "%65 Pamuk, %35 Polyester", CareInstructions = "Kuru temizlemeye verin.",
                Fit = FitType.RegularFit, ModelInfo = "Manken 178 cm, 58 kg, S beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"S\",\"Chest\":\"88-92 cm\"},{\"Size\":\"M\",\"Chest\":\"92-96 cm\"},{\"Size\":\"L\",\"Chest\":\"96-100 cm\"}]",
                IsBestSeller = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PSneaker, Name = "Sneaker", Slug = "sneaker", SubCategory = "Ayakkabı",
                CategoryId = CatAyakkabi, Price = 2290m,
                Description = "Günlük kullanıma uygun, hafif tabanlı deri sneaker.",
                Fabric = "Hakiki Deri, Kauçuk Taban", CareInstructions = "Nemli bezle silerek temizleyin.",
                IsBestSeller = true, CreatedAtUtc = SeedDate,
            },
            new Product
            {
                Id = PMiniElbise, Name = "Mini Elbise", Slug = "mini-elbise", SubCategory = "Elbise",
                CategoryId = CatKadin, Price = 1690m,
                Description = "Vücudu saran kesim, günlük ve özel günlere uygun mini elbise.",
                Fabric = "%95 Viskon, %5 Elastan", CareInstructions = "30°C'de hassas yıkayın.",
                Fit = FitType.SlimFit, ModelInfo = "Manken 178 cm, 58 kg, S beden giyiyor.",
                SizeChartJson = "[{\"Size\":\"XS\",\"Chest\":\"82-86 cm\"},{\"Size\":\"S\",\"Chest\":\"86-90 cm\"},{\"Size\":\"M\",\"Chest\":\"90-94 cm\"}]",
                IsBestSeller = true, CreatedAtUtc = SeedDate,
            }
        );

        modelBuilder.Entity<ProductVariant>().HasData(
            Variant("33333333-0000-0000-0000-000000000001", PKetenGomlek, "KG-BEJ-S", "S", "Bej", "#D8D3CA", 6),
            Variant("33333333-0000-0000-0000-000000000002", PKetenGomlek, "KG-BEJ-M", "M", "Bej", "#D8D3CA", 0),
            Variant("33333333-0000-0000-0000-000000000003", PKetenGomlek, "KG-BEYAZ-M", "M", "Beyaz", "#F5F5F2", 12),

            Variant("33333333-0000-0000-0000-000000000004", POversizeBlazer, "OB-SIYAH-S", "S", "Siyah", "#202020", 7),
            Variant("33333333-0000-0000-0000-000000000005", POversizeBlazer, "OB-KREM-S", "S", "Krem", "#EDE8DD", 0),

            Variant("33333333-0000-0000-0000-000000000006", PPamukluTisort, "PT-SIYAH-S", "S", "Siyah", "#202020", 15),
            Variant("33333333-0000-0000-0000-000000000007", PPamukluTisort, "PT-BEYAZ-M", "M", "Beyaz", "#F5F5F2", 18),

            Variant("33333333-0000-0000-0000-000000000008", PDeriCanta, "DC-KAHVE-TEK", "Tek Ebat", "Kahverengi", "#6B4A32", 8),
            Variant("33333333-0000-0000-0000-000000000009", PDeriCanta, "DC-SIYAH-TEK", "Tek Ebat", "Siyah", "#202020", 4),

            Variant("33333333-0000-0000-0000-00000000000a", PYunKazak, "YK-GRI-S", "S", "Gri", "#9C9A94", 10),
            Variant("33333333-0000-0000-0000-00000000000b", PYunKazak, "YK-BORDO-M", "M", "Bordo", "#6E2A32", 0),

            Variant("33333333-0000-0000-0000-00000000000c", PKotPantolon, "KKP-INDIGO-32", "32", "İndigo", "#38506B", 11),
            Variant("33333333-0000-0000-0000-00000000000d", PKotPantolon, "KKP-INDIGO-34", "34", "İndigo", "#38506B", 0),

            Variant("33333333-0000-0000-0000-00000000000e", PTrenckot, "TK-BEJ-S", "S", "Bej", "#D8D3CA", 4),
            Variant("33333333-0000-0000-0000-00000000000f", PTrenckot, "TK-BEJ-M", "M", "Bej", "#D8D3CA", 6),

            Variant("33333333-0000-0000-0000-000000000010", PSneaker, "SN-BEYAZ-41", "41", "Beyaz", "#F5F5F2", 7),
            Variant("33333333-0000-0000-0000-000000000011", PSneaker, "SN-BEYAZ-43", "43", "Beyaz", "#F5F5F2", 0),

            Variant("33333333-0000-0000-0000-000000000012", PMiniElbise, "ME-SIYAH-XS", "XS", "Siyah", "#202020", 2),
            Variant("33333333-0000-0000-0000-000000000013", PMiniElbise, "ME-SIYAH-S", "S", "Siyah", "#202020", 5)
        );

        modelBuilder.Entity<ProductRelation>().HasData(
            // Keten Gömlek -> Ürünü Tamamla: Kot Pantolon, Deri Çanta
            Relation("44444444-0000-0000-0000-000000000001", PKetenGomlek, PKotPantolon, ProductRelationType.Outfit),
            Relation("44444444-0000-0000-0000-000000000002", PKetenGomlek, PDeriCanta, ProductRelationType.Outfit),
            Relation("44444444-0000-0000-0000-000000000003", PKetenGomlek, POversizeBlazer, ProductRelationType.Similar),

            // Oversize Blazer -> Ürünü Tamamla: Kot Pantolon, Sneaker
            Relation("44444444-0000-0000-0000-000000000004", POversizeBlazer, PKotPantolon, ProductRelationType.Outfit),
            Relation("44444444-0000-0000-0000-000000000005", POversizeBlazer, PSneaker, ProductRelationType.Outfit),
            Relation("44444444-0000-0000-0000-000000000006", POversizeBlazer, PTrenckot, ProductRelationType.Similar)
        );
    }

    private static ProductVariant Variant(string id, Guid productId, string sku, string size, string color, string colorHex, int stock) =>
        new()
        {
            Id = Guid.Parse(id), ProductId = productId, Sku = sku, Size = size,
            Color = color, ColorHex = colorHex, Stock = stock, CreatedAtUtc = SeedDate,
        };

    private static ProductRelation Relation(string id, Guid productId, Guid relatedProductId, ProductRelationType type) =>
        new()
        {
            Id = Guid.Parse(id), ProductId = productId, RelatedProductId = relatedProductId,
            Type = type, CreatedAtUtc = SeedDate,
        };
}
