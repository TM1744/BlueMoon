using BlueMoon.DTO;
using BlueMoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/Bluemoon/[Controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _service;

        public PessoaController(IPessoaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaReadDTO>>> Get()
        {
            var itens = await _service.GetAllAsync();
            if (!itens.Any())
                return NoContent();
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
                    return NotFound();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}