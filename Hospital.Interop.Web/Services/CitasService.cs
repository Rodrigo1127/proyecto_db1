using System.Net.Http.Json;
using Hospital.Interop.Web.Models;

namespace Hospital.Interop.Web.Services
{
    public class CitasService
    {
        private readonly HttpClient _httpClient;

        public CitasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cita>> ObtenerTodasCitas()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<List<Cita>>("api/citas") ?? new List<Cita>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new List<Cita>();
            }
        }

        public async Task<Cita?> ObtenerCitaPorId(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Cita>($"api/citas/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearCita(Cita cita)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/citas", cita);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarCita(int id, Cita cita)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/citas/{id}", cita);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarCita(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/citas/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
