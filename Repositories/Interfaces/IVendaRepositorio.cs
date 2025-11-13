using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueMoon.Entities.Models;

namespace BlueMoon.Repositories.Interfaces
{
    public interface IVendaRepositorio : IRepositorio<Venda>
    {
        Task AddItensAsync(Venda venda);
        Task Faturar(Venda venda);
        Task Cancelar(Venda venda);
        Task Estornar(Venda venda);
        int GetGreatCodeNumber();
        Task<bool> ValidateIntegrity(Venda venda);
    }
}