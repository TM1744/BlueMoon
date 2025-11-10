using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;
using BlueMoon.Entities.Enuns;
using System.Text.RegularExpressions;

namespace BlueMoon.Services
{
    public class ProdutoService : IProdutoService
    {
        public readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoService(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<Produto> GetByCodigo(int codigo)
        {
            var produto = await _produtoRepositorio.GetByCodigo(codigo);

            if (produto == null)
                throw new ArgumentException("Não há nenhum produto com esse código");

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            var produtos = await _produtoRepositorio.GetByNome(nome.ToUpper());

            if (!produtos.Any())
                throw new ArgumentException("Não há nenhum produto com esse nome");

            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetByMarca(string marca)
        {
            var produtos = await _produtoRepositorio.GetByMarca(marca.ToUpper());

            if (!produtos.Any())
                throw new ArgumentException("Não há nenhum produto com essa marca");

            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetByNCM(string ncm)
        {
            ncm = Regex.Replace(ncm, "[^0-9]", "");

            var produtos = await _produtoRepositorio.GetByNCM(ncm);

            if (!produtos.Any())
                throw new ArgumentException("Não há nenhum produto com esse NCM");

            return produtos;
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            var produto = await _produtoRepositorio.GetByIdAsync(id);

            if (produto == null || produto.Situacao == SituacaoProdutoEnum.INATIVO)
                throw new ArgumentException("Não há nenhum produto com esse ID ou ele já foi deletado");

            await _produtoRepositorio.LogicalDeleteByIdAsync(produto);
        }

        public async Task<IEnumerable<Produto>> GetAllAsync()
        {
            var produtos = await _produtoRepositorio.GetAllAsync();

            if (!produtos.Any())
                throw new InvalidOperationException("Não há nenhum produto cadastrado");

            return produtos;
        }

        public async Task<Produto> GetByIdAsync(Guid id)
        {
            var produto = await _produtoRepositorio.GetByIdAsync(id);

            if (produto.Situacao != SituacaoProdutoEnum.ATIVO || produto == null)
                throw new ArgumentException("Não há nenhum produto com esse ID");

            return produto;
        }

        public async Task<Produto> AddAsync(Produto produto)
        {
            if (!await _produtoRepositorio.ValidateUniqueness(produto))
                throw new ArgumentException("A descrição ou código de barras de produto já foram cadastrados");

            produto.Codigo = await _produtoRepositorio.GetGreaterCodeNumber() + 1;
            await _produtoRepositorio.AddAsync(produto);

            return produto;
        }

        public async Task<Produto> UpdateAsync(Produto produto)
        {
            if (!await _produtoRepositorio.ValidateUniqueness(produto))
                throw new ArgumentException("A descrição do produto ou seu código de barras já foram cadastrados");

            await _produtoRepositorio.UpdateAsync(produto);

            return produto;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _produtoRepositorio.Exists(id);
        }

        public async Task<ProdutoReadDTO> BuildDTO(Produto produto)
        {
            ProdutoReadDTO dto = new ProdutoReadDTO();

            dto.Id = produto.Id.ToString();
            dto.Codigo = produto.Codigo;
            dto.Situacao = (int)produto.Situacao;
            dto.Nome = produto.Nome;
            dto.Descricao = produto.Descricao;
            dto.Marca = produto.Marca;
            dto.QuantidadeEstoque = produto.QuantidadeEstoque;
            dto.QuantidadeEstoqueMinimo = produto.QuantidadeEstoqueMinimo;
            dto.NCM = produto.NCM;
            dto.CodigoBarras = produto.CodigoBarras;
            dto.ValorCusto = produto.ValorCusto;
            dto.ValorVenda = produto.ValorVenda;
            dto.MargemLucro = produto.MargemLucro;

            return dto;
        }

        public async Task<IEnumerable<ProdutoReadDTO>> BuildDTOList(IEnumerable<Produto> produtos)
        {
            ICollection<ProdutoReadDTO> dtos = [];

            foreach (Produto produto in produtos)
            {
                ProdutoReadDTO dto = new ProdutoReadDTO();
                dto.Id = produto.Id.ToString();
                dto.Codigo = produto.Codigo;
                dto.Situacao = (int)produto.Situacao;
                dto.Nome = produto.Nome;
                dto.Descricao = produto.Descricao;
                dto.Marca = produto.Marca;
                dto.QuantidadeEstoque = produto.QuantidadeEstoque;
                dto.QuantidadeEstoqueMinimo = produto.QuantidadeEstoqueMinimo;
                dto.NCM = produto.NCM;
                dto.CodigoBarras = produto.CodigoBarras;
                dto.ValorCusto = produto.ValorCusto;
                dto.ValorVenda = produto.ValorVenda;
                dto.MargemLucro = produto.MargemLucro;

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}