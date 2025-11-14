// using Microsoft.AspNetCore.Mvc;
// using QuestPDF.Fluent;
// using QuestPDF.Helpers;
// using QuestPDF.Infrastructure;
// using QuestPDF.Infrastructure;



// namespace BlueMoon.Controllers
// {
//     [ApiController]
//     [Route("Bluemoon/[controller]")] // ← corrigido o uso do [controller]
//     public class RelatoriosController : ControllerBase
//     {
//         [HttpGet("gerar")]
//         public IActionResult GerarRelatorio()
//         {
//             // Gera o PDF em memória
//             var pdf = GerarRelatorioExemplo();

//             // Retorna o PDF como arquivo
//             return File(pdf, "application/pdf", "relatorio.pdf");
//         }
//     }
// }
