// This file defines the main Data Transfer Object (DTO) for a task.
// This is the object that our API will typically send back to clients when they ask for task information.

// Notice how this DTO is a "flattened" and curated version of our internal `TaskEntity`.
// For example, instead of nesting the entire `ProjectEntity` object within the task,
// we have included just the `ProjectId` and `ProjectName`.
// This is a deliberate choice to keep the API response clean, efficient, and focused on the task itself.

// Using DTOs like this is a best practice because it decouples our API's public contract
// from our internal database structure. We can change the `TaskEntity` internally, but as long
// as we can still correctly map it to this `TaskDto`, our external clients won't be affected.

namespace TaskFlowAPI.DTOs.Responses;

/// <summary>
/// Represents the data for a single task that is sent back to the client.
/// This is the public-facing model for a task.
/// Returned from controllers. Students will ensure naming stays clean starting Week 2.
/// </summary>
public class TaskDto
{
    /// <summary>
    /// The unique identifier for the task.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The title of the task.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The detailed description of the task. Can be null.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The priority level of the task (e.g., 1 for high, 2 for medium).
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// The date the task is due. Can be null if there is no due date.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// A boolean indicating whether the task has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// The timestamp of when the task was originally created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The timestamp of when the task was completed. Can be null if it's not yet completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// The ID of the project this task belongs to.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// The name of the project this task belongs to. This is a good example of "flattening" data for a DTO.
    /// </summary>
    public string ProjectName { get; set; } = string.Empty;
}