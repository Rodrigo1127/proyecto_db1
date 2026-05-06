using System.Net.Http.Json;
using Hospital.Interop.API.Models;

namespace Hospital.Interop.API.Integrations
{
    // Cliente base con tolerancia a fallos.
    // Todos los clientes de departamento heredan de este.
    public abstract class DepartamentoClientBase
    {
        protected readonly HttpClient _http;
        protected abstract string NombreDepartamento { get; }

        protected DepartamentoClientBase(HttpClient http)
        {
            _http = http;
        }

        // Llama al servicio externo. Si falla, devuelve RespuestaDepartamento con Disponible=false
        protected async Task<RespuestaDepartamento> LlamarAsync<T>(string url)
        {
            try
            {
                var datos = await _http.GetFromJsonAsync<List<T>>(url);
                return new RespuestaDepartamento
                {
                    Disponible = true,
                    Departamento = NombreDepartamento,
                    Datos = datos ?? new List<T>()
                };
            }
            catch (HttpRequestException ex)
            {
                return new RespuestaDepartamento
                {
                    Disponible = false,
                    Departamento = NombreDepartamento,
                    Datos = new List<T>(),
                    Error = $"Servicio no alcanzable: {ex.Message}"
                };
            }
            catch (TaskCanceledException)
            {
                return new RespuestaDepartamento
                {
                    Disponible = false,
                    Departamento = NombreDepartamento,
                    Datos = new List<T>(),
                    Error = "Tiempo de espera agotado (timeout)"
                };
            }
            catch (Exception ex)
            {
                return new RespuestaDepartamento
                {
                    Disponible = false,
                    Departamento = NombreDepartamento,
                    Datos = new List<T>(),
                    Error = $"Error inesperado: {ex.Message}"
                };
            }
        }
    }

    // ── Clientes por departamento ──────────────────────────────────────────────
    // Cada uno apunta a la URL base configurada en appsettings.json
    // y expone un método GetDatosPaciente(int pacienteId).

    public class AtencionPacienteClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Atención al Paciente";
        public AtencionPacienteClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoAtencionPaciente>($"/api/atencion-paciente/{id}");
    }

    public class EmergenciasClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Emergencias y Triaje";
        public EmergenciasClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoEmergencia>($"/api/emergencias/{id}");
    }

    public class FarmaciaHospitalariaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Farmacia Hospitalaria";
        public FarmaciaHospitalariaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoFarmacia>($"/api/farmacia/{id}");
    }

    public class MaternidadClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Maternidad y Neonatología";
        public MaternidadClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoMaternidad>($"/api/maternidad/{id}");
    }

    public class AmbulanciasClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión de Ambulancias";
        public AmbulanciasClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoAmbulancia>($"/api/ambulancias/{id}");
    }

    public class ControlEpidemiologicoClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Control Epidemiológico";
        public ControlEpidemiologicoClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoEpidemiologico>($"/api/epidemiologia/{id}");
    }

    public class GestionQuirurgicaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión Quirúrgica";
        public GestionQuirurgicaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoQuirurgico>($"/api/quirurgico/{id}");
    }

    public class EnfermeriaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión de Enfermería";
        public EnfermeriaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoEnfermeria>($"/api/enfermeria/{id}");
    }

    public class ConsultasExternasClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Consultas Externas";
        public ConsultasExternasClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoConsultaExterna>($"/api/consultas-externas/{id}");
    }

    public class TelemedicinaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Telemedicina";
        public TelemedicinaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoTelemedicina>($"/api/telemedicina/{id}");
    }

    public class LaboratorioExternoClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Laboratorio Clínico";
        public LaboratorioExternoClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoLaboratorio>($"/api/laboratorio/{id}");
    }

    public class DiagnosticoImagenesClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Diagnóstico por Imágenes";
        public DiagnosticoImagenesClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoDiagnosticoImagen>($"/api/imagenes/{id}");
    }

    public class TerapiasRehabilitacionClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Terapias y Rehabilitación";
        public TerapiasRehabilitacionClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoTerapia>($"/api/terapias/{id}");
    }

    public class HospitalizacionClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Hospitalización";
        public HospitalizacionClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoHospitalizacion>($"/api/hospitalizacion/{id}");
    }

    public class CuidadosCriticosClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Cuidados Críticos";
        public CuidadosCriticosClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoCuidadoCritico>($"/api/cuidados-criticos/{id}");
    }

    public class DepartamentoesMedicasClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Departamentoes Médicas";
        public DepartamentoesMedicasClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoConsultaExterna>($"/api/Departamentoes/{id}");
    }

    public class InvestigacionClinicaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Investigación Clínica";
        public InvestigacionClinicaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoInvestigacion>($"/api/investigacion/{id}");
    }

    // ── Administrativos ────────────────────────────────────────────────────────

    public class FacturacionExternaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Facturación y Seguros";
        public FacturacionExternaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoFacturacion>($"/api/facturacion/{id}");
    }

    public class GestionPacientesClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión de Pacientes";
        public GestionPacientesClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoAtencionPaciente>($"/api/gestion-pacientes/{id}");
    }

    public class GestionTurnosClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión de Turnos y Citas";
        public GestionTurnosClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoConsultaExterna>($"/api/turnos/{id}");
    }

    public class InventariosClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión de Inventarios";
        public InventariosClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoFarmacia>($"/api/inventarios/{id}");
    }

    public class ComprasAbastecimientoClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Compras y Abastecimiento";
        public ComprasAbastecimientoClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoFarmacia>($"/api/compras/{id}");
    }

    public class LogisticaHospitalariaClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Logística Hospitalaria";
        public LogisticaHospitalariaClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoAmbulancia>($"/api/logistica/{id}");
    }

    public class GestionFinancieraClient : DepartamentoClientBase
    {
        protected override string NombreDepartamento => "Gestión Financiera";
        public GestionFinancieraClient(HttpClient http) : base(http) { }
        public Task<RespuestaDepartamento> GetDatosPaciente(int id) =>
            LlamarAsync<DatoFacturacion>($"/api/financiero/{id}");
    }
}