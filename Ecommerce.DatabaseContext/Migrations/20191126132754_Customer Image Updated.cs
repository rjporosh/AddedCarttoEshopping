using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecommerce.DatabaseContext.Migrations
{
    public partial class CustomerImageUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Phone",
                table: "Customers",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Customers");
        }
    }
}
