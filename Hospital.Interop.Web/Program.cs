using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Hospital.Interop.Web;
using Hospital.Interop.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configurar HttpClient para conectar con el backend API.
// Usamos la dirección base del host para que funcione tanto en local como en la nube (misma origen)
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

// Registrar servicios del frontend
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<CitasService>();
builder.Services.AddScoped<LaboratorioService>();
builder.Services.AddScoped<FacturacionService>();
builder.Services.AddScoped<DepartamentosService>();

await builder.Build().RunAsync();