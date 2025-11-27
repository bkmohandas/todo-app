# ToDo App Backend

A backend service built with ASP.NET Core (.NET 8), Entity Framework Core, and FluentValidation. 
This API powers the ToDoApp frontend, providing task management endpoints, validation and logging.

---

## Overview

- **Purpose:** Provide a robust, maintainable backend for managing tasks.
- **Architecture:** ASP.NET Core minimal hosting, EF Core with SQLite, FluentValidation for input validation, Serilog for logging.
- **Key qualities:** Centralized validation constants, global exception handling, Swagger/OpenAPI documentation.

---

## Tech Stack

- **Framework:** ASP.NET Core (.NET 8)
- **ORM:** Entity Framework Core
- **Validation:** FluentValidation
- **Logging:** Serilog
- **Database:** SQLite (`todo.db` by default)
- **Documentation:** Swagger/OpenAPI

---

## Setup Steps
### Install prerequisites
- .NET 8 SDK
- dotnet ef 
	```bash 
	# CLI Command:
	dotnet tool install --global dotnet-ef --version 8.0.22
	```
- SQLite (bundled, no extra setup required)

### Restore, build, migrate, run
```bash
   cd backend
   dotnet restore
   dotnet build
   dotnet ef database update -p src/ToDoApp.Infrastructure -s src/ToDoApp.Api
   dotnet run --project src/ToDoApp.Api --urls "http://localhost:5000"
```

### Validation Rules (Backend)
- Title: required, max 100 characters
- Description: optional, max 500 characters
- Priority: must be Low, Medium, or High
- Due date: optional, must be in the future

### Test the API
Swagger ```http://localhost:5000/swagger```

### API quick reference
- GET ```/api/tasks``` -> ```TaskDto[]```
- GET ```/api/tasks/{id}``` -> ```TaskDto```
- POST ```/api/tasks (TaskCreateDto)``` -> ```201 Created```
- PUT ```/api/tasks/{id} (TaskUpdateDto)``` -> ```200 OK```
- DELETE ```/api/tasks/{id}``` -> ```204 No Content```
- Errors -> ```{ "error": { "code": string, "message": string } }```

###  Architecture & decisions
- DTOs for all requests/responses.
- Validation via FluentValidation for required fields, lengths, ranges, and future due dates.
- Global error handling middleware with consistent error shape and Serilog logging.
- SQLite + EF Core migrations for easy setup; production would use a cloud database.
- Controllers are thin; business logic in a service. Mapping is centralized.
- React Query handles caching/invalidation; UI shows loading, empty, and error states.

### Trade-offs & future work
- No authentication (MVP). **Next**: JWT + role-based authorization.
- SQLite is simple but not ideal for high concurrency. **Next**: SQL Server with connection pooling.
- Minimal logging and observability (currently logs are only written to the console). **Next**: structured logs with request IDs, health checks, and persistent sinks such as files or a logging service.
- Basic querying. **Next**: pagination, filters (completed, priority, due today), and search.
- Limited tests. **Next**: service/unit tests for validators, integration tests, frontend e2e.
- CORS: default policy allows any origin/method/header. **Next**: tighten policy for production.

### Testing
```bash
cd backend
dotnet test tests/ToDoApp.Tests
```
 
### Notes
- DueDateUtc must be in the future if provided.
- Priority is 0/1/2 -> Low/Medium/High.