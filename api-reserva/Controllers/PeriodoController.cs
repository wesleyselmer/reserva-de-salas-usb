using api_reserva.Models;
using api_reserva.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_reserva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodoController : ControllerBase
    {
        private readonly PeriodoService _periodoService;

        public PeriodoController(PeriodoService periodoService) =>
            _periodoService = periodoService;
        /// <summary>
        /// Retorna todas os períodos da base de Dados.
        /// </summary>
        [HttpGet]
        public async Task<List<Periodo>> Get() =>
            await _periodoService.GetAsync();

        /// <summary>
        /// Retorna um período específico, com base no ID.
        /// </summary>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Periodo>> Get(string id)
        {
            var periodo = await _periodoService.GetAsync(id);

            if (periodo is null)
            {
                return NotFound();
            } 
            
            return periodo;
        }

        /// <summary>
        /// Inclui um novo período.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Periodo newPeriodo)
        {
            await _periodoService.CreateAsync(newPeriodo);

            return CreatedAtAction(nameof(Get), new {id = newPeriodo.Id}, newPeriodo);
        }

        /// <summary>
        /// Atualiza um período específico, com base no ID.
        /// </summary>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Periodo updatedPeriodo)
        {
            var periodo = await _periodoService.GetAsync(id);

            if(periodo is null)
            {
                return NotFound();
            }

            updatedPeriodo.Id = periodo.Id;
            
            await _periodoService.UpdateAsync(id, updatedPeriodo);

            return NoContent();
        }

        /// <summary>
        /// Exclui um período específico, com base no ID.
        /// </summary>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reserva = await _periodoService.GetAsync(id);

            if(reserva is null)
            {
                return NotFound();
            }

            await _periodoService.RemoveAsync(id);

            return NoContent();
        }

    }
}