using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Warranty.Service.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceCustomerIndexWithCursorComposite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warranties_CustomerId",
                table: "Warranties");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_CustomerId_CreatedAt_Id",
                table: "Warranties",
                columns: new[] { "CustomerId", "CreatedAt", "Id" },
                descending: new[] { false, true, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Warranties_CustomerId_CreatedAt_Id",
                table: "Warranties");

            migrationBuilder.CreateIndex(
                name: "IX_Warranties_CustomerId",
                table: "Warranties",
                column: "CustomerId");
        }
    }
}
