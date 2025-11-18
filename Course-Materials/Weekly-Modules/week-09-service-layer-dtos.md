# Week 9: Service Layer & DTOs

## 1. Learning Objectives
- Implement business logic in the service layer using repository abstractions.
- Map between entities and DTOs with intentional helpers.
- Handle not-found and validation errors consistently (pre-FluentValidation).

## 2. Reading & Resources (60 min)
- **Clean Code Chapter 6: Objects and Data Structures (pp. 89-110)** – Balance data exposure with behaviour.
- **Clean Code Chapter 11 (selected sections)** – Separate policy from implementation.
- **[Data Structures: A Practical Guide](https://www.freecodecamp.org/news/the-top-data-structures-you-should-know-for-your-next-coding-interview-36af0831f5e3/)** – Conceptual grounding.
- Optional: **Microsoft Docs: DTO pattern** – When and how to use DTOs in ASP.NET Core.
- Optional: `MapStruct`/`AutoMapper` docs for ideas on extraction (use judiciously to avoid premature abstraction).

## 3. This Week’s Work
**REVISED: Guided Implementation with Decision Framework**

- **Step 0:** Understand DTO vs Entity distinctions (see Section 11 below) - 15 min
- Implement `TaskService` methods (`GetAll`, `Get`, `Add`) using repository + mapping helpers
- Add temporary guard clauses for create/update until Week 10 validation lands
- Return `TaskDto` results to controllers
- Use checkpoints to verify understanding after each method

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs` (ensure async naming after Week 2/3 refactor)
- Optional: add/update mapping helpers if you extracted them.
- This file (`Course-Materials/Weekly-Modules/week-09-service-layer-dtos.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### Step 0: Understand DTO vs Entity (15 minutes - NEW)

**Read Section 11 below** before coding - it explains WHEN to use DTO vs Entity

---

### Step 1: Implement GetAll (20 minutes)

1. Branch `week-09/<your-name>`.

2. **Implement GetAllAsync in TaskService:**
   ```csharp
   public async Task<List<TaskDto>> GetAllAsync(CancellationToken cancellationToken = default)
   {
       // 1. Fetch from repository
       var entities = await _taskRepository.GetAllAsync(cancellationToken);
       
       // 2. Map to DTOs
       var dtos = entities.Select(e => MapToDto(e)).ToList();
       
       // 3. Return
       return dtos;
   }
   ```

3. **Checkpoint - Ask yourself:**
   - [ ] Did I call repository's async method?
   - [ ] Did I pass cancellationToken through?
   - [ ] Did I map ALL entities to DTOs?
   - [ ] Why return DTO instead of entity? (Hint: See Section 11 decision framework)

---

### Step 2: Implement Get (Single Task) (20 minutes)

4. **Implement GetAsync in TaskService:**
   ```csharp
   public async Task<TaskDto?> GetAsync(int id, CancellationToken cancellationToken = default)
   {
       // 1. Fetch from repository
       var entity = await _taskRepository.GetByIdAsync(id, cancellationToken);
       
       // 2. Handle not found
       if (entity == null)
       {
           _logger.LogWarning("Task {TaskId} not found", id);
           return null;  // Controller will return 404
       }
       
       // 3. Map to DTO and return
       return MapToDto(entity);
   }
   ```

5. **Checkpoint - Ask yourself:**
   - [ ] What happens if id doesn't exist? (Should return null, not throw)
   - [ ] Did I log the warning?
   - [ ] Why check null HERE instead of in repository? (Hint: Service handles business logic)

---

### Step 3: Implement Add (Create Task) (25 minutes)

6. **Implement AddAsync in TaskService:**
   ```csharp
   public async Task<TaskDto> AddAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
   {
       // 1. Temporary validation (until Week 10)
       if (string.IsNullOrWhiteSpace(request.Title))
           throw new ArgumentException("Title is required", nameof(request.Title));
       
       if (request.ProjectId <= 0)
           throw new ArgumentException("ProjectId must be positive", nameof(request.ProjectId));
       
       // 2. Map request to entity (use Week 7 factory!)
       var entity = TaskEntity.Create(request.Title, request.ProjectId);
       entity.Description = request.Description;
       entity.DueDate = request.DueDate;
       entity.Priority = request.Priority;
       
       // 3. Save via repository
       var savedEntity = await _taskRepository.CreateAsync(entity, cancellationToken);
       
       // 4. Log success
       _logger.LogInformation("Created task {TaskId}: {Title}", savedEntity.Id, savedEntity.Title);
       
       // 5. Return DTO
       return MapToDto(savedEntity);
   }
   ```

7. **Checkpoint - Ask yourself:**
   - [ ] Why validate here instead of controller? (Service = business logic layer)
   - [ ] Why use `TaskEntity.Create()` from Week 7? (Factory enforces invariants)
   - [ ] What information is in the log? (Id and Title for traceability)
   - [ ] Why return DTO instead of entity? (API boundary - don't expose domain internals)

---

### Step 4: Update Mapping Helpers (10 minutes)

8. **Ensure MapToDto helper includes all properties:**
   ```csharp
   private TaskDto MapToDto(TaskEntity entity)
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
           ProjectName = entity.Project?.Name  // Null-safe navigation
       };
   }
   ```

9. **Checkpoint - Ask yourself:**
   - [ ] Did I handle `Project` navigation property safely? (null check with `?.`)
   - [ ] Are all Entity properties represented in DTO?
   - [ ] Why is DTO flat (no nested Project object)? (Hint: API simplicity)

10. Run build/tests: `dotnet build && dotnet test`

## 6. How to Test
```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
  - Optional: run API and test `GET /api/tasks` and `POST /api/tasks` via Swagger (use sample payload). Expect validation to be basic; more in Week 10.

## 7. Success Criteria
- No remaining `NotImplementedException` in `TaskService`.
- Controller endpoints return data instead of throwing.
- Logging statements added for create + not-found scenarios.
- Build/tests succeed; manual GET returns seeded tasks.

## 8. Submission Process
- Commit `Week 09 – task service implementation`.
- PR summary must include sample JSON payload used for manual testing.
- Weekly issue attaches Swagger screenshot showing GET response.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Mapping Choices:* Note one DTO vs entity mapping decision you made and why it improves clarity.

*Guard Rails:* Which inline validation or logging checks did you leave in place for now, and what is your plan for Week 10?

Discussion Prep:
- How does the service shield controllers from repository details?
- What validation gaps remain before Week 10?
- How will you extract mapper/validator in Week 11 without breaking consumers?
- Where might AutoMapper or manual mappers introduce risk in future refactors?
- What is the relationship between organizing objects and data structures and the Quality Manifesto's emphasis on technical excellence?

## 10. Time Estimate

- 60 min – Reading.
- **15 min – DTO vs Entity Decision Framework (NEW)**
- 20 min – Step 1: Implement GetAll with checkpoint
- 20 min – Step 2: Implement Get with checkpoint
- 25 min – Step 3: Implement Add with checkpoint
- 10 min – Step 4: Update mapping helpers
- 10 min – Build/test + manual verification
- 20 min – Journal + discussion.
**Total:** ~3 hours (more realistic for juniors, includes reflection time).

---

## 11. DTO vs Entity Decision Framework (NEW - Read BEFORE Coding)

**Why do we have BOTH TaskDto AND TaskEntity?**

This is one of the most confusing patterns for juniors. Let's clarify:

### The Problem Without DTOs

**If we return entities directly to API:**

```csharp
// ❌ BAD: Returning entity from controller
[HttpGet]
public async Task<TaskEntity> Get(int id)
{
    return await _taskRepository.GetByIdAsync(id);  // Directly expose domain entity
}
```

**Problems:**
1. **Exposes internal structure** - Clients see database columns, navigation properties
2. **Coupling** - API changes whenever entity changes
3. **Over-fetching** - Clients get ALL properties even if they don't need them
4. **Security risk** - Might expose sensitive fields (passwords, internal IDs)
5. **Circular references** - Entity.Project.Tasks.Project... (JSON serialization breaks!)

---

### The DTO Solution

**DTO = Data Transfer Object**

**Purpose:** Shape data specifically for API consumers (presentation layer)

| Aspect | TaskEntity (Domain) | TaskDto (API) |
|--------|---------------------|---------------|
| **Purpose** | Business logic + persistence | Data transfer to client |
| **Properties** | All domain properties + navigation | Only what API needs |
| **Behaviors** | Complete(), Reopen(), etc. | None (plain data) |
| **Mutability** | Controlled via methods | Immutable (init setters) |
| **Navigation props** | Entity.Project (EF Core) | ProjectId + ProjectName (flat) |
| **Used by** | Service, Repository | Controller, API response |

---

### Decision Framework: When to Use Each

**Use Entity when:**
- ✅ Inside service layer (business logic)
- ✅ Inside repository layer (database operations)
- ✅ When you need to call behaviors (Complete(), ChangePriority())
- ✅ When you need navigation properties (task.Project.Name)

**Use DTO when:**
- ✅ Returning from controller action (API response)
- ✅ Accepting input from controller (CreateTaskRequest)
- ✅ When you want to hide internal structure
- ✅ When you need different shapes for different endpoints

---

### Example: Why Separate?

**Scenario:** API consumers only need task summary (not full details)

**Entity (has everything):**
```csharp
public class TaskEntity
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; set; }  // Long text
    public int Priority { get; set; }
    public DateTime? DueDate { get; set; }
    public bool IsCompleted { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public DateTime CreatedAt { get; init; }
    public int ProjectId { get; init; }
    public ProjectEntity Project { get; set; }  // Navigation property
    // ... more internal fields
}
```

**DTO (tailored for API):**
```csharp
public class TaskSummaryDto  // Different shape for list endpoint
{
    public int Id { get; init; }
    public string Title { get; init; }
    public int Priority { get; init; }
    public bool IsCompleted { get; init; }
    // Description omitted - too long for list view
    // CompletedAt omitted - not needed in summary
    // CreatedAt omitted - not needed in summary
}
```

**Benefit:** List endpoint is smaller/faster (no Description, CompletedAt, CreatedAt in payload)

---

### Common Questions

**Q: Isn't this duplication? Why not just return Entity?**  
**A:** It's intentional separation, not duplication. Entity is for domain logic, DTO is for API shape. They can diverge over time.

**Q: Do I ALWAYS need a DTO?**  
**A:** For APIs, yes. For internal service-to-service calls within same app, maybe not.

**Q: Can DTO have computed properties Entity doesn't have?**  
**A:** Yes! Example: `DaysUntilDue` = `(DueDate - DateTime.UtcNow).Days` (calculated, not stored)

**Q: Can I have multiple DTOs for same entity?**  
**A:** Yes! `TaskSummaryDto` for lists, `TaskDetailDto` for single-item view, `CreateTaskRequest` for input.

---

### Week 9 Checkpoint

After implementing service methods, answer:

1. **Mapping Direction:**
   - Entity → DTO: In which method? (Hint: GetAll, Get - reading data)
   - Request → Entity: In which method? (Hint: Add - creating entity)

2. **Why Flatten Project?**
   - Entity has: `Project` navigation (full ProjectEntity object)
   - DTO has: `ProjectId` + `ProjectName` (two simple properties)
   - Why flatten? (Hint: Avoid circular references, simplify API)

3. **Your Prediction:**
   - Week 11 (SRP) will likely extract mapping into separate class
   - Why? (Hint: TaskService has multiple responsibilities: business logic + mapping)

**Deliverable:** Add answers to Section 9 (Journal)

---

## 12. Additional Resources
- **[Objects vs Data Structures](https://medium.com/hackernoon/objects-vs-data-structures-e380b962c1d2)**
- **[Data Structures: A Practical Guide](https://www.freecodecamp.org/news/the-top-data-structures-you-should-know-for-your-next-coding-interview-36af0831f5e3/)**
- **[Stackoverflow: Whats the difference between objects and data structures?](https://stackoverflow.com/questions/23406307/whats-the-difference-between-objects-and-data-structures)**
- **[DTO Pattern Explained](https://martinfowler.com/eaaCatalog/dataTransferObject.html)** - Martin Fowler
