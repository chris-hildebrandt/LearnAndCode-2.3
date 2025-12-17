# Week 17: Unit Testing & TDD

This week, we will explore the importance of unit testing and test-driven development, relating these practices to In Time Tec's focus on automated testing and the test automation pyramid. This module is based on **Clean Code Chapter 9: Unit Tests**.

## 1. Learning Objectives

- Understand the importance of unit testing in ensuring high-quality software.
- Write unit tests for your project using a test framework.
- Apply the principles of test-driven development (TDD) to implement a new feature.
- Discuss the relationship between unit tests, TDD, and the test automation pyramid.
- Write focused unit tests using Arrange-Act-Assert pattern.
- Practice TDD for a new feature (`CompleteTaskAsync`).
- Achieve ≥80% coverage on `TaskService` business logic.

## 2. Reading & Resources (60 min)

- **Clean Code Chapter 9: Unit Tests (pp. 167-192)**.
- **[The Art of Writing Beautiful Unit Tests](https://levelup.gitconnected.com/4-rules-for-clean-expressive-and-reliable-unit-tests-d88d5db82b7c)** – Patterns for expressive tests.
- **[Clean Code Tip: F.I.R.S.T. acronym for better unit tests](https://code4it.dev/cleancodetips/f-i-r-s-t-unit-tests/)** – Quick checklist.
- **[What is Test Driven Development (TDD)?](https://www.guru99.com/test-driven-development.html)** – Intro to the workflow we’ll use.
- Optional: **[YouTube: Test Driven Development | Crash Course](https://www.youtube.com/watch?v=z6gOPonp2t0)** (optional, 15 min) – Visual walkthrough.

## 3. This Week’s Work

**REVISED: Fix Broken Tests First, Then TDD New Feature**

- **Step 0:** Fix existing broken tests (see Section 11 below) - 15 min
- **Step 1:** Learn Mock vs Fake decision guide (Section 12) - 10 min  
- **Step 2:** TDD `CompleteTaskAsync` method using Red-Green-Refactor - 45 min
- Add `CompleteTaskAsync` method to `TaskService` (TDD it).
- Write tests in `TaskFlowAPI.Tests/Unit` covering:
  - `GetAllTasksAsync` mapping.
  - `CreateTaskAsync` happy path + validation failure.
  - New `CompleteTaskAsync` (success + already completed scenarios).
- Use `Moq` + fake clock to control time.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` (support completion logic)
- `TaskFlowAPI.Tests/Unit/*.cs` (create or expand test classes)
- Optional: `TaskFlowAPI.Tests/Examples` remove skip if you convert example.
- This file (`Course-Materials/Weekly-Modules/week-17-unit-testing-tdd.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-17/<your-name>`.
2. Start with a failing test for `CompleteTaskAsync` (TDD). Define behavior: mark complete, set timestamp, return DTO.
3. Implement minimal production code to pass the test using injected `ISystemClock`.
4. Add tests for existing methods, mocking repository interactions and validators.
5. Use `FluentAssertions` for assertions and `Moq` for verifying repository/validator calls.
6. Run `dotnet test` and inspect coverage report (`dotnet test /p:CollectCoverage=true` optional with coverlet).
7. Update placeholder tests (remove skip) once real tests exist.

## 6. How to Test

```bash
dotnet test TaskFlowAPI.sln --filter TaskService
# Optional coverage
dotnet test TaskFlowAPI.sln /p:CollectCoverage=true /p:CoverletOutputFormat=lcov
```

## 7. Success Criteria

- All placeholder `[Fact(Skip…)]` replaced with real tests.
- `CompleteTaskAsync` implemented and covered by tests.
- Coverage for `TaskService` ≥80% (include screenshot or coverage report).
- Tests pass consistently.

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-17-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 17 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Red-Green-Refactor:* Capture one notable iteration (test, implementation, refactor) and what you learned.
- *Coverage Review:* Where did coverage fall short of 80%, and what follow-up action will you take?

### Discussion Prep:
- *How did TDD influence your implementation of `CompleteTaskAsync`?*
- *What mocks/fakes did you choose and why?*
- *What gaps remain in test coverage?*
- *How will you keep tests maintainable as services evolve?*

## 10. Time Estimate

- 60 min – Reading.
- 45 min – Tests + implementation.
- 15 min – Coverage run.
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~120 minutes.

## 11. Fix Broken Tests First Exercise (NEW - Start Here)

**Goal:** Understand existing test infrastructure before writing new tests

**Time:** 15 minutes

### Current State

Your `TaskFlowAPI.Tests` project likely has placeholder/skipped tests:

```csharp
[Fact(Skip = "Placeholder - implement in Week 17")]
public void TaskService_CreateAsync_ReturnsDto()
{
    // TODO: Implement
}
```

### Your Task: Fix ONE Broken Test

**Before TDD, get comfortable with test infrastructure:**

1. **Find existing test file:**
   ```bash
   ls TaskFlowAPI.Tests/Unit/
   # Or TaskFlowAPI.Tests/Examples/
   ```

2. **Pick simplest test** (likely GetAllAsync or similar)

3. **Remove Skip attribute** and implement:
   ```csharp
   [Fact]  // Removed: (Skip = "...")
   public async Task GetAllAsync_ReturnsAllTasks()
   {
       // Arrange
       var fakeRepo = new FakeTaskRepository();
       var service = new TaskService(fakeRepo, new TaskMapper(), ...);
       
       // Act
       var result = await service.GetAllAsync();
       
       // Assert
       result.Should().NotBeEmpty();
   }
   ```

4. **Run test:**
   ```bash
   dotnet test --filter GetAllAsync
   ```

5. **Fix compilation errors** until test runs (pass or fail)

**Checkpoint:**
- [ ] Found test project structure?
- [ ] One test compiles and runs?
- [ ] Understand Arrange-Act-Assert?

**Why This Matters:**
- Validates test infrastructure works
- Fixes "broken window" (skipped tests)
- Builds confidence before TDD
- Faster than TDD from scratch (no red phase needed)

**Deliverable:** At least ONE previously-skipped test now passes

---

## 12. Mock vs Fake Decision Guide (NEW)

**Goal:** Choose right test double for the job

**Time:** 10 minutes

### The Problem: When to Mock, When to Fake?

**You need to test TaskService, which depends on ITaskRepository:**

```csharp
public class TaskService
{
    private readonly ITaskRepository _repository;
    // ... test needs repository!
}
```

**Option A: Use Moq (Mock)**
```csharp
var mockRepo = new Mock<ITaskRepository>();
mockRepo.Setup(r => r.GetAllAsync(It.IsAny<CancellationToken>()))
    .ReturnsAsync(new List<TaskEntity> { ... });

var service = new TaskService(mockRepo.Object, ...);
```

**Option B: Use FakeTaskRepository (Fake)**
```csharp
var fakeRepo = new FakeTaskRepository();
// Already has in-memory implementation

var service = new TaskService(fakeRepo, ...);
```

---

### Decision Matrix

| Aspect | Mock (Moq) | Fake (FakeTaskRepository) |
|--------|------------|---------------------------|
| **Setup Complexity** | HIGH (must configure every method call) | LOW (works out of box) |
| **Verification** | ✅ Can verify calls (Times.Once) | ❌ Can't verify (unless you add tracking) |
| **Reusability** | ❌ Setup per test | ✅ Same fake across all tests |
| **Maintenance** | ❌ Breaks when interface changes | ✅ Compiler catches changes |
| **Test Readability** | ❌ Lots of setup code | ✅ Minimal setup |
| **When to Use** | Verifying behavior (Was SaveAsync called?) | Testing logic (Does service return correct DTO?) |

---

### When To Use Each

**Use Mock (Moq) when:**
- ✅ **Verifying interactions** ("Did service call repository.SaveAsync once?")
- ✅ **Testing error paths** (throw exception, verify handling)
- ✅ **Dependency has side effects** (email service, payment gateway)
- ✅ **Quick one-off test** (don't want to maintain fake)

**Example:**
```csharp
[Fact]
public async Task CreateTaskAsync_CallsSaveAsync()
{
    // Arrange
    var mockRepo = new Mock<ITaskRepository>();
    mockRepo.Setup(r => r.CreateAsync(It.IsAny<TaskEntity>(), default))
        .ReturnsAsync((TaskEntity e, CancellationToken ct) => e);
    
    var service = new TaskService(mockRepo.Object, ...);
    
    // Act
    await service.CreateTaskAsync(new CreateTaskRequest { ... });
    
    // Assert
    mockRepo.Verify(r => r.CreateAsync(It.IsAny<TaskEntity>(), default), Times.Once);
    //        ^^^^^^ Verifying the call happened!
}
```

---

**Use Fake when:**
- ✅ **Testing business logic** ("Does service map entities to DTOs correctly?")
- ✅ **Multiple tests need same dependency** (reuse across test class)
- ✅ **Dependency is data-focused** (repository, cache)
- ✅ **Integration-style unit tests** (test multiple layers together)

**Example:**
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
    //      ^^^^^ Testing business logic, not interactions
}
```

---

### Week 17 Recommendation

**For this project:**
- Use **FakeTaskRepository** (from Week 8) for MOST tests
  - Easier to maintain
  - Already exists
  - Tests business logic

- Use **Moq** ONLY when you need to verify interactions:
  - "Was SaveAsync called?"
  - "Was exception thrown?"

**Ratio:** 80% Fake, 20% Mock

---

### Anti-Pattern: Over-Mocking

**❌ BAD: Mocking everything**
```csharp
var mockRepo = new Mock<ITaskRepository>();
mockRepo.Setup(r => r.GetAllAsync(default))
    .ReturnsAsync(new List<TaskEntity> { ... });

var mockMapper = new Mock<ITaskMapper>();  // ❌ Why mock mapper?
mockMapper.Setup(m => m.ToDto(It.IsAny<TaskEntity>()))
    .Returns(new TaskDto { ... });

var mockRules = new Mock<ITaskBusinessRules>();  // ❌ Why mock rules?
mockRules.Setup(r => r.ValidateCanComplete(It.IsAny<TaskEntity>()));

// Test becomes: "Did mocks return what I told them to?" (useless!)
```

**✅ GOOD: Fake data-focused, Mock side-effects**
```csharp
var fakeRepo = new FakeTaskRepository();  // ✅ Real logic
var realMapper = new TaskMapper();         // ✅ Real logic
var realRules = new TaskBusinessRules();   // ✅ Real logic

// Test becomes: "Does service coordinate these correctly?" (valuable!)
```

---

## 13. Coverage Anti-Patterns to Avoid (NEW)

**Goal:** Understand what coverage DOESN'T mean

**Time:** 10 minutes

### Anti-Pattern 1: Testing Getters/Setters

**❌ WASTE OF TIME:**
```csharp
[Fact]
public void TaskDto_Title_GetSet_Works()
{
    var dto = new TaskDto();
    dto.Title = "Test";
    dto.Title.Should().Be("Test");  // ❌ Testing the compiler!
}
```

**Why Skip:** Compiler guarantees this works. No business logic to test.

---

### Anti-Pattern 2: Testing Framework Code

**❌ WASTE OF TIME:**
```csharp
[Fact]
public void TaskRepository_Inherits_FromDbContext()
{
    typeof(TaskFlowDbContext).Should().BeAssignableTo<DbContext>();
    // ❌ Testing Entity Framework!
}
```

**Why Skip:** Testing Microsoft's code, not yours.

---

### Anti-Pattern 3: 100% Coverage Via Trivial Tests

**❌ FALSE CONFIDENCE:**
```csharp
[Fact]
public void TaskService_Constructor_Succeeds()
{
    var service = new TaskService(...);
    service.Should().NotBeNull();  // ❌ Increases coverage, tests nothing
}
```

**Why Harmful:** Coverage goes up, quality doesn't.

---

### Anti-Pattern 4: Testing Every Branch (Missing Edge Cases)

**❌ INCOMPLETE:**
```csharp
[Fact]
public async Task CompleteTask_WhenNotCompleted_Succeeds()
{
    // Tests happy path
}

// ❌ MISSING: What if task doesn't exist?
// ❌ MISSING: What if task already completed?
// ❌ MISSING: What if null passed?
```

**Why Dangerous:** 100% line coverage ≠ 100% scenario coverage

---

### What TO Test Instead

**✅ FOCUS ON:**
1. **Business Logic:** "Does priority validation work?"
2. **Edge Cases:** "What if list is empty?"
3. **Error Paths:** "Does it throw correct exception?"
4. **Integration Points:** "Does service call repository correctly?"

**✅ IGNORE:**
1. Getters/Setters (unless custom logic)
2. Framework code (EF Core, ASP.NET)
3. Trivial constructors
4. Auto-properties

---

### Coverage Goals

| Component | Target Coverage | Why |
|-----------|----------------|-----|
| **Services** | 80-90% | Core business logic |
| **Validators** | 90-100% | Critical for data integrity |
| **Mappers** | 50-70% | Simple transformations |
| **Controllers** | 40-60% | Thin orchestration layer |
| **DTOs** | 0% | No logic to test |
| **Entities** | 50-70% | Test behaviors (Complete, Reopen) |

**Overall Project:** 70-80% is excellent (don't chase 100%)

---

### Week 17 Deliverable

**Instead of coverage report, answer these:**

1. **What did you NOT test and why?**
   - Example: "Skipped TaskDto properties (no logic)"

2. **What edge case surprized you?**
   - Example: "CompleteAsync with null task threw NullReferenceException (fixed)"

3. **Which test found a real bug?**
   - Example: "Test revealed mapper didn't handle null Project (added ?. operator)"

**Deliverable:** Add to journal in Section 9

---

## 14. Additional Resources

- **[Test-Driven Development Example](../Examples/TestDrivenDevelopment.md)**

