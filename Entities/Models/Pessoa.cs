// using BlueMoon.Entities.Enuns;
// namespace BlueMoon.Entities.Models
// {
//     public class Pessoa
//     {
//         public Guid Id { get; private set; } = Guid.NewGuid();
//         public TipoPessoaEnum Tipo { get; set; }
//         public SituacaoPessoaEnum Situacao { get; set; }
//         public int Codigo { get; private set; }
//         public ICollection<Telefone> Telefones { get; private set; } = [];
//         public string Email { get; set; } = string.Empty;
//         public string Nome { get; set; } = string.Empty;
//         public string Documento { get; private set; } = string.Empty;
//         public string InscricaoMunicipal { get; private set; } = string.Empty;
//         public string InscricaoEstadual { get; private set; } = string.Empty;
//         public Endereco Endereco { get; set; }

        
//         public void AdicionarTelefone(Telefone telefone)
//         {
//             if (Telefones.Any(t => t.Numero == telefone.Numero))
//                 throw new InvalidOperationException("Telefone já existe.");
//             Telefones.Add(telefone);
//         }

//         public void RetirarTelefone(Telefone telefone)
//         {
//             if (!Telefones.Any(t => t.Numero == telefone.Numero))
//                 throw new InvalidOperationException("Telefone não existe.");
//             Telefones.Remove(telefone);
//         }

//         public void InativarPessoa()
//         {
//             if (Situacao == SituacaoPessoaEnum.INATIVO)
//                 throw new InvalidOperationException("Pessoa já inativa.");
//             Situacao = SituacaoPessoaEnum.INATIVO;
//         }
//     }
// }