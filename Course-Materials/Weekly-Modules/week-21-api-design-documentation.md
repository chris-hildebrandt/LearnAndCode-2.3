# Week 21: API Design & Documentation

This week focuses on helping junior developers understand REST API fundamentals and best practices, preparing them to work effectively with APIs in real projects.

## 1. Learning Objectives

- Understand fundamental REST API principles and conventions.
- Identify common HTTP methods and status codes.
- Read and understand API documentation.
- Recognize REST API best practices and common pitfalls.
- Work with existing APIs effectively.
- Polish RESTful design (status codes, resource names, pagination).
- Document API using Swagger annotations and XML comments.
- Introduce API versioning and response shaping.

## 2. Reading & Resources (50 min)

- **[RESTful Web API Design](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)**
- **[REST API Tutorial](https://restfulapi.net/)**
- **[Best Practices for REST API Design](https://stackoverflow.blog/2020/03/02/best-practices-for-rest-api-design/)**
- **Microsoft REST API Guidelines** – Resource naming, versioning, pagination.
- **REST API Tutorial (restfulapi.net)** – Core concepts and terminology.
- **Stack Overflow Blog: Best Practices for REST API Design** – Common pitfalls.
- **Swagger/OpenAPI Best Practices** (cohort wiki) – Documentation focus.
- Optional: Review public API docs (GitHub, Stripe, Twitter) for inspiration.
- Optional videos: REST API Concepts & Examples, REST API Design Best Practices.

## 3. This Week’s Work

- Add pagination support (`page`, `pageSize`) to task listing.
- Configure `Swashbuckle` for XML comments + operation summaries.
- Add API versioning (`v1` default) using `Microsoft.AspNetCore.Mvc.Versioning` package.
- Update `README.md` with API endpoint overview.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/DTOs/Responses/PagedResponse.cs`
- `TaskFlowAPI/Program.cs` (Swagger + versioning)
- `TaskFlowAPI/TaskFlowAPI.csproj` (add XML documentation output)
- This file (`Course-Materials/Weekly-Modules/week-21-api-design-documentation.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-21/<your-name>`.
2. Enable XML comments in `csproj`: `<GenerateDocumentationFile>true</GenerateDocumentationFile>`.
3. Install `Microsoft.AspNetCore.Mvc.Versioning` and configure `options.AssumeDefaultVersionWhenUnspecified = true`.
4. Update controller route to include version (e.g., `[Route("api/v{version:apiVersion}/tasks")]`).
5. Modify `GetTasks` action to accept `page`/`pageSize` query parameters and return `PagedResponse<TaskDto>`.
6. Update service to apply pagination after filters.
7. Add Swagger configuration to include XML comments and tags.
8. Update `README.md` “What You Will Build” with API endpoints overview.
9. Build/tests and hit Swagger to confirm docs show summaries + version.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual: `GET /api/v1/tasks?page=1&pageSize=10` expecting paged metadata.

## 7. Success Criteria

- API responds with paged payload containing metadata (page, pageSize, totalCount, totalPages).
- Swagger UI displays operation summaries and parameter descriptions.
- Versioned route works; old route removed or redirected.
- `README.md` documents major endpoints and query params.

## 8. Submission Process

- Commit `Week 21 – API design polish`.
- PR summary includes screenshot of updated Swagger UI.
- Weekly issue documents pagination decisions (default size, max size).

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Documentation Update:* Record the most significant addition you made to `README.md` or swagger descriptions.
- *Versioning Choice:* Note why you selected your versioning strategy and any migrations required later.

### Discussion Prep:
- *How did you choose default page size and limits?*
- *What versioning strategy did you implement and why?*
- *How can clients discover available filters/pagination from docs?*
- *Which external API docs inspired your approach?*

## 10. Time Estimate

- 45 min – Reading.
- 15 min – Planning.
- 45 min – Pagination + versioning + docs updates.
- 15 min – Swagger verification + PR/issue.
**Total:** ~120 minutes.

## 11. Additional Resources

- **[Documentation Example](../Examples/Documentation.md)**

### Video Tutorials:
- **[REST API Concepts and Examples](https://www.youtube.com/watch?v=7YcW25PHnAA)**
- **[REST API Design Best Practices](https://www.youtube.com/watch?v=MF9qGE2oPnE)**
- **[What is a REST API?](https://www.youtube.com/watch?v=lsMQRaeKNDk)**

### Documentation Examples:
- **[GitHub REST API Documentation](https://docs.github.com/en/rest)**
- **[Twitter API Documentation](https://developer.twitter.com/en/docs/api-reference-index)**
- **[Stripe API Documentation](https://stripe.com/docs/api)**
