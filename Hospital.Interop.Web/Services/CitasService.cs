using System.Net.Http.Json;

namespace Hospital.Interop.Web.Services
{
    public class CitasService
    {
        private readonly HttpClient _httpClient;

        public class CitaDTO
        {
            public int Id { get; set; }
            public int PacienteId { get; set; }
            public DateTime Fecha { get; set; }
            public string Hora { get; set; }
            public string Departamento { get; set; }
            public string Estado { get; set; }
            public string Observaciones { get; set; }
        }

        public CitasService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CitaDTO>> ObtenerCitas()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<CitaDTO>>("api/citas");
                return response ?? new List<CitaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener citas: {ex.Message}");
                return new List<CitaDTO>();
            }
        }

        public async Task<CitaDTO> ObtenerCita(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<CitaDTO>($"api/citas/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener cita: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearCita(CitaDTO cita)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/citas", cita);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cita: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarCita(int id, CitaDTO cita)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/citas/{id}", cita);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar cita: {ex.Message}");
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
                Console.WriteLine($"Error al eliminar cita: {ex.Message}");
                return false;
            }
        }
    }
}
