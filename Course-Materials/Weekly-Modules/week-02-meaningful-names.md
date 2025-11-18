# Week 2: Meaningful Names

This week, we are focusing on the importance of meaningful names in code, and how they contribute to clean code principles. We will explore best practices for naming variables, functions, classes, and other code elements to improve readability and maintainability. This module is based on **Clean Code Chapter 2: Meaningful Names**.

## 1. Learning Objectives

- Refactor ambiguous names so code documents itself without comments.
- Apply best practices for naming variables, functions, classes, and other code elements.
- Discuss the impact of meaningful names on code quality and maintainability.
- Practice batch renaming with Git-friendly commits.

## 2. Reading & Resources (30 min)

- **Clean Code - Chapter 2: Meaningful Names (pp. 17-34)**.
  - Key takeaways: reveal intent, avoid encodings/abbreviations, use pronounceable names.
  - Summary: Good names reduce the need for comments and make defects obvious. Every rename is a design decision.

## 3. This Week’s Work

- Refactor every bad name in `TasksController` and `ITaskService`.
- Update docstring comments where necessary to reflect new names.
- Ensure all new names are consistent across files.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- **NEW:** `TaskFlowAPI/Services/Tasks/TaskService.cs`
- Any other files impacted by renaming (hint: use 'find all references' or use compiler errors as your guide).
- `docs/week-02-naming-analysis.md` (NEW - pre-refactoring analysis)
- This file (`Course-Materials/Weekly-Modules/week-02-meaningful-names.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-02/<your-name>`.
2. **NEW:** Complete Name Analysis Worksheet (see Section 11 below) - 15 minutes
3. **NEW:** Refactor `ITaskService` interface FIRST (note compilation errors - this is learning!)
4. **NEW:** Refactor `TaskService` implementation to match interface
5. Refactor `TasksController` to use new names
6. **NEW:** Create "Rename Impact Map" showing cascading changes
7. Update XML comments and `nameof` references to match new names.
8. Run `dotnet build TaskFlowAPI.sln` to catch any missed references.
9. **NEW:** Complete Name Quality Metrics (see Section 12 below)
10. Stage your changes and inspect diff for any missed renames.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- No abbreviations remain (`svc`, `s`, `t`, `dt`, `req`, etc.).
- Method names are verb phrases (`GetAllTasksAsync`, `CreateTaskAsync`, etc.).
- Controller action names align with HTTP verbs.
- **NEW:** "Rename Impact Map" documented showing at least ONE cascading rename
- **NEW:** Name Quality Metrics table completed showing before/after improvement
- Build and tests succeed without warnings.

## 8. Submission Process

1. Commit with message `Week 02 – naming refactor`.
2. Open PR using the template, **include Rename Impact Map and metrics table** in PR description.
3. Submit weekly issue using the Week Submission template.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Customer-Centric Design: How does a vague name like `svc` or `dt` in a critical service method ultimately increase customer risk (e.g., through delayed bug fixes or new feature implementation)?*
- *Code Review/Collaboration: Which type of bad name (abbreviations, noise words, or mental mapping) is the most difficult for a new teammate to understand, and why does this slow team velocity?*
- *Single Responsibility: Can a bad class name (`Helper`, `Manager`) hide a Single Responsibility Principle (SRP) violation? Give an example from a real codebase or propose one.*
- *Performance vs. Clarity: If a shorter, vague name like `p1` compiles faster than `priorityOne`, why do we prioritize the reader's time over the computer's time, and how does this align with professionalism?*

### Discussion Prep:
- *Where did poor naming hide important behavior?*
- *Which rename felt controversial and why?*
- *How did you verify you didn’t miss any references?*
- *Please identify one problematic naming convention 'in the wild' (from a real-world or personal project) to share during the discussion.*
- *Share examples of how meaningful names improved your code readability and maintainability.*
- *Discuss the most effective naming conventions you applied and explain your reasoning.*
- *Compare different naming conventions and their impact on code quality.*
- *Explore the challenges of naming in different programming languages and contexts.*
- *Brainstorm strategies for maintaining consistent naming conventions in your team's codebase.*

## 10. Time Estimate

- 30 min – Reading.
- 15 min – Name Analysis Worksheet (NEW)
- 40 min – Refactoring and compile checks (extended for cascading changes)
- 10 min – Name Quality Metrics (NEW)
- 20 min – Journalling + discussion prep.
- 10 min – Review + PR/issue.
**Total:** ~2 hours 5 minutes.

## 11. Name Analysis Worksheet (NEW - Pre-Refactoring)

**Goal:** Analyze names BEFORE refactoring to build decision-making skills

**Time:** 15 minutes

**Instructions:** Create `docs/week-02-naming-analysis.md` in your fork

### Part A: Bad Name Categorization

Review your Week 1 inventory. Categorize each bad name found in TasksController and ITaskService:

| Bad Name | File | Category | Why It's Bad | Impact Score (1-5) |
|----------|------|----------|--------------|-------------------|
| svc | TasksController | Abbreviation | Mental mapping required | 4 |
| t | TasksController | Single letter | Could be anything | 5 |
| dt | TasksController | Abbreviation | Context-dependent | 4 |
| Get() | ITaskService | Too generic | Doesn't reveal what | 3 |
| s | TaskService | Single letter | Parameter of what? | 5 |
| (add more from your findings) | | | | |

**Categories:**
- Abbreviation
- Single letter
- Too generic
- Noise word (Manager, Helper, Handler)
- Misleading
- Inconsistent with domain

**Impact Score:**
- 5 = Blocks new developer completely
- 3 = Causes confusion, wastes time
- 1 = Minor annoyance

### Part B: Controversial Naming Decisions (Critical Thinking)

For these scenarios, propose 2-3 alternatives and defend your choice:

**Scenario 1:** Rename `TaskDto`
- Option A: `TaskResponse` (emphasizes it's API output)
- Option B: `TaskDto` (keeps DTO suffix, standard)
- Option C: `TaskViewModel` (emphasizes presentation layer)
- **Your Choice + Justification (50 words):** ___________

**Scenario 2:** Rename parameter `request` in `Add(CreateTaskRequest request)`  
- Option A: Keep `request` (obvious from type)
- Option B: `createTaskRequest` (fully descriptive)
- Option C: `taskRequest` (balance)
- **Your Choice + Justification (50 words):** ___________

**Scenario 3:** Rename `Get(int id)` - what verb?
- Option A: `GetTaskByIdAsync` (fully explicit)
- Option B: `GetByIdAsync` (Task implied by context)
- Option C: `FindAsync` (different semantic meaning - implies might not find)
- **Your Choice + Justification (50 words):** ___________

**Scenario 4:** Method that returns multiple tasks
- Option A: `GetTasks` (simple, plural)
- Option B: `GetAllTasks` (explicit - all tasks)
- Option C: `GetTaskList` (emphasizes return type)
- Option D: `RetrieveTasks` (different verb)
- **Your Choice + Justification (50 words):** ___________

**Deliverable:** Commit `docs/week-02-naming-analysis.md` BEFORE starting refactor

**Why This Matters:**
- Forces you to analyze, not just execute
- Introduces gray areas (critical thinking vs. binary right/wrong)
- Creates paper trail showing thought process
- Teaches that naming is a design decision, not mechanical task

---

## 12. Name Quality Metrics (NEW - Post-Refactoring)

**Goal:** Quantify the improvement from your refactoring

**Time:** 10 minutes

**Instructions:** Before merging your PR, add this comparison to your PR description:

### Metrics Table

| Metric | Before | After | Improvement |
|--------|--------|-------|-------------|
| Total abbreviations | ___ | 0 | -___% |
| Single-letter vars (excluding loop iterators) | ___ | 0 | -100% |
| Avg method name length (characters) | ___ | ___ | +___ chars |
| Methods with verb prefixes | ___% | 100% | +___% |
| "Obvious without comment" names | ___% | 100% | +___% |

### Rename Impact Map

Document at least ONE cascading rename to show coupling between layers:

**Example:**
```
Renamed ITaskService.GetAll → GetAllTasksAsync
↓ (forced changes in)
├─ TaskService.GetAll implementation → GetAllTasksAsync
├─ TasksController.Get() call site → updated to GetAllTasksAsync
├─ XML documentation comments → updated
└─ Unit test method names → GetAll_Should... → GetAllTasksAsync_Should...
```

**Your Cascade:**
```
Renamed _____________ → _____________
↓ (forced changes in)
├─ _____________
├─ _____________
└─ _____________
```

**Insight:** Interfaces and implementations must stay synchronized. Compilation errors = learning opportunities!

### Test: Can a New Developer Understand?

Pick ONE method signature you renamed. Show it to a friend/classmate who hasn't seen the code.

**Before:** `Task<IActionResult> Get(int id, CancellationToken ct)`  
**After:** `Task<IActionResult> GetTaskByIdAsync(int id, CancellationToken cancellationToken)`

**Questions for them:**
1. What does this method do? 
   - Before answer: ___________
   - After answer: ___________
2. How confident are you? (1-5 scale)
   - Before: ___ / 5
   - After: ___ / 5

**Result:** Include their responses in PR description

### Quantified Impact

Based on your metrics, estimate:
- **Seconds saved per name** when a developer reads this code: ___ seconds
- **Number of bad names fixed:** ___
- **Total time saved for next teammate:** ___ seconds × ___ names = ___ minutes!

**Journal Question:** "Based on metrics, how many MINUTES did you just save the next developer who reads this code?"

**Deliverable:** Include completed metrics table and impact map in PR description

---

## 13. Additional Resources

- **[Meaningful Names Example](../Examples/MeaningfulNames.md)**
- **[Clean Code: A Handbook of Agile Software Craftsmanship](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)**
- **[The Art of Naming: Best Practices for Meaningful Names](https://medium.com/@kentcdodds/the-art-of-naming-best-practices-for-meaningful-names-7c8f6d8f7d5)**
- **[Google Java Style Guide: Naming](https://google.github.io/styleguide/javaguide.html#s5.2-specific-identifier-names)**
- **[PEP 8: Style Guide for Python Code - Naming Conventions](https://www.python.org/dev/peps/pep-0008/#naming-conventions)**
- **[C# Coding Conventions: Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#naming-conventions)**