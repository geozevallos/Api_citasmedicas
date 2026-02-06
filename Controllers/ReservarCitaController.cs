using Api_citasmedicas.Models;
using Api_citasmedicas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservarCitaController : ControllerBase
    {
        private readonly ILogger<ReservarCitaController> _logger;
        private readonly ReservarCitaRepository _reservarCitaRepository;

        public ReservarCitaController(ILogger<ReservarCitaController> logger, ReservarCitaRepository reservarCitaRepository)
        {
            _logger = logger;
            _reservarCitaRepository = reservarCitaRepository;
        }


        // =========================
        // REGISTRAR CITA
        // =========================
        [HttpPost]
        public async Task<IActionResult> ReservarCita([FromBody] ReservarCitaModel citamodel)
        {
            if (citamodel == null)
            {
                return BadRequest("Los datos de la cita son requeridos.");
            }

            try
            {
                var resultado = await _reservarCitaRepository.ReservarCitaAsync(citamodel);
                return CreatedAtAction(nameof(ReservarCita), new { id = resultado }, citamodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reservar la cita.");
                return StatusCode(500, "Error interno del servidor.");  
            }
        }
    }
}
