# Week 18: Code Smells & Refactoring

This week, we will focus on refactoring techniques and how to identify code smells, connecting these concepts to In Time Tec's commitment to continuous improvement and the Quality Manifesto's emphasis on technical excellence.

## 1. Learning Objectives

- Understand the concept of code smells and heuristics.
- Identify code smells in your project.
- Apply refactoring techniques to address code smells.
- Discuss the benefits of addressing code smells in the context of continuous improvement and technical excellence.
- Identify common smells (long method, duplicate code, shotgun surgery, etc.).
- Apply targeted refactorings without changing behaviour.
- Document before/after impact for peer review.

## 2. Reading & Resources (80 min)

- **Clean Code Chapter 17: Smells and Heuristics**.
- **[Refactoring Guru: Code Smells](https://refactoring.guru/refactoring/smells)** – Catalogue of common smells and refactors.
- **[Clean Code Smells And Heuristics](https://medium.com/@mut_e/clean-code-smells-and-heuristics-9080d9ab67c1)** – Quick reference checklist.
- **[YouTube: Why Code with Code Smells is Harder to Understand](https://www.youtube.com/watch?v=gkjbWKp4VgM)**
- **[YouTube: Code Refactoring: Learn Code Smells And Level Up Your Game!](https://www.youtube.com/watch?v=D4auWwMsEnY)**

## 3. This Week’s Work

**REVISED: Code Smell Scavenger Hunt**

- **Step 0:** Read the Code Smells Catalog (Section 11 below) - 15 min
- Find at least **five** distinct smells in `TaskFlowAPI` using the catalog as your guide.
- **NEW:** At least 3 of your 5 smells must come from the catalog (marked with `// CODE SMELL:` comments).
- Refactor each smell using appropriate technique (extract method/class, replace conditional, parameter object, etc.).
- Document each change in PR description with “smell → refactor → result”.

## 4. Files to Modify

- Any production or test file containing the smell.
- Update documentation or `TODO` comments if necessary.
- This file (`Course-Materials/Weekly-Modules/week-18-code-smells-refactoring.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-18/<your-name>`.
2. **NEW:** Read `docs/code-smells-catalog.md` to understand the intentional smells added to the codebase.
3. **NEW:** Search for `// CODE SMELL:` comments in the codebase:
   ```bash
   grep -r "CODE SMELL" TaskFlowAPI/
   ```
   Or use your IDE's Find in Files feature.
4. **NEW:** Choose 5 smells to fix:
   - At least 3 must be from the catalog (marked with comments)
   - 2 can be smells you discover yourself
   - Mix of easy, medium, and hard difficulty
5. For each smell:
   - Read the catalog entry to understand the smell and suggested refactoring
   - Capture snippet before change (paste into PR description later)
   - Refactor carefully, running tests after each change
   - Ensure naming/structure aligns with earlier clean-code practices
   - Remove the `// CODE SMELL:` comment after fixing
6. Optional: add regression tests if refactor needed better coverage.
7. Run full build/tests at the end.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- At least **five** smells removed (at least 3 from catalog, 2 can be self-discovered).
- Each smell clearly described in PR with:
  - Smell name and location
  - Refactoring technique applied
  - Before/after code snippet
  - Outcome (tests pass, code cleaner, etc.)
- Behaviour unchanged (tests pass).
- No new smells introduced (e.g., giant helpers).
- All `// CODE SMELL:` comments removed from fixed code.

## 8. Submission Process

- Commit `Week 18 – smell cleanup` (use multiple commits if helpful, e.g., one per smell).
- PR summary includes table:
  | Smell | Location | Refactoring | Outcome |
- Weekly issue references same table + lessons learned.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Smell Catalog:* Log the five smells you targeted and the refactor applied.
- *Regression Safeguards:* Note which tests or checks gave you confidence behaviour stayed the same.

### Discussion Prep:
- *Which smell surprised you most?*
- *How did you ensure behaviour stayed the same?*
- *What tooling helped you locate smells?*
- *Where do you still see opportunities for future refactors?*

## 10. Time Estimate

- 80 min – Reading (Clean Code Ch 17).
- 15 min – Read Code Smells Catalog (Section 11).
- 15 min – Identify smells (search for `// CODE SMELL:` comments).
- 75 min – Refactor (approx. 5x ~15 min each, mix of easy/medium).
- 15 min – Tests + documentation.
**Total:** ~200 minutes (3.3 hours).

## 11. Code Smell Scavenger Hunt (NEW)

**Goal:** Find and fix intentional code smells added to the codebase

**Time:** 15 minutes to read catalog, then 2-3 hours to fix 5 smells

### The Catalog

A comprehensive catalog of 24+ intentional code smells has been added to the `TaskFlowAPI` codebase. Each smell is clearly marked with a comment:

```csharp
// CODE SMELL: [Smell Name] (Clean Code Ch 17, p. [page])
// [Brief explanation of why this is a smell]
// [What principle it violates]
// Refactor by: [Suggested refactoring approach]
```

### How to Find Smells

**Method 1: Search for Comments**
```bash
# Search for CODE SMELL comments
grep -r "CODE SMELL" TaskFlowAPI/
```

**Method 2: IDE Search**
- In Visual Studio/VS Code: Search for `// CODE SMELL`
- Use Find in Files (Ctrl+Shift+F)

**Method 3: Read the Catalog**
- Open `docs/code-smells-catalog.md`
- Each smell has a location, description, and suggested fix

### Smell Difficulty Levels

**Easy (5-20 minutes each):**
- Dead Code
- Bad Comments
- Magic Numbers
- Lazy Class
- Speculative Generality

**Medium (20-45 minutes each):**
- Long Method
- Duplicate Code
- Feature Envy
- Data Clumps
- Long Parameter List
- Large Class

**Hard (45-90 minutes each):**
- Primitive Obsession (advanced refactoring)

### Your Assignment

1. **Read the catalog:** `docs/code-smells-catalog.md`
2. **Find 5 smells:**
   - At least 3 from the catalog (marked with comments)
   - 2 can be smells you discover yourself
   - Mix of easy, medium, and hard
3. **Fix each smell:**
   - Follow the suggested refactoring in the catalog
   - Run tests after each fix
   - Remove the `// CODE SMELL:` comment
4. **Document in PR:**
   - Create a table with: Smell | Location | Refactoring | Outcome
   - Include before/after code snippets

### Example PR Entry

| Smell | Location | Refactoring | Outcome |
|-------|----------|-------------|---------|
| Magic Numbers | ReportsController.cs:85 | Extracted `PercentageMultiplier = 100` constant | Code more readable, tests pass |
| Long Method | ReportsController.cs:24 | Extracted 4 helper methods | Method reduced from 95 to 15 lines |
| Dead Code | TaskHelper.cs:160 | Deleted unused `OldFormatTitle()` method | Codebase cleaner |

### Success Checklist

- [ ] Read `docs/code-smells-catalog.md`
- [ ] Found 5 smells (3+ from catalog)
- [ ] Fixed all 5 smells
- [ ] All tests pass
- [ ] Removed `// CODE SMELL:` comments
- [ ] Documented in PR with table
- [ ] No new smells introduced

---

## 12. Additional Resources

- **[Code Smells and Heuristics Example](../Examples/CodeSmellsAndHeuristics.md)**
- **[Code Smells Catalog](../../docs/code-smells-catalog.md)** - Complete reference of intentional smells in TaskFlowAPI
