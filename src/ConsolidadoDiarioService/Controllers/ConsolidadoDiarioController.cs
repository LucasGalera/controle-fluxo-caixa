using Microsoft.AspNetCore.Mvc;
using ConsolidadoDiarioService.Models;
using ConsolidadoDiarioService.Services;
using System;
using System.Threading.Tasks;

namespace ConsolidadoDiarioService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsolidadoDiarioController : ControllerBase
    {
        private readonly IConsolidadoDiarioService _consolidadoDiarioService;

        public ConsolidadoDiarioController(IConsolidadoDiarioService consolidadoDiarioService)
        {
            _consolidadoDiarioService = consolidadoDiarioService;
        }

        [HttpGet("{data}")]
        public async Task<IActionResult> GetByDate(DateTime data)
        {
            var consolidado = await _consolidadoDiarioService.GetByDateAsync(data);
            if (consolidado == null)
                return NotFound();
            return Ok(consolidado);
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> GetByPeriod([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            var consolidados = await _consolidadoDiarioService.GetByPeriodAsync(inicio, fim);
            return Ok(consolidados);
        }

        [HttpPost("processar/{data}")]
        public async Task<IActionResult> ProcessarConsolidado(DateTime data)
        {
            var consolidado = await _consolidadoDiarioService.ProcessarConsolidadoDiarioAsync(data);
            return Ok(consolidado);
        }
    }
}