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
- Any other files impacted by renaming (hint: use 'find all references' or use compiler errors as your guide).
- This file (`Course-Materials/Weekly-Modules/week-02-meaningful-names.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-02/<your-name>`.
2. Read through both files and list every abbreviation or vague name.
3. Refactor constructor parameters, private fields, method names, and local variables.
4. Update XML comments and `nameof` references to match new names.
5. Run `dotnet build TaskFlowAPI.sln` to surface missed references.
6. Stage your changes and inspect diff for any missed renames.

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

1. Commit with message `Week 02 – naming refactor`.
2. Open PR using the template, attach build/test output.
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
- 30 min – Refactoring and compile checks.
- 20 min – Journalling + discussion prep.
- 10 min – Review + PR/issue.
**Total:** ~1 hour 30 minutes.

## 11. Additional Resources

- **[Meaningful Names Example](../Examples/MeaningfulNames.md)**
- **[Clean Code: A Handbook of Agile Software Craftsmanship](https://www.amazon.com/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882)**
- **[The Art of Naming: Best Practices for Meaningful Names](https://medium.com/@kentcdodds/the-art-of-naming-best-practices-for-meaningful-names-7c8f6d8f7d5)**
- **[Google Java Style Guide: Naming](https://google.github.io/styleguide/javaguide.html#s5.2-specific-identifier-names)**
- **[PEP 8: Style Guide for Python Code - Naming Conventions](https://www.python.org/dev/peps/pep-0008/#naming-conventions)**
- **[C# Coding Conventions: Naming Guidelines](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#naming-conventions)**