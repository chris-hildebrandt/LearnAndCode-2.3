using TaskFlowAPI.Entities;

namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// Read-only operations for querying tasks.
/// Use for: reports, dashboards, query services, read-only APIs.
///
/// TODO Week 14: Interface Segregation Principle (ISP)
/// Move the *read* method signatures from <see cref="ITaskRepository"/> into this interface.
/// - GetAllAsync
/// - GetByIdAsync
///
/// Important: Copy the full method signatures (return types + parameters) from <see cref="ITaskRepository"/>.
/// </summary>
public interface ITaskReader
{
    // TODO Week 14: Add GetAllAsync(...) signature here

    // TODO Week 14: Add GetByIdAsync(...) signature here
}
