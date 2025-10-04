using BlueMoon.Models.Enuns;
namespace BlueMoon.Models
{
    public class Telefone
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public DddEnum DDD { get; set; }
        public string Numero { get; set; }

        public Telefone(
            DddEnum ddd,
            string numero
        )
        {
            DDD = ddd;
            Numero = numero;
        }
    }
}