// This file defines the "Interface" for our Task Repository.
// Just like the ITaskService, this interface acts as a contract.
// It specifies all the data access operations that must be available for tasks.

// Why have an interface for the repository?
// 1.  **Decoupling:** The Service layer (TaskService) will depend on this `ITaskRepository` interface, not the
//     concrete `TaskRepository` class. This means we can change our data access technology (e.g., move from
//     Entity Framework to another tool like Dapper, or even a different database) without having to change
//     the Service layer. We would just need to create a new class that implements this same interface.
// 2.  **Testability:** When we write unit tests for our `TaskService`, we don't want to connect to a real database.
//     Instead, we can create a "mock" or "fake" version of this interface that just returns pre-defined data from memory.
//     This makes our tests fast, reliable, and independent of external systems.

using TaskFlowAPI.Entities; // We need to reference the TaskEntity, as that is the object this repository will manage.

namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// This is the ITaskRepository interface, which defines the contract for data access operations related to tasks.
/// Any class that is responsible for saving, retrieving, or modifying task data in the database must implement this interface.
/// Week 8 focus: implement this contract in <see cref="TaskRepository"/>.
/// Week 14: students will segregate this interface into reader/writer variations.
/// </summary>
public interface ITaskRepository
{
    /// <summary>
    /// Defines a method for retrieving all tasks from the data source.
    /// </summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task that resolves to a List of all TaskEntity objects.</returns>
    Task<List<TaskEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines a method for finding a single task by its unique ID.
    /// </summary>
    /// <param name="id">The ID of the task to find.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task that resolves to the found TaskEntity, or null if it doesn't exist.</returns>
    Task<TaskEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines a method for adding a new task to the data source.
    /// </summary>
    /// <param name="entity">The TaskEntity object to add.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task that resolves to the newly created TaskEntity (often with the ID populated by the database).</returns>
    Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines a method for updating an existing task in the data source.
    /// </summary>
    /// <param name="entity">The TaskEntity object with updated values.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task representing the completion of the asynchronous update operation.</returns>
    Task UpdateAsync(TaskEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Defines a method for deleting a task from the data source.
    /// </summary>
    /// <param name="entity">The TaskEntity object to delete.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task representing the completion of the asynchronous delete operation.</returns>
    Task DeleteAsync(TaskEntity entity, CancellationToken cancellationToken = default);
}