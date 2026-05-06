namespace Hospital.Interop.Web.Models
{
    public class PacienteDTOSinId
    {
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime? FechaNacimiento { get; set; }
        public string? Genero { get; set; }
    }

    public class PacienteDTOConId : PacienteDTOSinId
    {
        public int PacienteId { get; set; }
    }
}
