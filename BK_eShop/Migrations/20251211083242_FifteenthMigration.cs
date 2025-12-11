using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BK_eShop.Migrations
{
    /// <inheritdoc />
    public partial class FifteenthMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");
        }
    }
}
