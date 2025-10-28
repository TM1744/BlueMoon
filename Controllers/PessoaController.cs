using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Services;
using BlueMoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/Bluemoon/[Controller]")]
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _service;

        public PessoasController(IPessoaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> Get()
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
        public async Task<ActionResult<PessoaReadDTO>> Get(string id)
        {
            try
            {
                return Ok(await _service.GetByIdAssync(Guid.Parse(id)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PessoaReadDTO>> Post(PessoaCreateDTO dto)
        {
            try
            {
                return Ok(await _service.AddAssync(new Pessoa(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<PessoaReadDTO>> Put(PessoaUpdateDTO dto)
        {
            try
            {
                if (!await _service.Exists(Guid.Parse(dto.Id)))
                    return NotFound("Não há nehuma pessoa com esse ID");
                    
                return Ok(await _service.UpdateAssync(new Pessoa(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string Id)
        {
            try
            {
                await _service.LogicalDeleteByIdAsync(Guid.Parse(Id));
                return Ok("Pessoa deletada");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-nome/{nome}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByNome(string nome)
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

        [HttpGet("/por-documento/{documento}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByDocumento(string documento)
        {
            try
            {
                return Ok(await _service.GetByDocumento(documento));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-telefone/{telefone}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByTelefone(string telefone)
        {
            try
            {
                return Ok(await _service.GetByTelefone(telefone));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-local/{local}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByLocal(string local)
        {
            try
            {
                return Ok(await _service.GetByLocal(local));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-codigo/{codigo}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByCodigo(int codigo)
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