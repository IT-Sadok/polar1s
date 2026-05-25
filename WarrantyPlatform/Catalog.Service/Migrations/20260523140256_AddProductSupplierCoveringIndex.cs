using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddProductSupplierCoveringIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductSuppliers_ProductId",
                table: "ProductSuppliers",
                column: "ProductId")
                .Annotation("Npgsql:IndexInclude", new[] { "UnitCost" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSuppliers_ProductId",
                table: "ProductSuppliers");
        }
    }
}
