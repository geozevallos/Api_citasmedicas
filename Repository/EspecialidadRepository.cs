using System.Data;
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
            var lista = new List<EspecialidadModel>();

            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_listar_especialidades", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new EspecialidadModel
                {
                    IdEspecialidad = reader.GetInt32("id_especialidad"),
                    Nombre = reader.GetString("nombre")
                });
            }

            return lista;
        }
    }
}
