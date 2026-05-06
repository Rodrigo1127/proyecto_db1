# Hospital Interop API - Guía de Integración Frontend-Backend

## ?? Descripción General

Se ha completado la integración entre el **Backend (Hospital.Interop.API)** y el **Frontend (Hospital.Interop.Web)** con una interfaz gráfica moderna basada en Blazor WebAssembly.

## ??? Cambios Realizados

### Backend (Hospital.Interop.API)
- ? **CORS habilitado**: Permite comunicación desde el frontend en `https://localhost:7001` o la URL configurada
- ? **Gateway completamente funcional**: Endpoints API disponibles para gestión de pacientes, citas, laboratorio, etc.
- ? **Base de datos en memoria**: InMemoryDatabase para desarrollo rápido

### Frontend (Hospital.Interop.Web - Blazor WebAssembly)

#### ?? Nuevos Servicios Creados
1. **CitasService** - Gestión de citas médicas
2. **LaboratorioService** - Solicitudes y resultados de pruebas
3. **FacturacionService** - Gestión de facturas
4. **DepartamentosService** - Información de departamentos

#### ?? Nuevas Páginas Razor
1. **Dashboard** (`/dashboard`) - Panel de control con indicadores clave
2. **Pacientes** (`/pacientes`) - Gestión completa de pacientes
3. **Citas** (`/citas`) - Agendamiento y gestión de citas
4. **Laboratorio** (`/laboratorio`) - Solicitudes y resultados de pruebas
5. **Facturación** (`/facturacion`) - Gestión de facturas
6. **Departamentos** (`/departamentos`) - Información de departamentos
7. **Home** (`/`) - Página de inicio renovada

#### ?? Mejoras en la Navegación
- Menú lateral actualizado con nuevas secciones
- Navegación intuitiva entre módulos
- Accesos rápidos desde el dashboard

## ?? Cómo Ejecutar

### 1. Backend (API)
```bash
cd Hospital.Interop.API
dotnet run
# La API estará disponible en: https://localhost:7000
```

### 2. Frontend (Blazor)
```bash
cd Hospital.Interop.Web
dotnet run
# La aplicación estará disponible en: https://localhost:7001
```

## ?? Configuración de Conexión

### URL del Backend
El frontend está configurado para conectarse al backend en:
```
https://localhost:7000
```

Puedes cambiar esto en el archivo `Program.cs`:
```csharp
var backendUrl = builder.Configuration["BackendUrl"] ?? "https://localhost:7000";
```

O en `wwwroot/appsettings.json`:
```json
{
  "BackendUrl": "https://localhost:7000",
  "ApiSettings": {
    "BackendUrl": "https://localhost:7000"
  }
}
```

## ?? Módulos Disponibles

### 1. Dashboard
- Indicadores clave del sistema
- Próximas citas
- Actividad reciente
- Accesos rápidos a todos los módulos

### 2. Gestión de Pacientes
- Búsqueda de pacientes
- Registro de nuevos pacientes
- Edición de información
- Vista de historial

### 3. Citas Médicas
- Agendar nuevas citas
- Ver citas próximas
- Cambiar estado de citas
- Filtro por departamento

### 4. Laboratorio
- Solicitar pruebas
- Consultar resultados
- Historial de pruebas
- Gestión de muestras

### 5. Facturación
- Crear facturas
- Seguimiento de pagos
- Reportes de facturación
- Gestión de vencimientos

### 6. Departamentos
- Consultar departamentos
- Ver información de Departamentoes
- Contacto directo
- Personal y recursos

## ?? Seguridad

### CORS
El backend está configurado con política de CORS permisiva para desarrollo:
```csharp
builder.Services.AddCors(options =>
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
```

?? **Nota**: En producción, restringe los orígenes permitidos.

### Autenticación
Se incluye soporte para autenticación basada en headers (X-Admin-Key) para acceso administrativo.

## ??? Tecnologías Utilizadas

### Backend
- .NET 8.0
- ASP.NET Core WebAPI
- Entity Framework Core
- InMemoryDatabase

### Frontend
- Blazor WebAssembly
- .NET 8.0
- Bootstrap 5 (CSS Framework)
- Font Awesome (Icons)
- HttpClient para comunicación REST

## ?? Estructura de Carpetas

```
Hospital.Interop.API/
??? Controllers/          # Endpoints de la API
??? Models/              # Entidades y DTOs
??? Services/            # Lógica de negocio
??? Integrations/        # Clientes HTTP
??? Data/                # Contexto de BD
??? Program.cs           # Configuración

Hospital.Interop.Web/
??? Pages/               # Páginas Razor (.razor)
??? Services/            # Servicios HTTP
??? Layout/              # Componentes de layout
??? Models/              # Modelos de datos
??? wwwroot/             # Recursos estáticos
??? Program.cs           # Configuración
```

## ?? Flujo de Datos

```
Usuario
  ?
Página Blazor
  ?
Servicio HTTP (CitasService, PacienteService, etc.)
  ?
HttpClient
  ?
Backend API (https://localhost:7000)
  ?
Controllers
  ?
Services/Repositories
  ?
Base de Datos (InMemory)
```

## ?? Ejemplos de Uso

### Crear una Cita
```csharp
var servicio = new CitasService(httpClient);
var nuevaCita = new CitasService.CitaDTO 
{
    PacienteId = 1,
    Fecha = DateTime.Now.AddDays(1),
    Hora = "10:30",
    Departamento = "Cardiología",
    Estado = "Pendiente"
};
bool resultado = await servicio.CrearCita(nuevaCita);
```

### Obtener Paciente
```csharp
var servicio = new PacienteService(httpClient, configuration);
var paciente = await servicio.ObtenerPacienteSinId(5);
```

## ?? Troubleshooting

### Error: Conexión rechazada al backend
- Asegúrate de que el backend está corriendo en el puerto 7000
- Verifica que CORS está habilitado
- Revisa la configuración de firewall

### Error: Página en blanco
- Abre la consola del navegador (F12) y revisa los errores
- Verifica que los servicios están registrados en `Program.cs`
- Confirma que las rutas de navegación son correctas

### Error: Datos no se cargan
- Verifica que el backend está respondiendo (prueba Swagger)
- Revisa que los endpoints coincidan con la configuración
- Comprueba que los headers de autenticación son correctos

## ?? Documentación API

El backend incluye Swagger/OpenAPI. Accede a:
```
https://localhost:7000/swagger/index.html
```

## ?? Próximos Pasos

1. **Implementar autenticación real** (JWT)
2. **Agregar validación de formularios** más robusta
3. **Crear reportes** en PDF/Excel
4. **Implementar paginación** en listas
5. **Agregar búsqueda y filtros** avanzados
6. **Implementar notificaciones** en tiempo real (SignalR)
7. **Tema oscuro** y personalización de interfaz
8. **Exportación de datos**

## ?? Soporte

Para problemas o preguntas sobre la integración, consulta los logs tanto del backend como del navegador.

---

**Última actualización**: Enero 2024
**Versión**: 1.0.0
