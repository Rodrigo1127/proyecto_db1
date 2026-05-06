using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    public class CitasClient
    {
        public async Task<List<Cita>> GetCitas(int id)
        {
            await Task.Delay(200);

            return new List<Cita>
            {
                new Cita { Id = 1, Fecha = DateTime.Now, Departamento = "Cardiología" },
                new Cita { Id = 2, Fecha = DateTime.Now.AddDays(2), Departamento = "Dermatología" }
            };
        }
    }
}
