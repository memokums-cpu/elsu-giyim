using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElsuGiyim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    ParentCategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Slug = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    SubCategory = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    SalePrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Fabric = table.Column<string>(type: "text", nullable: false),
                    CareInstructions = table.Column<string>(type: "text", nullable: false),
                    Fit = table.Column<int>(type: "integer", nullable: true),
                    ModelInfo = table.Column<string>(type: "text", nullable: true),
                    SizeChartJson = table.Column<string>(type: "text", nullable: true),
                    IsNew = table.Column<bool>(type: "boolean", nullable: false),
                    IsBestSeller = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImages_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelatedProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRelations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRelations_Products_RelatedProductId",
                        column: x => x.RelatedProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sku = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Size = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Color = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ColorHex = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    Stock = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryMovements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductVariantId = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryMovements_ProductVariants_ProductVariantId",
                        column: x => x.ProductVariantId,
                        principalTable: "ProductVariants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAtUtc", "DisplayOrder", "IsActive", "Name", "ParentCategoryId", "Slug", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, true, "Kadın", null, "kadin", null },
                    { new Guid("11111111-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, true, "Erkek", null, "erkek", null },
                    { new Guid("11111111-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, true, "Ayakkabı", null, "ayakkabi", null },
                    { new Guid("11111111-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, true, "Aksesuar", null, "aksesuar", null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedAtUtc", "Name", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("55555555-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Admin", null },
                    { new Guid("55555555-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "UrunYoneticisi", null },
                    { new Guid("55555555-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "SiparisYoneticisi", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAtUtc", "Email", "FullName", "IsActive", "PasswordHash", "UpdatedAtUtc" },
                values: new object[] { new Guid("66666666-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@elsugiyim.com", "Mağaza Sahibi (Demo)", true, "210000.AAECAwQFBgcICQoLDA0ODw==.VMUd18nJtIldSv6u5JUCqVab5MaFcvk+Cu5FxnTXuzQ=", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CareInstructions", "CategoryId", "CreatedAtUtc", "Description", "Fabric", "Fit", "IsBestSeller", "IsNew", "IsPublished", "ModelInfo", "Name", "Price", "SalePrice", "SizeChartJson", "Slug", "SubCategory", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("22222222-0000-0000-0000-000000000001"), "30°C'de yıkayın, düşük ısıda ütüleyin.", new Guid("11111111-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Nefes alabilen %100 keten kumaştan üretilmiş, yaz aylarına uygun rahat kesim gömlek.", "%100 Keten", 1, false, true, true, "Manken 186 cm, 76 kg, M beden giyiyor.", "Keten Gömlek", 1490m, null, "[{\"Size\":\"S\",\"Chest\":\"96-100 cm\",\"Length\":\"72 cm\"},{\"Size\":\"M\",\"Chest\":\"100-104 cm\",\"Length\":\"74 cm\"},{\"Size\":\"L\",\"Chest\":\"104-108 cm\",\"Length\":\"76 cm\"},{\"Size\":\"XL\",\"Chest\":\"108-112 cm\",\"Length\":\"78 cm\"}]", "keten-gomlek", "Gömlek", null },
                    { new Guid("22222222-0000-0000-0000-000000000002"), "Kuru temizlemeye verin.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yapılandırılmamış omuzlara sahip, oversize blazer.", "%70 Yün, %30 Polyester", 2, true, true, true, "Manken 178 cm, 58 kg, S beden giyiyor.", "Oversize Blazer", 2390m, 1990m, "[{\"Size\":\"XS\",\"Chest\":\"88-92 cm\"},{\"Size\":\"S\",\"Chest\":\"92-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-100 cm\"},{\"Size\":\"L\",\"Chest\":\"100-104 cm\"}]", "oversize-blazer", "Ceket", null },
                    { new Guid("22222222-0000-0000-0000-000000000003"), "40°C'de yıkayın.", new Guid("11111111-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Günlük kullanıma uygun, %100 organik pamuktan temel tişört.", "%100 Organik Pamuk", 1, false, true, true, "Manken 184 cm, 74 kg, M beden giyiyor.", "Pamuklu Tişört", 690m, null, "[{\"Size\":\"S\",\"Chest\":\"94-98 cm\"},{\"Size\":\"M\",\"Chest\":\"98-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-106 cm\"},{\"Size\":\"XL\",\"Chest\":\"106-110 cm\"}]", "pamuklu-tisort", "Tişört", null },
                    { new Guid("22222222-0000-0000-0000-000000000004"), "Nemden koruyun, deri bakım kremi ile temizleyin.", new Guid("11111111-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El yapımı hakiki deri, günlük kullanıma uygun omuz çantası.", "Hakiki Deri", null, false, true, true, null, "Deri Çanta", 3190m, null, null, "deri-canta", "Çanta", null },
                    { new Guid("22222222-0000-0000-0000-000000000005"), "Elde, soğuk suda yıkayın.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yumuşak merinos yünden örülmüş, bol kesim kazak.", "%100 Merinos Yün", 4, false, false, true, "Manken 175 cm, 55 kg, S beden giyiyor.", "Yün Kazak", 1890m, null, "[{\"Size\":\"S\",\"Chest\":\"90-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-108 cm\"}]", "yun-kazak", "Sweatshirt", null },
                    { new Guid("22222222-0000-0000-0000-000000000006"), "30°C'de ters çevirerek yıkayın.", new Guid("11111111-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Zamansız kesim, orta bel, düz paça kot pantolon.", "%98 Pamuk, %2 Elastan", 1, true, false, true, "Manken 186 cm, 76 kg, 32 beden giyiyor.", "Klasik Kot Pantolon", 1190m, null, "[{\"Size\":\"30\",\"Waist\":\"76-80 cm\",\"Length\":\"102 cm\"},{\"Size\":\"32\",\"Waist\":\"81-85 cm\",\"Length\":\"104 cm\"},{\"Size\":\"34\",\"Waist\":\"86-90 cm\",\"Length\":\"106 cm\"},{\"Size\":\"36\",\"Waist\":\"91-95 cm\",\"Length\":\"108 cm\"}]", "klasik-kot-pantolon", "Pantolon", null },
                    { new Guid("22222222-0000-0000-0000-000000000007"), "Kuru temizlemeye verin.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Su iticiliği olan kumaştan üretilmiş, kemerli klasik trençkot.", "%65 Pamuk, %35 Polyester", 1, true, false, true, "Manken 178 cm, 58 kg, S beden giyiyor.", "Trençkot", 3490m, null, "[{\"Size\":\"S\",\"Chest\":\"88-92 cm\"},{\"Size\":\"M\",\"Chest\":\"92-96 cm\"},{\"Size\":\"L\",\"Chest\":\"96-100 cm\"}]", "trenckot", "Mont ve Ceket", null },
                    { new Guid("22222222-0000-0000-0000-000000000008"), "Nemli bezle silerek temizleyin.", new Guid("11111111-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Günlük kullanıma uygun, hafif tabanlı deri sneaker.", "Hakiki Deri, Kauçuk Taban", null, true, false, true, null, "Sneaker", 2290m, null, null, "sneaker", "Ayakkabı", null },
                    { new Guid("22222222-0000-0000-0000-000000000009"), "30°C'de hassas yıkayın.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vücudu saran kesim, günlük ve özel günlere uygun mini elbise.", "%95 Viskon, %5 Elastan", 0, true, false, true, "Manken 178 cm, 58 kg, S beden giyiyor.", "Mini Elbise", 1690m, null, "[{\"Size\":\"XS\",\"Chest\":\"82-86 cm\"},{\"Size\":\"S\",\"Chest\":\"86-90 cm\"},{\"Size\":\"M\",\"Chest\":\"90-94 cm\"}]", "mini-elbise", "Elbise", null }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("55555555-0000-0000-0000-000000000001"), new Guid("66666666-0000-0000-0000-000000000001") });

            migrationBuilder.InsertData(
                table: "ProductRelations",
                columns: new[] { "Id", "CreatedAtUtc", "ProductId", "RelatedProductId", "Type", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("44444444-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000006"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000004"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000002"), 1, null },
                    { new Guid("44444444-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000006"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000008"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000006"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000007"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "ColorHex", "CreatedAtUtc", "ProductId", "Size", "Sku", "Stock", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("33333333-0000-0000-0000-000000000001"), "Bej", "#D8D3CA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), "S", "KG-BEJ-S", 6, null },
                    { new Guid("33333333-0000-0000-0000-000000000002"), "Bej", "#D8D3CA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), "M", "KG-BEJ-M", 0, null },
                    { new Guid("33333333-0000-0000-0000-000000000003"), "Beyaz", "#F5F5F2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), "M", "KG-BEYAZ-M", 12, null },
                    { new Guid("33333333-0000-0000-0000-000000000004"), "Siyah", "#202020", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), "S", "OB-SIYAH-S", 7, null },
                    { new Guid("33333333-0000-0000-0000-000000000005"), "Krem", "#EDE8DD", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), "S", "OB-KREM-S", 0, null },
                    { new Guid("33333333-0000-0000-0000-000000000006"), "Siyah", "#202020", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000003"), "S", "PT-SIYAH-S", 15, null },
                    { new Guid("33333333-0000-0000-0000-000000000007"), "Beyaz", "#F5F5F2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000003"), "M", "PT-BEYAZ-M", 18, null },
                    { new Guid("33333333-0000-0000-0000-000000000008"), "Kahverengi", "#6B4A32", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000004"), "Tek Ebat", "DC-KAHVE-TEK", 8, null },
                    { new Guid("33333333-0000-0000-0000-000000000009"), "Siyah", "#202020", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000004"), "Tek Ebat", "DC-SIYAH-TEK", 4, null },
                    { new Guid("33333333-0000-0000-0000-00000000000a"), "Gri", "#9C9A94", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000005"), "S", "YK-GRI-S", 10, null },
                    { new Guid("33333333-0000-0000-0000-00000000000b"), "Bordo", "#6E2A32", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000005"), "M", "YK-BORDO-M", 0, null },
                    { new Guid("33333333-0000-0000-0000-00000000000c"), "İndigo", "#38506B", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000006"), "32", "KKP-INDIGO-32", 11, null },
                    { new Guid("33333333-0000-0000-0000-00000000000d"), "İndigo", "#38506B", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000006"), "34", "KKP-INDIGO-34", 0, null },
                    { new Guid("33333333-0000-0000-0000-00000000000e"), "Bej", "#D8D3CA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000007"), "S", "TK-BEJ-S", 4, null },
                    { new Guid("33333333-0000-0000-0000-00000000000f"), "Bej", "#D8D3CA", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000007"), "M", "TK-BEJ-M", 6, null },
                    { new Guid("33333333-0000-0000-0000-000000000010"), "Beyaz", "#F5F5F2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000008"), "41", "SN-BEYAZ-41", 7, null },
                    { new Guid("33333333-0000-0000-0000-000000000011"), "Beyaz", "#F5F5F2", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000008"), "43", "SN-BEYAZ-43", 0, null },
                    { new Guid("33333333-0000-0000-0000-000000000012"), "Siyah", "#202020", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000009"), "XS", "ME-SIYAH-XS", 2, null },
                    { new Guid("33333333-0000-0000-0000-000000000013"), "Siyah", "#202020", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000009"), "S", "ME-SIYAH-S", 5, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Slug",
                table: "Categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryMovements_ProductVariantId",
                table: "InventoryMovements",
                column: "ProductVariantId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId",
                table: "ProductImages",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelations_ProductId_RelatedProductId_Type",
                table: "ProductRelations",
                columns: new[] { "ProductId", "RelatedProductId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelations_RelatedProductId",
                table: "ProductRelations",
                column: "RelatedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Slug",
                table: "Products",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_Sku",
                table: "ProductVariants",
                column: "Sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryMovements");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductRelations");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
