# Week 19: Essential Design Patterns

This week, we will explore various design patterns and their applications in software development, focusing on how these concepts align with In Time Tec's emphasis on technical excellence and the Quality Manifesto's focus on well-structured code.

## 1. Learning Objectives

- Understand the purpose and benefits of design patterns in software development.
- Identify and differentiate between various design patterns.
- Apply appropriate design patterns to your project to improve code quality and maintainability.
- Implement Factory pattern for creating tasks with context-aware defaults.
- Review and solidify Strategy pattern usage (filters) and Repository pattern.
- Document when each pattern is appropriate within `TaskFlowAPI`.

## 2. Reading & Resources (50 min)

- **[Refactoring Guru: Design Patterns](https://refactoring.guru/design-patterns)** (20 min) - Focus on Factory and Strategy patterns.
- **[Patterns.dev - Creational Patterns](https://www.patterns.dev/posts)** (15 min) - Modern pattern implementations.
- **[Sourcemaking: Design Patterns](https://sourcemaking.com/design_patterns)** (15 min) - Additional explanations with variations.

## 3. This Week's Work

- Create `TaskFactory` responsible for constructing `TaskEntity` instances based on request type (default due dates, priority rules).
- Update `TaskService` to use factory instead of direct entity construction.
- Document existing Strategy usage for filters and ensure it's extensible (no direct instantiation in service).

## 4. Files to Modify

- New: `TaskFlowAPI/Services/Tasks/TaskFactory.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Mapping/TaskMapper.cs` (adjust to rely on factory).
- `TaskFlowAPI/Extensions/ServiceCollectionExtensions.cs` or `Program.cs` (DI registration).
- This file (`Course-Materials/Weekly-Modules/week-19-design-patterns.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-19-submission`.
2. Design `TaskFactory` with method `CreateNewTask(CreateTaskRequest request)` returning a fully initialized entity.
3. Move creation logic (default priority, `CreatedAt`) from mapper/business rules into factory.
4. Inject `ISystemClock` into factory for timestamp generation.
5. Update `TaskService` to call factory before saving.
6. Review filter strategy registration—ensure service receives filters via abstraction (no `new` in service). Refactor if needed.
7. Register factory in DI: `services.AddScoped<TaskFactory>();`.
8. Update tests to inject fake clock into factory.
9. Add XML doc comments summarizing when to use each pattern.
10. Run `dotnet build TaskFlowAPI.sln` and `dotnet test TaskFlowAPI.sln` to verify.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual verification (optional):
- Create a task via Swagger and verify defaults are applied correctly.
- Ensure unit tests using factory still reach 80% coverage.

## 7. Success Criteria

- `TaskService` no longer constructs entities directly; factory handles creation.
- Factory uses injected `ISystemClock` for timestamps.
- Strategy pattern remains intact with DI-based filter registration.
- Tests updated to use factory with fake dependencies.
- Build and tests succeed with no warnings.
- Week 19 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-19-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 19 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Pattern Selection: Document why you chose factory vs. keeping logic in mapper/business rules.*
- *Separation of Concerns: How does the factory clarify the difference between creation and conversion?*
- *Future Pattern Ideas: Note one additional pattern you considered and whether it fits the current scope.*

### Discussion Prep:

- *How did the factory improve creation logic clarity?*
- *What trade-offs exist when adding more patterns?*
- *Which future features could reuse this factory?*
- *How will you guard against over-patterning the codebase?*

## 10. Time Estimate

- 50 min — Reading.
- 10 min — Pattern review and design.
- 40 min — Implement factory, update service and tests.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 15 minutes.

## 11. Factory Pattern Implementation

### TaskFactory Template

Create: `TaskFlowAPI/Services/Tasks/TaskFactory.cs`

```csharp
namespace TaskFlowAPI.Services.Tasks;

/// <summary>
/// Factory for creating TaskEntity instances with context-aware defaults.
/// </summary>
public class TaskFactory
{
    private readonly ISystemClock _clock;
    
    public TaskFactory(ISystemClock clock)
    {
        _clock = clock;
    }
    
    /// <summary>
    /// Creates a new task entity from a request with appropriate defaults.
    /// </summary>
    public TaskEntity CreateNewTask(CreateTaskRequest request)
    {
        return TaskEntity.Create(
            title: request.Title!,
            projectId: request.ProjectId ?? 0,
            description: request.Description,
            priority: request.Priority ?? GetDefaultPriority(),
            dueDate: request.DueDate,
            createdAt: _clock.UtcNow
        );
    }
    
    private int GetDefaultPriority()
    {
        // Default priority logic (can be extended with user context)
        return 2; // Medium priority
    }
}
```

### Updated TaskService Usage

```csharp
public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly TaskFactory _factory;
    private readonly TaskMapper _mapper;
    
    public TaskService(
        ITaskRepository repository,
        TaskFactory factory,
        TaskMapper mapper)
    {
        _repository = repository;
        _factory = factory;
        _mapper = mapper;
    }
    
    public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
    {
        // Factory creates entity with defaults
        var entity = _factory.CreateNewTask(request);
        
        // Repository saves
        var created = await _repository.CreateAsync(entity);
        
        // Mapper converts to DTO
        return _mapper.ToDto(created);
    }
}
```

### Factory vs Mapper Responsibilities

**TaskFactory (Creates):**
- Request → Entity (with defaults).
- Applies business rules for creation.
- Context-aware initialization.

**TaskMapper (Converts):**
- Entity → DTO.
- DTO → Entity (for updates only, not creation).
- Pure conversion, no defaults.

### DI Registration

```csharp
// ServiceCollectionExtensions.cs
public static IServiceCollection AddTaskFlowServices(this IServiceCollection services)
{
    services.AddScoped<TaskFactory>();
    services.AddScoped<ITaskService, TaskService>();
    services.AddScoped<TaskMapper>();
    // ... other services
    
    return services;
}
```

### Test Example

```csharp
[Fact]
public async Task CreateTaskAsync_UsesFactory()
{
    // Arrange
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 15));
    var factory = new TaskFactory(fakeClock);
    var fakeRepo = new FakeTaskRepository();
    var service = new TaskService(fakeRepo, factory, new TaskMapper());
    
    var request = new CreateTaskRequest { Title = "Test Task" };
    
    // Act
    var result = await service.CreateTaskAsync(request);
    
    // Assert
    result.CreatedAt.Should().Be(new DateTime(2025, 1, 15));
}
```

## 12. Patterns Review

### Patterns Already in TaskFlowAPI

**Repository Pattern (Week 8):**
- Abstracts data access.
- `ITaskRepository` hides EF Core details.

**Strategy Pattern (Week 12):**
- Encapsulates algorithms (filtering).
- `ITaskFilter` implementations injected via DI.

**Factory Pattern (Week 19):**
- Encapsulates object creation.
- `TaskFactory` handles entity initialization.

### When to Use Each Pattern

| Pattern | Use When | Example in TaskFlowAPI |
|---------|----------|------------------------|
| Factory | Complex object creation with defaults | `TaskFactory.CreateNewTask()` |
| Strategy | Algorithm selection at runtime | `ITaskFilter` implementations |
| Repository | Abstract data access layer | `ITaskRepository` |

## 13. Additional Resources

### Examples

- **[Design Patterns Example](../Examples/DesignPatterns.md)** - Review for pattern implementations.

### External Resources

- **[Refactoring Guru: Factory Pattern](https://refactoring.guru/design-patterns/factory-method)** - Detailed pattern explanation.
- **[C# Design Patterns](https://www.dofactory.com/net/design-patterns)** - .NET-specific implementations.

### Optional Deep Dives

- **[YouTube: 10 Design Patterns Explained in 10 Minutes](https://www.youtube.com/watch?v=tv-_1er1mWI)** (10 min) - High-level tour.
- **[Tutorialspoint Design Patterns Overview](https://www.tutorialspoint.com/design_pattern/design_pattern_overview.htm)** - Quick refresher on categories.