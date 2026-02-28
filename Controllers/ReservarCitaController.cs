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


        [HttpPost("reservar")]
        public async Task<IActionResult> Reservar([FromBody] ReservarCitaModel request)
        {
            try
            {
                await _reservarCitaRepository.ReservarCitaAsync(request);
                return Ok(new { mensaje = "Cita reservada correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al reservar cita");
                return StatusCode(500, new { mensaje = "Error interno del servidor" });
            }
        }
    }
}
