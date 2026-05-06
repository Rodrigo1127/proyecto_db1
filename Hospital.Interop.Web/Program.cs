using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Hospital.Interop.Web;
using Hospital.Interop.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para conectar con el backend API.
// Usamos HTTP para evitar problemas de certificado local.
var backendUrl = builder.Configuration["BackendUrl"] ?? "http://localhost:5225";

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(backendUrl)
});

// Registrar servicios del frontend
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<LaboratorioService>();
builder.Services.AddScoped<FacturacionService>();
builder.Services.AddScoped<DepartamentosService>();

await builder.Build().RunAsync();