using Microsoft.EntityFrameworkCore;
using ConsolidadoDiarioService.Models;

namespace ConsolidadoDiarioService.Data
{
    public class ConsolidadoDiarioDbContext : DbContext
    {
        public ConsolidadoDiarioDbContext(DbContextOptions<ConsolidadoDiarioDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConsolidadoDiario> ConsolidadosDiarios { get; set; }
        public DbSet<MovimentoDiario> MovimentosDiarios { get; set; }
    }
}