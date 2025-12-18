// This file defines the `ITaskFilter` interface.
// This is the core of the **Strategy Design Pattern** for our filtering logic.

// The Strategy pattern is a behavioral design pattern that enables selecting an algorithm at runtime.
// Instead of implementing a single algorithm directly, code receives runtime instructions as to which in a
// family of algorithms to use.

// In our case, this interface defines a common contract for all our different filtering "strategies".
// Any class that knows how to filter a task (e.g., by status, by priority, by due date) will
// implement this interface. This allows us to treat all filters uniformly, and even combine them,
// without needing to know the specific details of each filter.

using TaskFlowAPI.Entities; // We need to reference the TaskEntity, as that is the object our filters will operate on.

namespace TaskFlowAPI.Services.Tasks.Filters;

/// <summary>
/// This interface is the contract for any task filtering strategy.
/// It declares a single method, `IsMatch`, that all concrete filter classes must implement.
/// Week 12 scaffolding: students will implement strategy pattern instances for filtering tasks.
/// </summary>
public interface ITaskFilter
{
    /// <summary>
    /// The single method that all filter strategies must implement.
    /// It takes a task and returns true if the task meets the filter's criteria, and false otherwise.
    /// </summary>
    /// <param name="task">The TaskEntity object to evaluate.</param>
    /// <returns>A boolean indicating whether the task is a match for the filter.</returns>
    bool IsMatch(TaskEntity task);
}