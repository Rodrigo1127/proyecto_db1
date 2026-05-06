using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospital.Interop.API.Models
{
	public class Tecnico
	{
		[Key]
		public int TecnicoId { get; set; }

		[Required]
		[StringLength(200)]
		public string Nombre { get; set; } = string.Empty;

		[StringLength(20)]
		public string Documento { get; set; } = string.Empty;

		[StringLength(15)]
		public string Telefono { get; set; } = string.Empty;

		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; } = string.Empty;

		[ForeignKey(nameof(Departamento))]
		public int DepartamentoId { get; set; }

		public Departamento? Departamento { get; set; }

		[StringLength(50)]
		public string Cargo { get; set; } = string.Empty;

		public bool Activo { get; set; } = true;

		public DateTime FechaContratacion { get; set; } = DateTime.UtcNow;
	}
}