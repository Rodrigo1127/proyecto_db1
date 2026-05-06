using System.ComponentModel.DataAnnotations;

namespace Hospital.Interop.Web.Models
{
    public class Paciente
    {
        public int PacienteId { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;

        [StringLength(20)]
        public string Documento { get; set; } = string.Empty;

        [StringLength(15)]
        public string Telefono { get; set; } = string.Empty;

        [StringLength(300)]
        public string Direccion { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "El email no es válido")]
        [StringLength(100)]
        public string Email { get; set; } = string.Empty;

        public DateTime? FechaNacimiento { get; set; }

        [StringLength(1)]
        public string? Genero { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public bool Activo { get; set; } = true;
    }
}
