using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DatabaseContext.Migrations
{
    public partial class DbsetOfProductVariantsandSizeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "Stocks");

            migrationBuilder.CreateTable(
                name: "Size",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Size", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductVariants",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Color = table.Column<string>(nullable: true),
                    Brand = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    SizeId = table.Column<long>(nullable: false),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductVariants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductVariants_Size_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Size",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products",
                column: "ProductVariantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_StockId",
                table: "Products",
                column: "StockId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVariants_SizeId",
                table: "ProductVariants",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductVariants_ProductVariantsId",
                table: "Products",
                column: "ProductVariantsId",
                principalTable: "ProductVariants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stocks_StockId",
                table: "Products",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductVariants_ProductVariantsId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stocks_StockId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductVariants");

            migrationBuilder.DropTable(
                name: "Size");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductVariantsId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_StockId",
                table: "Products");

            migrationBuilder.AddColumn<long>(
                name: "ProductId1",
                table: "Stocks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId1",
                table: "Stocks",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_Products_ProductId1",
                table: "Stocks",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
