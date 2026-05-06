using Hospital.Interop.API.Integrations;
using Hospital.Interop.API.Services;
using Hospital.Interop.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// ── Controllers y Swagger ─────────────────────────────────────────────────────
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Hospital Interop Gateway",
        Version = "v1",
        Description = "API Gateway para integración de microservicios del ERP Hospitalario"
    });
});

// ── Base de datos PostgreSQL ──────────────────────────────────────────────────
builder.Services.AddDbContext<HospitalDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException(
            "No se encontró la cadena de conexión 'DefaultConnection' en appsettings.json.");
    }

    options.UseNpgsql(connectionString);
});

// ── Servicio orquestador interno ──────────────────────────────────────────────
builder.Services.AddScoped<PacientesClient>();
builder.Services.AddScoped<LaboratorioClient>();
builder.Services.AddScoped<CitasClient>();
builder.Services.AddScoped<FacturacionClient>();
builder.Services.AddScoped<OrquestadorService>();
builder.Services.AddScoped<MapperService>();

// ── Clientes HTTP de departamentos externos ──────────────────────────────────
var urls = builder.Configuration.GetSection("ServiceUrls");

void AddDeptClient<T>(string key) where T : DepartamentoClientBase
{
    var url = urls[key];

    builder.Services.AddHttpClient<T>(client =>
    {
        if (!string.IsNullOrWhiteSpace(url))
        {
            client.BaseAddress = new Uri(url);
        }

        client.Timeout = TimeSpan.FromSeconds(10);
    });
}

AddDeptClient<AtencionPacienteClient>("AtencionPaciente");
AddDeptClient<EmergenciasClient>("Emergencias");
AddDeptClient<FarmaciaHospitalariaClient>("Farmacia");
AddDeptClient<MaternidadClient>("Maternidad");
AddDeptClient<AmbulanciasClient>("Ambulancias");
AddDeptClient<ControlEpidemiologicoClient>("ControlEpidemiologico");
AddDeptClient<GestionQuirurgicaClient>("GestionQuirurgica");
AddDeptClient<EnfermeriaClient>("Enfermeria");
AddDeptClient<ConsultasExternasClient>("ConsultasExternas");
AddDeptClient<TelemedicinaClient>("Telemedicina");
AddDeptClient<LaboratorioExternoClient>("LaboratorioClinico");
AddDeptClient<DiagnosticoImagenesClient>("DiagnosticoImagenes");
AddDeptClient<TerapiasRehabilitacionClient>("TerapiasRehabilitacion");
AddDeptClient<HospitalizacionClient>("Hospitalizacion");
AddDeptClient<CuidadosCriticosClient>("CuidadosCriticos");
AddDeptClient<DepartamentoesMedicasClient>("DepartamentoesMedicas");
AddDeptClient<InvestigacionClinicaClient>("InvestigacionClinica");
AddDeptClient<FacturacionExternaClient>("Facturacion");
AddDeptClient<GestionPacientesClient>("GestionPacientes");
AddDeptClient<GestionTurnosClient>("GestionTurnos");
AddDeptClient<InventariosClient>("Inventarios");
AddDeptClient<ComprasAbastecimientoClient>("ComprasAbastecimiento");
AddDeptClient<LogisticaHospitalariaClient>("LogisticaHospitalaria");
AddDeptClient<GestionFinancieraClient>("GestionFinanciera");

// ── CORS ──────────────────────────────────────────────────────────────────────
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ── Migraciones automáticas ───────────────────────────────────────────────────
// Esto crea/actualiza las tablas en PostgreSQL si existen migraciones.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<HospitalDbContext>();

    if (db.Database.IsRelational())
    {
        db.Database.Migrate();
    }
}

// ── Middleware ────────────────────────────────────────────────────────────────
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hospital.Interop.API v1");
    c.RoutePrefix = "swagger";
});

// IMPORTANTE:
// Se comenta para trabajar con el frontend usando HTTP.
// Si lo dejas activo, puede redirigir a HTTPS y bloquear la conexión desde Blazor.
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();