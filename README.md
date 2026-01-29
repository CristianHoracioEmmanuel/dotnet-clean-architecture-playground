# Clean Architecture Playground (.NET 8)

Backend-focused playground to practice **Clean Architecture**, **CQRS-style use cases**, and API design in **.NET 8**.

## Tech stack
- .NET 8 / ASP.NET Core Web API
- Swagger (Swashbuckle)
- Layered architecture: Api / Application / Domain / Infrastructure

## Architecture
Dependency rule:

Api → Application → Domain  
Infrastructure → (Application, Domain)

- **Domain**: entities + business rules (no frameworks)
- **Application**: use cases + orchestration
- **Infrastructure**: persistence/integrations (placeholder for future)
- **Api**: controllers + HTTP boundary

## Run locally
```bash
dotnet restore
dotnet run --project src/Api/Api.csproj
