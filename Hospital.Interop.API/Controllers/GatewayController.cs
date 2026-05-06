using Microsoft.AspNetCore.Mvc;
using Hospital.Interop.API.Models;
using Hospital.Interop.API.Models.DTOs;
using Hospital.Interop.API.Integrations;
using Hospital.Interop.API.Services;
using Hospital.Interop.API.Attributes;

namespace Hospital.Interop.API.Controllers
{
    [ApiController]
    [Route("api/gateway")]
    [Produces("application/json")]
    public class GatewayController : ControllerBase
    {
        // Clientes de servicios clínicos
        private readonly AtencionPacienteClient _atencionPaciente;
        private readonly EmergenciasClient _emergencias;
        private readonly FarmaciaHospitalariaClient _farmacia;
        private readonly MaternidadClient _maternidad;
        private readonly AmbulanciasClient _ambulancias;
        private readonly ControlEpidemiologicoClient _epidemiologia;
        private readonly GestionQuirurgicaClient _quirurgico;
        private readonly EnfermeriaClient _enfermeria;
        private readonly ConsultasExternasClient _consultasExternas;
        private readonly TelemedicinaClient _telemedicina;
        private readonly LaboratorioExternoClient _laboratorio;
        private readonly DiagnosticoImagenesClient _imagenes;
        private readonly TerapiasRehabilitacionClient _terapias;
        private readonly HospitalizacionClient _hospitalizacion;
        private readonly CuidadosCriticosClient _cuidadosCriticos;
        private readonly DepartamentoesMedicasClient _Departamentoes;
        private readonly InvestigacionClinicaClient _investigacion;

        // Clientes administrativos
        private readonly FacturacionExternaClient _facturacion;
        private readonly GestionPacientesClient _gestionPacientes;
        private readonly GestionTurnosClient _turnos;
        private readonly InventariosClient _inventarios;
        private readonly ComprasAbastecimientoClient _compras;
        private readonly LogisticaHospitalariaClient _logistica;
        private readonly GestionFinancieraClient _financiero;

        // Servicio propio (base de datos local)
        private readonly OrquestadorService _orquestador;
        private readonly MapperService _mapper;

        public GatewayController(
            AtencionPacienteClient atencionPaciente,
            EmergenciasClient emergencias,
            FarmaciaHospitalariaClient farmacia,
            MaternidadClient maternidad,
            AmbulanciasClient ambulancias,
            ControlEpidemiologicoClient epidemiologia,
            GestionQuirurgicaClient quirurgico,
            EnfermeriaClient enfermeria,
            ConsultasExternasClient consultasExternas,
            TelemedicinaClient telemedicina,
            LaboratorioExternoClient laboratorio,
            DiagnosticoImagenesClient imagenes,
            TerapiasRehabilitacionClient terapias,
            HospitalizacionClient hospitalizacion,
            CuidadosCriticosClient cuidadosCriticos,
            DepartamentoesMedicasClient Departamentoes,
            InvestigacionClinicaClient investigacion,
            FacturacionExternaClient facturacion,
            GestionPacientesClient gestionPacientes,
            GestionTurnosClient turnos,
            InventariosClient inventarios,
            ComprasAbastecimientoClient compras,
            LogisticaHospitalariaClient logistica,
            GestionFinancieraClient financiero,
            OrquestadorService orquestador,
            MapperService mapper)
        {
            _atencionPaciente = atencionPaciente;
            _emergencias = emergencias;
            _farmacia = farmacia;
            _maternidad = maternidad;
            _ambulancias = ambulancias;
            _epidemiologia = epidemiologia;
            _quirurgico = quirurgico;
            _enfermeria = enfermeria;
            _consultasExternas = consultasExternas;
            _telemedicina = telemedicina;
            _laboratorio = laboratorio;
            _imagenes = imagenes;
            _terapias = terapias;
            _hospitalizacion = hospitalizacion;
            _cuidadosCriticos = cuidadosCriticos;
            _Departamentoes = Departamentoes;
            _investigacion = investigacion;
            _facturacion = facturacion;
            _gestionPacientes = gestionPacientes;
            _turnos = turnos;
            _inventarios = inventarios;
            _compras = compras;
            _logistica = logistica;
            _financiero = financiero;
            _orquestador = orquestador;
            _mapper = mapper;
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET api/gateway/paciente-completo/{id}
        // Agrega datos del paciente desde TODOS los departamentos en paralelo.
        // Si un departamento no está disponible, igual responde con los demás.
        // ─────────────────────────────────────────────────────────────────────
        [HttpGet("paciente-completo/{id:int}")]
        [ProducesResponseType(typeof(RespuestaGatewayCompleta), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPacienteCompleto(int id)
        {
            // Obtenemos el paciente desde nuestra propia DB (base local)
            Paciente? paciente = null;
            try
            {
                var local = await _orquestador.ObtenerPacienteCompleto(id);
                paciente = local.Paciente;
            }
            catch { }

            if (paciente == null)
                return NotFound(new { mensaje = $"Paciente con id {id} no encontrado en el sistema local." });

            // Consultamos TODOS los departamentos en paralelo (no bloqueamos si uno falla)
            var tareas = new
            {
                AtencionPaciente = _atencionPaciente.GetDatosPaciente(id),
                Emergencias = _emergencias.GetDatosPaciente(id),
                Farmacia = _farmacia.GetDatosPaciente(id),
                Maternidad = _maternidad.GetDatosPaciente(id),
                Ambulancias = _ambulancias.GetDatosPaciente(id),
                Epidemiologia = _epidemiologia.GetDatosPaciente(id),
                Quirurgico = _quirurgico.GetDatosPaciente(id),
                Enfermeria = _enfermeria.GetDatosPaciente(id),
                ConsultasExternas = _consultasExternas.GetDatosPaciente(id),
                Telemedicina = _telemedicina.GetDatosPaciente(id),
                Laboratorio = _laboratorio.GetDatosPaciente(id),
                Imagenes = _imagenes.GetDatosPaciente(id),
                Terapias = _terapias.GetDatosPaciente(id),
                Hospitalizacion = _hospitalizacion.GetDatosPaciente(id),
                CuidadosCriticos = _cuidadosCriticos.GetDatosPaciente(id),
                Departamentoes = _Departamentoes.GetDatosPaciente(id),
                Investigacion = _investigacion.GetDatosPaciente(id),
                Facturacion = _facturacion.GetDatosPaciente(id),
                GestionPacientes = _gestionPacientes.GetDatosPaciente(id),
                Turnos = _turnos.GetDatosPaciente(id),
                Inventarios = _inventarios.GetDatosPaciente(id),
                Compras = _compras.GetDatosPaciente(id),
                Logistica = _logistica.GetDatosPaciente(id),
                Financiero = _financiero.GetDatosPaciente(id),
            };

            // Esperamos todas en paralelo
            await Task.WhenAll(
                tareas.AtencionPaciente, tareas.Emergencias, tareas.Farmacia,
                tareas.Maternidad, tareas.Ambulancias, tareas.Epidemiologia,
                tareas.Quirurgico, tareas.Enfermeria, tareas.ConsultasExternas,
                tareas.Telemedicina, tareas.Laboratorio, tareas.Imagenes,
                tareas.Terapias, tareas.Hospitalizacion, tareas.CuidadosCriticos,
                tareas.Departamentoes, tareas.Investigacion, tareas.Facturacion,
                tareas.GestionPacientes, tareas.Turnos, tareas.Inventarios,
                tareas.Compras, tareas.Logistica, tareas.Financiero
            );

            var respuesta = new RespuestaGatewayCompleta
            {
                Paciente = paciente,
                AtencionPaciente = tareas.AtencionPaciente.Result,
                Emergencias = tareas.Emergencias.Result,
                Farmacia = tareas.Farmacia.Result,
                Maternidad = tareas.Maternidad.Result,
                Ambulancias = tareas.Ambulancias.Result,
                ControlEpidemiologico = tareas.Epidemiologia.Result,
                GestionQuirurgica = tareas.Quirurgico.Result,
                Enfermeria = tareas.Enfermeria.Result,
                ConsultasExternas = tareas.ConsultasExternas.Result,
                Telemedicina = tareas.Telemedicina.Result,
                Laboratorio = tareas.Laboratorio.Result,
                DiagnosticoImagenes = tareas.Imagenes.Result,
                TerapiasRehabilitacion = tareas.Terapias.Result,
                Hospitalizacion = tareas.Hospitalizacion.Result,
                CuidadosCriticos = tareas.CuidadosCriticos.Result,
                DepartamentoesMedicas = tareas.Departamentoes.Result,
                InvestigacionClinica = tareas.Investigacion.Result,
                Facturacion = tareas.Facturacion.Result,
                GestionPacientes = tareas.GestionPacientes.Result,
                GestionTurnos = tareas.Turnos.Result,
                Inventarios = tareas.Inventarios.Result,
                ComprasAbastecimiento = tareas.Compras.Result,
                LogisticaHospitalaria = tareas.Logistica.Result,
                GestionFinanciera = tareas.Financiero.Result,
            };

            // Registrar cuáles servicios fallaron
            var todasRespuestas = new[] {
                respuesta.AtencionPaciente, respuesta.Emergencias, respuesta.Farmacia,
                respuesta.Maternidad, respuesta.Ambulancias, respuesta.ControlEpidemiologico,
                respuesta.GestionQuirurgica, respuesta.Enfermeria, respuesta.ConsultasExternas,
                respuesta.Telemedicina, respuesta.Laboratorio, respuesta.DiagnosticoImagenes,
                respuesta.TerapiasRehabilitacion, respuesta.Hospitalizacion, respuesta.CuidadosCriticos,
                respuesta.DepartamentoesMedicas, respuesta.InvestigacionClinica, respuesta.Facturacion,
                respuesta.GestionPacientes, respuesta.GestionTurnos, respuesta.Inventarios,
                respuesta.ComprasAbastecimiento, respuesta.LogisticaHospitalaria, respuesta.GestionFinanciera
            };

            respuesta.ServiciosConError = todasRespuestas
                .Where(r => !r.Disponible)
                .Select(r => r.Departamento)
                .ToList();

            return Ok(respuesta);
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET api/gateway/health
        // Muestra qué departamentos están online y cuáles no responden.
        // ─────────────────────────────────────────────────────────────────────
        [HttpGet("health")]
        [ProducesResponseType(typeof(object), 200)]
        public async Task<IActionResult> Health()
        {
            // Usamos id=0 solo para verificar conectividad (no importa el resultado)
            var checks = new Dictionary<string, Task<RespuestaDepartamento>>
            {
                ["Atención al Paciente"] = _atencionPaciente.GetDatosPaciente(0),
                ["Emergencias y Triaje"] = _emergencias.GetDatosPaciente(0),
                ["Farmacia Hospitalaria"] = _farmacia.GetDatosPaciente(0),
                ["Maternidad y Neonatología"] = _maternidad.GetDatosPaciente(0),
                ["Gestión de Ambulancias"] = _ambulancias.GetDatosPaciente(0),
                ["Control Epidemiológico"] = _epidemiologia.GetDatosPaciente(0),
                ["Gestión Quirúrgica"] = _quirurgico.GetDatosPaciente(0),
                ["Gestión de Enfermería"] = _enfermeria.GetDatosPaciente(0),
                ["Consultas Externas"] = _consultasExternas.GetDatosPaciente(0),
                ["Telemedicina"] = _telemedicina.GetDatosPaciente(0),
                ["Laboratorio Clínico"] = _laboratorio.GetDatosPaciente(0),
                ["Diagnóstico por Imágenes"] = _imagenes.GetDatosPaciente(0),
                ["Terapias y Rehabilitación"] = _terapias.GetDatosPaciente(0),
                ["Hospitalización"] = _hospitalizacion.GetDatosPaciente(0),
                ["Cuidados Críticos"] = _cuidadosCriticos.GetDatosPaciente(0),
                ["Departamentoes Médicas"] = _Departamentoes.GetDatosPaciente(0),
                ["Investigación Clínica"] = _investigacion.GetDatosPaciente(0),
                ["Facturación y Seguros"] = _facturacion.GetDatosPaciente(0),
                ["Gestión de Pacientes"] = _gestionPacientes.GetDatosPaciente(0),
                ["Gestión de Turnos y Citas"] = _turnos.GetDatosPaciente(0),
                ["Gestión de Inventarios"] = _inventarios.GetDatosPaciente(0),
                ["Compras y Abastecimiento"] = _compras.GetDatosPaciente(0),
                ["Logística Hospitalaria"] = _logistica.GetDatosPaciente(0),
                ["Gestión Financiera"] = _financiero.GetDatosPaciente(0),
            };

            await Task.WhenAll(checks.Values);

            var estado = checks.ToDictionary(
                kv => kv.Key,
                kv => kv.Value.Result.Disponible ? "online" : (object)new { estado = "offline", error = kv.Value.Result.Error }
            );

            var totalOnline = estado.Values.Count(v => v is string s && s == "online");

            return Ok(new
            {
                resumen = $"{totalOnline} de {checks.Count} servicios disponibles",
                servicios = estado,
                timestamp = DateTime.Now
            });
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET api/gateway/departamento/{nombre}/{pacienteId}
        // Consulta solo un departamento específico
        // ─────────────────────────────────────────────────────────────────────
        [HttpGet("departamento/{nombre}/{pacienteId:int}")]
        [ProducesResponseType(typeof(RespuestaDepartamento), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetDepartamento(string nombre, int pacienteId)
        {
            var mapa = new Dictionary<string, Func<int, Task<RespuestaDepartamento>>>(StringComparer.OrdinalIgnoreCase)
            {
                ["atencion-paciente"] = _atencionPaciente.GetDatosPaciente,
                ["emergencias"] = _emergencias.GetDatosPaciente,
                ["farmacia"] = _farmacia.GetDatosPaciente,
                ["maternidad"] = _maternidad.GetDatosPaciente,
                ["ambulancias"] = _ambulancias.GetDatosPaciente,
                ["epidemiologia"] = _epidemiologia.GetDatosPaciente,
                ["quirurgico"] = _quirurgico.GetDatosPaciente,
                ["enfermeria"] = _enfermeria.GetDatosPaciente,
                ["consultas-externas"] = _consultasExternas.GetDatosPaciente,
                ["telemedicina"] = _telemedicina.GetDatosPaciente,
                ["laboratorio"] = _laboratorio.GetDatosPaciente,
                ["imagenes"] = _imagenes.GetDatosPaciente,
                ["terapias"] = _terapias.GetDatosPaciente,
                ["hospitalizacion"] = _hospitalizacion.GetDatosPaciente,
                ["cuidados-criticos"] = _cuidadosCriticos.GetDatosPaciente,
                ["Departamentoes"] = _Departamentoes.GetDatosPaciente,
                ["investigacion"] = _investigacion.GetDatosPaciente,
                ["facturacion"] = _facturacion.GetDatosPaciente,
                ["gestion-pacientes"] = _gestionPacientes.GetDatosPaciente,
                ["turnos"] = _turnos.GetDatosPaciente,
                ["inventarios"] = _inventarios.GetDatosPaciente,
                ["compras"] = _compras.GetDatosPaciente,
                ["logistica"] = _logistica.GetDatosPaciente,
                ["financiero"] = _financiero.GetDatosPaciente,
            };

            if (!mapa.TryGetValue(nombre, out var getter))
                return BadRequest(new
                {
                    mensaje = $"Departamento '{nombre}' no reconocido.",
                    disponibles = mapa.Keys
                });

            var resultado = await getter(pacienteId);
            return Ok(resultado);
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET api/gateway/paciente/{id}
        // Retorna solo datos básicos del paciente SIN ID (para departamentos)
        // ─────────────────────────────────────────────────────────────────────
        [HttpGet("paciente/{id:int}")]
        [ProducesResponseType(typeof(PacienteDTOSinId), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPacienteSinId(int id)
        {
            try
            {
                var pacienteCompleto = await _orquestador.ObtenerPacienteCompleto(id);
                if (pacienteCompleto?.Paciente == null)
                    return NotFound(new { mensaje = $"Paciente con id {id} no encontrado." });

                var pacienteDTO = _mapper.MapearPacienteSinId(pacienteCompleto.Paciente);
                return Ok(pacienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // ─────────────────────────────────────────────────────────────────────
        // GET api/gateway/paciente-admin/{id}
        // Retorna datos del paciente CON ID (requiere autenticación de admin)
        // ─────────────────────────────────────────────────────────────────────
        [HttpGet("paciente-admin/{id:int}")]
        [ProducesResponseType(typeof(PacienteDTOConId), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPacienteConId(int id)
        {
            // Validar que sea admin (en producción, usar JWT o Similar)
            if (!Request.Headers.TryGetValue("X-Admin-Key", out var adminKey) || 
                adminKey != "admin-secret-key")
            {
                return Unauthorized(new { mensaje = "Se requiere X-Admin-Key header para acceder a esta información." });
            }

            try
            {
                var pacienteCompleto = await _orquestador.ObtenerPacienteCompleto(id);
                if (pacienteCompleto?.Paciente == null)
                    return NotFound(new { mensaje = $"Paciente con id {id} no encontrado." });

                var pacienteDTO = _mapper.MapearPacienteConId(pacienteCompleto.Paciente);
                return Ok(pacienteDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}