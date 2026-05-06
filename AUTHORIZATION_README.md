# Hospital Interop Gateway - Guía de Autorización y DTOs

## Descripción

Se han implementado tres endpoints para gestionar la información de pacientes con diferentes niveles de autorización:

### 1. GET `/api/gateway/paciente/{id}` - Sin ID (Públic)
Retorna los datos básicos del paciente **sin incluir el ID**.

**Propósito:** Para que otros departamentos consulten información del paciente sin acceso al identificador interno.

**Autenticación:** No requiere

**Ejemplo:**
```bash
curl -X GET "https://localhost:7000/api/gateway/paciente/1"
```

**Respuesta:**
```json
{
  "nombre": "Juan Pérez",
  "documento": "1234567890",
  "telefono": "1234567890",
  "direccion": "Calle Principal 123",
  "email": "juan@example.com",
  "fechaNacimiento": "1990-01-15T00:00:00",
  "genero": "M"
}
```

---

### 2. GET `/api/gateway/paciente-admin/{id}` - Con ID (Protegido)
Retorna los datos del paciente **incluyendo el ID interno**.

**Propósito:** Solo para administrador/propietario que necesita el ID para gestiones internas.

**Autenticación:** Requiere header `X-Admin-Key: admin-secret-key`

**Ejemplo:**
```bash
curl -X GET "https://localhost:7000/api/gateway/paciente-admin/1" \
  -H "X-Admin-Key: admin-secret-key"
```

**Respuesta:**
```json
{
  "pacienteId": 1,
  "nombre": "Juan Pérez",
  "documento": "1234567890",
  "telefono": "1234567890",
  "direccion": "Calle Principal 123",
  "email": "juan@example.com",
  "fechaNacimiento": "1990-01-15T00:00:00",
  "genero": "M"
}
```

---

### 3. GET `/api/gateway/paciente-completo/{id}` - Completo (Existente)
Retorna toda la información del paciente desde todos los departamentos.

**Nota:** Este endpoint existente permanece sin cambios para mantener compatibilidad hacia atrás.

---

## Cambios Realizados

### Archivos Creados

1. **`Models/DTOs/PacienteDTO.cs`** - DTOs para datos del paciente con y sin ID
2. **`Models/DTOs/PacienteCompletoDTO.cs`** - DTOs para la respuesta completa
3. **`Services/MapperService.cs`** - Servicio para mapear entre modelos y DTOs
4. **`Attributes/RequireAdminAttribute.cs`** - Atributo para validación de admin (preparado para futuro)

### Archivos Modificados

1. **`Controllers/GatewayController.cs`** - Agregados dos nuevos endpoints
2. **`Program.cs`** - Registrado `MapperService` en inyección de dependencias

---

## Configuración

### Para Producción

En `appsettings.json` (crear si no existe), puedes configurar:

```json
{
  "Authorization": {
    "AdminKey": "tu-clave-admin-segura"
  }
}
```

Luego modificar en `GatewayController.cs` la validación de:
```csharp
adminKey != "admin-secret-key"
```

A:
```csharp
adminKey != builder.Configuration["Authorization:AdminKey"]
```

### Seguridad Recomendada

Para producción, se recomienda implementar:
- **JWT (JSON Web Tokens)** en lugar de headers simples
- **OAuth2** para autenticación entre servicios
- **HTTPS obligatorio**
- **Rate limiting** para evitar abuso

---

## Casos de Uso

### Caso 1: Consulta Interna de Administrador
```bash
# Admin necesita ver el ID para referenciación interna
curl -X GET "https://localhost:7000/api/gateway/paciente-admin/1" \
  -H "X-Admin-Key: admin-secret-key"
```

### Caso 2: Consulta de Otro Departamento
```bash
# Departamento de Laboratorio necesita datos del paciente sin exponer el ID
curl -X GET "https://localhost:7000/api/gateway/paciente/1"
```

### Caso 3: Datos Completos (Existente)
```bash
# Gateway recolecta datos de todos los departamentos (sin cambios)
curl -X GET "https://localhost:7000/api/gateway/paciente-completo/1"
```

---

## Testing en Swagger

1. Abre Swagger en `https://localhost:7000/swagger/index.html`
2. Busca los endpoints:
   - `GET /api/gateway/paciente/{id}` - Sin autenticación
   - `GET /api/gateway/paciente-admin/{id}` - Con header X-Admin-Key
   - `GET /api/gateway/paciente-completo/{id}` - Existente

3. Para probar el endpoint protegido, haz clic en el endpoint y agrega el header `X-Admin-Key: admin-secret-key` en Swagger.

---

## Notas Finales

- Los DTOs sin ID pueden ser vistos por cualquiera, por lo que no exponen información sensible de identificación interna
- El endpoint con ID está protegido por un header simple (debe mejorarse con JWT en producción)
- La arquitectura permite escalar a roles más granulares (Laboratorio, Emergencias, etc.) en el futuro
