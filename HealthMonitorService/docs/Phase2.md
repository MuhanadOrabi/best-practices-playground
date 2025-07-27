# 🚀 Phase 2 – Infrastructure Plug-in

In this phase, we expand the HealthMonitorService by replacing fake implementations with **real infrastructure** using:

- ✅ Entity Framework Core for data persistence
- ✅ Logging with Serilog
- ✅ Basic error handling for DB failures

This continues from **Phase 1**, which covered Clean Architecture, configuration, and unit testing.

---

## ✅ What Was Added

### 1. 🔌 Real Database Writer (EF Core + PostgreSQL)

- Implemented `DbHealthCheckResultWriter`, using **EF Core**
- Registered with DI container in `Program.cs`
- Schema managed with **migrations** and applied via CLI:
  ```bash
  dotnet ef migrations add InitSchema \
    --project src/HealthMonitor.Infrastructure \
    --startup-project src/HealthMonitor.Console

  dotnet ef database update \
    --project src/HealthMonitor.Infrastructure \
    --startup-project src/HealthMonitor.Console
  ```

### 2. 🐘 PostgreSQL via Docker Compose

```yaml
db:
  image: postgres:16
  container_name: postgres
  environment:
    POSTGRES_DB: HealthMonitorDb
    POSTGRES_USER: healthmonitor
    POSTGRES_PASSWORD: secret123
  ports:
    - "5432:5432"
  volumes:
    - pgdata:/var/lib/postgresql/data
  networks:
    - healthmonitor-net

volumes:
  pgdata:

networks:
  healthmonitor-net:
    driver: bridge
```

Run with:

```bash
docker compose up -d
```

Ensure connection string in `appsettings.json` matches:

```json
"ConnectionStrings": {
  "Default": "Host=localhost;Port=5432;Database=HealthMonitorDb;Username=healthmonitor;Password=secret123"
}
```

To reset DB:

```bash
docker compose down -v && docker compose up -d
```

---

### 3. ⚙️ Configuration Binding

- Bound custom section `Servers` using:

```csharp
services.Configure<ServerConfig>(context.Configuration);
```

- Read servers from config:

```csharp
var servers = host.Services
  .GetRequiredService<IConfiguration>()
  .GetSection("Servers")
  .Get<List<Server>>();
```

---

### 4. 📋 Error Handling

- Exceptions from DB failures are caught and logged
- Hints included to enable retry resiliency via `EnableRetryOnFailure()` for later phases

---

### 5. 📄 Logging with Serilog

- Switched from default logger to **Serilog**
- Configured in `Program.cs`:

```csharp
hostBuilder.UseSerilog((context, services, config) =>
{
  config.ReadFrom.Configuration(context.Configuration);
});
```

- Controlled via `appsettings.json`:

```json
"Serilog": {
  "MinimumLevel": {
    "Default": "Information",
    "Override": {
      "Microsoft": "Warning",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  },
  "WriteTo": [ { "Name": "Console" } ]
}
```

---

## 🧠 Design Principles Reinforced

- Plug real infra behind abstractions: `IHealthCheckResultWriter`
- Maintain testability and separation via DI
- Respect Clean Architecture boundaries (infrastructure isolated)

---

## 🏁 Tag

Mark the end of this phase:

```bash
git tag -a phase-2-infrastructure -m "Phase 2: Real DB writer, Serilog, and config binding"
git push origin phase-2-infrastructure
```

---

Ready for [🔜 Phase 3](./Phase3.md): Real health checker + integration tests!

