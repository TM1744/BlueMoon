using System;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.CupomDTO
{
    public class CupomReadDTO
    {
        public Guid Id { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public DateTime PrazoValidade { get; set; }
        public decimal? ValorMinimoUtilizacao { get; set; }
        public string Situacao { get; set; }
        public decimal? ValorNumerico { get; set; }
        public decimal? ValorPorcentagem { get; set; }
    }
}
