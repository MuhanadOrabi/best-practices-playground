# 🩺 Phase 2: Real Infrastructure (EF Core + Logging)

In this phase, we expand the HealthMonitorService by replacing fake implementations with **real infrastructure** using:

- ✅ Entity Framework Core for data persistence
- ✅ Logging with Serilog
- ✅ Basic error handling for DB failures

This continues from **Phase 1**, which covered Clean Architecture, configuration, and unit testing.

---

## 🎯 Goal

Write real health check results to a relational database using EF Core, and introduce production-friendly logging and error handling.

---

## 🧱 What Was Added

### 🧩 EF Core Integration

- Created `HealthMonitorDbContext` in `HealthMonitor.Infrastructure`
- Added `DbHealthCheckResultWriter` that implements `IHealthCheckResultWriter`
- Updated `HealthCheckResult` entity to include `Id` for EF compatibility

### 🔧 Configuration

- Database connection string moved to `appsettings.json`
- Registered `HealthMonitorDbContext` in DI container

### 🪵 Logging

- Introduced [Serilog](https://serilog.net/) for structured console logging
- Configurable via `appsettings.json`

### 🧯 Error Handling

- `DbHealthCheckResultWriter` wraps DB save calls in `try/catch`
- Failures are logged with context

---

## 🧪 Verifying the Phase

1. Add migration & create DB:

```bash
dotnet ef migrations add Init --project src/HealthMonitor.Infrastructure --startup-project src/HealthMonitor.Console
dotnet ef database update --project src/HealthMonitor.Infrastructure --startup-project src/HealthMonitor.Console
```

2. Run the service:

```bash
dotnet run --project src/HealthMonitor.Console
```

✅ You should see console logs for successful writes. ✅ Results should appear in your SQL database.

---

## 🗃 Sample appsettings.json

```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=HealthMonitorDb;Trusted_Connection=True;"
  },
  "Servers": [
    {
      "Name": "Server A",
      "IpAddress": "10.0.0.1",
      "Type": "iDRAC",
      "Username": "admin",
      "Password": "pass"
    }
  ],
  "Serilog": {
    "MinimumLevel": "Information",
    "WriteTo": [
      { "Name": "Console" }
    ],
    "Enrich": [ "FromLogContext", "WithThreadId" ]
  }
}
```

---

## 📌 Git Tag

```bash
git tag -a phase-2-infrastructure-logging -m "Real DB writer with EF Core, Serilog logging, and error handling"
git push origin phase-2-infrastructure-logging
```

---

## 🎯 Learning Objectives

| Concept                               | Covered |
| ------------------------------------- | ------- |
| EF Core with DI                       | ✅ Yes   |
| appsettings.json + connection strings | ✅ Yes   |
| Real infra implementation             | ✅ Yes   |
| Logging (Serilog)                     | ✅ Yes   |
| Graceful error handling               | ✅ Yes   |

---

Next phase: **Integration Tests** using [Testcontainers](https://github.com/testcontainers/testcontainers-dotnet)

