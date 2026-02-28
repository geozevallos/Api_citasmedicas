using Api_citasmedicas.Models;
using MySqlConnector;
using System.Data;

namespace Api_citasmedicas.Repository
{
    public class MedicosRepository
    {
        private readonly string _conectionString;

        public MedicosRepository(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("MySqlConnection");
        }
        public async Task<List<MedicosModel>> ListarAsync()
        {
            try
            {
                List<MedicosModel> medicos = new List<MedicosModel>();

                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand("sp_medicos_listar", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var medico = new MedicosModel
                    {
                        IdMedico = reader.GetInt64(0),
                        IdEspecialidad = reader.GetInt64(1),
                        Nombres = reader.GetString(2),
                        Apellidos = reader.GetString(3),
                        Cmp = reader.GetString(4),
                        Email = reader.GetString(5)

                    };

                    medicos.Add(medico);
                }

                return medicos;
            }
            catch (Exception ex)
            {
                // Log obligatorio (archivo, consola, Serilog, etc.)
                throw new Exception("Error en la base de datos => " + ex.Message, ex);
            }
        }

        public async Task<List<MedicoResponse>> ListarPorEspecialidadAsync(int idEspecialidad)
        {
            var lista = new List<MedicoResponse>();

            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_listar_medicos_por_especialidad", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("p_id_especialidad", MySqlDbType.Int32)
                   .Value = idEspecialidad;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new MedicoResponse
                {
                    IdMedico = reader.GetInt32("id_medico"),
                    Nombres = reader.GetString("nombres"),
                    Apellidos = reader.GetString("apellidos"),
                    Cmp = reader.GetString("cmp")
                });
            }

            return lista;
        }


        public async Task<List<HorarioMedicoResponse>> ListarHorarioAsync(int idMedico, DateTime fecha)
        {
            var lista = new List<HorarioMedicoResponse>();

            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_listar_horarios_medico", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("p_id_medico", MySqlDbType.Int32).Value = idMedico;
            command.Parameters.Add("p_fecha", MySqlDbType.Date).Value = fecha.Date;

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new HorarioMedicoResponse
                {
                    IdHorario = reader.GetInt32("id_horario"),
                    Fecha = reader.GetDateTime("fecha"),
                    HoraInicio = reader.GetTimeSpan("hora_inicio"),
                    HoraFin = reader.GetTimeSpan("hora_fin"),
                    CuposDisponibles = reader.GetInt32("cupos_disponibles"),
                    Servicio = reader.GetString("servicio")
                });
            }

            return lista;
        }
    }

}


