// ============================================================================
// INTERFACE SEGREGATION PRINCIPLE (ISP) - TaskFlow Example
// ============================================================================

// BEFORE (Fat Interface - ISP Violation):
// ----------------------------------------
// Problem: This interface forces clients to depend on methods they don't use.
// For example, a read-only report service must implement write methods it never calls.

namespace TaskFlowAPI.Repositories.Interfaces;

public interface ITaskRepository  // ❌ Violates ISP - Too many responsibilities
{
    // Read operations
    Task<List<TaskEntity>> GetAllAsync();
    Task<TaskEntity?> GetByIdAsync(int id);
    
    // Write operations
    Task<TaskEntity> CreateAsync(TaskEntity entity);
    Task UpdateAsync(TaskEntity entity);
    Task DeleteAsync(TaskEntity entity);
}

// Why this is problematic:
// 1. Report services only need read methods but get write methods too
// 2. Bulk import services need write methods but get read methods too
// 3. Mock objects in tests must implement ALL methods even if only testing one
// 4. Interface changes affect ALL clients, even those that don't use changed methods

// ============================================================================

// AFTER (Segregated Interfaces - ISP Compliant):
// -----------------------------------------------
// Solution: Split into focused interfaces based on client needs

namespace TaskFlowAPI.Repositories.Interfaces;

/// <summary>
/// Read-only operations for querying tasks.
/// Use this for: Reports, dashboards, query services, read-only APIs
/// </summary>
public interface ITaskReader
{
    Task<List<TaskEntity>> GetAllAsync();
    Task<TaskEntity?> GetByIdAsync(int id);
}

/// <summary>
/// Write operations for modifying tasks.
/// Use this for: Create/Update/Delete services, import jobs, admin operations
/// </summary>
public interface ITaskWriter
{
    Task<TaskEntity> CreateAsync(TaskEntity entity);
    Task UpdateAsync(TaskEntity entity);
    Task DeleteAsync(TaskEntity entity);
}

// ============================================================================

// Implementation (one class, two interfaces):
// --------------------------------------------

namespace TaskFlowAPI.Repositories;

public class TaskRepository : ITaskReader, ITaskWriter
{
    private readonly TaskFlowDbContext _context;
    
    public TaskRepository(TaskFlowDbContext context)
    {
        _context = context;
    }
    
    // ITaskReader implementation
    public async Task<List<TaskEntity>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .OrderBy(t => t.Priority)
            .ThenBy(t => t.DueDate)
            .ToListAsync();
    }
    
    public async Task<TaskEntity?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);
    }
    
    // ITaskWriter implementation
    public async Task<TaskEntity> CreateAsync(TaskEntity entity)
    {
        _context.Tasks.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    
    public async Task UpdateAsync(TaskEntity entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(TaskEntity entity)
    {
        _context.Tasks.Remove(entity);
        await _context.SaveChangesAsync();
    }
}

// ============================================================================

// Client Usage Examples:
// ----------------------

// Example 1: Read-only report service
public class TaskReportService
{
    private readonly ITaskReader _taskReader;  // ✓ Only depends on what it needs
    
    public TaskReportService(ITaskReader taskReader)
    {
        _taskReader = taskReader;
    }
    
    public async Task<TaskReport> GenerateReport()
    {
        var tasks = await _taskReader.GetAllAsync();
        // Generate report using read-only data
        return new TaskReport(tasks);
    }
}

// Example 2: Write-focused import service
public class TaskImportService
{
    private readonly ITaskWriter _taskWriter;  // ✓ Only depends on what it needs
    
    public TaskImportService(ITaskWriter taskWriter)
    {
        _taskWriter = taskWriter;
    }
    
    public async Task ImportTasks(List<TaskEntity> tasks)
    {
        foreach (var task in tasks)
        {
            await _taskWriter.CreateAsync(task);
        }
    }
}

// Example 3: Service that needs both (like TaskService)
public class TaskService
{
    private readonly ITaskReader _taskReader;
    private readonly ITaskWriter _taskWriter;
    
    public TaskService(ITaskReader taskReader, ITaskWriter taskWriter)
    {
        _taskReader = taskReader;
        _taskWriter = taskWriter;
    }
    
    public async Task<TaskDto> GetByIdAsync(int id)
    {
        var task = await _taskReader.GetByIdAsync(id);
        return MapToDto(task);
    }
    
    public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
    {
        var entity = MapToEntity(request);
        var created = await _taskWriter.CreateAsync(entity);
        return MapToDto(created);
    }
}

// ============================================================================

// DI Registration (in Program.cs):
// ---------------------------------

// Register concrete class once
builder.Services.AddScoped<TaskRepository>();

// Register as ITaskReader (factory delegates to same instance)
builder.Services.AddScoped<ITaskReader>(sp => 
    sp.GetRequiredService<TaskRepository>());

// Register as ITaskWriter (factory delegates to same instance)
builder.Services.AddScoped<ITaskWriter>(sp => 
    sp.GetRequiredService<TaskRepository>());

// Why this pattern?
// 1. One concrete instance (efficient - no duplicate objects)
// 2. Two interface registrations (flexible - clients get what they need)
// 3. Clients only depend on methods they use
// 4. Easy to swap implementations per interface later

// ============================================================================

// Benefits of ISP:
// ----------------
// 1. Flexibility: Clients depend only on methods they actually use
// 2. Testability: Mocks only need to implement relevant methods
// 3. Maintainability: Changes to write methods don't affect read-only clients
// 4. Clarity: Interface names communicate intent (Reader vs Writer)
// 5. Security: Can restrict services to read-only if they don't need writes
// 6. Scalability: Easy to add specialized interfaces (e.g., ITaskBulkWriter)

// ============================================================================

// Red Flags (Signs you need ISP):
// --------------------------------
// 1. Interface has > 5-7 methods
// 2. Clients implement methods by throwing NotImplementedException
// 3. Mock objects in tests have lots of empty/unused methods
// 4. Interface has obvious groupings (CRUD, read/write, admin/user)
// 5. Different clients use completely different subsets of methods
