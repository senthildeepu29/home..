using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopForHome.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddShippingAndPaymentToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$743T2yyyeFWP/ENph5GkZO8Y3uLk3fjwYxctwgDkC1CPx8kHXlWRG");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$tWaxDdKIVLZpx/vMtF0NQ.H7QFHS9.Ct4vmmJlrVg7e5Wf5W8NLIi");
        }
    }
}
