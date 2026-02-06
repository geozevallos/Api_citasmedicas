using Microsoft.VisualBasic;

namespace Api_citasmedicas.Models
{
    public class ModelBase
    {
        public Int16 Activo { get; set; }
        public string Eliminado { get; set; }
        public Int64 UsuarioCrea { get; set; } 
        public DateTime FechaCrea { get; set; }
        public  Int64 UsuarioModifica {  get; set; }
        public DateTime FechaModifica { get; set; }
        public string IpPublicaCrea { get; set; }
        public string IpPublicaModifica { get; set; }
        public string IpLocalCrear { get; set; }
        public string IpLocalModifica { get; set ; }
        public string NavegadorModifica { get; set; }

        public string HostaNameCrea { get; set; }
        public string HostaNameModifica { get; set; }





    }
}
