using Api_citasmedicas.Models;
using Api_citasmedicas.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Api_citasmedicas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly UserRepository _userRepository;

        // Simula base de datos
        // private static List<UserModel> users = new List<UserModel>();
        private static int nextId = 1;

        public UserController(ILogger<UserController> logger, UserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        // =========================
        // REGISTRAR USUARIO
        // =========================
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserModel model)
        {
            try
            {

                // aca se crea una validacion que contenga User/Password
                if (string.IsNullOrEmpty(model.Usuario) || string.IsNullOrEmpty(model.Clave))
                    return BadRequest("Usuario y contraseña son obligatorios");

                int id = await _userRepository.CrearAsync(model);


                return Ok(new
                {
                    mensaje = "Usuario registrado correctamente",
                    idUsuario = id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al registrar el usuario", ex);
                return StatusCode(500, new
                {
                    mensaje = "Error al registrar el usuario => " + ex.Message
                });
            }
        }


        // =========================
        // 1. Login SERVICIOS
        // =========================
        [HttpGet("Login")]
        public async Task<IActionResult> LoginAsync([FromQuery]string usuario, [FromQuery] string password)
        {
            try
            {
                var users = await _userRepository.LoginAsync(usuario,password);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al listar usuarios", ex);
                return StatusCode(500, new
                {
                    mensaje = "Error al listar las especialidades => " + ex.Message
                });
            }
        }
        /*
        // =========================
        // 2. LOGIN / AUTENTICACIÓN
        // =========================
        [HttpPost("login")]
        public IActionResult Login(UserModel model)
        {
            var user = users.FirstOrDefault(u =>
            u.Usuario == model.Usuario && u.Clave == model.Clave);


            if (user == null)
                return Unauthorized("Credenciales incorrectas");


            return Ok(user);
        }


        // =========================
        // 3. ACTUALIZAR USUARIO
        // =========================
        [HttpPut("{id}")]
        public IActionResult Update(int id, UserModel model)
        {
            var user = users.FirstOrDefault(u => u.IdUsuario == id);


            if (user == null)
                return NotFound("Usuario no encontrado");


            user.Usuario = model.Usuario;


            return Ok("Usuario actualizado");
        }


        // =========================
        // 4. ELIMINAR USUARIO
        // =========================
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(u => u.IdUsuario == id);


            if (user == null)
                return NotFound("Usuario no encontrado");


            users.Remove(user);


            return Ok("Usuario eliminado");
        }


        // =========================
        // 5. CAMBIAR CONTRASEÑA
        // =========================
        [HttpPatch("change-password")]
        public IActionResult ChangePassword(
        int id,
        string oldPassword,
        string newPassword)
        {
            var user = users.FirstOrDefault(u => u.IdUsuario == id);


            if (user == null)
                return NotFound("Usuario no encontrado");


            if (user.Clave != oldPassword)
                return BadRequest("Contraseña actual incorrecta");


            user.Clave = newPassword;


            return Ok("Contraseña actualizada");
        }
        */
    }
}
