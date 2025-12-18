using TaskFlowAPI.Entities;

namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// Write operations for modifying tasks.
/// Use for: create/update/delete services, import jobs, admin operations.
///
/// TODO Week 14: Interface Segregation Principle (ISP)
/// Move the *write* method signatures from <see cref="ITaskRepository"/> into this interface.
/// - CreateAsync
/// - UpdateAsync
/// - DeleteAsync
///
/// Important: Copy the full method signatures (return types + parameters) from <see cref="ITaskRepository"/>.
/// </summary>
public interface ITaskWriter
{
    // TODO Week 14: Add CreateAsync(...) signature here

    // TODO Week 14: Add UpdateAsync(...) signature here

    // TODO Week 14: Add DeleteAsync(...) signature here
}
