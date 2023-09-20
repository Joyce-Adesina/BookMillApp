using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMillApp_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class rolesseeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66cd8afc-15c3-4206-b604-23463e24cd19");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d1897c9d-3767-4427-94a8-668bc798701e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed03458e-6f6d-4405-85d9-7af84532626e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "62f33956-20f8-4d87-9390-e4ac72159c84", null, "Admin", "ADMIN" },
                    { "a41596c9-4a30-4559-b4c6-e9f2330e2d4f", null, "Supplier", "SUPPLIER" },
                    { "c4f6cca7-693b-474f-aabf-dc819f091fed", null, "Manufacturer", "MANUFACTURER" },
                    { "e9d373a8-1245-405e-8de6-99276829adaf", null, "Regular", "REGULAR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62f33956-20f8-4d87-9390-e4ac72159c84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a41596c9-4a30-4559-b4c6-e9f2330e2d4f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4f6cca7-693b-474f-aabf-dc819f091fed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e9d373a8-1245-405e-8de6-99276829adaf");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66cd8afc-15c3-4206-b604-23463e24cd19", null, "Admin", "ADMIN" },
                    { "d1897c9d-3767-4427-94a8-668bc798701e", null, "Manufacturer", "MANUFACTURER" },
                    { "ed03458e-6f6d-4405-85d9-7af84532626e", null, "Supplier", "SUPPLIER" }
                });
        }
    }
}
