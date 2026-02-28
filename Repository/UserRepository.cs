using System.Data;
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

        public async Task<LoginResponseModel?> LoginAsync(string usuario, string password)
        {
            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_login_usuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("p_username", MySqlDbType.VarChar).Value = usuario;
            command.Parameters.Add("p_password", MySqlDbType.VarChar).Value = password;

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new LoginResponseModel
                {
                    IdUsuario = reader.GetInt32("id_usuario"),
                    Username = reader.GetString("username"),
                    IdRol = reader.GetInt32("id_rol"),
                    RolNombre = reader.GetString("rol_nombre"),
                    Activo = reader.GetBoolean("activo")
                };
            }

            return null;
        }


        public async Task<PacienteResponseModel?> ObtenerPorUsuarioAsync(int idUsuario)
        {
            using var connection = new MySqlConnection(_conectionString);
            await connection.OpenAsync();

            using var command = new MySqlCommand("sp_obtener_paciente_por_usuario", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.Add("p_id_usuario", MySqlDbType.Int32)
                   .Value = idUsuario;

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new PacienteResponseModel
                {
                    IdPaciente = reader.GetInt32("id_paciente"),
                    IdUsuario = reader.GetInt32("id_usuario"),
                    Nombres = reader.GetString("nombres"),
                    Apellidos = reader.GetString("apellidos"),
                    Documento = reader["documento"]?.ToString(),
                    FechaNacimiento = reader["fecha_nacimiento"] as DateTime?,
                    Sexo = reader["sexo"]?.ToString(),
                    Celular = reader["celular"]?.ToString(),
                    Email = reader["email"]?.ToString()
                };
            }

            return null;
        }

    }
}
