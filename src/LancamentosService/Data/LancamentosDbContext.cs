using Microsoft.EntityFrameworkCore;
using LancamentosService.Models;

namespace LancamentosService.Data
{
    public class LancamentosDbContext : DbContext
    {
        public LancamentosDbContext(DbContextOptions<LancamentosDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lancamento> Lancamentos { get; set; }
    }
}