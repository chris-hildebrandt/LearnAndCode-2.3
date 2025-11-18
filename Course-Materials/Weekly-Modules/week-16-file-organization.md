# Week 16: File Organization & Module Structure

This week, we will focus on defining and managing boundaries in code, connecting this concept to In Time Tec's emphasis on continuous integration and deployment. We will learn how to create clean interfaces between components, ensuring that our applications are modular, maintainable, and easy to integrate. This module is based on **Clean Code Chapter 5: Formatting**.

## 1. Learning Objectives

- Understand the importance of defining and managing boundaries in code.
- Apply best practices for creating clean interfaces between components.
- Identify boundaries in your project and refactor the code to improve integrations between components.
- Discuss the relationship between clean boundaries and In Time Tec's emphasis on continuous integration and deployment.
- Restructure files so each class lives in a focused module/folder.
- Break apart any remaining “god” files (e.g., legacy `TaskService` monolith, helpers).
- Establish namespace conventions aligned with directory structure.

## 2. Reading & Resources (60 min)

- **Clean Code Chapter 5: Formatting (pp. 77-96)** – emphasise readability, vertical openness, and logical grouping.
- **Clean Code Chapter 8 (Boundaries)** – Refresh boundary management concepts.
- **[Designing Software with Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)** – Uncle Bob on modular architectures.
- **[Reading Clean Code: Boundaries](https://medium.com/codex/reading-clean-code-week-5-boundaries-aba7fbefb861)** – Commentary on chapter application.
- **[Clean Architecture](https://github.com/serodriguez68/clean-architecture/blob/master/part-5-2-architecture.md)** – Additional perspective on organizing layers.

## 3. This Week’s Work

- Move mapper, validator, business rules into dedicated folders (`Services/Tasks/Mapping`, `.../Validation`, `.../Rules`).
- Create `Extensions/` modules for shared helpers (e.g., `ServiceCollectionExtensions`).
- Update namespaces to match new folders. Remove unused helpers.
- NOTE: implement resharper and modify the preset linting rules

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/*`
- `TaskFlowAPI/Extensions/*`
- `TaskFlowAPI/Program.cs` (update using statements)
- `TaskFlowAPI.Tests` (fix namespaces where necessary)
- This file (`Course-Materials/Weekly-Modules/week-16-file-organization.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-16/<your-name>`.
2. Inspect `TaskService` and related classes; identify any lingering nested classes or `TODO` comment referencing monolith.
3. Create new subfolders: `Services/Tasks/Mapping`, `Services/Tasks/Validation`, `Services/Tasks/Rules`, `Services/Tasks/Filters` (already present—confirm naming).
4. Move files into appropriate folders and update namespaces.
5. Introduce `Extensions/ServiceCollectionExtensions.cs` consolidating DI registrations (repositories, services, filters, validators). Call it from `Program.cs`.
6. Delete any duplicate helper files or unused folder junk (e.g., old `Utils/Helpers.cs` if present).
7. Run build/tests to ensure namespaces and DI still work.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- Directory structure reflects modules (`Controllers`, `Services/Tasks/…`, `Validators`, etc.).
- No circular namespace dependencies.
- DI registrations centralised via extension method.
- Build/tests succeed; `git diff` shows moves not rewrites (use `git mv`).

## 8. Submission Process

- Commit `Week 16 – file organization` (use `git mv` to preserve history).
- PR summary includes tree snippet of new structure.
- Weekly issue attaches screenshot from IDE solution explorer.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Before/After Snapshot:* Paste the old vs. new folder path that most improved discoverability.
- *Namespace Strategy:* Describe your naming convention and how it maps to the new structure.

### Discussion Prep:
- *How does the new structure improve onboarding for new devs?*
- *What naming conventions did you adopt for namespaces?*
- *Did you remove any dead files? Share before/after impact.*
- *What automation (solution filters, analyzers) will keep the structure from regressing?*

## 10. Time Estimate

- 60 min – Reading.
- 10 min – Plan folder structure.
- 35 min – Move files + update namespaces/DI.
- 15 min – Build/test + PR/issue.
**Total:** ~60 minutes.

## 11. Additional Resources

- **[Systems Example](../Examples/Systems.md)**
