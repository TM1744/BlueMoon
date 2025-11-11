using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.DTO;
using BlueMoon.Entities.Models;

namespace BlueMoon.Services.Interfaces
{
    public interface IVendaService
    {
        Task<Venda> GetByIdAsync(Guid id);
        Task<Venda> AddAsync(Venda venda);
        Task<Venda> AddItensAsync(Venda venda);
        Task<IEnumerable<Venda>> GetAllAsync();
        Task<VendaReadDTO> BuildDTO(Venda venda);
        Task<IEnumerable<VendaReadDTO>> BuildDTOList(IEnumerable<Venda> vendas);
        Task FaturarVenda(Guid id);
        Task CancelarVenda(Guid id);
    }
}