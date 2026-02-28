namespace Api_citasmedicas.Models
{
    public class HorarioMedicoResponse
    {
        public int IdHorario { get; set; }
        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
        public int CuposDisponibles { get; set; }
        public string Servicio { get; set; }
    }
}
