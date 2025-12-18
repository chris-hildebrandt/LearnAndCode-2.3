# Week 13: Liskov Substitution Principle (LSP)

This week is about a real-world problem junior developers hit all the time:

> "My tests pass with a fake… but production behaves differently."

That's an LSP problem. If two implementations share an interface, you should be able to **substitute** one for the other without breaking the calling code's expectations.

## 1. Learning Objectives

- Understand LSP as a **behavioral contract**, not an inheritance debate.
- Ensure repository implementations can be substituted without breaking consumers.
- Create contract tests that verify all implementations honor the same rules.
- Build an in-memory fake repository that behaves identically (from a caller's perspective) to the real repository.

## 2. Reading & Resources (45 min)

- **[LSP by Barbara Liskov - Wikipedia](https://en.wikipedia.org/wiki/Liskov_substitution_principle)** — Original definition and the famous Rectangle/Square problem.
- **[Understanding LSP - Dev.to](https://dev.to/extinctsion/solid-the-liskov-substitution-principle-lsp-in-c-1hl9)** — Practical C# examples showing violations and fixes.
- **[LSP in Practice - ITU Online](https://www.ituonline.com/tech-definitions/what-is-the-liskov-substitution-principle-lsp/)** — Real-world scenarios and anti-patterns.

**What to pay attention to while reading:**
- The Rectangle/Square problem is a useful warning sign, but it's not the assignment.
- The real lesson is this: **if callers have expectations, all implementations must meet them.**

## 3. This Week's Work

You will make sure two implementations of `ITaskRepository` can be swapped without changing program correctness:
- `TaskRepository` (real EF Core repository).
- `FakeTaskRepository` (in-memory fake used in tests).

If the same tests don't pass for both, your fake is lying to you — and LSP is broken.

## 4. Files to Modify

- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (add contract documentation).
- `TaskFlowAPI/Repositories/TaskRepository.cs` (ensure behavior matches the documented contract).
- `TaskFlowAPI.Tests/Unit/FakeTaskRepository.cs` (implement — scaffolded with TODOs).
- `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs` (add tests — scaffolded and skipped by default).
- This file (`Course-Materials/Weekly-Modules/week-13-liskov-substitution.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-13-submission`.

2. **Read about LSP first (45 min).**
   
   Pay special attention to:
   - What "behavioral contract" means (beyond the method signature).
   - How fakes can "lie" when they behave differently than production.
   - The difference between "returns null when missing" vs "throws when missing".

3. **Document behavioral contracts (10–15 min).**
   
   Open `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` and add explicit contracts to each method's XML documentation.
   
   **Example for `GetByIdAsync`:**
   
   ```csharp
   /// <summary>
   /// Retrieves a task by its unique ID.
   /// <para><strong>Contract:</strong> Returns null when the task does not exist. Does not throw a "not found" exception.</para>
   /// </summary>
   Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
   ```
   
   **Add (or confirm) contracts for:**
   - `GetByIdAsync` — returns null when not found (never throws "not found").
   - `CreateAsync` — returns entity with generated `Id > 0`.
   - `DeleteAsync(TaskEntity entity)` — idempotent; if the entity isn't present, return without throwing.
   - `GetAllAsync` — returns empty list when there are no tasks (never null).

4. **Create / implement `FakeTaskRepository` (25–35 min).**
   
   Open `TaskFlowAPI.Tests/Unit/FakeTaskRepository.cs` (already scaffolded with TODOs).
   
   **Your job:** Implement an in-memory repository that honors the SAME contracts as `TaskRepository`.
   
   **Key requirements:**
   - Use `List<TaskEntity>` to store tasks in memory.
   - `GetByIdAsync` returns null when not found (do not throw).
   - `CreateAsync` generates a new `Id` (> 0) and returns the created entity.
   - `DeleteAsync(TaskEntity)` does not throw if the entity isn't present (idempotent).
   - `GetAllAsync` returns an empty list when there are no tasks.
   
   **Complete all `TODO Week 13:` items in that file.**

5. **Study the contract test pattern (5–10 min).**
   
   Open `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs`.
   
   Notice that each test runs against **both** repository kinds with the **same assertions**.
   There should be no `if (repoKind == Fake)` logic in the asserts.

6. **Un-skip and run contract tests (5–10 min).**
   
   In `TaskRepositoryContractTests`, remove the `Skip = ...` part of the `[Theory]` attributes.
   
   Then run:
   
   ```bash
   dotnet test TaskFlowAPI.sln --filter TaskRepositoryContractTests
   ```
   
   **Expected on first run:** One or more tests fail because `FakeTaskRepository` is incomplete.

7. **Fix behavioral discrepancies until all contract tests pass (15–30 min).**
   
   For each failing test:
   1. Identify which implementation violated the contract (fake, real, or the contract itself).
   2. Check the contract in `ITaskRepository`.
   3. Fix the behavior (usually the fake).
   4. Re-run the tests.
   
   **Common fixes:**
   - Fake throws exception when real returns null → remove the throw, return null.
   - Fake returns `Id = 0` after create → generate an Id and return the entity.
   - Fake delete throws when missing → make delete idempotent (no throw).

8. **Stretch goal (optional, 10–15 min): ordering contract**
   
   If you want an extra challenge, add a contract test for `GetAllAsync` ordering (Priority → DueDate) and ensure both implementations match it.

## 6. How to Test

```bash
dotnet test TaskFlowAPI.sln --filter TaskRepositoryContractTests
```

## 7. Success Criteria

- Contract XML docs exist on `ITaskRepository` methods and are specific (not vague).
- `FakeTaskRepository` honors the same behavioral contract as `TaskRepository`.
- The same contract tests pass for both implementations (no conditional assertions).
- Build and tests succeed.
- Week 13 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

1. Create a new branch for your weekly work (e.g., `git checkout -b week-13-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "test: add repository contract tests"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Contract Definition: Write one sentence describing the most important repository contract you made explicit.*
- *Fake vs Real: What was the first behavior mismatch you found between fake and real?*
- *Customer Impact: How can a "lying fake" lead to production bugs that customers experience?*

### Discussion Prep:

- *Where else in TaskFlow could LSP break? (Think: service interfaces, filters, validators.)*
- *What makes a contract test "high value" vs "busy work"?*

## 10. Time Estimate

- 45 min — Reading.
- 60–75 min — Implement fake, contract tests, and fix mismatches.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 20 minutes.

## 11. Additional Resources

### Examples

- **[Liskov Substitution Example](../Examples/LiskovSubstitution.cs)** — Additional code examples (optional).

### External Resources

- **[Rectangle-Square Problem](https://en.wikipedia.org/wiki/Circle-ellipse_problem)** — The classic LSP violation explained (covered in your reading).
- **[Design by Contract](https://en.wikipedia.org/wiki/Design_by_contract)** — Formal basis for LSP and behavioral contracts.

### Optional Deep Dives

- **[Contract Testing in Practice](https://martinfowler.com/bliki/ContractTest.html)** — Martin Fowler on testing interface contracts.