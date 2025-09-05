using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopForHome.Api.Migrations
{
    /// <inheritdoc />
    public partial class Init4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetEmail",
                table: "Coupons");

            migrationBuilder.AddColumn<int>(
                name: "Discount",
                table: "Coupons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiscountPercentage",
                table: "Coupons",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Coupons",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { "Electronics", "https://example.com/laptop.jpg", "Laptop", 999.99m, 50 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Rating", "Stock" },
                values: new object[] { "Electronics", "https://example.com/smartphone.jpg", "Smartphone", 699.99m, 4.3m, 100 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { "Accessories", "https://example.com/headphones.jpg", "Headphones", 199.99m, 200 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "ImageUrl", "Name", "Price", "Rating", "Stock" },
                values: new object[,]
                {
                    { 4, "Home Appliances", "https://example.com/coffeemaker.jpg", "Coffee Maker", 49.99m, 3.8m, 80 },
                    { 5, "Electronics", "https://example.com/console.jpg", "Gaming Console", 399.99m, 4.7m, 30 }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$2PmBTGnJ4L3z74uPq87Kg.3jQ7/whxMz1t7qUbPNtz/sOh2IYwUui");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "TargetEmail",
                table: "Coupons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { "", "https://via.placeholder.com/150?text=Sofa", "Modern Sofa", 29999m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Rating", "Stock" },
                values: new object[] { "", "https://via.placeholder.com/150?text=Lamp", "Table Lamp", 1499m, 4.2m, 0 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "ImageUrl", "Name", "Price", "Stock" },
                values: new object[] { "", "https://via.placeholder.com/150?text=Art", "Wall Art", 1999m, 0 });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$xVY1zDynqdjCKXlv0ogPoeuKn2PaGnHunvJnovUB/03aekkX0o3Lu");
        }
    }
}
