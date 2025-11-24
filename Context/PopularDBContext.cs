using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Context
{
    public class PopularDBContext
    {
        public static void Popular(MySqlDataBaseContext contexto)
        {
            contexto.Database.EnsureCreated();
            PopularProduto(contexto);
            PopularPessoa(contexto);
            contexto.SaveChanges();
        }

        private static void PopularProduto(MySqlDataBaseContext contexto)
        {
            if (contexto.Produtos.Any())
                return;

            var produtos = new List<ProdutoCreateDTO>
            {
                new ProdutoCreateDTO
                {
                    Nome = "Mouse Gamer RGB",
                    Descricao = "Mouse óptico gamer com iluminação RGB e 6 botões programáveis.",
                    Marca = "Logitech",
                    QuantidadeEstoque = 150,
                    QuantidadeEstoqueMinimo = 20,
                    NCM = "84716052",
                    CodigoBarras = "7891234567890",
                    ValorCusto = 85.50m,
                    ValorVenda = 149.90m,
                    MargemLucro = 43.00m
                },
                new ProdutoCreateDTO
                {
                    Nome = "Teclado Mecânico ABNT2",
                    Descricao = "Teclado mecânico com switches azuis e iluminação LED branca.",
                    Marca = "Redragon",
                    QuantidadeEstoque = 90,
                    QuantidadeEstoqueMinimo = 15,
                    NCM = "84716053",
                    CodigoBarras = "7890987654321",
                    ValorCusto = 180.00m,
                    ValorVenda = 299.90m,
                    MargemLucro = 40.00m
                },
                new ProdutoCreateDTO
                {
                    Nome = "Monitor LED 24'' Full HD",
                    Descricao = "Monitor LED de 24 polegadas com resolução Full HD e tecnologia IPS.",
                    Marca = "AOC",
                    QuantidadeEstoque = 45,
                    QuantidadeEstoqueMinimo = 10,
                    NCM = "85285200",
                    CodigoBarras = "7893216549870",
                    ValorCusto = 650.00m,
                    ValorVenda = 899.90m,
                    MargemLucro = 27.78m
                },
                new ProdutoCreateDTO
                {
                    Nome = "Headset Bluetooth",
                    Descricao = "Fone de ouvido sem fio com microfone e cancelamento de ruído.",
                    Marca = "Sony",
                    QuantidadeEstoque = 120,
                    QuantidadeEstoqueMinimo = 25,
                    NCM = "85183000",
                    CodigoBarras = "7896541237890",
                    ValorCusto = 210.00m,
                    ValorVenda = 349.90m,
                    MargemLucro = 39.94m
                },
                new ProdutoCreateDTO
                {
                    Nome = "Cadeira Gamer Reclinável",
                    Descricao = "Cadeira ergonômica com apoio de braços ajustável e base metálica.",
                    Marca = "ThunderX3",
                    QuantidadeEstoque = 30,
                    QuantidadeEstoqueMinimo = 5,
                    NCM = "94013090",
                    CodigoBarras = "7897412589630",
                    ValorCusto = 890.00m,
                    ValorVenda = 1299.90m,
                    MargemLucro = 31.53m
                }
            };

            var valor = 1;
            foreach (var produtoDto in produtos)
            {
                var produto = new Produto(produtoDto);
                produto.Codigo = valor;
                contexto.Produtos.Add(produto);
                valor++;
            }
                
        }

        private static void PopularPessoa(MySqlDataBaseContext contexto)
        {
            if (contexto.Pessoas.Any())
                return;

            var pessoas = new List<PessoaCreateDTO>
            {
                new PessoaCreateDTO
                {
                    Tipo = 1, // 1 = Pessoa Física
                    Nome = "João Pedro da Silva",
                    Telefone = "(11) 98765-4321",
                    Email = "joaopedro.silva@gmail.com",
                    Documento = "988.747.390-10",
                    InscricaoMunicipal = "",
                    InscricaoEstadual = "",
                    CEP = "04567-000",
                    Logradouro = "Rua das Palmeiras",
                    Numero = "123",
                    Complemento = "Apto 45",
                    Bairro = "Jardim Paulista",
                    Cidade = "São Paulo",
                    Estado = 25 // SP
                },
                new PessoaCreateDTO
                {
                    Tipo = 2, // 2 = Pessoa Jurídica
                    Nome = "Tech Solutions LTDA",
                    Telefone = "(21) 4002-8922",
                    Email = "contato@techsolutions.com.br",
                    Documento = "16.064.164/0001-12",
                    InscricaoMunicipal = "",
                    InscricaoEstadual = "1431601-95",
                    CEP = "20031-170",
                    Logradouro = "Av. Rio Branco",
                    Numero = "350",
                    Complemento = "Sala 804",
                    Bairro = "Centro",
                    Cidade = "Rio de Janeiro",
                    Estado = 19 // RJ
                },
                new PessoaCreateDTO
                {
                    Tipo = 1,
                    Nome = "Maria Clara Souza",
                    Telefone = "(31) 99876-1122",
                    Email = "maria.clara@hotmail.com",
                    Documento = "618.384.830-12",
                    InscricaoMunicipal = "",
                    InscricaoEstadual = "",
                    CEP = "30140-110",
                    Logradouro = "Rua da Bahia",
                    Numero = "2100",
                    Complemento = "Bloco B",
                    Bairro = "Lourdes",
                    Cidade = "Belo Horizonte",
                    Estado = 31 // MG
                },
                new PessoaCreateDTO
                {
                    Tipo = 2,
                    Nome = "Agropecuária Santa Luzia ME",
                    Telefone = "(65) 3664-2299",
                    Email = "financeiro@agrosantaluzia.com",
                    Documento = "32.401.673/0001-40",
                    InscricaoMunicipal = "",
                    InscricaoEstadual = "507.896.635.130",
                    CEP = "78040-400",
                    Logradouro = "Rodovia BR-364",
                    Numero = "S/N",
                    Complemento = "Km 12 - Zona Rural",
                    Bairro = "Santa Luzia",
                    Cidade = "Cuiabá",
                    Estado = 51 // MT
                },
                new PessoaCreateDTO
                {
                    Tipo = 1,
                    Nome = "Carlos Henrique Almeida",
                    Telefone = "(41) 98822-3344",
                    Email = "carlos.h.almeida@gmail.com",
                    Documento = "967.667.850-39",
                    InscricaoMunicipal = "",
                    InscricaoEstadual = "",
                    CEP = "80030-000",
                    Logradouro = "Av. Sete de Setembro",
                    Numero = "1500",
                    Complemento = "Ap. 201",
                    Bairro = "Centro",
                    Cidade = "Curitiba",
                    Estado = 41 // PR
                }
            };

            var valor = 1;
            foreach (var pessoaDto in pessoas)
            {
                var pessoa = new Pessoa(pessoaDto);
                pessoa.Codigo = valor;
                contexto.Pessoas.Add(pessoa);
                valor++;
            }
                
        }
    }
}