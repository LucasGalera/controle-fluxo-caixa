using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LancamentosService.Data;
using LancamentosService.Models;
using StackExchange.Redis;

namespace LancamentosService.Services
{
    public class LancamentoService : ILancamentoService
    {
        private readonly LancamentosDbContext _context;
        private readonly IConnectionMultiplexer _redis;

        public LancamentoService(LancamentosDbContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        public async Task<IEnumerable<Lancamento>> GetAllAsync()
        {
            return await _context.Lancamentos.ToListAsync();
        }

        public async Task<Lancamento> GetByIdAsync(int id)
        {
            var db = _redis.GetDatabase();
            var cachedLancamento = await db.StringGetAsync($"lancamento:{id}");
            
            if (cachedLancamento.HasValue)
                return System.Text.Json.JsonSerializer.Deserialize<Lancamento>(cachedLancamento);

            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento != null)
            {
                await db.StringSetAsync(
                    $"lancamento:{id}",
                    System.Text.Json.JsonSerializer.Serialize(lancamento),
                    TimeSpan.FromMinutes(10)
                );
            }

            return lancamento;
        }

        public async Task<Lancamento> CreateAsync(Lancamento lancamento)
        {
            _context.Lancamentos.Add(lancamento);
            await _context.SaveChangesAsync();
            return lancamento;
        }

        public async Task<Lancamento> UpdateAsync(Lancamento lancamento)
        {
            _context.Entry(lancamento).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                var db = _redis.GetDatabase();
                await db.KeyDeleteAsync($"lancamento:{lancamento.Id}");
                return lancamento;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await LancamentoExists(lancamento.Id))
                    return null;
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lancamento = await _context.Lancamentos.FindAsync(id);
            if (lancamento == null)
                return false;

            _context.Lancamentos.Remove(lancamento);
            await _context.SaveChangesAsync();

            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync($"lancamento:{id}");

            return true;
        }

        private async Task<bool> LancamentoExists(int id)
        {
            return await _context.Lancamentos.AnyAsync(e => e.Id == id);
        }
    }
}