# Week 21: API Design & Documentation

This week focuses on helping junior developers understand REST API fundamentals and best practices, preparing them to work effectively with APIs in real projects.

## 1. Learning Objectives

- Understand fundamental REST API principles and conventions.
- Identify common HTTP methods and status codes.
- Read and understand API documentation.
- Polish RESTful design (status codes, resource names, pagination).
- Document API using Swagger annotations and XML comments.
- Introduce API versioning and response shaping.

## 2. Reading & Resources (50 min)

- **[RESTful Web API Design](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)** (20 min) - Microsoft's comprehensive REST guidelines.
- **[REST API Tutorial](https://restfulapi.net/)** (15 min) - Core concepts and terminology.
- **[Best Practices for REST API Design](https://stackoverflow.blog/2020/03/02/best-practices-for-rest-api-design/)** (15 min) - Common pitfalls and solutions.

## 3. This Week's Work

- Add pagination support (`page`, `pageSize`) to task listing.
- Configure `Swashbuckle` for XML comments and operation summaries.
- Add API versioning (`v1` default) using `Microsoft.AspNetCore.Mvc.Versioning` package.
- Update `README.md` with API endpoint overview.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- New: `TaskFlowAPI/DTOs/Responses/PagedResponse.cs`
- `TaskFlowAPI/Program.cs` (Swagger and versioning configuration).
- `TaskFlowAPI/TaskFlowAPI.csproj` (add XML documentation output).
- `README.md` (add API endpoints section).
- This file (`Course-Materials/Weekly-Modules/week-21-api-design-documentation.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-21-submission`.
2. Enable XML comments in `csproj`: `<GenerateDocumentationFile>true</GenerateDocumentationFile>`.
3. Install `Microsoft.AspNetCore.Mvc.Versioning`:
   ```bash
   dotnet add TaskFlowAPI package Microsoft.AspNetCore.Mvc.Versioning
   ```
4. Configure API versioning in `Program.cs` (see Section 11 for template).
5. Update controller route to include version: `[Route("api/v{version:apiVersion}/tasks")]`.
6. Create `PagedResponse<T>` DTO with page metadata (see Section 11 for template).
7. Modify `GetTasks` action to accept `page`/`pageSize` query parameters and return `PagedResponse<TaskDto>`.
8. Update service to apply pagination after filters using `Skip()` and `Take()`.
9. Add XML doc comments to controller actions (see Section 11 for examples).
10. Configure Swagger to include XML comments in `Program.cs`.
11. Update `README.md` with API endpoints overview.
12. Run `dotnet build TaskFlowAPI.sln` and verify no XML comment warnings.
13. Test pagination via Swagger: `GET /api/v1/tasks?page=1&pageSize=10`.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual testing:
```bash
# Start API
dotnet run --project TaskFlowAPI

# Test pagination
curl "https://localhost:5001/api/v1/tasks?page=1&pageSize=10"

# Verify Swagger docs
# Navigate to https://localhost:5001/swagger
```

## 7. Success Criteria

- API responds with paged payload containing metadata (page, pageSize, totalCount, totalPages).
- Swagger UI displays operation summaries and parameter descriptions.
- Versioned route works (`/api/v1/tasks`).
- `README.md` documents major endpoints and query parameters.
- XML documentation warnings suppressed or resolved.
- Week 21 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-21-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 21 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Documentation Update: Record the most significant addition you made to `README.md` or Swagger descriptions.*
- *Versioning Choice: Note why you selected your versioning strategy and any migrations required later.*
- *Pagination Defaults: Document your choice of default page size and maximum limit, and the reasoning behind it.*

### Discussion Prep:

- *How did you choose default page size and limits?*
- *What versioning strategy did you implement and why?*
- *How can clients discover available filters/pagination from docs?*
- *Which external API docs inspired your approach?*

## 10. Time Estimate

- 50 min — Reading.
- 15 min — Planning and package installation.
- 45 min — Pagination, versioning, and docs updates.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 25 minutes.

## 11. Implementation Templates

### API Versioning Configuration

In `Program.cs`, add after `AddControllers()`:

```csharp
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
});
```

Update controller route:

```csharp
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class TasksController : ControllerBase
{
    // ... endpoints
}
```

### Swagger Configuration with XML Comments

Enable XML documentation in `TaskFlowAPI.csproj`:

```xml
<PropertyGroup>
  <TargetFramework>net8.0</TargetFramework>
  <Nullable>enable</Nullable>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

Update `AddSwaggerGen()` in `Program.cs`:

```csharp
using System.Reflection;

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "TaskFlow API",
        Version = "v1",
        Description = "Task management API for Learn and Code curriculum"
    });
    
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
```

### XML Documentation Example

```csharp
/// <summary>
/// Retrieves all tasks with optional filtering and pagination
/// </summary>
/// <param name="page">Page number (default: 1)</param>
/// <param name="pageSize">Items per page (default: 20, max: 100)</param>
/// <param name="status">Filter by completion status</param>
/// <returns>Paged list of tasks</returns>
/// <response code="200">Returns the paged task list</response>
/// <response code="400">Invalid pagination parameters</response>
[HttpGet]
[ProducesResponseType(typeof(PagedResponse<TaskDto>), StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<PagedResponse<TaskDto>>> GetTasks(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20,
    [FromQuery] bool? status = null)
{
    if (page < 1)
        return BadRequest("Page must be >= 1");
        
    if (pageSize < 1 || pageSize > 100)
        return BadRequest("PageSize must be between 1 and 100");
    
    var result = await _service.GetAllTasksAsync(page, pageSize, status);
    return Ok(result);
}
```

### PagedResponse DTO

Create: `TaskFlowAPI/DTOs/Responses/PagedResponse.cs`

```csharp
namespace TaskFlowAPI.DTOs.Responses;

/// <summary>
/// Generic paged response wrapper
/// </summary>
public class PagedResponse<T>
{
    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int Page { get; set; }
    
    /// <summary>
    /// Items per page
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalCount { get; set; }
    
    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    
    /// <summary>
    /// Items on current page
    /// </summary>
    public List<T> Data { get; set; } = new();
    
    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNextPage => Page < TotalPages;
    
    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPreviousPage => Page > 1;
}
```

### Pagination Implementation

```csharp
public async Task<PagedResponse<TaskDto>> GetAllTasksAsync(int page, int pageSize)
{
    var query = _context.Tasks.AsQueryable();
    
    // Get total count before pagination
    var totalCount = await query.CountAsync();
    
    // Apply pagination
    var tasks = await query
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .ToListAsync();
    
    var dtos = tasks.Select(t => _mapper.ToDto(t)).ToList();
    
    return new PagedResponse<TaskDto>
    {
        Page = page,
        PageSize = pageSize,
        TotalCount = totalCount,
        Data = dtos
    };
}
```

### Pagination Defaults (Industry Standards)

- **Default page size: 20** - Balances data transfer vs number of requests.
- **Maximum page size: 100** - Prevents abuse and performance issues.
- **Default page: 1** - Intuitive for most users (first page).
- **Validation:** page >= 1, pageSize >= 1 && pageSize <= 100.

## 12. Additional Resources

### Examples

- **[Documentation Example](../Examples/Documentation.md)** - Review for API documentation patterns.

### External Resources

- **[GitHub REST API Documentation](https://docs.github.com/en/rest)** - Example of excellent API docs.
- **[Stripe API Documentation](https://stripe.com/docs/api)** - Gold standard for developer experience.
- **[OpenAPI Specification](https://swagger.io/specification/)** - Complete Swagger/OpenAPI reference.

### Optional Deep Dives

- **[REST API Concepts and Examples](https://www.youtube.com/watch?v=7YcW25PHnAA)** (15 min) - Visual walkthrough.
- **[REST API Design Best Practices](https://www.youtube.com/watch?v=MF9qGE2oPnE)** (12 min) - Common patterns.
- **[What is a REST API?](https://www.youtube.com/watch?v=lsMQRaeKNDk)** (8 min) - Quick primer.