# Week 10: Error Handling & Validation
This week, we will focus on the importance of error handling and how it contributes to the Quality Manifesto's focus on domain knowledge. We will learn best practices for handling exceptions and errors in software development, ensuring that our applications are robust, reliable, and user-friendly.

## 1. Learning Objectives
- Implement FluentValidation rules for create/update requests.
- Introduce domain-specific exceptions and map them to HTTP responses.
- Configure global exception handling middleware.

## 2. Reading & Resources (80 min)
- **Clean Code Chapter 7: Error Handling (pp. 111-134)** – prefer exceptions to error codes, keep error-handling code separate from happy-path logic.
- **Exception Handling Best Practices in .NET** – Pragmatic guidance for production APIs.
- **Error Handling in Large .NET Projects** – Anti-patterns and refactoring advice.
- **Clean Code and the Art of Exception Handling** – Reinforces Clean Code perspective.
- **Art of Clean Code — Error Handling** – Supplementary patterns and red flags.
- Optional: language-specific companion (e.g., **A Definitive Guide to Handling Errors in JavaScript**) if you support cross-stack clients.
- Optional: **New Relic error monitoring docs** for teams planning observability in Week 21+.

## 3. This Week’s Work
- Implement `CreateTaskValidator` and `UpdateTaskValidator` with actionable rules.
- Throw `DomainValidationException` or `TaskNotFoundException` from service methods as appropriate.
- Replace placeholder exception middleware with polished `ProblemDetails` outputs.

## 4. Files to Modify

- `TaskFlowAPI/Validators/CreateTaskValidator.cs`
- `TaskFlowAPI/Validators/UpdateTaskValidator.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs` (add exception usage + guard logic)
- `TaskFlowAPI/Extensions/ExceptionMiddlewareExtensions.cs`
- `TaskFlowAPI/Program.cs` (ensure middleware order correct)
- This file (`Course-Materials/Weekly-Modules/week-10-error-handling-validation.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions
1. Branch `week-10/<your-name>`.
2. Implement validation rules:
   - Title: required, 3-100 chars.
   - Priority: between 0-5.
   - DueDate: must be future or today.
   - Update request: require at least one field set.
3. Inject validators into `TaskService` (constructor) and use them in `Add`/`Update`.
4. Throw `DomainValidationException` when validation fails and include aggregated messages.
5. Throw `TaskNotFoundException` in Delete/Update when repository returns null.
6. Update `UseTaskFlowExceptionHandler` to map custom exceptions to 400/404 with structured `ProblemDetails`.
7. Log errors with context inside middleware or service (use `_logger.LogError`).
8. Run build/tests and hit endpoints with invalid payloads to confirm 400 responses with messages.

## 6. How to Test
```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual tests via Swagger/Postman:
  - POST with missing title → 400 + validation message.
  - PUT with nonexistent id → 404 + “Task not found.”

## 7. Success Criteria
- Validators enforce rules and produce clear messages.
- Services catch validator results and throw domain exceptions.
- Exception middleware returns consistent JSON (`application/problem+json`).
- Logs contain warning/error messages for invalid requests.

## 8. Submission Process
- Commit `Week 10 – validation and error handling`.
- PR summary includes sample error response JSON.
- Weekly issue attaches screenshot of Swagger error response.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Validation Coverage:* Which rule gave you the most confidence? Note the edge case it protects.

*Exception Mapping:* Record one decision you made in `UseTaskFlowExceptionHandler` and why it aligns with API consumers.

Discussion Prep:
- How did centralized error handling simplify controllers?
- What validation rules still feel brittle or missing?
- How will these exceptions influence future unit tests?
- What monitoring or logging would you add before releasing to partners?

## 10. Time Estimate
- 80 min – Reading + rule design.
- 25 min – Implement validators, exceptions, middleware.
- 15 min – Manual testing + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources
- **[Exception Handling Best Practices in .NET](https://stackify.com/csharp-exception-handling-best-practices/)**
- **[A Definitive Guide to Handling Errors in JavaScript](https://kinsta.com/blog/errors-in-javascript/)**
- **[Error Handling in Large .NET Projects](https://www.codeproject.com/Articles/9538/Exception-Handling-Best-Practices-in-NET)**
- **[Clean Code and the Art of Exception Handling](https://www.toptal.com/abap/clean-code-and-the-art-of-exception-handling)**
- **[Art of Clean Code — Error Handling](https://blog.devgenius.io/art-of-clean-code-error-handling-7951587eac98)**
- **[Getting Started with New Relic](https://docs.newrelic.com/docs/errors-inbox/getting-started/)**
- **[Logging Errors to New Relic in .Net using Serilog](https://github.com/newrelic/newrelic-logenricher-dotnet/blob/main/src/NewRelic.LogEnrichers.Serilog/README.md)**
