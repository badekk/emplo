# 🧩 .NET Recruitment Tasks 

This repository contains solutions for six recruitment tasks written in **C# / .NET 8**.
Each task focuses on different aspects of backend development: data structures, LINQ/EF queries, logic implementation, and performance optimization.
---
## 📁 Project Structure

| Folder / Project       | Description                                                                                                        |
| ---------------------- | ------------------------------------------------------------------------------------------------------------------ |
| **emploTask1**         | Solution for **Task 1** – Employee hierarchy structure and supervisor relation logic.                              |
| **emploTask2-5**       | Solutions for **Tasks 2–5** – LINQ / Entity Framework queries, business logic (`VacationService`)                  |
| **emploTask2-5.Tests** | Unit tests                                                                                                         |
| **README (this file)** | Contains explanation and answer for **Task 6** – query optimization techniques in EF Core.                         |
---
## 🧠 Task Overview

### 🧩 Task 1 — *Employee structure builder*

Implemented in `emploTask1`

### 💾 Tasks 2–5 — *LINQ EF, logic & tests*

Implemented in `emploTask2-5`

---
### ⚡ Task 6 — *SQL query optimization (EF Core)*

Answer below 👇

> To minimize the number of SQL queries in Entity Framework, use **Eager Loading (`Include`)** instead of lazy loading, **filter data on the database side**, and use **projections (`Select`)** to fetch only required fields.
> For read-only scenarios, apply **`AsNoTracking()`**, cache rarely changing data, and when needed, use **stored procedures or SQL views** to return complete datasets in a single query.

---

## 🧰 How to Run

```bash
# Build the solution
dotnet build

# Run main project (emploTask1)
dotnet run --project emploTask1

# Chande startup project to (emploTask2-5)
dotnet run --project emploTask2-5

# Run unit tests
dotnet test emploTask2-5.Tests
```
