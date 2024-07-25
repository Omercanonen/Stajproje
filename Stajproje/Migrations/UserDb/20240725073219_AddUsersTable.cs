using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stajproje.Migrations.UserDb
{
    /// <inheritdoc />
    public partial class AddUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
           name: "Users",
           columns: table => new
           {
               UserId = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
               Name = table.Column<string>(nullable: true),
               Surname = table.Column<string>(nullable: true),
               Email = table.Column<string>(nullable: true),
               PhoneNumber = table.Column<string>(nullable: true),
               RegStatus = table.Column<int>(nullable: false)
           },
           constraints: table =>
           {
               table.PrimaryKey("PK_Users", x => x.UserId);
           });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
           name: "Users");
        }
    }
}
