# ?? Guía de Inicio Rápido

## ¿Qué se ha realizado?

Tu proyecto ahora tiene:
- ? **Backend API** completamente funcional con endpoints para gestionar pacientes, citas, laboratorio y facturación
- ? **Frontend Blazor** con interfaz gráfica moderna y profesional
- ? **Conexión establecida** entre frontend y backend
- ? **CORS configurado** para permitir comunicación entre aplicaciones
- ? **7 nuevas páginas** Razor con interfaces completas
- ? **4 servicios HTTP** para consumir la API

## ?? Interfaz Gráfica Disponible

### Página de Inicio (`/`)
Presenta las 6 módulos principales de la aplicación con botones de acceso rápido y tema visual atractivo.

### Dashboard (`/dashboard`)
Panel de control con:
- Indicadores clave (Total Pacientes, Citas Hoy, Pruebas Pendientes, Facturas Vencidas)
- Próximas citas programadas
- Actividad reciente del sistema
- Botones de acceso rápido

### Gestión de Pacientes (`/pacientes`)
- Formulario para registrar nuevos pacientes
- Tabla con lista de pacientes
- Campos: ID, Nombre, Cédula, Email, Teléfono

### Citas Médicas (`/citas`)
- Formulario para agendar citas
- Selección de departamento
- Vista de citas próximas con tarjetas por estado
- Estados: Confirmada, Pendiente, Cancelada

### Laboratorio (`/laboratorio`)
- Dos pestañas: Solicitudes y Resultados
- Formulario para crear solicitudes de prueba
- Lista de solicitudes y resultados
- Tipos de prueba: Hemograma, Glucosa, Colesterol, Uroanálisis

### Facturación (`/facturacion`)
- Estadísticas: Total Facturado, Pagadas, Pendientes, Vencidas
- Formulario para crear facturas
- Tabla de facturas con detalles
- Búsqueda de facturas

### Departamentos (`/departamentos`)
- Tarjetas visuales de departamentos
- Información de Departamentoes
- Tabla detallada con personal y contactos

## ?? Pasos para Ejecutar

### Paso 1: Terminal para el Backend
```bash
# Navega a la carpeta del API
cd Hospital.Interop.API

# Ejecuta el backend
dotnet run

# Deberías ver algo como:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7000
#       Now listening on: http://localhost:5000
```

**Verifica que el backend está funcionando:**
- Abre en navegador: `https://localhost:7000/swagger`
- Deberías ver la documentación de Swagger

### Paso 2: Segunda Terminal para el Frontend
```bash
# Navega a la carpeta del Web
cd Hospital.Interop.Web

# Ejecuta el frontend
dotnet run

# Deberías ver algo como:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: https://localhost:7001
#       Now listening on: http://localhost:3000
```

**Accede a la aplicación:**
- Abre en navegador: `https://localhost:7001`
- Deberías ver la página de inicio con los 6 módulos

## ?? Lo que Verás

Una aplicación web moderna con:

1. **Barra de navegación superior** con logo "Hospital Interop Gateway"
2. **Menú lateral** con opciones de navegación
3. **Página principal** con tarjetas de acceso a módulos
4. **Interfaz responsiva** que se adapta a diferentes tamaños de pantalla
5. **Iconos profesionales** de Font Awesome
6. **Colores coordenados** con Bootstrap 5

## ?? Rutas Disponibles

| Ruta | Nombre | Descripción |
|------|--------|-------------|
| `/` | Inicio | Página principal |
| `/dashboard` | Panel Control | Indicadores y accesos rápidos |
| `/pacientes` | Pacientes | Gestión de pacientes |
| `/citas` | Citas | Agendamiento de citas |
| `/laboratorio` | Laboratorio | Solicitudes y resultados |
| `/facturacion` | Facturación | Gestión de facturas |
| `/departamentos` | Departamentos | Información de departamentos |
| `/paciente` | Buscar Paciente | Búsqueda pública |
| `/paciente-admin` | Admin | Acceso administrativo |
| `/counter` | Contador | Página de ejemplo |
| `/weather` | Clima | Página de ejemplo |

## ?? Endpoints de la API Disponibles

El backend expone estos endpoints principales:

### Pacientes
- `GET /api/pacientes` - Obtener todos
- `GET /api/pacientes/{id}` - Obtener por ID
- `POST /api/pacientes` - Crear nuevo
- `PUT /api/pacientes/{id}` - Actualizar
- `DELETE /api/pacientes/{id}` - Eliminar

### Citas
- `GET /api/citas` - Obtener todas
- `POST /api/citas` - Crear cita
- `PUT /api/citas/{id}` - Actualizar cita
- `DELETE /api/citas/{id}` - Eliminar cita

### Laboratorio
- `GET /api/solicitudes-prueba` - Obtener solicitudes
- `GET /api/resultados-prueba` - Obtener resultados
- `POST /api/solicitudes-prueba` - Crear solicitud
- `POST /api/resultados-prueba` - Registrar resultado

### Facturación
- `GET /api/facturacion` - Obtener facturas
- `POST /api/facturacion` - Crear factura
- `DELETE /api/facturacion/{id}` - Eliminar factura

## ?? Notas Importantes

1. **Base de Datos**: Usa InMemoryDatabase, los datos se pierden al reiniciar
2. **Puertos**: Asegúrate de que los puertos 7000 y 7001 estén disponibles
3. **HTTPS**: En desarrollo, .NET usa certificados autofirmados
4. **CORS**: Está permitido desde cualquier origen (cambiar en producción)

## ?? Personalización

### Cambiar URL del Backend
Si el backend está en otro puerto, edita:

**Hospital.Interop.Web/Program.cs**:
```csharp
var backendUrl = builder.Configuration["BackendUrl"] ?? "https://localhost:7000";
//                                                        ^^^^^^^^^^^^^^^^^^^^^^
//                                                        Cambia esta URL
```

### Cambiar Tema de Colores
Las páginas usan Bootstrap 5. Los colores se definen en las clases:
- `bg-primary` - Azul
- `bg-success` - Verde
- `bg-info` - Cyan
- `bg-warning` - Amarillo
- `bg-danger` - Rojo

## ?? Solución de Problemas

### "No se puede conectar al backend"
1. Verifica que el backend está corriendo en terminal 1
2. Revisa que está en `https://localhost:7000`
3. Intenta acceder a `https://localhost:7000/swagger`

### "Página en blanco o con errores"
1. Abre la consola del navegador (F12)
2. Busca errores rojos
3. Verifica que están registrados los servicios en `Program.cs`

### "Los datos no se guardan"
Los datos están en una base de datos en memoria. Se pierden al reiniciar el backend. Para persistencia, necesitas:
1. Cambiar de InMemoryDatabase a SQL Server o PostgreSQL
2. Crear migraciones de Entity Framework
3. Configurar la conexión en `appsettings.json`

## ?? Próximos Pasos Sugeridos

1. **Conectar a base de datos real**
   - Cambiar InMemoryDatabase por SQL Server
   - Agregar Entity Framework migrations

2. **Agregar autenticación**
   - Implementar JWT tokens
   - Agregar login/logout

3. **Validaciones**
   - Data Annotations en modelos
   - Validación en frontend

4. **Reportes**
   - Exportar a PDF
   - Exportar a Excel

5. **Mejoras UI**
   - Tema oscuro
   - Gráficos de datos
   - Más animaciones

## ? Checklist de Verificación

- [ ] Backend corriendo en `https://localhost:7000`
- [ ] Frontend corriendo en `https://localhost:7001`
- [ ] Swagger accesible en `https://localhost:7000/swagger`
- [ ] Página de inicio carga correctamente
- [ ] Puedo navegar entre módulos
- [ ] Los formularios se muestran
- [ ] La interfaz es responsiva

## ?? ¿Necesitas Ayuda?

Revisa los logs en la consola de ambas terminales para entender qué está pasando.

---

**¡Ya tienes una aplicación de gestión hospitalaria completamente funcional!** ??
