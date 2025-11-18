// This file defines a Data Transfer Object (DTO).
// A DTO is a simple class whose only purpose is to transfer data between different layers or parts of an application.
// They are used to shape the data that is sent to or from your API.

// This specific DTO, `CreateTaskRequest`, defines the exact structure of the data (the "payload")
// that a client must send in the body of an HTTP POST request when they want to create a new task.
// It separates the API's public contract from the internal database models (Entities).

namespace TaskFlowAPI.DTOs.Requests;

/// <summary>
/// Represents the data required from a client to create a new task.
/// The properties here are nullable (`?`) to allow for more flexible validation.
/// For example, we can provide a more user-friendly error message like "Title is required"
/// instead of the system throwing an error because a non-nullable property was missing.
/// Incoming payload for creating tasks. Validators will be implemented in Week 10.
/// </summary>
public class CreateTaskRequest
{
    /// <summary>
    /// The title of the new task. It is nullable to allow for validation.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// A detailed description of the new task. Can be null if not provided.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The priority level of the task. Nullable.
    /// </summary>
    public int? Priority { get; set; }

    /// <summary>
    /// The date the task is due. Can be null.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// The ID of the project this task belongs to. Can be null.
    /// </summary>
    public int? ProjectId { get; set; }
}