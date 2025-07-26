# 🧐 Best Practices Playground – Learn .NET by Doing

Welcome to the **Best Practices Playground** — a hands-on learning space for junior and mid-level .NET developers to explore real-world architectural and testing practices, one concept at a time.

---

## 🎯 What Is This?

This repository is a collection of small, focused projects and examples, each designed to teach a key software engineering practice — from Clean Architecture and SOLID principles to unit testing, integration testing, configuration management, and more.

Rather than throwing everything into one complex app, we use a **constructed learning approach**:

> Each example grows in *phases*, building your understanding layer by layer.

---

## 🛡️ Structure

Each folder in this repo is a separate example:

```
best-practices-playground/
├── HealthMonitorService/        ← Clean architecture + testing
├── MigrationService/            ← Coming soon
├── ObservabilityExample/        ← Coming soon
└── docs/
```

Each project is structured using best practices (separated by layers) and supports real-world tooling like DI, configuration, and test frameworks.

---

## 📚 How Constructed Learning Works

Each project evolves over time. The key learning milestones are **tagged in Git**.

For example:

```
HealthMonitorService/
├─ phase-1-clean-architecture     ← Clean layering, config, unit tests
├─ phase-2-add-infrastructure     ← Real DB & iDRAC writer
└─ phase-3-integration-tests      ← Testcontainers
```

---

## 🔀 Switching Between Learning Phases

You can explore each stage of the project using Git tags:

### 📑View available tags:

```bash
git tag
```

### 🔀 Checkout a specific phase:

```bash
git checkout tags/phase-1-clean-architecture
```

This lets you see how the code evolves with each concept, without overwhelming complexity upfront.

---

## 🚀 Running & Testing Projects

Each subproject has its own `README.md` with:

* How to run the app
* How to run tests
* What concepts are being demonstrated

Example:

```bash
cd HealthMonitorService

dotnet run --project src/HealthMonitor.Console

dotnet test tests/HealthMonitor.Application.UnitTests
```

---

## 🧰 Topics Covered (Growing Over Time)

| Topic                                  | Status        |
| -------------------------------------- | ------------- |
| Clean Architecture                     | ✅ Implemented |
| SOLID Principles                       | ✅ Applied     |
| Unit Testing (xUnit + Moq)             | ✅ Included    |
| Configuration Binding                  | ✅ Implemented |
| Integration Testing (Testcontainers)   | 🕲️ Planned   |
| Observability (Serilog, OpenTelemetry) | 🕲️ Planned   |

---

## 🙌 Contributing / Adapting

Feel free to fork this repo for your own internal teams, bootcamps, or workshops. It’s designed to be clean, simple, and focused on *understanding by building*.

---

## 🔖 License

MIT License — use freely for teaching and learning.

---

> Made for developers who want to go from “just coding” to **clean, testable, and scalable software engineering** 🚀
