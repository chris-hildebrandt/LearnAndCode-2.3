# Week 13: Liskov Substitution Principle (LSP)

This week, we are focusing on the Liskov Substitution Principle (LSP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the LSP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Understand the Liskov Substitution Principle and its importance in creating maintainable, extensible, and testable code.
- Ensure repository/service interfaces can be substituted without breaking consumers.
- Validate assumptions via contract-style unit tests using fakes.
- Tighten exception behaviour so all implementations honour the same rules.

## 2. Reading & Resources (45 min)

- **[LSP by Barbara Liskov - Wikipedia](https://en.wikipedia.org/wiki/Liskov_substitution_principle)** – Original definition and formal background.
- **[Understanding LSP - Dev.to](https://dev.to/extinctsion/solid-the-liskov-substitution-principle-lsp-in-c-1hl9)** – Practical C# examples.
- **[LSP in Practice - ITU Online](https://www.ituonline.com/tech-definitions/what-is-the-liskov-substitution-principle-lsp/)** – Additional scenarios and anti-patterns.
- Optional: **[LSP Implementation - Stackify](https://stackify.com/solid-design-liskov-substitution-principle/)** – In-depth design discussion.
- Optional: **[LSP Design Patterns - Dev.to](https://dev.to/martingaston/where-s-my-inheritance-understanding-the-liskov-substitution-principle-1911)** – Advanced perspective on inheritance hierarchies.

## 3. This Week’s Work

- Create in-memory `FakeTaskRepository` for tests to exercise `ITaskService` contract.
- Adjust `TaskRepository` and `TaskService` to ensure behaviour matches fake (e.g., null vs. exception cases).
- Add contract tests verifying both real and fake repositories honour expectations.

## 4. Files to Modify

- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (update XML summary clarifying contract)
- `TaskFlowAPI/Repositories/TaskRepository.cs` (ensure behaviour matches contract)
- `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs` (new)
- Optional: `TaskFlowAPI/Services/Tasks/TaskService.cs` for behaviour tweaks
- This file (`Course-Materials/Weekly-Modules/week-13-liskov-substitution.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-13/<your-name>`.
2. Define contract in interface comments (e.g., `GetByIdAsync` returns `null` when missing, never throws).
3. Implement `FakeTaskRepository` inside test project (in-memory list support) adhering to contract.
4. Write `[Theory]` tests using abstract helper verifying behaviours like:
   - `CreateAsync` returns entity with generated Id.
   - `DeleteAsync` silently succeeds when missing.
   - `GetAllAsync` ordering matches specification.
5. Run tests against both fake and real repository (use xUnit `[ClassData]` or manual loops with shared test method).
6. Adjust production repository/service to satisfy failing contract tests.

## 6. How to Test

```bash
dotnet test TaskFlowAPI.sln --filter TaskRepositoryContractTests
```
- Ensure both implementations pass without conditional expectations.

## 7. Success Criteria

- Interface documentation clearly states behavioural contract.
- Fake repository mirrors real repository behaviour (no diverging edge cases).
- Contract tests pass for both implementations.
- Build/tests succeed.

## 8. Submission Process

- Commit `Week 13 – LSP contract tests`.
- PR summary includes snippet of contract test and link to passing run.
- Weekly issue documents at least one behaviour clarified by the contract.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Contract Definition:* Capture one behavioural rule you wrote into interface docs.
- *Test Coverage:* Note which contract test caught differences between fake and real implementations.

### Discussion Prep:
- *What assumptions did the real repository make that weren’t explicit?*
- *How will fakes/stubs help future testing weeks?*
- *Where else might LSP be at risk in this codebase?*
- *How will you maintain contract tests as new repository methods appear?*

## 10. Time Estimate

- 60 min – Reading.
- 10 min – Review contracts + plan tests.
- 35 min – Implement fake + contract tests.
- 15 min – Fix discrepancies + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **[Liskov Substitution Example](../Examples/LiskovSubstitution.cs)**
