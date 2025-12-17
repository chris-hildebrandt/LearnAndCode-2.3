# Learn and Code: Course & Repository Master Reference

## 1. Executive Summary

**Course Name:** Learn and Code (In Time Tec)  
**Based On:** "Clean Code" (Bob Martin), SOLID Principles, In Time Tec Quality Manifesto  
**Target Audience:** New developers (apprentices), mid-level devs upskilling in .NET/Clean Code.  
**Repository Purpose:** A hands-on learning environment consisting of a theoretical curriculum and a practical application (`TaskFlowAPI`) that evolves from a "messy" state to a robust, clean architecture over 23 weeks.

This document is the **single master reference** for AI agents and humans to understand the curriculum structure, the training application, and the non-negotiable course constraints.

---

## 2. Quickstart & Commands

**Build & Test:**
```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

**Run API:**
```bash
dotnet run --project TaskFlowAPI
# Swagger UI available at: /swagger
```

**Database Setup (SQLite):**
```bash
dotnet tool install --global dotnet-ef
cd TaskFlowAPI
dotnet ef database update
```

---

## 3. Course Constraints & Evaluation Rubric

**Crucial:** This is a *learning* repo. Changes must optimize for student understanding, not just code perfection.

### Non-Negotiable Constraints
1.  **Tightly Coupled to Learning:** Changes must directly exercise the week's specific principle (e.g., Week 2 is *only* about Naming, don't fix Architecture yet).
2.  **Accessible but Non-Trivial:** Scaffolding should guide the student (compiler errors, broken tests) but **never** provide copy-paste solutions.
3.  **Matters to TaskFlowAPI:** Assignments must result in visible changes (Swagger behavior, passing tests, error messages). No busywork.

### Agent Behavioral Guidelines
-   **DO NOT** automatically fix "code smells" (like bad variable names `svc`, `dt`) unless the user is specifically working on that Week's assignment.
-   **DO NOT** fill in `NotImplementedException` or `TODO` blocks proactively. These are student homework.
-   **DO** point out these issues if asked to "review" or "audit," framing them as learning opportunities.
-   **DO** check the "Target Week" before suggesting advanced patterns (e.g., don't suggest MediatR in Week 2).

---

## 4. Repository Structure & Hotspots

### Curriculum ("Source of Truth")
-   `Course-Materials/Weekly-Modules/`: The core progression (`week-01` to `week-23`).
-   `Course-Materials/SETUP.md`: Environment setup.
-   `docs/code-smells-catalog.md`: **Intentional** code smells placed for Week 18.

### TaskFlowAPI (The "Lab")
A .NET 8 Web API designed to teach boundaries.
-   **Controllers** (`TaskFlowAPI/Controllers/`): HTTP concerns only.
    -   *Note:* `ReportsController.cs` intentionally contains long methods and async anti-patterns (fixed in Week 4 & 22).
-   **Services** (`TaskFlowAPI/Services/`): Business logic.
    -   *Note:* `TaskService.cs` is the main orchestration point.
-   **Repositories** (`TaskFlowAPI/Repositories/`): EF Core data access.
    -   *Note:* `TaskRepository.cs` contains `NotImplementedException` stubs for Week 8.
-   **Entities** (`TaskFlowAPI/Entities/`): Domain models.

---

## 5. Curriculum Map & File Targets

This index maps learning objectives to the specific files students must modify.

### Phase 1: Foundations (Weeks 1-5)
| Week | Focus | Primary Files to Touch | Outcome |
| :--- | :--- | :--- | :--- |
| **01** | Intro & Quality | `SETUP.md`, `docs/architecture-diagrams.md` | Dev env running, Swagger accessible. |
| **02** | Meaningful Names | `TasksController.cs`, `ITaskService.cs`, `TaskService.cs` | No `svc`, `dt`, `s` variables. Clear intent. |
| **03** | Comments | Same as Week 2 | Delete "what" comments, keep "why" docs. |
| **04** | Functions | `ReportsController.cs` | Extract methods from `GenerateProjectSummaryReport`. |
| **05** | AI Tools | `Examples/Week-05-AI-Assignment.md` | Compare AI refactor vs Human refactor. |

### Phase 2: Architecture (Weeks 6-10)
| Week | Focus | Primary Files to Touch | Outcome |
| :--- | :--- | :--- | :--- |
| **06** | Git Workflow | N/A | Branching/PR practice. |
| **07** | Encapsulation | `TaskEntity.cs` | Protect invariants (no empty titles, valid priority). |
| **08** | Repository Pattern | `TaskRepository.cs`, `TaskFlowDbContext.cs` | Implement EF Core methods (`AsNoTracking`, `Include`). |
| **09** | Services & DTOs | `TaskService.cs`, `DTOs/*` | Service returns DTOs, not Entities. |
| **10** | Error/Validation | `Validators/*`, `ExceptionMiddleware` | 400 Bad Request on invalid input, not 500. |

### Phase 3: SOLID (Weeks 11-15)
| Week | Focus | Primary Files to Touch | Outcome |
| :--- | :--- | :--- | :--- |
| **11** | SRP | `TaskService.cs`, new `TaskMapper`, `TaskBusinessRules` | Extract logic out of giant Service class. |
| **12** | OCP (Strategy) | `Services/Tasks/Filters/*`, `TasksController` | Add filters without modifying existing logic. |
| **13** | LSP | `FakeTaskRepository`, Tests | Contract tests ensure Fake acts like Real repo. |
| **14** | ISP | `ITaskRepository` | Split into `ITaskReader` / `ITaskWriter`. |
| **15** | DIP | `TaskService`, `ISystemClock` | Inject time dependencies for testability. |

### Phase 4: Quality & Advanced (Weeks 16-23)
| Week | Focus | Primary Files to Touch | Outcome |
| :--- | :--- | :--- | :--- |
| **16** | File Org | Namespace restructuring | Better folder structure. |
| **17** | TDD | `TaskFlowAPI.Tests/`, `TaskService` | TDD `CompleteTaskAsync`. Replace skipped tests. |
| **18** | Refactoring | `TaskHelper.cs`, `ReportsController` | Fix smells from `code-smells-catalog.md`. |
| **19** | Design Patterns | Factory Pattern | Context-aware object creation. |
| **21** | API Design | `TasksController` | Pagination, Versioning. |
| **22** | Async Best Practices | `ReportsController.cs` | Remove `.Result`, `Task.Run`, fire-and-forget. |

---

## 6. Known State Quirks (Do Not Fix Prematurely)

1.  **Broken Tests:** The `TaskFlowAPI.Tests` project contains skipped tests. This is intentional until Week 17.
2.  **NotImplementedExceptions:** `TaskRepository` and `TaskService` have these to block functionality until the student implements them.
3.  **ReportsController:** Intentionally messy and contains async anti-patterns for Week 4 and Week 22 assignments.
4.  **TaskHelper.cs:** A "Code Smell Playground" containing bad static methods.

---

## 7. How to Verify Changes
All changes should be verified by:
1.  **Build:** `dotnet build` (Must pass).
2.  **Test:** `dotnet test` (Should pass for implemented weeks, ignore explicit skips).
3.  **Observation:** Use Swagger (`/swagger`) to verify the endpoint behaves as expected (returns 200/400/404).
