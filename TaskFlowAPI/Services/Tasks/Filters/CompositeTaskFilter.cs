// This file implements the Composite Design Pattern for our task filters.
// The Composite pattern is a structural design pattern that lets you compose objects into
// tree-like structures and then work with these structures as if they were individual objects.

// In our case, this `CompositeTaskFilter` allows us to combine multiple individual filters
// (like StatusFilter, PriorityFilter, etc.) into a single, unified filter.
// This is a powerful demonstration of the Open/Closed Principle: we can create new
// filtering combinations without changing any of the existing filter classes.

using TaskFlowAPI.Entities; // We need access to the TaskEntity to apply filters to it.

namespace TaskFlowAPI.Services.Tasks.Filters;

/// <summary>
/// This class represents a composite filter that can hold and apply multiple ITaskFilter instances.
/// It also implements the ITaskFilter interface, so from the outside, it looks just like a single filter.
/// This allows a client to treat a single object and a composition of objects uniformly.
/// Week 12: Combine multiple filters without modifying existing ones (Open/Closed Principle).
/// </summary>
public class CompositeTaskFilter : ITaskFilter
{
    // A private, read-only collection to hold the individual filter instances.
    private readonly IReadOnlyCollection<ITaskFilter> _filters;

    /// <summary>
    /// The constructor takes a collection of ITaskFilter objects.
    /// This is how we "compose" our composite filter. We pass it all the individual filters it should manage.
    /// </summary>
    /// <param name="filters">An enumerable of ITaskFilter objects to be included in this composite.</param>
    public CompositeTaskFilter(IEnumerable<ITaskFilter> filters)
    {
        // This line safely initializes the _filters collection.
        // If the incoming 'filters' is null, it creates a new empty list.
        // Otherwise, it converts the incoming IEnumerable to a List.
        _filters = filters?.ToList() ?? new List<ITaskFilter>();
    }

    /// <summary>
    /// This is the method that applies the combined filtering logic.
    /// It will check if a given task matches the criteria of ALL the individual filters it contains.
    /// </summary>
    /// <param name="task">The TaskEntity to check.</param>
    /// <returns>True if the task matches all filters, otherwise false.</returns>
    public bool IsMatch(TaskEntity task)
    {
        // This is a placeholder for the Week 12 assignment.
        // The goal is to implement the logic that iterates through all the filters in the `_filters` collection
        // and ensures that the `IsMatch` method of every single one returns true for the given task.
        // TODO Week 12: Ensure all filters return true.
        throw new NotImplementedException("Week 12: Implement CompositeTaskFilter.IsMatch");
    }
}