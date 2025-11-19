# Week 12: Open/Closed Principle (OCP)

This week, we are focusing on the Open/Closed Principle (OCP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the OCP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Understand the Open/Closed Principle and its importance in creating maintainable, extensible, and testable code.
- Extend functionality (task filtering) without modifying existing service orchestration.
- Implement strategy pattern for task filters.
- Register strategies in DI and compose them dynamically.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 11 (Systems)** – Focus on keeping policies decoupled from details.
- **[Understanding SOLID: Open-Closed Principle - Dev.to](https://dev.to/tkarropoulos/the-open-closed-principle-a-guide-to-writing-maintainable-code-15gm)** – Practical examples across languages.
- **[Open-Closed Principle - Wikipedia](https://en.wikipedia.org/wiki/Open-closed_principle)** – Original definition and historical context.
- **[Mastering SOLID: Open-Closed Principle - LinkedIn](https://www.linkedin.com/pulse/solid-principles-python-open-closed-principle-mahdi-jafari)** – Python-focused interpretation.
- Optional: **[Open-Closed Principle in Practice - HowToDoInJava](https://howtodoinjava.com/best-practices/open-closed-principle/)** - Real-world applications and best practices.

## 3. This Week’s Work

**REVISED: See "Before OCP" Anti-Pattern First**

- **Step 0:** Review "Before OCP" example (Section 11 below) - understand what NOT to do - 10 min
- Implement `StatusTaskFilter`, `PriorityTaskFilter`, `DueDateTaskFilter`, and `CompositeTaskFilter`.
- Add `ITaskFilterFactory` (or similar) to build filters based on query parameters.
- Update `TaskService` (or controller) to use filters when fetching tasks.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/Filters/*.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Controllers/TasksController.cs` (wire query parameters)
- `TaskFlowAPI/Program.cs` (register filters/factory)
- This file (`Course-Materials/Weekly-Modules/week-12-open-closed-principle.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-12/<your-name>`.
2. Implement each concrete filter’s `IsMatch` method.
3. Create `TaskFilterFactory` that accepts query parameters (`status`, `priority`, `dueBefore`, `dueAfter`) and returns a composite filter.
4. Update `TaskService.GetAllTasksAsync` to accept optional filter input (introduce new method or parameter object) and apply `Where(filter.IsMatch)`.
5. Update controller `GET /api/tasks` to accept query params and pass to service.
6. Register filters and factory in DI.
7. Ensure default behavior (no filters) still returns all tasks.
8. Build/tests + manual requests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual: `GET /api/tasks?status=Completed`, `?priority=1,2`, `?dueBefore=2025-01-01`.

## 7. Success Criteria

- No `switch` statements or `if-chains` inside service for filtering logic.
- Adding a new filter is additive (new class + DI registration only).
- Query parameters correctly influence results.
- Build/tests succeed; manual requests filter data as expected.

## 8. Submission Process

- Commit `Week 12 – task filter strategies`.
- PR summary includes list of supported query params and example outputs.
- Weekly issue attaches screenshot or `curl` results showing filter working.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Filter Composition:* Document the signature you settled on for combining filters and why it remains OCP-friendly.
- *Extensibility Audit:* List one prospective filter you might add later and what changes would (or wouldn’t) be required.

### Discussion Prep:
- *What would it take to add a new filter now?*
- *How did you decide where the factory lives (controller vs. service)?*
- *What caching or performance implications come with in-memory filtering?*
- *Which guardrails prevent this design from devolving into complex chains?*

## 10. Time Estimate

- 45 min – Reading.
- 10 min – Design filters.
- 50 min – Implement filters, factory, controller updates.
- 15 min – Manual verification + PR/issue.
**Total:** ~120 minutes.

## 11. "Before OCP" Anti-Pattern (NEW - Learn What NOT To Do)

**Goal:** See the problem OCP solves (if/else explosion)

**Time:** 10 minutes

### The Anti-Pattern: Modification Required for Each Feature

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
- ❌ Merge conflicts

### The OCP Solution: Strategy Pattern

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

---

## 12. When NOT To Use OCP (NEW - Avoid Premature Abstraction)

**Goal:** Know when OCP is overkill

**Decision Matrix:**

| Scenario | Use OCP? | Why? |
|----------|---------|------|
| Task filtering (3+ types) | ✅ YES | Multiple variations, user-driven |
| Payment processors | ✅ YES | Stripe, PayPal - clear variation |
| Single SMTP email | ❌ NO | Only one implementation |
| Simple CRUD | ❌ NO | Same for all entities |

**Rule of Three:**
1. First time: Implement directly
2. Second time: Notice duplication
3. Third time: Refactor to OCP

**This Week:** Task filtering justified (5+ filter types expected)

---

## 13. Additional Resources

- **[Open-Closed Example](../Examples/Open-Closed.ts)**
- **[Understanding SOLID: Open-Closed Principle - Dev.to](https://dev.to/tkarropoulos/the-open-closed-principle-a-guide-to-writing-maintainable-code-15gm)** - Comprehensive guide with practical code examples.
- **[Open-Closed Principle - Wikipedia](https://en.wikipedia.org/wiki/Open-closed_principle)** - Original definition by Bertrand Meyer and evolution of the principle.
- **[Open-Closed Principle in Java - GeeksForGeeks](https://www.geeksforgeeks.org/open-closed-principle-in-java-with-examples/)** - Practical implementation examples in Java.
- **[Mastering SOLID: Open-Closed Principle - LinkedIn](https://www.linkedin.com/pulse/solid-principles-python-open-closed-principle-mahdi-jafari)** - Modern interpretation with Python examples.
- **[Open-Closed Principle in Practice - HowToDoInJava](https://howtodoinjava.com/best-practices/open-closed-principle/)** - Real-world applications and best practices.

