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

### Step 0: Study Example Implementation (10 minutes - NEW)

**Before implementing NotImplemented methods, study this WORKING example:**

**Example: GetAllAsync** (fully implemented pattern to learn from)

```csharp
public async Task<List<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default)
{
    return await _dbContext.Tasks      // 1. Start with DbSet
        .AsNoTracking()                // 2. Read-only optimization
        .Include(t => t.Project)       // 3. Load navigation property
        .OrderBy(t => t.Priority)      // 4. Sort by priority
        .ThenBy(t => t.DueDate)        // 5. Then by due date
        .ToListAsync(cancellationToken);  // 6. Execute async
}
```

**Pattern Breakdown:**
1. **`_dbContext.Tasks`** - The table (DbSet<TaskEntity>)
2. **`AsNoTracking()`** - "I'm only reading, don't track changes" (performance optimization)
3. **`Include(t => t.Project)`** - "Load the Project navigation property too" (eager loading)
4. **`OrderBy/ThenBy`** - Sort results (business logic: priority first, then due date)
5. **`ToListAsync(cancellationToken)`** - Execute query asynchronously and wait for results

**Key Concepts Explained:**
- **`await`** - "Wait for database to respond, don't block the thread"
- **`CancellationToken`** - "User can cancel if operation is slow"
- **`AsNoTracking`** - "I won't call SaveChanges() on these entities, so don't track them"

**When to use AsNoTracking:**
- ✅ Read-only queries (GET operations)
- ❌ NOT for Update/Delete (need tracking to detect changes)

---

### Your Implementation Tasks

Now implement the remaining methods using the pattern above:

1. Branch `week-08/<your-name>`.

2. **Task 1: GetByIdAsync** (15 min)  
   **Pattern:** Similar to GetAllAsync, but use `.FirstOrDefaultAsync(t => t.Id == id)`
   
   **Self-Check After Implementation:**
   - [ ] Used `AsNoTracking()`?
   - [ ] Included `Project` navigation property?
   - [ ] Returns `null` if not found (NOT exception)?
   - [ ] Passed `cancellationToken`?
   
   ```csharp
   // Template to guide you:
   public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
   {
       return await _dbContext.Tasks
           .AsNoTracking()              // TODO: Add this
           .Include(t => t.Project)     // TODO: Add this
           .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);  // Returns null if not found
   }
   ```

3. **Task 2: CreateAsync** (15 min)  
   **Pattern:** `Add` entity, then `SaveChangesAsync`
   
   **Self-Check After Implementation:**
   - [ ] Called `SaveChangesAsync()`?
   - [ ] Returned entity with generated `Id`?
   - [ ] Used `cancellationToken` in SaveChangesAsync?
   
   ```csharp
   // Template:
   public async Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken cancellationToken = default)
   {
       await _dbContext.Tasks.AddAsync(entity, cancellationToken);
       await _dbContext.SaveChangesAsync(cancellationToken);  // This generates the Id
       return entity;  // Now has Id populated
   }
   ```

4. **Task 3: UpdateAsync** (20 min)  
   **Pattern:** `Update` entity, then `SaveChangesAsync`  
   **Note:** EF Core must be tracking the entity to detect changes
   
   **Self-Check:**
   - [ ] Called `SaveChangesAsync()`?
   - [ ] Entity is tracked (don't use AsNoTracking for updates)?
   - [ ] Used `cancellationToken`?

5. **Task 4: DeleteAsync** (15 min)  
   **Pattern:** Find entity, `Remove`, then `SaveChangesAsync`  
   **Important:** Silently succeed if entity doesn't exist (idempotent operation)
   
   **Self-Check:**
   - [ ] Null check (don't throw if missing)?
   - [ ] Called `SaveChangesAsync()` only if entity found?
   - [ ] Used `cancellationToken`?

6. **Optional:** Add `cancellationToken.ThrowIfCancellationRequested()` at method start for long operations

7. Run build/tests: `dotnet build && dotnet test`

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

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-08-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 8 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

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
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~120 minutes.

## 11. Testing Your Repository Without Running the Full App (NEW)

**Why test repositories directly?**
- ✅ Faster than starting full app + Swagger
- ✅ Tests survive refactoring (Swagger tests don't)
- ✅ Can test edge cases easily (null scenarios, cancellation)
- ✅ Prepares you for Week 17 (Unit Testing & TDD)

### Option A: Quick In-Memory Test (5 minutes)

Create `TaskFlowAPI.Tests/Unit/TaskRepositoryTests.cs`:

```csharp
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TaskFlowAPI.Data;
using TaskFlowAPI.Entities;
using TaskFlowAPI.Repositories;
using Xunit;

namespace TaskFlowAPI.Tests.Unit;

public class TaskRepositoryTests
{
    private TaskFlowDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())  // Unique DB per test
            .Options;
        
        return new TaskFlowDbContext(options);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsTasksOrderedByPriority()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TaskRepository(context);
        
        // Add test data
        var project = new ProjectEntity { Id = 1, Name = "Test Project" };
        context.Projects.Add(project);
        
        var task1 = TaskEntity.Create("Low priority task", 1);
        task1.Priority = 5;
        
        var task2 = TaskEntity.Create("High priority task", 1);
        task2.Priority = 1;
        
        context.Tasks.AddRange(task1, task2);
        await context.SaveChangesAsync();
        
        // Act
        var result = await repository.GetAllAsync();
        
        // Assert
        result.Should().HaveCount(2);
        result.First().Title.Should().Be("High priority task");  // Priority 1 comes first
        result.Last().Title.Should().Be("Low priority task");    // Priority 5 comes last
    }
    
    [Fact]
    public async Task GetByIdAsync_WhenNotFound_ReturnsNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TaskRepository(context);
        
        // Act
        var result = await repository.GetByIdAsync(999);  // ID doesn't exist
        
        // Assert
        result.Should().BeNull();  // Important: returns null, not exception!
    }
    
    [Fact]
    public async Task CreateAsync_GeneratesId()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TaskRepository(context);
        
        var project = new ProjectEntity { Id = 1, Name = "Test Project" };
        context.Projects.Add(project);
        await context.SaveChangesAsync();
        
        var task = TaskEntity.Create("New task", 1);
        
        // Act
        var result = await repository.CreateAsync(task);
        
        // Assert
        result.Id.Should().BeGreaterThan(0);  // EF Core generated the ID
        result.Title.Should().Be("New task");
    }
    
    [Fact]
    public async Task DeleteAsync_WhenNotFound_SilentlySucceeds()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new TaskRepository(context);
        
        // Act
        Func<Task> act = async () => await repository.DeleteAsync(999);  // ID doesn't exist
        
        // Assert
        await act.Should().NotThrowAsync();  // Important: doesn't throw, just succeeds
    }
}
```

**To run:**
```bash
dotnet test --filter TaskRepositoryTests
```

**Expected:**  
All tests PASS ✅ - if any fail, your repository has a bug!

---

### Option B: Manual Verification (If tests seem complex)

Run this in a console app or `dotnet script`:

```csharp
using TaskFlowAPI.Data;
using TaskFlowAPI.Repositories;
using Microsoft.EntityFrameworkCore;

// Setup in-memory database
var options = new DbContextOptionsBuilder<TaskFlowDbContext>()
    .UseInMemoryDatabase("TestDb")
    .Options;

var context = new TaskFlowDbContext(options);
var repo = new TaskRepository(context);

// Add project
var project = new ProjectEntity { Id = 1, Name = "Test" };
context.Projects.Add(project);
await context.SaveChangesAsync();

// Test CreateAsync
var task = TaskEntity.Create("Test Task", 1);
var created = await repo.CreateAsync(task);
Console.WriteLine($"Created task with ID: {created.Id}");  // Should be > 0

// Test GetByIdAsync
var found = await repo.GetByIdAsync(created.Id);
Console.WriteLine($"Found: {found?.Title}");  // Should be "Test Task"

// Test GetAllAsync
var all = await repo.GetAllAsync();
Console.WriteLine($"Total tasks: {all.Count}");  // Should be 1

// Test DeleteAsync
await repo.DeleteAsync(created.Id);
var afterDelete = await repo.GetByIdAsync(created.Id);
Console.WriteLine($"After delete: {afterDelete?.Title ?? "null"}");  // Should be null

Console.WriteLine("✅ All repository methods work!");
```

---

### Why In-Memory Database?

**Pros:**
- ✅ Fast (no disk I/O)
- ✅ Isolated (each test gets fresh database)
- ✅ No setup required (no migrations, no connection strings)

**Cons:**
- ❌ Doesn't test real database constraints
- ❌ Slightly different behavior than SQL Server/PostgreSQL

**For Week 8:** In-memory is perfect for verifying your LINQ queries and async patterns work!

---

## 12. Additional Resources

- **Microsoft Docs: Repository Pattern** – Official guidance with EF Core examples.
- **Fowler: Service & Repository patterns** – Conceptual background (short article).
- **Refactoring Guru: Repository Pattern** – Alternative explanations and diagrams.
- **[EF Core In-Memory Testing](https://docs.microsoft.com/en-us/ef/core/testing/testing-with-the-in-memory-database)** - Official guide
- Optional: skim `Pro .NET Design Patterns` (Repository chapter) for advanced nuances.
