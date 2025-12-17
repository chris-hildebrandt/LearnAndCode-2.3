# Sample PR: Week 17 - TDD CompleteTask Implementation

## PR Description
Implemented `CompleteTaskAsync` method using Test-Driven Development (TDD) as per Week 17 requirements.

## Changes Made
- Added `CompleteTaskAsync` to `ITaskService` interface
- Implemented method in `TaskService` with completion logic
- Created comprehensive unit tests for happy path and edge cases

## Files Modified
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Entities/TaskEntity.cs`
- `TaskFlowAPI.Tests/Unit/TaskServiceTests.cs`

---

## Code Diff

### ITaskService.cs (MODIFIED)

```csharp
public interface ITaskService
{
    Task<List<TaskDto>> GetAllTasksAsync();
    Task<TaskDto?> GetByIdAsync(int id);
    Task<TaskDto> CreateTaskAsync(CreateTaskRequest request);
    Task<TaskDto> CompleteTaskAsync(int taskId); // NEW
}
```

### TaskEntity.cs (MODIFIED)

```csharp
public class TaskEntity
{
    // ... existing properties
    
    public void Complete(DateTime completedAt)
    {
        IsComplete = true;
        CompletedAt = completedAt;
    }
}
```

### TaskService.cs (MODIFIED)

```csharp
public async Task<TaskDto> CompleteTaskAsync(int taskId)
{
    var task = _taskReader.GetByIdAsync(taskId);
    
    if (task == null)
        throw new TaskNotFoundException(taskId);
    
    task.Complete(_clock.UtcNow);
    
    await _taskWriter.UpdateAsync(task);
    
    return _mapper.ToDto(task);
}
```

### TaskServiceTests.cs (NEW TESTS)

```csharp
[Fact]
public async Task CompleteTaskAsync_WhenTaskExists_MarksTaskComplete()
{
    // Arrange
    var taskId = 1;
    var existingTask = new TaskEntity 
    { 
        Id = taskId, 
        Title = "Test", 
        IsComplete = false 
    };
    
    _mockReader.Setup(r => r.GetByIdAsync(taskId))
        .ReturnsAsync(existingTask);
    
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 1));
    var service = new TaskService(_mockReader.Object, _mockWriter.Object, 
        _mapper, fakeClock);
    
    // Act
    var result = await service.CompleteTaskAsync(taskId);
    
    // Assert
    Assert.True(existingTask.IsComplete);
    Assert.Equal(new DateTime(2025, 1, 1), existingTask.CompletedAt);
}

[Fact]
public async Task CompleteTaskAsync_WhenTaskNotFound_ThrowsException()
{
    // Arrange
    _mockReader.Setup(r => r.GetByIdAsync(999))
        .ReturnsAsync((TaskEntity?)null);
    
    // Act & Assert
    await Assert.ThrowsAsync<TaskNotFoundException>(
        () => _service.CompleteTaskAsync(999)
    );
}
```

---

## Review Assignment

**Your task:** Review this PR and identify issues. Leave at least **3 comments** addressing:

### Issues to Identify:

1. **Missing Await Keyword**: Line 3 in `CompleteTaskAsync` is missing `await` before `_taskReader.GetByIdAsync(taskId)`. This will cause a compilation error since the method returns `Task<TaskEntity?>`, not `TaskEntity?`.

2. **Missing Test Coverage**: There's no test for completing an already completed task. What happens if `CompleteTaskAsync` is called twice? Should it throw an exception or be idempotent?

3. **Domain Logic Leak**: The `Complete()` method on `TaskEntity` should probably check if the task is already complete and throw a domain exception. Currently, it blindly overwrites the completion state.

### Positive Aspects to Praise:

1. **TDD Approach**: Tests were written first (evident from test structure) following the Red-Green-Refactor cycle.

2. **Fake Clock Usage**: Excellent use of `FakeSystemClock` for deterministic testing of time-dependent logic.

---

## Expected Review Comments

### Example High-Quality Comments:

**Comment 1 (Critical Bug):**
> **Critical:** Missing `await` keyword on line 3 of `CompleteTaskAsync`:
> ```csharp
> var task = await _taskReader.GetByIdAsync(taskId); // Add await here
> ```
> Without this, the code won't compile because you're trying to assign a `Task<TaskEntity?>` to a variable of type `TaskEntity?`. This will cause a build failure.

**Comment 2 (Missing Test):**
> Consider adding a test for the scenario where a task is already completed:
> ```csharp
> [Fact]
> public async Task CompleteTaskAsync_WhenTaskAlreadyComplete_ThrowsOrReturns()
> {
>     // Test behavior when completing an already completed task
> }
> ```
> Should this operation be idempotent (succeed silently) or throw an exception? This is an important business rule to clarify.

**Comment 3 (Positive Feedback):**
> Great job following TDD principles! The tests are clear, focused, and use the Arrange-Act-Assert pattern consistently. The use of `FakeSystemClock` is especially nice—it makes the tests deterministic and easy to understand. This is exactly how time-dependent logic should be tested.

---

## Testing Instructions

1. Build: `dotnet build TaskFlowAPI.sln` (**Note: Will fail due to missing await**)
2. Run tests: `dotnet test TaskFlowAPI.sln`
3. Manual test via Swagger:
   - Create a task: `POST /api/v1/tasks`
   - Complete it: `POST /api/v1/tasks/{id}/complete`
   - Verify `isComplete = true` and `completedAt` is set
