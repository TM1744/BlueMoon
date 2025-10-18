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
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var itens = await _service.GetAllAsync();
            return Ok(itens);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<Produto>> Get(string Id)
        {
            var idConvertido = Guid.Parse(Id);
            var item = await _service.GetByIdAsync(idConvertido);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(ProdutoCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Produto produto = new Produto(createDTO);
            var id = produto.Id;
            await _service.AddAsync(produto);
            return CreatedAtAction(nameof(Get), new { id = produto.Id }, produto);
        }

        [HttpPut]
        public async Task<ActionResult<Produto>> Put(ProdutoUpdateDTO updateDTO)
        {
            Produto produto = new Produto(updateDTO);
            await _service.UpdateAsync(produto);
            return Ok(produto);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            var produto = await _service.GetByIdAsync(Guid.Parse(Id));
            if (produto == null)
                return NotFound();

            await _service.LogicalDeleteByIdAsync(produto);
            return Ok();
        }

        [HttpGet("por-descricao")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByDescricao([FromQuery] string descricao)
        {
            var produtos = await _service.GetByDescricao(descricao.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet("por-ncm")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByNCM([FromQuery] string ncm)
        {
            var produtos = await _service.GetByNCM(ncm.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

        [HttpGet("por-marca")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetByMarca([FromQuery] string marca)
        {
            var produtos = await _service.GetByMarca(marca.ToUpper());
            if (!produtos.Any())
                return NotFound();

            return Ok(produtos);
        }

    }
}