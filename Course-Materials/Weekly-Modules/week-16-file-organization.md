# Week 16: File Organization & Module Structure

This week, we will focus on defining and managing boundaries in code, connecting this concept to In Time Tec's emphasis on continuous integration and deployment. We will learn how to create clean interfaces between components, ensuring that our applications are modular, maintainable, and easy to integrate. This module is based on **Clean Code Chapter 5: Formatting** and **Chapter 8: Boundaries**.

## 1. Learning Objectives

- Understand the importance of defining and managing boundaries in code.
- Apply best practices for creating clean interfaces between components.
- Identify boundaries in your project and refactor the code to improve integrations between components.
- Restructure files so each class lives in a focused module/folder.
- Break apart any remaining "god" files (e.g., legacy `TaskService` monolith, helpers).
- Establish namespace conventions aligned with directory structure.

## 2. Reading & Resources (60 min)

- **Clean Code Chapter 5: Formatting (pp. 77-96)** (20 min) - Emphasize readability, vertical openness, and logical grouping.
- **Clean Code Chapter 8: Boundaries (pp. 113-122)** (15 min) - Refresh boundary management concepts.
- **[Designing Software with Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)** (15 min) - Uncle Bob on modular architectures.
- **[Reading Clean Code: Boundaries](https://medium.com/codex/reading-clean-code-week-5-boundaries-aba7fbefb861)** (10 min) - Commentary on chapter application.

## 3. This Week's Work

- Move mapper, validator, business rules into dedicated folders (`Services/Tasks/Mapping`, `.../Validation`, `.../Rules`).
- Create `Extensions/` modules for shared helpers (e.g., `ServiceCollectionExtensions`).
- Update namespaces to match new folders.
- Remove unused helpers and consolidate DI registrations.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/*` (reorganize subfolders).
- New: `TaskFlowAPI/Extensions/ServiceCollectionExtensions.cs`
- `TaskFlowAPI/Program.cs` (update using statements and DI calls).
- `TaskFlowAPI.Tests` (fix namespaces where necessary).
- This file (`Course-Materials/Weekly-Modules/week-16-file-organization.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-16-submission`.
2. Inspect `TaskService` and related classes; identify any lingering nested classes or `TODO` comments referencing monoliths.
3. Create new subfolders if they don't exist:
   - `Services/Tasks/Mapping`
   - `Services/Tasks/Validation`
   - `Services/Tasks/Rules`
   - `Services/Tasks/Filters` (should already exist—confirm naming).
4. Move files into appropriate folders using `git mv` to preserve history:
   ```bash
   git mv TaskFlowAPI/Services/Tasks/TaskMapper.cs TaskFlowAPI/Services/Tasks/Mapping/
   ```
5. Update namespaces in moved files to match new folder structure.
6. Create `Extensions/ServiceCollectionExtensions.cs` consolidating DI registrations (repositories, services, filters, validators).
7. Update `Program.cs` to call the new extension method instead of inline registrations.
8. Delete any duplicate helper files or unused folder junk (e.g., old `Utils/Helpers.cs` if present).
9. Run `dotnet build TaskFlowAPI.sln` to catch any namespace/DI issues.
10. Run `dotnet test TaskFlowAPI.sln` to verify tests still pass with new structure.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Verify structure:
```bash
# Review new folder structure
tree TaskFlowAPI/Services/Tasks/
tree TaskFlowAPI/Extensions/

# Verify git preserved history
git log --follow TaskFlowAPI/Services/Tasks/Mapping/TaskMapper.cs
```

## 7. Success Criteria

- Directory structure reflects logical modules (`Controllers`, `Services/Tasks/Mapping`, `Services/Tasks/Validation`, `Services/Tasks/Rules`, etc.).
- No circular namespace dependencies.
- DI registrations centralized via `ServiceCollectionExtensions`.
- Build and tests succeed with no warnings.
- `git diff` shows moves not rewrites (used `git mv` for file moves).
- Week 16 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-16-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 16 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Before/After Snapshot: Paste the old vs. new folder path that most improved discoverability. Why does the new structure make it easier to find what you need?*
- *Namespace Strategy: Describe your naming convention and how it maps to the new folder structure.*
- *Boundary Impact: How does the new organization clarify the boundaries between different layers of your application?*

### Discussion Prep:

- *How does the new structure improve onboarding for new developers?*
- *What naming conventions did you adopt for namespaces and folders?*
- *Did you remove any dead files? Share the before/after impact.*
- *What automation (solution filters, analyzers, EditorConfig) could help keep the structure from regressing?*

## 10. Time Estimate

- 60 min — Reading.
- 10 min — Plan folder structure.
- 35 min — Move files, update namespaces, consolidate DI.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 20 minutes.

## 11. Implementation Templates

### ServiceCollectionExtensions.cs

Create: `TaskFlowAPI/Extensions/ServiceCollectionExtensions.cs`

```csharp
namespace TaskFlowAPI.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all TaskFlow application services.
    /// </summary>
    public static IServiceCollection AddTaskFlowServices(this IServiceCollection services)
    {
        // Task services
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<TaskMapper>();
        services.AddScoped<TaskBusinessRules>();
        
        // Task filters
        services.AddScoped<ITaskFilter, StatusFilter>();
        services.AddScoped<ITaskFilter, PriorityFilter>();
        services.AddScoped<CompositeTaskFilter>();
        
        // Validation
        services.AddValidatorsFromAssemblyContaining<TaskValidator>();
        
        return services;
    }
    
    /// <summary>
    /// Registers all TaskFlow repositories.
    /// </summary>
    public static IServiceCollection AddTaskFlowRepositories(this IServiceCollection services)
    {
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        
        return services;
    }
    
    /// <summary>
    /// Registers infrastructure services (clock, cache, etc.).
    /// </summary>
    public static IServiceCollection AddTaskFlowInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ISystemClock, UtcSystemClock>();
        
        return services;
    }
}
```

### Updated Program.cs

```csharp
// Program.cs (simplified)
var builder = WebApplication.CreateBuilder(args);

// Add services using extension methods
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// TaskFlow services (organized via extensions)
builder.Services.AddTaskFlowRepositories();
builder.Services.AddTaskFlowServices();
builder.Services.AddTaskFlowInfrastructure();

var app = builder.Build();

// Middleware configuration...
app.Run();
```

### Folder Structure Example

**Target structure:**
```
TaskFlowAPI/
├── Controllers/
├── Services/
│   └── Tasks/
│       ├── TaskService.cs
│       ├── Mapping/
│       │   └── TaskMapper.cs
│       ├── Validation/
│       │   └── TaskValidator.cs
│       ├── Rules/
│       │   └── TaskBusinessRules.cs
│       └── Filters/
│           ├── ITaskFilter.cs
│           ├── StatusFilter.cs
│           └── PriorityFilter.cs
├── Extensions/
│   └── ServiceCollectionExtensions.cs
└── ...
```

### Git Move Commands

```bash
# Move single file
git mv TaskFlowAPI/Services/Tasks/TaskMapper.cs TaskFlowAPI/Services/Tasks/Mapping/

# Move multiple files
git mv TaskFlowAPI/Services/Tasks/TaskBusinessRules.cs TaskFlowAPI/Services/Tasks/Rules/
git mv TaskFlowAPI/Services/Tasks/TaskValidator.cs TaskFlowAPI/Services/Tasks/Validation/

# Verify moves preserved history
git log --follow -- TaskFlowAPI/Services/Tasks/Mapping/TaskMapper.cs
```

## 12. Additional Resources

### Examples

- **[Systems Example](../Examples/Systems.md)** - Review for boundary patterns.

### External Resources

- **[C# Namespace Best Practices](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/names-of-namespaces)** - Microsoft's namespace guidelines.
- **[Project Organization in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/)** - Structuring applications.

### Optional Deep Dives

- **[Clean Architecture Folder Structure](https://github.com/jasontaylordev/CleanArchitecture)** - Example repository structure.
- **[Modular Monolith Architecture](https://www.kamilgrzybek.com/design/modular-monolith-primer/)** - Advanced module organization.