
namespace BlueMoon.DTO
{
    public class TelefoneCreateDTO
    {
        public int DDD { get; set; }
        public string Numero { get; set; } = string.Empty;
    }

    public class TelefoneUpdateDTO
    {
        public string Id { get; set; } = string.Empty;
        public int DDD { get; set; }
        public string Numero { get; set; } = string.Empty;
    }

    public class TelefoneReadDTO
    {
        public string Id { get; set; } = string.Empty;
        public int DDD { get; set; }
        public string Numero { get; set; } = string.Empty;
    }
}