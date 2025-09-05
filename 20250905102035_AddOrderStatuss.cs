using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopForHome.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderStatuss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$Jh.Ia8Hhb8d/KipYfM0p5uTuLEpr8hhISWBdiJb4i/8gvZ.Kzg4Lm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$OrBAbvKenogZ.YORcFj3xOuB4nRUZVpNeklHW6FpS8Q.t3v2QmA8q");
        }
    }
}
