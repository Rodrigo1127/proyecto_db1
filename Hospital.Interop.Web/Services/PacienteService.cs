using Hospital.Interop.Web.Models;
using System.Net.Http.Json;

namespace Hospital.Interop.Web.Services
{
    public class PacienteService
    {
        private readonly HttpClient _httpClient;

        public PacienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Obtiene todos los pacientes
        /// </summary>
        public async Task<List<Paciente>> ObtenerTodosPacientes()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/pacientes");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener pacientes. Código: {response.StatusCode}. Detalle: {error}");
                    return new List<Paciente>();
                }

                var pacientes = await response.Content.ReadFromJsonAsync<List<Paciente>>();
                return pacientes ?? new List<Paciente>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener pacientes: {ex.Message}");
                return new List<Paciente>();
            }
        }

        /// <summary>
        /// Obtiene un paciente por ID
        /// </summary>
        public async Task<Paciente?> ObtenerPaciente(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/pacientes/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener paciente. Código: {response.StatusCode}. Detalle: {error}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<Paciente>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener paciente: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Crea un nuevo paciente
        /// </summary>
        public async Task<bool> CrearPaciente(Paciente paciente)
        {
            try
            {
                paciente.PacienteId = 0;
                paciente.FechaRegistro = DateTime.UtcNow;
                paciente.Activo = true;

                if (paciente.FechaNacimiento.HasValue)
                {
                    paciente.FechaNacimiento = DateTime.SpecifyKind(
                        paciente.FechaNacimiento.Value,
                        DateTimeKind.Utc
                    );
                }

                var response = await _httpClient.PostAsJsonAsync("api/pacientes", paciente);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al crear paciente. Código: {response.StatusCode}. Detalle: {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear paciente: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Actualiza un paciente existente
        /// </summary>
        public async Task<bool> ActualizarPaciente(int id, Paciente paciente)
        {
            try
            {
                paciente.PacienteId = id;

                if (paciente.FechaNacimiento.HasValue)
                {
                    paciente.FechaNacimiento = DateTime.SpecifyKind(
                        paciente.FechaNacimiento.Value,
                        DateTimeKind.Utc
                    );
                }

                var response = await _httpClient.PutAsJsonAsync($"api/pacientes/{id}", paciente);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al actualizar paciente. Código: {response.StatusCode}. Detalle: {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar paciente: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Elimina un paciente por ID
        /// </summary>
        public async Task<bool> EliminarPaciente(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/pacientes/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al eliminar paciente. Código: {response.StatusCode}. Detalle: {error}");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar paciente: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtiene datos del paciente SIN ID para consultas públicas
        /// </summary>
        public async Task<PacienteDTOSinId?> ObtenerPacienteSinId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/gateway/paciente/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener paciente sin ID. Código: {response.StatusCode}. Detalle: {error}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<PacienteDTOSinId>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener paciente: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene datos del paciente CON ID, requiere admin
        /// </summary>
        public async Task<PacienteDTOConId?> ObtenerPacienteConId(int id, string adminKey = "admin-secret-key")
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, $"api/gateway/paciente-admin/{id}");
                request.Headers.Add("X-Admin-Key", adminKey);

                var response = await _httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener paciente admin. Código: {response.StatusCode}. Detalle: {error}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<PacienteDTOConId>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener paciente admin: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtiene todos los datos del paciente, incluyendo departamentos
        /// </summary>
        public async Task<object?> ObtenerPacienteCompleto(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/gateway/paciente-completo/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al obtener paciente completo. Código: {response.StatusCode}. Detalle: {error}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener paciente completo: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Busca pacientes por nombre o documento
        /// </summary>
        public async Task<List<Paciente>> BuscarPacientes(string? nombre = null, string? documento = null)
        {
            try
            {
                var query = "api/pacientes/buscar?";
                var parametros = new List<string>();

                if (!string.IsNullOrWhiteSpace(nombre))
                    parametros.Add($"nombre={Uri.EscapeDataString(nombre)}");

                if (!string.IsNullOrWhiteSpace(documento))
                    parametros.Add($"documento={Uri.EscapeDataString(documento)}");

                if (parametros.Count > 0)
                    query += string.Join("&", parametros);
                else
                    query = "api/pacientes";

                var response = await _httpClient.GetAsync(query);

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al buscar pacientes. Código: {response.StatusCode}. Detalle: {error}");
                    return new List<Paciente>();
                }

                var pacientes = await response.Content.ReadFromJsonAsync<List<Paciente>>();
                return pacientes ?? new List<Paciente>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al buscar pacientes: {ex.Message}");
                return new List<Paciente>();
            }
        }

        /// <summary>
        /// Verifica el estado de salud del gateway
        /// </summary>
        public async Task<object?> VerificarSalud()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/gateway/health");

                if (!response.IsSuccessStatusCode)
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error al verificar salud. Código: {response.StatusCode}. Detalle: {error}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<object>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al verificar salud: {ex.Message}");
                return null;
            }
        }
    }
}