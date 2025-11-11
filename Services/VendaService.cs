using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
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
            venda.Codigo = await _repositorio.GetGreatCodeNumber() + 1;
            await _repositorio.AddAsync(venda);

            return venda;
        }

        public async Task<Venda> AddItensAsync(Venda venda)
        {
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

            await _repositorio.Cancelar(venda);
        }

        public async Task FaturarVenda(Guid id)
        {
            var venda = await _repositorio.GetByIdAsync(id);

            if (venda == null)
                throw new ArgumentException("Não há nenhuma venda com esse ID");

            await _repositorio.Faturar(venda);
        }
    }
}