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
    public class VendasController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IPessoaService _pessoaService;
        private readonly IProdutoService _produtoService;
        private readonly IVendaService _vendaService;

        public VendasController(IUsuarioService usuarioService, IPessoaService pessoaService,
        IProdutoService produtoService, IVendaService vendaService)
        {
            _usuarioService = usuarioService;
            _pessoaService = pessoaService;
            _produtoService = produtoService;
            _vendaService = vendaService;
        }

        [HttpPost]
        public async Task<ActionResult<VendaReadDTO>> PostVenda(VendaCreateDTO dto)
        {
            try
            {
                var pessoa = await _pessoaService.GetByIdAssync(Guid.Parse(dto.IdPessoa));
                var usuario = await _usuarioService.GetByIdAssync(Guid.Parse(dto.IdUsuario));

                return Ok(await _vendaService.BuildDTO(await _vendaService.AddAsync(new Venda(usuario, pessoa))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/Itens")]
        public async Task<ActionResult<VendaReadDTO>> PostItens(string id, IEnumerable<ItemVendaCreateDTO> dtos)
        {
            try
            {
                var venda = await _vendaService.GetByIdAsync(Guid.Parse(id));
                ICollection<ItemVenda> itens = [];
                foreach (ItemVendaCreateDTO dtoItem in dtos)
                {
                    var produto = await _produtoService.GetByIdAsync(Guid.Parse(dtoItem.IdProduto));
                    var itemVenda = new ItemVenda(produto, dtoItem.Quantidade);
                    itens.Add(itemVenda);
                }
                venda.AdicionarItens(itens);

                return Ok(await _vendaService.BuildDTO(await _vendaService.AddItensAsync(venda)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VendaReadDTO>>> Get()
        {
            try
            {
                return Ok(await _vendaService.BuildDTOList(await _vendaService.GetAllAsync()));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VendaReadDTO>> Get(string id)
        {
            try
            {
                return Ok(await _vendaService.BuildDTO(await _vendaService.GetByIdAsync(Guid.Parse(id))));
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id}/Faturar")]
        public async Task<ActionResult<string>> PatchFaturar(string id)
        {
            try
            {
                await _vendaService.FaturarVenda(Guid.Parse(id));
                return Ok("Venda faturada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/Cancelar")]
        public async Task<ActionResult<string>> PatchCancelar(string id)
        {
            try
            {
                await _vendaService.CancelarVenda(Guid.Parse(id));
                return Ok("Venda cancelada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}/Estornar")]
        public async Task<ActionResult<string>> PatchEstornar(string id)
        {
            try
            {
                await _vendaService.EstornarVenda(Guid.Parse(id));
                return Ok("Venda cancelada");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}