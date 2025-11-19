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
                return Ok(await _service.BuildDTOList(await _service.GetAllAsync()));
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
                return Ok(await _service.BuildDTO(await _service.GetByIdAssync(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<PessoaReadDTO>> Post(PessoaCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                return base.Ok(await _service.BuildDTO(await _service.AddAssync(new Pessoa(dto))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<PessoaReadDTO>> Put(PessoaUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!await _service.Exists(Guid.Parse(dto.Id)))
                    return NotFound("Não há nenhuma pessoa com esse ID");

                return base.Ok(await _service.BuildDTO(await _service.UpdateAssync(new Pessoa(dto))));
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

        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<PessoaMiniReadDTO>>> Search(PessoaSearchDTO dto)
        {
            try
            {
                return Ok(await _service.BuildDTOList(await _service.GetBySearch(dto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    
        [HttpGet("No-users")]
        public async Task<ActionResult<IEnumerable<PessoaMiniReadDTO>>> GetNoUsers()
        {
            try
            {
                return Ok(await _service.GetNoUsers());
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}