# 🧱 Architecture Overview – HealthMonitorService

This document explains the architectural design of the `HealthMonitorService` project, built as a teaching tool for Clean Architecture, SOLID principles, testing practices, and infrastructure layering.

---

## 🛍 Project Structure

The solution is organized into **layers** that reflect Clean Architecture:

```
HealthMonitorService/
├── Domain/            ← Core business models & interfaces
├── Application/       ← Use cases / service orchestration
├── Infrastructure/    ← Real implementations (DB, APIs)
├── Console/           ← Entry point (console app)
└── Tests/             ← Unit & integration tests
```

---

## 📙 Layers Explained

### 1. `Domain` Layer

- Contains business models (`Server`, `HealthCheckResult`)
- Defines contracts (`IHealthChecker`, `IHealthCheckResultWriter`)
- No dependencies on other layers

### 2. `Application` Layer

- Implements business workflows (`HealthCheckService`)
- Uses interfaces defined in `Domain`
- No infrastructure-specific logic

### 3. `Infrastructure` Layer

- Contains real implementations (e.g., EF Core DB writer)
- Depends on EF Core, logging, config, etc.
- Injected via DI, accessed only through interfaces

### 4. `Console` (Entry Point)

- Reads config
- Sets up DI container
- Calls `HealthCheckService`

---

## 📊 Architectural Patterns Used

- **Clean Architecture**: separation of concerns across layers
- **Dependency Inversion Principle**: business logic depends on abstractions, not implementations
- **Configuration Binding**: settings injected via `appsettings.json`
- **Dependency Injection (DI)**: services registered via `HostBuilder`

---

## 🔍 SOLID Principles in Practice

| Principle                 | Example                                                                 |
| ------------------------- | ----------------------------------------------------------------------- |
| **S**ingle Responsibility | `HealthCheckService` only coordinates checking + writing                |
| **O**pen/Closed           | Add new `IHealthChecker` without modifying service                      |
| **L**iskov                | `DbHealthCheckResultWriter` follows `IHealthCheckResultWriter` contract |
| **I**nterface Segregation | Small, focused interfaces (`IHealthChecker`, `IWriter`)                 |
| **D**ependency Inversion  | Console app wires up real dependencies via DI                           |

---

## 🧠 Design Goals

- Easy to test each layer independently
- Gradually replace mocks with real implementations
- Provide clarity through explicit layering
- Teach patterns through real-world examples

---

## ✅ Summary

This architecture is designed to be:

- **Extensible**: New checkers/writers can be added easily
- **Testable**: Mocks can be injected for unit tests
- **Maintainable**: Code is separated by responsibility
- **Scalable**: Will evolve in future phases to support integration testing and real hardware

