using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueMoon.Migrations
{
    /// <inheritdoc />
    public partial class Migration_0004 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.AddColumn<string>(
                name: "bairro",
                table: "Pessoas",
                type: "varchar(70)",
                maxLength: 70,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "cep",
                table: "Pessoas",
                type: "varchar(8)",
                maxLength: 8,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "cidade",
                table: "Pessoas",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "complemento",
                table: "Pessoas",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "estado",
                table: "Pessoas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "logradouro",
                table: "Pessoas",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "numero",
                table: "Pessoas",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "telefone",
                table: "Pessoas",
                type: "varchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bairro",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "cep",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "cidade",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "complemento",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "estado",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "logradouro",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "numero",
                table: "Pessoas");

            migrationBuilder.DropColumn(
                name: "telefone",
                table: "Pessoas");

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    bairro = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cep = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cidade = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    complemento = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    estado = table.Column<int>(type: "int", nullable: false),
                    logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    numero = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_pessoa = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Pessoas_id_pessoa",
                        column: x => x.id_pessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    ddd = table.Column<int>(type: "int", nullable: false),
                    numero = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_pessoa = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Telefones_Pessoas_id_pessoa",
                        column: x => x.id_pessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_id_pessoa",
                table: "Enderecos",
                column: "id_pessoa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_id_pessoa",
                table: "Telefones",
                column: "id_pessoa");
        }
    }
}
