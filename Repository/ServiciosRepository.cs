using Api_citasmedicas.Models;
using MySqlConnector;

namespace Api_citasmedicas.Repository
{
    public class ServiciosRepository
    {
        private readonly string _conectionString;

        public ServiciosRepository(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("MySqlConnection");
        }
        public async Task<List<ServiciosModel>> ListarAsync()
        {
            try
            {
                List<ServiciosModel> servicios = new List<ServiciosModel>();

                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand("sp_servicios_listar", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var servicio = new ServiciosModel
                    {
                        IdServicio = reader.GetInt64(0),                        
                        NombreServicio = reader.GetString(1),
                        Descripcion = reader.GetString(2),                      

                    };

                    servicios.Add(servicio);
                }

                return servicios;
            }
            catch (Exception ex)
            {
                // Log obligatorio (archivo, consola, Serilog, etc.)
                throw new Exception("Error en la base de datos => " + ex.Message, ex);
            }
        }
    }

}
