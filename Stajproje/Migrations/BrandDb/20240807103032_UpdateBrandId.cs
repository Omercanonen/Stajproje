using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Stajproje.Migrations.BrandDb
{
    /// <inheritdoc />
    public partial class UpdateBrandId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
            name: "BrandId",
            table: "Brands",
            nullable: false,
            oldClrType: typeof(int),
            oldType: "int")
            .Annotation("SqlServer:Identity", "1, 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
