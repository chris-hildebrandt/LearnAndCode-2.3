// This file defines a database "Entity".
// An Entity is a class that directly maps to a table in our database.
// Each property in the class typically corresponds to a column in the table, and each instance
// of the class corresponds to a row in the table.

// This `ProjectEntity` class represents a single project in our `Projects` table.
// Entity Framework (EF) Core uses this class to understand the structure of the `Projects` table,
// and to create, read, update, and delete project records.

// These classes are the foundational data models for the entire application.

namespace TaskFlowAPI.Entities;

/// <summary>
/// Represents a project in the database. This is a direct mapping to the `Projects` table.
/// Week 7 scaffolding: students will enrich this entity with validation and helper methods.
/// </summary>
// CODE SMELL: Data Class (Clean Code Ch 17, p. 295)
// This entity has only getters/setters with no behavior or domain logic.
// It's a pure data container without domain methods (AddTask, RemoveTask, UpdateName, etc.).
// Refactor by: Add domain behavior methods like AddTask, RemoveTask, UpdateDescription, etc.
//
// CODE SMELL: Feature Envy (Clean Code Ch 17, p. 291)
// This class doesn't provide methods to work with its own name.
// Other classes might implement validation/formatting of the name externally.
// Refactor by: Add methods like ValidateName(), UpdateName(), GetDisplayName() to encapsulate the behavior.
public class ProjectEntity
{
    // TODO Week 7: Protect invariants (non-empty name, optional description length, etc.).
    
    /// <summary>
    /// The unique identifier for the project. This corresponds to the Primary Key in the database table.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The name of the project. This is a required field.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// An optional, detailed description of the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// This is a "navigation property".
    /// It defines the relationship between a Project and its Tasks.
    /// EF Core uses this to understand that a Project can have a collection of many Tasks.
    /// This allows us to easily load all tasks associated with a project.
    /// It's initialized to an empty list to prevent null reference exceptions.
    /// </summary>
    public ICollection<TaskEntity> Tasks { get; set; } = new List<TaskEntity>();

    // TODO Week 7: Introduce helper methods (AddTask, RemoveTask) once encapsulation is implemented.
}