using Microsoft.AspNetCore.Mvc;
using LancamentosService.Models;
using LancamentosService.Services;
using System.Threading.Tasks;

namespace LancamentosService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LancamentosController : ControllerBase
    {
        private readonly ILancamentoService _lancamentoService;

        public LancamentosController(ILancamentoService lancamentoService)
        {
            _lancamentoService = lancamentoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var lancamentos = await _lancamentoService.GetAllAsync();
            return Ok(lancamentos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lancamento = await _lancamentoService.GetByIdAsync(id);
            if (lancamento == null)
                return NotFound();
            return Ok(lancamento);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Lancamento lancamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _lancamentoService.CreateAsync(lancamento);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Lancamento lancamento)
        {
            if (id != lancamento.Id)
                return BadRequest();

            var updated = await _lancamentoService.UpdateAsync(lancamento);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _lancamentoService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}