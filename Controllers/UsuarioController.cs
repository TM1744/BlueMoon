using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Services;
using BlueMoon.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlueMoon.Controllers
{
    [ApiController]
    [Route("/Bluemoon/[Controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPessoaService _pessoaService;

        public UsuariosController(IUsuarioService usuarioService, IPessoaService pessoaService)
        {
            _usuarioService = usuarioService;
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> Get()
        {
            try
            {
                return Ok(await _usuarioService.BuildDTOList(await _usuarioService.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioReadDTO>> Get(string id)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTO(await _usuarioService.GetByIdAssync(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioReadDTO>> Post(UsuarioCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return Ok(await _usuarioService.BuildDTO(
                    await _usuarioService.AddAssync(
                        new Usuario(
                            await _pessoaService.GetByIdAssync(Guid.Parse(dto.IdPessoa)),
                            dto
                        )
                    )
                ));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioReadDTO>> Put(UsuarioUpdateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                if (!await _usuarioService.Exists(Guid.Parse(dto.Id)))
                    return NotFound("Não há nenhum usuário com esse ID");

                return Ok(await _usuarioService.BuildDTO(
                    await _usuarioService.UpdateAssync(
                        new Usuario(
                            await _pessoaService.GetByIdAssync(Guid.Parse(dto.IdPessoa)),
                            dto
                        )
                    )
                ));
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
                await _usuarioService.LogicalDeleteByIdAsync(Guid.Parse(Id));
                return Ok("Usuário deletado");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-nome/{nome}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetByNome(string nome)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTOList(await _usuarioService.GetByNome(nome)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-documento/{documento}")]
        public async Task<ActionResult<UsuarioReadDTO>> GetByDocumento(string documento)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTO(await _usuarioService.GetByDocumento(documento)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-telefone/{telefone}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetByTelefone(string telefone)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTOList(await _usuarioService.GetByTelefone(telefone)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-local/{local}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetByLocal(string local)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTOList(await _usuarioService.GetByLocal(local)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/por-codigo/{codigo}")]
        public async Task<ActionResult<IEnumerable<UsuarioReadDTO>>> GetByCodigo(int codigo)
        {
            try
            {
                return Ok(await _usuarioService.BuildDTO(await _usuarioService.GetByCodigo(codigo)));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<bool>> PostLogin(UsuarioPostLoginDTO dto)
        {
            var resultado = await _usuarioService.PostLogin(dto);

            if (resultado)
                return Ok(true);

            return BadRequest(false);
        }
    }
}