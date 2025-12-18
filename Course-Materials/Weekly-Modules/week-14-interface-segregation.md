# Week 14: Interface Segregation Principle (ISP)

This week, we are focusing on the Interface Segregation Principle (ISP) and how it contributes to writing maintainable, extensible, and testable code. You'll learn to split "fat" interfaces into focused contracts so clients only depend on methods they actually use, aligning with In Time Tec's emphasis on technical excellence.

## 1. Learning Objectives

- Split "fat" interfaces into focused contracts.
- Update implementations and consumers to depend only on what they use.
- Ensure DI configuration honors the new abstractions.
- Understand when interface segregation improves testability and flexibility.

## 2. Reading & Resources (60 min)

- **[Interface Segregation Principle - Wikipedia](https://en.wikipedia.org/wiki/Interface_segregation_principle)** — Original definition and history from Robert C. Martin's work at Xerox (20 min).
- **[ISP in Practice - Dev.to](https://dev.to/paulocappa/solid-i-interface-segregation-principle-isp-385f)** — Comprehensive guide with practical code examples (20 min).
- **[ISP Implementation - ByteHide](https://www.bytehide.com/blog/interface-segregation-principle-in-csharp-solid-principles)** — Detailed examples and best practices in C# (20 min).

## 3. This Week's Work

- Create `ITaskReader` and `ITaskWriter` interfaces (scaffolded with TODOs).
- Update `TaskRepository` to implement both interfaces (while keeping `ITaskRepository` temporarily for backwards compatibility).
- Update one real consumer (`TaskService`) to depend on the appropriate interface(s).
- Register both interfaces in DI pointing to the same underlying concrete implementation.

## 4. Files to Modify

- `TaskFlowAPI/Repositories/Interfaces/ITaskReader.cs` (complete the scaffold).
- `TaskFlowAPI/Repositories/Interfaces/ITaskWriter.cs` (complete the scaffold).
- `TaskFlowAPI/Repositories/TaskRepository.cs` (implement both new interfaces).
- `TaskFlowAPI/Services/Tasks/TaskService.cs` (update constructor to use split interfaces).
- `TaskFlowAPI/Program.cs` (register both interfaces).
- `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` (read-only reference; do not delete this week).
- This file (`Course-Materials/Weekly-Modules/week-14-interface-segregation.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-14-submission`.

2. **Read About ISP First (60 min)**
   
   Pay special attention to:
   - Why forcing clients to depend on unused methods creates problems.
   - The difference between "interface pollution" and "focused interfaces".
   - How ISP improves testability (mock only what you need).

3. **Review the ISP Violation (5 min)**
   
   Open `TaskFlowAPI/Repositories/Interfaces/ITaskRepository.cs` and read the TODO Week 14 comments explaining why this interface violates ISP.

4. **Create ITaskReader Interface (10 min)**
   
   Open `TaskFlowAPI/Repositories/Interfaces/ITaskReader.cs` (scaffolded with TODOs).
   
   **Your job:** Add method signatures for read-only operations:
   - `GetAllAsync`
   - `GetByIdAsync`
   
   Complete all TODO Week 14 items in the file.

5. **Create ITaskWriter Interface (10 min)**
   
   Open `TaskFlowAPI/Repositories/Interfaces/ITaskWriter.cs` (scaffolded with TODOs).
   
   **Your job:** Add method signatures for write operations:
   - `CreateAsync`
   - `UpdateAsync`
   - `DeleteAsync`
   
   Complete all TODO Week 14 items in the file.

6. **Update TaskRepository to Implement Both (10-15 min)**
   
   Open `TaskFlowAPI/Repositories/TaskRepository.cs` and update the class declaration to implement both new interfaces.
   Your method bodies should not need to change — you're just changing the abstractions it implements.
   
   **Important:** Do not delete `ITaskRepository` this week. We are doing a safe migration.

7. **Register Both Interfaces in DI (15 min)**
   
   Open `TaskFlowAPI/Program.cs` and register both interfaces to resolve to the same underlying `TaskRepository` instance.
   
   (Tip: register the concrete type once, then map both interfaces back to that same instance.)

8. **Update TaskService to Use Split Interfaces (10-15 min)**
   
   Open `TaskFlowAPI/Services/Tasks/TaskService.cs`.
   Update the constructor to depend on `ITaskReader` for reads and `ITaskWriter` for writes.
   Then update call sites accordingly.

9. **Update tests/fakes (optional, 10-15 min)**
   
   If your Week 13 `FakeTaskRepository` exists, update it to implement `ITaskReader` and `ITaskWriter`.

10. Run build and tests:
    ```bash
    dotnet build TaskFlowAPI.sln
    dotnet test TaskFlowAPI.sln
    ```

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- `ITaskReader` and `ITaskWriter` interfaces exist with appropriate methods.
- `TaskRepository` implements both interfaces.
- DI container resolves both interfaces to the same underlying `TaskRepository` instance.
- `TaskService` depends only on the interface(s) it needs.
- Tests compile and pass with new abstractions.
- Week 14 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

1. Create a new branch for your weekly work (e.g., `git checkout -b week-14-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "refactor: split ITaskRepository per ISP"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Before and After: Look at TaskService's constructor before and after your changes. Which interface(s) does it depend on now? Why is this better than depending on ITaskRepository?*
- *The DI Pattern: In Program.cs, you registered the same TaskRepository class for both ITaskReader and ITaskWriter. Does this mean TaskService gets two separate repository objects, or the same one? How do you know?*

### Discussion Prep:

- *What surprised you most when splitting the interfaces? Was it easier or harder than you expected?*
- *Look at TaskService now. If you wanted to test ONLY the read operations, what would you need to mock? Compare that to before the split.*
- *“ReportsController depends on ITaskService. If we had a read-only service, could it depend only on ITaskReader indirectly?”*

## 10. Time Estimate

- 60 min — Reading.
- 5 min — Review ISP violation.
- 10 min — Create ITaskReader interface.
- 10 min — Create ITaskWriter interface.
- 10-15 min — Update TaskRepository.
- 15 min — Register both interfaces in DI.
- 10-15 min — Update TaskService constructor.
- 10-15 min — Optional: Update tests and fakes.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 30 minutes.

## 11. Additional Resources

### Examples

- **[Interface Segregation Example](../Examples/InterfaceSegregation.cs)** — Code examples showing ISP violations and fixes.

### External Resources

- **[Interface Segregation Principle - Wikipedia](https://en.wikipedia.org/wiki/Interface_segregation_principle)** — Comprehensive overview and history.
- **[ISP in Practice - Dev.to](https://dev.to/paulocappa/solid-i-interface-segregation-principle-isp-385f)** — Practical implementations.
- **[CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)** — Command Query Responsibility Segregation (advanced ISP application).

### Optional Deep Dives

- **[ISP Design Patterns - OODesign](https://www.oodesign.com/interface-segregation-principle)** — Advanced patterns and implementation strategies.