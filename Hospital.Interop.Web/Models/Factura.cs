using System;

namespace Hospital.Interop.Web.Models
{
    public class Factura
    {
        public int FacturaId { get; set; }
        public int PacienteId { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaFactura { get; set; }
        public string Estado { get; set; }
        public string Concepto { get; set; }
        public string MetodoPago { get; set; }
    }
}
