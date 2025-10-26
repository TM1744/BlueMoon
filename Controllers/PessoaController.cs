using BlueMoon.DTO;
using BlueMoon.Entities.Models;
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
            var itens = await _service.GetAllAsync();
            if (!itens.Any())
                return NotFound("Não há nenhuma pessoa cadastrada");
            return Ok(itens);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaReadDTO>> Get(string id)
        {
            try
            {
                var idConvertido = Guid.Parse(id);
                var item = await _service.GetByIdAssync(idConvertido);
                if (item == null)
                    return NotFound("Não foi encontrada nenhuma pessoa com esse Id");
                return Ok(item);
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
                Pessoa pessoa = new Pessoa(dto);
                return Ok(await _service.AddAssync(pessoa));
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
                Pessoa pessoa = new Pessoa(dto);
                return Ok(await _service.UpdateAssync(pessoa));
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
                var idConvertido = Guid.Parse(Id);
                await _service.LogicalDeleteByIdAsync(idConvertido);
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
            var pessoas = await _service.GetByNome(nome);
            if (pessoas == null)
                return NotFound("Não foi encontrada nenhuma pessoa com esse nome");

            return Ok(pessoas);
        }

        [HttpGet("/por-documento/{documento}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByDocumento(string documento)
        {
            var pessoas = await _service.GetByDocumento(documento);
            if (pessoas == null)
                return NotFound("Não foi encontrada nenhuma pessoa com esse documento");

            return Ok(pessoas);
        }

        [HttpGet("/por-telefone/{telefone}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByTelefone(string telefone)
        {
            var pessoas = await _service.GetByTelefone(telefone);
            if (pessoas == null)
                return NotFound("Não foi encontrada nenhuma pessoa com esse telefone");

            return Ok(pessoas);
        }

        [HttpGet("/por-local/{local}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByLocal(string local)
        {
            var pessoas = await _service.GetByLocal(local);
            if (pessoas == null)
                return NotFound("Não foi encontrada nenhuma pessoa relacionada a esse local");

            return Ok(pessoas);
        }

        [HttpGet("/por-codigo/{codigo}")]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> GetByCodigo(int codigo)
        {
            var pessoas = await _service.GetByCodigo(codigo);
            if (pessoas == null)
                return NotFound("Não foi encontrada nenhuma pessoa com esse codigo");

            return Ok(pessoas);
        }
    }
}