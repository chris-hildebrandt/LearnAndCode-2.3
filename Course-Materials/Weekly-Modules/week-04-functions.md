# Week 4: Functions

This week we will practice refactoring code with a focus on improving function names and structure. The purpose of this example is to demonstrate the positive impact of well-named and well-structured functions on code readability and maintainability. This module is based on **Clean Code Chapter 3: Functions**.

## 1. Learning Objectives

- **Identify** multiple responsibilities within a single large function.
- **Extract** logical blocks of code into small, private helper functions with clear, descriptive names.
- **Apply** the "Stepdown Rule," where a function's body reads like a high-level summary, and the details are in the functions it calls.
- **Refactor** a primary function to be a simple coordinator of calls to these new helper functions.
- Design small, intention-revealing controller actions.
- Extend `TaskFlow` endpoints (update/delete) while keeping flows cohesive.

## 2. Reading & Resources (60 min)

- **Clean Code – Chapter 3: Functions (pp. 35-58)**.
  - Focus on small functions, descriptive names, and minimizing arguments.

## 3. This Week’s Work

This week, a new feature request came in: "As a project manager, I need a summary report for a project so I can quickly assess its status."

A new `ReportsController.cs` has been added to the project with a `GenerateProjectSummaryReport` method. This method works, but it was written quickly and violates the principles of clean functions. It's long, hard to read, and mixes different levels of abstraction.

Your task is to refactor this `GenerateProjectSummaryReport` method.

### The "Before" State: A Function Doing Too Much
The current `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs` is responsible for:
1.  Validating the input `projectId`.
2.  Fetching all tasks.
3.  Filtering tasks for the specified project.
4.  Calculating statistics (total tasks, completed tasks, percentage complete).
5.  Identifying overdue tasks.
6.  Identifying high-priority tasks.
7.  Finding the next upcoming deadline.
8.  Assembling the final `ProjectSummaryDto` response.

This is a classic example of a function that does more than one thing.

### Your Refactoring Goal
Refactor the `GenerateProjectSummaryReport` method by extracting its various responsibilities into several small, well-named private helper methods. The final `GenerateProjectSummaryReport` method should be short and easy to read, delegating all the work to the helper methods you create.

You should be able to extract at least **five** distinct helper methods.

- Extend `ITaskService` with `UpdateTaskAsync` and `DeleteTaskAsync` signatures using expressive parameter names.
- Implement controller actions:
  - `PUT /api/tasks/{taskId}` consumes `UpdateTaskRequest`, returns `204 No Content`.
  - `DELETE /api/tasks/{taskId}` returns `204 No Content` (idempotent).
- Leave service/repository `TODOs` in place (students will implement later weeks) but ensure controller flow reads cleanly.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/ReportsController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/DTOs/Requests/UpdateTaskRequest.cs` (create or update as needed)
- This file (`Course-Materials/Weekly-Modules/week-04-functions.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1.  Create a new branch for your work: `week-04/<your-name>`.
2.  Locate and read the `GenerateProjectSummaryReport` method in `TaskFlowAPI/Controllers/ReportsController.cs`.
3.  Identify the distinct logical operations within the function. Use comments to mentally block them out first (e.g., `// Filter tasks`, `// Calculate stats`).
4.  Extract the first logical block into a new private method. Choose a descriptive name that clearly states what the method does (e.g., `FilterTasksByProjectId`).
5.  Replace the original code block with a call to your new method.
6.  Repeat this process for the other logical blocks. Aim for at least five extractions. Examples of methods you might create:
    -   `FilterTasksByProject(IEnumerable<TaskDto> allTasks, int projectId)`
    -   `CalculateCompletionPercentage(IEnumerable<TaskDto> projectTasks)`
    -   `CountOverdueTasks(IEnumerable<TaskDto> projectTasks)`
    -   `FindNextUpcomingDeadline(IEnumerable<TaskDto> projectTasks)`
    -   `AssembleSummaryDto(...)`
7.  Review your final `GenerateProjectSummaryReport` method. It should now be very short and read like a high-level summary of the steps involved in generating the report.
8.  Ensure the code still builds and runs.
9. Add `UpdateTaskAsync`/`DeleteTaskAsync` to the service interface and adjust constructor DI parameters after Week 2 renames.
10. Implement `PUT` and `DELETE` endpoints following HTTP semantics and returning appropriate status codes.
11. Ensure helper names articulate intent (e.g., `CreateNotFoundResponse` vs. `Handle404`).
12. Run build/tests.

## 6. How to Test

You can manually test your changes to ensure the logic remains the same.
1.  Run the API: `dotnet run --project TaskFlowAPI`
2.  Use a tool like Postman, or the `TaskFlowAPI.http` file in Visual Studio Code, to send a `GET` request to the new endpoint:
    ```http
    GET http://localhost:5000/api/Reports/project-summary/1
    ```
    *(Note: The port may vary. Check the application output when it starts.)*

The endpoint should return a JSON summary that looks something like this:
```json
{
  "projectId": 1,
  "projectName": "Project 1",
  "totalTasks": 5,
  "completedTasks": 2,
  "percentageComplete": 40.0,
  "overdueTasks": 1,
  "highPriorityTasks": 2,
  "nextUpcomingDeadline": "2025-12-01T00:00:00Z"
}
```
*(The exact values will depend on the seed data in your database.)*

## 7. Success Criteria

- The `GenerateProjectSummaryReport` method is refactored to be no more than 15 lines long.
- At least five private helper methods have been extracted from the original function.
- Each new helper method has a clear, descriptive name that reveals its intent.
- The `GenerateProjectSummaryReport` method now acts as a coordinator, and its body follows the Stepdown Rule.
- The API builds successfully and the `/api/Reports/project-summary/{projectId}` endpoint functions as it did before the refactor.

## 8. Submission Process

1.  Commit your changes with a message like: `Refactor: Break down GenerateProjectSummaryReport into smaller functions`.
2.  Create a pull request. In the summary, list the helper functions you created and explain how your refactoring improved the code.
3.  In your weekly submission issue, include a "before" and "after" snippet of the `GenerateProjectSummaryReport` method to showcase the improvement.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Function Size:* Describe the most significant function you extracted. Why did you choose to separate it? What naming strategy did you use to make its purpose clear?
- *Readability:* How did applying the "Stepdown Rule" change the way you read and understand the `GenerateProjectSummaryReport` function?
- *Testability:* How might these smaller functions be easier to unit test than the original, large function?

### Discussion Prep:
- *Be prepared to share one of the helper functions you created and justify its name and purpose.*
- *Was there any part of the original function that was difficult to extract? Why?*
- *How does this exercise relate to the "Single Responsibility Principle" you will learn about later?*

## 10. Time Estimate

- 45 min – Reading & planning the refactor.
- 45 min – Implementing the refactoring by extracting methods.
- 20 min – Testing and creating the pull request.
**Total:** ~110 minutes.

## 11. Additional Resources

- **[Function Refactor Example](../Examples/FunctionRefactor.cs)**
- **[Google Java Style Guide: Function Names](https://google.github.io/styleguide/javaguide.html#s5.2.3-method-names)**
- **[PEP 8: Style Guide for Python Code - Function Names](https://www.python.org/dev/peps/pep-0008/#function-and-variable-names)**
- **[C# Coding Conventions: Function Names](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/inside-a-program/coding-conventions#naming-conventions)**
- **[Clean Code Functions](https://dipta007.com/clean-code-functions/)**
- **[The Art of Writing Small and Plain Functions](https://dmitripavlutin.com/the-art-of-writing-small-and-plain-functions/)**
- **[A Good Function-Writing Process](https://education.launchcode.org/intro-to-professional-web-dev/chapters/functions/process.html)**
- **[Youtube: The Ultimate Guide to Writing Functions](https://www.youtube.com/watch?v=yatgY4NpZXE)**
