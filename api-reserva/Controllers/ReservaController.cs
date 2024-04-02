using api_reserva.Models;
using api_reserva.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_reserva.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservaController : ControllerBase
    {
        private readonly ReservaService _reservaService;

        public ReservaController(ReservaService reservaService) =>
            _reservaService = reservaService;
        /// <summary>
        /// Retorna todas as reservas da base de Dados.
        /// </summary>
        [HttpGet]
        public async Task<List<Reserva>> Get() =>
            await _reservaService.GetAsync();

        /// <summary>
        /// Retorna uma reserva específica, com base no ID.
        /// </summary>
        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reserva>> Get(string id)
        {
            var reserva = await _reservaService.GetAsync(id);

            if (reserva is null)
            {
                return NotFound();
            } 
            
            return reserva;
        }

        /// <summary>
        /// Inclui uma nova reserva.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Post(Reserva newReserva)
        {
            await _reservaService.CreateAsync(newReserva);

            return CreatedAtAction(nameof(Get), new {id = newReserva.Id}, newReserva);
        }

        /// <summary>
        /// Atualiza uma reserva específica, com base no ID.
        /// </summary>
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Reserva updatedReserva)
        {
            var reserva = await _reservaService.GetAsync(id);

            if(reserva is null)
            {
                return NotFound();
            }

            updatedReserva.Id = reserva.Id;
            
            await _reservaService.UpdateAsync(id, updatedReserva);

            return NoContent();
        }

        /// <summary>
        /// Exclui uma reserva específica, com base no ID.
        /// </summary>
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reserva = await _reservaService.GetAsync(id);

            if(reserva is null)
            {
                return NotFound();
            }

            await _reservaService.RemoveAsync(id);

            return NoContent();
        }

    }
}