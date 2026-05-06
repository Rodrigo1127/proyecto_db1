# Hospital Interop Gateway - Guía de Uso

## ?? Descripción General

Hospital Interop API es una solución integrada que combina:
- **Backend API**: ASP.NET Core 8.0 con Swagger/OpenAPI
- **Frontend**: Blazor WebAssembly

## ?? Cómo Iniciar

### Opción 1: Ejecutar ambos proyectos simultáneamente

1. Abre la solución en Visual Studio
2. Haz clic derecho en la **solución** ? **Set Startup Projects...**
3. Selecciona **Multiple startup projects**
4. Asegúrate de que ambos proyectos estén en `Start`:
   - `Hospital.Interop.API`
   - `Hospital.Interop.Web`
5. Presiona `F5` o haz clic en el botón de reproducción

### Opción 2: Ejecutar proyectos por separado

**Backend (API)**:
```powershell
cd Hospital.Interop.API
dotnet run
```
Se ejecutará en: `https://localhost:7110`

**Frontend (Web)**:
```powershell
cd Hospital.Interop.Web
dotnet run
```
Se ejecutará en: `https://localhost:7211`

## ?? Acceder a Swagger

### Desde el navegador:
- Directo: `https://localhost:7110/swagger/index.html`

### Desde la aplicación Blazor:
1. Abre la aplicación en `https://localhost:7211`
2. En la barra lateral, busca la sección **HERRAMIENTAS**
3. Puedes:
   - Hacer clic en **Documentación API** para ver la documentación dentro de Blazor
   - Hacer clic en **Swagger (Nueva Pestaña)** para abrir Swagger en una nueva pestaña del navegador

## ?? Estructura del Proyecto

```
Hospital.Interop.API/
??? Controllers/          # Controladores API (REST endpoints)
??? Services/            # Lógica de negocio
??? Data/                # DbContext y modelos EF Core
??? Models/              # Modelos de datos
??? Integrations/        # Clientes HTTP para servicios externos
??? Middleware/          # Middleware personalizado
??? Program.cs           # Configuración de la aplicación
??? appsettings.json     # Configuración

Hospital.Interop.Web/
??? Pages/               # Páginas Razor (.razor)
??? Layout/              # Componentes de diseño
??? Models/              # Modelos locales
??? Services/            # Servicios de cliente HTTP
??? wwwroot/             # Archivos estáticos
??? Program.cs           # Configuración Blazor WASM
??? appsettings.json     # Configuración del cliente
```

## ?? Puertos y URLs

| Componente | URL | Descripción |
|-----------|-----|-------------|
| Backend API | `https://localhost:7110` | API REST Principal |
| Backend API (HTTP) | `http://localhost:5225` | HTTP alternativo |
| Frontend Blazor | `https://localhost:7211` | Aplicación Web |
| Frontend Blazor (HTTP) | `http://localhost:5237` | HTTP alternativo |
| Swagger UI | `https://localhost:7110/swagger/index.html` | Documentación API interactiva |

## ??? Endpoints Principales

### Pacientes
- `GET /api/pacientes` - Obtener todos los pacientes
- `GET /api/pacientes/{id}` - Obtener paciente por ID
- `POST /api/pacientes` - Crear nuevo paciente
- `PUT /api/pacientes/{id}` - Actualizar paciente
- `DELETE /api/pacientes/{id}` - Eliminar paciente

### Citas
- `GET /api/citas` - Obtener todas las citas
- `GET /api/citas/{id}` - Obtener cita por ID
- `POST /api/citas` - Crear nueva cita
- `PUT /api/citas/{id}` - Actualizar cita
- `DELETE /api/citas/{id}` - Eliminar cita

### Laboratorio
- `GET /api/laboratorio` - Obtener exámenes
- `POST /api/laboratorio` - Crear examen
- `GET /api/resultados-prueba` - Obtener resultados
- `POST /api/solicitudes-prueba` - Crear solicitud

### Facturación
- `GET /api/facturacion` - Obtener facturas
- `POST /api/facturacion` - Crear factura
- `PUT /api/facturacion/{id}` - Actualizar factura
- `DELETE /api/facturacion/{id}` - Eliminar factura

### Departamentos
- `GET /api/departamentos` - Obtener todos los departamentos
- `GET /api/departamentos/{id}` - Obtener departamento por ID
- `POST /api/departamentos` - Crear departamento
- `PUT /api/departamentos/{id}` - Actualizar departamento
- `DELETE /api/departamentos/{id}` - Eliminar departamento

### Gateway (Orquestación)
- `GET /api/gateway/paciente-completo/{id}` - Obtener información completa del paciente (combina datos de múltiples servicios)

## ?? Seguridad

- ? CORS habilitado para permitir comunicación entre frontend y backend
- ?? Considera implementar autenticación (JWT, OAuth2) para producción
- ?? Implementa validación de entrada en todos los endpoints
- ?? Usa HTTPS en producción

## ?? Base de Datos

Actualmente usa **Entity Framework Core In-Memory**:
- Perfecta para desarrollo y pruebas
- Los datos se pierden al reiniciar la aplicación
- Para producción, considera cambiar a SQL Server, PostgreSQL, etc.

## ?? Testing

Los endpoints se pueden probar fácilmente desde Swagger:
1. Abre Swagger: `https://localhost:7110/swagger/index.html`
2. Haz clic en cualquier endpoint para expandirlo
3. Haz clic en "Try it out"
4. Ingresa los parámetros necesarios
5. Haz clic en "Execute"

## ?? Notas de Desarrollo

- La API genera automáticamente una BD en memoria al iniciar
- Los servicios de departamentos externos se pueden configurar en `appsettings.json`
- Todos los servicios están inyectados por dependencia
- Los modelos EF Core están configurados con validaciones y restricciones

## ? Solución de Problemas

### Error de certificado SSL
Si obtienes errores de certificado, ejecuta:
```powershell
dotnet dev-certs https --trust
```

### Puerto en uso
Si el puerto ya está en uso, modifica el puerto en `Properties/launchSettings.json`

### CORS bloqueado
Verifica que `AddCors` esté configurado en `Program.cs`

---

¡Listo! ?? Ahora puedes acceder al Swagger directamente desde tu aplicación Blazor.
