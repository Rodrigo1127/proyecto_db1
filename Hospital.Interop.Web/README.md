# Hospital Interop Web - Frontend en Blazor

## Descripción

Frontend moderno en **Blazor WebAssembly** conectado con el Gateway API del Hospital.

## ?? Características

- ? Consulta de pacientes sin ID (pública)
- ? Consulta administrativo con ID (protegido)
- ? Interfaz responsiva con Bootstrap 5
- ? Manejo de errores y loading states
- ? Integración segura con backend

## ?? Estructura de Carpetas

```
Hospital.Interop.Web/
??? Models/
?   ??? PacienteDTO.cs              # Modelos de datos
??? Services/
?   ??? PacienteService.cs          # Servicio HTTP para API
??? Pages/
?   ??? Home.razor                   # Página de inicio
?   ??? Paciente.razor               # Consulta pública
?   ??? PacienteAdmin.razor          # Consulta admin
??? Layout/
?   ??? NavMenu.razor                # Menú de navegación
??? wwwroot/
?   ??? appsettings.json             # Configuración
??? Program.cs                       # Configuración de servicios
```

## ?? Configuración

### 1. Actualizar URL del Backend

En `wwwroot/appsettings.json`:

```json
{
  "ApiSettings": {
    "BackendUrl": "https://localhost:7000"
  }
}
```

Cambia `localhost:7000` por la URL de tu backend en producción.

### 2. Ejecutar la Aplicación

```bash
cd Hospital.Interop.Web
dotnet run
```

La aplicación estará disponible en `https://localhost:7001` (o el puerto asignado).

## ?? Páginas Disponibles

### 1. Inicio (`/`)
- Pantalla de bienvenida
- Acceso rápido a las funciones principales
- Información del gateway

### 2. Consultar Paciente (`/paciente`)
- Busca pacientes por ID
- Muestra información pública (sin ID)
- Disponible para todos los departamentos

**Datos mostrados:**
- Nombre
- Documento
- Teléfono
- Email
- Dirección
- Fecha de Nacimiento
- Género

### 3. Vista Administrativa (`/paciente-admin`)
- Búsqueda protegida con clave admin
- Muestra información confidencial incluyendo el ID
- Solo para administradores

**Datos adicionales:**
- ?? ID del Paciente (confidencial)

## ?? Seguridad

### Autenticación Admin

Para acceder a la vista administrativa, necesitas:

1. **Clave Administrativa:** `admin-secret-key` (por defecto)

En producción, se recomienda:
- Cambiar la clave en `appsettings.json`
- Implementar JWT en lugar de headers simples
- Usar HTTPS obligatoriamente

### Encriptación

- Las credenciales se envían en headers HTTPS
- No se guardan en el navegador (excepto durante la sesión)
- Se validan en el servidor

## ?? Integración API

### Servicio PacienteService

```csharp
// Obtener paciente sin ID (público)
var paciente = await _pacienteService.ObtenerPacienteSinId(id);

// Obtener paciente con ID (admin)
var paciente = await _pacienteService.ObtenerPacienteConId(id, "tu-clave-admin");
```

### Endpoints Utilizados

| Método | Endpoint | Autenticación | Descripción |
|--------|----------|---------------|-------------|
| GET | `/api/gateway/paciente/{id}` | No | Datos públicos sin ID |
| GET | `/api/gateway/paciente-admin/{id}` | X-Admin-Key | Datos con ID (admin) |
| GET | `/api/gateway/paciente-completo/{id}` | No | Datos completos de todos los departamentos |

## ?? Casos de Uso

### Caso 1: Laboratorio Consulta Paciente
```
1. Navega a /paciente
2. Ingresa ID: 1
3. Ve los datos sin el ID interno
4. Utiliza la información para su operativa
```

### Caso 2: Admin Verifica Información
```
1. Navega a /paciente-admin
2. Ingresa clave: admin-secret-key
3. Ingresa ID: 1
4. Ve todos los datos incluyendo el ID
```

## ?? Dependencias

- Blazor WebAssembly (.NET 8)
- Bootstrap 5 (CSS Framework)
- HttpClient nativo de Blazor

## ?? Troubleshooting

### "No se puede conectar al backend"
- Verifica que el backend esté corriendo en `https://localhost:7000`
- Revisa `appsettings.json` tiene la URL correcta
- Comprueba que CORS esté habilitado en el backend

### "Clave administrativa inválida"
- Verifica que la clave sea exactamente `admin-secret-key`
- En el backend, puedes cambiarla en `Program.cs` o `appsettings.json`

### "Paciente no encontrado"
- Asegúrate de que el ID existe en la base de datos
- Verifica que el backend tenga datos de prueba cargados

## ?? Próximos Pasos

1. **Autenticación JWT**: Implementar JWT en lugar de headers simples
2. **Roles Granulares**: Crear roles específicos por departamento
3. **Reportes**: Generar reportes de consultas de pacientes
4. **Auditoría**: Registrar todas las consultas a información sensible
5. **Caché**: Implementar caché del lado del cliente para consultas frecuentes

## ?? Soporte

Para problemas, revisa:
- Logs del backend en Visual Studio
- Consola del navegador (F12 ? Console)
- Network tab para verificar requests HTTP
