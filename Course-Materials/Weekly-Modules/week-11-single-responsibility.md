# Week 11: Single Responsibility Principle (SRP)

This week, we are focusing on the Single Responsibility Principle (SRP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the SRP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Identify and eliminate SRP violations within `TaskService`.
- Extract mapping, validation, and business-rule logic into focused classes.
- Update dependency injection to wire new components.

## 2. Reading & Resources (70 min)

- **Clean Code Chapter 3 (Functions)** – Revisit the “do one thing” mantra.
- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** – Original definition and context.
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** – Practical examples and pitfalls.
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** – Strategies for untangling mixed responsibilities.
- **[Understanding SOLID: SRP - Dev.to](https://dev.to/ggorantala/solid-single-responsibility-principle-with-examples-h0f)** – Additional code samples and anti-patterns.
- Optional: **[The Single Responsibility Principle Revisited](https://thevaluable.dev/single-responsibility-principle-revisited/)** – Advanced heuristics for complex domains.

## 3. This Week’s Work

- Extract `TaskMapper`, `TaskValidator` (domain-specific), and `TaskBusinessRules` from `TaskService`.
- Register new services in DI and adjust `TaskService` constructor.
- Ensure unit tests (examples) reflect new structure.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskMapper.cs` (create)
- `TaskFlowAPI/Services/Tasks/Validation/TaskValidator.cs` (create)
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` (create)
- `TaskFlowAPI/Program.cs` (add registrations)
- `TaskFlowAPI.Tests` examples if needed for namespace updates
- This file (`Course-Materials/Weekly-Modules/week-11-single-responsibility.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-11/<your-name>`.
2. Create folders `Services/Tasks/Validation` and `Services/Tasks/Rules` if they don’t exist.
3. Move mapping logic into `TaskMapper` with methods like `ToDto`, `ToEntity`.
4. Move domain validation (non-FluentValidation) into `TaskBusinessRules` (e.g., checking completion status transitions).
5. Inject new dependencies into `TaskService` via constructor. Remove internal helper methods replaced by new classes.
6. Update DI in `Program.cs` to register mapper, business rules, and FluentValidation validators.
7. Ensure service uses new collaborators; logging remains inside service.
8. Build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual `GET`/`POST` to verify behavior unchanged.

## 7. Success Criteria

- `TaskService` focused on orchestration only (≤150 lines ideally).
- Mapper/business rules classes contain clear, single responsibilities.
- Dependency injection updated without circular references.
- Build/tests succeed.

## 8. Submission Process

- Commit `Week 11 – SRP refactor`.
- PR summary outlines classes extracted and reasons.
- Weekly issue includes diagram or bullet list of new responsibilities.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Responsibility Audit:* Capture the smell or metric that convinced you a new class was warranted.
- *Dependency Impact:* Note any DI registration changes and how they affect testability.

### Discussion Prep:
- *What metric indicated `TaskService` was doing too much?*
- *How did extraction change your approach to future unit tests?*
- *Were there responsibilities you intentionally kept inside the service? Why?*
- *What risks do new collaborators introduce (e.g., mapping drift, validation duplication)?*

## 10. Time Estimate

- 70 min – Reading
- 10 min – Identify responsibilities + plan.
- 45 min – Extract classes + DI updates.
- 15 min – Test + PR/issue.
**Total:** ~140 minutes.

## 11. Additional Resources

- **[Single Responsibility Example](../Examples/SingleResponsibility.cs)**
- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** - Comprehensive overview of SRP with Robert C. Martin's original definition.
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** - Practical examples and implementation details.
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** - In-depth analysis of practical applications.
- **[Understanding SOLID: SRP - Dev.to](https://dev.to/ggorantala/solid-single-responsibility-principle-with-examples-h0f)** - Clear explanations with code examples.
- **[The Single Responsibility Principle Revisited](https://thevaluable.dev/single-responsibility-principle-revisited/)** - Advanced discussion of SRP implementation.
