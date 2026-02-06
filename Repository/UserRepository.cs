using Api_citasmedicas.Models;
using MySqlConnector;

namespace Api_citasmedicas.Repository
{
    public class UserRepository
    {
        private readonly string _conectionString;

        public UserRepository(IConfiguration configuration)
        {
            _conectionString = configuration.GetConnectionString("MySqlConnection");
        }
        public async Task<int> CrearAsync(UserModel usermodel)
        {
            try
            {
                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand(@"
                INSERT INTO alumnos (nombre, email)
                VALUES (@nombre, @email);
                SELECT LAST_INSERT_ID();", connection);

                command.Parameters.AddWithValue("@nombre", usermodel.IdUsuario);
                command.Parameters.AddWithValue("@email", usermodel.Clave);

                return Convert.ToInt32(await command.ExecuteScalarAsync());

            }
            catch (Exception ex)
            {
                // Log obligatorio (archivo, consola, Serilog, etc.)
                throw new Exception("Error en la base de datos => " + ex.Message, ex);
            }
        }

        public async Task<List<UserModel>> LoginAsync(string usuario, string password)
        {
            try
            {
                List<UserModel> users = new List<UserModel>();

                using var connection = new MySqlConnection(_conectionString);
                await connection.OpenAsync();

                var command = new MySqlCommand("sp_login_usuario", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("p_username", usuario);
                command.Parameters.AddWithValue("p_password", password);

                using var reader = await command.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var user = new UserModel
                    {
                        IdUsuario = reader.GetInt64(0),
                        IdPersona = reader.GetInt64(1),
                        RolNombre = reader.GetString(2),
                        Activo = reader.GetInt16(3)
                    };

                    users.Add(user);
                }

                return users;
            }
            catch (Exception ex)
            {
                // Log obligatorio (archivo, consola, Serilog, etc.)
                throw new Exception("Error en la base de datos => " + ex.Message, ex);
            }
        }

    }
}
