using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stajproje.Migrations.ServiceDb
{
    /// <inheritdoc />
    public partial class AddDeliveryStatusToService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "DeliveryStatus",
            table: "Services",
            nullable: false,
            defaultValue: 0);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
            name: "DeliveryStatus",
            table: "Services");
        }
    }
}
