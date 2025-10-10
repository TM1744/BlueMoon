using System.Security.Cryptography;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.Entities.Models
{
    public abstract class AbstractUsuario : AbstractPessoa
    {
        public string Login { get; private set; } = string.Empty;
        public string UserName { get; private set; } = string.Empty;
        public string Senha { get; private set; } = string.Empty;
        public CargoUsuarioEnum Cargo { get; private set; }

        protected AbstractUsuario(ICollection<Telefone> telefones, string email, Endereco endereco,
            string login, string userName, string senha, CargoUsuarioEnum cargo)
            : base(telefones, email, endereco)
        {
            Login = login;
            UserName = userName;
            Senha = SenhaParaHash(senha);
            Cargo = cargo;
        }

        private string SenhaParaHash(string senha)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); // 128 bits

            // Deriva a chave com PBKDF2
            var pbkdf2 = new Rfc2898DeriveBytes(senha, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // 256 bits

            // Junta o salt e o hash no mesmo array
            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            // Codifica para Base64 para salvar em string
            return Convert.ToBase64String(hashBytes);
        }
    }
}