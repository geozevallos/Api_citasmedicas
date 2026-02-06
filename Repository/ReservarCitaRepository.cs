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
        public async Task<int> ReservarCitaAsync(ReservarCitaModel citamodel)
        {
            try
            {
                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand("sp_cita_reserva", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_id_paciente", citamodel.IdPaciente);
                command.Parameters.AddWithValue("p_id_horario", citamodel.IdHorario);
                command.Parameters.AddWithValue("p_fecha_reserva", citamodel.FechaReserva);
                command.Parameters.AddWithValue("p_id_estado", citamodel.IdEstado);
                command.Parameters.AddWithValue("p_observacion", citamodel.Observacion);

                return Convert.ToInt32(await command.ExecuteScalarAsync());

            }
            catch (Exception ex)
            {
                // Aquí puedes manejar el error, como registrar en un log
                throw new Exception("Error al reservar la cita => " + ex.Message, ex);
            }
        }
    }
}

