# Week 18: Code Smells & Refactoring

This week, we will focus on refactoring techniques and how to identify code smells, connecting these concepts to In Time Tec's commitment to continuous improvement and the Quality Manifesto's emphasis on technical excellence.

## 1. Learning Objectives

- Understand the concept of code smells and heuristics.
- Identify code smells in your project.
- Apply refactoring techniques to address code smells.
- Identify common smells (long method, duplicate code, shotgun surgery, etc.).
- Apply targeted refactorings without changing behavior.
- Document before/after impact for peer review.

## 2. Reading & Resources (80 min)

- **Clean Code Chapter 17: Smells and Heuristics (pp. 285-333)** (60 min) - Comprehensive catalog of code smells and refactoring techniques.
- **[Refactoring Guru: Code Smells](https://refactoring.guru/refactoring/smells)** (20 min) - Catalog of common smells and refactors.

## 3. This Week's Work

- Read the Code Smells Catalog (`docs/code-smells-catalog.md`) to understand intentional smells in the codebase.
- Find at least **five** distinct smells in `TaskFlowAPI` using the catalog as your guide.
- At least 3 of your 5 smells must come from the catalog (marked with `// CODE SMELL:` comments).
- Refactor each smell using appropriate technique (extract method/class, replace conditional, parameter object, etc.).
- Document each change in PR description with "smell → refactor → result".

## 4. Files to Modify

- Any production or test file containing the smell.
- Update documentation or `TODO` comments if necessary.
- This file (`Course-Materials/Weekly-Modules/week-18-code-smells-refactoring.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-18-submission`.
2. Read `docs/code-smells-catalog.md` to understand the intentional smells added to the codebase (15 min).
3. Search for `// CODE SMELL:` comments in the codebase:
   ```bash
   grep -r "CODE SMELL" TaskFlowAPI/
   ```
   Or use your IDE's Find in Files feature (Ctrl+Shift+F).
4. Choose 5 smells to fix:
   - At least 3 must be from the catalog (marked with comments).
   - 2 can be smells you discover yourself.
   - Mix of easy, medium, and hard difficulty (see catalog for difficulty guide).
5. For each smell:
   - Read the catalog entry to understand the smell and suggested refactoring.
   - Capture snippet before change (paste into PR description later).
   - Refactor carefully, running tests after each change.
   - Ensure naming/structure aligns with earlier clean-code practices.
   - Remove the `// CODE SMELL:` comment after fixing.
   - Commit after each successful fix: `git commit -m "refactor: remove magic numbers from ReportsController"`.
6. Optional: Add regression tests if refactor needed better coverage.
7. Run `dotnet build TaskFlowAPI.sln` and `dotnet test TaskFlowAPI.sln` after EACH refactor (not just at end).
8. Create PR with table documenting each smell fix (see Section 11 for example).

## 6. How to Test

```bash
# Build after each refactor
dotnet build TaskFlowAPI.sln

# Run tests after each refactor
dotnet test TaskFlowAPI.sln

# Optional: Run specific test file
dotnet test --filter TaskService
```

**Testing Strategy:**
- Run tests after EACH smell fix (not just at the end).
- If tests fail, revert and try a smaller refactor.
- Use git commits to checkpoint each successful fix.

## 7. Success Criteria

- At least **five** smells removed (at least 3 from catalog, 2 can be self-discovered).
- Each smell clearly described in PR with:
  - Smell name and location.
  - Refactoring technique applied.
  - Before/after code snippet.
  - Outcome (tests pass, code cleaner, etc.).
- Behavior unchanged (tests pass after each refactor).
- No new smells introduced (e.g., giant helpers).
- All `// CODE SMELL:` comments removed from fixed code.
- Week 18 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-18-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 18 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Smell Catalog: Log the five smells you targeted and the refactoring technique applied to each one.*
- *Regression Safeguards: Which tests or checks gave you confidence that behavior stayed the same after your refactorings?*
- *Unexpected Challenge: Which smell took longer than expected to fix, and why?*

### Discussion Prep:

- *Which smell surprised you most when you found it?*
- *How did you ensure behavior stayed the same after each refactor?*
- *What tooling (IDE features, grep, tests) helped you locate and verify smells?*
- *Where do you still see opportunities for future refactorings?*

## 10. Time Estimate

- 80 min — Reading (Clean Code Ch 17 + Refactoring Guru).
- 15 min — Read Code Smells Catalog.
- 15 min — Identify smells (search for comments).
- 75 min — Refactor 5 smells (approx. 15 min each, mix of easy/medium).
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~3 hours 20 minutes.

## 11. PR Documentation Template

### Create This Table in Your PR Description

| Smell | Location | Refactoring Applied | Outcome |
|-------|----------|---------------------|---------|
| Magic Numbers | ReportsController.cs:85 | Extracted `PercentageMultiplier = 100` constant | Code more readable, tests pass |
| Long Method | ReportsController.cs:24 | Extracted 4 helper methods | Method reduced from 95 to 15 lines |
| Dead Code | TaskHelper.cs:160 | Deleted unused `OldFormatTitle()` method | Codebase cleaner, tests pass |
| Duplicate Code | TaskService.cs:50, 120 | Extracted `ValidateTaskEntity()` method | DRY principle restored |
| Feature Envy | TaskMapper.cs:35 | Moved logic to `TaskEntity` class | Better encapsulation |

### Include Before/After Snippets

**Example:**

```csharp
// BEFORE (Magic Numbers):
var percentage = (completedCount / totalCount) * 100;

// AFTER:
private const int PercentageMultiplier = 100;
var percentage = (completedCount / totalCount) * PercentageMultiplier;
```

## 12. Smell Difficulty Guide

Refer to `docs/code-smells-catalog.md` for the complete catalog. Here's a quick difficulty reference:

### Easy (5-20 minutes each)
- Dead Code.
- Bad Comments.
- Magic Numbers.
- Lazy Class.
- Speculative Generality.

### Medium (20-45 minutes each)
- Long Method.
- Duplicate Code.
- Feature Envy.
- Data Clumps.
- Long Parameter List.
- Large Class.

### Hard (45-90 minutes each)
- Primitive Obsession (advanced refactoring).

### How to Find Smells

**Method 1: Search for Comments**
```bash
grep -r "CODE SMELL" TaskFlowAPI/
```

**Method 2: IDE Search**
- Use Find in Files (Ctrl+Shift+F or Cmd+Shift+F).
- Search for `// CODE SMELL`.

**Method 3: Read the Catalog**
- Open `docs/code-smells-catalog.md`.
- Each smell has location, description, and suggested fix.

## 13. Safe Refactoring Strategy

### Before You Start
1. Ensure all tests pass: `dotnet test TaskFlowAPI.sln`.
2. Commit clean baseline: `git commit -m "chore: baseline before refactoring"`.
3. Read the smell's catalog entry to understand why it's a smell and suggested fix.

### During Refactoring
1. Change one thing at a time.
2. Test after each step: `dotnet test`.
3. Commit after each successful refactor.
4. If tests fail: `git reset --hard` and try smaller step.

### Git Strategy

```bash
# Start with clean baseline
git status
git commit -m "chore: baseline before Week 18 refactoring"

# Create branch
git checkout -b week-18-submission

# Fix one smell
# ... make changes ...
dotnet test
git add .
git commit -m "refactor: remove magic numbers from ReportsController"

# Fix another smell
# ... make changes ...
dotnet test
git add .
git commit -m "refactor: extract method to reduce ReportsController complexity"

# If something goes wrong
git log --oneline
git reset --hard <last-good-commit>
```

## 14. Additional Resources

### Examples

- **[Code Smells and Heuristics Example](../Examples/CodeSmellsAndHeuristics.md)** - Review for patterns.
- **[Code Smells Catalog](../../docs/code-smells-catalog.md)** - Complete reference of intentional smells in TaskFlowAPI.

### External Resources

- **[Refactoring Guru](https://refactoring.guru/)** - Comprehensive refactoring patterns.
- **[Martin Fowler's Refactoring Catalog](https://refactoring.com/catalog/)** - Classic refactoring reference.

### Optional Deep Dives

- **[YouTube: Why Code with Code Smells is Harder to Understand](https://www.youtube.com/watch?v=gkjbWKp4VgM)** (10 min) - Visual explanation.
- **[YouTube: Code Refactoring: Learn Code Smells And Level Up Your Game!](https://www.youtube.com/watch?v=D4auWwMsEnY)** (12 min) - Practical examples.
- **[Working Effectively with Legacy Code](https://www.oreilly.com/library/view/working-effectively-with/0131177052/)** - Book on refactoring without tests.