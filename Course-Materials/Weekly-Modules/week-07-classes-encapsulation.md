# Week 7: Classes & Encapsulation

This week, we will focus on the principles of organizing classes, emphasizing how these concepts align with In Time Tec's commitment to modular, maintainable code and the Quality Manifesto's focus on technical excellence. This module is based on **Clean Code Chapter 10: Classes**.

## 1. Learning Objectives

- Understand the principles of organizing classes and systems for modularity and maintainability.
- Apply best practices for organizing classes in your project.
- Refactor your project's classes to improve modularity and maintainability.
- Discuss how we can create and refactor classes to align with the Quality Manifesto's emphasis on technical excellence.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 10: Classes (pp. 137-154)**.
  - Focus on cohesion, encapsulation, and hiding implementation details.

## 3. This Week’s Work

- Refactor `TaskEntity` to use private fields and guarded property access.
- Add domain behaviors: `Complete()`, `Reopen()`, `UpdateDetails(...)`, `ChangePriority(...)`.
- Introduce static factory `TaskEntity.Create(...)` that validates inputs.

## 4. Files to Modify

- `TaskFlowAPI/Entities/TaskEntity.cs`
- `TaskFlowAPI/Entities/ProjectEntity.cs` (optional: add helper to attach tasks)
- Any affected migration snapshot (run `dotnet ef migrations add` only if schema changes).
- This file (`Course-Materials/Weekly-Modules/week-07-classes-encapsulation.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-07/<your-name>`.
2. Replace auto-properties with private fields + public getters where necessary.
3. Add constructor(s) or factory ensuring `Title`, `Priority`, and `ProjectId` are validated.
4. Implement domain methods:
   - `Complete(DateTime completedAt)` marks task complete and sets timestamp.
   - `Reopen()` resets completion state.
   - `UpdateDetails(string title, string? description, DateTime? dueDate)` guards null/empty titles.
   - `ChangePriority(int priority)` ensures priority range (0-5 default suggestion).
5. Ensure all state changes flow through these methods; remove public setters.
6. Update seed data in `TaskFlowDbContext` to use the new factory or constructor.
7. Build and run migrations if compilation demands (expected minimal changes if property names remain).

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Optional: temporary console app or `dotnet script` to instantiate `TaskEntity` and ensure methods behave.

## 7. Success Criteria

- No public setters on `TaskEntity` (other than EF Core required parameterless constructor if used).
- Domain methods enforce invariants and throw meaningful exceptions when inputs invalid.
- Seed data still builds; migrations remain valid.
- Build + tests succeed.

## 8. Submission Process

- Commit `Week 07 – task entity encapsulation`.
- PR summary must describe each new domain method and rule enforced.
- Weekly submission issue includes snippet of new `TaskEntity.Create` signature.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Invariants:* Document one invariant you enforced and the business rule it protects.
- *Factory vs Constructor:* Why did you choose your current creation approach? Note trade-offs for future teams.

### Discussion Prep:
- *Which invariants did you guard and why?*
- *How would another developer know how to create a valid `TaskEntity` now?*
- *What future bugs does this encapsulation prevent?*
- *Where might encapsulation conflict with EF Core conveniences, and how will you mitigate that?*
- *Discuss the relationship between class, system organization, and the Quality Manifesto's emphasis on technical excellence.*

## 10. Time Estimate

- 40 min – Reading + design sketch.
- 70 min – Implementation + seed data adjustments.
- 10 min – Build/test + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **[Classes Example](../Examples/Classes.md)**
- **[System Design in Software Development](https://medium.com/the-andela-way/system-design-in-software-development-f360ce6fcbb9)**
- **[Clean Code: Chapter 10 Classes](https://medium.com/the-command-line/clean-code-chapter-10-classes-98be694f1fa2)**
