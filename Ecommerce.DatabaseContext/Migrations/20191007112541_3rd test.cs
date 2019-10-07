using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DatabaseContext.Migrations
{
    public partial class _3rdtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductVariants_ProductVariantsId1",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductVariantsId1",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductVariantsId1",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products",
                column: "ProductVariantsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "ProductVariantsId1",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products",
                column: "ProductVariantsId",
                unique: true,
                filter: "[ProductVariantsId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductVariantsId1",
                table: "Products",
                column: "ProductVariantsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductVariants_ProductVariantsId1",
                table: "Products",
                column: "ProductVariantsId1",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
