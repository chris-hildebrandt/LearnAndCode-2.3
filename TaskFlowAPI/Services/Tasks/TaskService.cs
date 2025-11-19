// This file contains the "Service" layer for our tasks.
// A service class holds the core business logic of the application.
// Its job is to coordinate operations, enforce rules, and work with data repositories.
// It acts as the bridge between the web-facing Controller and the database-facing Repository.
// For example, the TaskService knows HOW to get all tasks, create a new task, etc.,
// by calling the appropriate methods on the TaskRepository.

using Microsoft.Extensions.Logging; // Imports the logging framework to allow us to write log messages.
using TaskFlowAPI.DTOs.Requests; // Imports the request DTOs.
using TaskFlowAPI.DTOs.Responses; // Imports the response DTOs.
using TaskFlowAPI.Entities; // Imports the database entity classes.
using TaskFlowAPI.Repositories.Interfaces; // Imports the repository interface.
using TaskFlowAPI.Services.Interfaces; // Imports the service interface this class will implement.

namespace TaskFlowAPI.Services.Tasks;

/// <summary>
/// This is the concrete implementation of the ITaskService interface.
/// It contains the actual business logic for managing tasks.
/// It depends on the ITaskRepository to interact with the database and ILogger to log information.
/// Week 9 scaffolding: methods throw until students implement them using repository + mapping helpers.
/// Week 11: students will refactor mapper/validator logic into dedicated classes.
/// </summary>
public class TaskService : ITaskService
{
    // Private, readonly fields to hold the dependencies that are injected through the constructor.
    private readonly ITaskRepository _taskRepository; // The repository for data access.
    private readonly ILogger<TaskService> _logger; // The logger for logging messages.

    // This is the constructor, where Dependency Injection happens.
    // The ASP.NET Core service container will provide an instance of ITaskRepository and ILogger<TaskService>.
    public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    /// <summary>
    /// This method will contain the logic to get all tasks.
    /// It implements the GetAll method defined in the ITaskService interface.
    /// TODO Week 9: Fetch all tasks, map to DTOs, and return as read-only list.
    /// Steps provided in assignment doc.
    /// </summary>
    public Task<List<TaskDto>> GetAll(CancellationToken cancellationToken = default)
    {
        // This line is a placeholder. It throws an exception to indicate that the method hasn't been implemented yet.
        // In Week 9, you will replace this with the actual logic to fetch tasks from the repository.
        throw new NotImplementedException("Week 9: Implement GetAll and return TaskDto list.");
    }

    /// <summary>
    /// This method will contain the logic to get a single task by its ID.
    /// It implements the Get method defined in the ITaskService interface.
    /// TODO Week 9: Fetch a single task. Throw or return null when not found (see doc for guidance).
    /// </summary>
    public Task<TaskDto?> Get(int id, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. You will implement the logic to fetch a specific task in Week 9.
        throw new NotImplementedException("Week 9: Implement Get and handle not-found behavior.");
    }

    /// <summary>
    /// This method will contain the logic to create a new task.
    /// It implements the Add method defined in the ITaskService interface.
    /// TODO Week 9: Validate request (temporary inline validation).
    /// Week 10 adds FluentValidation + custom exceptions.
    /// </summary>
    public Task<TaskDto> Add(CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        // This is a placeholder. You will implement the logic to add a new task in Week 9.
        throw new NotImplementedException("Week 9: Implement Add to create a new task.");
    }

    // Helper methods are used to encapsulate logic that is repeated within a class.
    // These two methods handle the conversion between database "Entity" objects and "DTO" (Data Transfer Objects).
    // This is an important separation: Entities map to the database, while DTOs map to what the outside world (the client) sees.
    // Helper methods intentionally private for Week 11 extraction into TaskMapper / TaskValidator classes.
    
    // CODE SMELL: Feature Envy (Clean Code Ch 17, p. 291)
    // This method accesses multiple properties of TaskEntity to create a DTO.
    // The mapping logic should be in a dedicated mapper class, not in the service.
    // Refactor by: Extract to TaskMapper class (Week 11 exercise).
    /// <summary>
    /// Maps a TaskEntity (database object) to a TaskDto (data transfer object).
    /// This is used to control what data about a task is sent back to the client.
    /// </summary>
    /// <param name="entity">The database entity to map.</param>
    /// <returns>A DTO suitable for sending to the client.</returns>
    internal TaskDto MapToDto(TaskEntity entity)
    {
        return new TaskDto
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Priority = entity.Priority,
            DueDate = entity.DueDate,
            IsCompleted = entity.IsCompleted,
            CreatedAt = entity.CreatedAt,
            CompletedAt = entity.CompletedAt,
            ProjectId = entity.ProjectId,
            // This is a null-conditional operator. If entity.Project is not null, it uses Project.Name.
            // Otherwise (??), it uses an empty string. This prevents errors if a task doesn't have a project.
            ProjectName = entity.Project?.Name ?? string.Empty
        };
    }

    // CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
    // This mapping pattern (property-by-property assignment) is similar to MapToDto above.
    // Both methods do data transformation and could share common logic.
    // Refactor by: Extract both to TaskMapper class to centralize mapping logic.
    /// <summary>
    /// Maps a CreateTaskRequest (DTO from the client) to a TaskEntity (database object).
    /// This is used to translate incoming data into a format that can be saved to the database.
    /// </summary>
    /// <param name="request">The request DTO from the client.</param>
    /// <returns>An entity ready to be saved to the database.</returns>
    internal TaskEntity MapToEntity(CreateTaskRequest request)
    {
        return new TaskEntity
        {
            Title = request.Title ?? string.Empty,
            Description = request.Description,
            Priority = request.Priority ?? 0,
            DueDate = request.DueDate,
            ProjectId = request.ProjectId ?? 1 // Default to project 1 if not provided.
        };
    }
}