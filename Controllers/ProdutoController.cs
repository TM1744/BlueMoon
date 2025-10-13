using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/BlueMoon/[Controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _service;

        public ProdutoController(ProdutoService service)
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
        public async Task<ActionResult<Produto>> Get(string id)
        {
            var idConvertido = Guid.Parse(id);
            var item = await _service.GetByIdAsync(idConvertido);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> Post(ProdutoCreateDTO createDTO)
        {
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
        public async Task<IActionResult> Delete(string id)
        {
            var idConvertido = Guid.Parse(id);
            var result = await _service.GetByIdAsync(idConvertido);
            if (result == null)
                return NotFound();
            await _service.LogicalDeleteByIdAsync(idConvertido);
            return NoContent();
        }

        [HttpGet("/Search/{Descricao}")]
        public async Task<ActionResult<IEnumerable<Produto>>> SearchByDescricao(string descricao)
        {
            var results = await _service.GetByDescricao(descricao);
            return Ok(results);
        }

        [HttpGet("/Search/{NCM}")]
        public async Task<ActionResult<IEnumerable<Produto>>> SearchByNCM(string ncm)
        {
            var results = await _service.GetByNCM(ncm);
            return Ok(results);
        }

        [HttpGet("/Search/{Marca}")]
        public async Task<ActionResult<IEnumerable<Produto>>> SearchByMarca(string marca)
        {
            var results = await _service.GetByMarca(marca);
            return Ok(results);
        }
    }
}