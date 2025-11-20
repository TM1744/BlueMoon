using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories;
using BlueMoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/Bluemoon/[Controller]")]
    public class RelatoriosController : ControllerBase
    {
        private readonly IRelatorioService _service;

        public RelatoriosController(IRelatorioService service) => _service = service;

        [HttpPost("ProdutosMaisVendidos_R")]
        public async Task<IActionResult> GetProdutosMaisVendidosRelatorio(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var pdfBytes = await _service.GerarRelatorioProdutosMaisVendidosAsync(dto.DataInicio, dto.DataFim);
                return File(pdfBytes, "application/pdf", "RelatorioProdutosMaisVendidos.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("ProdutosMaisVendidos")]
        public async Task<IActionResult> GetProdutosMaisVendidos(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _service.GetProdutosMaisVendidosAsync(dto.DataInicio, dto.DataFim));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("PessoasQueMaisCompraram_R")]
        public async Task<IActionResult> GetPessoasQueMaisCompraramRelatorio(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pdfBytes = await _service.GerarRelatorioPessoasQueMaisCompraramAsync(dto.DataInicio, dto.DataFim);
                return File(pdfBytes, "application/pdf", "RelatorioPessoasQueMaisCompraram.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PessoasQueMaisCompraram")]
        public async Task<IActionResult> GetPessoasQueMaisCompraram(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _service.GetPessoasQueMaisCompraramAsync(dto.DataInicio, dto.DataFim));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("VendedoresQueMaisVenderam")]
        public async Task<IActionResult> GetVendedoresQueMaisVenderam(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _service.GetVendedoresQueMaisVenderamsAsync(dto.DataInicio, dto.DataFim));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("VendedoresQueMaisVenderam_R")]
        public async Task<IActionResult> GetVendedoresQueMaisVenderamRelatorio(PeriodoBuscaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var pdfBytes = await _service.GerarRelatorioVendedoresQueMaisVenderamAsync(dto.DataInicio, dto.DataFim);
                return File(pdfBytes, "application/pdf", "RelatorioVendedoresQueMaisVenderam.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}