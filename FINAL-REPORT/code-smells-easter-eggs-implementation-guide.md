# Code Smells Easter Eggs - Detailed Implementation Guide

**Purpose:** Add intentional code smells throughout TaskFlowAPI as educational "easter eggs" for Week 18 students to find and fix.

**Estimated Effort:** 8-12 hours  
**Priority:** High

---

## Step 1: Read Clean Code Chapter 17 (1 hour)

### Smells to Document from Chapter 17

**Method-Level Smells:**
1. Long Method (p. 288)
2. Long Parameter List (p. 290)
3. Feature Envy (p. 291)
4. Data Clumps (p. 291)
5. Primitive Obsession (p. 292)
6. Switch Statements (p. 293)

**Class-Level Smells:**
7. Large Class (p. 294)
8. Data Class (p. 295)
9. Lazy Class (p. 295)

**General Smells:**
10. Duplicate Code (p. 289)
11. Dead Code (p. 296)
12. Speculative Generality (p. 296)
13. Comments (bad comments - p. 297)
14. Inappropriate Intimacy (p. 297)
15. Message Chains (p. 297)
16. Middle Man (p. 298)
17. Incomplete Library Class (p. 298)

**Heuristics:**
18. G5: Too Many Arguments
19. G6: Output Arguments
20. G7: Flag Arguments
21. G8: Dead Function
22. G9: Too Much Information
23. G10: Vertical Distance
24. G11: Inconsistency
25. G12: Clutter
26. G13: Artificial Coupling
27. G14: Feature Envy
28. G15: Selector Arguments
29. G16: Obscured Intent
30. G17: Misplaced Responsibility
31. G18: Inappropriate Static
32. G19: Use Explanatory Variables
33. G20: Function Names Should Say What They Do
34. G21: Understand the Algorithm
35. G22: Make Logical Dependencies Physical
36. G23: Prefer Polymorphism to If/Else or Switch/Case
37. G24: Follow Standard Conventions
38. G25: Replace Magic Numbers with Named Constants
39. G26: Be Precise
40. G27: Structure over Convention
41. G28: Encapsulate Conditionals
42. G29: Avoid Negative Conditionals
43. G30: Functions Should Do One Thing
44. G31: Hidden Temporal Couplings
45. G32: Don't Be Arbitrary
46. G33: Encapsulate Boundary Conditions
47. G34: Functions Should Descend Only One Level of Abstraction
48. G35: Keep Configurable Data at High Levels
49. G36: Avoid Transitive Navigation

---

## Step 2: Create Code Smells Inventory (1 hour)

### Target Smells for TaskFlowAPI (Prioritized)

**High Priority (Easy to add, educational, fixable):**

1. **Long Method** (3-4 instances)
   - Location: TaskService, ReportsController
   - Example: Combine validation + creation + mapping in one method
   - Fix: Extract Method pattern

2. **Duplicate Code** (3-4 instances)
   - Location: Validators, Service methods
   - Example: Same validation logic in multiple places
   - Fix: Extract Method, Extract Class

3. **Long Parameter List** (2-3 instances)
   - Location: Controller methods, Service methods
   - Example: Method with 5+ parameters
   - Fix: Parameter Object pattern

4. **Data Clumps** (2-3 instances)
   - Location: Method parameters, DTOs
   - Example: Three related parameters always passed together
   - Fix: Extract Class

5. **Primitive Obsession** (2-3 instances)
   - Location: Priority, Status values
   - Example: Using int for priority instead of Priority enum
   - Fix: Replace Primitive with Object

6. **Feature Envy** (1-2 instances)
   - Location: Service methods accessing entity properties directly
   - Example: Service method that should be on entity
   - Fix: Move Method

7. **Switch Statements** (1-2 instances)
   - Location: Service methods, Validators
   - Example: Switch on priority/status values
   - Fix: Replace with Polymorphism or Strategy

8. **Dead Code** (1-2 instances)
   - Location: Unused methods, commented code
   - Example: Old implementation left in comments
   - Fix: Delete

9. **Comments (Bad)** (2-3 instances)
   - Location: Throughout codebase
   - Example: Comments explaining what code does
   - Fix: Make code self-documenting

10. **Large Class** (1 instance)
    - Location: Helper/Utility class
    - Example: Class with 10+ methods doing different things
    - Fix: Extract Class

**Medium Priority (Good learning value):**

11. **Data Class** (1-2 instances)
    - Location: DTOs that could have behavior
    - Example: DTO with only getters/setters
    - Fix: Add behavior methods

12. **Lazy Class** (1 instance)
    - Location: Small utility class
    - Example: Class with only one method
    - Fix: Inline Class

13. **Speculative Generality** (1 instance)
    - Location: Abstract class/interface used once
    - Example: Interface with only one implementation
    - Fix: Remove abstraction

14. **Message Chains** (1 instance)
    - Location: Service accessing nested properties
    - Example: `task.Project.Customer.Name`
    - Fix: Hide Delegate

15. **Magic Numbers** (2-3 instances)
    - Location: Throughout codebase
    - Example: Hard-coded priority values, timeouts
    - Fix: Extract to named constants

**Total Target:** 20-25 intentional smells

---

## Step 3: Add Intentional Smells to TaskFlowAPI (6-8 hours)

### Comment Format Standard

```csharp
// CODE SMELL: [Smell Name] (Clean Code Ch 17, p. [page])
// [Brief explanation of why this is a smell]
// [What principle it violates]
// Refactor by: [Suggested refactoring approach]
```

### Implementation by File

#### TasksController.cs (3-4 smells)

```csharp
// CODE SMELL: Long Parameter List (Clean Code Ch 17, p. 290)
// This method has 7 parameters, violating the "fewer arguments are better" principle.
// Multiple related parameters suggest they should be grouped into a parameter object.
// Refactor by: Create TaskQueryParameters class to encapsulate filter parameters.
[HttpGet]
public async Task<ActionResult<PagedResponse<TaskDto>>> GetAllTasks(
    [FromQuery] int page,
    [FromQuery] int pageSize,
    [FromQuery] bool? isCompleted,
    [FromQuery] int? priority,
    [FromQuery] DateTime? dueBefore,
    [FromQuery] DateTime? dueAfter,
    [FromQuery] int? projectId,
    CancellationToken ct = default)
{
    // Implementation...
}

// CODE SMELL: Feature Envy (Clean Code Ch 17, p. 291)
// This method accesses multiple properties of TaskEntity to format a message.
// The formatting logic should be on the entity itself, not in the controller.
// Refactor by: Move formatting logic to TaskEntity.FormatSummary() method.
private string FormatTaskSummary(TaskEntity task)
{
    return $"{task.Title} (Priority: {task.Priority}, Due: {task.DueDate?.ToString("yyyy-MM-dd")})";
}
```

#### ReportsController.cs (2-3 smells)

```csharp
// CODE SMELL: Long Method (Clean Code Ch 17, p. 288)
// This method is 60+ lines and does multiple things:
// 1. Validates project exists
// 2. Fetches tasks
// 3. Calculates statistics
// 4. Formats report
// 5. Handles errors
// Refactor by: Extract methods for each responsibility (Extract Method pattern).
public async Task<ActionResult<ProjectSummaryDto>> GenerateProjectSummaryReport(
    int projectId, CancellationToken ct = default)
{
    // 60+ lines of mixed concerns...
}

// CODE SMELL: Data Clumps (Clean Code Ch 17, p. 291)
// These three parameters (startDate, endDate, includeCompleted) are always
// passed together and represent a single concept: "date range filter".
// Refactor by: Create DateRangeFilter class to encapsulate these parameters.
private List<TaskDto> FilterTasksByDateRange(
    List<TaskDto> tasks, 
    DateTime? startDate, 
    DateTime? endDate, 
    bool includeCompleted)
{
    // Implementation...
}
```

#### TaskService.cs (4-5 smells)

```csharp
// CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
// This validation pattern (null check + throw) appears in 4 other methods.
// Extract to a shared validation method to follow DRY principle.
// Refactor by: Create ValidateTaskExists() helper method.
public async Task<TaskDto> GetTaskByIdAsync(int id, CancellationToken ct = default)
{
    var task = await _taskReader.GetByIdAsync(id, ct);
    if (task == null)
        throw new TaskNotFoundException(id);
    // ... rest of method
}

// CODE SMELL: Long Method (Clean Code Ch 17, p. 288)
// This method is 45 lines and handles multiple concerns:
// 1. Validates request
// 2. Calculates defaults
// 3. Creates entity via factory
// 4. Saves to database
// 5. Maps to DTO
// 6. Logs result
// Refactor by: Extract smaller methods for each step.
public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request, CancellationToken ct = default)
{
    // 45+ lines...
}

// CODE SMELL: Primitive Obsession (Clean Code Ch 17, p. 292)
// Using int for priority (0-5) instead of a type-safe Priority enum or value object.
// This allows invalid values and makes code less readable.
// Refactor by: Create Priority enum or Priority value object class.
private void ValidatePriority(int priority)
{
    if (priority < 0 || priority > 5)
        throw new ArgumentException("Priority must be 0-5");
}

// CODE SMELL: Switch Statements (Clean Code Ch 17, p. 293)
// Switch statement on priority value suggests polymorphism would be better.
// Refactor by: Replace with Strategy pattern or Priority value object with behavior.
private string GetPriorityLabel(int priority)
{
    return priority switch
    {
        0 => "None",
        1 => "Low",
        2 => "Medium",
        3 => "High",
        4 => "Urgent",
        5 => "Critical",
        _ => "Unknown"
    };
}
```

#### TaskRepository.cs (2-3 smells)

```csharp
// CODE SMELL: Data Clumps (Clean Code Ch 17, p. 291)
// These three parameters (skip, take, orderBy) are always passed together
// and represent pagination/sorting configuration.
// Refactor by: Create PaginationOptions class.
public async Task<List<TaskEntity>> GetAllAsync(
    int skip, 
    int take, 
    string orderBy, 
    CancellationToken ct = default)
{
    // Implementation...
}

// CODE SMELL: Primitive Obsession (Clean Code Ch 17, p. 292)
// Using string for orderBy instead of a type-safe enum or value object.
// Refactor by: Create SortOrder enum or OrderBy value object.
```

#### Validators (2-3 smells)

```csharp
// CODE SMELL: Long Parameter List (Clean Code Ch 17, p. 290)
// This validation method has 6 parameters, all related to task validation rules.
// Refactor by: Create TaskValidationRules class to encapsulate these parameters.
private bool ValidateTaskRules(
    string title,
    int priority,
    DateTime? dueDate,
    int projectId,
    bool isCompleted,
    DateTime? completedAt)
{
    // Implementation...
}

// CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
// This title validation logic is duplicated in CreateTaskValidator and UpdateTaskValidator.
// Refactor by: Extract to shared validation rule class.
```

#### DTOs (1-2 smells)

```csharp
// CODE SMELL: Data Class (Clean Code Ch 17, p. 295)
// This DTO has only getters/setters with no behavior.
// Consider adding helper methods like IsOverdue(), GetPriorityLabel(), etc.
// Refactor by: Add domain behavior methods to DTO (if appropriate) or create extension methods.
public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    // ... only properties, no methods
}
```

#### Tests (2-3 smells)

```csharp
// CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
// This test setup code (creating mocks, setting up test data) is duplicated
// across multiple test methods.
// Refactor by: Extract to test fixture or helper method.
[Fact]
public async Task CreateTaskAsync_ValidRequest_ReturnsTaskDto()
{
    // 15 lines of setup code duplicated in other tests...
}

// CODE SMELL: Obscure Test (Clean Code Ch 17, p. 299)
// This test name doesn't clearly describe what scenario is being tested.
// The test body is also unclear about expected behavior.
// Refactor by: Use descriptive test name following "MethodName_Scenario_ExpectedBehavior" pattern.
[Fact]
public async Task Test1()
{
    // Unclear what this tests...
}
```

#### Helper/Utility Classes (1-2 smells)

```csharp
// CODE SMELL: Large Class (Clean Code Ch 17, p. 294)
// This utility class has 12+ methods doing unrelated things:
// - String formatting
// - Date calculations
// - Validation helpers
// - Mapping utilities
// Refactor by: Split into focused classes (StringFormatter, DateHelper, ValidationHelper, etc.).
public static class TaskHelper
{
    public static string FormatTitle(string title) { }
    public static DateTime CalculateDueDate(DateTime start, int days) { }
    public static bool IsValidPriority(int priority) { }
    public static TaskDto MapToDto(TaskEntity entity) { }
    // ... 8+ more unrelated methods
}

// CODE SMELL: Lazy Class (Clean Code Ch 17, p. 295)
// This class has only one method and doesn't justify its own class.
// Refactor by: Inline the method into the calling class.
public static class PriorityFormatter
{
    public static string Format(int priority) => $"Priority: {priority}";
}
```

#### Dead Code (1-2 instances)

```csharp
// CODE SMELL: Dead Code (Clean Code Ch 17, p. 296)
// This method is never called and serves no purpose.
// Refactor by: Delete the method (or verify it's truly unused first).
private TaskDto OldMappingMethod(TaskEntity entity)
{
    // Old implementation, replaced by TaskMapper
    // ... unused code
}

// CODE SMELL: Comments (Clean Code Ch 17, p. 297)
// This commented-out code is dead code that should be deleted.
// If you need it later, use version control history.
// Refactor by: Delete commented code.
// private void OldImplementation()
// {
//     // ... 20 lines of commented code
// }
```

---

## Step 4: Create Smell Catalog Document (1 hour)

### Document Structure: `docs/code-smells-catalog.md`

```markdown
# Code Smells Catalog - TaskFlowAPI

**Purpose:** Reference document for Week 18 students to understand intentional code smells added to the codebase.

**Total Smells:** 20-25  
**Difficulty Distribution:**
- Easy: 8-10 smells
- Medium: 8-10 smells
- Hard: 4-5 smells

---

## Smell #1: Long Method - TaskService.CreateTaskAsync

**Location:** `TaskFlowAPI/Services/Tasks/TaskService.cs`, line 45  
**Type:** Long Method (Clean Code Ch 17, p. 288)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
The `CreateTaskAsync` method is 45 lines and handles multiple responsibilities:
1. Validates request
2. Calculates default values
3. Creates entity via factory
4. Saves to database
5. Maps to DTO
6. Logs result

**Why It's Bad:**
- Violates Single Responsibility Principle
- Hard to test individual concerns
- Difficult to understand at a glance
- Changes to one concern affect others

**Suggested Refactoring:**
Extract smaller methods:
- `ValidateCreateRequest()`
- `CalculateDefaultValues()`
- `CreateAndSaveTask()`
- `LogTaskCreation()`

**Expected Time to Fix:** 30-45 minutes

---

[Repeat for each smell...]
```

---

## Step 5: Update Week 18 Assignment (1 hour)

### Add to `week-18-code-smells-refactoring.md`:

```markdown
## 11. Code Smell Scavenger Hunt (NEW)

**Goal:** Find and fix intentional code smells added to TaskFlowAPI as educational examples.

### Part A: Find the Smells (30 minutes)

1. **Read the Code Smells Catalog:**
   - Review `docs/code-smells-catalog.md`
   - Understand what smells to look for
   - Note the comment format: `// CODE SMELL: [Name]`

2. **Search for Smells:**
   - Search codebase for "CODE SMELL" comments
   - Or use IDE search: `// CODE SMELL`
   - Document each smell you find

3. **Create Your Smell Inventory:**
   Create `docs/my-week-18-smell-inventory.md`:
   ```markdown
   # My Code Smell Inventory
   
   ## Smells Found:
   1. Long Method - TaskService.CreateTaskAsync (line 45)
   2. Duplicate Code - Validators (multiple locations)
   3. ...
   ```

### Part B: Fix the Smells (90 minutes)

**Target:** Find and fix **5 of the 20-25 intentional smells**

**Requirements:**
- Fix at least 1 "Easy" smell
- Fix at least 2 "Medium" smells
- Fix at least 1 "Hard" smell (optional, but recommended)
- Document your refactoring approach

**For Each Smell Fixed:**
1. Read the smell comment to understand the issue
2. Apply appropriate refactoring technique
3. Run tests to ensure behavior unchanged
4. Document in PR description:
   - Smell name and location
   - Refactoring technique used
   - Before/after comparison

### Part C: Reflection (15 minutes)

Answer in your journal:
- Which smell was easiest to identify? Why?
- Which refactoring was most challenging? Why?
- How did fixing smells improve code quality?
- What patterns did you notice across multiple smells?

### Success Criteria:
- ✅ Found at least 5 intentional smells
- ✅ Fixed at least 5 smells using appropriate refactoring
- ✅ All tests pass after refactoring
- ✅ PR description documents each fix
- ✅ Smell inventory document created
```

---

## Verification Checklist

Before completing this task:

- [ ] Read Clean Code Chapter 17 completely
- [ ] Created smell inventory document
- [ ] Added 20-25 intentional smells to codebase
- [ ] Each smell has explanatory comment with:
  - [ ] Smell name
  - [ ] Chapter 17 page reference
  - [ ] Why it's bad
  - [ ] Suggested refactoring
- [ ] All smells are fixable by students
- [ ] All tests still pass (functionality unchanged)
- [ ] Created `docs/code-smells-catalog.md`
- [ ] Updated Week 18 assignment with scavenger hunt
- [ ] Verified smells are distributed across codebase
- [ ] Mixed difficulty levels (easy/medium/hard)

---

## Example Smell Distribution

**Controllers:** 3-4 smells
- Long Parameter List (1)
- Feature Envy (1)
- Long Method (1-2)

**Services:** 4-5 smells
- Long Method (2)
- Duplicate Code (1)
- Primitive Obsession (1)
- Switch Statements (1)

**Repositories:** 2-3 smells
- Data Clumps (1)
- Primitive Obsession (1)
- Long Parameter List (1)

**Validators:** 2-3 smells
- Duplicate Code (1)
- Long Parameter List (1)
- Switch Statements (1)

**DTOs:** 1-2 smells
- Data Class (1-2)

**Tests:** 2-3 smells
- Duplicate Code (1)
- Obscure Test (1-2)

**Helpers:** 1-2 smells
- Large Class (1)
- Lazy Class (1)

**General:** 2-3 smells
- Dead Code (1-2)
- Comments (1)

**Total:** 20-25 smells

---

## Notes

- **Don't break functionality:** All smells should be cosmetic/structural, not logical errors
- **Make it educational:** Each smell should teach a specific Clean Code principle
- **Vary difficulty:** Mix easy wins with challenging refactorings
- **Document well:** Comments should be clear and educational
- **Test thoroughly:** Ensure all tests pass after adding smells
