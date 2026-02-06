using Microsoft.AspNetCore.Mvc;
using MySqlConnector;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly string _connectionString;

        public HealthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MySqlConnection");
        }

        // =========================
        // HEALTH CHECK BD
        // =========================
        [HttpGet("db")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                using var connection = new MySqlConnection(_connectionString);
                await connection.OpenAsync();

                return Ok(new
                {
                    estado = "OK",
                    mensaje = "API conectada correctamente a la base de datos",
                    database = connection.Database,
                    servidor = connection.DataSource
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    estado = "ERROR",
                    mensaje = ex.Message
                });
            }
        }
    }
}