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

Note (optional):

- If you prefer all cross-cutting collaborators to be interface-backed, consider adding a small task to create `ITaskBusinessRules` (see Week 11). Creating `ITaskBusinessRules` lets business-rule logic be injected via an interface (like other infra abstractions) and simplifies mocking in tests. Files to modify would be `TaskFlowAPI/Services/Tasks/Rules/ITaskBusinessRules.cs` and making `TaskBusinessRules` implement it; update `Program.cs` to register the interface.

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
6. In tests, create fake clock for deterministic behavior.
7. Audit constructors for any other concrete types (e.g., `TaskRepository` uses `TaskFlowDbContext`—acceptable). Replace direct instantiations with DI where needed.
8. Build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Ensure tests use fake clock to verify behaviors that depend on time.

## 7. Success Criteria

- No direct `DateTime.UtcNow` usages in business logic (all go through `ISystemClock`).
- Services depend only on interfaces.
- Tests prove ability to swap clock implementation.
- Build/tests succeed.

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-15-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 15 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

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
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~120 minutes.

## 11. Why Abstract System Time?

### Problem with `DateTime.UtcNow`:

**Hard to Test:**
- Cannot control time in tests
- Tests break if time-sensitive logic fails
- Cannot test "created 5 minutes ago" scenarios
- Every test run produces different timestamps

**Example of the problem:**

```csharp
// BEFORE (Direct Dependency - Hard to Test):
public class TaskService
{
    public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
    {
        var entity = new TaskEntity
        {
            Title = request.Title,
            CreatedAt = DateTime.UtcNow  // ❌ Can't control in tests
        };
        
        await _repository.CreateAsync(entity);
        return MapToDto(entity);
    }
}

// Test problems:
[Fact]
public async Task CreateTask_SetsCreatedAt()
{
    // ❌ How do we verify CreatedAt is "now"?
    // ❌ Test will have race conditions
    // ❌ Can't test time-based logic deterministically
    var result = await _service.CreateTaskAsync(request);
    
    // This assertion is flaky:
    Assert.True((DateTime.UtcNow - result.CreatedAt).TotalSeconds < 1);
}
```

### Solution: Invert the dependency

**AFTER (Abstraction - Easy to Test):**

```csharp
public class TaskService
{
    private readonly ISystemClock _clock;  // ✓ Abstraction injected
    
    public TaskService(ISystemClock clock, ...)
    {
        _clock = clock;
    }
    
    public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
    {
        var entity = new TaskEntity
        {
            Title = request.Title,
            CreatedAt = _clock.UtcNow  // ✓ Can inject FakeClock in tests
        };
        
        await _repository.CreateAsync(entity);
        return MapToDto(entity);
    }
}

// Test benefits:
[Fact]
public async Task CreateTask_SetsCreatedAt()
{
    // ✓ Deterministic time control
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 15, 10, 30, 0));
    var service = new TaskService(fakeClock, ...);
    
    var result = await _service.CreateTaskAsync(request);
    
    // ✓ Exact assertion, no flakiness
    Assert.Equal(new DateTime(2025, 1, 15, 10, 30, 0), result.CreatedAt);
}
```

### Benefits:

1. **Deterministic tests** - Always returns same time
2. **Test time-based logic** - Can advance time in tests
3. **No race conditions** - Time is controlled
4. **Test edge cases** - Easily test past/future scenarios

**Advanced testing example:**

```csharp
[Fact]
public async Task GetOverdueTasks_ReturnsTasksDueBeforeNow()
{
    // Set "now" to specific date
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 20));
    
    // Create task due on Jan 15 (5 days ago)
    var overdueTask = new TaskEntity 
    { 
        DueDate = new DateTime(2025, 1, 15) 
    };
    
    // Test overdue logic with controlled time
    var overdueTasks = await _service.GetOverdueTasksAsync();
    
    // ✓ Clear assertion
    Assert.Contains(overdueTask, overdueTasks);
}

[Fact]
public async Task TaskExpires_AfterSevenDays()
{
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 1));
    var service = new TaskService(fakeClock, ...);
    
    // Create task
    var task = await service.CreateTaskAsync(request);
    
    // Advance time by 7 days
    fakeClock.Advance(TimeSpan.FromDays(7));
    
    // Verify expiration logic
    var isExpired = await service.IsTaskExpiredAsync(task.Id);
    Assert.True(isExpired);
}
```

## 12. Scaffolding & Templates

### Template: ISystemClock.cs

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

### Template: UtcSystemClock.cs

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

### Template: FakeSystemClock.cs (for tests)

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
    /// Useful for testing time-based behavior.
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

### DI Registration (Program.cs):

```csharp
// Register clock as singleton (same time for entire application lifetime per instance)
builder.Services.AddSingleton<ISystemClock, UtcSystemClock>();
```

### Usage in Services:

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

### Usage in Tests:

```csharp
[Fact]
public void IsOverdue_WhenDueDateInPast_ReturnsTrue()
{
    // Arrange
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 20));
    var rules = new TaskBusinessRules(fakeClock);
    var task = new TaskEntity 
    { 
        DueDate = new DateTime(2025, 1, 15) // 5 days ago
    };
    
    // Act
    var result = rules.IsOverdue(task);
    
    // Assert
    Assert.True(result);
}
```

## 13. Additional Resources

- **[Dependency Inversion Example](../Examples/DependencyInversion.ts)**
