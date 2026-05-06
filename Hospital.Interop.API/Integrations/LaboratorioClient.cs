using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    public class LaboratorioClient
    {
        public async Task<List<Examen>> GetExamenes(int id)
        {
            await Task.Delay(200);

            return new List<Examen>
            {
                new Examen { Id = 1, Nombre = "Sangre", Resultado = "Normal" },
                new Examen { Id = 2, Nombre = "Orina", Resultado = "OK" }
            };
        }
    }
}
