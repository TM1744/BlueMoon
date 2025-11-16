using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Context;
using BlueMoon.DTO;
using BlueMoon.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace BlueMoon.Repositories
{
    public class RelatorioRepositorio : IRelatorioRepositorio
    {
        protected readonly MySqlDataBaseContext _context;

        public RelatorioRepositorio(MySqlDataBaseContext context) => _context = context;

        public async Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidos(DateTime inicio, DateTime fim)
        {
            var sql = @"
                        SELECT
                            CAST(Produtos.id AS CHAR(36)) as Id,
                            Produtos.codigo AS Codigo, 
                            Produtos.nome AS Nome, 
                            SUM(ItemVendas.quantidade) AS QuantidadeVendida, 
                            SUM(ItemVendas.subtotal) AS TotalVendido
                        FROM Vendas
                        JOIN ItemVendas ON ItemVendas.id_venda = Vendas.id
                        JOIN Produtos ON ItemVendas.id_produto = Produtos.id
                        WHERE Vendas.data_faturamento >= @inicio
                        AND Vendas.data_faturamento <  @fim
                        GROUP BY Produtos.id, Produtos.codigo, Produtos.nome
                        ORDER BY QuantidadeVendida DESC;
                    ";

            return await _context.Database
                .SqlQueryRaw<ProdutosMaisVendidosDTO>(
                    sql,
                    new MySqlParameter("@inicio", inicio.Date),
                    new MySqlParameter("@fim", fim.Date)
                )
                .ToListAsync();
        }

    }
}