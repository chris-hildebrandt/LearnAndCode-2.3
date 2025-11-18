// This file defines the database "Entity" for a task.
// This `TaskEntity` class represents a single task in our `Tasks` table.
// It is the most detailed and authoritative data model for a task in our system.
// Entity Framework (EF) Core uses this class to interact with the `Tasks` table in the database.

namespace TaskFlowAPI.Entities;

/// <summary>
/// Represents a single task record in the database. This is a direct mapping to the `Tasks` table.
/// Week 7 scaffolding: this entity is intentionally anemic so students can add encapsulation.
/// Right now everything is public and there is almost no behavior.
/// By the end of Week 7 this class should expose private state, guard invariants,
/// and provide methods for domain behaviors (complete, reschedule, reprioritize, etc.).
/// </summary>
public class TaskEntity
{
    // TODO Week 7: Replace these auto-properties with properly encapsulated members.
    
    /// <summary>
    /// The unique identifier and Primary Key for the task.
    /// </summary>
    public int Id { get; set; }

    // TODO Week 7: Enforce non-empty titles and move validation into rich domain behavior.
    /// <summary>
    /// The title of the task. This is a required field.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// An optional, detailed description of the task.
    /// </summary>
    public string? Description { get; set; }

    // TODO Week 7: Replace primitive priority with value object or guarded property.
    /// <summary>
    /// A numerical priority for the task.
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// The optional date that the task is due.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// A flag indicating if the task has been completed.
    /// </summary>
    public bool IsCompleted { get; set; }

    /// <summary>
    /// The timestamp of when the task was created. Defaults to the current UTC time.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The timestamp of when the task was marked as complete. Null if not completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    // These two properties work together to define the relationship to the ProjectEntity.

    /// <summary>
    /// This is the "foreign key" property.
    /// It holds the ID of the Project that this Task belongs to.
    /// EF Core uses this to create the relationship between the Tasks and Projects tables in the database.
    /// </summary>
    public int ProjectId { get; set; }

    /// <summary>
    /// This is a "navigation property".
    /// It allows us to easily access the related ProjectEntity object from a TaskEntity instance.
    /// For example, you could get the project name via `myTask.Project.Name`.
    /// The `?` indicates that it can be null, though in a valid database state, it should be populated if ProjectId has a value.
    /// </summary>
    public ProjectEntity? Project { get; set; }

    // TODO Week 7: Add domain behaviors like Complete, Reopen, UpdateDetails, etc.
}