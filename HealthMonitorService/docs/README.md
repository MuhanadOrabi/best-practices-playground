# 🩺 HealthMonitorService – Clean Architecture (Phase 1)

This is a reference implementation of a simple Health Monitoring service, designed using Clean Architecture principles and fully unit-tested.

---

## ✅ Project Goal

Check the health of remote servers (e.g., iDRAC) and record the result.

In Phase 1:
- Focus on layering and abstraction
- Use interfaces and dependency injection
- Add unit tests with mocked dependencies
- Load servers from appsettings.json

---

## 📁 Solution Folder Structure

```
HealthMonitorService.sln
├── 00 - Solution Items
│   └── README.md
├── 01 - Core
│   ├── HealthMonitor.Domain/            // Entities and interfaces
│   ├── HealthMonitor.Application/       // Orchestration logic
│   ├── HealthMonitor.Infrastructure/    // Fake checker + console writer
│   └── HealthMonitor.Console/           // Main program + appsettings.json
├── 02 - Tests
│   └── HealthMonitor.Application.UnitTests/ // Unit tests for application logic
```

---

## ✅ What’s Included

- Cleanly layered project structure
- Unit test with xUnit and Moq
- Server list loaded from `appsettings.json`
- Fully async orchestration logic

---

## 🧪 Unit Testing

- `HealthCheckServiceTests` verifies:
    - HealthChecker is called
    - ResultWriter receives expected result

Run tests:
```bash
cd tests/HealthMonitor.Application.Tests.Unit
dotnet test
```

---

## ⚙️ Configuration Binding

- Servers are configured in `HealthMonitor.Console/appsettings.json`
- Bound using `Microsoft.Extensions.Configuration` and `Bind<T>()`

```json
{
  "Servers": [
    {
      "Name": "Server A",
      "IpAddress": "10.0.0.1",
      "Type": "iDRAC",
      "Username": "admin",
      "Password": "pass"
    }
  ]
}
```

---

## 🚀 Run the App

From the root directory:
```bash
dotnet run --project src/HealthMonitor.Console
```

Expected Output:
```
[2025-07-26T...] Server A - Status: OK - Healthy
```

---

## 📌 Git Tag

```bash
git tag -a phase-1-clean-architecture -m "Clean Architecture baseline with mocked dependencies, unit tests, and config-driven execution"
```

---

## 🎯 Learning Objectives

| Concept | Covered |
|---------|---------|
| Clean Architecture | ✅ Yes |
| SOLID (SRP, DIP) | ✅ Yes |
| Configuration | ✅ appsettings.json binding |
| Dependency Injection | ✅ via constructor |
| Testing | ✅ Unit tests with mocks |
| Async Programming | ✅ Yes |

---

Phase 2 will introduce:
- Real iDRAC health checker
- EF Core result writer
- Integration tests using Testcontainers
