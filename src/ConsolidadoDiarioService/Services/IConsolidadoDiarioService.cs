using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConsolidadoDiarioService.Models;

namespace ConsolidadoDiarioService.Services
{
    public interface IConsolidadoDiarioService
    {
        Task<ConsolidadoDiario> GetByDateAsync(DateTime data);
        Task<IEnumerable<ConsolidadoDiario>> GetByPeriodAsync(DateTime inicio, DateTime fim);
        Task<ConsolidadoDiario> ProcessarConsolidadoDiarioAsync(DateTime data);
    }
}