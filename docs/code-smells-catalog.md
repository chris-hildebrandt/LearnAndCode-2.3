# Code Smells Catalog - TaskFlowAPI

**Purpose:** Reference document for Week 18 students to understand intentional code smells added to the codebase.

**Total Smells:** 32  
**Difficulty Distribution:**
- Easy: 18 smells
- Medium: 12 smells
- Hard: 2 smells

**How to Use:**
1. Search codebase for `// CODE SMELL:` comments
2. Read the smell explanation
3. Apply appropriate refactoring technique
4. Verify tests still pass
5. Document your fix in PR description

---

## Smell #1: Large Class - TaskHelper

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, entire class  
**Type:** Large Class (Clean Code Ch 17, p. 294)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
The `TaskHelper` class has 12+ methods doing unrelated things:
- String formatting
- Date calculations
- Validation helpers
- Mapping utilities
- Priority formatting

**Why It's Bad:**
- Violates Single Responsibility Principle
- Class has multiple reasons to change
- Hard to test (too many concerns)
- Difficult to understand at a glance

**Suggested Refactoring:**
Split into focused classes:
- `StringFormatter` - string formatting methods
- `DateHelper` - date calculation methods
- `ValidationHelper` - validation methods
- `PriorityFormatter` - priority-related methods

**Expected Time to Fix:** 45-60 minutes

---

## Smell #2: Primitive Obsession - Priority Values

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 18  
**Type:** Primitive Obsession (Clean Code Ch 17, p. 292)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
Using `int` for priority (0-5) instead of a type-safe Priority enum or value object.

**Why It's Bad:**
- Allows invalid values (e.g., priority = 99)
- Makes code less readable
- No compile-time safety
- Magic numbers throughout codebase

**Suggested Refactoring:**
Create Priority enum:
```csharp
public enum Priority
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3,
    Urgent = 4,
    Critical = 5
}
```

**Expected Time to Fix:** 30-45 minutes

---

## Smell #3: Switch Statements - Priority Formatting

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 25  
**Type:** Switch Statements (Clean Code Ch 17, p. 293)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
Switch statement on priority value suggests polymorphism would be better.

**Why It's Bad:**
- Violates Open/Closed Principle
- Must modify method to add new priority
- Harder to extend

**Suggested Refactoring:**
Replace with Priority enum with behavior or Strategy pattern.

**Expected Time to Fix:** 20-30 minutes

---

## Smell #4: Long Method - CalculateDueDate

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 35  
**Type:** Long Method (Clean Code Ch 17, p. 288)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
The `CalculateDueDate` method is 25+ lines and does multiple things:
1. Validates input
2. Calculates date
3. Formats output
4. Handles edge cases

**Why It's Bad:**
- Violates Single Responsibility Principle
- Hard to test individual concerns
- Difficult to understand at a glance

**Suggested Refactoring:**
Extract smaller methods:
- `ValidateDaysToAdd()`
- `AddDaysSkippingWeekends()`
- `FormatDateForDisplay()`

**Expected Time to Fix:** 30-45 minutes

---

## Smell #5: Duplicate Code - Validation Pattern

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 60  
**Type:** Duplicate Code (Clean Code Ch 17, p. 289)  
**Severity:** Medium  
**Difficulty:** Easy

**Description:**
This validation pattern (null check + throw) appears in 3 other methods in this class.

**Why It's Bad:**
- Violates DRY (Don't Repeat Yourself)
- Changes must be made in multiple places
- Risk of inconsistency

**Suggested Refactoring:**
Extract to shared validation method:
```csharp
private static void ValidateNonNegative(int value, string parameterName)
{
    if (value < 0)
        throw new ArgumentException($"{parameterName} cannot be negative");
}
```

**Expected Time to Fix:** 20-30 minutes

---

## Smell #6: Long Parameter List - FilterTasks

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 75  
**Type:** Long Parameter List (Clean Code Ch 17, p. 290)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
This method has 7 parameters, violating the "fewer arguments are better" principle.

**Why It's Bad:**
- Hard to remember parameter order
- Easy to pass wrong values
- Suggests parameters should be grouped

**Suggested Refactoring:**
Create TaskQueryParameters class:
```csharp
public class TaskQueryParameters
{
    public bool? IsCompleted { get; set; }
    public int? MinPriority { get; set; }
    public int? MaxPriority { get; set; }
    public DateTime? DueBefore { get; set; }
    public DateTime? DueAfter { get; set; }
    public int? ProjectId { get; set; }
}
```

**Expected Time to Fix:** 30-45 minutes

---

## Smell #7: Feature Envy - FormatTaskSummary

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 120  
**Type:** Feature Envy (Clean Code Ch 17, p. 291)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This method accesses multiple properties of TaskDto to format a message. The formatting logic should be on the DTO itself.

**Why It's Bad:**
- Violates encapsulation
- Logic belongs with the data
- Harder to maintain

**Suggested Refactoring:**
Move formatting logic to TaskDto.FormatSummary() method.

**Expected Time to Fix:** 15-20 minutes

---

## Smell #8: Data Clumps - Date Range Parameters

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 130  
**Type:** Data Clumps (Clean Code Ch 17, p. 291)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
These three parameters (startDate, endDate, includeCompleted) are always passed together and represent a single concept: "date range filter".

**Why It's Bad:**
- Parameters always appear together
- Suggests they should be grouped
- Harder to maintain consistency

**Suggested Refactoring:**
Create DateRangeFilter class to encapsulate these parameters.

**Expected Time to Fix:** 30-45 minutes

---

## Smell #9: Dead Code - OldFormatTitle

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 160  
**Type:** Dead Code (Clean Code Ch 17, p. 296)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This method is never called and serves no purpose.

**Why It's Bad:**
- Clutters codebase
- Confuses readers
- Wastes maintenance effort

**Suggested Refactoring:**
Delete the method (or verify it's truly unused first).

**Expected Time to Fix:** 5 minutes

---

## Smell #10: Comments - Bad Comments

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 170  
**Type:** Comments (Clean Code Ch 17, p. 297)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
Commented-out code is dead code that should be deleted.

**Why It's Bad:**
- Dead code clutters the file
- Version control has the history
- Confuses future developers

**Suggested Refactoring:**
Delete commented code.

**Expected Time to Fix:** 5 minutes

---

## Smell #11: Lazy Class - TrimTitle

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 180  
**Type:** Lazy Class (Clean Code Ch 17, p. 295)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This method has only one simple operation and doesn't justify its own method in a utility class.

**Why It's Bad:**
- Unnecessary abstraction
- Adds indirection without benefit
- Could be inlined

**Suggested Refactoring:**
Inline the method into the calling class, or remove if trivial.

**Expected Time to Fix:** 10-15 minutes

---

## Smell #12: Speculative Generality - FormatString

**Location:** `TaskFlowAPI/Helpers/TaskHelper.cs`, line 190  
**Type:** Speculative Generality (Clean Code Ch 17, p. 296)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This method was created "just in case" but is only used once (or never).

**Why It's Bad:**
- Premature abstraction
- Adds complexity without benefit
- YAGNI (You Aren't Gonna Need It) violation

**Suggested Refactoring:**
Inline the method or remove if unused.

**Expected Time to Fix:** 10-15 minutes

---

## Smell #13: Long Method - GenerateProjectSummaryReport

**Location:** `TaskFlowAPI/Controllers/ReportsController.cs`, line 24  
**Type:** Long Method (Clean Code Ch 17, p. 288)  
**Severity:** High  
**Difficulty:** Medium

**Description:**
This method is 95+ lines and does multiple things:
1. Validates project ID
2. Fetches all tasks
3. Filters tasks by project
4. Calculates statistics
5. Identifies overdue tasks
6. Identifies high priority tasks
7. Finds next deadline
8. Assembles DTO

**Why It's Bad:**
- Violates Single Responsibility Principle
- Hard to test individual concerns
- Difficult to understand at a glance
- Changes to one concern affect others

**Suggested Refactoring:**
Extract smaller methods:
- `ValidateProjectId()`
- `FilterTasksByProject()`
- `CalculateStatistics()`
- `IdentifyOverdueTasks()`
- `IdentifyHighPriorityTasks()`
- `FindNextDeadline()`
- `AssembleSummaryDto()`

**Expected Time to Fix:** 45-60 minutes

---

## Smell #14: Duplicate Code - Filtering Pattern

**Location:** `TaskFlowAPI/Controllers/ReportsController.cs`, lines 50, 88, 105  
**Type:** Duplicate Code (Clean Code Ch 17, p. 289)  
**Severity:** Medium  
**Difficulty:** Easy

**Description:**
This filtering pattern (foreach + if + add) is repeated 3 times in this method.

**Why It's Bad:**
- Violates DRY principle
- Changes must be made in multiple places
- Risk of inconsistency

**Suggested Refactoring:**
Extract to shared filtering method:
```csharp
private List<TaskDto> FilterTasks(List<TaskDto> tasks, Func<TaskDto, bool> predicate)
{
    return tasks.Where(predicate).ToList();
}
```

**Expected Time to Fix:** 20-30 minutes

---

## Smell #15: Magic Numbers - Percentage Calculation

**Location:** `TaskFlowAPI/Controllers/ReportsController.cs`, line 85  
**Type:** Magic Numbers (Clean Code Ch 17, G25)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
The number 100 is a magic number used for percentage calculation.

**Why It's Bad:**
- Unclear what the number represents
- Hard to maintain if calculation changes
- Not self-documenting

**Suggested Refactoring:**
Extract to named constant:
```csharp
private const int PercentageMultiplier = 100;
```

**Expected Time to Fix:** 5 minutes

---

## Smell #16: Magic Numbers - High Priority

**Location:** `TaskFlowAPI/Controllers/ReportsController.cs`, line 112  
**Type:** Magic Numbers (Clean Code Ch 17, G25)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
The number 1 is a magic number representing "High" priority.

**Why It's Bad:**
- Unclear what priority 1 means
- Should use Priority enum or named constant

**Suggested Refactoring:**
Extract to named constant or use Priority enum:
```csharp
private const int HighPriority = 1;
```

**Expected Time to Fix:** 5 minutes

---

## Smell #17: Magic Numbers - Decimal Precision

**Location:** `TaskFlowAPI/Controllers/ReportsController.cs`, line 141  
**Type:** Magic Numbers (Clean Code Ch 17, G25)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
The number 2 is a magic number representing decimal precision.

**Why It's Bad:**
- Unclear what 2 represents
- Hard to change if precision requirements change

**Suggested Refactoring:**
Extract to named constant:
```csharp
private const int DecimalPlaces = 2;
```

**Expected Time to Fix:** 5 minutes

---

## Smell #18: Feature Envy - MapToDto

**Location:** `TaskFlowAPI/Services/Tasks/TaskService.cs`, line 79  
**Type:** Feature Envy (Clean Code Ch 17, p. 291)  
**Severity:** Medium  
**Difficulty:** Easy

**Description:**
This method accesses multiple properties of TaskEntity to create a DTO. The mapping logic should be in a dedicated mapper class.

**Why It's Bad:**
- Violates Single Responsibility Principle
- Service shouldn't know DTO structure
- Harder to test mapping separately

**Suggested Refactoring:**
Extract to TaskMapper class (Week 11 exercise).

**Expected Time to Fix:** 20-30 minutes

---

## Smell #19: Duplicate Code - Mapping Pattern

**Location:** `TaskFlowAPI/Services/Tasks/TaskService.cs`, line 108  
**Type:** Duplicate Code (Clean Code Ch 17, p. 289)  
**Severity:** Medium  
**Difficulty:** Easy

**Description:**
This mapping pattern (property-by-property assignment) is similar to MapToDto above. Both methods do data transformation.

**Why It's Bad:**
- Violates DRY principle
- Mapping logic scattered
- Harder to maintain consistency

**Suggested Refactoring:**
Extract both to TaskMapper class to centralize mapping logic.

**Expected Time to Fix:** 20-30 minutes

---

## Smell #20: Data Class - TaskDto

**Location:** `TaskFlowAPI/DTOs/Responses/TaskDto.cs`, entire class  
**Type:** Data Class (Clean Code Ch 17, p. 295)  
**Severity:** Low  
**Difficulty:** Medium

**Description:**
This DTO has only getters/setters with no behavior.

**Why It's Bad:**
- Could have helpful methods
- Logic scattered elsewhere
- Less object-oriented

**Suggested Refactoring:**
Add helper methods like:
- `IsOverdue()`
- `GetPriorityLabel()`
- `FormatSummary()`

**Expected Time to Fix:** 30-45 minutes

---

## Smell #21: Data Clumps - Date Range

**Location:** `TaskFlowAPI/Services/Tasks/Filters/DueDateTaskFilter.cs`, line 11  
**Type:** Data Clumps (Clean Code Ch 17, p. 291)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
These two parameters (start, end) are always passed together and represent a single concept: "date range".

**Why It's Bad:**
- Parameters always appear together
- Suggests they should be grouped
- Harder to maintain consistency

**Suggested Refactoring:**
Create DateRange class:
```csharp
public class DateRange
{
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
```

**Expected Time to Fix:** 30-45 minutes

---

## Smell #22: Comments - Bad Comments

**Location:** `TaskFlowAPI/Validators/CreateTaskValidator.cs`, line 23  
**Type:** Comments (Clean Code Ch 17, p. 297)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This comment explains what code should do, but the code doesn't exist yet. Once validation rules are implemented, this comment becomes redundant.

**Why It's Bad:**
- Comment explains "what" instead of "why"
- Will become outdated
- Code should be self-explanatory

**Suggested Refactoring:**
Remove comment once code is implemented.

**Expected Time to Fix:** 5 minutes

---

## Smell #23: Primitive Obsession - Task ID

**Location:** `TaskFlowAPI/Repositories/TaskRepository.cs`, line 49  
**Type:** Primitive Obsession (Clean Code Ch 17, p. 292)  
**Severity:** Low  
**Difficulty:** Hard

**Description:**
Using `int` for ID instead of a type-safe TaskId value object.

**Why It's Bad:**
- Allows invalid values (negative, zero)
- Less type-safe
- Can mix up different ID types

**Suggested Refactoring:**
Create TaskId value object class (advanced refactoring):
```csharp
public record TaskId(int Value)
{
    public static TaskId From(int value) => value > 0 
        ? new TaskId(value) 
        : throw new ArgumentException("Task ID must be positive");
}
```

**Expected Time to Fix:** 60-90 minutes (advanced)

---

## Smell #24: Obscure Test - Test Name

**Location:** `TaskFlowAPI.Tests/Examples/TaskServiceTests.Example.cs`, line 28  
**Type:** Obscure Test (Clean Code Ch 17, p. 299)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
This test name doesn't clearly describe what scenario is being tested. The test body is also unclear about expected behavior.

**Why It's Bad:**
- Hard to understand test purpose
- Difficult to maintain
- Fails to document behavior

**Suggested Refactoring:**
Use descriptive test name following "MethodName_Scenario_ExpectedBehavior" pattern.

**Expected Time to Fix:** 10-15 minutes

---

## Smell #25: Comments - Redundant Test Comments

**Location:** `TaskFlowAPI.Tests/Examples/TaskServiceTests.Example.cs`, line 39  
**Type:** Comments (Clean Code Ch 17, p. 297)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
These comments explain what the code does, which should be obvious from reading the code. Comments like "ARRANGE: In this section..." are redundant.

**Why It's Bad:**
- Comments repeat what code says
- Code should be self-documenting
- Comments become outdated

**Suggested Refactoring:**
Remove explanatory comments, keep only "why" comments if needed.

**Expected Time to Fix:** 10-15 minutes

---

## Smell #26: Magic Numbers - Test Data

**Location:** `TaskFlowAPI.Tests/Examples/TaskServiceTests.Example.cs`, lines 59, 60, 72  
**Type:** Magic Numbers (Clean Code Ch 17, G25)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
The numbers 2, 1, and 42 are magic numbers without clear meaning in test context.

**Why It's Bad:**
- Unclear what values represent
- Hard to understand test intent
- Difficult to maintain

**Suggested Refactoring:**
Extract to named constants:
```csharp
private const int MediumPriority = 2;
private const int DefaultProjectId = 1;
private const int TestTaskId = 42;
```

**Expected Time to Fix:** 10-15 minutes

## Smell #27: Magic Strings - Configuration Keys

**Location:** `TaskFlowAPI/Program.cs`, line ~51  
**Type:** Magic Strings (Clean Code Ch 17, G25)  
**Severity:** Low  
**Difficulty:** Easy

**Description:**
The configuration key "DefaultConnection" is hardcoded as a magic string. If the key is renamed in `appsettings.json`, the code breaks silently or throws at runtime without compile-time verification.

**Why It's Bad:**
- String magic values are not refactoring-safe
- Easy to misspell or mistype
- No compile-time verification of key names
- Scattered throughout code, hard to find and change consistently

**Suggested Refactoring:**
Extract to named constant:
```csharp
private const string DefaultConnectionKey = "DefaultConnection";
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString(DefaultConnectionKey)));
```

**Expected Time to Fix:** 10 minutes

---

## Smell #28: Data Class - ProjectEntity

**Location:** `TaskFlowAPI/Entities/ProjectEntity.cs`, entire class  
**Type:** Data Class (Clean Code Ch 17, p. 295)  
**Severity:** Low  
**Difficulty:** Medium

**Description:**
The `ProjectEntity` has only getters/setters with no behavior or domain logic. It's a pure data container without methods like `AddTask()`, `RemoveTask()`, `UpdateName()`, etc.

**Why It's Bad:**
- Violates object-oriented principles (encapsulation)
- Logic that works with projects is scattered elsewhere
- Missed opportunity for domain-driven design
- No invariant protection (e.g., name length, task count limits)

**Suggested Refactoring:**
Add domain behavior methods:
```csharp
public void AddTask(TaskEntity task) { /* ... */ }
public void RemoveTask(TaskEntity task) { /* ... */ }
public void UpdateName(string newName) { /* ... */ }
public void UpdateDescription(string newDescription) { /* ... */ }
public int GetTaskCount() => Tasks.Count;
public bool HasOverdueTasks() => Tasks.Any(t => !t.IsCompleted && t.DueDate < DateTime.UtcNow);
```

**Expected Time to Fix:** 30-45 minutes

---

## Smell #29: Feature Envy - ProjectEntity Name Property

**Location:** `TaskFlowAPI/Entities/ProjectEntity.cs`, line 24  
**Type:** Feature Envy (Clean Code Ch 17, p. 291)  
**Severity:** Low  
**Difficulty:** Medium

**Description:**
The `ProjectEntity` doesn't provide methods to work with its own name. Other classes validate, format, or manipulate the name externally instead of encapsulating that behavior.

**Why It's Bad:**
- Logic for working with project names is scattered throughout codebase
- Violates encapsulation
- Multiple places might implement name validation inconsistently
- Harder to maintain (changes in multiple places)

**Suggested Refactoring:**
Add methods to encapsulate name behavior:
```csharp
public void ValidateName(string name)
{
    if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentException("Name cannot be empty");
    if (name.Length > 200)
        throw new ArgumentException("Name cannot exceed 200 characters");
}

public string GetDisplayName() => Name.Trim();

public void UpdateName(string newName)
{
    ValidateName(newName);
    Name = newName;
}
```

**Expected Time to Fix:** 20-30 minutes

---

## Smell #30: Primitive Obsession - DueDateTaskFilter Date Range

**Location:** `TaskFlowAPI/Services/Tasks/Filters/DueDateTaskFilter.cs`, lines 18-22  
**Type:** Primitive Obsession (Clean Code Ch 17, p. 292)  
**Severity:** Medium  
**Difficulty:** Hard

**Description:**
Using nullable `DateTime` for range boundaries without validation or encapsulation. There's no guarantee that `_start <= _end`, allowing invalid filter states like `start = 2025-01-31, end = 2025-01-01`.

**Why It's Bad:**
- No invariant protection (start must be <= end)
- Filter behavior becomes unpredictable with invalid dates
- Validation logic scattered wherever filters are created
- No type safety for the concept of "date range"

**Suggested Refactoring:**
Create a `DateRange` value object:
```csharp
public record DateRange
{
    public DateTime? Start { get; }
    public DateTime? End { get; }
    
    public DateRange(DateTime? start, DateTime? end)
    {
        if (start.HasValue && end.HasValue && start.Value > end.Value)
            throw new ArgumentException("Start date must be <= end date");
        Start = start;
        End = end;
    }
}

// Then use in filter:
public DueDateTaskFilter(DateRange dateRange) { /* ... */ }
```

**Expected Time to Fix:** 45-60 minutes (advanced)

---

## Smell #31: Duplicate Code - UpdateTaskValidator vs CreateTaskValidator

**Location:** `TaskFlowAPI/Validators/UpdateTaskValidator.cs`, entire class  
**Type:** Duplicate Code (Clean Code Ch 17, p. 289)  
**Severity:** Medium  
**Difficulty:** Medium

**Description:**
The `UpdateTaskValidator` will likely have similar rule definitions (property validation chains) as `CreateTaskValidator`. For example, both validate Title length, Priority range, and DueDate validity. Only the conditions (required vs. optional) differ.

**Why It's Bad:**
- Violates DRY principle
- Changes to validation rules must be made in two places
- Risk of inconsistency (e.g., update allows title of 500 chars, create allows 200)
- Harder to maintain

**Suggested Refactoring:**
Extract common validation rules:
```csharp
public static class TaskValidationRules
{
    public static IRuleBuilderOptions<T, string> ValidateTitle<T>(
        this IRuleBuilder<T, string> rule)
        => rule
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");
    
    public static IRuleBuilderOptions<T, int> ValidatePriority<T>(
        this IRuleBuilder<T, int> rule)
        => rule
            .GreaterThanOrEqualTo(0).WithMessage("Priority must be 0-5")
            .LessThanOrEqualTo(5);
}

// Then in both validators:
RuleFor(x => x.Title).ValidateTitle();
RuleFor(x => x.Priority).ValidatePriority();
```

**Expected Time to Fix:** 30-45 minutes

---

## Smell #32: Abbreviations & Unclear Names - TasksController

**Location:** `TaskFlowAPI/Controllers/TasksController.cs`, entire class  
**Type:** Abbreviations & Unclear Names (Clean Code Ch 2, pp. 17-48)
**Severity:** Medium
**Difficulty:** Medium
**Description:**
The controller uses multiple abbreviations and unclear names:
- Field: `svc` (should be `taskService`)
- Parameter: `s` (should be `taskService`)
- Variables: `t` (should be `allTasks` or `tasks`)
- Variables: `dt` (should be `createdTask` or `newTaskDto`)
- Parameter: `req` (should be `createTaskRequest`)
- Methods: `Get()` (should be `GetAllTasks()`)
- Methods: `GetOne()` (should be `GetTaskById()`)
- Methods: `Add()` (should be `CreateTask()`)

**Why It's Bad:**
- Names are not searchable (if you search for "svc", you get noise)
- Names are not pronounceable (can't discuss "essVeeC" in code review)
- Abbreviations obscure intent and confuse new team members
- Single-letter variables are ambiguous and often misleading

**Suggested Refactoring:**
Apply Clean Code Chapter 2 principles:
1. Replace abbreviations: `svc` → `taskService`, `req` → `createTaskRequest`, `t` → `allTasks`, `dt` → `createdTask`
2. Clarify method names: `Get()` → `GetAllTasks()`, `GetOne()` → `GetTaskById()`, `Add()` → `CreateTask()`
3. Ensure names are pronounceable and searchable
4. Use no single-letter variables (except loop counters)

**Example:**
```csharp
private readonly ITaskService _taskService;

public TasksController(ITaskService taskService)
{
    _taskService = taskService;
}

[HttpGet]
public async Task<IActionResult> GetAllTasks(CancellationToken cancellationToken)
{
    var allTasks = await _taskService.GetAll(cancellationToken);
    return Ok(allTasks);
}

[HttpPost]
public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest createRequest, CancellationToken cancellationToken)
{
    var createdTask = await _taskService.Add(createRequest, cancellationToken);
    return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
}
```

**Expected Time to Fix:** 30-45 minutes

---

## Summary by Difficulty

### Easy (18 smells)
- Smell #3: Switch Statements
- Smell #5: Duplicate Code (Validation)
- Smell #7: Feature Envy
- Smell #9: Dead Code
- Smell #10: Comments (TaskHelper)
- Smell #11: Lazy Class
- Smell #12: Speculative Generality
- Smell #14: Duplicate Code (Filtering)
- Smell #15: Magic Numbers (Percentage)
- Smell #16: Magic Numbers (Priority)
- Smell #17: Magic Numbers (Decimal)
- Smell #18: Feature Envy (Mapping)
- Smell #19: Duplicate Code (Mapping)
- Smell #22: Comments (Validator)
- Smell #24: Obscure Test - Test Name
- Smell #25: Comments (Redundant Test Comments)
- Smell #26: Magic Numbers (Tests)
- Smell #27: Magic Strings (Configuration Keys)

### Medium (12 smells)
- Smell #1: Large Class
- Smell #2: Primitive Obsession (Priority)
- Smell #4: Long Method (CalculateDueDate)
- Smell #6: Long Parameter List
- Smell #8: Data Clumps (Date Range)
- Smell #13: Long Method (Report)
- Smell #20: Data Class - TaskDto
- Smell #21: Data Clumps (Date Range)
- Smell #28: Data Class - ProjectEntity
- Smell #29: Feature Envy - ProjectEntity Name Property
- Smell #31: Duplicate Code - UpdateTaskValidator vs CreateTaskValidator
- Smell #32: Abbreviations & Unclear Names - TasksController

### Hard (2 smells)
- Smell #23: Primitive Obsession - Task ID
- Smell #30: Primitive Obsession - DueDateTaskFilter Date Range

---

## How to Find Smells

**Method 1: Search for Comments**
```bash
# Search for CODE SMELL comments
grep -r "CODE SMELL" TaskFlowAPI/
```

**Method 2: IDE Search**
- In Visual Studio/VS Code: Search for `// CODE SMELL`
- Use Find in Files (Ctrl+Shift+F)

**Method 3: Review by File**
- Check each file in the codebase
- Look for the comment format: `// CODE SMELL: [Name]`

---

## Refactoring Techniques Reference

**Extract Method:** Break long method into smaller methods  
**Extract Class:** Split large class into focused classes  
**Replace Primitive with Object:** Create value objects for primitives  
**Parameter Object:** Group related parameters into a class  
**Replace Conditional with Polymorphism:** Use inheritance/strategy instead of switch  
**Remove Dead Code:** Delete unused code  
**Inline Method/Class:** Remove unnecessary abstraction  
**Move Method:** Move method to appropriate class  
**Extract Constant:** Replace magic numbers with named constants

---

## Success Criteria

After fixing smells, verify:
- ✅ All tests pass (`dotnet test`)
- ✅ Code compiles without errors
- ✅ Functionality unchanged (manual test via Swagger)
- ✅ Code is more readable
- ✅ Each class/method has single responsibility
- ✅ No new smells introduced

---

**Total Estimated Time to Fix All Smells:** 8-12 hours  
**Recommended:** Fix 5-7 smells for Week 18 assignment (2-3 hours)
