using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlueMoon.Migrations
{
    /// <inheritdoc />
    public partial class Migration_0007 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    id_pessoa = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id_usuario = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    codigo = table.Column<int>(type: "int", nullable: false),
                    situacao = table.Column<int>(type: "int", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    data = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Vendas_Pessoas_id_pessoa",
                        column: x => x.id_pessoa,
                        principalTable: "Pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vendas_Usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ItemVendas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", maxLength: 36, nullable: false, collation: "ascii_general_ci"),
                    id_produto = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    produto_nome = table.Column<string>(type: "varchar(70)", maxLength: 70, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    produto_marca = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    produto_codigo = table.Column<int>(type: "int", nullable: false),
                    produto_valor_venda = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(11,2)", nullable: false),
                    id_venda = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVendas", x => x.id);
                    table.ForeignKey(
                        name: "FK_ItemVendas_Produtos_id_produto",
                        column: x => x.id_produto,
                        principalTable: "Produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemVendas_Vendas_id_venda",
                        column: x => x.id_venda,
                        principalTable: "Vendas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVendas_id_produto",
                table: "ItemVendas",
                column: "id_produto");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVendas_id_venda",
                table: "ItemVendas",
                column: "id_venda");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_id_pessoa",
                table: "Vendas",
                column: "id_pessoa");

            migrationBuilder.CreateIndex(
                name: "IX_Vendas_id_usuario",
                table: "Vendas",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemVendas");

            migrationBuilder.DropTable(
                name: "Vendas");
        }
    }
}
