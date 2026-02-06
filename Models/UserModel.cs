namespace Api_citasmedicas.Models
{
    public class UserModel: ModelBase
    {
        public Int64 IdUsuario { get; set; }
        public Int64 IdPersona { get; set; }
        public String RolNombre { get; set; }
        public string Usuario { get; set; }
        public string Language {  get; set; }
        public string Clave { get; set; }
        public DateTime UltimoAcceso { get; set; }

    }

}
