# Week 8: Repository Pattern

This week, we will focus on the principles of organizing systems, emphasizing how these concepts align with In Time Tec's commitment to modular, maintainable code and the Quality Manifesto's focus on technical excellence. This module is based on **Clean Code Chapter 11: Systems**.

## 1. Learning Objectives

- Implement repository methods using EF Core best practices.
- Apply async patterns (`await`, `CancellationToken`) consistently.
- Understand how repositories isolate data access from services.
- Understand the principles systems and system organization for modularity and maintainability.
- Refactor your project's system architecture to improve modularity and maintainability.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 11: Systems (pp. 155-174)** – Separate construction from use; keep boundaries clean.
- **[System Design in Software Development](https://medium.com/the-andela-way/system-design-in-software-development-f360ce6fcbb9)**

## 3. This Week’s Work

- Implement all `TODOs` in `TaskRepository`.
- Use `AsNoTracking` for read operations and include `Project` navigation.
- Ensure create/update/delete paths save changes with cancellation support.

## 4. Files to Modify

- `TaskFlowAPI/Repositories/TaskRepository.cs`
- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (doc comments if needed)
- Optional: `TaskFlowDbContext` if you need helper queries (keep minimal).
- This file (`Course-Materials/Weekly-Modules/week-08-repository-pattern.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-08/<your-name>`.
2. Implement `GetAllAsync` using ordering by `Priority` then `DueDate` then `CreatedAt`.
3. Implement `GetByIdAsync` including related `Project` via `.Include` and `.AsNoTracking()`.
4. Implement `CreateAsync` using `_dbContext.Tasks.AddAsync` and `SaveChangesAsync`.
5. Implement `UpdateAsync` ensuring entity is tracked (attach if necessary) and call `SaveChangesAsync`.
6. Implement `DeleteAsync` with null check—no exception if entity missing.
7. Add guard clauses for `cancellationToken.ThrowIfCancellationRequested()` when appropriate.
8. Run build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- (Optional) `dotnet ef database update` followed by manual `GET` via Swagger to ensure repository works (will still hit `NotImplementedException` in service until Week 9, that’s expected).

## 7. Success Criteria

- No remaining `NotImplementedException` in `TaskRepository`.
- All methods use `async/await` and respect `CancellationToken`.
- Query methods include `AsNoTracking` and navigation properties as needed.
- Build/tests succeed.

## 8. Submission Process

- Commit `Week 08 – repository implementation`.
- PR summary must list each method and how you tested it.
- Weekly submission issue includes code snippet of your favourite query.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Query Design:* Capture one LINQ query decision (ordering, includes) and why it matches business expectations.
- *Cancellation:* Note where you propagated `CancellationToken` and any gaps you spotted for future work.

### Discussion Prep:
- *What trade-offs did you consider regarding eager vs. lazy loading?*
- *How does cancelling a request propagate through repository methods?*
- *Which helper methods or extension methods might simplify repository code later?*
- *What additional indexes or database constraints might you add once migrations are in play?*
- *Discuss the relationship between system organization and the Quality Manifesto's emphasis on technical excellence.*

## 10. Time Estimate

- 60 min – Reading + plan queries.
- 50 min – Implement methods + guard clauses.
- 10 min – Build/test + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **Microsoft Docs: Repository Pattern** – Official guidance with EF Core examples.
- **Fowler: Service & Repository patterns** – Conceptual background (short article).
- **Refactoring Guru: Repository Pattern** – Alternative explanations and diagrams.
- Optional: skim `Pro .NET Design Patterns` (Repository chapter) for advanced nuances.
