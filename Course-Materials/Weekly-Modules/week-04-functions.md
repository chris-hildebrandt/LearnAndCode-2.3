# Week 4: Functions

This week we will practice refactoring code with a focus on improving function names and structure. The purpose of this example is to demonstrate the positive impact of well-named and well-structured functions on code readability and maintainability. This module is based on **Clean Code Chapter 3: Functions**.

## 1. Learning Objectives

- **Identify** multiple responsibilities within a single large function.
- **Extract** logical blocks of code into small, private helper functions with clear, descriptive names.
- **Apply** the "Stepdown Rule," where a function's body reads like a high-level summary, and the details are in the functions it calls.
- **Refactor** a primary function to be a simple coordinator of calls to these new helper functions.
- Design small, intention-revealing controller actions.
- Extend `TaskFlow` endpoints (update/delete) while keeping flows cohesive.

## 2. Reading & Resources (60 min)

- **Clean Code – Chapter 3: Functions (pp. 35-58)**.
  - Focus on small functions, descriptive names, and minimizing arguments.

## 3. This Week’s Work

This week, a new feature request came in: "As a project manager, I need a summary report for a project so I can quickly assess its status."

A new `ReportsController.cs` has been added to the project with a `GenerateProjectSummaryReport` method. This method works, but it was written quickly and violates the principles of clean functions. It's long, hard to read, and mixes different levels of abstraction.

Your task is to refactor this `GenerateProjectSummaryReport` method.

### The "Before" State: A Function Doing Too Much
The current `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs` is responsible for:
1.  Validating the input `projectId`.
2.  Fetching all tasks.
3.  Filtering tasks for the specified project.
4.  Calculating statistics (total tasks, completed tasks, percentage complete).
5.  Identifying overdue tasks.
6.  Identifying high-priority tasks.
7.  Finding the next upcoming deadline.
8.  Assembling the final `ProjectSummaryDto` response.

This is a classic example of a function that does more than one thing.

### Your Refactoring Goal
Refactor the `GenerateProjectSummaryReport` method by extracting its various responsibilities into several small, well-named private helper methods. The final `GenerateProjectSummaryReport` method should be short and easy to read, delegating all the work to the helper methods you create.

You should be able to extract at least **five** distinct helper methods.

- Extend `ITaskService` with `UpdateTaskAsync` and `DeleteTaskAsync` signatures using expressive parameter names.
- Implement controller actions:
  - `PUT /api/tasks/{taskId}` consumes `UpdateTaskRequest`, returns `204 No Content`.
  - `DELETE /api/tasks/{taskId}` returns `204 No Content` (idempotent).
- Leave service/repository `TODOs` in place (students will implement later weeks) but ensure controller flow reads cleanly.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/ReportsController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/DTOs/Requests/UpdateTaskRequest.cs` (create or update as needed)
- This file (`Course-Materials/Weekly-Modules/week-04-functions.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### Core Assignment (Required): Refactor GenerateProjectSummaryReport

1.  Create a new branch for your work: `week-04/<your-name>`.
2.  **NEW:** Complete Method Extraction Decision Framework (see Section 11 below) - 10 minutes
3.  Locate and read the `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs`.
4.  **REVISED:** Apply extraction framework to identify candidates (comment blocks, scores, order)
5.  Extract methods in CORRECT ORDER (lowest-level operations first, then coordinators)
6.  Examples of methods you might create:
    -   `FilterTasksByProject(IEnumerable<TaskDto> allTasks, int projectId)`
    -   `CalculateCompletionPercentage(IEnumerable<TaskDto> projectTasks)`
    -   `CountOverdueTasks(IEnumerable<TaskDto> projectTasks)`
    -   `FindNextUpcomingDeadline(IEnumerable<TaskDto> projectTasks)`
    -   `AssembleSummaryDto(...)`
7.  Review your final `GenerateProjectSummaryReport` method. It should now be very short and read like a high-level summary of the steps involved in generating the report.
8.  Ensure the code still builds and runs.
9.  **NEW:** Compare your refactor with example (see Section 12 below)

### Extension (OPTIONAL - if time permits): Add UPDATE/DELETE Endpoints

**Why Optional:**
This week's learning objective is FUNCTION EXTRACTION. Adding new endpoints is a different skill (API design, HTTP semantics). If you're still learning function refactoring, focus there. You can always add UPDATE/DELETE later.

**If you choose to do extension:**
10. Add `UpdateTaskAsync`/`DeleteTaskAsync` to `ITaskService` interface
11. Implement `PUT /api/tasks/{id}` and `DELETE /api/tasks/{id}` controller actions
12. Leave service implementation as `NotImplementedException` (will implement in Week 9)
13. Ensure endpoint names follow HTTP conventions

## 6. How to Test

You can manually test your changes to ensure the logic remains the same.
1.  Run the API: `dotnet run --project TaskFlowAPI`
2.  Use a tool like Postman, or the `TaskFlowAPI.http` file in Visual Studio Code, to send a `GET` request to the new endpoint:
    ```http
    GET http://localhost:5000/api/Reports/project-summary/1
    ```
    *(Note: The port may vary. Check the application output when it starts.)*

The endpoint should return a JSON summary that looks something like this:
```json
{
  "projectId": 1,
  "projectName": "Project 1",
  "totalTasks": 5,
  "completedTasks": 2,
  "percentageComplete": 40.0,
  "overdueTasks": 1,
  "highPriorityTasks": 2,
  "nextUpcomingDeadline": "2025-12-01T00:00:00Z"
}
```
*(The exact values will depend on the seed data in your database.)*

## 7. Success Criteria

**Core Assignment (Required):**
- The `GenerateProjectSummaryReport` method is refactored to be no more than 15 lines long.
- At least five private helper methods have been extracted from the original function.
- Each new helper method has a clear, descriptive name that reveals its intent.
- The `GenerateProjectSummaryReport` method now acts as a coordinator, and its body follows the Stepdown Rule.
- The API builds successfully and the `/api/Reports/project-summary/{projectId}` endpoint functions as it did before the refactor.
- **NEW:** Decision matrix completed and committed before refactoring

**Optional Extension (if completed):**
- UPDATE/DELETE endpoints added to controller
- Service interface extended with method signatures
- Service methods throw `NotImplementedException` (will implement Week 9)

## 8. Submission Process

1.  Commit your changes with a message like: `Refactor: Break down GenerateProjectSummaryReport into smaller functions`.
2.  Create a pull request. In the summary, list the helper functions you created and explain how your refactoring improved the code.
3.  In your weekly submission issue, include a "before" and "after" snippet of the `GenerateProjectSummaryReport` method to showcase the improvement.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Function Size:* Describe the most significant function you extracted. Why did you choose to separate it? What naming strategy did you use to make its purpose clear?
- *Readability:* How did applying the "Stepdown Rule" change the way you read and understand the `GenerateProjectSummaryReport` function?
- *Testability:* How might these smaller functions be easier to unit test than the original, large function?

### Discussion Prep:
- *Be prepared to share one of the helper functions you created and justify its name and purpose.*
- *Was there any part of the original function that was difficult to extract? Why?*
- *How does this exercise relate to the "Single Responsibility Principle" you will learn about later?*

## 10. Time Estimate

**Core Assignment:**
- 45 min – Reading & planning the refactor.
- 10 min – Method Extraction Decision Framework (NEW)
- 50 min – Implementing the refactoring by extracting methods (increased for systematic approach)
- 10 min – Compare with example (NEW)
- 20 min – Testing and creating the pull request.
**Core Total:** ~2 hours 15 minutes

**Optional Extension:**
- 30 min – Add UPDATE/DELETE endpoints (interface + controller stubs)

**Maximum Total (with extension):** ~2 hours 45 minutes

## 11. Method Extraction Decision Framework (NEW - Pre-Refactoring)

**Goal:** Extract systematically, not randomly

**Time:** 10 minutes

**Instructions:** Create `docs/week-04-extraction-plan.md` before coding

### Step 1: Identify Extraction Candidates

Read through `GenerateProjectSummaryReport`. Highlight blocks that:
1. Have a comment describing what they do (`// Calculate statistics`)
2. Could be described in 3-4 words (`FilterTasksByProject`)
3. Repeat a pattern (multiple similar operations)
4. Are indented ≥2 levels (nested logic)
5. Would make sense in isolation (don't reference variables from 20 lines earlier)

### Step 2: Decision Matrix

For EACH candidate block, score 1-3 (3 = definitely extract):

| Block Description | Lines | Has Descriptive Name? | Reusable? | Testable Alone? | Total Score |
|-------------------|-------|----------------------|-----------|-----------------|-------------|
| Filter tasks by project | 8 | Yes ("FilterTasksByProject") | Maybe | Yes | 2+2+2 = 6 |
| Count completed tasks | 6 | Yes | Likely | Yes | 2+2+3 = 7 |
| Calculate percentage | 5 | Yes | Likely | Yes | 2+2+3 = 7 |
| Find next deadline | 10 | Yes | Maybe | Yes | 2+2+2 = 6 |
| Validate projectId | 3 | Yes | No | Yes | 2+1+2 = 5 |
| (add your findings) | | | | | |

**Scoring:**
- **Has Descriptive Name:** Can you describe it in 3-4 words? (Yes=2, No=0)
- **Reusable:** Might another method use this? (Likely=3, Maybe=2, No=1)
- **Testable Alone:** Can you test without full context? (Yes=3, Partial=2, No=1)

**Extract if score ≥ 6**

### Step 3: Extraction Order (CRITICAL)

Extract in this order to avoid compiler errors:

1. **Lowest-level operations first** (leaf nodes) - no dependencies on other candidates
2. **Then mid-level** - may call low-level methods
3. **Finally top-level coordinator** - calls everything

**Example:**
```csharp
// BAD ORDER - won't compile:
private ProjectSummaryDto AssembleSummary(...)  // calls CountCompleted which doesn't exist yet
{
    var completed = CountCompleted(tasks);  // ERROR: method not found
}

private int CountCompleted(List<TaskDto> tasks) { ... }  // defined AFTER usage

// GOOD ORDER:
// 1. FIRST: Extract lowest-level (no dependencies)
private int CountCompleted(List<TaskDto> tasks) { ... }
private int CountOverdue(List<TaskDto> tasks) { ... }
private DateTime? FindNextDeadline(List<TaskDto> tasks) { ... }

// 2. THEN: Extract mid-level (calls low-level)
private double CalculatePercentage(int completed, int total) { ... }

// 3. FINALLY: Extract coordinator (calls everything)
private ProjectSummaryDto AssembleSummary(
    List<TaskDto> tasks,
    int projectId,
    string projectName)
{
    var completed = CountCompleted(tasks);  // ✓ exists now!
    var overdue = CountOverdue(tasks);
    var percentage = CalculatePercentage(completed, tasks.Count);
    var nextDeadline = FindNextDeadline(tasks);
    
    return new ProjectSummaryDto { ... };
}
```

**Your Extraction Order:**
1. _____________ (leaf)
2. _____________ (leaf)
3. _____________ (leaf)
4. _____________ (mid-level, uses above)
5. _____________ (coordinator, uses all)

### Step 4: "Too Far" Boundary

**Don't extract if:**
- Method would be ≤ 2 lines
- Name would be longer than code (`GetProjectIdFromRequest()` for `var id = request.ProjectId`)
- Only used once AND doesn't improve clarity
- Introduces more parameters than it removes

**Examples:**
```csharp
// ❌ TOO FAR - extraction adds no value
private int GetProjectId(ProjectSummaryRequest request)
{
    return request.ProjectId;  // Just return a property - don't extract
}

// ✓ GOOD - extraction clarifies logic
private bool IsTaskOverdue(TaskDto task)
{
    return !task.IsCompleted && 
           task.DueDate.HasValue && 
           task.DueDate.Value < DateTime.UtcNow;
}
```

### Deliverable

Commit `docs/week-04-extraction-plan.md` with:
- Candidate list with scores
- Extraction order (numbered 1-5+)
- Any "too far" candidates you decided NOT to extract

**Why This Matters:**
- Systematic refactoring (not random)
- Understand trade-offs (extraction has costs)
- Avoid compiler errors (extraction order)
- Build habit of planning before coding

---

## 12. Example Refactor Comparison (NEW - Post-Refactoring)

**Goal:** Self-assess your refactoring quality

**Time:** 10 minutes

**Instructions:** After completing your refactor, compare with example

### Step 1: Review Example

See: `Course-Materials/Examples/ReportsControllerRefactored.cs`

**This is ONE possible solution.** Yours may differ!

### Step 2: Comparison Checklist

| Aspect | Your Refactor | Example | Notes |
|--------|--------------|---------|-------|
| Number of extracted methods | ___ | 6 | 5-7 is good range |
| Main method line count | ___ | ~12 | Should be ≤15 |
| Method names descriptive? | Yes/No | Yes | Compare naming styles |
| Follows Stepdown Rule? | Yes/No | Yes | Read top-to-bottom flow |
| Parameter counts reasonable? | ___ avg | 1-3 avg | >3 params might need refactor |

### Step 3: Analysis Questions

**1. Method Count:**
- Did you extract more or fewer methods than example?
- If different, why? (Different granularity? Different abstraction choices?)

**2. Naming Comparison:**
Pick one method where your name differs from example:
- Your name: _____________
- Example name: _____________
- Which is clearer to a new developer? Why?

**3. Abstraction Levels:**
- Does your main method mix high/low level operations? (Should all be high-level)
- Example: `AssembleSummary(...)` is high-level ✓, `tasks.Where(...)` is low-level ❌

**4. Parameter Patterns:**
- Did you pass fewer parameters by grouping data?
- Example: Passing `tasks` + `projectId` separately, or creating `ProjectContext` class?

### Step 4: "Better Than Example" Justification

If your solution differs, answer:

**My refactor is better because:**
- ___________

**OR**

**Example is better because:**
- ___________

**Key Insight:** There's NO single "right" refactor. Evaluate based on:
- Readability (can new dev understand?)
- Testability (can pieces be tested alone?)
- Maintainability (easy to change one piece?)

### Discussion Prompt

Bring to discussion:
- One extraction choice you're proud of (and why)
- One extraction you're uncertain about (get feedback)
- How your approach differed from example (neither wrong, just different!)

**Deliverable:** Add comparison notes to your PR description

---

## 13. Additional Resources

- **[Function Refactor Example](../Examples/FunctionRefactor.cs)**
- **[Google Java Style Guide: Function Names](https://google.github.io/styleguide/javaguide.html#s5.2.3-method-names)**
- **[PEP 8: Style Guide for Python Code - Function Names](https://www.python.org/dev/peps/pep-0008/#function-and-variable-names)**
- **[C# Coding Conventions: Function Names](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#naming-conventions)**
- **[Clean Code Functions](https://dipta007.com/clean-code-functions/)**
- **[The Art of Writing Small and Plain Functions](https://dmitripavlutin.com/the-art-of-writing-small-and-plain-functions/)**
- **[A Good Function-Writing Process](https://education.launchcode.org/intro-to-professional-web-dev/chapters/functions/process.html)**
- **[Youtube: The Ultimate Guide to Writing Functions](https://www.youtube.com/watch?v=yatgY4NpZXE)**
