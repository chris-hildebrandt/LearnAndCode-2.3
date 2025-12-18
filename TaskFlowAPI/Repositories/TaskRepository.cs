// This file defines the "Repository" for our tasks.
// The Repository pattern is a design pattern that isolates the data access logic from the rest of the application.
// Its main job is to mediate between the application's business logic (the Service layer) and the database.
// This class, `TaskRepository`, handles all the database operations for `TaskEntity` objects, such as
// creating, reading, updating, and deleting (CRUD) tasks.

// By using a repository, our Service layer doesn't need to know anything about the database itself
// (e.g., that we are using Entity Framework Core). It just calls methods like `GetAllAsync` or `CreateAsync`.

using Microsoft.EntityFrameworkCore; // Imports Entity Framework Core, the library we use to interact with the database.
using TaskFlowAPI.Data; // Imports the DbContext, which represents our database session.
using TaskFlowAPI.Entities; // Imports the TaskEntity, which represents the data model for a task.
using TaskFlowAPI.Repositories.Interfaces; // Imports the interface that this class implements.

namespace TaskFlowAPI.Repositories;

/// <summary>
/// This is the concrete implementation of the ITaskRepository interface.
/// It uses Entity Framework Core to perform CRUD (Create, Read, Update, Delete) operations on tasks in the database.
/// Week 8 scaffolding: this class throws until students implement it with EF Core.
/// </summary>
public class TaskRepository : ITaskRepository
{
    // TODO Week 14 (ISP):
    // Update this class to also implement ITaskReader and ITaskWriter.
    // Keep ITaskRepository for backwards compatibility during the migration.


    // A private, readonly field to hold the database context.
    // The DbContext is the primary gateway for interacting with the database.
    private readonly TaskFlowDbContext _dbContext;

    /// <summary>
    /// The constructor, where our DbContext is injected by the dependency injection container.
    /// </summary>
    /// <param name="dbContext">The database context instance.</param>
    public TaskRepository(TaskFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// This method will contain the logic to retrieve all tasks from the database.
    /// TODO Week 8: Return all tasks ordered by priority, then due date.
    /// Hint: use <see cref="AsNoTracking"/> for read-only queries.
    /// Timebox: 15 minutes.
    /// </summary>
    public Task<List<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        // This is a placeholder. In Week 8, you will write the Entity Framework Core query to get all tasks.
        throw new NotImplementedException("Week 8: Implement repository read operations.");
    }

    // CODE SMELL: Primitive Obsession (Clean Code Ch 17, p. 292)
    // Using int for ID instead of a type-safe TaskId value object.
    // This allows invalid values (negative, zero) and makes code less type-safe.
    // Refactor by: Create TaskId value object class (advanced refactoring).
    /// <summary>
    /// This method will contain the logic to find a single task by its ID in the database.
    /// TODO Week 8: Query for a single task. Include the Project navigation property.
    /// Return <c>null</c> when not found.
    /// </summary>
    public Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. In Week 8, you will write the EF Core query to find a task by its ID.
        throw new NotImplementedException("Week 8: Implement repository read operations.");
    }

    /// <summary>
    /// This method will contain the logic to add a new task to the database.
    /// TODO Week 8: Persist a new task. Save changes and return the entity with generated key.
    /// </summary>
    public Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. In Week 8, you will write the EF Core code to add a new entity and save changes.
        throw new NotImplementedException("Week 8: Implement repository create operation.");
    }

    /// <summary>
    /// This method will contain the logic to update an existing task in the database.
    /// TODO Week 8: Update an existing task. Ensure concurrency-safe pattern.
    /// </summary>
    public Task UpdateAsync(TaskEntity entity, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. In Week 8, you will write the EF Core code to update an existing entity.
        throw new NotImplementedException("Week 8: Implement repository update operation.");
    }

    /// <summary>
    /// This method will contain the logic to delete a task from the database.
    /// TODO Week 8: Remove a task. Return without error when entity is already deleted.
    /// </summary>
    public Task DeleteAsync(TaskEntity entity, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. In Week 8, you will write the EF Core code to remove an entity.
        throw new NotImplementedException("Week 8: Implement repository delete operation.");
    }
}