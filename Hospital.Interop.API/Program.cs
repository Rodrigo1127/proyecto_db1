using Hospital.Interop.API.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Controllers
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
builder.Services.AddScoped<Hospital.Interop.API.Services.OrquestadorService>();
builder.Services.AddScoped<Hospital.Interop.API.Integrations.PacientesClient>();
builder.Services.AddScoped<Hospital.Interop.API.Integrations.LaboratorioClient>();
builder.Services.AddScoped<Hospital.Interop.API.Integrations.CitasClient>();
builder.Services.AddScoped<Hospital.Interop.API.Integrations.FacturacionClient>();
builder.Services.AddScoped<Hospital.Interop.API.Integrations.FarmaciaClient>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

});
// --------------------------------

// 🔴 ACTIVAR CORS (ANTES de MapControllers)
app.UseCors("AllowAll");

// app.UseHttpsRedirection(); // Desactivado para facilitar interoperabilidad en redes locales

app.UseAuthorization();

app.MapControllers();

app.Run();

