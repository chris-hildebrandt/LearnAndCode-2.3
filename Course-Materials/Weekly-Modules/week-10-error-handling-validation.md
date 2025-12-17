# Week 10: Error Handling & Validation

This week, we will focus on the importance of error handling and how it contributes to the Quality Manifesto's focus on domain knowledge. We will learn best practices for handling exceptions and errors in software development, ensuring that our applications are robust, reliable, and user-friendly. This module is based on **Clean Code Chapter 7: Error Handling**.

## 1. Learning Objectives

- Implement FluentValidation rules for create/update requests.
- Introduce domain-specific exceptions and map them to HTTP responses.
- Configure global exception handling middleware.
- Apply Clean Code principles to separate error-handling code from happy-path logic.

## 2. Reading & Resources (80 min)

- **Clean Code Chapter 7: Error Handling (pp. 111-134)** — Prefer exceptions to error codes, keep error-handling code separate from happy-path logic (40 min).
- **[Exception Handling Best Practices in .NET](https://stackify.com/csharp-exception-handling-best-practices/)** — Pragmatic guidance for production APIs (20 min).
- **[Error Handling in Large .NET Projects](https://www.codeproject.com/Articles/9538/Exception-Handling-Best-Practices-in-NET)** — Anti-patterns and refactoring advice (20 min).

## 3. This Week's Work

- Implement `CreateTaskValidator` and `UpdateTaskValidator` with actionable rules.
- Throw `DomainValidationException` or `TaskNotFoundException` from service methods as appropriate.
- Replace placeholder exception middleware with polished `ProblemDetails` outputs.
- Configure global exception handling middleware.

## 4. Files to Modify

- `TaskFlowAPI/Validators/CreateTaskValidator.cs`
- `TaskFlowAPI/Validators/UpdateTaskValidator.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs` (add exception usage and guard logic).
- `TaskFlowAPI/Extensions/ExceptionMiddlewareExtensions.cs`
- `TaskFlowAPI/Program.cs` (ensure middleware order correct).
- This file (`Course-Materials/Weekly-Modules/week-10-error-handling-validation.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-10-submission`.

2. **Implement CreateTaskValidator (15 min)**
   
   Create validation rules in `TaskFlowAPI/Validators/CreateTaskValidator.cs`:
   - Title: required, 3-100 characters.
   - Priority: between 0-5.
   - DueDate: must be today or future date.
   
   ```csharp
   public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
   {
       public CreateTaskValidator()
       {
           RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is required")
               .Length(3, 100).WithMessage("Title must be between 3 and 100 characters");
           
           RuleFor(x => x.Priority)
               .InclusiveBetween(0, 5).WithMessage("Priority must be between 0 and 5");
           
           RuleFor(x => x.DueDate)
               .GreaterThanOrEqualTo(DateTime.Today)
               .When(x => x.DueDate.HasValue)
               .WithMessage("Due date must be today or in the future");
       }
   }
   ```

3. **Implement UpdateTaskValidator (10 min)**
   
   Create validation rules in `TaskFlowAPI/Validators/UpdateTaskValidator.cs`:
   - Require at least one field to be set.
   - Apply same rules as CreateTaskValidator for fields that are provided.

4. **Inject Validators into TaskService (5 min)**
   
   Add validators to `TaskService` constructor:
   ```csharp
   private readonly IValidator<CreateTaskRequest> _createValidator;
   private readonly IValidator<UpdateTaskRequest> _updateValidator;
   
   public TaskService(
       ITaskRepository taskRepository,
       IValidator<CreateTaskRequest> createValidator,
       IValidator<UpdateTaskRequest> updateValidator,
       ILogger<TaskService> logger)
   {
       _taskRepository = taskRepository;
       _createValidator = createValidator;
       _updateValidator = updateValidator;
       _logger = logger;
   }
   ```

5. **Use Validators in Service Methods (20 min)**
   
   Update `AddAsync` method:
   ```csharp
   public async Task<TaskDto> AddAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
   {
       // Validate
       var validationResult = await _createValidator.ValidateAsync(request, cancellationToken);
       if (!validationResult.IsValid)
       {
           var errors = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage));
           throw new DomainValidationException(errors);
       }
       
       // Rest of implementation...
   }
   ```
   
   Update `UpdateAsync` and `DeleteAsync` to throw `TaskNotFoundException` when entity not found:
   ```csharp
   var entity = await _taskRepository.GetByIdAsync(id, cancellationToken);
   if (entity == null)
   {
       throw new TaskNotFoundException($"Task with ID {id} not found");
   }
   ```

6. **Implement Exception Middleware (25 min)**
   
   Update `UseTaskFlowExceptionHandler` in `TaskFlowAPI/Extensions/ExceptionMiddlewareExtensions.cs`:
   - Map `DomainValidationException` → 400 Bad Request
   - Map `TaskNotFoundException` → 404 Not Found
   - Map all other exceptions → 500 Internal Server Error
   - Return structured `ProblemDetails` JSON (`application/problem+json`)
   - Log errors with context using `_logger.LogError`
   
   ```csharp
   app.UseExceptionHandler(errorApp =>
   {
       errorApp.Run(async context =>
       {
           var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
           var exception = exceptionHandlerPathFeature?.Error;
           
           var problemDetails = exception switch
           {
               DomainValidationException validationEx => new ProblemDetails
               {
                   Status = StatusCodes.Status400BadRequest,
                   Title = "Validation Error",
                   Detail = validationEx.Message
               },
               TaskNotFoundException notFoundEx => new ProblemDetails
               {
                   Status = StatusCodes.Status404NotFound,
                   Title = "Resource Not Found",
                   Detail = notFoundEx.Message
               },
               _ => new ProblemDetails
               {
                   Status = StatusCodes.Status500InternalServerError,
                   Title = "An error occurred",
                   Detail = "An unexpected error occurred. Please try again later."
               }
           };
           
           context.Response.StatusCode = problemDetails.Status ?? 500;
           context.Response.ContentType = "application/problem+json";
           
           await context.Response.WriteAsJsonAsync(problemDetails);
       });
   });
   ```

7. **Verify Middleware Order in Program.cs (5 min)**
   
   Ensure exception middleware is registered early in the pipeline:
   ```csharp
   app.UseTaskFlowExceptionHandler();  // Early in pipeline
   app.UseHttpsRedirection();
   app.UseAuthorization();
   app.MapControllers();
   ```

8. Run build and tests:
   ```bash
   dotnet build TaskFlowAPI.sln
   dotnet test TaskFlowAPI.sln
   ```

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual tests via Swagger/Postman:
- **POST /api/tasks** with missing title → Expect 400 Bad Request with validation message.
- **POST /api/tasks** with priority = 10 → Expect 400 Bad Request with "Priority must be between 0 and 5".
- **PUT /api/tasks/999** with nonexistent id → Expect 404 Not Found with "Task not found" message.
- **GET /api/tasks/999** with nonexistent id → Expect 404 Not Found.

Check logs to verify error messages are being logged with appropriate context.

## 7. Success Criteria

- Validators enforce rules and produce clear messages.
- Services catch validator results and throw domain exceptions.
- Exception middleware returns consistent JSON (`application/problem+json`).
- Logs contain warning/error messages for invalid requests.
- Manual tests confirm appropriate HTTP status codes (400, 404, 500).
- Week 10 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

1. Create a new branch for your weekly work (e.g., `git checkout -b week-10-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 10 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Validation Coverage: Which validation rule gave you the most confidence? Note the edge case it protects.*
- *Exception Mapping: Record one decision you made in `UseTaskFlowExceptionHandler` and why it aligns with API consumers' needs.*
- *Clean Code Principles: How does separating error-handling code from happy-path logic improve code readability?*

### Discussion Prep:

- *How did centralized error handling simplify controllers?*
- *What validation rules still feel brittle or missing?*
- *How will these exceptions influence future unit tests?*
- *What monitoring or logging would you add before releasing to partners?*
- *How does proper error handling relate to the Quality Manifesto's emphasis on domain knowledge?*

## 10. Time Estimate

- 80 min — Reading and rule design.
- 80 min — Implementing validators, exceptions, and middleware.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~3 hours 15 minutes.

## 11. Additional Resources

### Examples

- **FluentValidation Documentation** — Official validator examples and patterns.

### External Resources

- **[Exception Handling Best Practices in .NET](https://stackify.com/csharp-exception-handling-best-practices/)** — Comprehensive guide to .NET exception handling.
- **[Error Handling in Large .NET Projects](https://www.codeproject.com/Articles/9538/Exception-Handling-Best-Practices-in-NET)** — Anti-patterns and refactoring strategies.
- **[Clean Code and the Art of Exception Handling](https://www.toptal.com/abap/clean-code-and-the-art-of-exception-handling)** — Reinforces Clean Code perspective.
- **[Art of Clean Code — Error Handling](https://blog.devgenius.io/art-of-clean-code-error-handling-7951587eac98)** — Supplementary patterns and red flags.

### Optional Deep Dives

- **[A Definitive Guide to Handling Errors in JavaScript](https://kinsta.com/blog/errors-in-javascript/)** — For teams supporting cross-stack clients.
- **[Getting Started with New Relic](https://docs.newrelic.com/docs/errors-inbox/getting-started/)** — Error monitoring for production (relevant for Week 21+).
- **[Logging Errors to New Relic in .NET using Serilog](https://github.com/newrelic/newrelic-logenricher-dotnet/blob/main/src/NewRelic.LogEnrichers.Serilog/README.md)** — Integration guide for observability.