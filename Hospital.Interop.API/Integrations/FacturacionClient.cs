using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    public class FacturacionClient
    {
        public async Task<List<Factura>> GetFacturas(int id)
        {
            await Task.Delay(200);

            return new List<Factura>
            {
                new Factura { FacturaId = 1, Monto = 150, PacienteId = id, Estado = "Pagada", Concepto = "Consulta", FechaFactura = DateTime.UtcNow },
                new Factura { FacturaId = 2, Monto = 300, PacienteId = id, Estado = "Pendiente", Concepto = "Laboratorio", FechaFactura = DateTime.UtcNow }
            };
        }
    }
}
