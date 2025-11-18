# Sample PR: Week 19 - Factory Pattern Implementation

## PR Description
This PR implements the Factory pattern for creating TaskEntity instances with context-aware defaults as per Week 19 requirements.

## Changes Made
- Created `TaskFactory` class with `CreateNewTask` method
- Updated `TaskService` to use factory instead of direct entity construction
- Registered factory in DI container

## Files Modified
- `TaskFlowAPI/Services/Tasks/TaskFactory.cs` (new)
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Program.cs`

---

## Code Diff

### TaskFlowAPI/Services/Tasks/TaskFactory.cs (NEW FILE)

```csharp
namespace TaskFlowAPI.Services.Tasks;

public class TaskFactory
{
    private readonly ISystemClock _clock;

    public TaskFactory(ISystemClock clock)
    {
        _clock = clock;
    }

    public TaskEntity CreateNewTask(CreateTaskRequest request)
    {
        return new TaskEntity
        {
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority ?? 0,
            DueDate = request.DueDate,
            ProjectId = request.ProjectId,
            CreatedAt = _clock.UtcNow,
            IsComplete = false
        };
    }
}
```

### TaskFlowAPI/Services/Tasks/TaskService.cs (MODIFIED)

**BEFORE:**
```csharp
public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
{
    var entity = new TaskEntity
    {
        Title = request.Title,
        Description = request.Description,
        Priority = request.Priority ?? 0,
        ProjectId = request.ProjectId,
        CreatedAt = DateTime.UtcNow
    };
    
    var created = await _taskWriter.CreateAsync(entity);
    return _mapper.ToDto(created);
}
```

**AFTER:**
```csharp
public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
{
    var entity = _factory.CreateNewTask(request);
    var created = await _taskWriter.CreateAsync(entity);
    return _mapper.ToDto(created);
}
```

### TaskFlowAPI/Program.cs (MODIFIED)

**BEFORE:**
```csharp
builder.Services.AddScoped<ITaskService, TaskService>();
```

**AFTER:**
```csharp
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<TaskFactory>();
```

---

## Review Assignment

**Your task:** Review this PR and identify issues. Leave at least **3 comments** addressing:

### Issues to Identify:

1. **Missing Validation**: The factory doesn't validate that `request.Title` is not null/empty before creating the entity. This could allow invalid entities to be created.

2. **Default Priority Logic**: The `Priority = request.Priority ?? 0` logic is in the factory, but what if business rules say priority should be 3 by default? This seems like business logic leaking into the factory.

3. **Missing Constructor Injection**: The `TaskService` constructor isn't shown being updated to inject `TaskFactory`. This will cause a compilation error.

### Positive Aspects to Praise:

1. **Good Separation**: The factory clearly separates entity creation from mapping, improving SRP adherence.

2. **Clock Abstraction**: Nice use of `ISystemClock` for testability - no direct `DateTime.UtcNow` calls.

---

## Expected Review Comments

### Example High-Quality Comments:

**Comment 1 (Validation Issue):**
> The `CreateNewTask` method should validate that `request.Title` is not null or empty before creating the entity. Without this, invalid entities can be created. Consider adding:
> ```csharp
> if (string.IsNullOrWhiteSpace(request.Title))
>     throw new ArgumentException("Title cannot be empty", nameof(request));
> ```
> This protects against bugs where validation might be bypassed.

**Comment 2 (Missing DI):**
> The `TaskService` constructor needs to be updated to inject `TaskFactory`. Currently this code won't compile because `_factory` is referenced but not provided. Please update the constructor:
> ```csharp
> public TaskService(ITaskWriter taskWriter, TaskMapper mapper, TaskFactory factory, ...)
> {
>     // ... existing code
>     _factory = factory;
> }
> ```

**Comment 3 (Positive Feedback):**
> Excellent use of the Factory pattern here! This separation of concerns makes it much easier to add context-aware defaults in the future (like setting default priority based on user role). The entity creation logic is now isolated and testable independently of the service layer.

---

## Testing Instructions

1. Build the solution: `dotnet build TaskFlowAPI.sln`
2. Run tests: `dotnet test TaskFlowAPI.sln`
3. Test endpoint: `POST /api/v1/tasks` with payload:
   ```json
   {
     "title": "Test Task",
     "projectId": 1,
     "priority": null
   }
   ```
4. Verify task created with `Priority = 0` and `CreatedAt` set correctly
