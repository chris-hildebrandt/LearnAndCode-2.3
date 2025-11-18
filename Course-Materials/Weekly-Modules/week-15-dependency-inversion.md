# Week 15: Dependency Inversion Principle (DIP)

This week, we are focusing on the Dependency Inversion Principle (DIP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the DIP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Invert dependencies so high-level modules depend on abstractions, not concrete implementations.
- Introduce infrastructure abstractions (e.g., clock, cache) for future enhancements.
- Audit constructors to ensure only interfaces are injected.

## 2. Reading & Resources (45 min)

- **[Dependency Inversion Principle - Wikipedia](https://en.wikipedia.org/wiki/Dependency_inversion_principle)** - Original definition and comprehensive explanation of high-level and low-level module relationships.
- **[DIP in Practice - Principles.dev](https://principles.dev/p/dependency-inversion-principle/)** - Practical implementation guide with detailed architectural examples.
- **[Understanding DIP - Dev.to](https://dev.to/tamerlang/understanding-solid-principles-dependency-inversion-1b0f)** - Clear breakdown of concepts with modern implementation examples.
- **[DIP Implementation - Stackify](https://stackify.com/dependency-inversion-principle/)** - Practical applications and best practices for software design.
- Optional: **[DIP Design Patterns - OODesign](https://www.oodesign.com/dependency-inversion-principle)** - Advanced patterns and implementation strategies with code examples.

## 3. This Week’s Work

- Create `ISystemClock` abstraction (with production `UtcSystemClock`).
- Inject clock into services needing timestamps (e.g., `TaskBusinessRules`, `TaskService`).
- Audit constructors and replace any direct concrete dependencies with interfaces.
- Update `Program.cs` DI registrations accordingly.

## 4. Files to Modify

- New: `TaskFlowAPI/Infrastructure/Time/ISystemClock.cs`, `UtcSystemClock.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs`
- `TaskFlowAPI/Program.cs`
- Update tests to use fake clock (`TaskFlowAPI.Tests` as needed)
- This file (`Course-Materials/Weekly-Modules/week-15-dependency-inversion.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-15/<your-name>`.
2. Create `ISystemClock` with `DateTime UtcNow { get; }`.
3. Implement `UtcSystemClock : ISystemClock` returning `DateTime.UtcNow`.
4. Inject `ISystemClock` where `DateTime.UtcNow` is currently used (entity factory, business rules, service logging timestamps).
5. Update DI: `builder.Services.AddSingleton<ISystemClock, UtcSystemClock>();`
6. In tests, create fake clock for deterministic behaviour.
7. Audit constructors for any other concrete types (e.g., `TaskRepository` uses `TaskFlowDbContext`—acceptable). Replace direct instantiations with DI where needed.
8. Build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Ensure tests use fake clock to verify behaviours that depend on time.

## 7. Success Criteria

- No direct `DateTime.UtcNow` usages in business logic (all go through `ISystemClock`).
- Services depend only on interfaces.
- Tests prove ability to swap clock implementation.
- Build/tests succeed.

## 8. Submission Process

- Commit `Week 15 – dependency inversion`.
- PR summary includes list of newly introduced abstractions.
- Weekly issue documents how fake clock improved testability.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Abstraction Rationale:* Describe one concrete dependency you inverted and the runtime risk it mitigates.
- *Testing Impact:* How did fake or stub clocks/caches simplify tests this week?

### Discussion Prep:
- *What other infrastructure components might need abstractions (caching, email, etc.)?*
- *How does DIP reduce coupling to external time or frameworks?*
- *Did any parts remain concrete by choice? Explain why.*
- *How will you ensure new abstractions don’t become leaky or over-engineered?*

## 10. Time Estimate

- 60 min – Reading.
- 10 min – Identify concrete dependencies.
- 35 min – Implement clock abstraction + inject.
- 15 min – Update tests + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **[Dependency Inversion Example](../Examples/DependencyInversion.ts)**
