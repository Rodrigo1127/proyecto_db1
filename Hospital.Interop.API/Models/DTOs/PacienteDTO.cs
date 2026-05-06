namespace Hospital.Interop.API.Models.DTOs
{
    /// <summary>
    /// DTO para exponer datos del paciente sin ID (para consultas de otros departamentos)
    /// </summary>
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

    /// <summary>
    /// DTO para exponer datos del paciente con ID (solo para administrador/propietario)
    /// </summary>
    public class PacienteDTOConId : PacienteDTOSinId
    {
        public int PacienteId { get; set; }
    }
}
