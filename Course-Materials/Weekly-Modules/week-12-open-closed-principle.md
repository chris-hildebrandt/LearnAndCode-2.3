# Week 12: Open/Closed Principle (OCP)

This week, we are focusing on the Open/Closed Principle (OCP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the OCP and the Quality Manifesto's focus on technical excellence. This module is based on **Clean Code Chapter 11: Systems**.

## 1. Learning Objectives

- Understand the Open/Closed Principle and its importance in creating maintainable, extensible, and testable code.
- Extend functionality (task filtering) without modifying existing service orchestration.
- Implement strategy pattern for task filters.
- Register strategies in DI and compose them dynamically.
- Recognize when OCP is appropriate and when it's premature abstraction.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 11: Systems (pp. 155-174)** — Focus on keeping policies decoupled from details (20 min).
- **[Understanding SOLID: Open-Closed Principle - Dev.to](https://dev.to/tkarropoulos/the-open-closed-principle-a-guide-to-writing-maintainable-code-15gm)** — Practical examples across languages (15 min).
- **[Open-Closed Principle - Wikipedia](https://en.wikipedia.org/wiki/Open-closed_principle)** — Original definition and historical context (10 min).

## 3. This Week's Work

- Review the "Before OCP" anti-pattern example in Additional Resources to understand what NOT to do.
- Implement `StatusTaskFilter`, `PriorityTaskFilter`, `DueDateTaskFilter`, and `CompositeTaskFilter`.
- Add `ITaskFilterFactory` to build filters based on query parameters.
- Update `TaskService` to use filters when fetching tasks.
- Wire query parameters through controller.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/Filters/*.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Controllers/TasksController.cs` (wire query parameters).
- `TaskFlowAPI/Program.cs` (register filters and factory).
- This file (`Course-Materials/Weekly-Modules/week-12-open-closed-principle.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-12-submission`.

2. **Review Anti-Pattern Example (10 min)**
   
   Before implementing filters, read the "Before OCP Anti-Pattern" section in Additional Resources (Section 11). This shows the if/else cascade that OCP solves.
   
   **Key Question:** What happens to the service method every time you add a new filter type?

3. **Understand the Pattern with StatusTaskFilter (15 min) - EXAMPLE PROVIDED**
   
   Open `TaskFlowAPI/Services/Tasks/Filters/StatusTaskFilter.cs` (already scaffolded with TODOs).
   
   **What this filter does in plain English:**
   ```
   When I create a StatusTaskFilter:
       - I tell it what status I'm looking for (completed or not completed)
       - It remembers that choice
   
   When someone asks "does this task match?":
       - I check if the task's completion status matches what I'm looking for
       - Return true if it matches, false if it doesn't
   ```
   
   **Complete implementation (use as reference for other filters):**
   ```csharp
   namespace TaskFlowAPI.Services.Tasks.Filters;
   
   public class StatusTaskFilter : ITaskFilter
   {
       private readonly bool _isCompleted;
       
       public StatusTaskFilter(bool isCompleted)
       {
           _isCompleted = isCompleted;
       }
       
       public bool IsMatch(TaskEntity task)
       {
           return task.IsCompleted == _isCompleted;
       }
   }
   ```
   
   **Study this pattern:** Constructor stores criteria → IsMatch compares task to criteria

4. **Implement PriorityTaskFilter (15 min) - YOU IMPLEMENT THIS**
   
   Open `TaskFlowAPI/Services/Tasks/Filters/PriorityTaskFilter.cs` (scaffolded with TODOs).
   
   **What this filter does in plain English:**
   ```
   When I create a PriorityTaskFilter:
       - I tell it what priority level I'm looking for (1, 2, 3, etc.)
       - It remembers that priority number
   
   When someone asks "does this task match?":
       - I check if the task's priority equals the priority I'm looking for
       - Return true if it matches, false if it doesn't
   ```
   
   **Your job:**
   - Follow the same pattern as StatusTaskFilter
   - Constructor should accept `int priority` and store it
   - `IsMatch` should return `true` if task's priority matches the stored priority
   
   **Hint:** Look at StatusTaskFilter and replace `bool _isCompleted` with `int _priority`

5. **Implement DueDateTaskFilter (20 min) - YOU IMPLEMENT THIS**
   
   Open `TaskFlowAPI/Services/Tasks/Filters/DueDateTaskFilter.cs` (scaffolded with TODOs).
   
   **What this filter does in plain English:**
   ```
   When I create a DueDateTaskFilter:
       - I tell it a cutoff date (like "January 1, 2025")
       - I tell it if I want tasks BEFORE or AFTER that date
       - It remembers both pieces of information
   
   When someone asks "does this task match?":
       - First, check if the task even has a due date (might be null!)
       - If no due date, return false (can't match)
       - If it has a due date, check if it's before/after my cutoff date
       - Return true if it matches my criteria
   ```
   
   **Your job:**
   - Constructor accepts `DateTime cutoffDate` and `bool isBefore` (default to `true`)
   - Store both in private fields
   - `IsMatch` logic:
     1. If `task.DueDate` is null, return `false`
     2. If `_isBefore` is true, check if task due date < cutoff date
     3. If `_isBefore` is false, check if task due date > cutoff date
   
   **Hint:** Use ternary operator: `return _isBefore ? (task.DueDate < _cutoffDate) : (task.DueDate > _cutoffDate);`

6. **Implement CompositeTaskFilter (25 min) - YOU IMPLEMENT THIS**
   
   Open `TaskFlowAPI/Services/Tasks/Filters/CompositeTaskFilter.cs` (scaffolded with TODOs).
   
   **What this filter does in plain English:**
   ```
   When I create a CompositeTaskFilter:
       - I give it a list of OTHER filters (like StatusFilter AND PriorityFilter)
       - It stores that list
   
   When someone asks "does this task match?":
       - I ask EVERY filter in my list "does this task match you?"
       - ALL of them must say "yes" for me to say "yes" (AND logic)
       - If even ONE says "no", I return false
   ```
   
   **Your job:**
   - Constructor accepts `List<ITaskFilter> filters` and stores it
   - `IsMatch` must check ALL filters against the task
   - Return `true` only if ALL filters return `true`
   
   **Hint:** Use LINQ's `All()` method: `return _filters.All(filter => filter.IsMatch(task));`

7. **Implement TaskFilterFactory (30 min) - YOU IMPLEMENT THIS**
   
   Open `TaskFlowAPI/Services/Tasks/Filters/TaskFilterFactory.cs` (scaffolded with TODOs).
   
   **What this factory does in plain English:**
   ```
   When someone calls CreateFilter with query parameters:
       - Look at each parameter (status, priority, dueBefore, dueAfter)
       - For each parameter that's NOT null:
           - Create the appropriate filter
           - Add it to a list
       
       - If no parameters were provided, return null (no filtering)
       - If only ONE filter was created, return just that filter
       - If MULTIPLE filters were created, wrap them in a CompositeTaskFilter
   ```
   
   **Your job:**
   - Method signature: `public ITaskFilter? CreateFilter(string? status, int? priority, DateTime? dueBefore, DateTime? dueAfter)`
   - Create a `List<ITaskFilter> filters = new();`
   - For each parameter:
     - `if (status != null)` → create StatusTaskFilter, add to list
     - `if (priority.HasValue)` → create PriorityTaskFilter, add to list
     - `if (dueBefore.HasValue)` → create DueDateTaskFilter (isBefore: true), add to list
     - `if (dueAfter.HasValue)` → create DueDateTaskFilter (isBefore: false), add to list
   - Return logic:
     - If `filters.Count == 0` → return `null`
     - If `filters.Count == 1` → return `filters[0]`
     - Otherwise → return `new CompositeTaskFilter(filters)`
   
   **Hint:** For status, convert string to bool: `bool isCompleted = status.Equals("Completed", StringComparison.OrdinalIgnoreCase);`

8. **Update TaskService (15 min)**
   
   Open `TaskFlowAPI/Services/Tasks/TaskService.cs` and find the `GetAllTasksAsync` method.
   
   **What you need to change:**
   - Add optional parameter: `ITaskFilter? filter = null`
   - After fetching tasks from repository, apply filter if provided
   
   **Pseudocode:**
   ```
   Method: GetAllTasksAsync(filter, cancellationToken)
       1. Get all tasks from repository (await)
       2. If filter is not null:
           - Filter the tasks using: tasks.Where(filter.IsMatch).ToList()
       3. Map to DTOs and return
   ```
   
   **Complete the TODO in the file.** The method should still work when `filter` is `null` (return all tasks).

9. **Update TasksController (20 min)**
   
   Open `TaskFlowAPI/Controllers/TasksController.cs` and find the `GetAll` action method.
   
   **What you need to change:**
   1. Inject `TaskFilterFactory` into the controller constructor
   2. Add query parameters to the `GetAll` method:
      - `[FromQuery] string? status`
      - `[FromQuery] int? priority`
      - `[FromQuery] DateTime? dueBefore`
      - `[FromQuery] DateTime? dueAfter`
   3. Use factory to create filter from query params
   4. Pass filter to service
   
   **Pseudocode:**
   ```
   Method: GetAll(status, priority, dueBefore, dueAfter, cancellationToken)
       1. Call _filterFactory.CreateFilter(status, priority, dueBefore, dueAfter)
       2. Call _taskService.GetAllTasksAsync(filter, cancellationToken)
       3. Return Ok(tasks)
   ```
   
   **Complete the TODO in the file.**

10. **Register in DI (5 min)**
    
    Open `TaskFlowAPI/Program.cs` and add this line after the other service registrations:
    ```csharp
    builder.Services.AddScoped<TaskFilterFactory>();
    ```
    
    **Why Scoped?** Factory is created once per HTTP request, same as controllers and services.

11. Run build and tests:
    ```bash
    dotnet build TaskFlowAPI.sln
    dotnet test TaskFlowAPI.sln
    ```

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual testing via Swagger:
- **No filters:** `GET /api/tasks` — Returns all tasks.
- **Status filter:** `GET /api/tasks?status=Completed` — Returns only completed tasks.
- **Priority filter:** `GET /api/tasks?priority=1` — Returns only priority 1 tasks.
- **Due date filter:** `GET /api/tasks?dueBefore=2025-01-01` — Returns tasks due before date.
- **Combined filters:** `GET /api/tasks?status=Completed&priority=1` — Returns completed priority 1 tasks.

## 7. Success Criteria

- No `switch` statements or if-chains inside service for filtering logic.
- Adding a new filter requires only: new class and DI registration (no service modification).
- Query parameters correctly influence results.
- Build and tests succeed; manual requests filter data as expected.
- Week 12 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

1. Create a new branch for your weekly work (e.g., `git checkout -b week-12-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 12 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Before vs. After: Look at the "Before OCP" if/else cascade in Additional Resources. If your team had that code and someone asked you to add a "tags" filter, what would you have to change? Now with your OCP implementation, what would you have to change?*
- *Your First Filter: Which filter did you implement first, and what surprised you about how simple (or complex) it was?*
- *Adding Filters Later: Imagine your PM asks you to add a filter for "assigned to me" next sprint. Walk through exactly what files you'd create or modify. Does that feel safe or risky?*

### Discussion Prep:

- *If you had to add a filter for "overdue tasks" tomorrow, would you touch TaskService.cs? Why or why not?*
- *Where did you put the TaskFilterFactory? Did you inject it into the controller or service? Why did you choose that location?*
- *Right now, filtering happens in memory after fetching all tasks from the database. What happens when you have 10,000 tasks? Is this a problem we should solve this week?*
- *Look at your CompositeTaskFilter. What happens if someone passes it an empty list of filters? Should that be allowed?*

## 10. Time Estimate

- 45 min — Reading.
- 10 min — Review anti-pattern example.
- 15 min — Implement StatusTaskFilter.
- 15 min — Implement PriorityTaskFilter.
- 15 min — Implement DueDateTaskFilter.
- 20 min — Implement CompositeTaskFilter.
- 20 min — Create TaskFilterFactory.
- 15 min — Update TaskService.
- 15 min — Update TasksController.
- 5 min — Register in DI.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~3 hours 30 minutes.

## 11. Additional Resources

### Before OCP Anti-Pattern (Read Before Implementing)

**Goal:** See the problem OCP solves (if/else explosion).

**The Anti-Pattern: Modification Required for Each Feature**

```csharp
// ❌ BAD: Violates OCP - must modify this method for each new filter
public async Task<List<TaskDto>> GetAllTasksAsync(
    string? status = null,
    int? priority = null,
    DateTime? dueBefore = null)
{
    var tasks = await _repository.GetAllAsync();
    var results = tasks.AsEnumerable();
    
    // IF/ELSE CASCADE GROWS WITH EACH FEATURE
    if (status != null)
    {
        if (status == "Completed")
            results = results.Where(t => t.IsCompleted);
        else if (status == "Pending")
            results = results.Where(t => !t.IsCompleted);
    }
    
    if (priority != null)
        results = results.Where(t => t.Priority == priority);
    
    if (dueBefore != null)
        results = results.Where(t => t.DueDate < dueBefore);
    
    // Method grows forever with each new filter...
    
    return results.Select(_mapper.ToDto).ToList();
}
```

**Problems:**
- ❌ Every new filter requires changing this method
- ❌ Method gets longer (SRP violation)
- ❌ Testing nightmare (2^N combinations)
- ❌ Merge conflicts when multiple developers add filters

**The OCP Solution: Strategy Pattern**

```csharp
// ✅ GOOD: Open for extension, closed for modification
public async Task<List<TaskDto>> GetAllTasksAsync(ITaskFilter? filter = null)
{
    var tasks = await _repository.GetAllAsync();
    
    if (filter != null)
        tasks = tasks.Where(filter.IsMatch).ToList();
    
    return tasks.Select(_mapper.ToDto).ToList();
}
```

**Benefits:**
- ✅ New filter = new class (service unchanged)
- ✅ Service stays same size
- ✅ Easy testing (each filter independent)
- ✅ No merge conflicts

---

### When NOT To Use OCP (Avoid Premature Abstraction)

**Goal:** Know when OCP is overkill.

**Decision Matrix:**

| Scenario | Use OCP? | Why? |
|----------|---------|------|
| Task filtering (3+ types) | ✅ YES | Multiple variations, user-driven |
| Payment processors | ✅ YES | Stripe, PayPal, Square - clear variation |
| Single SMTP email | ❌ NO | Only one implementation needed |
| Simple CRUD operations | ❌ NO | Same pattern for all entities |
| Reporting formats (PDF/Excel/CSV) | ✅ YES | Multiple output formats |

**Rule of Three:**
1. First time: Implement directly
2. Second time: Notice duplication
3. Third time: Refactor to OCP

**This Week:** Task filtering is justified because we expect 5+ filter types (status, priority, due date, assignee, project, tags).

---

### Examples

- **[Open-Closed Example](../Examples/Open-Closed.ts)** — TypeScript implementation showing OCP in practice.

### External Resources

- **[Understanding SOLID: Open-Closed Principle - Dev.to](https://dev.to/tkarropoulos/the-open-closed-principle-a-guide-to-writing-maintainable-code-15gm)** — Comprehensive guide with practical code examples.
- **[Open-Closed Principle - Wikipedia](https://en.wikipedia.org/wiki/Open-closed_principle)** — Original definition by Bertrand Meyer and evolution of the principle.
- **[Open-Closed Principle in Java - GeeksForGeeks](https://www.geeksforgeeks.org/open-closed-principle-in-java-with-examples/)** — Practical implementation examples.

### Optional Deep Dives

- **[Mastering SOLID: Open-Closed Principle - LinkedIn](https://www.linkedin.com/pulse/solid-principles-python-open-closed-principle-mahdi-jafari)** — Modern interpretation with Python examples.
- **[Open-Closed Principle in Practice - HowToDoInJava](https://howtodoinjava.com/best-practices/open-closed-principle/)** — Real-world applications and best practices.