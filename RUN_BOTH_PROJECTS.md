# ?? Ejecutar Ambos Proyectos Simultáneamente

## Opción 1: Visual Studio (Recomendado)

### Paso 1: Configurar Multiple Startup Projects

1. **Haz clic derecho en la solución** `Hospital.Interop.API` en el Explorador de Soluciones
2. **Selecciona:** `Properties` o `Set Startup Projects...`
3. **Elige:** `Multiple startup projects`

### Paso 2: Configurar cada proyecto

| Proyecto | Acción |
|----------|--------|
| Hospital.Interop.API | Start |
| Hospital.Interop.Web | Start |

Haz clic en **OK**

### Paso 3: Ejecutar

- Presiona **F5** o haz clic en el botón verde de reproducción (?)
- Espera a que ambas ventanas del navegador se abran automáticamente

### Resultado esperado:
- **Pestaña 1**: API Backend con Swagger (`https://localhost:7110/swagger`)
- **Pestaña 2**: Frontend Blazor (`https://localhost:7211`)

---

## Opción 2: Terminal/PowerShell (Avanzado)

### Abrir 2 terminales:

**Terminal 1 - Backend**:
```powershell
cd Hospital.Interop.API
dotnet run
```

**Terminal 2 - Frontend**:
```powershell
cd Hospital.Interop.Web
dotnet run
```

### Resultado:
- El backend estará disponible en: `https://localhost:7110`
- El frontend estará disponible en: `https://localhost:7211`

---

## Opción 3: Ejecutar un proyecto a la vez

### Solo Backend (para pruebas de API):
```powershell
cd Hospital.Interop.API
dotnet run
```
- Accede a Swagger: `https://localhost:7110/swagger`

### Solo Frontend (si el backend está en otra máquina):
```powershell
cd Hospital.Interop.Web
dotnet run
```
- Accede a la app: `https://localhost:7211`
- Modifica la URL del backend en `appsettings.json` si es necesario

---

## ? Verificación

### Si todo está bien:
1. ? No hay errores en la consola
2. ? Se abrieron 2 pestañas del navegador
3. ? El Swagger carga correctamente
4. ? Puedes ver el menú en el frontend

### Si hay problemas:
- Revisa los logs en la consola de Debug
- Verifica que los puertos no estén en uso
- Ejecuta `dotnet dev-certs https --trust` si hay errores de certificado

---

## ?? Checklist de Inicio Rápido

- [ ] Abre la solución en Visual Studio
- [ ] Configura "Multiple startup projects"
- [ ] Selecciona "Start" para ambos proyectos
- [ ] Presiona F5
- [ ] Espera a que carguen ambas ventanas
- [ ] En el Frontend, busca "HERRAMIENTAS" en el menú
- [ ] Haz clic en "Documentación API" o "Swagger (Nueva Pestaña)"
- [ ] ¡Disfruta del Swagger interactivo! ??

---

## ?? Configuración por defecto

```json
Backend (API):
- Host: https://localhost:7110
- HTTP: http://localhost:5225
- Swagger: /swagger/index.html

Frontend (Web):
- Host: https://localhost:7211
- HTTP: http://localhost:5237
- Backend API URL: https://localhost:7110
```

---

## ?? Notas importantes

- Los certificados SSL se generan automáticamente en desarrollo
- Si te pide confiar en el certificado, hazlo
- La BD está en memoria, los datos se pierden al reiniciar
- CORS ya está configurado para permitir comunicación entre proyectos

¡Listo! Tu proyecto está completamente integrado y listo para usar. ??
