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

        public async Task<IEnumerable<PessoasQueMaisCompraramDTO>> GetPessoasQueMaisCompraram(DateTime inicio, DateTime fim)
        {
            var sql = @"
                        SELECT
	                        CAST(Pessoas.id AS CHAR(36)) as Id,
	                        Pessoas.codigo as Codigo,
	                        Pessoas.nome as Nome,
	                        COUNT(Vendas.id) as QuantidadeVendas,
	                        SUM(Vendas.valor_total) as ValorTotalVendas
                        FROM Vendas JOIN Pessoas ON Vendas.id_pessoa = Pessoas.id
                        WHERE
	                        Vendas.data_faturamento >= @inicio AND Vendas.data_faturamento < @fim AND
	                        Vendas.situacao = 5
                        GROUP BY Id, Codigo, Nome
                        ORDER BY QuantidadeVendas DESC;
                    ";

            return await _context.Database
                .SqlQueryRaw<PessoasQueMaisCompraramDTO>(
                    sql,
                    new MySqlParameter("@inicio", inicio.Date),
                    new MySqlParameter("@fim", fim.Date)
                )
                .ToListAsync();
        }

        public async Task<IEnumerable<VendedoresQueMaisVenderamDTO>> GetVendedoresQueMaisVenderam(DateTime inicio, DateTime fim)
        {
            var sql = @"
                        SELECT
	                        CAST(Usuarios.id AS CHAR(36)) as Id,
	                        Usuarios.codigo as Codigo,
	                        Pessoas.nome as Nome,
	                        COUNT(Vendas.id) as QuantidadeVendas,
	                        SUM(Vendas.valor_total) as ValorTotalVendas
                        FROM 
	                        Vendas JOIN Usuarios ON Vendas.id_usuario = Usuarios.id
	                        JOIN Pessoas ON Usuarios.id_pessoa = Pessoas.id
                        WHERE
	                        Vendas.data_faturamento >= @inicio AND Vendas.data_faturamento < @fim AND
	                        Vendas.situacao = 5
                        GROUP BY Id, Codigo, Nome
                        ORDER BY QuantidadeVendas DESC;
                    ";

            return await _context.Database
                .SqlQueryRaw<VendedoresQueMaisVenderamDTO>(
                    sql,
                    new MySqlParameter("@inicio", inicio.Date),
                    new MySqlParameter("@fim", fim.Date)
                )
                .ToListAsync();
        }
    }
}