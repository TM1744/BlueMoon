using System;
using BlueMoon.Entities.Enuns;

namespace BlueMoon.DTO.CupomDTO
{
    public class CupomCreateDTO
    {
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public DateTime PrazoValidade { get; set; }
        public decimal? ValorMinimoUtilizacao { get; set; }
        public string Situacao { get; set; } = "ATIVO";
        public decimal? ValorNumerico { get; set; }
        public decimal? ValorPorcentagem { get; set; }
    }
}
