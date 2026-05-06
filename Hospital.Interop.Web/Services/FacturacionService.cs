using System.Net.Http.Json;

namespace Hospital.Interop.Web.Services
{
    public class FacturacionService
    {
        private readonly HttpClient _httpClient;

        public class FacturaDTO
        {
            public int Id { get; set; }
            public int PacienteId { get; set; }
            public DateTime Fecha { get; set; }
            public decimal Monto { get; set; }
            public string Estado { get; set; }
            public string Concepto { get; set; }
        }

        public FacturacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<FacturaDTO>> ObtenerFacturas()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<FacturaDTO>>("api/facturacion");
                return response ?? new List<FacturaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener facturas: {ex.Message}");
                return new List<FacturaDTO>();
            }
        }

        public async Task<FacturaDTO?> ObtenerFactura(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<FacturaDTO>($"api/facturacion/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener factura: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearFactura(FacturaDTO factura)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/facturacion", factura);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear factura: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarFactura(int id, FacturaDTO factura)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/facturacion/{id}", factura);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar factura: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarFactura(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/facturacion/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar factura: {ex.Message}");
                return false;
            }
        }

        public async Task<List<FacturaDTO>> ObtenerFacturasPorPaciente(int pacienteId)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<FacturaDTO>>($"api/facturacion/paciente/{pacienteId}");
                return response ?? new List<FacturaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener facturas por paciente: {ex.Message}");
                return new List<FacturaDTO>();
            }
        }

        public async Task<decimal> ObtenerTotalFacturado()
        {
            try
            {
                var facturas = await ObtenerFacturas();
                return facturas.Sum(f => f.Monto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al calcular total facturado: {ex.Message}");
                return 0;
            }
        }
    }
}
