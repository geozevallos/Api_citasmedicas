using Api_citasmedicas.Models;
using MySqlConnector;
using System.Data;

namespace Api_citasmedicas.Repository
{
    public class ReservarCitaRepository
    {
        private readonly string _conectionString;

        public ReservarCitaRepository(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("MySqlConnection");
        }

        public async Task ReservarCitaAsync(ReservarCitaModel request)
        {
            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_reservar_cita", connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            command.Parameters.AddWithValue("p_id_paciente", request.IdPaciente);
            command.Parameters.AddWithValue("p_id_horario", request.IdHorario);
            command.Parameters.AddWithValue("p_observacion", request.Observacion);

            try
            {
                await command.ExecuteNonQueryAsync();
            }
            catch (MySqlException ex)
            {
                if (ex.Number == 1644)
                {
                    throw new InvalidOperationException("No hay cupos disponibles");
                }
                throw;
            }
        }
    }
}

