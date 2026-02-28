namespace Api_citasmedicas.Models
{
    public class UsuarioResponseModel
    {
        public int IdUsuario { get; set; }
        public string Username { get; set; }
        public int IdRol { get; set; }
        public string RolNombre { get; set; }
        public bool Activo { get; set; }
    }
}
