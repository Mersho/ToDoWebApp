# ToDoListWebApplication

## Overview
This solution implements a simple ToDo API using ASP.NET Core 8.0, Clean Architecture, CQRS (MediatR), Entity Framework Core, and FluentValidation.

### Projects
- **Domain**: Entities and enums.
- **Application**: CQRS commands, queries, handlers, DTOs, and validation.
- **Infrastructure**: EF Core DbContext and migrations.
- **API**: ASP.NET Core Web API (TODOListWebApplication).
- **Application.UnitTests**: Unit tests for Application layer.
- **API.UnitTests**: Unit tests for API controllers.

## Prerequisites
- .NET 8 SDK
- Postgresql (or modify to SQLite)
- Visual Studio 2022 / VS Code

## Setup
1. Clone the repository.
2. Configure the connection string in `TODOListWebApplication/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ToDoDb;Trusted_Connection=True;"
  }
}
```

## Build & Run
```bash
dotnet build
cd TODOListWebApplication
dotnet ef database update
dotnet run
```
The API will be available at `https://localhost:5001`.

## Testing
```bash
dotnet test
```

## Notes
- Migrations are located in `Infrastructure/Migrations`.
- Swagger UI is enabled in the Development environment.
- Validation errors return 400 Bad Request with detailed error information.