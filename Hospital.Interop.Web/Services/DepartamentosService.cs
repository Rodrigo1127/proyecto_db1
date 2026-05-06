using System.Net.Http.Json;

namespace Hospital.Interop.Web.Services
{
    public class DepartamentosService
    {
        private readonly HttpClient _httpClient;

        public class DepartamentoDTO
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int PacientesCount { get; set; }
        }

        public DepartamentosService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<DepartamentoDTO>> ObtenerDepartamentos()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<DepartamentoDTO>>("api/departamentos");
                return response ?? new List<DepartamentoDTO>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener departamentos: {ex.Message}");
                return new List<DepartamentoDTO>();
            }
        }

        public async Task<DepartamentoDTO?> ObtenerDepartamento(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<DepartamentoDTO>($"api/departamentos/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener departamento: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CrearDepartamento(DepartamentoDTO departamento)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/departamentos", departamento);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear departamento: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarDepartamento(int id, DepartamentoDTO departamento)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/departamentos/{id}", departamento);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar departamento: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarDepartamento(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/departamentos/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar departamento: {ex.Message}");
                return false;
            }
        }
    }
}
