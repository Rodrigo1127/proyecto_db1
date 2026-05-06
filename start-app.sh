#!/bin/bash
# Hospital Interop Gateway - Script de Inicio Rápido (Linux/Mac)

echo "?? Hospital Interop Gateway - Iniciando ambos proyectos..."
echo ""

# Verificar si dotnet está instalado
if ! command -v dotnet &> /dev/null; then
    echo "? Error: dotnet CLI no está instalado"
    echo "Por favor instala .NET 8.0 SDK desde: https://dotnet.microsoft.com/download"
    exit 1
fi

echo "? dotnet CLI encontrado: $(dotnet --version)"
echo ""

# Crear dos procesos en paralelo
echo "?? Iniciando Backend API..."
cd Hospital.Interop.API
dotnet run &
BACKEND_PID=$!
echo "   PID del Backend: $BACKEND_PID"
echo ""

# Esperar un poco para que el backend esté listo
sleep 5

echo "?? Iniciando Frontend Blazor WebAssembly..."
cd ../Hospital.Interop.Web
dotnet run &
FRONTEND_PID=$!
echo "   PID del Frontend: $FRONTEND_PID"
echo ""

echo "? Ambos proyectos iniciando..."
echo ""
echo "?? URLs disponibles:"
echo "   ?? Backend API:    https://localhost:7110"
echo "   ?? Swagger UI:     https://localhost:7110/swagger/index.html"
echo "   ?? Frontend:       https://localhost:7211"
echo "   ?? Documentación:  https://localhost:7211/api-docs"
echo ""
echo "? Abriendo navegador en 3 segundos..."
sleep 3

# Abrir navegador (funciona en macOS)
if [[ "$OSTYPE" == "darwin"* ]]; then
    open https://localhost:7211
    sleep 2
    open https://localhost:7110/swagger/index.html
elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
    # Para Linux, intenta con xdg-open
    xdg-open https://localhost:7211 &
    sleep 2
    xdg-open https://localhost:7110/swagger/index.html &
fi

echo ""
echo "? Aplicación iniciada exitosamente"
echo ""
echo "Para detener la aplicación, presiona Ctrl+C"
echo ""
echo "Esperando procesos..."
wait
