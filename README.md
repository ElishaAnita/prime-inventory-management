# Prime Inventory Management

Cross-platform .NET 8 inventory management system (MAUI client + API) with a clean layered architecture.

Prime Inventory Management is a cross-platform inventory solution built on .NET 8. It separates concerns across `Domain`, `ApplicationCore`, `Infrastructure`, `Persistence` and a .NET MAUI mobile client so the UI stays thin and testable while business rules remain centralized.

Key features
- .NET MAUI mobile client (MAUI / .NET 8) for staff inventory workflows  
- Backend API with Domain-driven design and EF Core persistence  
- Clean layering: `Domain`, `ApplicationCore` (use-cases/DTOs), `Infrastructure`, `Persistence`, `Mobile`  
- Thin Anti‑Corruption Layer (ACL) / mappers so UI never directly uses server internals  
- Pluggable services (barcode scanner, repositories, API clients) registered via DI  
- Prepared for Azure integration (Key Vault, managed identity) and Dynamics 365 (Dataverse) connectors  
- CI/CD and migration-friendly: EF Core migrations live in `Persistence`/`Infrastructure`

## Dynamics 365 (Dataverse) & Azure cloud integration

This project integrates Microsoft Dynamics 365 (Dataverse) and Azure services through the backend API only — the mobile client calls the API and the API is the single, secure integration point with Dynamics/Azure.

High-level architecture
- Mobile → API (HTTP) → Dataverse / Azure (Dataverse calls performed server-side).
- Dataverse-specific logic and mapping live in `Infrastructure`; application-facing DTOs and use-cases remain in `ApplicationCore`.
- Background sync and long-running work run in worker services or Azure Functions, not on the mobile client.

Authentication & secrets
- Register a server app in Azure AD (client credentials) for Dataverse access; record `TenantId` and `ClientId`. Store the client secret in __Azure Key Vault__.
- Prefer Managed Identity for production App Service or Function (avoid client secrets).
- Load Key Vault secrets in the API host at startup (add Key Vault provider to configuration in `Program.cs`).

Configuration locations
- Non-sensitive settings (Dataverse instance URL, scopes, feature flags) go in `PrimeInventoryManagement.Api/appsettings.json` or environment variables (e.g. `Dynamics:InstanceUrl`, `Dynamics:Scope`).
- Secret names (e.g., Key Vault secret name for client secret) stored in the same config, actual secret values only in Key Vault or pipeline secrets.

Server-side integration components (recommended)
- `IDynamicsService` (Infrastructure) — typed connector that encapsulates Dataverse calls and exposes application DTOs.
- `IDynamicsTokenProvider` — token provider abstraction with `ClientCredentialsDynamicsTokenProvider` (dev) and `ManagedIdentityDynamicsTokenProvider` (prod) implementations.
- `DynamicsAuthHandler` — delegating handler that attaches a bearer token to `HttpClient` requests to Dataverse.
- Register a named `HttpClient` (e.g., `"Dataverse"`) via `IHttpClientFactory` and apply Polly policies for resilience.

Resilience & observability
- Use `Polly` for retries, exponential backoff and circuit-breaker around Dataverse calls; respect Dataverse throttling limits.
- Log Dataverse latencies, throttles and errors to Application Insights (server-side) for monitoring and alerting.
- Implement idempotent background sync jobs and surface sync status via the API.

Sync patterns
- Near-real-time: Dataverse change notifications → Azure Event Grid / Service Bus → background worker to process and update domain state.
- Bulk or initial sync: batch workers with checkpointing and idempotency.

CI/CD & infra
- Manage infrastructure as IaC (Bicep/Terraform): App Service / Function, Azure SQL, Key Vault, Service Bus/Event Grid, Application Insights.
- Pipeline tasks should: build, run tests, deploy API, set Managed Identity, grant Key Vault access, run EF migrations, run smoke tests including a harmless Dataverse read.
- Gate Dataverse sync behind feature flags during rollout.

Security & governance
- Grant least-privilege application permissions to Dataverse and request admin consent.
- Never store client secrets in source control; use Key Vault and pipeline secrets.
- Audit data-mutation operations and review access policies regularly.

Minimal acceptance criteria (before promoting to production)
- API can acquire a Dataverse access token and perform a simple read (lookup) successfully.
- ACL mapping tests convert Dataverse DTO → Application DTO reliably.
- Background sync job processes a change and completes idempotently.
- CI pipeline deploys to staging and runs smoke tests that call a safe Dataverse endpoint.
