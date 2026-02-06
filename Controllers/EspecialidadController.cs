using Api_citasmedicas.Models;
using Api_citasmedicas.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly ILogger<EspecialidadController> _logger;
        private readonly EspecialidadRepository _especialidadRepository;

        public EspecialidadController(ILogger<EspecialidadController> logger, EspecialidadRepository especialidadRepository)
        {
            _logger = logger;
            _especialidadRepository = especialidadRepository;
        }

        // =========================
        // 1. LISTAR ESPECIALIDADES
        // =========================
        [HttpGet("listar")]
        public async Task<IActionResult> ListarAsync()
        {
            try
            {
                var especialidades = await _especialidadRepository.ListarAsync();
                return Ok(especialidades);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al listar las especialidades", ex);
                return StatusCode(500, new
                {
                    mensaje = "Error al listar las especialidades => " + ex.Message
                });
            }
        }
    }
}