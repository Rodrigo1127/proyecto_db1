namespace Hospital.Interop.API.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public int PacienteId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaFactura { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Concepto { get; set; } = string.Empty;

        public Paciente? Paciente { get; set; }
    }
}
