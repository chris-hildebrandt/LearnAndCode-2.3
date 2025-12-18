# Week 17: Unit Testing & TDD

This week, we will explore the importance of unit testing and test-driven development, relating these practices to In Time Tec's focus on automated testing and the test automation pyramid. This module is based on **Clean Code Chapter 9: Unit Tests**.

## 1. Learning Objectives

- Understand the importance of unit testing in ensuring high-quality software.
- Write unit tests for your project using a test framework.
- Apply the principles of test-driven development (TDD) to implement a new feature.
- Write focused unit tests using Arrange-Act-Assert pattern.
- Practice TDD for a new feature (`CompleteTaskAsync`).
- Achieve ≥80% coverage on `TaskService` business logic.

## 2. Reading & Resources (60 min)

- **Clean Code Chapter 9: Unit Tests (pp. 123-136)** (25 min) - Focus on clean test principles and F.I.R.S.T. acronym.
- **[The Art of Writing Beautiful Unit Tests](https://levelup.gitconnected.com/4-rules-for-clean-expressive-and-reliable-unit-tests-d88d5db82b7c)** (15 min) - Patterns for expressive tests.
- **[Clean Code Tip: F.I.R.S.T. acronym for better unit tests](https://code4it.dev/cleancodetips/f-i-r-s-t-unit-tests/)** (10 min) - Quick checklist.
- **[What is Test Driven Development (TDD)?](https://www.guru99.com/test-driven-development.html)** (10 min) - Intro to the Red-Green-Refactor workflow.

## 3. This Week's Work

- Fix at least one existing broken/skipped test to understand test infrastructure.
- TDD `CompleteTaskAsync` method using Red-Green-Refactor cycle.
- Write tests in `TaskFlowAPI.Tests/Unit` covering:
  - `GetAllTasksAsync` mapping.
  - `CreateTaskAsync` happy path + validation failure.
  - New `CompleteTaskAsync` (success + already completed scenarios).
- Use `FakeTaskRepository` for most tests; use `Moq` only when verifying interactions.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` (support completion logic).
- `TaskFlowAPI.Tests/Unit/*.cs` (create or expand test classes).
- Optional: `TaskFlowAPI.Tests/Examples` (remove skip attributes if converting examples).
- This file (`Course-Materials/Weekly-Modules/week-17-unit-testing-tdd.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-17-submission`.
2. **Fix one broken test first:**
   - Find test with `[Fact(Skip = "...")]` in `TaskFlowAPI.Tests/Unit/` or `Examples/`.
   - Remove skip attribute and implement using Arrange-Act-Assert.
   - Run `dotnet test --filter <TestName>` until it passes.
3. **TDD `CompleteTaskAsync`** (Red-Green-Refactor):
   - **Red**: Write failing test defining expected behavior.
   - **Green**: Implement minimal code to pass test.
   - **Refactor**: Clean up implementation while keeping tests green.
4. Add tests for existing methods:
   - Use `FakeTaskRepository` for data access.
   - Use `FakeSystemClock` for time control.
   - Use `Moq` only when verifying interactions (e.g., "Was SaveAsync called?").
5. Use `FluentAssertions` for assertions.
6. Run `dotnet test TaskFlowAPI.sln --filter TaskService` to verify all tests pass.
7. Optional: Run coverage `dotnet test /p:CollectCoverage=true`.
8. Document what you did NOT test and why in journal (Section 9).

## 6. How to Test

```bash
# Run all TaskService tests
dotnet test TaskFlowAPI.sln --filter TaskService

# Run specific test
dotnet test --filter CompleteTaskAsync

# Optional: Generate coverage report
dotnet test TaskFlowAPI.sln /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

## 7. Success Criteria

- At least one previously-skipped test now passes.
- `CompleteTaskAsync` implemented using TDD (test-first approach).
- Tests cover happy paths and edge cases (already completed, null task, etc.).
- Coverage for `TaskService` ≥80% on business logic (document gaps in journal).
- All tests pass consistently.
- Week 17 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-17-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 17 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Red-Green-Refactor: Capture one notable TDD iteration (test, implementation, refactor) and what you learned from the cycle.*
- *Coverage Review: Document what you did NOT test and why. Which edge case surprised you? Did any test find a real bug?*
- *Mock vs Fake: Which approach did you use more, and why did it fit your tests better?*

### Discussion Prep:

- *How did TDD influence your implementation of `CompleteTaskAsync`?*
- *Share a test that found a real bug in your implementation.*
- *What made you choose Fake vs Mock for different scenarios?*
- *How will you keep tests maintainable as services evolve?*

## 10. Time Estimate

- 60 min — Reading.
- 15 min — Fix one broken test.
- 45 min — TDD CompleteTaskAsync and write additional tests.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 35 minutes.

## 11. TDD Example: CompleteTaskAsync

### Red Phase (Write Failing Test)

```csharp
[Fact]
public async Task CompleteTaskAsync_MarksTaskAsCompleted()
{
    // Arrange
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 15, 10, 0, 0));
    var fakeRepo = new FakeTaskRepository();
    var task = TaskEntity.Create("Test Task", 1);
    await fakeRepo.CreateAsync(task);
    
    var service = new TaskService(fakeRepo, new TaskMapper(), fakeClock, ...);
    
    // Act
    await service.CompleteTaskAsync(task.Id);  // ❌ Method doesn't exist yet!
    
    // Assert
    var completed = await fakeRepo.GetByIdAsync(task.Id);
    completed.IsCompleted.Should().BeTrue();
    completed.CompletedAt.Should().Be(new DateTime(2025, 1, 15, 10, 0, 0));
}
```

**Run test:** ❌ Fails (compilation error).

### Green Phase (Minimal Implementation)

```csharp
public async Task CompleteTaskAsync(int id, CancellationToken cancellationToken = default)
{
    var task = await _repository.GetByIdAsync(id, cancellationToken);
    task.Complete(_clock);
    await _repository.UpdateAsync(task, cancellationToken);
}
```

**Run test:** ✅ Passes!

### Refactor Phase (Add Validation)

```csharp
public async Task CompleteTaskAsync(int id, CancellationToken cancellationToken = default)
{
    var task = await _repository.GetByIdAsync(id, cancellationToken);
    
    if (task == null)
        throw new NotFoundException($"Task {id} not found");
    
    if (task.IsCompleted)
        return; // Idempotent
    
    task.Complete(_clock);
    await _repository.UpdateAsync(task, cancellationToken);
}
```

**Run test:** ✅ Still passes!

### Add Edge Case Test

```csharp
[Fact]
public async Task CompleteTaskAsync_WhenAlreadyCompleted_DoesNothing()
{
    // Arrange
    var fakeClock = new FakeSystemClock(new DateTime(2025, 1, 15, 10, 0, 0));
    var fakeRepo = new FakeTaskRepository();
    var task = TaskEntity.Create("Test Task", 1);
    task.Complete(fakeClock);
    await fakeRepo.CreateAsync(task);
    
    var service = new TaskService(fakeRepo, new TaskMapper(), fakeClock, ...);
    
    // Act
    await service.CompleteTaskAsync(task.Id);
    
    // Assert
    var completed = await fakeRepo.GetByIdAsync(task.Id);
    completed.CompletedAt.Should().Be(new DateTime(2025, 1, 15, 10, 0, 0)); // Unchanged
}
```

## 12. Mock vs Fake Decision Guide

### When to Use Fake (80% of tests)

**Use `FakeTaskRepository` when testing business logic:**

```csharp
[Fact]
public async Task GetAllAsync_ReturnsAllTasksAsDtos()
{
    // Arrange
    var fakeRepo = new FakeTaskRepository();
    await fakeRepo.CreateAsync(TaskEntity.Create("Task 1", 1));
    await fakeRepo.CreateAsync(TaskEntity.Create("Task 2", 1));
    
    var service = new TaskService(fakeRepo, new TaskMapper(), ...);
    
    // Act
    var result = await service.GetAllAsync();
    
    // Assert
    result.Should().HaveCount(2);
    result.First().Title.Should().Be("Task 1");
}
```

**Benefits:** Easy to use, minimal setup, reusable.

### When to Use Mock (20% of tests)

**Use `Moq` when verifying interactions:**

```csharp
[Fact]
public async Task CreateTaskAsync_CallsRepositoryCreate()
{
    // Arrange
    var mockRepo = new Mock<ITaskRepository>();
    mockRepo.Setup(r => r.CreateAsync(It.IsAny<TaskEntity>(), default))
        .ReturnsAsync((TaskEntity e, CancellationToken ct) => e);
    
    var service = new TaskService(mockRepo.Object, ...);
    
    // Act
    await service.CreateTaskAsync(new CreateTaskRequest { Title = "Test" });
    
    // Assert
    mockRepo.Verify(r => r.CreateAsync(It.IsAny<TaskEntity>(), default), Times.Once);
}
```

**Benefits:** Can verify method was called, useful for void methods or side effects.

### Anti-Pattern: Over-Mocking

**❌ DON'T mock everything:**
```csharp
var mockRepo = new Mock<ITaskRepository>();
var mockMapper = new Mock<ITaskMapper>();  // ❌ Why?
var mockRules = new Mock<ITaskBusinessRules>();  // ❌ Why?
```

**✅ DO use real implementations for logic:**
```csharp
var fakeRepo = new FakeTaskRepository();  // ✅
var realMapper = new TaskMapper();         // ✅
var realRules = new TaskBusinessRules();   // ✅
```

## 13. Coverage Guidelines

### What to Test
- ✅ Business logic in services.
- ✅ Edge cases (null, empty, boundary conditions).
- ✅ Error paths (exceptions thrown correctly).
- ✅ Integration points (service calls repository).

### What NOT to Test
- ❌ Getters/Setters (unless custom logic).
- ❌ Framework code (EF Core, ASP.NET).
- ❌ Trivial constructors.
- ❌ DTOs with no behavior.

### Coverage Targets

| Component | Target | Rationale |
|-----------|--------|-----------|
| Services | 80-90% | Core business logic |
| Validators | 90-100% | Critical for data integrity |
| Mappers | 50-70% | Simple transformations |
| Controllers | 40-60% | Thin orchestration |
| DTOs | 0% | No logic |
| Entities | 50-70% | Behavior methods only |

**Project Overall:** 70-80% is excellent.

### Journal Question

Instead of just reporting coverage percentage, answer in journal:
1. **What did you NOT test and why?**
2. **What edge case surprised you?**
3. **Which test found a real bug?**

## 14. Additional Resources

### Examples

- **[Test-Driven Development Example](../Examples/TestDrivenDevelopment.md)** - Review for TDD patterns.

### External Resources

- **[xUnit Documentation](https://xunit.net/)** - Official testing framework docs.
- **[Moq Quickstart](https://github.com/moq/moq4/wiki/Quickstart)** - Mocking framework guide.
- **[FluentAssertions Documentation](https://fluentassertions.com/introduction)** - Assertion library reference.

### Optional Deep Dives

- **[YouTube: Test Driven Development Crash Course](https://www.youtube.com/watch?v=z6gOPonp2t0)** (15 min) - Visual walkthrough.
- **[The Art of Unit Testing](https://www.manning.com/books/the-art-of-unit-testing-second-edition)** - Comprehensive book.
- **[Test Pyramid](https://martinfowler.com/articles/practical-test-pyramid.html)** - Understanding test strategy.