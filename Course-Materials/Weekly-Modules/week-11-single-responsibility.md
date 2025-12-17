# Week 11: Single Responsibility Principle (SRP)

This week, we are focusing on the Single Responsibility Principle (SRP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the SRP and the Quality Manifesto's focus on technical excellence. This module is based on **Clean Code Chapter 10: Classes**.

## 1. Learning Objectives

- Identify and eliminate SRP violations within `TaskService`.
- Extract mapping, validation, and business-rule logic into focused classes.
- Update dependency injection to wire new components.
- Understand how SRP improves testability and maintainability.

## 2. Reading & Resources (60 min)

- **Clean Code Chapter 10: Classes (pp. 137-154)** — Single Responsibility Principle, cohesion, and organizing for change (30 min).
- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** — Original definition and context (10 min).
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** — Practical examples and pitfalls (10 min).
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** — Strategies for untangling mixed responsibilities (10 min).

## 3. This Week's Work

- Review the SRP Smell Detector Framework in Additional Resources to identify violations.
- Extract `TaskMapper` class for all entity/DTO mapping logic.
- Extract `TaskBusinessRules` class for domain validation logic.
- Verify FluentValidation validators work via DI (no extraction needed).
- Update DI registrations and verify no behavior change.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskMapper.cs` (create).
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` (create).
- Optional: `TaskFlowAPI/Services/Tasks/Rules/ITaskBusinessRules.cs` (create interface for testability).
- `TaskFlowAPI/Program.cs` (add registrations).
- This file (`Course-Materials/Weekly-Modules/week-11-single-responsibility.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-11-submission`.

2. **Study SRP Smell Detector Framework (15 min)**
   
   Before extracting anything, read the "SRP Smell Detector Framework" in Additional Resources (Section 11). This will help you identify WHAT responsibilities to extract and WHY.
   
   Optional: Create `docs/week-11-extraction-plan.md` with your analysis using the framework.

3. **Extract TaskMapper (30 min)**
   
   **Why first:** No side effects, pure data transformation.
   
   Create `Services/Tasks/TaskMapper.cs`:
   ```csharp
   namespace TaskFlowAPI.Services.Tasks;
   
   public class TaskMapper
   {
       public TaskDto ToDto(TaskEntity entity)
       {
           return new TaskDto
           {
               Id = entity.Id,
               Title = entity.Title,
               Description = entity.Description,
               Priority = entity.Priority,
               DueDate = entity.DueDate,
               IsCompleted = entity.IsCompleted,
               CompletedAt = entity.CompletedAt,
               CreatedAt = entity.CreatedAt,
               ProjectId = entity.ProjectId,
               ProjectName = entity.Project?.Name
           };
       }
   }
   ```
   
   Update `TaskService` to use mapper:
   ```csharp
   private readonly TaskMapper _mapper;
   
   public TaskService(
       ITaskRepository repository,
       TaskMapper mapper,  // NEW
       ILogger<TaskService> logger)
   {
       _repository = repository;
       _mapper = mapper;
       _logger = logger;
   }
   
   // Replace MapToDto calls:
   return _mapper.ToDto(entity);  // Instead of MapToDto(entity)
   ```
   
   Remove old mapping methods from `TaskService`.
   
   **Checkpoint:**
   - [ ] TaskService no longer has mapping methods?
   - [ ] All MapToDto calls use `_mapper`?
   - [ ] Does service still compile?

4. **Extract TaskBusinessRules (30 min)**
   
   **Why second:** May depend on entities, but still isolated logic.
   
   Create `Services/Tasks/Rules/TaskBusinessRules.cs`:
   ```csharp
   namespace TaskFlowAPI.Services.Tasks.Rules;
   
   public class TaskBusinessRules
   {
       public void ValidateCanComplete(TaskEntity task)
       {
           if (task.IsCompleted)
               throw new InvalidOperationException($"Task {task.Id} is already completed");
       }
       
       public void ValidateCanReopen(TaskEntity task)
       {
           if (!task.IsCompleted)
               throw new InvalidOperationException($"Task {task.Id} is not completed");
       }
   }
   ```
   
   Update `TaskService`:
   ```csharp
   private readonly TaskBusinessRules _businessRules;
   
   // In Complete() method:
   _businessRules.ValidateCanComplete(task);
   ```
   
   **Checkpoint:**
   - [ ] Business rules extracted from service?
   - [ ] Service calls `_businessRules` methods?
   - [ ] Does service still compile?

5. **Optional: Create ITaskBusinessRules Interface (10 min)**
   
   For improved testability, extract an interface:
   ```csharp
   namespace TaskFlowAPI.Services.Tasks.Rules;
   
   public interface ITaskBusinessRules
   {
       void ValidateCanComplete(TaskEntity task);
       void ValidateCanReopen(TaskEntity task);
   }
   ```
   
   Update `TaskBusinessRules` to implement the interface:
   ```csharp
   public class TaskBusinessRules : ITaskBusinessRules
   {
       // Implementation...
   }
   ```
   
   Update `TaskService` to depend on interface:
   ```csharp
   private readonly ITaskBusinessRules _businessRules;
   
   public TaskService(
       ITaskRepository repository,
       TaskMapper mapper,
       ITaskBusinessRules businessRules,  // Interface instead of concrete class
       ILogger<TaskService> logger)
   ```

6. **Update DI Registrations (10 min)**
   
   Register in `Program.cs`:
   ```csharp
   builder.Services.AddScoped<TaskMapper>();
   builder.Services.AddScoped<ITaskBusinessRules, TaskBusinessRules>();
   // Or without interface:
   // builder.Services.AddScoped<TaskBusinessRules>();
   ```

7. **Verify No Behavior Change (5 min)**
   
   Build and test:
   ```bash
   dotnet build TaskFlowAPI.sln
   dotnet test TaskFlowAPI.sln
   ```
   
   Manual test via Swagger to verify all endpoints work as before.

8. **Verify Service Simplification**
   
   Check that `TaskService` is now focused on orchestration only:
   - Ideally ≤150 lines
   - No mapping logic
   - No business rule validation logic
   - Only orchestrates calls to collaborators

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual tests via Swagger:
- **GET /api/tasks** — Verify mapping still works.
- **POST /api/tasks** — Verify creation and validation still work.
- **PUT /api/tasks/{id}** — Verify updates work.
- **Complete task endpoint** — Verify business rules are enforced.

All functionality should work exactly as before. If anything changed, you have a bug in the extraction.

## 7. Success Criteria

- `TaskService` focused on orchestration only (≤150 lines ideally).
- Mapper and business rules classes contain clear, single responsibilities.
- Dependency injection updated without circular references.
- Build and tests succeed.
- Manual tests confirm no behavior changes.
- Week 11 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

1. Create a new branch for your weekly work (e.g., `git checkout -b week-11-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 11 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Responsibility Audit: Capture the smell or metric that convinced you a new class was warranted.*
- *Dependency Impact: Note any DI registration changes and how they affect testability.*
- *SRP Application: How did applying the SRP Smell Detector Framework help you identify which responsibilities to extract?*

### Discussion Prep:

- *What metric indicated `TaskService` was doing too much?*
- *How did extraction change your approach to future unit tests?*
- *Were there responsibilities you intentionally kept inside the service? Why?*
- *What risks do new collaborators introduce (e.g., mapping drift, validation duplication)?*
- *How does SRP relate to the Quality Manifesto's emphasis on technical excellence?*

## 10. Time Estimate

- 60 min — Reading.
- 15 min — Study SRP Smell Detector Framework.
- 30 min — Extract TaskMapper.
- 30 min — Extract TaskBusinessRules.
- 10 min — Optional interface creation.
- 10 min — Update DI registrations.
- 5 min — Verify no behavior change.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~3 hours 15 minutes.

## 11. Additional Resources

### SRP Smell Detector Framework

**Read this section BEFORE extracting classes.**

**Purpose:** Systematic approach to identifying SRP violations before extracting classes.

**Time:** 15 minutes.

---

#### Step 1: Identify Responsibilities

For each method in `TaskService`, answer these questions:

1. **What is this method's primary purpose?**
   - Example: `CreateTaskAsync` - "Creates a new task in the system"

2. **What other concerns does it handle?**
   - Example: `CreateTaskAsync` also:
     - Validates the request
     - Maps request to entity
     - Applies business rules
     - Saves to database
     - Maps entity to DTO
     - Logs the operation

3. **Could this method be split into smaller methods?**
   - If yes, list what each extracted method would do

---

#### Step 2: Responsibility Scoring

For each responsibility identified, score it (1-5):

| Score | Meaning | Example |
|-------|---------|---------|
| 1 | Core responsibility (must stay) | Orchestrating the create flow |
| 2 | Related but separable | Logging the operation |
| 3 | Related but different concern | Validating the request |
| 4 | Unrelated concern | Mapping between layers |
| 5 | Completely unrelated | Business rule validation |

**Scoring Guidelines:**
- **Score 1:** The method's main purpose (orchestration)
- **Score 2-3:** Related concerns that could be extracted but are somewhat related
- **Score 4-5:** Different concerns that should definitely be extracted

---

#### Step 3: Extraction Decision Matrix

Create a table for each method:

| Method | Responsibility | Score | Extract? | Target Class | Reason |
|--------|---------------|-------|----------|--------------|--------|
| CreateTaskAsync | Orchestration | 1 | No | (stays) | Core purpose |
| CreateTaskAsync | Validation | 3 | Yes | TaskValidator | Different concern |
| CreateTaskAsync | Mapping Request→Entity | 4 | Yes | TaskMapper | Different layer |
| CreateTaskAsync | Business Rules | 5 | Yes | TaskBusinessRules | Domain logic |
| CreateTaskAsync | Mapping Entity→DTO | 4 | Yes | TaskMapper | Different layer |
| CreateTaskAsync | Logging | 2 | Maybe | (stays) | Cross-cutting, acceptable |

**Decision Rule:**
- **Score 4-5:** Always extract
- **Score 3:** Extract if it appears in multiple methods (duplicate code)
- **Score 2:** Extract if it's complex or reusable
- **Score 1:** Keep in service

---

#### Step 4: Extraction Impact Prediction

Before extracting, predict the impact:

**For each extraction candidate, answer:**

1. **How many files will change?**
   - Example: Extracting TaskMapper affects:
     - TaskService.cs (remove mapping methods)
     - Program.cs (add DI registration)
     - TaskService tests (update mocks)

2. **Will tests need updates?**
   - Example: Yes - need to mock TaskMapper in service tests

3. **Will DI registration change?**
   - Example: Yes - add `builder.Services.AddScoped<TaskMapper>()`

4. **Will this break existing functionality?**
   - Example: No - just moving code, not changing logic

---

#### Step 5: Extraction Order Planning

**Recommended order (easiest to hardest):**

1. **TaskMapper** (easiest)
   - ✅ No dependencies on other services
   - ✅ Pure data transformation
   - ✅ Easy to test in isolation
   - ✅ Low risk of breaking things

2. **TaskBusinessRules** (medium)
   - ⚠️ May depend on entities
   - ⚠️ May need ISystemClock (if time-based rules)
   - ✅ Still isolated logic
   - ✅ Testable with fake entities

3. **TaskValidator** (if needed)
   - ⚠️ May already exist (FluentValidation)
   - ⚠️ Check if extraction is needed

**Why this order?**
- Start with easiest (builds confidence)
- Each extraction makes the next one easier
- Reduces risk of breaking functionality

---

#### Example: TaskService.CreateTaskAsync Analysis

**Method:** `CreateTaskAsync(CreateTaskRequest request, CancellationToken ct)`

**Responsibilities Identified:**

| Responsibility | Score | Extract? | Target Class |
|----------------|-------|----------|--------------|
| Orchestrate create flow | 1 | No | (stays in service) |
| Validate request | 3 | Yes | TaskValidator (or FluentValidation) |
| Map request to entity | 4 | Yes | TaskMapper |
| Apply business rules | 5 | Yes | TaskBusinessRules |
| Save to repository | 1 | No | (stays - calls repository) |
| Map entity to DTO | 4 | Yes | TaskMapper |
| Log operation | 2 | Maybe | (stays - acceptable) |

**Extraction Plan:**
1. Extract TaskMapper (handles both request→entity and entity→DTO mapping)
2. Extract TaskBusinessRules (handles business rule validation)
3. Keep validation in FluentValidation (already extracted)
4. Keep orchestration, repository calls, and logging in service

**Predicted Impact:**
- Files changed: 4 (TaskService, new TaskMapper, new TaskBusinessRules, Program.cs)
- Tests updated: Yes (mock mapper and rules)
- DI updated: Yes (register 2 new services)
- Risk: Low (pure extraction, no logic changes)

---

#### Checklist: Is This an SRP Violation?

Answer these questions for each method/class:

**For Methods:**
- [ ] Does this method do more than one thing?
- [ ] Could you describe what it does with "and" (e.g., "validates AND maps AND saves")?
- [ ] Does it have multiple levels of abstraction?
- [ ] Would extracting a method make the code clearer?

**For Classes:**
- [ ] Does this class have multiple reasons to change?
- [ ] Could you split it into 2+ focused classes?
- [ ] Do methods in this class have different concerns?
- [ ] Is the class >200 lines?

**If you answered "yes" to 2+ questions, it's likely an SRP violation.**

---

#### When NOT to Extract

**Don't extract if:**
- ❌ The responsibility is trivial (1-2 lines)
- ❌ Extraction would create unnecessary indirection
- ❌ The code is only used in one place (no reuse benefit)
- ❌ The abstraction would be more complex than the code itself

**Example of when NOT to extract:**
```csharp
// ❌ DON'T extract this - too trivial
private string FormatTitle(string title) => title.Trim();

// ✅ DO extract this - complex logic, reusable
private TaskDto MapToDto(TaskEntity entity)
{
    // 15 lines of mapping logic used in 3 places
}
```

---

### Examples

- **[Single Responsibility Example](../Examples/SingleResponsibility.cs)** — Code examples showing SRP violations and fixes.

### External Resources

- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** — Comprehensive overview with Robert C. Martin's original definition.
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** — Practical examples and implementation details.
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** — In-depth analysis of practical applications.
- **[Understanding SOLID: SRP - Dev.to](https://dev.to/ggorantala/solid-single-responsibility-principle-with-examples-h0f)** — Clear explanations with code examples.

### Optional Deep Dives

- **[The Single Responsibility Principle Revisited](https://thevaluable.dev/single-responsibility-principle-revisited/)** — Advanced discussion of SRP implementation in complex domains.