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
            var dateInicio = StringToDateTimeInicio(inicio);
            var dateFim = StringToDateTimeFim(fim);

            var dados = await _repostorio.GetProdutosMaisVendidos(dateInicio, dateFim);

            dateFim = dateFim.AddDays(-1);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Element(ComposeHeader);

                    void ComposeHeader(IContainer container)
                    {
                        container.Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item()
                                    .Text("Relatório - Produtos mais vendidos por período")
                                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken4);

                                column.Item()
                                    .Text($"Período: {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}")
                                    .FontSize(12).FontColor(Colors.Grey.Darken2);

                                column.Item().PaddingBottom(10);
                            });
                        });
                    }

                    page.Content().Column(col =>
                    {
                        var faturamentoTotalDoPeriodo = 0.00m;

                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).AlignLeft().Text("Código");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Nome");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Quantidade vendida");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Estoque atual");
                                header.Cell().Element(CellStyle).AlignRight().Text("Faturamento total");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Background(Colors.Blue.Darken4)
                                        .DefaultTextStyle(x => x.FontColor(Colors.White).Bold())
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(5);
                                }
                            });


                            var i = 1;
                            foreach (var item in dados)
                            {
                                table.Cell().Element(CellStyle).AlignLeft().Text(item.Codigo.ToString());

                                if (item.Situacao != 1)
                                    table.Cell().Element(CellStyle).AlignCenter().Text($"<{item.Nome}> - INATIVADO").FontColor(Colors.Red.Accent1);
                                else
                                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Nome);
                                    
                                table.Cell().Element(CellStyle).AlignCenter().Text(item.QuantidadeVendida.ToString());
                                table.Cell().Element(CellStyle).AlignCenter().Text(item.EstoqueAtual.ToString());
                                table.Cell().Element(CellStyle).AlignRight().Text("R$" + item.TotalVendido.ToString("N2"));

                                IContainer CellStyle(IContainer container)
                                {
                                    var backgroundColor = i % 2 == 0
                                        ? Colors.White
                                        : Colors.Grey.Lighten4;

                                    return container
                                        .Background(backgroundColor)
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(16);
                                }

                                i++;
                                faturamentoTotalDoPeriodo += item.TotalVendido;
                            }
                        });

                        col.Item().PaddingTop(20);

                        col.Item().Text($"Faturamento total somado entre {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}:")
                                    .Italic().FontSize(14).FontColor(Colors.Grey.Darken2);
                        col.Item().Text("R$" + faturamentoTotalDoPeriodo.ToString("N2"))
                                    .FontSize(13).FontColor(Colors.Grey.Darken2);
                    });

                    page.Footer().AlignCenter().PaddingTop(10).Row(row =>
                    {
                        row.AutoItem().Text(text =>
                        {
                            text.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm} | ");
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        public async Task<IEnumerable<ProdutosMaisVendidosDTO>> GetProdutosMaisVendidosAsync(string inicio, string fim)
        {
            var itens = await _repostorio.GetProdutosMaisVendidos(StringToDateTimeInicio(inicio), StringToDateTimeFim(fim));
            return itens;
        }

        public async Task<byte[]> GerarRelatorioPessoasQueMaisCompraramAsync(string inicio, string fim)
        {
            var dateInicio = StringToDateTimeInicio(inicio);
            var dateFim = StringToDateTimeFim(fim);

            var dados = await _repostorio.GetPessoasQueMaisCompraram(dateInicio, dateFim);

            dateFim = dateFim.AddDays(-1);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Element(ComposeHeader);

                    void ComposeHeader(IContainer container)
                    {
                        container.Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item()
                                    .Text("Relatório - Clientes que mais compraram por período")
                                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken4);

                                column.Item()
                                    .Text($"Período: {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}")
                                    .FontSize(12).FontColor(Colors.Grey.Darken2);

                                column.Item().PaddingBottom(10);
                            });
                        });
                    }

                    page.Content().Column(col =>
                    {
                        var faturamentoTotalDoPeriodo = 0.00m;

                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).AlignLeft().Text("Código");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Nome");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Quantidade de vendas");
                                header.Cell().Element(CellStyle).AlignRight().Text("Faturamento total");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Background(Colors.Blue.Darken4)
                                        .DefaultTextStyle(x => x.FontColor(Colors.White).Bold())
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(5);
                                }
                            });


                            var i = 1;
                            foreach (var item in dados)
                            {
                                table.Cell().Element(CellStyle).AlignLeft().Text(item.Codigo.ToString());

                                if (item.Situacao != 1)
                                    table.Cell().Element(CellStyle).AlignCenter().Text($"<{item.Nome}> - INATIVADO").FontColor(Colors.Red.Accent1);
                                else
                                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Nome);

                                table.Cell().Element(CellStyle).AlignCenter().Text(item.QuantidadeVendas.ToString());
                                table.Cell().Element(CellStyle).AlignRight().Text("R$" + item.ValorTotalVendas.ToString("N2"));

                                IContainer CellStyle(IContainer container)
                                {
                                    var backgroundColor = i % 2 == 0
                                        ? Colors.White
                                        : Colors.Grey.Lighten4;

                                    return container
                                        .Background(backgroundColor)
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(16);
                                }

                                i++;
                                faturamentoTotalDoPeriodo += item.ValorTotalVendas;
                            }
                        });

                        col.Item().PaddingTop(20);

                        col.Item().Text($"Faturamento total somado entre {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}:")
                                    .Italic().FontSize(14).FontColor(Colors.Grey.Darken2);
                        col.Item().Text("R$" + faturamentoTotalDoPeriodo.ToString("N2"))
                                    .FontSize(13).FontColor(Colors.Grey.Darken2);
                    });

                    page.Footer().AlignCenter().PaddingTop(10).Row(row =>
                    {
                        row.AutoItem().Text(text =>
                        {
                            text.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm} | ");
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        public async Task<IEnumerable<PessoasQueMaisCompraramDTO>> GetPessoasQueMaisCompraramAsync(string inicio, string fim)
        {
            var itens = await _repostorio.GetPessoasQueMaisCompraram(StringToDateTimeInicio(inicio), StringToDateTimeFim(fim));
            return itens;
        }

        public async Task<byte[]> GerarRelatorioVendedoresQueMaisVenderamAsync(string inicio, string fim)
        {
            var dateInicio = StringToDateTimeInicio(inicio);
            var dateFim = StringToDateTimeFim(fim);

            var dados = await _repostorio.GetVendedoresQueMaisVenderam(dateInicio, dateFim);

            dateFim = dateFim.AddDays(-1);

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(30);
                    page.Header().Element(ComposeHeader);

                    void ComposeHeader(IContainer container)
                    {
                        container.Row(row =>
                        {
                            row.RelativeItem().Column(column =>
                            {
                                column.Item()
                                    .Text("Relatório - Vendedores que mais venderam por período")
                                    .SemiBold().FontSize(18).FontColor(Colors.Blue.Darken4);

                                column.Item()
                                    .Text($"Período: {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}")
                                    .FontSize(12).FontColor(Colors.Grey.Darken2);

                                column.Item().PaddingBottom(10);
                            });
                        });
                    }

                    page.Content().Column(col =>
                    {
                        var faturamentoTotalDoPeriodo = 0.00m;

                        col.Item().Table(table =>
                        {

                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(1);
                                columns.RelativeColumn(3);
                                columns.RelativeColumn(2);
                                columns.RelativeColumn(2);
                            });

                            table.Header(header =>
                            {
                                header.Cell().Element(CellStyle).AlignLeft().Text("Código");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Nome");
                                header.Cell().Element(CellStyle).AlignCenter().Text("Quantidade de vendas");
                                header.Cell().Element(CellStyle).AlignRight().Text("Faturamento total");

                                static IContainer CellStyle(IContainer container)
                                {
                                    return container
                                        .Background(Colors.Blue.Darken4)
                                        .DefaultTextStyle(x => x.FontColor(Colors.White).Bold())
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(5);
                                }
                            });


                            var i = 1;
                            foreach (var item in dados)
                            {
                                table.Cell().Element(CellStyle).AlignLeft().Text(item.Codigo.ToString());

                                if (item.Situacao != 1)
                                    table.Cell().Element(CellStyle).AlignCenter().Text($"<{item.Nome}> - INATIVADO").FontColor(Colors.Red.Accent1);
                                else
                                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Nome);

                                table.Cell().Element(CellStyle).AlignCenter().Text(item.QuantidadeVendas.ToString());
                                table.Cell().Element(CellStyle).AlignRight().Text("R$" + item.ValorTotalVendas.ToString("N2"));

                                IContainer CellStyle(IContainer container)
                                {
                                    var backgroundColor = i % 2 == 0
                                        ? Colors.White
                                        : Colors.Grey.Lighten4;

                                    return container
                                        .Background(backgroundColor)
                                        .PaddingVertical(8)
                                        .PaddingHorizontal(16);
                                }

                                i++;
                                faturamentoTotalDoPeriodo += item.ValorTotalVendas;
                            }
                        });

                        col.Item().PaddingTop(20);

                        col.Item().Text($"Faturamento total somado entre {dateInicio:dd/MM/yyyy} até {dateFim:dd/MM/yyyy}:")
                                    .Italic().FontSize(14).FontColor(Colors.Grey.Darken2);
                        col.Item().Text("R$" + faturamentoTotalDoPeriodo.ToString("N2"))
                                    .FontSize(13).FontColor(Colors.Grey.Darken2);
                    });

                    page.Footer().AlignCenter().PaddingTop(10).Row(row =>
                    {
                        row.AutoItem().Text(text =>
                        {
                            text.Span($"Gerado em {DateTime.Now:dd/MM/yyyy HH:mm} | ");
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                    });
                });
            });

            return document.GeneratePdf();
        }

        public async Task<IEnumerable<VendedoresQueMaisVenderamDTO>> GetVendedoresQueMaisVenderamsAsync(string inicio, string fim)
        {
            var itens = await _repostorio.GetVendedoresQueMaisVenderam(StringToDateTimeInicio(inicio), StringToDateTimeFim(fim));
            return itens;
        }

        private DateTime StringToDateTimeInicio(string data)
        {
            data = Regex.Replace(data, "[^0-9]", "");

            if (data.Length != 8)
                throw new ArgumentException("Data informada é inválida");

            return DateTime.ParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture);
        }

        private DateTime StringToDateTimeFim(string data)
        {
            data = Regex.Replace(data, "[^0-9]", "");

            if (data.Length != 8)
                throw new ArgumentException("Data informada é inválida");

            var date = DateTime.ParseExact(data, "ddMMyyyy", CultureInfo.InvariantCulture);

            return date.Date.AddDays(1);
        }
    }
}