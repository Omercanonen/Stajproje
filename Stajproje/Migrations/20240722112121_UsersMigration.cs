using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stajproje.Migrations
{
    /// <inheritdoc />
    public partial class UsersMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
       name: "Password",
       table: "Users",
       newName: "Surname");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
      name: "Surname",
      table: "Users",
      newName: "Password");
        }
    }
}
