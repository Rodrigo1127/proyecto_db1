using System;

namespace Hospital.Interop.Web.Models
{
    public class Cita
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public string Departamento { get; set; }
        public string Estado { get; set; }
        public string Observaciones { get; set; }
    }
}
