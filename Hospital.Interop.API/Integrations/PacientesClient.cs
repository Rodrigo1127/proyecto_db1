using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    public class PacientesClient
    {
        public async Task<Paciente> GetPaciente(int id)
        {
            await Task.Delay(200);

            return new Paciente
            {
                PacienteId = id,
                Nombre = "Rodrigo Lopez"
            };
        }
    }
}
