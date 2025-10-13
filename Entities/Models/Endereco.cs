// using BlueMoon.Entities.Enuns;

// namespace BlueMoon.Entities.Models
// {
//     public sealed class Endereco
//     {
//         public Guid Id { get; private set; } = Guid.NewGuid();
//         public string CEP { get; set; } = string.Empty;
//         public string Logradouro { get; set; } = string.Empty;
//         public string Numero { get; set; } = string.Empty;
//         public string Complemento { get; set; } = string.Empty;
//         public string Bairro { get; set; } = string.Empty;
//         public string Cidade { get; set; } = string.Empty;
//         public EstadoEnum Estado { get; set; }

//         private Endereco() { }

//         public Endereco(
//             string cep,
//             string logradouro,
//             string numero,
//             string? complemento,
//             string bairro,
//             string cidade,
//             EstadoEnum estado
//         )
//         {
//             CEP = cep;
//             Logradouro = logradouro;
//             Numero = numero;
//             Complemento = complemento ?? "NÃO POSSUI COMPLEMENTO";
//             Bairro = bairro;
//             Cidade = cidade;
//             Estado = estado;
//         }
//     }
// }