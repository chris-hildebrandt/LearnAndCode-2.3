# Week 7: Objects & Data Structures

This week, we will focus on the fundamental difference between **Objects** and **Data Structures**, aligning with In Time Tec's commitment to modular, maintainable code. We will transition our core entity from a "bag of data" into a true object that protects its own integrity. This module is based on **Clean Code Chapter 6: Objects and Data Structures**.

## 1. Learning Objectives

- Distinguish between **Objects** (expose behavior, hide data) and **Data Structures** (expose data, have no behavior).
- Apply encapsulation to protect class invariants (e.g., "A task must always have a title").
- Implement **Factory Methods** to control object creation.
- Refactor dependent services (TaskService) to respect new encapsulation rules.

## 2. Reading & Resources (45 min)

- **Clean Code Chapter 6: Objects and Data Structures (pp. 93-101)**.
  - Focus on "Data Abstraction" and "Data/Object Anti-Symmetry."
  - *Key Takeaway:* You shouldn't blindly add getters and setters to everything. Objects should hide their data.
- **[Objects vs Data Structures](https://medium.com/hackernoon/objects-vs-data-structures-e380b962c1d2)** — A quick conceptual refresher.

## 3. This Week's Work

- Refactor TaskEntity to be a true **Object**:
  - Protect the Title invariant (cannot be empty).
  - Add a static factory method for validated creation.
  - Add a domain behavior method (Complete()).
- Refactor TaskService to use the new factory method instead of raw constructors.
- Update seed data in TaskFlowDbContext to use the factory.

## 4. Files to Modify

- `TaskFlowAPI/Entities/TaskEntity.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs` (Fix compilation errors).
- `TaskFlowAPI/Data/TaskFlowDbContext.cs` (Update seed data).
- This file (`Course-Materials/Weekly-Modules/week-07-objects-encapsulation.md`) — append your journal.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-07-submission`.
2. Review the encapsulation example file: `Course-Materials/Examples/Week-07-Encapsulation-Guide.md` (10 min).
3. **Step 3: Enforce Immutability**
   - Open `TaskFlowAPI/Entities/TaskEntity.cs`.
   - Change the properties to use `init` or `private set` where appropriate to prevent modification after creation without a method call.
   ```csharp
   public string Title { get; private set; } = string.Empty; // Changed from get; set;
   ```
4. **Step 4: The Private Constructor**
   - Add a private constructor that enforces your validation rules.
   ```csharp
   private TaskEntity(string title, int projectId)
   {
       if (string.IsNullOrWhiteSpace(title))
           throw new DomainValidationException("Title cannot be empty"); // Use your custom exception!

       Title = title;
       ProjectId = projectId;
       CreatedAt = DateTime.UtcNow;
       IsCompleted = false;
   }
   ```
5. **Step 5: The EF Core "Backdoor"**
   - Entity Framework requires a parameterless constructor to read from the database. Add one, but keep it private.
   ```csharp
   // Required by EF Core
   private TaskEntity() { }
   ```
6. **Step 6: The Factory Method**
   - Add a public static method to allow creation. This is now the ONLY way to make a new Task.
   ```csharp
   public static TaskEntity Create(string title, int projectId)
   {
       return new TaskEntity(title, projectId);
   }
   ```
7. **Step 7: Domain Behavior**
   - Add the `Complete()` method. This replaces the need for public setters on `IsCompleted`.
   ```csharp
   public void Complete()
   {
       if (IsCompleted) return; // Idempotent

       IsCompleted = true;
       CompletedAt = DateTime.UtcNow;
   }
   ```
8. **Step 8: Fix TaskService.cs (CRITICAL)**
   - Try to build (`dotnet build`). It will fail!
   - The `TaskService.MapToEntity` method tries to use `new TaskEntity { ... }`, which is no longer allowed.
   - Refactor `MapToEntity` to use your new factory:
   ```csharp
   internal TaskEntity MapToEntity(CreateTaskRequest request)
   {
       // 1. Create using the factory (enforces Title/ProjectId rules)
       var entity = TaskEntity.Create(request.Title!, request.ProjectId ?? 0);

       // 2. Set optional properties afterwards
       entity.Description = request.Description;
       entity.Priority = request.Priority ?? 0;
       entity.DueDate = request.DueDate;

       return entity;
   }
   ```
   *Note: You may need to add setters for Description/Priority if you made them private in Step 3, or pass them into the factory/constructor if you want them strictly immutable.*
9. **Step 9: Fix Seed Data (TaskFlowDbContext.cs)**
   - The seed data also uses `new TaskEntity`. Update it to use the factory.
   - Important: The factory doesn't allow setting Id (because usually databases generate IDs), but for Seeding, we MUST specify the ID.
   ```csharp
   // Old: new TaskEntity { Id = 1, Title = "..." }
   // New:
   var t1 = TaskEntity.Create("Wireframe onboarding flow", 1);
   t1.Id = 1; // Explicitly set ID for seeding
   t1.Priority = 1;
   t1.Description = "Starter task...";

   modelBuilder.Entity<TaskEntity>().HasData(t1, ...);
   ```
10. **Build and Verify:**
    ```bash
    dotnet build TaskFlowAPI.sln
    dotnet run --project TaskFlowAPI
    ```

## 6. How to Test

```bash
dotnet test TaskFlowAPI.sln
```

- **Expectation:** Tests should pass.
- **Manual Test:** Try to create a task with an empty title using Swagger (POST /api/tasks).
  - *Before Week 7:* It might have created a bad record.
  - *After Week 7:* It should throw DomainValidationException (returning 400 Bad Request if your middleware from Week 10 is ready, or 500 if not).

## 7. Success Criteria

- TaskEntity has NO public constructors.
- TaskEntity.Title has a private setter.
- TaskService uses TaskEntity.Create() instead of new.
- TaskFlowDbContext seed data compiles and runs.
- You can explain why TaskDto (Data Structure) has public setters but TaskEntity (Object) does not.
- Week 7 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-07-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 7 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Objects vs. Data Structures: Look at TaskDto.cs and your new TaskEntity.cs. Which one is a Data Structure? Which one is an Object? Why?*
- *The "Hybrid" Trap: Clean Code warns against "Hybrids" (half object, half data structure). Did we create a hybrid? Why or why not?*
- *EF Core Friction: How did the requirement to use a private parameterless constructor make you feel about the tool (EF Core) dictating your design?*

### Discussion Prep:

- *Why do we allow TaskDto to have public setters if we are so strict about TaskEntity?*
- *How does this change affect how we write Unit Tests for TaskEntity?*
- *Share the specific compiler error you hit in TaskService and how you fixed it.*

## 10. Time Estimate

- 45 min — Reading (Chapter 6).
- 30 min — Refactoring TaskEntity (The Object).
- 20 min — Fixing TaskService and DbContext.
- 15 min — Build, Test, Verify.
- 20 min — Journal and discussion prep.

**Total:** ~2 hours 10 minutes.

## 11. Additional Resources

### Examples

- **[Week 7 Encapsulation Guide](../Examples/Week-07-Encapsulation-Guide.md)** — Review before starting your refactor.

### External Resources

- **[C# Init Only Setters](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/init)**
- **[Validation in Domain Models](https://enterprisecraftsmanship.com/posts/validation-in-domain-model-attribute-based-vs-always-valid/)**
- **[Objects vs Data Structures](https://medium.com/hackernoon/objects-vs-data-structures-e380b962c1d2)**