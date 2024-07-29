using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Stajproje.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class AddTitleAndTaxAdminAndTaxNoToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "TaxAdmin",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TaxNo",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaxAdmin",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TaxNo",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Users");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Name", "PhoneNumber", "RegStatus", "Surname" },
                values: new object[,]
                {
                    { 1, "john.doe@example.com", "John", "1234567890", 1, "Doe" },
                    { 2, "jane.doe@example.com", "Jane", "0987654321", 1, "Doe" }
                });
        }
    }
}
