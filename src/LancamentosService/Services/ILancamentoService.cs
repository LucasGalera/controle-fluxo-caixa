using System.Collections.Generic;
using System.Threading.Tasks;
using LancamentosService.Models;

namespace LancamentosService.Services
{
    public interface ILancamentoService
    {
        Task<IEnumerable<Lancamento>> GetAllAsync();
        Task<Lancamento> GetByIdAsync(int id);
        Task<Lancamento> CreateAsync(Lancamento lancamento);
        Task<Lancamento> UpdateAsync(Lancamento lancamento);
        Task<bool> DeleteAsync(int id);
    }
}