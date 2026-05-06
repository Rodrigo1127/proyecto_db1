using System.Net.Http.Json;

namespace Hospital.Interop.Web.Services
{
    public class LaboratorioService
    {
        private readonly HttpClient _httpClient;

        public class SolicitudPruebaDTO
        {
            public int Id { get; set; }
            public int PacienteId { get; set; }
            public string TipoPrueba { get; set; }
            public DateTime FechaSolicitud { get; set; }
            public string Estado { get; set; }
            public string Observaciones { get; set; }
        }

        public class ResultadoPruebaDTO
        {
            public int Id { get; set; }
            public int SolicitudId { get; set; }
            public string Resultado { get; set; }
            public DateTime FechaResultado { get; set; }
            public string Interpretacion { get; set; }
        }

        public LaboratorioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<SolicitudPruebaDTO>> ObtenerSolicitudes()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<SolicitudPruebaDTO>>("api/solicitudes-prueba");
                return response ?? new List<SolicitudPruebaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener solicitudes: {ex.Message}");
                return new List<SolicitudPruebaDTO>();
            }
        }

        public async Task<List<ResultadoPruebaDTO>> ObtenerResultados()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ResultadoPruebaDTO>>("api/resultados-prueba");
                return response ?? new List<ResultadoPruebaDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener resultados: {ex.Message}");
                return new List<ResultadoPruebaDTO>();
            }
        }

        public async Task<SolicitudPruebaDTO?> ObtenerSolicitud(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<SolicitudPruebaDTO>($"api/solicitudes-prueba/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener solicitud: {ex.Message}");
                return null;
            }
        }

        public async Task<ResultadoPruebaDTO?> ObtenerResultado(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ResultadoPruebaDTO>($"api/resultados-prueba/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener resultado: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearSolicitud(SolicitudPruebaDTO solicitud)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/solicitudes-prueba", solicitud);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear solicitud: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarSolicitud(int id, SolicitudPruebaDTO solicitud)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/solicitudes-prueba/{id}", solicitud);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar solicitud: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarSolicitud(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/solicitudes-prueba/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar solicitud: {ex.Message}");
                return false;
            }
        }

        public async Task<List<object>> ObtenerSolicitudesPorPaciente()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<object>>("api/solicitudes-prueba/mis/solicitudes-por-paciente");
                return response ?? new List<object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener solicitudes por paciente: {ex.Message}");
                return new List<object>();
            }
        }
    }
}
