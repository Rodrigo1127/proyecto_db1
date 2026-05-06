namespace Hospital.Interop.API.Models
{
    public class PacienteCompleto
    {
        public Paciente Paciente { get; set; } = new();
        public List<Examen> Examenes { get; set; } = new();
        public List<Cita> Citas { get; set; } = new();
        public List<Factura> Facturas { get; set; } = new();
    }
}
