FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Hospital.Interop.API.csproj", "./"]
RUN dotnet restore "Hospital.Interop.API.csproj"

COPY . .
RUN dotnet publish "Hospital.Interop.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://0.0.0.0:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "Hospital.Interop.API.dll"]