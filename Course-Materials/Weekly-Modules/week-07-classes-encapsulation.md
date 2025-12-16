# Week 7: Classes & Encapsulation

This week, we will focus on the principles of organizing classes, emphasizing how these concepts align with In Time Tec's commitment to modular, maintainable code and the Quality Manifesto's focus on technical excellence. This module is based on **Clean Code Chapter 10: Classes**.

## 1. Learning Objectives

- Understand the principles of organizing classes and systems for modularity and maintainability.
- Apply best practices for organizing classes in your project.
- Refactor your project's classes to improve modularity and maintainability.
- Discuss how we can create and refactor classes to align with the Quality Manifesto's emphasis on technical excellence.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 10: Classes (pp. 137-154)**.
  - Focus on cohesion, encapsulation, and hiding implementation details.

## 3. This Week’s Work

**REVISED: Two-Phase Approach**

**Phase 1 (Core - Required, 90 min):**
- Refactor `TaskEntity` to encapsulate ONLY the Title property
- Add domain behavior: `Complete()` method with validation
- Introduce static factory `TaskEntity.Create(...)` that validates title
- Learn EF Core encapsulation patterns (`init` setters OR private constructors)

**Phase 2 (Optional Extension, +30 min):**
- Add remaining domain behaviors: `Reopen()`, `UpdateDetails(...)`, `ChangePriority(...)`
- Encapsulate additional properties with validation

## 4. Files to Modify

- `TaskFlowAPI/Entities/TaskEntity.cs`
- `TaskFlowAPI/Entities/ProjectEntity.cs` (optional: add helper to attach tasks)
- Any affected migration snapshot (run `dotnet ef migrations add` only if schema changes).
- This file (`Course-Materials/Weekly-Modules/week-07-classes-encapsulation.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### Phase 1: Basic Encapsulation (CORE - Required, 90 min)

**Goal:** Protect ONE critical invariant - Title cannot be empty

1. Branch `week-07/<your-name>`.

2. **NEW:** Review Encapsulation Decision Framework (see Section 11 below) - 10 min

3. **Encapsulate Title property** (choose ONE approach from Section 12 below):
   - **Option A:** Modern `init` setters (RECOMMENDED for juniors)
   - **Option B:** Private fields with validated property setter

4. **Add private constructor** with validation:
   ```csharp
   private TaskEntity(string title, int projectId)
   {
       if (string.IsNullOrWhiteSpace(title))
           throw new ArgumentException("Title cannot be empty", nameof(title));
       
       Title = title;  // or _title = title if using private field
       ProjectId = projectId;
       CreatedAt = DateTime.UtcNow;
       IsCompleted = false;
   }
   ```

5. **Add static factory method:**
   ```csharp
   public static TaskEntity Create(string title, int projectId)
   {
       return new TaskEntity(title, projectId);
   }
   ```

6. **Add Complete() method:**
   ```csharp
   public void Complete()
   {
       if (IsCompleted)
           throw new InvalidOperationException("Task is already completed");
       
       IsCompleted = true;
       CompletedAt = DateTime.UtcNow;
   }
   ```

7. **Add EF Core parameterless constructor** (see Section 12 for details):
   ```csharp
   // Required by EF Core - don't use this directly!
   private TaskEntity() 
   {
       Title = string.Empty;  // placeholder for EF Core
   }
   ```

8. **Update seed data** in `TaskFlowDbContext`:
   - Change from: `new TaskEntity { Title = "...", ... }`
   - To: `TaskEntity.Create("...", projectId)`
   - **Expected:** This will break! Fix the errors (this is learning!)

9. Build and test: `dotnet build && dotnet ef database update`

**Phase 1 Deliverable:**
- Title immutable after creation
- Can't create task with empty title (throws exception)
- Complete() enforces "can't complete twice" rule
- EF Core still works

---

### Phase 2: Full Encapsulation (OPTIONAL EXTENSION, +30 min)

**If you finished Phase 1 early, add these methods:**

10. **Reopen() method:**
    ```csharp
    public void Reopen()
    {
        if (!IsCompleted)
            throw new InvalidOperationException("Task is not completed");
        
        IsCompleted = false;
        CompletedAt = null;
    }
    ```

11. **UpdateDetails() method:**
    ```csharp
    public void UpdateDetails(string title, string? description, DateTime? dueDate)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        Title = title;
        Description = description;
        DueDate = dueDate;
    }
    ```

12. **ChangePriority() method** with range validation:
    ```csharp
    public void ChangePriority(int priority)
    {
        if (priority < 0 || priority > 5)
            throw new ArgumentOutOfRangeException(nameof(priority), 
                "Priority must be between 0 and 5");
        
        Priority = priority;
    }
    ```

**Phase 2 Deliverable:**
- All state changes go through methods (no public setters)
- Each method validates its inputs
- Meaningful exceptions for invalid operations

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Optional: temporary console app or `dotnet script` to instantiate `TaskEntity` and ensure methods behave.

## 7. Success Criteria

**Phase 1 (Core - Required):**
- ✅ Title property is immutable after creation (no public setter OR uses `init`)
- ✅ Can't create task with empty title (throws ArgumentException)
- ✅ Complete() enforces "can't complete twice" rule (throws InvalidOperationException)
- ✅ Static factory `TaskEntity.Create()` exists and validates inputs
- ✅ EF Core parameterless constructor added (private)
- ✅ Seed data updated to use `TaskEntity.Create()`
- ✅ Build + tests succeed, database updates successfully

**Phase 2 (Optional Extension - if time permits):**
- ✅ Reopen(), UpdateDetails(), ChangePriority() methods implemented
- ✅ All methods validate inputs and throw meaningful exceptions
- ✅ No public setters remain (except EF Core requirements)

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-07-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 7 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Invariants:* Document one invariant you enforced and the business rule it protects.
- *Factory vs Constructor:* Why did you choose your current creation approach? Note trade-offs for future teams.

### Discussion Prep:
- *Which invariants did you guard and why?*
- *How would another developer know how to create a valid `TaskEntity` now?*
- *What future bugs does this encapsulation prevent?*
- *Where might encapsulation conflict with EF Core conveniences, and how will you mitigate that?*
- *Discuss the relationship between class, system organization, and the Quality Manifesto's emphasis on technical excellence.*

## 10. Time Estimate

**Phase 1 (Core - REQUIRED):**
- 40 min – Reading.
- 10 min – Review Encapsulation Decision Framework (NEW)
- 15 min – Choose and implement encapsulation approach (init vs private field)
- 20 min – Add constructor, factory, Complete() method
- 15 min – EF Core parameterless constructor + understanding why
- 20 min – Update seed data and fix compilation errors
- 10 min – Test and verify database updates
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Phase 1 Total:** ~2 hours 25 minutes (more realistic for juniors)

**Phase 2 (Optional Extension):**
- 30 min – Add Reopen(), UpdateDetails(), ChangePriority() methods

**Maximum Total (with extension):** ~2 hours 55 minutes

**Note:** Most students will complete Phase 1 only, which is pedagogically sufficient.

---

## 11. Encapsulation Decision Framework (NEW)

**When should you encapsulate a property?**

### Step 1: Identify Candidates

Ask these questions for each property:

| Property | Has Validation Rules? | Has Invariant? | Changes Through Behavior? | Should Encapsulate? |
|----------|----------------------|----------------|---------------------------|---------------------|
| Title | Yes (not empty) | Yes | Via UpdateDetails() | ✅ YES |
| Priority | Yes (0-5 range) | Yes | Via ChangePriority() | ✅ YES |
| IsCompleted | No direct set | Yes (only via Complete/Reopen) | Yes | ✅ YES |
| CreatedAt | No (set once) | No | No | ❌ NO (use init) |
| Id | No (EF Core manages) | No | No | ❌ NO (EF Core) |

### Step 2: Decision Criteria

**Encapsulate (private set OR init) if:**
- ✅ Property has validation rules (Title not empty, Priority 0-5)
- ✅ Property has invariant relationship with others (IsCompleted ↔ CompletedAt)
- ✅ Property should only change through specific behaviors (Complete(), not direct set)
- ✅ Invalid value could corrupt domain state

**Don't encapsulate (public set OK) if:**
- ❌ Property is simple data with no rules (Description can be any string)
- ❌ Property is set-once-never-change (CreatedAt with init setter)
- ❌ Property is managed by infrastructure (Id by EF Core)
- ❌ No business logic depends on its value

### Step 3: Choose Approach

**For Week 7, encapsulate ONLY Title** (learn one pattern deeply):

**Decision Tree:**
```
Does property need validation?
├─ YES → Encapsulate
│   ├─ Set once at creation? 
│   │   → Use `init` setter (modern, simple)
│   └─ Can change after creation?
│       → Use private field + validated setter
└─ NO → Public property with `init` is fine
```

### Example: Title Property

**Decision:** Encapsulate because:
1. ✅ Has validation rule (not empty/whitespace)
2. ✅ Business invariant (every task MUST have a title)
3. ✅ Can change via UpdateDetails() but needs validation

**Approach:** See Section 12 for two options

---

## 12. EF Core Encapsulation Survival Guide (NEW)

**The Problem:** EF Core needs to create entities when loading from database, but your private constructor prevents this!

### Option A: Modern `init` Setters (RECOMMENDED for Juniors)

**Best for:** Properties that CAN'T change after creation

```csharp
public class TaskEntity
{
    // EF Core can set these during instantiation, but users can't change them after
    public string Title { get; init; } = string.Empty;
    public int ProjectId { get; init; }
    public DateTime CreatedAt { get; init; }
    
    // Still need validation, so add constructor
    private TaskEntity(string title, int projectId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        Title = title;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
    }
    
    // EF Core 5+ supports init setters natively - no parameterless constructor needed!
    
    public static TaskEntity Create(string title, int projectId)
    {
        return new TaskEntity(title, projectId);
    }
    
    // But Title CAN'T be changed even through methods now!
    // If you need UpdateDetails(), Title needs to be mutable...
}
```

**Pros:**
- ✅ Simple (no private fields)
- ✅ Modern C# 9+ pattern
- ✅ EF Core 5+ supports natively
- ✅ Clear intent: "set once, read many"

**Cons:**
- ❌ Can't change after creation (no UpdateDetails() for Title)
- ❌ Requires C# 9+ and EF Core 5+

**Use when:** Property should NEVER change (CreatedAt, ProjectId)

---

### Option B: Private Fields + Parameterless Constructor (Traditional)

**Best for:** Properties that CAN change through behaviors

```csharp
public class TaskEntity
{
    private string _title = string.Empty;
    
    public string Title 
    { 
        get => _title; 
        private set => _title = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public int ProjectId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    // Private constructor for YOUR code
    private TaskEntity(string title, int projectId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        _title = title;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
    }
    
    // Parameterless constructor for EF CORE ONLY
    private TaskEntity() 
    {
        _title = string.Empty;  // Required for private field
        // EF Core will set properties via private setters after instantiation
    }
    
    public static TaskEntity Create(string title, int projectId)
    {
        return new TaskEntity(title, projectId);
    }
    
    // Now you CAN update title through validated method
    public void UpdateDetails(string title, string? description)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        _title = title;
        Description = description;
    }
}
```

**Pros:**
- ✅ Full control over property changes
- ✅ Can validate on every change
- ✅ Supports UpdateDetails() and other mutations
- ✅ Works with all EF Core versions

**Cons:**
- ❌ More boilerplate (private fields)
- ❌ Parameterless constructor feels like a hack
- ❌ Easy to forget null check in private set

**Use when:** Property can change after creation (Title via UpdateDetails())

---

### Comparison Table

| Aspect | `init` Setters | Private Fields |
|--------|---------------|----------------|
| **Complexity** | Low | Medium |
| **Mutability after creation** | ❌ No | ✅ Yes |
| **EF Core support** | EF Core 5+ | All versions |
| **Validation** | Once (constructor) | Every change |
| **Code size** | Less | More |
| **Best for** | Immutable properties | Mutable properties |

### Week 7 Recommendation

**For learning, use Option A (`init` setters):**
- Simpler for juniors
- Modern C# pattern
- TaskFlowAPI uses EF Core 8 (supports `init`)
- You'll learn Option B naturally when you add UpdateDetails() in Phase 2

**Example for Phase 1:**
```csharp
public class TaskEntity
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public int ProjectId { get; init; }
    public DateTime CreatedAt { get; init; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    
    private TaskEntity(string title, int projectId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        Title = title;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
    }
    
    // No parameterless constructor needed with init!
    
    public static TaskEntity Create(string title, int projectId)
    {
        return new TaskEntity(title, projectId);
    }
    
    public void Complete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Task is already completed");
        
        IsCompleted = true;
        CompletedAt = DateTime.UtcNow;
    }
}
```

**Note:** In Phase 2, if you want UpdateDetails() to change Title, you'll need to switch to Option B (private field) for Title. This teaches the trade-off between immutability and flexibility!

---

## 13. Additional Resources

- **[Classes Example](../Examples/Classes.md)**
- **[System Design in Software Development](https://medium.com/the-andela-way/system-design-in-software-development-f360ce6fcbb9)**
- **[Clean Code: Chapter 10 Classes](https://medium.com/the-command-line/clean-code-chapter-10-classes-98be694f1fa2)**
- **[C# 9 init Setters](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init)** - Modern approach
- **[EF Core and Encapsulation](https://docs.microsoft.com/en-us/ef/core/modeling/constructors)** - Official guidance
