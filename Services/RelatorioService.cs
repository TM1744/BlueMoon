using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.VisualBasic;
using BlueMoon.DTO;

namespace BlueMoon.Services
{
    public class RelatorioService : IRelatorioService
    {
        private readonly IRelatorioRepositorio _repostorio;

        public RelatorioService(IRelatorioRepositorio repositorio) => _repostorio = repositorio;

        public async Task<byte[]> GerarRelatorioProdutosMaisVendidosAsync(string inicio, string fim)
        {
            var dateInicio = StringToDateTime(inicio);
            var dateFim = StringToDateTime(fim);

            // 1. Buscar os dados do relatório
            var dados = await _repostorio.GetProdutosMaisVendidos(dateInicio, dateFim);

            // 2. Criar o documento PDF
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);

                    // Cabeçalho
                    page.Header().Text("Relatório - Produtos Mais Vendidos")
                        .SemiBold().FontSize(18).FontColor(Colors.Blue.Medium);

                    page.Content().Column(col =>
                    {
                        // Período consultado
                        col.Item().Text($"Período: {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}")
                            .FontSize(12).FontColor(Colors.Grey.Darken1);

                        col.Item().PaddingVertical(10);

                        // Tabela com os dados
                        col.Item().Table(table =>
                        {
                            // Definição das colunas
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1); // Código
                                columns.RelativeColumn(3); // Nome
                                columns.RelativeColumn(2); // Quantidade
                                columns.RelativeColumn(2); // Total vendido
                            });

                            // Cabeçalho da tabela
                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).Text("Código");
                                header.Cell().Element(CellStyle).Text("Nome");
                                header.Cell().Element(CellStyle).Text("Quantidade de vendas");
                                header.Cell().Element(CellStyle).Text("Valor total das vendas (R$)");
                            });

                            // Linhas da tabela
                            foreach (var item in dados)
                            {
                                table.Cell().Element(CellStyle).Text(item.Codigo.ToString());
                                table.Cell().Element(CellStyle).Text(item.Nome);
                                table.Cell().Element(CellStyle).Text(item.QuantidadeVendida.ToString());
                                table.Cell().Element(CellStyle).Text(item.TotalVendido.ToString("N2"));
                            }

                            // Estilo das células
                            static IContainer CellStyle(IContainer container)
                            {
                                return container
                                    .Padding(6)
                                    .BorderBottom(1)
                                    .BorderColor(Colors.Grey.Lighten2);
                            }
                        });
                    });

                    // Rodapé
                    page.Footer().AlignCenter().Text($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm}");
                });
            });

            // 3. Renderizar PDF em memória
            return document.GeneratePdf();
        }

        public async Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidosAsync(string inicio, string fim)
        {
            var itens = await _repostorio.GetProdutosMaisVendidos(StringToDateTime(inicio), StringToDateTime(fim));
            return itens;
        }

        private DateTime StringToDateTime(string data)
        {
            data = Regex.Replace(data, "[^0-9]", "");

            if (data.Length != 8)
                throw new ArgumentException("Data informada é inválida");

            return DateTime.ParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture);
        }

    }
}