namespace Api_citasmedicas.Models
{
    public class MedicosModel
    {
        public Int64 IdMedico { get; set; }
        public Int64 IdEspecialidad { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cmp { get; set; }
        public string Email { get; set; }

    }
}
