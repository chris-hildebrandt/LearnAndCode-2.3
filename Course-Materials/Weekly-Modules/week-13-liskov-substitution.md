# Week 13: Liskov Substitution Principle (LSP)

This week, we are focusing on the Liskov Substitution Principle (LSP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the LSP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Understand the Liskov Substitution Principle and its importance in creating maintainable, extensible, and testable code.
- Ensure repository/service interfaces can be substituted without breaking consumers.
- Validate assumptions via contract-style unit tests using fakes.
- Tighten exception behavior so all implementations honor the same rules.

## 2. Reading & Resources (45 min)

- **[LSP by Barbara Liskov - Wikipedia](https://en.wikipedia.org/wiki/Liskov_substitution_principle)** – Original definition and formal background.
- **[Understanding LSP - Dev.to](https://dev.to/extinctsion/solid-the-liskov-substitution-principle-lsp-in-c-1hl9)** – Practical C# examples.
- **[LSP in Practice - ITU Online](https://www.ituonline.com/tech-definitions/what-is-the-liskov-substitution-principle-lsp/)** – Additional scenarios and anti-patterns.
- Optional: **[LSP Implementation - Stackify](https://stackify.com/solid-design-liskov-substitution-principle/)** – In-depth design discussion.
- Optional: **[LSP Design Patterns - Dev.to](https://dev.to/martingaston/where-s-my-inheritance-understanding-the-liskov-substitution-principle-1911)** – Advanced perspective on inheritance hierarchies.

## 3. This Week’s Work

- Create in-memory `FakeTaskRepository` for tests to exercise `ITaskService` contract.
- Adjust `TaskRepository` and `TaskService` to ensure behavior matches fake (e.g., null vs. exception cases).
- Add contract tests verifying both real and fake repositories honor expectations.

## 4. Files to Modify

**Stage 1 (Generic Lab):**
- `TaskFlowAPI.Tests/Examples/LSPLab/ShapeHierarchy.cs` (NEW - you'll fix the bug here)
- `TaskFlowAPI.Tests/Examples/LSPLab/ShapeContractTests.cs` (NEW - failing test provided)
- `docs/week-13-lsp-lab.md` (NEW - your analysis)

**Stage 2 (TaskFlow Application):**
- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (update XML summary clarifying contract)
- `TaskFlowAPI/Repositories/TaskRepository.cs` (ensure behavior matches contract)
- `TaskFlowAPI.Tests/Unit/FakeTaskRepository.cs` (NEW - in-memory fake)
- `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs` (NEW - verify both implementations)
- Optional: `TaskFlowAPI/Services/Tasks/TaskService.cs` for behavior tweaks

**Stage 3 (Reflection):**
- `docs/week-13-lsp-reflection.md` (NEW - connect both examples)
- This file (`Course-Materials/Weekly-Modules/week-13-liskov-substitution.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### Stage 1: Generic LSP Lab (30 minutes - START HERE)

**Goal:** Discover LSP violation through debugging a failing test

1. Branch `week-13/<your-name>`.
2. Read Section 11: "Stage 1 - The Shape Lab" below
3. Create `TaskFlowAPI.Tests/Examples/LSPLab/ShapeHierarchy.cs` with the provided code
4. Create `TaskFlowAPI.Tests/Examples/LSPLab/ShapeContractTests.cs` with the failing test
5. Run test: `dotnet test --filter Shape_SetWidthAndHeight`
   - **Expected:** Test FAILS for Square (area = 100 instead of 50)
6. Create `docs/week-13-lsp-lab.md` and answer:
   - Why did test fail?
   - Which LSP red flag?
   - Choose fix (Option A or B)
7. Implement your fix in `ShapeHierarchy.cs`
8. Run test again - it should PASS now
9. Commit: "feat: Stage 1 - Fixed shape LSP violation"

### Stage 2: Apply to TaskFlow (45-60 minutes)

10. Read Section 12: "Stage 2 - TaskFlow Application" below
11. Update `ITaskRepository` with explicit behavioral contracts in XML comments
12. Create `TaskFlowAPI.Tests/Unit/FakeTaskRepository.cs` (in-memory implementation)
13. Create `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs` with `[Theory]` tests:
    - `CreateAsync` returns entity with generated Id
    - `GetByIdAsync` returns null when not found (NOT exception)
    - `DeleteAsync` silently succeeds when missing
    - `GetAllAsync` ordering matches specification
14. Run tests: `dotnet test --filter TaskRepositoryContractTests`
    - **Expected:** Some FAIL (Real and Fake differ in behavior)
15. Fix discrepancies in `TaskRepository.cs` and/or `FakeTaskRepository.cs`
16. Run tests again - ALL should PASS
17. Commit: "feat: Stage 2 - TaskRepository contract tests"

### Stage 3: Reflection (20 minutes)

18. Read Section 13: "Stage 3 - Reflection & Connection" below
19. Create `docs/week-13-lsp-reflection.md` and answer reflection questions
20. Complete LSP Red Flags table comparing both examples
21. Optional: Try "Subtype Swap" challenge (swap Fake ↔ Real, run tests)
22. Commit: "docs: Stage 3 - LSP reflection and connection"
23. Create PR with all three stages

## 6. How to Test

```bash
dotnet test TaskFlowAPI.sln --filter TaskRepositoryContractTests
```
- Ensure both implementations pass without conditional expectations.

## 7. Success Criteria

**Stage 1 (Generic Lab):**
- ✅ Shape contract test initially FAILS for Square
- ✅ You fixed the LSP violation (chose Option A or B)
- ✅ Test now PASSES for all shapes
- ✅ `docs/week-13-lsp-lab.md` explains why it failed and your fix

**Stage 2 (TaskFlow):**
- ✅ Interface documentation clearly states behavioral contract
- ✅ Fake repository mirrors real repository behavior (no diverging edge cases)
- ✅ Contract tests pass for BOTH implementations
- ✅ No conditional test logic (same assertions for both)

**Stage 3 (Reflection):**
- ✅ Completed LSP Red Flags comparison table
- ✅ Answered reflection questions in `docs/week-13-lsp-reflection.md`
- ✅ Can explain LSP in 2 sentences

**Overall:**
- ✅ Build/tests succeed
- ✅ PR shows progression through all 3 stages

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-13-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 13 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Contract Definition:* Capture one behavioral rule you wrote into interface docs.
- *Test Coverage:* Note which contract test caught differences between fake and real implementations.

### Discussion Prep:
- *What assumptions did the real repository make that weren’t explicit?*
- *How will fakes/stubs help future testing weeks?*
- *Where else might LSP be at risk in this codebase?*
- *How will you maintain contract tests as new repository methods appear?*

## 10. Time Estimate

- 45 min – Reading (unchanged).
- **30 min – Stage 1: Generic LSP Lab (NEW)**
  - 5 min: Read setup + run failing test
  - 10 min: Answer analysis questions
  - 10 min: Implement fix
  - 5 min: Verify + commit
- **45-60 min – Stage 2: TaskFlow Application (REVISED)**
  - 10 min: Document contracts
  - 20 min: Implement FakeTaskRepository
  - 15 min: Write contract tests
  - 10-25 min: Fix discrepancies
- **20 min – Stage 3: Reflection (NEW)**
  - 10 min: Red flags comparison
  - 10 min: Reflection questions
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.

**Total:** ~2 hours 30 minutes (increased from 2h, but FAR better retention and understanding)

## 11. Stage 1: The Shape Lab (DISCOVERY LEARNING)

**⚠️ READ THIS FIRST:** You're about to experience an LSP violation BEFORE learning the theory. This is intentional! Debugging this bug will make the principle memorable.

### The Setup: Everything Seems Fine

Create `TaskFlowAPI.Tests/Examples/LSPLab/ShapeHierarchy.cs`:

```csharp
namespace TaskFlowAPI.Tests.Examples.LSPLab;

/// <summary>
/// LSP Lab: Broken shape hierarchy
/// Your task: Run tests, identify the bug, fix the hierarchy
/// </summary>
public interface IShape
{
    int GetArea();
    void SetWidth(int width);
    void SetHeight(int height);
}

public class Rectangle : IShape
{
    protected int _width, _height;
    
    public int GetArea() => _width * _height;
    public void SetWidth(int w) => _width = w;
    public void SetHeight(int h) => _height = h;
}

public class Square : IShape  // ❌ VIOLATES LSP
{
    private int _side;
    
    public int GetArea() => _side * _side;
    
    // BUG: Square changes BOTH dimensions when setting one!
    public void SetWidth(int w)
    {
        _side = w;  // ❌ Also changes height implicitly!
    }
    
    public void SetHeight(int h)
    {
        _side = h;  // ❌ Also changes width implicitly!
    }
}
```

**Question:** Does this look correct? Square IS-A shape, right?  
**Answer:** Mathematically yes, but **behaviorally** this will break clients!

---

### The Twist: New Requirement Breaks Everything

Create `TaskFlowAPI.Tests/Examples/LSPLab/ShapeContractTests.cs`:

```csharp
using FluentAssertions;
using Xunit;

namespace TaskFlowAPI.Tests.Examples.LSPLab;

public class ShapeContractTests
{
    [Theory]
    [InlineData(typeof(Rectangle))]
    [InlineData(typeof(Square))]  // ❌ This will FAIL
    public void Shape_SetWidthAndHeight_MaintainsIndependence(Type shapeType)
    {
        // Arrange
        var shape = (IShape)Activator.CreateInstance(shapeType);
        
        // Act: Set width and height independently
        shape.SetWidth(5);
        shape.SetHeight(10);
        
        // Assert: Width and height should be independent
        var area = shape.GetArea();
        area.Should().Be(50);  // Expected: 5 * 10 = 50
        
        // Rectangle: ✓ 50
        // Square: ❌ 100 (both dimensions became 10!)
    }
}
```

---

### The Climax: Run the Test and Watch It FAIL

```bash
dotnet test --filter Shape_SetWidthAndHeight
```

**Result:**
```
❌ Shape_SetWidthAndHeight_MaintainsIndependence(shapeType: Square)
   Expected area to be 50, but found 100.
```

**Why did it fail?**
- Rectangle: width=5, height=10, area=50 ✓
- Square: SetWidth(5) → side=5, SetHeight(10) → side=10, area=100 ❌

Square's SetHeight **overwrote** the width! The behavioral contract of "independent dimensions" was violated.

---

### Student Task: Debug and Fix

Create `docs/week-13-lsp-lab.md` and answer these questions:

#### 1. Why did the test fail for Square? (50 words)

**Your answer:**
_____________

**Hint:** What assumption did the test make about width and height? Did Square honor that assumption?

---

#### 2. Identify the LSP Red Flag

Which LSP violation occurred?

- [ ] Subtype **strengthened preconditions** (requires more than parent)
- [ ] Subtype **weakened postconditions** (delivers less than parent)
- [x] Subtype **changed behavioral contract** (different side effects)
- [ ] Subtype **throws new exceptions** (caller not expecting)

**Explanation:** The `IShape` interface implies width and height are **independent** (you can set one without affecting the other). Square violated this by making them **coupled** (setting one changes both).

---

#### 3. Choose Your Fix

**Option A: Remove Square from hierarchy (RECOMMENDED)**

```csharp
// Square is NOT a behavioral substitute for IShape
// Keep Square as separate class with single SetSize(int) method
public class Square  // No longer implements IShape
{
    private int _side;
    public int GetArea() => _side * _side;
    public void SetSize(int size) => _side = size;  // ✓ No false promises
}
```

**Pros:**
- Square no longer makes false promises
- IShape contract stays simple
- Clients that need Square use SetSize(), not SetWidth/SetHeight

**Cons:**
- Can't use Square in code expecting IShape
- Loses polymorphism

---

**Option B: Change interface contract to match Square's behavior**

```csharp
// Make width/height ALWAYS coupled for ALL IShape implementations
public interface IShape
{
    int GetArea();
    void SetSize(int width, int height);  // Both parameters required together
}

// Rectangle must change too
public class Rectangle : IShape
{
    private int _width, _height;
    public int GetArea() => _width * _height;
    public void SetSize(int w, int h)  // Can no longer set independently
    {
        _width = w;
        _height = h;
    }
}

// Now Square's behavior matches contract
public class Square : IShape
{
    private int _side;
    public int GetArea() => _side * _side;
    public void SetSize(int width, int height) => _side = Math.Max(width, height);
}
```

**Pros:**
- Square can stay in hierarchy
- Contract is explicit (both dimensions set together)

**Cons:**
- Breaks clients that want to set dimensions independently
- Rectangle loses flexibility

---

#### 4. Implement Your Fix

**Your choice:** Option ___

**Justification (50 words):**
_____________

**Implementation:**
[Paste your fixed code here]

---

#### 5. Run Test Again

```bash
dotnet test --filter Shape_SetWidthAndHeight
```

**Expected:**
- If Option A: Test still fails for Square (but that's OK - Square no longer claims to be IShape)
- If Option B: Test PASSES for both (but you changed the contract)

---

### The Lesson: LSP in One Sentence

**Liskov Substitution Principle:** Subtypes must be **behaviorally substitutable** for their base types without breaking client expectations.

**Translation:** If code expects an IShape, you should be able to give it ANY IShape implementation without surprises.

Square **looked** like it worked, but **behaviorally** it violated the implicit contract of independent dimensions. This is an LSP violation.

**Next:** Stage 2 applies this EXACT same lesson to TaskRepository!

---

## 12. Stage 2: TaskFlow Application (NOW YOU GET IT)

**Now that you understand LSP through shapes, let's apply it to TaskFlow:**

### The Problem: Same Bug, Different Domain

Instead of `Rectangle` vs `Square`, we have:
- `TaskRepository` (real database)
- `FakeTaskRepository` (in-memory tests)

Both implement `ITaskRepository`, but do they have **identical behavior**?

---

### The Scenario: Null vs Exception

```csharp
// Contract (implied): GetByIdAsync returns null when task doesn't exist
public interface ITaskRepository
{
    Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct = default);
}

// Implementation 1: TaskRepository (returns null)
public class TaskRepository : ITaskRepository
{
    public async Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _dbContext.Tasks.FindAsync(id, ct);  // Returns null if not found ✓
    }
}

// Implementation 2: FakeTaskRepository (THROWS exception!)
public class FakeTaskRepository : ITaskRepository
{
    private List<TaskEntity> _tasks = new();
    
    public Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        if (task == null)
            throw new TaskNotFoundException(id);  // ❌ VIOLATES LSP - different behavior!
        return Task.FromResult<TaskEntity?>(task);
    }
}
```

**Sound familiar?** This is the Square problem again:
- Interface implies: "returns null when not found"
- Fake violates it: "throws exception when not found"

---

### The Disaster in Production vs Tests

**Production (uses TaskRepository):**
```csharp
public class TaskService
{
    private readonly ITaskRepository _repo;  // Real repo injected
    
    public async Task<TaskDto?> Get(int id)
    {
        var entity = await _repo.GetByIdAsync(id);  // Returns null when not found
        if (entity == null)
        {
            _logger.LogWarning("Task {Id} not found", id);
            return null;  // Controller returns 404
        }
        return _mapper.ToDto(entity);
    }
}

// GET /api/tasks/999 → Returns 404 ✓ Works as expected
```

**Tests (uses FakeTaskRepository):**
```csharp
[Fact]
public async Task Get_WhenTaskNotFound_ReturnsNull()
{
    // Arrange: Fake repo injected instead of real
    var result = await _service.Get(999);  // BOOM! TaskNotFoundException thrown
    
    // Expected: null
    // Actual: Exception
}

// Test fails: "Expected null, but exception was thrown"
```

**Problem:** You can't SUBSTITUTE Fake for Real - they have different behavior!

---

### Your Task: Make Them Substitutable

**Step 1: Explicit Contracts**

Update `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs`:

```csharp
public interface ITaskRepository
{
    /// <summary>
    /// Retrieves a task by its unique ID.
    /// <para><strong>Contract:</strong> Returns null when task with given ID does not exist. NEVER throws NotFoundException.</para>
    /// </summary>
    Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct = default);
    
    /// <summary>
    /// Creates a new task entity.
    /// <para><strong>Contract:</strong> Returns created entity with generated ID (>0). Never returns null.</para>
    /// </summary>
    Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken ct = default);
    
    /// <summary>
    /// Deletes a task by ID.
    /// <para><strong>Contract:</strong> Silently succeeds if ID doesn't exist (idempotent). Never throws NotFoundException.</para>
    /// </summary>
    Task DeleteAsync(int id, CancellationToken ct = default);
}
```

**Step 2: Implement FakeTaskRepository**

Create `TaskFlowAPI.Tests/Unit/FakeTaskRepository.cs`:

```csharp
namespace TaskFlowAPI.Tests.Unit;

public class FakeTaskRepository : ITaskRepository
{
    private readonly List<TaskEntity> _tasks = new();
    private int _nextId = 1;
    
    public Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        return Task.FromResult(task);  // ✓ Returns null, doesn't throw
    }
    
    public Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken ct = default)
    {
        entity.Id = _nextId++;  // ✓ Generate ID just like real repo
        _tasks.Add(entity);
        return Task.FromResult(entity);
    }
    
    public Task DeleteAsync(int id, CancellationToken ct = default)
    {
        var task = _tasks.SingleOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
        }
        // ✓ Silently succeeds if not found
        return Task.CompletedTask;
    }
    
    public Task<List<TaskEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return Task.FromResult(_tasks.ToList());
    }
}
```

**Step 3: Contract Tests (Verify Both Implementations)**

Create `TaskFlowAPI.Tests/Unit/TaskRepositoryContractTests.cs`:

```csharp
using FluentAssertions;
using Xunit;

namespace TaskFlowAPI.Tests.Unit;

public class TaskRepositoryContractTests
{
    [Theory]
    [InlineData(typeof(TaskRepository))]
    [InlineData(typeof(FakeTaskRepository))]
    public async Task GetByIdAsync_WhenNotFound_ReturnsNull(Type repoType)
    {
        // Arrange
        var repo = CreateRepository(repoType);
        
        // Act
        var result = await repo.GetByIdAsync(999);  // ID doesn't exist
        
        // Assert: BOTH implementations must return null
        result.Should().BeNull();
    }
    
    [Theory]
    [InlineData(typeof(TaskRepository))]
    [InlineData(typeof(FakeTaskRepository))]
    public async Task CreateAsync_ReturnsEntityWithGeneratedId(Type repoType)
    {
        // Arrange
        var repo = CreateRepository(repoType);
        var entity = new TaskEntity { Title = "Test", Priority = 1 };
        
        // Act
        var result = await repo.CreateAsync(entity);
        
        // Assert: BOTH implementations must generate ID
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be("Test");
    }
    
    [Theory]
    [InlineData(typeof(TaskRepository))]
    [InlineData(typeof(FakeTaskRepository))]
    public async Task DeleteAsync_WhenNotFound_SilentlySucceeds(Type repoType)
    {
        // Arrange
        var repo = CreateRepository(repoType);
        
        // Act
        Func<Task> act = async () => await repo.DeleteAsync(999);  // ID doesn't exist
        
        // Assert: BOTH implementations must NOT throw
        await act.Should().NotThrowAsync();
    }
    
    private ITaskRepository CreateRepository(Type type)
    {
        // Factory creates either TaskRepository or FakeTaskRepository
        // (Implementation depends on DI setup)
        return (ITaskRepository)Activator.CreateInstance(type);
    }
}
```

**Step 4: Run Tests**

```bash
dotnet test --filter TaskRepositoryContractTests
```

**Expected (first run):**
- Some tests may FAIL (Real and Fake differ)
- Example: Fake throws exception when Real returns null

**Fix discrepancies in both implementations until ALL tests PASS**

---

### Success: Perfect Substitutability

When contract tests PASS for both implementations:
- You can swap Fake ↔ Real without tests breaking
- Production and test behavior identical
- LSP satisfied ✓

**This is the SAME fix you did for shapes:**
- Shapes: Removed Square from hierarchy OR changed interface
- TaskFlow: Made Fake honor Real's behavioral contract

---

## 13. Stage 3: Reflection & Connection

**Now connect both examples:**

Create `docs/week-13-lsp-reflection.md` and complete:

### LSP Red Flags Comparison

| Red Flag | Shape Example | TaskFlow Example |
|----------|---------------|------------------|
| Changed behavioral contract | Square altered width/height independence assumption | Fake throws instead of returning null |
| Strengthened preconditions | (Not in this lab) | Example: Repository requires non-null entities when interface allows null |
| Weakened postconditions | Square didn't deliver 5x10 rectangle (gave 10x10) | Example: Fake doesn't populate CreatedAt when Real does |
| Threw unexpected exceptions | (Not in this lab) | Fake throws TaskNotFoundException when Real returns null |

### Reflection Questions

**1. Lab vs. Production**

"Why did we start with shapes instead of jumping straight to TaskRepository?"

**Your answer (50 words):**
_____________

**Model answer:** Shapes are instantly graspable - everyone understands rectangles. Once you SEE the bug fail a test in a simple domain, you understand WHY explicit contracts matter when dealing with complex production code.

---

**2. Red Flag Spotting**

"Name another TaskFlow interface that worries you now. Which LSP red flag might it trigger?"

**Your answer:**
- Interface: `I_____________`
- Potential red flag: _____________
- Why: _____________

**Think about:** ITaskFilter, ITaskService, IValidator<T>, ITaskCache

---

**3. Real-World Impact**

"How would you explain LSP to a new team member in 2 sentences?"

**Your answer:**
_____________

**Model answer:** "Any subtype (implementation) should be substitutable for its base type (interface) without changing the program's correctness. If your test fake behaves differently than production, you've violated LSP and your tests are lying to you."

---

### Gamification: Subtype Swap Challenge

**After completing all stages, try this:**

1. In `TaskService.cs`, swap the injected repository:
   - Swap `FakeTaskRepository` ↔ `TaskRepository`
2. Run all tests: `dotnet test`
3. Document what breaks (if anything)

**Perfect LSP:** NOTHING breaks! Both implementations behave identically.

**LSP violation found:** Tests fail when swapping implementations.

---

### Discussion Prep

**Bring to discussion:**
- How the shape bug made LSP "click" for you
- One TaskFlow interface you're worried violates LSP
- How you'll use contract tests in future projects

---

## 14. Additional Resources

- **[Liskov Substitution Example](../Examples/LiskovSubstitution.cs)**
- **[Rectangle-Square Problem](https://en.wikipedia.org/wiki/Circle-ellipse_problem)** - Classic LSP violation
- **[Design by Contract](https://en.wikipedia.org/wiki/Design_by_contract)** - Formal basis for LSP
