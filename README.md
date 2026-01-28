# Clean Architecture Playground (.NET 8)

Technical playground to practice **Clean Architecture**, CQRS and backend best practices using **.NET 8**.

## Purpose
This repository is intended to demonstrate:
- Clean Architecture principles
- Separation of concerns
- Backend-focused system design
- API-first development

## Structure
- `src/Api` – ASP.NET Core Web API
- `src/Application` – Use cases and orchestration
- `src/Domain` – Business entities and rules
- `src/Infrastructure` – Persistence and integrations

## Run locally
```bash
dotnet build
dotnet run --project src/Api
