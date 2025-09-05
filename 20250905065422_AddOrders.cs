using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopForHome.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$tWaxDdKIVLZpx/vMtF0NQ.H7QFHS9.Ct4vmmJlrVg7e5Wf5W8NLIi");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$9EOMxP7rzCEs.VaRkCJ/hePJWsTPQQjnhbJAH3fs6b44qKH5T6vNe");
        }
    }
}
