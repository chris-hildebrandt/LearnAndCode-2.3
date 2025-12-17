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

## 3. This Week's Work

- Refactor every bad name in `TasksController` and `ITaskService`.
- Update docstring comments where necessary to reflect new names.
- Ensure all new names are consistent across files.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- Any other files impacted by renaming (hint: use 'find all references' or use compiler errors as your guide).
- This file (`Course-Materials/Weekly-Modules/week-02-meaningful-names.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-02-submission`.
2. Refactor `ITaskService` interface FIRST (note compilation errors - this is learning!).
3. Refactor `TaskService` implementation to match interface.
4. Refactor `TasksController` to use new names.
5. Update XML comments and nameof references to match new names.**Pro Tip:** Do not manually backspace and retype variable names\! Use the **Rename Symbol** feature. In Codespaces, simply right-click the name and select **Rename Symbol** (or press F2). This ensures all references, comments, and nameof calls are updated safely in one click.  
6. Run dotnet build TaskFlowAPI.sln to catch any missed references.  

7. Stage your changes and inspect diff for any missed renames.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- No abbreviations remain (`svc`, `s`, `t`, `dt`, `req`, etc.).
- Method names are verb phrases (`GetAllTasksAsync`, `CreateTaskAsync`, etc.).
- Controller action names align with HTTP verbs.
- Build and tests succeed without warnings.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-02-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 2 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

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
- *How did you verify you didn't miss any references?*
- *Please identify one problematic naming convention 'in the wild' (from a real-world or personal project) to share during the discussion.*

## 10. Time Estimate

- 55 min – Reading.
- 35 min – Refactoring and compile checks.
- 20 min – Journal and discussion prep.
- 15 min – Test, create PR, and merge.

**Total:** ~2 hours 5 minutes.

## 11. Additional Resources

### External Resources

- **[Meaningful Names Example](../Examples/MeaningfulNames.md)**
- **[Clean Code: A Handbook of Agile Software Craftsmanship](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)**
- **[The Art of Naming: Best Practices for Meaningful Names](https://medium.com/@kentcdodds/the-art-of-naming-best-practices-for-meaningful-names-7c8f6d8f7d5)**
- **[Google Java Style Guide: Naming](https://google.github.io/styleguide/javaguide.html#s5.2-specific-identifier-names)**
- **[PEP 8: Style Guide for Python Code - Naming Conventions](https://www.python.org/dev/peps/pep-0008/#naming-conventions)**
- **[C# Coding Conventions: Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#naming-conventions)**

### Optional Practice

#### Part A: Bad Name Categorization

| Bad Name | File | Category | Why It's Bad | Impact Score (1-5) |
|----------|------|----------|--------------|-------------------|
| svc | TasksController | Abbreviation | Mental mapping required | 4 |
| t | TasksController | Single letter | Could be anything | 5 |
| dt | TasksController | Abbreviation | Context-dependent | 4 |
| Get() | ITaskService | Too generic | Doesn't reveal what | 3 |
| s | TaskService | Single letter | Parameter of what? | 5 |
| (add more from your findings) | | | | |

**Categories:**
- Abbreviation.
- Single letter.
- Too generic.
- Noise word (Manager, Helper, Handler).
- Misleading.
- Inconsistent with domain.

**Impact Score:**
- 5 = Blocks new developer completely.
- 3 = Causes confusion, wastes time.
- 1 = Minor annoyance.

#### Part B: Controversial Naming Decisions (Critical Thinking)

For these scenarios, select an option or propose an alternative:

**Scenario 1:** Rename `TaskDto`
- Option A: `TaskResponse` (emphasizes it's API output).
- Option B: `TaskDto` (keeps DTO suffix, standard).
- Option C: `TaskViewModel` (emphasizes presentation layer).
- **Your Choice?** ___________

**Scenario 2:** Rename parameter `request` in `Add(CreateTaskRequest request)`  
- Option A: Keep `request` (obvious from type).
- Option B: `createTaskRequest` (fully descriptive).
- Option C: `taskRequest` (balance).
- **Your Choice?** ___________

**Scenario 3:** Rename `Get(int id)` - what verb?
- Option A: `GetTaskByIdAsync` (fully explicit).
- Option B: `GetByIdAsync` (Task implied by context).
- Option C: `FindAsync` (different semantic meaning - implies might not find).
- **Your Choice?** ___________

**Scenario 4:** Method that returns multiple tasks
- Option A: `GetTasks` (simple, plural).
- Option B: `GetAllTasks` (explicit - all tasks).
- Option C: `GetTaskList` (emphasizes return type).
- Option D: `RetrieveTasks` (different verb).
- **Your Choice?** ___________

**Why This Matters:**
- Forces you to analyze, not just execute.
- Introduces gray areas (critical thinking vs. binary right/wrong).
- Teaches that naming is a design decision, not mechanical task.