@echo off
REM Hospital Interop Gateway - Script de Inicio Rápido (Windows)

echo.
echo ?? Hospital Interop Gateway - Iniciando ambos proyectos...
echo.

REM Verificar si dotnet está instalado
where dotnet >nul 2>nul
if %errorlevel% neq 0 (
    echo ? Error: dotnet CLI no está instalado
    echo Por favor instala .NET 8.0 SDK desde: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo ? dotnet CLI encontrado
echo.

echo ?? Iniciando Backend API en otra ventana...
start "Hospital Interop API" cmd /k "cd Hospital.Interop.API && dotnet run"

echo   Esperando a que el Backend esté listo...
timeout /t 5 /nobreak

echo.
echo ?? Iniciando Frontend Blazor WebAssembly en otra ventana...
start "Hospital Interop Web" cmd /k "cd Hospital.Interop.Web && dotnet run"

echo.
echo ? Ambos proyectos iniciando...
echo.
echo ?? URLs disponibles:
echo    ?? Backend API:    https://localhost:7110
echo    ?? Swagger UI:     https://localhost:7110/swagger/index.html
echo    ?? Frontend:       https://localhost:7211
echo    ?? Documentación:  https://localhost:7211/api-docs
echo.
echo ? Abriendo navegador en 5 segundos...
timeout /t 5 /nobreak

REM Abrir navegador
start https://localhost:7211
timeout /t 2 /nobreak
start https://localhost:7110/swagger/index.html

echo.
echo ? Aplicación iniciada exitosamente
echo.
echo Presiona cualquier tecla para cerrar esta ventana...
pause >nul
