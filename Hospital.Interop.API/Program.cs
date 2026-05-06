using Hospital.Interop.API.Data;
using Hospital.Interop.API.Services;
using Hospital.Interop.API.Integrations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar el puerto para Railway (solo en producción/nube)
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

// Controllers con configuración de JSON para interoperabilidad
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

// HTTP CLIENT para consumir otros sistemas
builder.Services.AddHttpClient();

// CORS (IMPORTANTE para conectar frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// DB Context
builder.Services.AddDbContext<HospitalDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// --- REGISTRO DE SERVICIOS PROPIOS ---
builder.Services.AddScoped<OrquestadorService>();
builder.Services.AddScoped<MapperService>();
builder.Services.AddScoped<PacientesClient>();
builder.Services.AddScoped<LaboratorioClient>();
builder.Services.AddScoped<CitasClient>();
builder.Services.AddScoped<FacturacionClient>();
builder.Services.AddScoped<FarmaciaClient>();

// --- REGISTRO DE CLIENTES DE DEPARTAMENTOS (para el Gateway) ---
builder.Services.AddScoped<AtencionPacienteClient>();
builder.Services.AddScoped<EmergenciasClient>();
builder.Services.AddScoped<FarmaciaHospitalariaClient>();
builder.Services.AddScoped<MaternidadClient>();
builder.Services.AddScoped<AmbulanciasClient>();
builder.Services.AddScoped<ControlEpidemiologicoClient>();
builder.Services.AddScoped<GestionQuirurgicaClient>();
builder.Services.AddScoped<EnfermeriaClient>();
builder.Services.AddScoped<ConsultasExternasClient>();
builder.Services.AddScoped<TelemedicinaClient>();
builder.Services.AddScoped<LaboratorioExternoClient>();
builder.Services.AddScoped<DiagnosticoImagenesClient>();
builder.Services.AddScoped<TerapiasRehabilitacionClient>();
builder.Services.AddScoped<HospitalizacionClient>();
builder.Services.AddScoped<CuidadosCriticosClient>();
builder.Services.AddScoped<DepartamentoesMedicasClient>();
builder.Services.AddScoped<InvestigacionClinicaClient>();
builder.Services.AddScoped<FacturacionExternaClient>();
builder.Services.AddScoped<GestionPacientesClient>();
builder.Services.AddScoped<GestionTurnosClient>();
builder.Services.AddScoped<InventariosClient>();
builder.Services.AddScoped<ComprasAbastecimientoClient>();
builder.Services.AddScoped<LogisticaHospitalariaClient>();
builder.Services.AddScoped<GestionFinancieraClient>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- APLICAR MIGRACIONES AL INICIAR (Para la nube) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<HospitalDbContext>();
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        // Si falla la migración, lo registramos pero dejamos que la app intente iniciar
        Console.WriteLine($"Error aplicando migraciones: {ex.Message}");
    }
}

// Global Error Handling para Interoperabilidad
app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        var error = new { message = "Error interno en el servidor de Interoperabilidad." };
        await context.Response.WriteAsJsonAsync(error);
    });
});

// Se habilitan Swagger y SwaggerUI fuera del bloque IsDevelopment
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Proyecto_H API V1");
    c.RoutePrefix = "swagger";
});
// --------------------------------

// --- SOPORTE PARA EL FRONTEND (BLAZOR WASM) ---
app.UseBlazorFrameworkFiles();
app.UseDefaultFiles();
app.UseStaticFiles();
// ---------------------------------------------

// 🔴 ACTIVAR CORS (ANTES de MapControllers)
app.UseCors("AllowAll");

// app.UseHttpsRedirection(); // Desactivado para facilitar interoperabilidad en redes locales

app.UseAuthorization();

// Health Check
app.MapGet("/health", () => Results.Ok(new { status = "Running", timestamp = DateTime.UtcNow }));

// 🔴 IMPORTANTE: MapControllers ANTES de MapFallbackToFile
// para que las rutas /api/* siempre sean manejadas por los controllers
app.MapControllers();

// Fallback para Blazor (permite que el routing del cliente funcione)
// DEBE ir DESPUÉS de MapControllers para no interceptar las rutas de la API
app.MapFallbackToFile("index.html");

app.Run();
