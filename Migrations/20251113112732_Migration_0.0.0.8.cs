using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueMoon.Migrations
{
    /// <inheritdoc />
    public partial class Migration_0008 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "data",
                table: "Vendas",
                newName: "data_faturamento");

            migrationBuilder.AddColumn<DateTime>(
                name: "data_abertura",
                table: "Vendas",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_abertura",
                table: "Vendas");

            migrationBuilder.RenameColumn(
                name: "data_faturamento",
                table: "Vendas",
                newName: "data");
        }
    }
}
