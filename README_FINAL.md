# ?? Hospital Interop Gateway - Tu Solución Completa

## ? ¿Qué se logró?

Integraste **2 proyectos .NET 8** en una solución unificada donde el frontend Blazor WebAssembly puede acceder al Swagger de la API directamente desde la interfaz gráfica.

---

## ?? Acceder a Swagger - 3 Maneras

### 1?? Desde la Página de Documentación (Recomendado)

```
???????????????????????????????????????????
? ?? Hospital Interop Gateway             ?
???????????????????????????????????????????
? Dashboard                               ?
?                                         ?
? GESTIÓN                                 ?
? • Pacientes                             ?
? • Citas                                 ?
? • Laboratorio                           ?
? • Facturación                           ?
? • Departamentos                         ?
?                                         ?
? CONSULTAS                               ?
? • Buscar Paciente                       ?
? • Acceso Admin                          ?
?                                         ?
? ? HERRAMIENTAS                          ?
?   ?? ?? Documentación API  (Haz click)  ?
?   ?? ?? Swagger (Nueva Pestaña)         ?
?                                         ?
? Estado del Gateway: ? Conectado         ?
???????????????????????????????????????????
           ?
      Se abre página
           ?
???????????????????????????????????????????
? API Documentation                       ?
???????????????????????????????????????????
?                                         ?
? Información sobre endpoints...          ?
?                                         ?
? ?? Tabla de endpoints principales       ?
?                                         ?
? [Abrir Swagger UI] ? Haz click          ?
?                                         ?
???????????????????????????????????????????
           ?
    Se abre en nueva pestaña
           ?
https://localhost:7110/swagger/index.html
```

### 2?? Botón Directo a Swagger

```
???????????????????????????????????????????
? ?? Hospital Interop Gateway             ?
?                                         ?
? ? HERRAMIENTAS                          ?
? ?? ?? Swagger (Nueva Pestaña) (Click)  ?
?                                         ?
???????????????????????????????????????????
           ?
    Se abre en nueva pestaña
           ?
https://localhost:7110/swagger/index.html
```

### 3?? URL Directa en el Navegador

```
Escribe en la barra de dirección:
https://localhost:7110/swagger/index.html
```

---

## ?? Pasos para Iniciar

### Opción A: Visual Studio (Recomendado)

```
1. Abre la solución en Visual Studio

2. Haz clic DERECHO en "Solución"
   ?
   Set Startup Projects...

3. Selecciona "Multiple startup projects"
   ?
   Hospital.Interop.API      ? Start
   Hospital.Interop.Web      ? Start
   ?
   OK

4. Presiona F5 (o el botón ?)

5. Se abrirán automáticamente:
   ? Pestaña 1: https://localhost:7110/swagger (API)
   ? Pestaña 2: https://localhost:7211 (Frontend)
```

### Opción B: Terminal/PowerShell

```powershell
# Abre PowerShell y navega a la carpeta del proyecto

# Terminal 1
cd Hospital.Interop.API
dotnet run

# Terminal 2 (en otra ventana)
cd Hospital.Interop.Web
dotnet run

# Resultado:
# - Backend: https://localhost:7110
# - Frontend: https://localhost:7211
```

---

## ?? Vista de Swagger

Una vez abierto, verás:

```
???????????????????????????????????????????????
? Hospital Interop Gateway v1         Docs    ?
?                                             ?
? ?? GET    /api/pacientes                    ?
?    Obtener todos los pacientes              ?
?    [Try it out] [Execute]                  ?
?                                             ?
? ?? POST   /api/pacientes                    ?
?    Crear un nuevo paciente                  ?
?    [Try it out] [Execute]                  ?
?                                             ?
? ?? PUT    /api/pacientes/{id}               ?
?    Actualizar un paciente                   ?
?    [Try it out] [Execute]                  ?
?                                             ?
? ?? DELETE /api/pacientes/{id}               ?
?    Eliminar un paciente                     ?
?    [Try it out] [Execute]                  ?
?                                             ?
? ... Citas, Laboratorio, Facturación...     ?
?                                             ?
???????????????????????????????????????????????
```

---

## ?? Probando un Endpoint en Swagger

```
Ejemplo: Obtener todos los Pacientes

1. Haz clic en: ?? GET /api/pacientes

2. Haz clic en: [Try it out]

3. (No hay parámetros para este endpoint)

4. Haz clic en: [Execute]

5. Ver resultado abajo:
   ? Response code: 200
   ? Response body (JSON con los pacientes)
```

---

## ?? Archivos Principales

```
Hospital.Interop.API/
??? Controllers/              ? Endpoints REST
??? Services/                 ? Lógica de negocio
??? Data/                     ? Base de datos (EF Core)
??? Models/                   ? Modelos de datos
??? Program.cs                ? Configuración principal
??? appsettings.json          ? Configuración

Hospital.Interop.Web/
??? Pages/
?   ??? ApiDocumentation.razor   ? ?? NUEVA: Página de documentación
?   ??? Dashboard.razor
?   ??? Pacientes.razor
?   ??? Citas.razor
?   ??? ... otras páginas
??? Layout/
?   ??? NavMenu.razor        ? ? MODIFICADO: Agregado botones
??? Services/                ? Clientes HTTP
??? Program.cs               ? Configuración Blazor WASM
??? appsettings.json         ? Configuración del cliente
```

---

## ?? Puertos y URLs

| Componente | URL |
|-----------|-----|
| ?? Backend API (HTTPS) | `https://localhost:7110` |
| ?? Backend API (HTTP) | `http://localhost:5225` |
| ?? Swagger UI | `https://localhost:7110/swagger/index.html` |
| ?? Frontend Blazor (HTTPS) | `https://localhost:7211` |
| ?? Frontend Blazor (HTTP) | `http://localhost:5237` |
| ?? Documentación API en Frontend | `https://localhost:7211/api-docs` |

---

## ? Checklist de Verificación

```
Después de iniciar, verifica:

? Se abrieron 2 pestañas del navegador
? Pestaña 1: Swagger carga correctamente
? Pestaña 2: Frontend Blazor carga
? En el menú lateral aparece "HERRAMIENTAS"
? Existe el botón "Documentación API"
? Existe el botón "Swagger (Nueva Pestaña)"
? Puedo hacer clic en ellos sin errores
? Se abre Swagger correctamente
? Puedo expandir y probar endpoints
```

---

## ?? Cambios Realizados

### ? Modificaciones en código:

1. **Hospital.Interop.Web/Layout/NavMenu.razor**
   - ? Agregado `@inject IJSRuntime JS`
   - ? Agregada sección "HERRAMIENTAS" en el menú
   - ? Agregado botón para Documentación API
   - ? Agregado botón para Swagger en nueva pestaña
   - ? Método `OpenSwagger()` que abre URL en nueva pestaña

2. **Hospital.Interop.Web/Pages/ApiDocumentation.razor** (NUEVO)
   - ? Página con tabla de endpoints
   - ? Información del sistema
   - ? Botón para abrir Swagger UI
   - ? Estilos Bootstrap integrados

### ? Documentación Creada:

1. `GETTING_STARTED.md` - Guía de inicio rápido
2. `SWAGGER_GUIDE.md` - Guía visual paso a paso
3. `RUN_BOTH_PROJECTS.md` - Cómo ejecutar ambos proyectos
4. `RESUMEN_CAMBIOS.md` - Resumen completo de cambios

---

## ?? Documentación Adicional

En el repositorio encontrarás:

- ?? **GETTING_STARTED.md** - Guía completa para principiantes
- ?? **SWAGGER_GUIDE.md** - Instrucciones visuales para usar Swagger
- ?? **RUN_BOTH_PROJECTS.md** - Diferentes formas de ejecutar los proyectos
- ?? **RESUMEN_CAMBIOS.md** - Detalles técnicos de todos los cambios

---

## ?? Seguridad

El proyecto ya tiene:
- ? CORS habilitado
- ? Configuración HTTPS lista
- ? Certificados de desarrollo

Para producción:
- ?? Implementa autenticación (JWT/OAuth2)
- ?? Configura CORS más restrictivo
- ?? Valida todas las entradas
- ?? Usa base de datos persistente (SQL Server, PostgreSQL)

---

## ?? Solución de Problemas

### "No se puede conectar a localhost:7110"
? Verifica que el Backend está ejecutándose

### "Error 404 en Swagger"
? Abre directamente: `https://localhost:7110/swagger/index.html`

### "Certificado SSL no válido"
? Ejecuta: `dotnet dev-certs https --trust`

### "Puerto ya está en uso"
? Cambia el puerto en `Properties/launchSettings.json`

---

## ?? ¡Listo!

Tu solución está completamente integrada y funcionando.

Ahora puedes:
? Ejecutar ambos proyectos simultáneamente
? Acceder a Swagger desde el menú de la aplicación
? Explorar toda la API de forma interactiva
? Probar endpoints con datos de prueba

---

**Versión**: 1.0
**Última actualización**: 2024
**Estado**: ? Completado y Funcional

¡Gracias por usar Hospital Interop Gateway! ??
