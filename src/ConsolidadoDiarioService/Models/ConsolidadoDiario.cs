using System;
using System.Collections.Generic;

namespace ConsolidadoDiarioService.Models
{
    public class ConsolidadoDiario
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalCreditos { get; set; }
        public decimal TotalDebitos { get; set; }
        public decimal SaldoDiario { get; set; }
        public List<MovimentoDiario> Movimentos { get; set; }
    }

    public class MovimentoDiario
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeOperacoes { get; set; }
    }
}