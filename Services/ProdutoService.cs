using BlueMoon.DTO;
using BlueMoon.Entities.Models;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services.Interfaces;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Services
{
    public class ProdutoService : IProdutoService
    {
        public readonly IProdutoRepositorio _produtoRepositorio;
        public ProdutoService(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        public async Task<ProdutoReadDTO> GetByCodigo(int codigo)
        {
            var produto = await _produtoRepositorio.GetByCodigo(codigo);

            if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                return await BuildDTO(produto);

            return null;
        }

        public async Task<IEnumerable<ProdutoReadDTO>> GetByDescricao(string descricao)
        {
            var produtos = await _produtoRepositorio.GetByDescricao(descricao);
            ICollection<ProdutoReadDTO> produtosDtos = [];
            foreach (Produto produto in produtos)
            {
                if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                    produtosDtos.Add(await BuildDTO(produto));

            }

            return produtosDtos;
        }

        public async Task<IEnumerable<ProdutoReadDTO>> GetByMarca(string marca)
        {
            var produtos = await _produtoRepositorio.GetByMarca(marca);
            ICollection<ProdutoReadDTO> produtosDtos = [];
            foreach (Produto produto in produtos)
            {
                if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                    produtosDtos.Add(await BuildDTO(produto));
            }

            return produtosDtos;
        }

        public async Task<IEnumerable<ProdutoReadDTO>> GetByNCM(string ncm)
        {
            var produtos = await _produtoRepositorio.GetByNCM(ncm);
            ICollection<ProdutoReadDTO> produtosDtos = [];
            foreach (Produto produto in produtos)
            {
                if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                    produtosDtos.Add(await BuildDTO(produto));
            }

            return produtosDtos;
        }

        public async Task LogicalDeleteByIdAsync(Guid id)
        {
            if (await _produtoRepositorio.Exists(id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            var produto = await _produtoRepositorio.GetByIdAsync(id);
            await _produtoRepositorio.LogicalDeleteByIdAsync(produto);
        }

        public async Task<IEnumerable<ProdutoReadDTO>> GetAllAsync()
        {
            var produtos = await _produtoRepositorio.GetAllAsync();
            ICollection<ProdutoReadDTO> produtosDtos = [];
            foreach (Produto produto in produtos)
            {
                if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                    produtosDtos.Add(await BuildDTO(produto));
            }

            return produtosDtos;
        }

        public async Task<ProdutoReadDTO> GetByIdAsync(Guid id)
        {
            if (await _produtoRepositorio.Exists(id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            var produto = await _produtoRepositorio.GetByIdAsync(id);

            if (produto.Situacao == SituacaoProdutoEnum.ATIVO)
                return await BuildDTO(produto);

            return null;
        }

        public async Task<ProdutoReadDTO> AddAsync(Produto produto)
        {
            if (!await _produtoRepositorio.ValidateUniqueness(produto))
                throw new ArgumentException("A descrição do produto ou seu código de barras já foram cadastrados");

            produto.Codigo = await _produtoRepositorio.GetGreaterCodeNumber() + 1;
            await _produtoRepositorio.AddAsync(produto);

            return await BuildDTO(produto);
        }

        public async Task<ProdutoReadDTO> UpdateAsync(Produto produto)
        {
            if (await _produtoRepositorio.Exists(produto.Id) == false)
                throw new ArgumentException("O id fornecido não existe no banco de dados");

            if (!await _produtoRepositorio.ValidateUniqueness(produto))
                throw new ArgumentException("A descrição do produto ou seu código de barras já foram cadastrados");

            await _produtoRepositorio.UpdateAsync(produto);

            return await BuildDTO(produto);
        }

        private async Task<ProdutoReadDTO> BuildDTO(Produto produto)
        {
            ProdutoReadDTO dto = new ProdutoReadDTO();

            dto.Id = produto.Id.ToString();
            dto.Codigo = produto.Codigo;
            dto.Situacao = (int)produto.Situacao;
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
    }
}