using Api_citasmedicas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiciosController : ControllerBase
    {
        private readonly ILogger<ServiciosController> _logger;
        private readonly ServiciosRepository _serviciosRepository;

        public ServiciosController(ILogger<ServiciosController> logger, ServiciosRepository serviciosRepository)
        {
            _logger = logger;
            _serviciosRepository = serviciosRepository;
        }

        // =========================
        // 1. LISTAR SERVICIOS
        // =========================
        [HttpGet("listar")]
        public async Task<IActionResult> ListarAsync()
        {
            try
            {
                var servicios = await _serviciosRepository.ListarAsync();
                return Ok(servicios);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al listar servicios", ex);
                return StatusCode(500, new
                {
                    mensaje = "Error al listar las especialidades => " + ex.Message
                });
            }
        }
    }
}
