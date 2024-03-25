FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Skopiuj pliki .csproj i przywróć zależności
COPY LabAPI.Api/LabAPI.Api.csproj LabAPI.Api/
COPY LabAPI.Application/LabAPI.Application.csproj LabAPI.Application/
COPY LabAPI.Domain/LabAPI.Domain.csproj LabAPI.Domain/
COPY LabAPI.Infrastructure/LabAPI.Infrastructure.csproj LabAPI.Infrastructure/
RUN dotnet restore LabAPI.Api/LabAPI.Api.csproj
RUN dotnet restore LabAPI.Application/LabAPI.Application.csproj
RUN dotnet restore LabAPI.Domain/LabAPI.Domain.csproj
RUN dotnet restore LabAPI.Infrastructure/LabAPI.Infrastructure.csproj

# Skopiuj resztę plików i zbuduj aplikację
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build-env /src/out .

# Skopiuj pliki DLL
COPY --from=build-env /src/LabAPI.Api/bin/Release/net8.0/LabAPI.Api.dll .
COPY --from=build-env /src/LabAPI.Application/bin/Release/net8.0/LabAPI.Application.dll .
COPY --from=build-env /src/LabAPI.Domain/bin/Release/net8.0/LabAPI.Domain.dll .
COPY --from=build-env /src/LabAPI.Infrastructure/bin/Release/net8.0/LabAPI.Infrastructure.dll .

EXPOSE 5000

# Ustaw punkt wejścia dla kontenera
ENTRYPOINT ["dotnet", "LabAPI.Api.dll"]