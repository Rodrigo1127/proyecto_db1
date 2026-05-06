# Stage 1: Build Web App (Blazor WASM)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-web
WORKDIR /src
COPY ["Hospital.Interop.Web/Hospital.Interop.Web.csproj", "Hospital.Interop.Web/"]
RUN dotnet restore "Hospital.Interop.Web/Hospital.Interop.Web.csproj"
COPY . .
WORKDIR "/src/Hospital.Interop.Web"
RUN dotnet publish "Hospital.Interop.Web.csproj" -c Release -o /app/web-publish

# Stage 2: Build API
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-api
WORKDIR /src
COPY ["Hospital.Interop.API/Hospital.Interop.API.csproj", "Hospital.Interop.API/"]
RUN dotnet restore "Hospital.Interop.API/Hospital.Interop.API.csproj"
COPY . .
WORKDIR "/src/Hospital.Interop.API"
RUN dotnet publish "Hospital.Interop.API.csproj" -c Release -o /app/api-publish

# Stage 3: Final Image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy API publish output
COPY --from=build-api /app/api-publish .

# Copy Web publish output to API's wwwroot
# Note: Blazor WASM publish output is in the 'wwwroot' folder of the publish directory
COPY --from=build-web /app/web-publish/wwwroot ./wwwroot

# Configuración para Railway (PORT environment variable)
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Hospital.Interop.API.dll"]

