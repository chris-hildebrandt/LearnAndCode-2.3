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

- Find at least **three** distinct smells in `TaskFlowAPI` (code or tests).
- Refactor each smell using appropriate technique (extract method/class, replace conditional, parameter object, etc.).
- Document each change in PR description with “smell → refactor → result”.

## 4. Files to Modify

- Any production or test file containing the smell.
- Update documentation or `TODO` comments if necessary.
- This file (`Course-Materials/Weekly-Modules/week-18-code-smells-refactoring.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-18/<your-name>`.
2. Scan recent code (controllers, services, filters, validators, tests) for smells.
3. For each smell:
   - Capture snippet before change (paste into PR description later).
   - Refactor carefully, running tests after each change.
   - Ensure naming/structure aligns with earlier clean-code practices.
4. Optional: add regression tests if refactor needed better coverage.
5. Run full build/tests at the end.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- At least three smells removed; each clearly described in PR.
- Behaviour unchanged (tests pass).
- No new smells introduced (e.g., giant helpers).

## 8. Submission Process

- Commit `Week 18 – smell cleanup` (use multiple commits if helpful, e.g., one per smell).
- PR summary includes table:
  | Smell | Location | Refactoring | Outcome |
- Weekly issue references same table + lessons learned.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Smell Catalog:* Log the three smells you targeted and the refactor applied.
- *Regression Safeguards:* Note which tests or checks gave you confidence behaviour stayed the same.

### Discussion Prep:
- *Which smell surprised you most?*
- *How did you ensure behaviour stayed the same?*
- *What tooling helped you locate smells?*
- *Where do you still see opportunities for future refactors?*

## 10. Time Estimate

- 100 min – Reading.
- 10 min – Identify smells.
- 40 min – Refactor (approx. 3x ~13 min each).
- 10 min – Tests + documentation.
**Total:** ~160 minutes.

## 11. Additional Resources

- **[Code Smells and Heuristics Example](../Examples/CodeSmellsAndHeuristics.md)**
