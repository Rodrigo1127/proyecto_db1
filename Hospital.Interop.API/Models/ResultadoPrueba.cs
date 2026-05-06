using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Interop.API.Models
{
    public class ResultadoPrueba
    {
        [Key]
        public int ResultadoPruebaId { get; set; }

        [ForeignKey("SolicitudPrueba")]
        public int SolicitudPruebaId { get; set; }

        public SolicitudPrueba? SolicitudPrueba { get; set; }

        [StringLength(200)]
        public string ValorResultado { get; set; } = string.Empty;

        [StringLength(50)]
        public string ValorMinimo { get; set; } = string.Empty;

        [StringLength(50)]
        public string ValorMaximo { get; set; } = string.Empty;

        [StringLength(50)]
        public string UnidadMedida { get; set; } = string.Empty;

        [StringLength(20)]
        public string Estado { get; set; } = "NORMAL"; // NORMAL, ANORMAL, CRÍTICO

        public DateTime FechaResultado { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string AnalystaNombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Observaciones { get; set; } = string.Empty;
    }
}
