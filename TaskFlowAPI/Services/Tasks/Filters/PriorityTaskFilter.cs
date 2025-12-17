// This file defines another specific filter: one that filters tasks by their priority level.
// Like the other filters, this class is a "Concrete Strategy" that implements the ITaskFilter interface.
// Its single responsibility is to determine if a task's priority is in a list of desired priorities.

using TaskFlowAPI.Entities; // We need access to the TaskEntity to check its Priority property.

namespace TaskFlowAPI.Services.Tasks.Filters;

/// <summary>
/// A specific implementation of ITaskFilter that checks if a task's priority
/// is present in a given set of allowed priorities.
/// Week 12: Filter tasks by allowed priority values.
/// </summary>
public class PriorityTaskFilter : ITaskFilter
{
    // This private field will hold the set of priority levels we want to match.
    // A HashSet is used here for performance. Checking if an item exists in a HashSet
    // is much faster (O(1) on average) than checking for it in a List (O(n)).
    private readonly HashSet<int> _priorities;

    /// <summary>
    /// The constructor for the filter.
    /// It takes a collection of integers representing the priority levels to filter by.
    /// </summary>
    /// <param name="priorities">An enumerable of integer priority levels to match.</param>
    public PriorityTaskFilter(IEnumerable<int> priorities)
    {
        // This safely initializes the HashSet. If the input is null, an empty HashSet is created.
        // Otherwise, the incoming collection of numbers is converted into a HashSet.
        _priorities = priorities?.ToHashSet() ?? new HashSet<int>();
    }

    /// <summary>
    /// This method contains the actual filtering logic.
    /// It checks if the provided task's Priority is in the `_priorities` HashSet.
    /// </summary>
    /// <param name="task">The TaskEntity to check.</param>
    /// <returns>True if the task's priority is in the allowed set, otherwise false.</returns>
    public bool IsMatch(TaskEntity task)
    {
        // This is a placeholder for the Week 12 assignment.
        // The goal is to implement the logic that checks if the `_priorities` HashSet contains the task's `Priority`.
        // You should also consider what to do if the `_priorities` set is empty.
        // TODO Week 12: Implement filter logic for priority.
        throw new NotImplementedException("Week 12: Implement PriorityTaskFilter.IsMatch");
    }
}