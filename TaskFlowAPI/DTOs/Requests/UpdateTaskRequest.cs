// This file defines the Data Transfer Object (DTO) for updating an existing task.
// Like the CreateTaskRequest, this class defines the shape of the data a client can send.

// This DTO is often used for HTTP PUT or PATCH requests.
// By making all properties nullable, we allow clients to perform partial updates (PATCH).
// For example, a client might only want to update the `Title` of a task without changing its `Description` or `Priority`.
// If a property is `null` in the incoming request, our business logic can interpret that as "no change requested" for that field.

namespace TaskFlowAPI.DTOs.Requests;

/// <summary>
/// Represents the data a client can provide to update an existing task.
/// All properties are nullable to support partial updates (e.g., via an HTTP PATCH request).
/// Incoming payload for updating tasks. Week 10 will add validation.
/// </summary>
public class UpdateTaskRequest
{
    /// <summary>
    /// The new title for the task. If null, the title will not be updated.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The new description for the task. If null, the description will not be updated.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The new priority for the task. If null, the priority will not be updated.
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// The new due date for the task. If null, the due date will not be updated.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// The new completion status for the task. If null, the status will not be updated.
    /// </summary>
    public bool? IsCompleted { get; set; }

    /// <summary>
    /// The new project ID for the task. If null, the project will not be updated.
    /// </summary>
    public int? ProjectId { get; set; }
}