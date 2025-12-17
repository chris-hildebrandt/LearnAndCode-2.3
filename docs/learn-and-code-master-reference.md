# Learn & Code (In Time Tec) — Master Repository Reference

This document is a **single master reference** for AI agents (and humans) to quickly understand:

- The **curriculum structure** (23 weeks of readings + weekly assignments)
- The **TaskFlowAPI training application** (what it is, how it’s structured, where to change things)
- The **course constraints** (no busywork, beginner-accessible, changes must matter to the app)
- The **best “entry points”** for making changes or evaluating alignment with course goals and ITT values

---

## Why this repo exists

**Learn & Code** is an internal, job-training course based on:

- **Clean Code** (Robert C. Martin)
- **SOLID principles**
- **In Time Tec Quality Manifesto** (corporate standards for “done”)

The repo is designed to be **forked per student**, and the weekly work simulates real partner tasks: students must translate goals into implementable changes, with mentorship support.

---

## Non‑negotiable course constraints (use these to judge any change)

Any assignment, refactor, or feature change should satisfy all three:

1. **Tightly coupled to learning**
   - The change should directly exercise the week’s reading/principle.
   - Avoid “busywork refactors” that don’t improve comprehension or reinforce the concept.

2. **Accessible but not trivial**
   - Works for students new to C#/.NET (clear steps, small increments, compiler-guided workflow).
   - Still rewarding for experienced devs (stretch goals, optional hard modes, deeper tradeoffs).
   - Scaffolding should guide without being copy/paste answers.

3. **Matters to TaskFlowAPI**
   - Students should see visible impact: Swagger endpoint behavior, error responses, filters, tests, docs, etc.
   - Avoid work on unused files or dead branches. If it doesn’t affect the app or its quality, it’s likely misaligned.

---

## Audience + operating model (important context for agents)

- **Primary learners**: new developers (often without formal CS background) and mid-level devs upskilling in C#/.NET and clean design.
- **Operating model**: each student **forks** the repo; weekly work happens on their fork/branches; this repo is curriculum + training app.
- **Key implication**: TaskFlowAPI intentionally contains **incomplete code** and **intentional smells** so students can practice improving it.

---

## Repository map (what’s where)

### Curriculum “source of truth”

- `Course-Materials/Weekly-Modules/`
  - **Primary weekly assignment docs** (`week-01-...md` through `week-23-...md`).
  - Each file typically contains: objectives, readings, step-by-step tasks, test instructions, success criteria, journal prompts.

- `Course-Materials/Examples/`
  - Supporting examples and guides (naming, SRP, patterns, TDD, documentation, etc.).
  - Includes special guides like Week 5 AI assignment and Week 7 encapsulation guide.

- `Course-Materials/About TaskFlowAPI/`
  - High-level framing:
    - `TaskFlowAPI_Project_Overview.md`
    - `TaskFlowAPI_Current_State.md`
    - `TaskFlowAPI_Future_State.md`
    - `TaskFlowAPI_Curriculum_Evaluation.md`

- `Course-Materials/SETUP.md`
  - Codespaces-focused setup + commands to run API/tests.

- `Course-Materials/learn-and-code-ai-use-rules.md`
  - AI usage policy for students (manual typing, “AI as junior dev,” 30-minute rule, etc.).

- `Course-Materials/WEEKLY_PROGRESS.md`
  - Checklist across all 23 weeks.

### App + tests (hands-on learning tool)

- `TaskFlowAPI/`
  - ASP.NET Core Web API (C#, .NET 8)

- `TaskFlowAPI.Tests/`
  - xUnit tests project (currently mostly placeholders/examples; Week 17 expands).

### Course-wide supporting docs

- `docs/architecture-diagrams.md`
  - Mermaid diagrams for current/future architecture, data flow, sequence diagrams, evolution timeline.

- `docs/code-smells-catalog.md`
  - Catalog of **intentional code smells** placed in the codebase for Week 18.

- `docs/sample-prs/`
  - Sample PR writeups for Weeks 12/17/19 used in Week 20 review practice.

---

## Quickstart commands (for agents)

From repo root:

- **Build**: `dotnet build TaskFlowAPI.sln`
- **Tests**: `dotnet test TaskFlowAPI.sln`

Database setup (per `Course-Materials/SETUP.md`):

- `dotnet tool install --global dotnet-ef`
- `dotnet ef database update` (run from `TaskFlowAPI/`)

Run API:

- `dotnet run --project TaskFlowAPI`
- Swagger: `/swagger`

---

## TaskFlowAPI — architecture and key extension points

### Current architecture pattern (learning-friendly layered design)

The app is intentionally structured to teach boundaries:

- **Controllers**: HTTP concerns only (routing, status codes)
  - `TaskFlowAPI/Controllers/TasksController.cs`
  - `TaskFlowAPI/Controllers/ReportsController.cs`

- **Services**: business logic orchestration
  - `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
  - `TaskFlowAPI/Services/Tasks/TaskService.cs`

- **Repositories**: EF Core data access abstraction
  - `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs`
  - `TaskFlowAPI/Repositories/TaskRepository.cs`

- **Data**: EF Core DbContext + seeding
  - `TaskFlowAPI/Data/TaskFlowDbContext.cs`

- **Entities**: persistence/domain models
  - `TaskFlowAPI/Entities/TaskEntity.cs`
  - `TaskFlowAPI/Entities/ProjectEntity.cs`

- **DTOs**: API boundary models
  - Requests: `TaskFlowAPI/DTOs/Requests/*`
  - Responses: `TaskFlowAPI/DTOs/Responses/*`

- **Cross-cutting concerns**
  - Validation scaffolding: `TaskFlowAPI/Validators/*` (FluentValidation)
  - Exceptions: `TaskFlowAPI/Exceptions/*`
  - Global exception handling: `TaskFlowAPI/Extensions/ExceptionMiddlewareExtensions.cs`
  - Code smell playground: `TaskFlowAPI/Helpers/TaskHelper.cs`
  - Filter strategies (OCP/Strategy): `TaskFlowAPI/Services/Tasks/Filters/*`

### Startup wiring (DI + middleware)

- `TaskFlowAPI/Program.cs` shows:
  - Serilog logging
  - EF Core SQLite DbContext
  - DI registrations for repository/service
  - FluentValidation integration
  - Response compression + memory cache
  - Global exception handler (extension method)

This file is the main “system composition” hotspot for Weeks 8–16 and later.

### Intentional training scaffolding in code

The app includes:

- **Week-labeled TODOs** (e.g., “TODO Week 10”, “TODO Week 22”)
- **NotImplementedException scaffolding** in repository/service/filter implementations
- **Embedded “code smell” comments** (`// CODE SMELL:`), with a matching catalog in `docs/code-smells-catalog.md`

These are not accidents; they are deliberate learning surfaces.

---

## Curriculum overview (what each week changes in the app)

This is the fastest mapping from learning objective → “where students work” → visible outcome.

### Phase 1 — Foundation

- **Week 1 (Intro + Quality Manifesto)**
  - **Work**: setup, run Swagger/tests, code smell inventory journal.
  - **Primary files**: `Course-Materials/SETUP.md`, `docs/architecture-diagrams.md`, weekly module file.

- **Week 2 (Meaningful Names)**
  - **Work**: rename controller/service interface members (remove abbreviations like `svc`, `dt`, `GetOne`).
  - **Primary files**: `TasksController.cs`, `ITaskService.cs`, `TaskService.cs`.
  - **Outcome**: same behavior, dramatically clearer code.

- **Week 3 (Comments & Documentation)**
  - **Work**: delete “what” comments, keep “why”; improve XML docs where useful.
  - **Primary files**: same as Week 2.

- **Week 4 (Functions)**
  - **Work**: refactor `ReportsController.GenerateProjectSummaryReport` into small helpers; add Update/Delete endpoints stubs.
  - **Primary files**: `ReportsController.cs`, `TasksController.cs`, `ITaskService.cs`, `TaskService.cs`.
  - **Outcome**: report endpoint still works; Swagger shows PUT/DELETE (even if backed by NotImplemented service methods in early weeks).

- **Week 5 (AI tools)**
  - **Work**: compare AI refactor to human refactor; ethics + “trust but verify.”
  - **Primary files**: `Course-Materials/Examples/Week-05-AI-Assignment.md`, policy in `learn-and-code-ai-use-rules.md`.

### Phase 2 — Architecture

- **Week 6 (Git workflow)**
  - **Work**: branching/PR practice and commit hygiene.

- **Week 7 (Classes & Encapsulation / Objects vs Data Structures)**
  - **Work**: evolve `TaskEntity` from anemic data model into an object protecting invariants.
  - **Primary files**: `TaskEntity.cs`, `TaskService.cs`, `TaskFlowDbContext.cs`.
  - **Outcome**: invalid states become harder/impossible (e.g., empty title).

- **Week 8 (Repository Pattern)**
  - **Work**: implement EF Core repository methods (async, cancellation, AsNoTracking, include Project).
  - **Primary files**: `TaskRepository.cs`, `TaskFlowDbContext.cs`.
  - **Outcome**: real DB access is implemented; still may be blocked until service is implemented in Week 9.

- **Week 9 (Service Layer + DTOs)**
  - **Work**: implement service orchestration + mapping; return DTOs.
  - **Primary files**: `TaskService.cs` (+ DTOs and mapping helpers).
  - **Outcome**: API endpoints return real data instead of throwing.

- **Week 10 (Error Handling + Validation)**
  - **Work**: FluentValidation rules + consistent `ProblemDetails` error responses.
  - **Primary files**: `Validators/*`, `ExceptionMiddlewareExtensions.cs`, `TaskService.cs`, `Program.cs`.
  - **Outcome**: invalid requests return 400 with actionable messages; not-found returns 404.

### Phase 3 — SOLID

- **Week 11 (SRP)**
  - **Work**: extract mapper + business rules out of `TaskService`.
  - **Primary files**: `TaskService.cs`, new mapper/rules classes, `Program.cs` registrations.
  - **Outcome**: service becomes orchestration; dependencies become testable.

- **Week 12 (OCP)**
  - **Work**: implement Strategy filters + Composite filter; add factory to build filter set from query params.
  - **Primary files**: `Services/Tasks/Filters/*`, `TasksController.cs`, `TaskService.cs`, DI registrations.
  - **Outcome**: adding a new filter is additive (new class), not a service if-chain.

- **Week 13 (LSP)**
  - **Work**: contract tests; fake repository must match real behavior.
  - **Primary files**: new test lab under `TaskFlowAPI.Tests/Examples/LSPLab/*` + `FakeTaskRepository` + contract tests.
  - **Outcome**: test doubles stop “lying” about production behavior.

- **Week 14 (ISP)**
  - **Work**: split repository interface into `ITaskReader`/`ITaskWriter` (CQRS-style separation).
  - **Outcome**: consumers depend only on what they use.

- **Week 15 (DIP)**
  - **Work**: introduce `ISystemClock` and inject time usage.
  - **Outcome**: deterministic tests for time-based behavior; fewer direct framework dependencies.

### Phase 4 — Quality & Patterns

- **Week 16 (File organization / boundaries)**
  - **Work**: restructure folders and namespaces; centralize DI registration in extensions.
  - **Outcome**: onboarding and navigation improve; boundaries clearer.

- **Week 17 (Unit Testing + TDD)**
  - **Work**: replace skipped tests; TDD new feature (often `CompleteTaskAsync`).
  - **Outcome**: meaningful service tests; coverage targeted at business logic.

- **Week 18 (Code smells + refactoring)**
  - **Work**: remove 5 smells (≥3 from catalog), keep behavior stable.
  - **Outcome**: cleaner code + confidence from tests.

- **Week 19 (Design patterns)**
  - **Work**: Factory pattern for task creation; ensure strategy patterns stay DI-driven.
  - **Outcome**: entity creation becomes explicit/testable and context-aware.

- **Week 20 (Code review)**
  - **Work**: review sample PRs in `docs/sample-prs/` (or peer reviews), document feedback.

### Phase 5 — Production ready

- **Week 21 (API design + documentation)**
  - **Work**: pagination, swagger XML comments, API versioning.
  - **Outcome**: more partner-realistic API surface.

- **Week 22 (Async best practices)**
  - **Work**: remove `.Result`, `Task.Run`, fake cancellation tokens, fire-and-forget in request paths.
  - **Primary file hotspot**: `ReportsController.cs` currently contains several explicit async anti-pattern TODOs.

- **Week 23 (Final polish)**
  - **Work**: documentation polish, tests green, demo narrative, retro.

---

## “Hotspot” file index (fast entry points for agents)

### Curriculum control points

- Weekly assignments: `Course-Materials/Weekly-Modules/*.md`
- Setup + workflow: `Course-Materials/SETUP.md`
- AI policy: `Course-Materials/learn-and-code-ai-use-rules.md`
- Architecture vision: `docs/architecture-diagrams.md`
- Smell inventory: `docs/code-smells-catalog.md`

### App control points

- Composition root: `TaskFlowAPI/Program.cs`
- Request entry points: `TaskFlowAPI/Controllers/*.cs`
- Core orchestration: `TaskFlowAPI/Services/Tasks/TaskService.cs`
- Persistence boundary: `TaskFlowAPI/Repositories/TaskRepository.cs`
- Database schema: `TaskFlowAPI/Data/TaskFlowDbContext.cs` + `TaskFlowAPI/Migrations/*`

---

## How to evaluate or design an assignment (rubric for course maintainers)

Use this when editing weekly modules or adding new tasks.

### Must-haves

- **Learning alignment**: explicitly cite the Clean Code chapter/principle and name the target code smell/pattern.
- **Visibility**: students can verify change via Swagger, logs, tests, or clearly observable behavior.
- **Incrementality**: steps are small enough that compiler errors guide progress.
- **Verification instructions**: include `dotnet build` + `dotnet test` and (optionally) a Swagger request.

### Scaffolding levers (to balance beginner vs experienced)

- **Beginner-friendly**:
  - Provide file paths and “start here” ordering.
  - Provide checklists and self-check questions.
  - Include templates/snippets that demonstrate *shape* but not full solution.

- **Challenge for experienced devs**:
  - Add stretch goals: e.g., extract interface, add tests, enforce invariants, reduce method size.
  - Add “tradeoff prompts”: where would this logic live, what breaks if moved, performance implications.

### Anti-patterns to avoid (common “busywork” failure modes)

- Pure renames with no learning hook (unless Week 2).
- Large refactors without a test harness or verification path.
- Tasks that touch files unused by app runtime/tests.
- Assignments that require introducing new libraries *without teaching the why*.

---

## AI agent guardrails (critical: this is a learning repo)

This repository is not meant to be “fixed” all at once. Many “problems” are **deliberate homework**.

### Do NOT do these unless explicitly requested (or the current week calls for it)

- **Do not proactively remove code smells** (bad names, magic strings, long methods) unless the week is about that refactor (e.g., Week 2 naming, Week 18 smells).
- **Do not implement `NotImplementedException` / `TODO Week X` blocks** early “to help.” Those are student assignments.
- **Do not introduce advanced frameworks** (CQRS libs, MediatR, AutoMapper, etc.) unless a module explicitly teaches/justifies it.

### Do these by default

- **Ask/assume the week context** before proposing changes. If week is unknown, default to the *earliest safe* approach (keep changes small and aligned to Weeks 1–4 patterns).
- **Provide guidance over solutions**: explain approach, provide scaffolding, and require verification steps instead of dumping paste-ready full implementations.
- **Tie decisions to customer impact and ITT values**: explain how a change affects onboarding speed, defect rate, and partner ROI.

---

## Agent usage guidance (recommended prompt skeleton)

When spinning up an AI agent to modify this repo, include:

- **Target week/module** (e.g., “Week 12 OCP filters”) and the relevant file(s).
- **Constraint**: “No busywork; changes must visibly improve TaskFlowAPI.”
- **Student accessibility**: require clear steps, not a pasteable final answer.
- **Verification**: require build/test instructions and a Swagger/manual check.

Suggested prompt skeleton:

- “You are updating the Learn & Code curriculum and/or TaskFlowAPI.
  - Goal: [what outcome]
  - Week alignment: [week + principle]
  - Files to touch: [paths]
  - Don’t introduce new deps unless the module explicitly teaches them.
  - Provide a short rationale mapping changes to learning objectives.
  - Ensure changes have a visible effect in the API (Swagger, error responses, tests, etc.).”

---

## Common student pitfalls (high-signal watchlist)

These are recurring “gotchas” that increase frustration for beginners and are good targets for extra scaffolding.

- **Week 2 (Naming)**: partial renames that miss `nameof(...)`, comments, DI parameter names, or route/action names.
- **Week 4 (Functions)**: extracting helpers that mix abstraction levels, or leaving the coordinator method too long (>15 lines).
- **Week 8 (Repository)**: forgetting `AsNoTracking()` for read paths, not propagating `CancellationToken`, or forgetting to `.Include(...)` needed navigation properties.
- **Week 9 (Service/DTOs)**: leaking entities from controllers (DTO boundary bypass), or mapping causing null-reference issues on `Project` navigation.
- **Week 10 (Validation/Errors)**: producing unclear validation messages, or returning inconsistent HTTP shapes (non-ProblemDetails).
- **Week 12 (OCP filters)**: reintroducing `if/switch` cascades in service/controller instead of additive filter strategies.
- **Week 17 (Testing)**: over-mocking everything; tests become “I told the mock to return X, it returned X.” Prefer fakes for state and mocks for side-effect verification.
- **Week 22 (Async)**: sync-over-async (`.Result/.Wait()`), fake cancellation tokens (`new CancellationToken()`), and fire-and-forget tasks in request paths.

---

## Maintenance notes (for curriculum maintainers)

### Keep curriculum and code start-state aligned

When editing weekly markdowns, ensure TaskFlowAPI reflects the **expected start state** for that week. Doc↔code drift is one of the fastest ways to create “busywork” and confusion for brand-new students.

### Consider explicit “start state” snapshots

If you want stronger guarantees, consider defining week start states via:

- **Git tags** (e.g., `week-07-start`, `week-12-start`) or
- **Long-lived branches** per phase/week

This keeps the weekly module instructions mechanically accurate and reduces accidental drift over time.

### Sample PR documents

Files under `docs/sample-prs/` can intentionally contain issues for review practice, but should stay consistent with the repo’s naming conventions unless the inconsistency is explicitly labeled as intentional.

---

## Known current-state quirks (important for agent expectations)

- Many methods are intentionally **NotImplemented** (repository/service/filters) to stage learning across weeks.
- The tests project currently includes **skipped placeholder tests**; meaningful tests are primarily introduced around Week 17.
- `ReportsController.GenerateProjectSummaryReport` is intentionally a “bad” function and also contains **Week 22 async anti-patterns**.
- `TaskHelper.cs` is intentionally a **code smell playground**.

---

## Related docs (start here)

- Repo overview: `README.md`
- Setup: `Course-Materials/SETUP.md`
- Weekly modules: `Course-Materials/Weekly-Modules/`
- Architecture diagrams: `docs/architecture-diagrams.md`
- Code smells catalog: `docs/code-smells-catalog.md`
- TaskFlowAPI overview: `Course-Materials/About TaskFlowAPI/TaskFlowAPI_Project_Overview.md`
