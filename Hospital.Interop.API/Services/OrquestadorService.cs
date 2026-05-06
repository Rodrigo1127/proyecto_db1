using Hospital.Interop.API.Models;
using Hospital.Interop.API.Integrations;

namespace Hospital.Interop.API.Services
{
    public class OrquestadorService
    {
        private readonly PacientesClient _pacientes;
        private readonly LaboratorioClient _laboratorio;
        private readonly CitasClient _citas;
        private readonly FacturacionClient _facturacion;

        public OrquestadorService(
            PacientesClient pacientes,
            LaboratorioClient laboratorio,
            CitasClient citas,
            FacturacionClient facturacion)
        {
            _pacientes = pacientes;
            _laboratorio = laboratorio;
            _citas = citas;
            _facturacion = facturacion;
        }

        public async Task<PacienteCompleto> ObtenerPacienteCompleto(int id)
        {
            var paciente = await _pacientes.GetPaciente(id);
            var examenes = await _laboratorio.GetExamenes(id);
            var citas = await _citas.GetCitas(id);
            var facturas = await _facturacion.GetFacturas(id);

            return new PacienteCompleto
            {
                Paciente = paciente,
                Examenes = examenes,
                Citas = citas,
                Facturas = facturas
            };
        }
    }
}
