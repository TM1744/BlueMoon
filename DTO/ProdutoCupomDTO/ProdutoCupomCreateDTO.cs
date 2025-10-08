using System;
using BlueMoon.Models.Enuns;

namespace BlueMoon.DTO.ProdutoCupomDTO
{
    public class ProdutoCupomCreateDTO
    {
        public Guid IdProduto { get; set; }
        public Guid IdCupom { get; set; }
    }
}
