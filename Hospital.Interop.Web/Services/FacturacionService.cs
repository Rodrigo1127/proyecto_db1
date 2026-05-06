using System.Net.Http.Json;
using Hospital.Interop.Web.Models;

namespace Hospital.Interop.Web.Services
{
    public class FacturacionService
    {
        private readonly HttpClient _httpClient;

        public FacturacionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Factura>> ObtenerTodasFacturas()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Factura>>("api/facturacion") ?? new List<Factura>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Factura>();
            }
        }

        public async Task<bool> CrearFactura(Factura factura)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/facturacion", factura);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Factura>> ObtenerFacturasPorPaciente(int pacienteId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Factura>>($"api/facturacion/paciente/{pacienteId}") ?? new List<Factura>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Factura>();
            }
        }
    }
}
