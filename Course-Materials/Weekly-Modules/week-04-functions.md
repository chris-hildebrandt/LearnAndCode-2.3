# Week 4: Functions

This week we will practice refactoring code with a focus on improving function structure and design. We will apply the principles of small, well-named functions with clear responsibilities. This module is based on **Clean Code Chapter 3: Functions**.

## 1. Learning Objectives

- Identify multiple responsibilities within a single large function.
- Extract logical blocks of code into small, private helper functions with clear, descriptive names.
- Apply the "Stepdown Rule," where a function's body reads like a high-level summary.
- Design small, intention-revealing controller actions.
- Extend `TaskFlow` endpoints (update/delete) while keeping flows cohesive.

## 2. Reading & Resources (50 min)

- **Clean Code – Chapter 3: Functions (pp. 35-58)**.
  - Focus on small functions, descriptive names, and minimizing arguments.

## 3. This Week's Work

- Refactor the `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs`. This method works, but it was written quickly and violates clean function principles. It's long, hard to read, and mixes different levels of abstraction.
- Extend `ITaskService` with `UpdateTaskAsync` and `DeleteTaskAsync` signatures using expressive parameter names.
- Implement controller actions:
  - `PUT /api/tasks/{id}` consumes `UpdateTaskRequest`, returns `204 No Content`.
  - `DELETE /api/tasks/{id}` returns `204 No Content` (idempotent).
- Leave service/repository implementations as `NotImplementedException` (students will implement in Week 9) but ensure controller flow reads cleanly.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/ReportsController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Controllers/TasksController.cs`
- This file (`Course-Materials/Weekly-Modules/week-04-functions.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-04-submission`.
2. Read the `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs`.
3. Identify logical blocks that can be extracted into private helper methods. Look for code with comments, nested logic (≥2 levels), or blocks you can describe in 3-4 words.
4. Extract methods bottom-up to avoid compiler errors:
   - First: Simple operations with no dependencies (count tasks, filter by status).
   - Then: Mid-level operations that might call the simple ones (calculate percentage).
   - Finally: Coordinator methods that assemble results.
5. Refactor until `GenerateProjectSummaryReport` is under 15 lines and reads like a high-level summary.
6. Add `UpdateTaskAsync` and `DeleteTaskAsync` method signatures to `ITaskService`.
7. Implement both methods in `TaskService` with `NotImplementedException`.
8. Add `PUT /api/tasks/{id}` and `DELETE /api/tasks/{id}` controller actions in `TasksController`.
9. Run `dotnet build TaskFlowAPI.sln` to verify no compilation errors.
10. Compare your refactor with `Course-Materials/Examples/FunctionRefactor.cs`. Note differences in your PR description.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual testing (optional):
```bash
dotnet run --project TaskFlowAPI
```

- Test the refactored report endpoint: `GET https://localhost:5001/api/Reports/project-summary/1`
- Verify new endpoints appear in Swagger: `PUT /api/tasks/{id}` and `DELETE /api/tasks/{id}`
- Calling the new endpoints will return 500 errors (NotImplementedException) - this is expected until Week 9.

## 7. Success Criteria

- `GenerateProjectSummaryReport` method is no more than 15 lines long.
- At least five private helper methods extracted with clear, descriptive names.
- The refactored method acts as a coordinator and follows the Stepdown Rule.
- The API builds successfully and the `/api/Reports/project-summary/{projectId}` endpoint functions as before.
- UPDATE/DELETE endpoints added to controller with proper HTTP attributes.
- Service interface extended with method signatures.
- Service methods throw `NotImplementedException` (will implement Week 9).

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-04-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 4 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Function Size: Describe the most significant function you extracted. Why did you choose to separate it? What naming strategy did you use to make its purpose clear?*
- *Readability: How did applying the "Stepdown Rule" change the way you read and understand the `GenerateProjectSummaryReport` function?*
- *Testability: How might these smaller functions be easier to unit test than the original, large function?*
- *Abstraction Levels: Did you find any places where you mixed high-level and low-level operations in the same method? How did you resolve this?*

### Discussion Prep:

- *Be prepared to share one of the helper functions you created and justify its name and purpose.*
- *Was there any part of the original function that was difficult to extract? Why?*
- *How did your refactor compare to the example? What did you do differently and why?*
- *Which extraction decision are you most uncertain about? (Bring for group feedback.)*
- *Is the 'Functions should do one thing' rule always the best rule to make code readable? When might you consciously choose to disobey this rule?*

## 10. Time Estimate

- 50 min – Reading.
- 25 min – Refactoring assignment.
- 25 min – Adding UPDATE/DELETE endpoints (interface and controller stubs).
- 20 min – Journal and discussion prep.
- 15 min – Test, create PR, and merge.

**Total:** ~2 hours 15 minutes.

## 11. Additional Resources

### Examples

- **[Function Refactor Example](../Examples/FunctionRefactor.cs)** – Review after completing your refactor.
- **[Optional: Systematic Method Extraction Guide](../Examples/MethodExtractionFramework.md)** – For students who want a structured approach.

### External Resources

- **[Clean Code Functions Summary](https://dipta007.com/clean-code-functions/)**
- **[The Art of Writing Small and Plain Functions](https://dmitripavlutin.com/the-art-of-writing-small-and-plain-functions/)**

### Quick Refactoring Tips

**When to Extract:**
- Code block has a comment explaining what it does → Extract with that comment as the method name.
- Logic is nested ≥2 levels → Extract to reduce complexity.
- You can describe it in 3-4 words → That's your method name.

**When NOT to Extract:**
- Method would be ≤2 lines with no nested logic.
- Only used once AND doesn't improve clarity.
- Name would be longer than the code itself.

**Extraction Order Matters:**

Extract from bottom-up to avoid compiler errors:
1. Leaf operations (no dependencies): `CountCompleted()`, `CountOverdue()`.
2. Mid-level operations: `CalculatePercentage()`.
3. Coordinator: `AssembleSummary()`.