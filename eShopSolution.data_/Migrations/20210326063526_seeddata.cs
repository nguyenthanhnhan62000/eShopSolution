using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.data.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppConfigs",
                columns: new[] { "Key", "Value" },
                values: new object[,]
                {
                    { "homeTitle", "this is home page of eShopsolution" },
                    { "homeKeyWord", "this is Keyword of eShopsolution" },
                    { "homeDescription", "this is description of eShopsolution" }
                });

            migrationBuilder.InsertData(
                table: "Categorys",
                columns: new[] { "Id", "IsShowOnHome", "ParentId", "SortOrder", "Status" },
                values: new object[,]
                {
                    { 1, true, null, 1, 1 },
                    { 2, true, null, 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "IsDefault", "Name" },
                values: new object[,]
                {
                    { "vi-VN", true, "tiếng Viêt" },
                    { "en-US", false, "English" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateCreated", "IsFeatured", "OriginalPrice", "Price" },
                values: new object[] { 1, new DateTime(2021, 3, 26, 13, 35, 25, 665, DateTimeKind.Local).AddTicks(6217), null, 100000m, 20000m });

            migrationBuilder.InsertData(
                table: "CategoryTranslations",
                columns: new[] { "Id", "CategoryId", "LanguageId", "Name", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, 1, "vi-VN", "Áo nam", "ao-nam", "sản phẩm áo thời trang nam", "sản phẩm áo thời trang nam" },
                    { 3, 2, "vi-VN", "Áo nữ", "ao-nu", "sản phẩm áo thời trang nu", "sản phẩm áo thời trang nu" },
                    { 2, 1, "en-US", "Men shirt", "Men-shirt", "the Shirt products for men", "the Shirt products for men" },
                    { 4, 2, "en-US", "Women shirt", "Women-shirt", "the Shirt products for women", "the Shirt products for women" }
                });

            migrationBuilder.InsertData(
                table: "ProductInCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductTranslations",
                columns: new[] { "Id", "Description", "Details", "LanguageId", "Name", "ProductId", "SeoAlias", "SeoDescription", "SeoTitle" },
                values: new object[,]
                {
                    { 1, "Áo sơ mi trắng Việt Tiến", "Áo sơ mi trắng Việt Tiến", "vi-VN", "Áo sơ mi trắng Việt Tiến", 1, "ao-so-mi-trang-viet-tien", "Áo sơ mi trắng Việt Tiến", "Áo sơ mi trắng Việt Tiến" },
                    { 2, "viet tien Men T-shirt", "viet tien Men T-shirt", "en-US", "viet tien Men T-shirt", 1, "viet-tien-Men-T-shirt", "viet tien Men T-shirt", "viet tien Men T-shirt" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "homeDescription");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "homeKeyWord");

            migrationBuilder.DeleteData(
                table: "AppConfigs",
                keyColumn: "Key",
                keyValue: "homeTitle");

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoryTranslations",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ProductInCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ProductTranslations",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categorys",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "en-US");

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: "vi-VN");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
