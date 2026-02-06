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
    }

}


