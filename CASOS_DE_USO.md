# ?? Casos de Uso y Ejemplos de Uso

## 1. Gestión de Pacientes

### Caso de Uso: Registrar Nuevo Paciente

**Ubicación**: `/pacientes`

**Pasos**:
1. Haz clic en "Nuevo Paciente"
2. Completa el formulario:
   - Nombre
   - Apellido
   - Cédula
   - Email
   - Teléfono
   - Fecha de Nacimiento
3. Haz clic en "Guardar"

**Datos Ejemplares**:
```
Nombre: Juan
Apellido: Pérez García
Cédula: 1234567890
Email: juan.perez@example.com
Teléfono: +34 912 345 678
Fecha de Nacimiento: 1990-05-15
```

**Lo que sucede en backend**:
```
POST /api/pacientes
{
  "nombre": "Juan",
  "apellido": "Pérez García",
  "cedula": "1234567890",
  "email": "juan.perez@example.com",
  "telefono": "+34 912 345 678",
  "fechaNacimiento": "1990-05-15T00:00:00"
}
? Retorna 201 Created
? Se almacena en base de datos
```

### Caso de Uso: Buscar Paciente

**Ubicación**: `/pacientes` ? Tabla

**Pasos**:
1. Abre la página de Pacientes
2. Busca en la tabla
3. Haz clic en una fila para ver detalles

---

## 2. Gestión de Citas

### Caso de Uso: Agendar Cita Médica

**Ubicación**: `/citas`

**Pasos**:
1. Haz clic en "Nueva Cita"
2. Completa:
   - Selecciona paciente
   - Elige departamento
   - Establece fecha
   - Elige hora
   - Agrega observaciones (opcional)
3. Haz clic en "Guardar Cita"

**Ejemplo de Datos**:
```
Paciente: Juan Pérez
Departamento: Cardiología
Fecha: 2024-01-20
Hora: 10:30
Observaciones: Dolor en el pecho desde hace 3 días
```

**Estados de Cita**:
- ?? **Confirmada** - Cita confirmada, paciente avisado
- ?? **Pendiente** - Esperando confirmación
- ?? **Cancelada** - Cita cancelada

### Caso de Uso: Ver Citas Próximas

**Ubicación**: `/citas` ? Sección inferior

**Información Visible**:
- Nombre del paciente
- Departamento
- Fecha y hora
- Estado actual

---

## 3. Laboratorio

### Caso de Uso: Solicitar Análisis

**Ubicación**: `/laboratorio` ? Pestaña "Solicitudes"

**Pasos**:
1. Haz clic en "Nueva Solicitud"
2. Completa:
   - Paciente
   - Tipo de prueba
   - Observaciones
3. Envía solicitud

**Tipos de Prueba Disponibles**:
- Hemograma Completo (10 - 15 días)
- Glucosa (1 - 2 días)
- Colesterol Total (2 - 3 días)
- Uroanálisis (1 día)

**Ejemplo**:
```
Paciente: María García
Prueba: Hemograma Completo
Observaciones: Sospecha de anemia
Fecha Solicitud: 2024-01-15
Estado: En procesamiento
```

### Caso de Uso: Consultar Resultados

**Ubicación**: `/laboratorio` ? Pestaña "Resultados"

**Información en Resultados**:
- ID del resultado
- Paciente
- Tipo de prueba
- Fecha de resultado
- Interpretación

**Estados de Resultado**:
- ? Pendiente - Aún procesando
- ? Completado - Listo para ver
- ?? Anormal - Requiere atención médica

---

## 4. Facturación

### Caso de Uso: Crear Factura

**Ubicación**: `/facturacion`

**Pasos**:
1. Haz clic en "Nueva Factura"
2. Completa:
   - Paciente
   - Monto
   - Concepto
   - Fecha vencimiento
3. Crea la factura

**Conceptos Disponibles**:
- Consulta Médica
- Laboratorio
- Cirugía
- Hospitalización

**Ejemplo**:
```
Paciente: Carlos López
Monto: $1,200.00
Concepto: Cirugía
Vencimiento: 2024-02-20
```

### Caso de Uso: Monitorear Pagos

**Ubicación**: `/facturacion` ? Tabla

**Estados de Factura**:
- ?? **Pagada** - Pago completado
- ?? **Pendiente** - Esperando pago
- ?? **Vencida** - Pasó la fecha de vencimiento

**Estadísticas Mostradas**:
- Total Facturado: $125,430.00
- Facturas Pagadas: 18
- Pendientes: 5
- Vencidas: 2

---

## 5. Dashboard

### Caso de Uso: Monitorear Sistema

**Ubicación**: `/dashboard`

**Indicadores Clave (KPIs)**:

#### 1. Total Pacientes
```
Número: 245
Descripción: Pacientes registrados en el sistema
Acción: Haz clic para ver lista completa
```

#### 2. Citas Hoy
```
Número: 12
Descripción: Citas programadas para hoy
Acción: Haz clic para gestionar
```

#### 3. Pruebas Pendientes
```
Número: 8
Descripción: Análisis esperando resultados
Acción: Haz clic para ver solicitudes
```

#### 4. Facturas Vencidas
```
Número: 3
Descripción: Facturas que requieren atención
Acción: Haz clic para cobrar
```

### Casos de Uso del Dashboard

**1. Inicio de Día**
- Revisar citas de hoy
- Checar actividad reciente
- Identificar facturas vencidas

**2. Reportes Rápidos**
- Ver tendencias de pacientes
- Revisar actividad del sistema
- Identificar problemas

**3. Acceso Rápido**
- Desde el dashboard, accede a cualquier módulo
- Botones grandes y claros
- Shortcuts para acciones frecuentes

---

## 6. Departamentos

### Caso de Uso: Consultar Información de Departamento

**Ubicación**: `/departamentos`

**Departamentos Disponibles**:

| Código | Nombre | Departamento | Personal |
|--------|--------|--------------|----------|
| 01 | Cardiología | Enfermedades del corazón | 5 médicos |
| 02 | Oftalmología | Enfermedades de los ojos | 3 médicos |
| 03 | Pediatría | Medicina infantil | 4 médicos |
| 04 | Laboratorio Clínico | Análisis clínicos | 8 técnicos |
| 05 | Neumología | Enfermedades pulmonares | 3 médicos |
| 06 | Cirugía | Procedimientos quirúrgicos | 6 médicos |
| 07 | Farmacia | Medicamentos e insumos | 6 farmacéuticos |
| 08 | Hospitalización | Internación de pacientes | 20 camas |

**Información por Departamento**:
- Descripción de servicios
- Cantidad de personal
- Número de pacientes activos
- Extensión telefónica
- Ubicación

---

## 7. Búsqueda Pública

### Caso de Uso: Acceso Público a Información de Paciente

**Ubicación**: `/paciente`

**Información Visible** (sin permisos admin):
- Nombre
- Cédula/Documento
- Teléfono
- Email
- Dirección
- Fecha de Nacimiento

**Información Oculta** (requiere admin):
- ID del paciente (número interno)
- Historial de citas
- Resultados de laboratorio
- Historial de facturas

**Ejemplo de Búsqueda**:
```
Ingresa: 1234567890 (cédula)
?
Retorna:
  Nombre: Juan Pérez García
  Email: juan.perez@example.com
  Teléfono: +34 912 345 678
  Fecha Nacimiento: 15/05/1990
```

---

## 8. Acceso Administrativo

### Caso de Uso: Acceso a Información Sensible

**Ubicación**: `/paciente-admin`

**Permisos Especiales**:
- Ver ID del paciente
- Acceso a historial completo
- Modificar información sensible
- Generar reportes

**Requisito**:
- Header `X-Admin-Key: admin-secret-key`

---

## ?? Ejemplos de API Directa

Si deseas llamar a la API directamente (usando Postman, curl, etc.):

### GET - Obtener Todos los Pacientes
```bash
curl -X GET "https://localhost:7000/api/pacientes" \
  -H "Content-Type: application/json"
```

### POST - Crear Paciente
```bash
curl -X POST "https://localhost:7000/api/pacientes" \
  -H "Content-Type: application/json" \
  -d '{
    "nombre": "Juan",
    "apellido": "Pérez",
    "cedula": "1234567890",
    "email": "juan@example.com",
    "telefono": "+34 912 345 678",
    "fechaNacimiento": "1990-05-15T00:00:00"
  }'
```

### GET - Obtener Paciente por ID
```bash
curl -X GET "https://localhost:7000/api/pacientes/1" \
  -H "Content-Type: application/json"
```

### PUT - Actualizar Paciente
```bash
curl -X PUT "https://localhost:7000/api/pacientes/1" \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "nombre": "Juan",
    "apellido": "Pérez García",
    ...
  }'
```

### DELETE - Eliminar Paciente
```bash
curl -X DELETE "https://localhost:7000/api/pacientes/1"
```

---

## ?? Autenticación (Futura)

### Ejemplo de Header Admin
```
Headers:
  Content-Type: application/json
  X-Admin-Key: admin-secret-key
  Authorization: Bearer <token_jwt>
```

---

## ?? Flujo Completo: Ejemplo Real

### Escenario: Paciente con Sospecha de Diabetes

**Día 1: Consulta Inicial**
1. Paciente llega al hospital
2. Administrativo registra paciente en `/pacientes`
3. Se crea cita en `/citas` para endocrinología
4. Cita se muestra en dashboard

**Día 2: Cita**
1. Médico ve cita en `/citas`
2. Solicita análisis en `/laboratorio`
3. Se crea solicitud de "Glucosa"
4. Aparece en pruebas pendientes

**Día 3: Análisis**
1. Laboratorio procesa muestra
2. Ingresa resultado en `/laboratorio`
3. Médico ve resultado "Glucosa: 180 mg/dL - ANORMAL"

**Día 4: Facturación**
1. Administrativo crea factura en `/facturacion`
2. Concepto: "Consulta + Análisis"
3. Paciente recibe factura

**Día 5+: Seguimiento**
1. Ver en dashboard todas las acciones realizadas
2. Próxima cita ya está agendada
3. Medicamentos en `/departamentos` ? Farmacia

---

## ?? Tips de Uso

### 1. Navegar Rápidamente
- Usa el menú lateral para cambiar entre módulos
- Los botones del dashboard son accesos directos

### 2. Búsqueda Eficiente
- Usa cédula/documento para búsquedas precisas
- Los filtros se aplican en tiempo real

### 3. Gestión de Errores
- Si algo falla, mira la consola del navegador (F12)
- Verifica que el backend está corriendo

### 4. Datos de Prueba
- Usa los ejemplos de este documento
- Los datos se pierden al reiniciar (InMemory DB)

### 5. Mejores Prácticas
- Completa todos los campos obligatorios
- Revisa dos veces antes de confirmar
- Usa formatos correctos (email, teléfono)
- Guarda cópias de reportes importantes

---

## ?? Preguntas Frecuentes

**P: ¿Se guardan los datos?**
R: Solo mientras el backend está corriendo. InMemoryDatabase no persiste.

**P: ¿Puedo ver datos de otros usuarios?**
R: En desarrollo, todos ven todo. En producción, agregar permisos.

**P: ¿Cómo cambiar la hora de una cita?**
R: Edita directamente en la tabla o usa el formulario de cita.

**P: ¿Qué pasa si cierro el navegador?**
R: Todos los datos se mantienen (servidor no se afecta).

**P: ¿Puedo descargar reportes?**
R: No en esta versión. Pendiente para futuras mejoras.

---

**Versión**: 1.0.0  
**Última actualización**: Enero 2024
