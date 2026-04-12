# FTS — Project Guidelines

Food Tracking System: .NET 10 Web API + Blazor Server UI, orchestrated with Azure Aspire.

## Architecture

Layered architecture with CQRS (MediatR):

```
FTS.Api (Controllers) → FTS.Application (Commands/Queries/Handlers) → FTS.Core (Entities) ← FTS.Infrastructure (EF Core, Azure services)
```

- **FTS.Core** — Domain entities, exceptions, abstractions. No external dependencies.
- **FTS.Application** — CQRS handlers, DTOs, FluentValidation validators, repository interfaces.
- **FTS.Infrastructure** — EF Core (SQL Server), Azure Blob Storage, AI Foundry OCR, JWT auth.
- **FTS.Api** — ASP.NET Core controllers, exception middleware.
- **FTS.App** — Blazor Server UI with MudBlazor components.
- **FTS.AppHost** — Aspire orchestration (SQL via Docker, Azurite emulator, AI Foundry).
- **FTS.ServiceDefaults** — OpenTelemetry, health checks, resilience.

## Build and Test

```bash
dotnet build src/FTS.Api/FTS.Api.csproj        # Build API
dotnet build src/FTS.AppHost/FTS.AppHost.csproj # Build with Aspire (full stack)
dotnet ef database update --project src/FTS.Infrastructure --startup-project src/FTS.Api  # Apply migrations
azd up                                          # Deploy to Azure
```

No test projects exist yet.

## Conventions

### CQRS Pattern

Commands and queries live in `FTS.Application/Handlers/{Feature}/Commands/` and `.../Queries/`. Each command folder contains the command record, handler, and validator:

```
Handlers/Ingredients/Commands/CreateIngredient/
├── CreateIngredientCommand.cs
├── CreateIngredientCommandHandler.cs
└── CreateIngredientValidator.cs
```

- Commands implement `IRequest` or `IRequest<T>` (MediatR).
- Handlers use **primary constructors** for DI: `class Handler(IRepository repo, IValidator<T> validator) : IRequestHandler<T>`.
- Validators use FluentValidation `AbstractValidator<T>`.
- Handlers validate explicitly: call `validator.ValidateAsync()`, throw on failure.

### Entities

- Private constructors + public static `Create()` factory methods.
- Domain exceptions extend `CustomException` (mapped to 400 by `ExceptionMiddleware`).

### Controllers

- Inherit `ControllerBase`, annotated with `[ApiController]` and `[Route("api")]`.
- Use primary constructors: `class Controller(IMediator mediator) : ControllerBase`.
- Delegate all logic to MediatR — no business logic in controllers.
- Authorization via `[Authorize(Roles = Roles.User)]` or `[Authorize(Roles = Roles.Admin)]`.

### Repository Pattern

- Interfaces in `FTS.Application/Abstractions/`.
- Implementations in `FTS.Infrastructure/DAL/Repositories/`.
- Each repository calls `SaveChangesAsync()` per operation.

### Database

- EF Core 10 with SQL Server. DbContext: `FTSDbContext` (extends `IdentityDbContext`).
- Entity configurations use `IEntityTypeConfiguration<T>` in the DAL layer.
- Migrations in `FTS.Infrastructure/DAL/Migrations/`.
- `DatabaseInitializer` (hosted service) auto-migrates and seeds roles on startup.

### Authentication

- JWT Bearer tokens + Google OAuth 2.0.
- ASP.NET Identity with `User` entity and role-based authorization ("Admin", "User").
- Config in `appsettings.json` under `auth` section.

### File Storage

- Azure Blob Storage via `IFileStorage` / `BlobFileStorage`.
- Connection string: `ConnectionStrings:blobs` (injected by Aspire).

### OCR / Vision

- `AiFoundryOcrService` uses GPT-4.1-mini for text extraction and product recognition from images.
- 5-minute request timeout for large files.
