using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueMoon.Migrations
{
    /// <inheritdoc />
    public partial class Migration_0003 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoas_Enderecos_id_endereco",
                table: "Pessoas");

            migrationBuilder.DropIndex(
                name: "IX_Pessoas_id_endereco",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "id_endereco",
                table: "Pessoas");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Produtos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(70)",
                oldMaxLength: 70)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "nome",
                table: "Produtos",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "id_pessoa",
                table: "Enderecos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_id_pessoa",
                table: "Enderecos",
                column: "id_pessoa",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoas_id_pessoa",
                table: "Enderecos",
                column: "id_pessoa",
                principalTable: "Pessoas",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoas_id_pessoa",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_id_pessoa",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "nome",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "id_pessoa",
                table: "Enderecos");

            migrationBuilder.AlterColumn<string>(
                name: "descricao",
                table: "Produtos",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<Guid>(
                name: "id_endereco",
                table: "Pessoas",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_id_endereco",
                table: "Pessoas",
                column: "id_endereco",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoas_Enderecos_id_endereco",
                table: "Pessoas",
                column: "id_endereco",
                principalTable: "Enderecos",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
