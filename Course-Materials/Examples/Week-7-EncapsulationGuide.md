# Week 7: Encapsulation Guide

This guide provides detailed explanations and optional extensions for Week 7's encapsulation work.

**Estimated Reading Time:** 15-20 minutes

---

## Table of Contents

1. [Core Concepts](#core-concepts)
2. [Encapsulation Decision Framework](#encapsulation-decision-framework)
3. [EF Core Compatibility Patterns](#ef-core-compatibility-patterns)
4. [Optional Extensions](#optional-extensions)

---

## Core Concepts

### What is Encapsulation?

Encapsulation means hiding the internal state of an object and requiring all interaction to happen through methods. This prevents objects from entering invalid states.

**Without Encapsulation:**
```csharp
var task = new TaskEntity();
task.Title = "";  // Oops! Empty title is invalid
task.IsCompleted = true;
task.CompletedAt = null;  // Oops! Inconsistent state
```

**With Encapsulation:**
```csharp
var task = TaskEntity.Create("Write tests", projectId);  // Title validated
task.Complete();  // Sets both IsCompleted AND CompletedAt correctly
```

### What is an Invariant?

An invariant is a rule that must always be true about an object. Examples:

- **Title Invariant:** A task's title can never be empty
- **Completion Invariant:** If IsCompleted is true, CompletedAt must have a value
- **Priority Invariant:** Priority must be between 0 and 5

Encapsulation protects these invariants by preventing direct property access.

---

## Encapsulation Decision Framework

### When Should You Encapsulate?

Ask these questions for each property:

| Property | Has Validation Rules? | Has Invariant? | Changes Through Behavior? | Should Encapsulate? |
|----------|----------------------|----------------|---------------------------|---------------------|
| Title | Yes (not empty) | Yes | Rarely changes | ✅ YES |
| Priority | Yes (0-5 range) | Yes | Via ChangePriority() | ✅ YES |
| IsCompleted | No direct set | Yes (with CompletedAt) | Via Complete()/Reopen() | ✅ YES |
| CreatedAt | No (set once) | No | Never | ❌ NO (use init) |
| Id | No (EF Core manages) | No | Never | ❌ NO (EF Core) |
| Description | No rules | No | Can change freely | ❌ NO |

### Decision Criteria

**Encapsulate if:**
- ✅ Property has validation rules (Title not empty, Priority 0-5)
- ✅ Property has an invariant relationship with others (IsCompleted ↔ CompletedAt)
- ✅ Property should only change through specific behaviors (Complete(), not direct set)
- ✅ Invalid value could corrupt domain state

**Don't encapsulate if:**
- ❌ Property is simple data with no rules (Description can be any string)
- ❌ Property is set-once-never-change (CreatedAt with init setter)
- ❌ Property is managed by infrastructure (Id by EF Core)
- ❌ No business logic depends on its value

---

## EF Core Compatibility Patterns

### The Challenge

Entity Framework Core needs to create entity objects when loading data from the database. If you only have a private constructor with parameters, EF Core can't instantiate your objects!

### Solution: Parameterless Constructor

You need to provide a private parameterless constructor specifically for EF Core:

```csharp
// Private constructor for YOUR code
private TaskEntity(string title, int projectId)
{
    if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Title cannot be empty", nameof(title));
    
    Title = title;
    ProjectId = projectId;
    CreatedAt = DateTime.UtcNow;
}

// Private parameterless constructor for EF Core ONLY
private TaskEntity() 
{
    Title = string.Empty;  // Required initialization
    // EF Core will set properties after instantiation
}
```

### Why This Works

1. EF Core uses reflection to call the private parameterless constructor
2. EF Core then sets properties directly (bypassing validation)
3. Your application code uses the validated constructor via the factory method
4. Everyone is happy!

### Two Approaches for Properties

#### Option A: `init` Setters (Week 7 Assignment)

**Best for:** Properties that shouldn't change after creation

```csharp
public class TaskEntity
{
    public string Title { get; init; } = string.Empty;
    public int ProjectId { get; init; }
    public DateTime CreatedAt { get; init; }
    
    private TaskEntity(string title, int projectId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        Title = title;
        ProjectId = projectId;
        CreatedAt = DateTime.UtcNow;
    }
    
    private TaskEntity() { Title = string.Empty; }
    
    public static TaskEntity Create(string title, int projectId)
    {
        return new TaskEntity(title, projectId);
    }
}
```

**Pros:**
- ✅ Simple and clean
- ✅ Modern C# 9+ pattern
- ✅ EF Core 5+ supports natively
- ✅ Clear intent: "set once, read many"

**Cons:**
- ❌ Can't change property after creation
- ❌ No UpdateTitle() method possible

#### Option B: Private Setters with Private Fields

**Best for:** Properties that CAN change through methods

```csharp
public class TaskEntity
{
    private string _title = string.Empty;
    
    public string Title 
    { 
        get => _title; 
        private set => _title = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    private TaskEntity(string title, int projectId)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        _title = title;
        ProjectId = projectId;
    }
    
    private TaskEntity() { _title = string.Empty; }
    
    public static TaskEntity Create(string title, int projectId)
    {
        return new TaskEntity(title, projectId);
    }
    
    // Now you CAN update title through a validated method
    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        
        _title = title;
    }
}
```

**Pros:**
- ✅ Full control over property changes
- ✅ Can validate on every change
- ✅ Supports update methods

**Cons:**
- ❌ More boilerplate code
- ❌ Private fields add complexity

### Comparison

| Aspect | `init` Setters | Private Setters |
|--------|---------------|-----------------|
| Complexity | Low | Medium |
| Mutability | Immutable | Mutable |
| EF Core Support | EF Core 5+ | All versions |
| Validation | Once (constructor) | Every change |
| Best For | Set-once properties | Updatable properties |

---

## Optional Extensions

If you finish the core assignment early, consider adding these domain behaviors:

### Extension 1: Reopen() Method

Allow completed tasks to be reopened:

```csharp
public void Reopen()
{
    if (!IsCompleted)
        throw new InvalidOperationException("Task is not completed");
    
    IsCompleted = false;
    CompletedAt = null;
}
```

**Why this matters:** Enforces the invariant that CompletedAt is only set when IsCompleted is true.

### Extension 2: UpdateDetails() Method

Allow updating multiple properties with validation:

```csharp
public void UpdateDetails(string title, string? description, DateTime? dueDate)
{
    if (string.IsNullOrWhiteSpace(title))
        throw new ArgumentException("Title cannot be empty", nameof(title));
    
    // Note: If using init setters, you'll need to switch Title to private setter pattern
    Description = description;
    DueDate = dueDate;
}
```

**Trade-off:** This requires changing Title from `init` to `private set` pattern.

### Extension 3: ChangePriority() Method

Add validation for priority range:

```csharp
public void ChangePriority(int priority)
{
    if (priority < 0 || priority > 5)
        throw new ArgumentOutOfRangeException(nameof(priority), 
            "Priority must be between 0 and 5");
    
    Priority = priority;
}
```

**Why this matters:** Prevents invalid priority values (negative numbers, values > 5).

### Extension 4: Encapsulate More Properties

Apply the same patterns to other properties:

```csharp
public int Priority { get; private set; }
public DateTime CreatedAt { get; init; }
public bool IsCompleted { get; private set; }
public DateTime? CompletedAt { get; private set; }
```

**Result:** All state changes go through methods, all invariants are protected.

---

## Key Takeaways

1. **Encapsulation protects invariants** - Invalid states become impossible to create
2. **Factory methods provide validated construction** - No direct constructor access
3. **Domain behavior methods enforce rules** - Complete() sets both flags correctly
4. **EF Core compatibility requires compromise** - Private parameterless constructor
5. **Trade-offs exist** - Immutability vs flexibility, simplicity vs control

The goal isn't to encapsulate everything, but to protect the rules that matter for your domain.