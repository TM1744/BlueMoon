using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Services;
using BlueMoon.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/BlueMoon/[Controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutoController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> Get()
        {
            var itens = await _service.GetAllAsync();
            if (itens == null)
                return NoContent();
            return Ok(itens);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ProdutoReadDTO>> Get(string Id)
        {
            try
            {
                var idConvertido = Guid.Parse(Id);
                var item = await _service.GetByIdAsync(idConvertido);
                if (item == null)
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProdutoReadDTO>> Post(ProdutoCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState); 

            Produto produto = new Produto(createDTO);
            var id = produto.Id;
            await _service.AddAsync(produto);
            return Ok(produto);
        }

        [HttpPut]
        public async Task<ActionResult<ProdutoReadDTO>> Put(ProdutoUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                Produto produto = new Produto(updateDTO);
                await _service.UpdateAsync(produto);
                return Ok(produto);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                var idConvertido = Guid.Parse(Id);
                await _service.LogicalDeleteByIdAsync(idConvertido);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("por-descricao")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByDescricao([FromQuery] string descricao)
        {
            var produtos = await _service.GetByDescricao(descricao.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet("por-ncm")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByNCM([FromQuery] string ncm)
        {
            var produtos = await _service.GetByNCM(ncm.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet("por-marca")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByMarca([FromQuery] string marca)
        {
            var produtos = await _service.GetByMarca(marca.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet("por-codigo")]
        public async Task<ActionResult<ProdutoReadDTO>> GetByCodigo([FromQuery] int codigo)
        {
            var produto = await _service.GetByCodigo(codigo);
            if (produto == null)
                return NotFound();

            return Ok(produto);
        }

    }
}