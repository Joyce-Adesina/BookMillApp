using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookMillApp_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a42bee84-1665-4368-9ca2-d2e8ecc06372");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4030c78-6819-4888-9597-e4ffac0c360c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c6e8f0ff-9dd3-4e09-9f5e-6a14de427e86");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "a42bee84-1665-4368-9ca2-d2e8ecc06372", null, "Supplier", "SUPPLIER" },
                    { "c4030c78-6819-4888-9597-e4ffac0c360c", null, "Admin", "ADMIN" },
                    { "c6e8f0ff-9dd3-4e09-9f5e-6a14de427e86", null, "Manufacturer", "MANUFACTURER" }
                });
        }
    }
}
