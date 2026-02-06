namespace Api_citasmedicas.Models
{
    public class ReservarCitaModel
    {
        public Int64 IdPaciente { get; set; }
        public int IdHorario { get; set; }
        public DateTime FechaReserva {  get; set; }
        public int IdEstado { get; set; }
        public string Observacion { get; set; } 
    }
}
    