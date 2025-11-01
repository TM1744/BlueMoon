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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _service;

        public ProdutosController(IProdutoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> Get()
        {
            try
            {
                return Ok(await _service.GetAllAsync());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProdutoReadDTO>> Get(string id)
        {
            try
            {
                return Ok(await _service.GetByIdAsync(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<ProdutoReadDTO>> Post(ProdutoCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return Ok(await _service.AddAsync(new Produto(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ProdutoReadDTO>> Put(ProdutoUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!await _service.Exists(Guid.Parse(dto.Id)))
                    return NotFound("Não há nenhum produto com esse ID");
                    
                return Ok(await _service.UpdateAsync(new Produto(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.LogicalDeleteByIdAsync(Guid.Parse(id));
                return Ok("Produto deletado");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-nome")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByNome([FromQuery] string nome)
        {
            try
            {
                return Ok(await _service.GetByNome(nome));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-ncm")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByNCM([FromQuery] string ncm)
        {
            try
            {
                return Ok(await _service.GetByNCM(ncm));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-marca")]
        public async Task<ActionResult<IEnumerable<ProdutoReadDTO>>> GetByMarca([FromQuery] string marca)
        {
            try
            {
                return Ok(await _service.GetByMarca(marca));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-codigo")]
        public async Task<ActionResult<ProdutoReadDTO>> GetByCodigo([FromQuery] int codigo)
        {
            try
            {
                return Ok(await _service.GetByCodigo(codigo));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}