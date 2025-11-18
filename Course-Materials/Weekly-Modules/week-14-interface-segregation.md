# Week 14: Interface Segregation Principle (ISP)

This week, we are focusing on the Interface Segregation Principle (ISP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the ISP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Split “fat” interfaces into focused contracts.
- Update implementations and consumers to depend only on what they use.
- Ensure DI configuration honours the new abstractions.

## 2. Reading & Resources (60 min)

- **[Interface Segregation Principle - Wikipedia](https://en.wikipedia.org/wiki/Interface_segregation_principle)** - Original definition and history from Robert C. Martin's work at Xerox.
- **[ISP in Practice - Dev.to](https://dev.to/paulocappa/solid-i-interface-segregation-principle-isp-385f)** - Comprehensive guide with practical code examples.
- **[ISP Implementation - ByteHide](https://www.bytehide.com/blog/interface-segregation-principle-in-csharp-solid-principles)** - Detailed examples and best practices in C#.
- Optional: **[ISP Design Patterns - OODesign](https://www.oodesign.com/interface-segregation-principle)** - Advanced patterns and implementation strategies.

## 3. This Week’s Work

- Create `ITaskReader` and `ITaskWriter` interfaces.
- Update repository implementation to implement both.
- Update services/controllers to depend on the appropriate interface(s).

## 4. Files to Modify

- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (split or replace)
- New files: `ITaskReader.cs`, `ITaskWriter.cs`
- `TaskFlowAPI/Repositories/TaskRepository.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Program.cs`
- This file (`Course-Materials/Weekly-Modules/week-14-interface-segregation.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-14/<your-name>`.
2. Create new interfaces:
   - `ITaskReader`: `GetAllAsync`, `GetByIdAsync`.
   - `ITaskWriter`: `CreateAsync`, `UpdateAsync`, `DeleteAsync`.
3. Update `TaskRepository` to implement both interfaces. Remove obsolete combined interface.
4. Update DI registrations: register concrete type for both reader and writer (scoped).
5. Update `TaskService` constructor to depend on required interfaces (likely both).
6. Update controller or other consumers to request only the reader interface when appropriate.
7. Adjust tests/fakes to mirror new interfaces.
8. Build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- No files depend on unused repository methods.
- DI container resolves service with split interfaces.
- Tests compile and pass with new abstractions.

## 8. Submission Process

- Commit `Week 14 – interface segregation`.
- PR summary explains who consumes reader vs. writer.
- Weekly issue includes diagram or table of dependencies after refactor.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Interface Audit:* List one consumer and the minimal interface it now depends on.
- *Dependency Graph:* Sketch how DI registrations changed; note any surprising impacts.

### Discussion Prep:
- *What benefits did you notice after splitting interfaces?*
- *Could any service depend on only `ITaskReader` now?*
- *Where else could ISP apply in this codebase?*
- *What migration steps would be required if you introduced additional specialised writers/readers later?*

## 10. Time Estimate

- 60 min – Reading.
- 10 min – Plan new interfaces.
- 35 min – Implement + update consumers.
- 15 min – Test + PR/issue.
**Total:** ~120 minutes.

## 11. Scaffolding & Templates

### TODO Comments (Add to ITaskRepository.cs):

```csharp
namespace TaskFlowAPI.Repositories.Interfaces;

// TODO Week 14: This interface violates ISP - it forces clients to depend on methods they don't use
// TODO: Create ITaskReader interface with read-only methods (GetAllAsync, GetByIdAsync)
// TODO: Create ITaskWriter interface with write-only methods (CreateAsync, UpdateAsync, DeleteAsync)
// TODO: Update TaskRepository to implement both ITaskReader and ITaskWriter

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAllAsync();        // Read
    Task<TaskEntity?> GetByIdAsync(int id);      // Read
    Task<TaskEntity> CreateAsync(TaskEntity e);  // Write
    Task UpdateAsync(TaskEntity entity);         // Write
    Task DeleteAsync(TaskEntity entity);         // Write
}
```

### Template: ITaskReader.cs

```csharp
namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// Read-only operations for querying tasks.
/// Use for: Reports, dashboards, query services, read-only APIs
/// </summary>
public interface ITaskReader
{
    Task<List<TaskEntity>> GetAllAsync();
    Task<TaskEntity?> GetByIdAsync(int id);
}
```

### Template: ITaskWriter.cs

```csharp
namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// Write operations for modifying tasks.
/// Use for: Create/Update/Delete services, import jobs, admin operations
/// </summary>
public interface ITaskWriter
{
    Task<TaskEntity> CreateAsync(TaskEntity entity);
    Task UpdateAsync(TaskEntity entity);
    Task DeleteAsync(TaskEntity entity);
}
```

### Registering One Class as Two Interfaces

In `Program.cs` or `ServiceCollectionExtensions.cs`:

```csharp
// Register concrete class once
builder.Services.AddScoped<TaskRepository>();

// Register as ITaskReader (factory delegates to same instance)
builder.Services.AddScoped<ITaskReader>(sp => 
    sp.GetRequiredService<TaskRepository>());

// Register as ITaskWriter (factory delegates to same instance)
builder.Services.AddScoped<ITaskWriter>(sp => 
    sp.GetRequiredService<TaskRepository>());
```

**Why this pattern?**

1. **One concrete instance** (efficient) - Only one `TaskRepository` object created per scope
2. **Two interface registrations** (flexible) - Clients can request either `ITaskReader` or `ITaskWriter`
3. **Factory delegation** - Both interfaces resolve to the same underlying `TaskRepository` instance

**How it works:**

```csharp
// When TaskService requests both interfaces:
public class TaskService
{
    public TaskService(ITaskReader reader, ITaskWriter writer)
    {
        // Both parameters reference THE SAME TaskRepository instance
        // reader == writer (in terms of object identity)
    }
}

// DI container flow:
// 1. Create ONE TaskRepository instance (scoped)
// 2. When ITaskReader requested → return that instance
// 3. When ITaskWriter requested → return that same instance
// Result: Both injections point to same object
```

### Benefits:

- Clients only depend on methods they use
- Easy to test (mock only needed interface)
- Can swap implementations per interface later (advanced)
- Clear separation of concerns

## 12. Additional Resources

- **[Interface Segregation Example](../Examples/InterfaceSegregation.cs)**
- **[Interface Segregation in TypeScript - LinkedIn](https://www.linkedin.com/pulse/interface-segregation-principle-typescript-dhananjay-kumar)** - Modern implementation with TypeScript.
