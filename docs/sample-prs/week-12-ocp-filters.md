# Sample PR: Week 12 - Open/Closed Principle (Task Filters)

## PR Description
Implemented extensible task filtering system using Strategy pattern to comply with Open/Closed Principle. Filters can now be added without modifying existing service code.

## Changes Made
- Created filter interfaces and concrete implementations
- Implemented `CompositeTaskFilter` for combining multiple filters
- Updated `TaskService` to accept and apply filters
- Updated controller to parse query parameters into filters

## Files Modified
- `TaskFlowAPI/Services/Tasks/Filters/ITaskFilter.cs` (new)
- `TaskFlowAPI/Services/Tasks/Filters/StatusTaskFilter.cs` (new)
- `TaskFlowAPI/Services/Tasks/Filters/PriorityTaskFilter.cs` (new)
- `TaskFlowAPI/Services/Tasks/Filters/CompositeTaskFilter.cs` (new)
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Controllers/TasksController.cs`

---

## Code Diff

### ITaskFilter.cs (NEW FILE)

```csharp
namespace TaskFlowAPI.Services.Tasks.Filters;

public interface ITaskFilter
{
    bool IsMatch(TaskDto task);
}
```

### StatusTaskFilter.cs (NEW FILE)

```csharp
namespace TaskFlowAPI.Services.Tasks.Filters;

public class StatusTaskFilter : ITaskFilter
{
    private readonly bool _isComplete;

    public StatusTaskFilter(bool isComplete)
    {
        _isComplete = isComplete;
    }

    public bool IsMatch(TaskDto task)
    {
        return task.IsComplete == _isComplete;
    }
}
```

### PriorityTaskFilter.cs (NEW FILE)

```csharp
namespace TaskFlowAPI.Services.Tasks.Filters;

public class PriorityTaskFilter : ITaskFilter
{
    private readonly List<int> _priorities;

    public PriorityTaskFilter(List<int> priorities)
    {
        _priorities = priorities ?? new List<int>();
    }

    public bool IsMatch(TaskDto task)
    {
        return _priorities.Contains(task.Priority);
    }
}
```

### CompositeTaskFilter.cs (NEW FILE)

```csharp
namespace TaskFlowAPI.Services.Tasks.Filters;

public class CompositeTaskFilter : ITaskFilter
{
    private readonly List<ITaskFilter> _filters;

    public CompositeTaskFilter(List<ITaskFilter> filters)
    {
        _filters = filters ?? new List<ITaskFilter>();
    }

    public bool IsMatch(TaskDto task)
    {
        // All filters must match
        return _filters.All(filter => filter.IsMatch(task));
    }
}
```

### TaskService.cs (MODIFIED)

**BEFORE:**
```csharp
public async Task<List<TaskDto>> GetAllTasksAsync()
{
    var tasks = await _taskReader.GetAllAsync();
    return tasks.Select(t => _mapper.ToDto(t)).ToList();
}
```

**AFTER:**
```csharp
public async Task<List<TaskDto>> GetAllTasksAsync(ITaskFilter? filter = null)
{
    var tasks = await _taskReader.GetAllAsync();
    var dtos = tasks.Select(t => _mapper.ToDto(t));
    
    if (filter != null)
    {
        dtos = dtos.Where(filter.IsMatch);
    }
    
    return dtos.ToList();
}
```

### TasksController.cs (MODIFIED)

```csharp
[HttpGet]
public async Task<ActionResult<List<TaskDto>>> GetTasks(
    [FromQuery] bool? isComplete,
    [FromQuery] string? priorities)
{
    var filters = new List<ITaskFilter>();
    
    if (isComplete.HasValue)
    {
        filters.Add(new StatusTaskFilter(isComplete.Value));
    }
    
    if (!string.IsNullOrEmpty(priorities))
    {
        var priorityList = priorities.Split(',')
            .Select(int.Parse)
            .ToList();
        filters.Add(new PriorityTaskFilter(priorityList));
    }
    
    var compositeFilter = filters.Any() 
        ? new CompositeTaskFilter(filters) 
        : null;
    
    var tasks = await _taskService.GetAllTasksAsync(compositeFilter);
    return Ok(tasks);
}
```

---

## Review Assignment

**Your task:** Review this PR and provide feedback. Leave at least **3 comments** noting:

### What's Done Well:

1. **Excellent OCP Implementation**: Adding new filters (e.g., `DueDateFilter`) requires only creating a new class—no modifications to existing code. This perfectly demonstrates the Open/Closed Principle.

2. **Clean Strategy Pattern**: Each filter is a focused class with a single responsibility. The `CompositeTaskFilter` elegantly combines filters using standard LINQ operations.

3. **Extensible Design**: The filter system can be extended to support OR logic, NOT logic, or more complex compositions without touching existing filter implementations.

### Minor Suggestions:

1. **Null Safety**: The `PriorityTaskFilter` constructor handles null `priorities`, but consider what happens if the list is empty—should it match all tasks or none?

2. **Controller Parsing**: The `int.Parse` in the controller could throw if non-numeric values are passed. Consider using `int.TryParse` with validation.

3. **Filter Factory**: Consider creating a `TaskFilterFactory` to centralize filter creation from query parameters, keeping the controller even cleaner.

---

## Expected Review Comments

### Example High-Quality Comments:

**Comment 1 (Praise - OCP):**
> Excellent implementation of the Open/Closed Principle! Adding a new filter type (like `DueDateFilter`) now requires only:
> 1. Create new class implementing `ITaskFilter`
> 2. Parse query param in controller
> 3. Add to filters list
> 
> No modification of existing filters or service logic needed. This is exactly what OCP aims to achieve—software entities open for extension but closed for modification.

**Comment 2 (Improvement - Error Handling):**
> Consider adding validation in the controller for the `priorities` parameter:
> ```csharp
> if (!string.IsNullOrEmpty(priorities))
> {
>     var priorityList = new List<int>();
>     foreach (var p in priorities.Split(','))
>     {
>         if (int.TryParse(p, out var priority))
>             priorityList.Add(priority);
>         else
>             return BadRequest($"Invalid priority value: {p}");
>     }
>     filters.Add(new PriorityTaskFilter(priorityList));
> }
> ```
> This prevents exceptions from malformed input like `?priorities=high,1,two`.

**Comment 3 (Suggestion - Factory Pattern):**
> As the number of supported filters grows, consider extracting filter creation into a `TaskFilterFactory`:
> ```csharp
> public class TaskFilterFactory
> {
>     public ITaskFilter CreateFromQueryParams(TaskFilterParams params)
>     {
>         var filters = new List<ITaskFilter>();
>         // Centralize all filter creation logic here
>         return new CompositeTaskFilter(filters);
>     }
> }
> ```
> This would keep the controller focused on HTTP concerns while isolating filter creation logic.

---

## Testing Instructions

1. Build: `dotnet build TaskFlowAPI.sln`
2. Run tests: `dotnet test TaskFlowAPI.sln`
3. Manual testing via Swagger:
   - All tasks: `GET /api/v1/tasks`
   - Completed only: `GET /api/v1/tasks?isComplete=true`
   - High priority: `GET /api/v1/tasks?priorities=4,5`
   - Combined: `GET /api/v1/tasks?isComplete=false&priorities=3,4,5`
4. Verify correct filtering in each case

## How This Demonstrates OCP

**Before (Violates OCP):**
```csharp
// Adding new filter type requires modifying this method
public async Task<List<TaskDto>> GetAllTasksAsync(bool? isComplete, int? priority)
{
    var tasks = await _taskReader.GetAllAsync();
    
    if (isComplete.HasValue)
        tasks = tasks.Where(t => t.IsComplete == isComplete.Value);
    
    if (priority.HasValue)
        tasks = tasks.Where(t => t.Priority == priority.Value);
    
    // Every new filter = another parameter + another if block
    return tasks.ToList();
}
```

**After (Follows OCP):**
```csharp
// Adding new filter = new class only, this method unchanged
public async Task<List<TaskDto>> GetAllTasksAsync(ITaskFilter? filter = null)
{
    var tasks = await _taskReader.GetAllAsync();
    var dtos = tasks.Select(t => _mapper.ToDto(t));
    
    if (filter != null)
        dtos = dtos.Where(filter.IsMatch);
    
    return dtos.ToList();
}
```

**Extension Example (No Existing Code Modified):**
```csharp
// New filter added without touching TaskService or existing filters
public class DueDateFilter : ITaskFilter
{
    private readonly DateTime _before;
    
    public DueDateFilter(DateTime before) => _before = before;
    
    public bool IsMatch(TaskDto task) 
        => task.DueDate.HasValue && task.DueDate.Value < _before;
}
```
