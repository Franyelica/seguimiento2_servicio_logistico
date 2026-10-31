# SGCAE - Sistema de Gestión y Control de Acceso de Empleados

Backend con API REST para controlar el acceso de empleados a las instalaciones.
Aplica Clean Architecture, DDD y CQRS, con EF Core sobre SQL Server.

## Arquitectura
Api → Application → Domain ← (Infrastructure implementa Application)

- **Domain:** entidades (Employee, Credential, AccessPoint, AccessLog), objeto de valor Email, enums y reglas de negocio.
- **Application:** comandos y consultas (CQRS), handlers, interfaces de repositorios y servicios de lectura.
- **Infrastructure:** EF Core, configuraciones, repositorios, servicios de lectura y migraciones.
- **Api:** controllers, middleware de errores y Swagger.

## Requisitos
.NET 10 SDK, SQL Server (Express o instancia por defecto), herramienta `dotnet-ef`.

## Configuración
1. Ajusta `ConnectionStrings:Default` en `src/SGCAE.Api/appsettings.json` según tu instancia de SQL Server.
2. Aplica las migraciones:
   `dotnet ef database update -p src/SGCAE.Infrastructure -s src/SGCAE.Api`
3. Ejecuta: `dotnet run --project src/SGCAE.Api` y abre `/swagger`.

## Endpoints
(Tabla de endpoints por requerimiento: RF-01, RF-02, RF-03, RF-04, RF-06)

## Reglas de negocio de RF-03
Un acceso se rechaza (y se registra) si la credencial no existe, está bloqueada o revocada, el empleado está inactivo o el punto de acceso está deshabilitado.

## Evidencias
(Capturas de Swagger, de las tablas en SQL Server y de `dotnet test`)
