using Api_citasmedicas.Models;
using MySqlConnector;

namespace Api_citasmedicas.Repository
{
    public class EspecialidadRepository
    {
        private readonly string _conectionString;

        public EspecialidadRepository(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("MySqlConnection");
        }
        public async Task<List<EspecialidadModel>> ListarAsync()
        {
            try
            {
                List<EspecialidadModel> especialidades = new List<EspecialidadModel>();

                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand("sp_especialidad_listar", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var especialidad = new EspecialidadModel
                    {
                        IdEspecialidad = reader.GetInt64(0), // Asegúrate de que el índice sea correcto
                        Nombre = reader.GetString(1),
                        Descripcion = reader.GetString(2)
                    };

                    especialidades.Add(especialidad);
                }

                return especialidades;
            }
            catch (Exception ex)
            {
                // Log obligatorio (archivo, consola, Serilog, etc.)
                throw new Exception("Error en la base de datos => " + ex.Message, ex);
            }
        }
    }
}
