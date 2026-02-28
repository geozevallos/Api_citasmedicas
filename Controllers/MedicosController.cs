using Api_citasmedicas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicosController : ControllerBase
    {
        private readonly ILogger<MedicosController> _logger;
        private readonly MedicosRepository _medicosRepository;

        public MedicosController(ILogger<MedicosController> logger, MedicosRepository medicosRepository)
        {
            _logger = logger;
            _medicosRepository = medicosRepository;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> ListarAsync()
        {
            try
            {
                var medicos = await _medicosRepository.ListarAsync();
                return Ok(medicos);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al listar los medicos", ex);
                return StatusCode(500, new
                {
                    mensaje = "Error al listar las medicos => " + ex.Message
                });
            }
        }

        [HttpGet("especialidad/{idEspecialidad}")]
        public async Task<IActionResult> ListarPorEspecialidad(int idEspecialidad)
        {
            try
            {
                var lista = await _medicosRepository.ListarPorEspecialidadAsync(idEspecialidad);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar médicos");
                return StatusCode(500, new
                {
                    mensaje = "Error interno del servidor"
                });
            }
        }

        [HttpGet("horario")]
        public async Task<IActionResult> Listar(
        [FromQuery] int medico,
        [FromQuery] DateTime fecha)
        {
            try
            {
                var lista = await _medicosRepository.ListarHorarioAsync(medico, fecha);
                return Ok(lista);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al listar horarios");
                return StatusCode(500, new
                {
                    mensaje = "Error interno del servidor"
                });
            }
        }
    }
}
