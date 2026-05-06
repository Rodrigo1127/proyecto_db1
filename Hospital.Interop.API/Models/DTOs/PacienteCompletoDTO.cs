namespace Hospital.Interop.API.Models.DTOs
{
    /// <summary>
    /// DTO para exponer respuesta completa del paciente (sin IDs para terceros)
    /// </summary>
    public class PacienteCompletoDTO
    {
        public PacienteDTOSinId Paciente { get; set; } = new();
        public List<object> Examenes { get; set; } = new();
        public List<object> Citas { get; set; } = new();
        public List<object> Facturas { get; set; } = new();
    }

    /// <summary>
    /// DTO para exponer respuesta completa del paciente (con IDs - solo para admin)
    /// </summary>
    public class PacienteCompletoConIdDTO
    {
        public PacienteDTOConId Paciente { get; set; } = new();
        public List<object> Examenes { get; set; } = new();
        public List<object> Citas { get; set; } = new();
        public List<object> Facturas { get; set; } = new();
    }
}
