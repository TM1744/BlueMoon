using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Services.Interfaces;
using QuestPDF.Fluent;
using QuestPDF.Helpers;

namespace BlueMoon.Services
{
    public class RelatorioService : IRelatorioService
    {
        public byte[] GerarRelatorioProdutosMaisVendidos()
        {
            

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Relatório Genérico")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content()
                        .PaddingVertical(20)
                        .Column(col =>
                        {
                            col.Spacing(10);

                            col.Item().Text($"Data de geração: {DateTime.Now:dd/MM/yyyy HH:mm}");
                            col.Item().Text("Este é um relatório de exemplo gerado com QuestPDF em uma API ASP.NET Core convencional.");

                            col.Item().LineHorizontal(1).LineColor(Colors.Grey.Lighten1);

                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.ConstantColumn(40);
                                    columns.RelativeColumn();
                                    columns.RelativeColumn();
                                });

                                // Cabeçalho
                                table.Header(header =>
                                {
                                    header.Cell().Text("#").Bold();
                                    header.Cell().Text("Nome").Bold();
                                    header.Cell().Text("Valor").Bold();
                                });

                                // Dados de exemplo
                                for (int i = 1; i <= 5; i++)
                                {
                                    table.Cell().Text(i.ToString());
                                    table.Cell().Text($"Item {i}");
                                    table.Cell().Text($"R$ {i * 10},00");
                                }
                            });
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Página ").FontSize(10);
                            x.CurrentPageNumber().FontSize(10);
                            x.Span(" de ").FontSize(10);
                            x.TotalPages().FontSize(10);
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}