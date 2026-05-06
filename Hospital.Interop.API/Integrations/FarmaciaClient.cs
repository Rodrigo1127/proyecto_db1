using System.Net.Http.Json;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    public class FarmaciaClient
    {
        private readonly HttpClient _http;

        public FarmaciaClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<Medicamento>> GetMedicamentos(int id)
        {
            return await _http.GetFromJsonAsync<List<Medicamento>>($"/api/farmacia/{id}");
        }
    }
}
