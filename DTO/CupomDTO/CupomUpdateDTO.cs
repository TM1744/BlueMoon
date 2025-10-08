using System;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.CupomDTO
{
    public class CupomUpdateDTO
    {
        public Guid Id { get; set; }
        public DateTime PrazoValidade { get; set; }
        public decimal? ValorMinimoUtilizacao { get; set; }
        public string Situacao { get; set; }
    }
}
