using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eShop.Migrations
{
    /// <inheritdoc />
    public partial class SeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75953dc0-a77c-4651-87b2-b5ae5ba4f221");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "abfe34ca-a5c5-484b-9dfc-bf5ba52a373d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3c322207-d6e4-47a7-b8ca-5d32288573d8", "0", "Admin", "Admin" },
                    { "6419c553-0748-49ea-bb0f-ad9bf8c698cd", "1", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3c322207-d6e4-47a7-b8ca-5d32288573d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6419c553-0748-49ea-bb0f-ad9bf8c698cd");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75953dc0-a77c-4651-87b2-b5ae5ba4f221", "0", "User", "User" },
                    { "abfe34ca-a5c5-484b-9dfc-bf5ba52a373d", "0", "Admin", "Admin" }
                });
        }
    }
}
