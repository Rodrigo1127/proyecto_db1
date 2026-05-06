FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
# Using the full path since we are building from the repository root
COPY ["Hospital.Interop.API/Hospital.Interop.API.csproj", "Hospital.Interop.API/"]
RUN dotnet restore "Hospital.Interop.API/Hospital.Interop.API.csproj"

# Copy the entire source and publish the app
COPY . .
WORKDIR "/src/Hospital.Interop.API"
RUN dotnet publish "Hospital.Interop.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expose port and set entry point
# Railway uses the PORT environment variable, but .NET 8 respects ASPNETCORE_URLS
ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Hospital.Interop.API.dll"]
