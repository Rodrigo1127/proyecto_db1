using Hospital.Interop.API.Integrations;
using Hospital.Interop.API.Services;
using Hospital.Interop.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Net;

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
// Local: usa appsettings.json
// Railway: usa DATABASE_URL
builder.Services.AddDbContext<HospitalDbContext>(options =>
{
    var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

    string connectionString;

    if (!string.IsNullOrWhiteSpace(databaseUrl))
    {
        var uri = new Uri(databaseUrl);
        var userInfo = uri.UserInfo.Split(':');

        var username = WebUtility.UrlDecode(userInfo[0]);
        var password = WebUtility.UrlDecode(userInfo[1]);
        var database = uri.AbsolutePath.TrimStart('/');

        connectionString =
            $"Host={uri.Host};" +
            $"Port={uri.Port};" +
            $"Database={database};" +
            $"Username={username};" +
            $"Password={password};" +
            $"SSL Mode=Require;" +
            $"Trust Server Certificate=true";
    }
    else
    {
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("No se encontró la cadena de conexión DefaultConnection.");
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

// Clientes/departamentos externos
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

// Esta clase es la que existe en tu proyecto.
// No uses EspecialidadesMedicasClient porque no existe.
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

// En Railway no usamos redirección HTTPS interna.
// Localmente también lo dejamos comentado para evitar problemas con Blazor Web.
// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();