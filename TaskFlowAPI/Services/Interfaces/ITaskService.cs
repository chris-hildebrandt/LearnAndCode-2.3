// This file defines an "Interface".
// An interface is like a contract. It defines WHAT a class must do, but not HOW it does it.
// This ITaskService interface declares all the operations (methods) that any class implementing it must provide.
// This is a core concept of "programming to an interface, not an implementation," which makes our code
// flexible and loosely coupled. For example, we could swap out our main TaskService for a different one
// for testing purposes, and the controller wouldn't know the difference as long as the new class
// honors this contract.

// We need to use the DTOs (Data Transfer Objects) that our service methods will receive or return.
using TaskFlowAPI.DTOs.Requests;
using TaskFlowAPI.DTOs.Responses;

namespace TaskFlowAPI.Services.Interfaces;

// WEEK 2: These names are intentionally terrible. Fix them all.

/// <summary>
/// This is the ITaskService interface, which acts as a contract for our TaskService.
/// It defines the set of public operations that are available for managing tasks.
/// The controller will depend on this interface, not the concrete TaskService class.
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Defines a method for retrieving all tasks.
    /// </summary>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains a list of TaskDto objects.</returns>
    Task<List<TaskDto>> GetAll(CancellationToken cancellationToken = default); // TODO: Rename to reveal intent.

    /// <summary>
    /// Defines a method for retrieving a single task by its unique identifier.
    /// </summary>
    /// <param name="id">The ID of the task to retrieve.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>
    /// A Task that represents the asynchronous operation.
    /// The task result contains the TaskDto if found, or null if no task with the given ID exists.
    /// The 'TaskDto?' syntax indicates that the result is "nullable" (it can be null).
    /// </returns>
    Task<TaskDto?> Get(int id, CancellationToken cancellationToken = default); // TODO: Rename arguments too.

    /// <summary>
    /// Defines a method for creating a new task.
    /// </summary>
    /// <param name="request">The data needed to create the new task, wrapped in a CreateTaskRequest object.</param>
    /// <param name="cancellationToken">A token to allow the operation to be cancelled.</param>
    /// <returns>A Task that represents the asynchronous operation. The task result contains the newly created TaskDto.</returns>
    Task<TaskDto> Add(CreateTaskRequest request, CancellationToken cancellationToken = default);

    // WEEK 4 TODO: Add UpdateTaskAsync and DeleteTaskAsync method signatures
    // 
    // UpdateTaskAsync:
    //   - Return type: Task<TaskDto?> (nullable because task might not exist)
    //   - Parameters: int taskId, UpdateTaskRequest request, CancellationToken cancellationToken = default
    //   - Follow the async naming convention (ends with Async)
    // 
    // DeleteTaskAsync:
    //   - Return type: Task (void async - nothing to return)
    //   - Parameters: int taskId, CancellationToken cancellationToken = default
    //   - Remember: Delete operations are idempotent (safe to call multiple times)
    //
    // Model your signatures after the existing methods above (GetAll, Get, Add).
}