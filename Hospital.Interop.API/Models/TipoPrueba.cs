using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Interop.API.Models
{
    public class TipoPrueba
    {
        [Key]
        public int TipoPruebaId { get; set; }

        [Required]
        [StringLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(500)]
        public string Descripcion { get; set; } = string.Empty;

        [ForeignKey("Departamento")]
        public int DepartamentoId { get; set; }

        public Departamento? Departamento { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal CostoReferencia { get; set; }

        [StringLength(50)]
        public string UnidadMedida { get; set; } = string.Empty;

        public bool Activo { get; set; } = true;
    }
}
