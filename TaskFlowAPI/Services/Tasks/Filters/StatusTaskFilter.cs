// This file defines our final specific filter: one that filters tasks based on their completion status.
// This is another "Concrete Strategy" class that implements the ITaskFilter interface.
// Its single responsibility is to determine if a task is complete or incomplete.

using TaskFlowAPI.Entities; // We need access to the TaskEntity to check its IsCompleted property.

namespace TaskFlowAPI.Services.Tasks.Filters;

/// <summary>
/// A specific implementation of ITaskFilter that checks if a task's completion status
/// matches the desired status (either complete or incomplete).
/// Week 12: Filter tasks by status. Students will finish implementation.
/// </summary>
public class StatusTaskFilter : ITaskFilter
{
    // A private field to store the desired completion status to filter by.
    // `true` means we are looking for completed tasks, `false` means we are looking for incomplete tasks.
    private readonly bool _completed;

    /// <summary>
    /// The constructor for the filter.
    /// </summary>
    /// <param name="completed">The completion status to filter for (true for completed, false for incomplete).</param>
    public StatusTaskFilter(bool completed)
    {
        _completed = completed;
    }

    /// <summary>
    /// This method contains the actual filtering logic.
    /// It checks if the provided task's `IsCompleted` property matches the `_completed` value stored in this filter.
    /// </summary>
    /// <param name="task">The TaskEntity to check.</param>
    /// <returns>True if the task's status matches, otherwise false.</returns>
    public bool IsMatch(TaskEntity task)
    {
        // This is a placeholder for the Week 12 assignment.
        // The goal is to implement the logic that compares the task's `IsCompleted` property
        // with the `_completed` field of this class.
        // TODO Week 12: Implement filter using TaskEntity state.
        throw new NotImplementedException("Week 12: Implement StatusTaskFilter.IsMatch");
    }
}