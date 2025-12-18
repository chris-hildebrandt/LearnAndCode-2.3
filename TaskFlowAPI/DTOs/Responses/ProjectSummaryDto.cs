// This file defines a Data Transfer Object (DTO) that represents a summarized view of a project.
// Sometimes, a client doesn't need all the details of an object.
// For example, on a dashboard, you might want to show a list of projects with just their names
// and a count of their tasks, rather than sending all the tasks for every project.

// This is a key principle of API design: only send the data that the client actually needs.
// This `ProjectSummaryDto` is a lightweight object designed for exactly that purpose.
// It provides a high-level overview without the heavy details of the full ProjectEntity.

using System;

namespace TaskFlowAPI.DTOs.Responses
/// <summary>
/// Represents a lightweight, summarized view of a project.
/// This is useful for API endpoints that need to display a list of projects
/// without the overhead of including all associated tasks.
/// A lightweight summary of a project. To be implemented in Week 13.
/// </summary>
{
    public class ProjectSummaryDto
    {
        /// <summary>
    /// The unique identifier for the project.
    /// </summary>
        public int Id { get; set; }
        
        /// <summary>
    /// The name of the project.
    /// Initialized to `string.Empty` to avoid null reference issues.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The total number of tasks associated with this project.
    /// </summary>
        public int TotalTasks { get; set; }

    /// <summary>
    /// The number of tasks within this project that are marked as completed.
    /// </summary>
        public int CompletedTasks { get; set; }
        public decimal PercentageComplete { get; set; }
        public int OverdueTasks { get; set; }
        public int HighPriorityTasks { get; set; }
        public DateTime? NextUpcomingDeadline { get; set; }
    }
}
