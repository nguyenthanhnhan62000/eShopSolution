using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eShopSolution.data.Migrations
{
    public partial class ChangeFileLengthType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "1297ff29-6b10-474d-a500-41cfa501fa0f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ea5e5b08-86fd-4d15-997b-148566b88bac", "AQAAAAEAACcQAAAAEH8rCZwc2x4+OrmKRCn3pW6FmIcHYSmLN/jHJnBTqJGbNKu4wOQY0qzp6Xj69/ay7A==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 3, 28, 14, 24, 53, 655, DateTimeKind.Local).AddTicks(9914));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "0a152011-10d8-4b97-a337-3ab6f640a389");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "205940f6-0a2d-49c2-9fdf-941862d4f2e4", "AQAAAAEAACcQAAAAELnoRbBPBzVrUW0hv5uZoNk9pQkYd+Qe5C/6YfCsZBXOpkQdiHgrBxZBXZTOCJ3Tyg==" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2021, 3, 26, 16, 6, 40, 546, DateTimeKind.Local).AddTicks(2099));
        }
    }
}
