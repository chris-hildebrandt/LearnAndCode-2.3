// This file defines a specific filter for our tasks: one that filters by a due date range.
// This class is an example of a "Concrete Strategy" if you think in terms of the Strategy Design Pattern.
// It provides a specific algorithm (filtering by date) that conforms to the general ITaskFilter interface.
// This is also a great example of the Single Responsibility Principle: this class has only one job,
// which is to determine if a task's due date falls within a specified range.

using TaskFlowAPI.Entities; // We need access to the TaskEntity to check its DueDate property.

namespace TaskFlowAPI.Services.Tasks.Filters;

// CODE SMELL: Data Clumps (Clean Code Ch 17, p. 291)
// These two parameters (start, end) are always passed together and represent a single concept: "date range".
// Refactor by: Create DateRange class to encapsulate these parameters.
/// <summary>
/// A specific implementation of ITaskFilter that checks if a task's due date
/// falls within a given start and end date range.
/// Week 12: Filter tasks by due date range.
/// </summary>
public class DueDateTaskFilter : ITaskFilter
{
    // Private fields to store the start and end of the date range for the filter.
    // They are nullable (DateTime?) which means they can be null if a start or end date isn't provided.
    private readonly DateTime? _start;
    private readonly DateTime? _end;

    /// <summary>
    /// The constructor for the filter.
    /// It accepts nullable DateTime objects for the start and end of the desired date range.
    /// </summary>
    /// <param name="start">The start date of the range. A task's due date must be on or after this date.</param>
    /// <param name="end">The end date of the range. A task's due date must be on or before this date.</param>
    public DueDateTaskFilter(DateTime? start, DateTime? end)
    {
        _start = start;
        _end = end;
    }

    /// <summary>
    /// This method contains the actual filtering logic.
    /// It checks if the provided task's DueDate falls within the [_start, _end] range.
    /// </summary>
    /// <param name="task">The TaskEntity to check.</param>
    /// <returns>True if the task is a match, otherwise false.</returns>
    public bool IsMatch(TaskEntity task)
    {
        // This is a placeholder for the Week 12 assignment.
        // The goal is to implement the logic that checks the task's DueDate against the _start and _end fields.
        // You'll need to handle cases where _start or _end might be null.
        // For example, if _start is null, it means you should match all tasks due before _end.
        // If both are null, it should probably match any task that has a due date.
        // TODO Week 12: Implement date range filtering.
        throw new NotImplementedException("Week 12: Implement DueDateTaskFilter.IsMatch");
    }
}