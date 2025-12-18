# Week 15: Dependency Inversion Principle (DIP)

This week, we are focusing on the Dependency Inversion Principle (DIP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence.

## 1. Learning Objectives

- Invert dependencies so high-level modules depend on abstractions, not concrete implementations.
- Introduce infrastructure abstractions (e.g., clock) for deterministic testing.
- Audit constructors to ensure only interfaces are injected.

## 2. Reading & Resources (45 min)

- **[Dependency Inversion Principle - Wikipedia](https://en.wikipedia.org/wiki/Dependency_inversion_principle)** (10 min) - Original definition and comprehensive explanation.
- **[DIP in Practice - Principles.dev](https://principles.dev/p/dependency-inversion-principle/)** (10 min) - Practical implementation guide.
- **[Understanding DIP - Dev.to](https://dev.to/tamerlang/understanding-solid-principles-dependency-inversion-1b0f)** (10 min) - Clear breakdown with examples.
- **[DIP Implementation - Stackify](https://stackify.com/dependency-inversion-principle/)** (10 min) - Practical applications and best practices.
- Optional: **[DIP Design Patterns - OODesign](https://www.oodesign.com/dependency-inversion-principle)** (5 min) - Advanced patterns.

## 3. This Week's Work

- Create `ISystemClock` abstraction (with production `UtcSystemClock`).
- Inject clock into services needing timestamps (e.g., `TaskBusinessRules`, `TaskService`).
- Audit constructors and replace any direct concrete dependencies with interfaces.
- Update `Program.cs` DI registrations accordingly.

## 4. Files to Modify

- New: `TaskFlowAPI/Infrastructure/Time/ISystemClock.cs`
- New: `TaskFlowAPI/Infrastructure/Time/UtcSystemClock.cs`
- New: `TaskFlowAPI.Tests/Fakes/FakeSystemClock.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs`
- `TaskFlowAPI/Program.cs`
- Update tests as needed (`TaskFlowAPI.Tests`).
- This file (`Course-Materials/Weekly-Modules/week-15-dependency-inversion.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-15-submission`.
2. Create `ISystemClock` with `DateTime UtcNow { get; }` property.
3. Implement `UtcSystemClock : ISystemClock` returning `DateTime.UtcNow`.
4. Create `FakeSystemClock` for tests with `Advance()` and `SetTime()` methods.
5. Find all `DateTime.UtcNow` usages in business logic:
   ```bash
   grep -r "DateTime.UtcNow" TaskFlowAPI/Services/
   ```
6. Inject `ISystemClock` where found; replace direct `DateTime.UtcNow` calls with `_clock.UtcNow`.
7. Update DI in `Program.cs`: `builder.Services.AddSingleton<ISystemClock, UtcSystemClock>();`.
8. Update tests to use `FakeSystemClock` for deterministic time control.
9. Audit constructors for any other concrete types (e.g., `TaskRepository` uses `TaskFlowDbContext`—acceptable as infrastructure).
10. Run `dotnet build TaskFlowAPI.sln` and `dotnet test TaskFlowAPI.sln` to verify.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual verification (optional):
- Create a task via Swagger and verify `CreatedAt` timestamp is set correctly.
- Tests should use fake clock to verify time-dependent behaviors deterministically.

## 7. Success Criteria

- No direct `DateTime.UtcNow` usages in business logic (all go through `ISystemClock`).
- Services depend only on interfaces for infrastructure concerns.
- Tests prove ability to swap clock implementation.
- `FakeSystemClock` created with `Advance()` method for testing time progression.
- Build and tests succeed with no warnings.
- Week 15 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-15-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 15 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Abstraction Rationale: Describe one concrete dependency you inverted this week and the runtime risk it mitigates.*
- *Testing Impact: How did the fake clock simplify your tests? Give a specific example of a test that would have been flaky without it.*
- *Constructor Audit: What other concrete dependencies did you find in your constructor audit? Did you invert them, and if not, why?*

### Discussion Prep:

- *What other infrastructure components might need abstractions (caching, email, file system)?*
- *How does DIP reduce coupling to external frameworks like `DateTime`?*
- *Did any dependencies remain concrete by choice? Explain your reasoning.*
- *Share an example where inverting a dependency made a test significantly easier to write.*

## 10. Time Estimate

- 45 min — Reading.
- 10 min — Identify concrete dependencies.
- 35 min — Implement clock abstraction and inject.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 5 minutes.

## 11. Implementation Templates

### ISystemClock.cs

Create: `TaskFlowAPI/Infrastructure/Time/ISystemClock.cs`

```csharp
namespace TaskFlowAPI.Infrastructure.Time;

/// <summary>
/// Abstraction for system time to enable deterministic testing.
/// </summary>
public interface ISystemClock
{
    /// <summary>
    /// Gets the current UTC time.
    /// </summary>
    DateTime UtcNow { get; }
}
```

### UtcSystemClock.cs

Create: `TaskFlowAPI/Infrastructure/Time/UtcSystemClock.cs`

```csharp
namespace TaskFlowAPI.Infrastructure.Time;

/// <summary>
/// Production implementation that returns real system time.
/// </summary>
public class UtcSystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
```

### FakeSystemClock.cs (for tests)

Create: `TaskFlowAPI.Tests/Fakes/FakeSystemClock.cs`

```csharp
namespace TaskFlowAPI.Tests.Fakes;

/// <summary>
/// Test implementation that returns controlled time.
/// </summary>
public class FakeSystemClock : ISystemClock
{
    private DateTime _currentTime;
    
    public FakeSystemClock(DateTime initialTime)
    {
        _currentTime = initialTime;
    }
    
    public DateTime UtcNow => _currentTime;
    
    /// <summary>
    /// Advances the clock by the specified duration.
    /// </summary>
    public void Advance(TimeSpan duration)
    {
        _currentTime = _currentTime.Add(duration);
    }
    
    /// <summary>
    /// Sets the clock to a specific time.
    /// </summary>
    public void SetTime(DateTime time)
    {
        _currentTime = time;
    }
}
```

### DI Registration (Program.cs)

```csharp
// Register clock as singleton
builder.Services.AddSingleton<ISystemClock, UtcSystemClock>();
```

### Usage Example

```csharp
public class TaskBusinessRules
{
    private readonly ISystemClock _clock;
    
    public TaskBusinessRules(ISystemClock clock)
    {
        _clock = clock;
    }
    
    public bool IsOverdue(TaskEntity task)
    {
        if (!task.DueDate.HasValue)
            return false;
            
        return task.DueDate.Value < _clock.UtcNow;
    }
}
```

### Test Example

```csharp
[Fact]
public void IsOverdue_WhenDueDateInPast_ReturnsTrue()
{
    // Arrange
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 20));
    var rules = new TaskBusinessRules(fakeClock);
    var task = new TaskEntity { DueDate = new DateTime(2025, 1, 15) };
    
    // Act
    var result = rules.IsOverdue(task);
    
    // Assert
    Assert.True(result);
}
```

## 12. Additional Resources

### Examples

- **[Dependency Inversion Example](../Examples/DependencyInversion.ts)** - Review for additional patterns.

### External Resources

- **[Martin Fowler on Dependency Injection](https://martinfowler.com/articles/injection.html)** - Classic article on DI patterns.
- **[C# Dependency Injection Best Practices](https://docs.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-guidelines)** - Microsoft's official guidance.
- **[TimeProvider in .NET 8](https://learn.microsoft.com/en-us/dotnet/api/system.timeprovider)** - Built-in abstraction (note: we're building our own for learning).

### Optional Deep Dives

- **[SOLID Principles in C#](https://www.pluralsight.com/courses/csharp-solid-principles)** - Comprehensive SOLID course.
- **[Clean Architecture by Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)** - Architectural patterns and DIP.