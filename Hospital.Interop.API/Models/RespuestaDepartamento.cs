namespace Hospital.Interop.API.Models
{
    // Respuesta genérica que envuelve cualquier dato de servicio externo
    public class RespuestaDepartamento
    {
        public bool Disponible { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public object? Datos { get; set; }
        public string? Error { get; set; }
        public DateTime ConsultadoEn { get; set; } = DateTime.Now;
    }

    // ── Modelos de respuesta por departamento ──────────────────────────────────

    public class DatoAtencionPaciente
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class DatoEmergencia
    {
        public int Id { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Prioridad { get; set; } = string.Empty;
        public DateTime Ingreso { get; set; }
    }

    public class DatoFarmacia
    {
        public int Id { get; set; }
        public string Medicamento { get; set; } = string.Empty;
        public string Dosis { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }

    public class DatoMaternidad
    {
        public int Id { get; set; }
        public string Procedimiento { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; } = string.Empty;
    }

    public class DatoAmbulancia
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string Destino { get; set; } = string.Empty;
        public DateTime FechaSalida { get; set; }
    }

    public class DatoEpidemiologico
    {
        public int Id { get; set; }
        public string Diagnostico { get; set; } = string.Empty;
        public bool Notificado { get; set; }
        public DateTime Fecha { get; set; }
    }

    public class DatoQuirurgico
    {
        public int Id { get; set; }
        public string TipoCirugia { get; set; } = string.Empty;
        public string Cirujano { get; set; } = string.Empty;
        public DateTime FechaProgramada { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class DatoEnfermeria
    {
        public int Id { get; set; }
        public string Actividad { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

    public class DatoConsultaExterna
    {
        public int Id { get; set; }
        public string Departamento { get; set; } = string.Empty;
        public string Medico { get; set; } = string.Empty;
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; } = string.Empty;
    }

    public class DatoTelemedicina
    {
        public int Id { get; set; }
        public string Plataforma { get; set; } = string.Empty;
        public string Especialista { get; set; } = string.Empty;
        public DateTime FechaSesion { get; set; }
    }

    public class DatoLaboratorio
    {
        public int Id { get; set; }
        public string Examen { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
        public string UnidadMedida { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

    public class DatoDiagnosticoImagen
    {
        public int Id { get; set; }
        public string TipoEstudio { get; set; } = string.Empty;
        public string Informe { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

    public class DatoTerapia
    {
        public int Id { get; set; }
        public string TipoTerapia { get; set; } = string.Empty;
        public string Terapeuta { get; set; } = string.Empty;
        public int Sesiones { get; set; }
        public DateTime Inicio { get; set; }
    }

    public class DatoHospitalizacion
    {
        public int Id { get; set; }
        public string Sala { get; set; } = string.Empty;
        public string Cama { get; set; } = string.Empty;
        public DateTime Ingreso { get; set; }
        public DateTime? Alta { get; set; }
        public string Motivo { get; set; } = string.Empty;
    }

    public class DatoFacturacion
    {
        public int Id { get; set; }
        public decimal Monto { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
    }

    public class DatoCuidadoCritico
    {
        public int Id { get; set; }
        public string Unidad { get; set; } = string.Empty;
        public string NivelCuidado { get; set; } = string.Empty;
        public DateTime Ingreso { get; set; }
    }

    public class DatoInvestigacion
    {
        public int Id { get; set; }
        public string Estudio { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public bool ConsentimientoFirmado { get; set; }
    }

    // Respuesta completa del gateway con todos los departamentos
    public class RespuestaGatewayCompleta
    {
        public Paciente? Paciente { get; set; }

        // Servicios clínicos
        public RespuestaDepartamento AtencionPaciente { get; set; } = new();
        public RespuestaDepartamento Emergencias { get; set; } = new();
        public RespuestaDepartamento Farmacia { get; set; } = new();
        public RespuestaDepartamento Maternidad { get; set; } = new();
        public RespuestaDepartamento Ambulancias { get; set; } = new();
        public RespuestaDepartamento ControlEpidemiologico { get; set; } = new();
        public RespuestaDepartamento GestionQuirurgica { get; set; } = new();
        public RespuestaDepartamento Enfermeria { get; set; } = new();
        public RespuestaDepartamento ConsultasExternas { get; set; } = new();
        public RespuestaDepartamento Telemedicina { get; set; } = new();
        public RespuestaDepartamento Laboratorio { get; set; } = new();
        public RespuestaDepartamento DiagnosticoImagenes { get; set; } = new();
        public RespuestaDepartamento TerapiasRehabilitacion { get; set; } = new();
        public RespuestaDepartamento Hospitalizacion { get; set; } = new();
        public RespuestaDepartamento CuidadosCriticos { get; set; } = new();
        public RespuestaDepartamento DepartamentoesMedicas { get; set; } = new();
        public RespuestaDepartamento InvestigacionClinica { get; set; } = new();

        // Servicios administrativos
        public RespuestaDepartamento Facturacion { get; set; } = new();
        public RespuestaDepartamento GestionPacientes { get; set; } = new();
        public RespuestaDepartamento GestionTurnos { get; set; } = new();
        public RespuestaDepartamento Inventarios { get; set; } = new();
        public RespuestaDepartamento ComprasAbastecimiento { get; set; } = new();
        public RespuestaDepartamento LogisticaHospitalaria { get; set; } = new();
        public RespuestaDepartamento GestionFinanciera { get; set; } = new();

        // Resumen de estado
        public int ServiciosDisponibles => ContarDisponibles();
        public int TotalServicios => 24;
        public List<string> ServiciosConError { get; set; } = new();
        public DateTime GeneradoEn { get; set; } = DateTime.Now;

        private int ContarDisponibles()
        {
            var todos = new List<RespuestaDepartamento>
            {
                AtencionPaciente, Emergencias, Farmacia, Maternidad, Ambulancias,
                ControlEpidemiologico, GestionQuirurgica, Enfermeria, ConsultasExternas,
                Telemedicina, Laboratorio, DiagnosticoImagenes, TerapiasRehabilitacion,
                Hospitalizacion, CuidadosCriticos, DepartamentoesMedicas, InvestigacionClinica,
                Facturacion, GestionPacientes, GestionTurnos, Inventarios,
                ComprasAbastecimiento, LogisticaHospitalaria, GestionFinanciera
            };
            return todos.Count(r => r.Disponible);
        }
    }
}