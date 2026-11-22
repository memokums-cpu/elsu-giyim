using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ElsuGiyim.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductRelationsAndSizeChart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SizeChartJson",
                table: "Products",
                type: "text",
                nullable: true);

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

            migrationBuilder.InsertData(
                table: "ProductRelations",
                columns: new[] { "Id", "CreatedAtUtc", "ProductId", "RelatedProductId", "Type", "UpdatedAtUtc" },
                values: new object[] { new Guid("44444444-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000002"), 1, null });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000006"),
                columns: new[] { "Color", "ColorHex", "Size", "Sku", "Stock" },
                values: new object[] { "Siyah", "#202020", "S", "PT-SIYAH-S", 15 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000007"),
                columns: new[] { "Color", "ColorHex", "Size", "Sku", "Stock" },
                values: new object[] { "Beyaz", "#F5F5F2", "M", "PT-BEYAZ-M", 18 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000001"),
                column: "SizeChartJson",
                value: "[{\"Size\":\"S\",\"Chest\":\"96-100 cm\",\"Length\":\"72 cm\"},{\"Size\":\"M\",\"Chest\":\"100-104 cm\",\"Length\":\"74 cm\"},{\"Size\":\"L\",\"Chest\":\"104-108 cm\",\"Length\":\"76 cm\"},{\"Size\":\"XL\",\"Chest\":\"108-112 cm\",\"Length\":\"78 cm\"}]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000002"),
                column: "SizeChartJson",
                value: "[{\"Size\":\"XS\",\"Chest\":\"88-92 cm\"},{\"Size\":\"S\",\"Chest\":\"92-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-100 cm\"},{\"Size\":\"L\",\"Chest\":\"100-104 cm\"}]");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                columns: new[] { "CareInstructions", "Description", "Fabric", "IsBestSeller", "IsNew", "ModelInfo", "Name", "Price", "SizeChartJson", "Slug", "SubCategory" },
                values: new object[] { "40°C'de yıkayın.", "Günlük kullanıma uygun, %100 organik pamuktan temel tişört.", "%100 Organik Pamuk", false, true, "Manken 184 cm, 74 kg, M beden giyiyor.", "Pamuklu Tişört", 690m, "[{\"Size\":\"S\",\"Chest\":\"94-98 cm\"},{\"Size\":\"M\",\"Chest\":\"98-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-106 cm\"},{\"Size\":\"XL\",\"Chest\":\"106-110 cm\"}]", "pamuklu-tisort", "Tişört" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CareInstructions", "CategoryId", "CreatedAtUtc", "Description", "Fabric", "Fit", "IsBestSeller", "IsNew", "IsPublished", "ModelInfo", "Name", "Price", "SalePrice", "SizeChartJson", "Slug", "SubCategory", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("22222222-0000-0000-0000-000000000004"), "Nemden koruyun, deri bakım kremi ile temizleyin.", new Guid("11111111-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "El yapımı hakiki deri, günlük kullanıma uygun omuz çantası.", "Hakiki Deri", null, false, true, true, null, "Deri Çanta", 3190m, null, null, "deri-canta", "Çanta", null },
                    { new Guid("22222222-0000-0000-0000-000000000005"), "Elde, soğuk suda yıkayın.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Yumuşak merinos yünden örülmüş, bol kesim kazak.", "%100 Merinos Yün", 4, false, false, true, "Manken 175 cm, 55 kg, S beden giyiyor.", "Yün Kazak", 1890m, null, "[{\"Size\":\"S\",\"Chest\":\"90-96 cm\"},{\"Size\":\"M\",\"Chest\":\"96-102 cm\"},{\"Size\":\"L\",\"Chest\":\"102-108 cm\"}]", "yun-kazak", "Sweatshirt", null },
                    { new Guid("22222222-0000-0000-0000-000000000006"), "30°C'de ters çevirerek yıkayın.", new Guid("11111111-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Zamansız kesim, orta bel, düz paça kot pantolon.", "%98 Pamuk, %2 Elastan", 1, true, false, true, "Manken 186 cm, 76 kg, 32 beden giyiyor.", "Klasik Kot Pantolon", 1190m, null, "[{\"Size\":\"30\",\"Waist\":\"76-80 cm\",\"Length\":\"102 cm\"},{\"Size\":\"32\",\"Waist\":\"81-85 cm\",\"Length\":\"104 cm\"},{\"Size\":\"34\",\"Waist\":\"86-90 cm\",\"Length\":\"106 cm\"},{\"Size\":\"36\",\"Waist\":\"91-95 cm\",\"Length\":\"108 cm\"}]", "klasik-kot-pantolon", "Pantolon", null },
                    { new Guid("22222222-0000-0000-0000-000000000007"), "Kuru temizlemeye verin.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Su iticiliği olan kumaştan üretilmiş, kemerli klasik trençkot.", "%65 Pamuk, %35 Polyester", 1, true, false, true, "Manken 178 cm, 58 kg, S beden giyiyor.", "Trençkot", 3490m, null, "[{\"Size\":\"S\",\"Chest\":\"88-92 cm\"},{\"Size\":\"M\",\"Chest\":\"92-96 cm\"},{\"Size\":\"L\",\"Chest\":\"96-100 cm\"}]", "trenckot", "Mont ve Ceket", null },
                    { new Guid("22222222-0000-0000-0000-000000000008"), "Nemli bezle silerek temizleyin.", new Guid("11111111-0000-0000-0000-000000000003"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Günlük kullanıma uygun, hafif tabanlı deri sneaker.", "Hakiki Deri, Kauçuk Taban", null, true, false, true, null, "Sneaker", 2290m, null, null, "sneaker", "Ayakkabı", null },
                    { new Guid("22222222-0000-0000-0000-000000000009"), "30°C'de hassas yıkayın.", new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Vücudu saran kesim, günlük ve özel günlere uygun mini elbise.", "%95 Viskon, %5 Elastan", 0, true, false, true, "Manken 178 cm, 58 kg, S beden giyiyor.", "Mini Elbise", 1690m, null, "[{\"Size\":\"XS\",\"Chest\":\"82-86 cm\"},{\"Size\":\"S\",\"Chest\":\"86-90 cm\"},{\"Size\":\"M\",\"Chest\":\"90-94 cm\"}]", "mini-elbise", "Elbise", null }
                });

            migrationBuilder.InsertData(
                table: "ProductRelations",
                columns: new[] { "Id", "CreatedAtUtc", "ProductId", "RelatedProductId", "Type", "UpdatedAtUtc" },
                values: new object[,]
                {
                    { new Guid("44444444-0000-0000-0000-000000000001"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000006"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000002"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000001"), new Guid("22222222-0000-0000-0000-000000000004"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000004"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000006"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000005"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000008"), 0, null },
                    { new Guid("44444444-0000-0000-0000-000000000006"), new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("22222222-0000-0000-0000-000000000002"), new Guid("22222222-0000-0000-0000-000000000007"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "ProductVariants",
                columns: new[] { "Id", "Color", "ColorHex", "CreatedAtUtc", "ProductId", "Size", "Sku", "Stock", "UpdatedAtUtc" },
                values: new object[,]
                {
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
                name: "IX_ProductRelations_ProductId_RelatedProductId_Type",
                table: "ProductRelations",
                columns: new[] { "ProductId", "RelatedProductId", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductRelations_RelatedProductId",
                table: "ProductRelations",
                column: "RelatedProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductRelations");

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000a"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000b"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000c"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000d"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000e"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-00000000000f"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000009"));

            migrationBuilder.DropColumn(
                name: "SizeChartJson",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000006"),
                columns: new[] { "Color", "ColorHex", "Size", "Sku", "Stock" },
                values: new object[] { "İndigo", "#38506B", "32", "KKP-INDIGO-32", 11 });

            migrationBuilder.UpdateData(
                table: "ProductVariants",
                keyColumn: "Id",
                keyValue: new Guid("33333333-0000-0000-0000-000000000007"),
                columns: new[] { "Color", "ColorHex", "Size", "Sku", "Stock" },
                values: new object[] { "İndigo", "#38506B", "34", "KKP-INDIGO-34", 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("22222222-0000-0000-0000-000000000003"),
                columns: new[] { "CareInstructions", "Description", "Fabric", "IsBestSeller", "IsNew", "ModelInfo", "Name", "Price", "Slug", "SubCategory" },
                values: new object[] { "30°C'de ters çevirerek yıkayın.", "Zamansız kesim, orta bel, düz paça kot pantolon.", "%98 Pamuk, %2 Elastan", true, false, "Manken 186 cm, 76 kg, 32 beden giyiyor.", "Klasik Kot Pantolon", 1190m, "klasik-kot-pantolon", "Pantolon" });
        }
    }
}
