# Week 11: Single Responsibility Principle (SRP)

This week, we are focusing on the Single Responsibility Principle (SRP) and how it contributes to writing maintainable, extensible, and testable code, aligning with In Time Tec's emphasis on technical excellence. We will also explore the relationship between the SRP and the Quality Manifesto's focus on technical excellence.

## 1. Learning Objectives

- Identify and eliminate SRP violations within `TaskService`.
- Extract mapping, validation, and business-rule logic into focused classes.
- Update dependency injection to wire new components.

## 2. Reading & Resources (70 min)

- **Clean Code Chapter 3 (Functions)** – Revisit the “do one thing” mantra.
- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** – Original definition and context.
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** – Practical examples and pitfalls.
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** – Strategies for untangling mixed responsibilities.
- **[Understanding SOLID: SRP - Dev.to](https://dev.to/ggorantala/solid-single-responsibility-principle-with-examples-h0f)** – Additional code samples and anti-patterns.
- Optional: **[The Single Responsibility Principle Revisited](https://thevaluable.dev/single-responsibility-principle-revisited/)** – Advanced heuristics for complex domains.

## 3. This Week’s Work

**REVISED: Phased Extraction with SRP Smell Detection**

- **Step 0:** Use SRP Smell Detector to identify violations (see Section 11 below) - 15 min
- **Step 1:** Extract `TaskMapper` first (isolated, no side effects) - 30 min
- **Step 2:** Extract `TaskBusinessRules` second (depends on entities) - 30 min

Additional (optional) step:

- Add `ITaskBusinessRules` interface

   - Rationale: to keep business-rule dependencies mockable and aligned with full interface-based DI across the codebase, optionally extract an interface `ITaskBusinessRules` and update DI registration. This is a lightweight addition to Week 11 and will make later TDD and mocking (Week 17) simpler.
   - Files to Modify: create `TaskFlowAPI/Services/Tasks/Rules/ITaskBusinessRules.cs` and update `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` to implement the interface; register in `Program.cs`.
- **Step 3:** Verify FluentValidation validators work via DI (no extraction needed) - 10 min
- Update DI registrations and verify no behavior change

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskMapper.cs` (create)
- `TaskFlowAPI/Services/Tasks/Validation/TaskValidator.cs` (create)
- `TaskFlowAPI/Services/Tasks/Rules/TaskBusinessRules.cs` (create)
- `TaskFlowAPI/Program.cs` (add registrations)
- `TaskFlowAPI.Tests` examples if needed for namespace updates
- This file (`Course-Materials/Weekly-Modules/week-11-single-responsibility.md`) – append your journal and discussion prompt responses.
## 5. Step-by-Step Instructions

### Step 0: SRP Smell Detection (15 minutes - NEW)

**Before extracting anything, identify WHAT to extract using Section 11 below**

1. Branch `week-11/<your-name>`.
2. **NEW:** Complete SRP Smell Detector checklist (Section 11)
3. **NEW:** Create `docs/week-11-extraction-plan.md` with predicted impact matrix

---

### Step 1: Extract TaskMapper (30 minutes)

**Why first:** No side effects, pure data transformation

4. Create `Services/Tasks/TaskMapper.cs`:
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

5. Update `TaskService` to use mapper:
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

6. Remove old mapping methods from TaskService

**Checkpoint:**
- [ ] TaskService no longer has mapping methods?
- [ ] All MapToDto calls use _mapper?
- [ ] Does service still compile?

---

### Step 2: Extract TaskBusinessRules (30 minutes)

**Why second:** May depend on entities, but still isolated logic

7. Create `Services/Tasks/Rules/TaskBusinessRules.cs`:
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

8. Update `TaskService`:
   ```csharp
   private readonly TaskBusinessRules _businessRules;
   
   // In Complete() method:
   _businessRules.ValidateCanComplete(task);
   ```

---

### Step 3: Update DI Registrations (10 minutes)

9. Register in `Program.cs`:
   ```csharp
   builder.Services.AddScoped<TaskMapper>();
   builder.Services.AddScoped<TaskBusinessRules>();
   ```

10. Build and test: `dotnet build && dotnet test`

---

### Step 4: Verify No Behavior Change (5 minutes)

11. Manual test via Swagger - verify all endpoints work as before
6. Update DI in `Program.cs` to register mapper, business rules, and FluentValidation validators.
7. Ensure service uses new collaborators; logging remains inside service.
8. Build/tests.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual `GET`/`POST` to verify behavior unchanged.

## 7. Success Criteria

- `TaskService` focused on orchestration only (≤150 lines ideally).
- Mapper/business rules classes contain clear, single responsibilities.
- Dependency injection updated without circular references.
- Build/tests succeed.

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-11-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 11 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Responsibility Audit:* Capture the smell or metric that convinced you a new class was warranted.
- *Dependency Impact:* Note any DI registration changes and how they affect testability.

### Discussion Prep:
- *What metric indicated `TaskService` was doing too much?*
- *How did extraction change your approach to future unit tests?*
- *Were there responsibilities you intentionally kept inside the service? Why?*
- *What risks do new collaborators introduce (e.g., mapping drift, validation duplication)?*

## 10. Time Estimate

- 70 min – Reading
- 10 min – Identify responsibilities + plan.
- 45 min – Extract classes + DI updates.
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~140 minutes.

## 11. SRP Smell Detector Framework (NEW)

**Purpose:** Systematic approach to identifying SRP violations before extracting classes

**Time:** 15 minutes

### Step 1: Identify Responsibilities

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

### Step 2: Responsibility Scoring

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

### Step 3: Extraction Decision Matrix

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

### Step 4: Extraction Impact Prediction

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

### Step 5: Extraction Order Planning

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

### Example: TaskService.CreateTaskAsync Analysis

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
- Files changed: 3 (TaskService, new TaskMapper, new TaskBusinessRules, Program.cs)
- Tests updated: Yes (mock mapper and rules)
- DI updated: Yes (register 2 new services)
- Risk: Low (pure extraction, no logic changes)

### Checklist: Is This an SRP Violation?

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

### When NOT to Extract

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

## 12. Additional Resources

- **[Single Responsibility Example](../Examples/SingleResponsibility.cs)**
- **[Single Responsibility Principle - Wikipedia](https://en.wikipedia.org/wiki/Single-responsibility_principle)** - Comprehensive overview of SRP with Robert C. Martin's original definition.
- **[Single Responsibility in SOLID Design - GeeksforGeeks](https://www.geeksforgeeks.org/single-responsibility-in-solid-design-principle/)** - Practical examples and implementation details.
- **[Making the SRP Practical - HackerNoon](https://hackernoon.com/making-the-single-responsibility-principle-practical)** - In-depth analysis of practical applications.
- **[Understanding SOLID: SRP - Dev.to](https://dev.to/ggorantala/solid-single-responsibility-principle-with-examples-h0f)** - Clear explanations with code examples.
- **[The Single Responsibility Principle Revisited](https://thevaluable.dev/single-responsibility-principle-revisited/)** - Advanced discussion of SRP implementation.
