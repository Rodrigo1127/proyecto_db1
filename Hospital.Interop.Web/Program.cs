using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Hospital.Interop.Web;
using Hospital.Interop.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para conectar con el backend API.
// En local usa appsettings.json, en producción usa el mismo host.
var backendUrl = builder.Configuration["BackendUrl"] ?? builder.HostEnvironment.BaseAddress;
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