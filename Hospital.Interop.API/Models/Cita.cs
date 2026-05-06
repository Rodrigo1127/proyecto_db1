using System.ComponentModel.DataAnnotations;

namespace Hospital.Interop.API.Models
{
    public class Cita
    {
        [Key]
        public int Id { get; set; }

        public int PacienteId { get; set; }

        public DateTime Fecha { get; set; }

        [StringLength(10)]
        public string Hora { get; set; } = string.Empty;

        [StringLength(100)]
        public string Departamento { get; set; } = string.Empty;

        [StringLength(50)]
        public string Estado { get; set; } = "Pendiente";

        [StringLength(500)]
        public string Observaciones { get; set; } = string.Empty;
    }
}