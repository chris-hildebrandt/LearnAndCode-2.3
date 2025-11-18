# Week 19: Essential Design Patterns

This week, we will explore various design patterns and their applications in software development, focusing on how these concepts align with In Time Tec's emphasis on technical excellence and the Quality Manifesto's focus on well-structured code.

## 1. Learning Objectives

- Understand the purpose and benefits of design patterns in software development.
- Identify and differentiate between various design patterns.
- Apply appropriate design patterns to your project to improve code quality and maintainability.
- Discuss the relationship between design patterns and the Quality Manifesto's emphasis on well-structured code.
- Implement Factory pattern for creating tasks with context-aware defaults.
- Review and solidify Strategy pattern usage (filters) and Repository pattern.
- Document when each pattern is appropriate within `TaskFlowAPI`.

## 2. Reading & Resources (50 min)

- **[Patterns.dev](https://www.patterns.dev/posts)** – Browse sections on creational and behavioural patterns.
- **[Refactoring Guru: Design Patterns](https://refactoring.guru/design-patterns)** – Reference implementations and UML diagrams.
- **[Sourcemaking: Design Patterns](https://sourcemaking.com/design_patterns)** – Additional explanations with variations.
- **[Tutorialspoint Design Patterns Overview](https://www.tutorialspoint.com/design_pattern/design_pattern_overview.htm)** – Quick refresher on categories.
- **[YouTube: 10 Design Patterns Explained in 10 Minutes](https://www.youtube.com/watch?v=tv-_1er1mWI)** – High-level tour.
- Focus on Factory & Strategy sections as they align with this week’s work.

## 3. This Week’s Work

- Create `TaskFactory` responsible for constructing `TaskEntity` instances based on request type (default due dates, priority rules).
- Update `TaskService` to use factory instead of `MapToEntity` for creation.
- Document existing Strategy usage for filters and ensure it’s extensible (no direct instantiation in service).

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskFactory.cs` (new)
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Mapping/TaskMapper.cs` (adjust to rely on factory)
- Update DI registration (`Program.cs` or `ServiceCollectionExtensions`)
- `docs/` add short explanation snippet if relevant
- This file (`Course-Materials/Weekly-Modules/week-19-design-patterns.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-19/<your-name>`.
2. Design `TaskFactory` with methods like `CreateNewTask(CreateTaskRequest request, ISystemClock clock)` returning a fully initialised entity.
3. Move creation logic (default priority, `CreatedAt`) from mapper/business rules into factory.
4. Update `TaskService` to call factory before saving; ensure tests updated to mock factory.
5. Review filter strategy registration—ensure service receives filters via abstraction (no `new` in service). Refactor if needed.
6. Add XML doc or inline comments summarising when to use each pattern.
7. Update tests to use fake factory as needed.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Ensure unit tests mocking factory still reach 80% coverage.

## 7. Success Criteria

- `TaskService` no longer constructs entities directly; factory handles creation.
- Strategy pattern remains intact with DI-based filter registration.
- Tests updated to use factory mock.
- Build/tests succeed.

## 8. Submission Process

- Commit `Week 19 – design patterns`.
- PR summary lists patterns implemented/refined and rationale.
- Weekly issue includes short paragraph on when to use Factory vs. Strategy.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Pattern Selection:* Document why you chose factory vs. keeping logic in mapper/business rules.
- *Future Pattern Ideas:* Note one additional pattern you considered and whether it fits the current scope.

### Discussion Prep:
- *How did the factory improve creation logic clarity?*
- *What trade-offs exist when adding more patterns?*
- *Which future features could reuse this factory?*
- *How will you guard against over-patterning the codebase?*

## 10. Time Estimate

- 55 min – Reading.
- 10 min – Pattern review + design.
- 40 min – Implement factory + service/test updates.
- 15 min – Build/test + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **[Design Patterns Example](../Examples/DesignPatterns.md)**
