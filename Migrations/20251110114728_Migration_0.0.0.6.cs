using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueMoon.Migrations
{
    /// <inheritdoc />
    public partial class Migration_0006 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    id_pessoa = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    situacao = table.Column<int>(type: "int", nullable: false),
                    login = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    senha = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cargo = table.Column<int>(type: "int", nullable: false),
                    salario = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    admissao = table.Column<DateOnly>(type: "date", nullable: false),
                    horario_inicio_carga_horaria = table.Column<TimeOnly>(type: "time(6)", nullable: false),
                    horario_fim_carga_horaria = table.Column<TimeOnly>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Pessoas_id_pessoa",
                        column: x => x.id_pessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_id_pessoa",
                table: "Usuarios",
                column: "id_pessoa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
