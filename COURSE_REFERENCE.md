# Learn and Code: Course & Repository Reference

## 1. Executive Summary

**Course Name:** Learn and Code  
**Based On:** "Clean Code" (Bob Martin), SOLID Principles, In Time Tec Quality Manifesto  
**Target Audience:** New developers (apprentices), mid-level devs upskilling in .NET/Clean Code.  
**Repository Purpose:** A hands-on learning environment consisting of a theoretical curriculum and a practical application (`TaskFlowAPI`) that evolves from a "messy" state to a robust, clean architecture over 23 weeks.

This document serves as a master reference for AI agents and instructors to understand the structure, goals, and technical details of the course.

---

## 2. Repository Structure

The repository is divided into two primary sections:

### A. Curriculum (`/Course-Materials`)
Contains the educational content and assignments.
-   **`Weekly-Modules/`**: The core progression. Files like `week-01-introduction.md`, `week-08-repository-pattern.md`. Each file contains:
    -   Learning Objectives.
    -   Readings ("Clean Code" chapters, "Quality Manifesto").
    -   **"This Week's Work"**: Specific coding assignments in `TaskFlowAPI`.
    -   Reflective Journal/Discussion prompts.
-   **`Readings/`**: PDF copies of core texts.
-   **`Examples/`**: Standalone code examples illustrating concepts (e.g., `MeaningfulNames.md`, `DependencyInversion.ts`).
-   **`About TaskFlowAPI/`**: High-level context about the project's evolution.

### B. Project (`/TaskFlowAPI` & `/TaskFlowAPI.Tests`)
The hands-on learning tool.
-   **Type:** ASP.NET Core Web API (.NET 8).
-   **Database:** SQLite (Entity Framework Core).
-   **Testing:** xUnit, Moq, FluentAssertions.
-   **State:** The project contains **intentional code smells** and **incomplete implementations** that students must fix/finish as part of their assignments.

---

## 3. Curriculum Progression

The course follows a linear progression from basic coding hygiene to advanced architectural patterns.

| Phase | Weeks | Focus | Key Assignments |
| :--- | :--- | :--- | :--- |
| **Foundations** | 1-4 | Clean Code Basics | Setup, Naming (`TasksController` refactor), Comments, Functions. |
| **Core Implementation** | 5-9 | Layered Architecture | Repository Pattern (`TaskRepository`), Service Layer, DTOs. |
| **SOLID Principles** | 10-15 | OOD Principles | SRP (Validation), OCP (Filters), LSP, ISP, DIP. |
| **Advanced Topics** | 16-23 | Process & Polish | File Org, TDD (`CompleteTaskAsync`), Refactoring, Design Patterns, Perf. |

---

## 4. TaskFlowAPI Technical Reference

### Architecture
The app follows a standard layered architecture:
1.  **Presentation:** `Controllers/` (e.g., `TasksController.cs`). Handles HTTP, input parsing.
2.  **Application/Service:** `Services/` (e.g., `TaskService.cs`). Business logic, validation, DTO mapping.
3.  **Data Access:** `Repositories/` (e.g., `TaskRepository.cs`). DB interactions via EF Core.
4.  **Domain/Data:** `Entities/` (e.g., `TaskEntity.cs`) and `Data/` (`TaskFlowDbContext`).

### Key Patterns Used
-   **Dependency Injection:** Heavily used in `Program.cs` and constructors.
-   **Repository Pattern:** Abstracting data access (Week 8).
-   **Strategy Pattern:** Used for Task Filters (Week 12).
-   **Factory Pattern:** Planned for Week 19.

### Testing Strategy
-   **Unit Tests:** Focus on Service layer logic.
-   **Fakes vs. Mocks:** Strong preference for **Fakes** (e.g., `FakeTaskRepository`) for state-based testing, reserving **Mocks** (Moq) for interaction verification.
-   **TDD:** Explicitly taught in Week 17; students must implement `CompleteTaskAsync` using TDD.

---

## 5. Pedagogical Strategy & Agent Guidelines

**Crucial for AI Agents:** This is a *learning* repo, not just a codebase to be "fixed".

1.  **Intentional Imperfections:**
    -   **DO NOT** automatically fix "code smells" (like bad variable names `svc`, `dt` or magic strings) unless the user specifically asks or is working on the relevant week (e.g., Week 2).
    -   **DO NOT** fill in `NotImplementedException` or `TODO` blocks proactively. These are student homework.
    -   **DO** point out these issues if asked to "review" or "audit" the code, framing them as learning opportunities.

2.  **Scaffolding vs. Solutions:**
    -   When assisting, provide **guidance and patterns** rather than full copy-paste solutions.
    -   Example: If a student asks how to implement `GetAllAsync`, show the generic pattern (DbSet -> AsNoTracking -> ToList), not the exact code for `TaskEntity`.

3.  **Context Awareness:**
    -   Always check which **Week** the user is currently on.
    -   *Example:* If a user is on Week 2, do not suggest advanced patterns like MediatR or CQRS. Stick to "Meaningful Names".
    -   *Example:* If a user is on Week 8, focus strictly on EF Core and Repository pattern syntax.

4.  **Alignment with Values:**
    -   **Customer Centricity:** Link coding decisions to customer impact (e.g., "Bad naming slows down future features, costing the customer money").
    -   **Quality Manifesto:** Reference the manifesto values (e.g., "Right First Time", "Total Quality Management") in explanations.

### Common Student Pitfalls to Watch For
-   **Week 2:** Missed renames in `Program.cs` or comments.
-   **Week 8:** Forgetting `AsNoTracking()` for read-only queries.
-   **Week 9:** Leaking Entities into the Controller (bypassing DTOs).
-   **Week 17:** Mocking *everything* instead of using Fakes for logic and Mocks for side-effects.

---

## 6. Maintenance & Contributor Notes

-   **Updating Curriculum:** If modifying weekly markdowns, ensure the corresponding code in `TaskFlowAPI` reflects the *start state* expected for that week.
-   **Snapshotting:** The repo design relies on the code evolving. Future improvements might involve using Git tags/branches to define the "Start State" for each week more rigorously.
