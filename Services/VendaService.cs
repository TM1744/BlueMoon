using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Enuns;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;

namespace BlueMoon.Services
{
    public class VendaService : IVendaService
    {
        public readonly IVendaRepositorio _repositorio;

        public VendaService(IVendaRepositorio repositorio) => _repositorio = repositorio;

        public async Task<Venda> AddAsync(Venda venda)
        {
            if (venda.Cliente.Id == venda.Vendedor.Pessoa.Id)
                throw new InvalidOperationException("Cliente e Vendedor não podem ser a mesma pessoa");

            venda.Codigo = await _repositorio.GetGreatCodeNumber() + 1;
            await _repositorio.AddAsync(venda);

            return venda;
        }

        public async Task<Venda> AddItensAsync(Venda venda)
        {
            if (!await _repositorio.ValidateIntegrity(venda))
                throw new InvalidOperationException("Só é possível adicionar itens a uma venda aberta");

            if (venda.Itens.Count == 0)
                throw new InvalidOperationException("Não é possível fechar uma venda sem itens");

            await _repositorio.AddItensAsync(venda);
            return venda;
        }

        public async Task<VendaReadDTO> BuildDTO(Venda venda)
        {

            var dto = new VendaReadDTO();
            dto.Id = venda.Id.ToString();
            dto.IdCliente = venda.Cliente.Id.ToString();
            dto.NomeCliente = venda.Cliente.Nome;
            dto.IdVendedor = venda.Vendedor.Id.ToString();
            dto.NomeVendedor = venda.Vendedor.Pessoa.Nome;
            dto.Codigo = venda.Codigo;
            dto.Situacao = (int)venda.Situacao;
            dto.ValorTotal = decimal.Round(venda.ValorTotal, 2);
            dto.Data = venda.Data.ToString();

            foreach (var item in venda.Itens)
            {
                var dtoItem = new ItemVendaReadDTO();
                dtoItem.Id = item.Id.ToString();
                dtoItem.IdProduto = item.Produto.Id.ToString();
                dtoItem.ProdutoNome = item.ProdutoNome;
                dtoItem.ProdutoMarca = item.ProdutoMarca;
                dtoItem.ProdutoCodigo = item.ProdutoCodigo;
                dtoItem.ProdutoValorVenda = item.ProdutoValorVenda;
                dtoItem.Quantidade = item.Quantidade;
                dtoItem.SubTotal = item.SubTotal;

                dto.Itens.Add(dtoItem);
            }

            return dto;
        }

        public async Task<IEnumerable<VendaReadDTO>> BuildDTOList(IEnumerable<Venda> vendas)
        {
            ICollection<VendaReadDTO> dtos = [];

            foreach (var venda in vendas)
            {
                var dto = new VendaReadDTO();
                dto.Id = venda.Id.ToString();
                dto.IdCliente = venda.Cliente.Id.ToString();
                dto.NomeCliente = venda.Cliente.Nome;
                dto.IdVendedor = venda.Vendedor.Id.ToString();
                dto.NomeVendedor = venda.Vendedor.Pessoa.Nome;
                dto.Codigo = venda.Codigo;
                dto.Situacao = (int)venda.Situacao;
                dto.ValorTotal = decimal.Round(venda.ValorTotal, 2);
                dto.Data = venda.Data.ToString();

                foreach (var item in venda.Itens)
                {
                    var dtoItem = new ItemVendaReadDTO();
                    dtoItem.Id = item.Id.ToString();
                    dtoItem.IdProduto = item.Produto.Id.ToString();
                    dtoItem.ProdutoNome = item.ProdutoNome;
                    dtoItem.ProdutoMarca = item.ProdutoMarca;
                    dtoItem.ProdutoCodigo = item.ProdutoCodigo;
                    dtoItem.ProdutoValorVenda = item.ProdutoValorVenda;
                    dtoItem.Quantidade = item.Quantidade;
                    dtoItem.SubTotal = item.SubTotal;

                    dto.Itens.Add(dtoItem);
                }

                dtos.Add(dto);
            }

            return dtos;
        }

        public async Task<IEnumerable<Venda>> GetAllAsync()
        {
            var vendas = await _repositorio.GetAllAsync();

            if (!vendas.Any())
                throw new ArgumentException("Não há nenhuma venda cadastrada");

            return vendas;
        }

        public async Task<Venda> GetByIdAsync(Guid id)
        {
            var venda = await _repositorio.GetByIdAsync(id);

            if (venda == null)
                throw new ArgumentException("Não há nenhuma venda com esse ID");

            return venda;
        }

        public async Task CancelarVenda(Guid id)
        {
            var venda = await _repositorio.GetByIdAsync(id);

            if (venda == null)
                throw new ArgumentException("Não há nenhuma venda com esse ID");

            if (venda.Situacao == EnumSituacaoVenda.CANCELADA)
                throw new InvalidOperationException("Essa venda já foi cancelada");

            if (venda.Situacao != EnumSituacaoVenda.ABERTA && venda.Situacao != EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Somente vendas abertas ou fechadas podem ser canceladas");

            await _repositorio.Cancelar(venda);
        }

        public async Task FaturarVenda(Guid id)
        {
            var venda = await _repositorio.GetByIdAsync(id);

            if (venda == null)
                throw new ArgumentException("Não há nenhuma venda com esse ID");

            if (venda.Situacao == EnumSituacaoVenda.FATURADA)
                throw new InvalidOperationException("Venda já faturada");

            if (venda.Situacao != EnumSituacaoVenda.FECHADA)
                throw new InvalidOperationException("Somente vendas fechadas podem ser faturadas");

            await _repositorio.Faturar(venda);
        }
    }
}