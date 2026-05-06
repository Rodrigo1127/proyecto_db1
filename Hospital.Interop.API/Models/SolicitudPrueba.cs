using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Interop.API.Models
{
    public class SolicitudPrueba
    {
        [Key]
        public int SolicitudPruebaId { get; set; }

        [ForeignKey("Paciente")]
        public int PacienteId { get; set; }

        public Paciente? Paciente { get; set; }

        [ForeignKey("TipoPrueba")]
        public int TipoPruebaId { get; set; }

        public TipoPrueba? TipoPrueba { get; set; }

        [ForeignKey("Tecnico")]
        public int? TecnicoId { get; set; }

        public Tecnico? Tecnico { get; set; }

        public DateTime FechaSolicitud { get; set; } = DateTime.Now;

        public DateTime? FechaRealización { get; set; }

        [StringLength(20)]
        public string Estado { get; set; } = "PENDIENTE"; // PENDIENTE, REALIZADO, CANCELADO

        [StringLength(500)]
        public string Observaciones { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoFinal { get; set; }
    }
}
