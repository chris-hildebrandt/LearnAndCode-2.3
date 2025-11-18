# TaskFlowAPI: Current State Analysis

## 1. Overview

The initial state of the TaskFlowAPI project is a minimal ASP.NET Core Web API with a basic structure. It includes a single controller, a service interface, and placeholder implementations for the repository and service layers. The project is intentionally left incomplete to serve as a starting point for the 23-week clean code curriculum.

## 2. Key Components

*   **`TasksController`:** The entry point for API requests related to tasks. It handles HTTP GET, POST, PUT, and DELETE requests. The initial implementation has poor naming and is not fully implemented.
*   **`ITaskService`:** The interface that defines the contract for the task service. It includes methods for getting, adding, updating, and deleting tasks.
*   **`TaskService`:** The initial implementation of `ITaskService`. The methods in this class throw `NotImplementedException`, as the business logic is to be implemented by the students.
*   **`ITaskRepository`:** The interface that defines the contract for the task repository.
*   **`TaskRepository`:** The initial implementation of `ITaskRepository`. The methods in this class throw `NotImplementedException`, as the data access logic is to be implemented by the students.
*   **`TaskEntity` and `ProjectEntity`:** The database entities that map to the `Tasks` and `Projects` tables.
*   **`TaskFlowDbContext`:** The Entity Framework Core `DbContext` for interacting with the database.
*   **`Program.cs`:** The main entry point of the application, where services are configured and the application is launched.

## 3. Architecture

The initial architecture follows a basic N-tier pattern, with a clear separation of concerns between the controller, service, and repository layers. However, the implementation is incomplete, and the focus of the initial weeks is on improving the code quality and implementing the core functionality.

## 4. UML Diagram (Initial State)

The following UML diagram illustrates the initial structure of the TaskFlowAPI project.

```plantuml
@startuml
' skinparam to make it look better
skinparam classAttributeIconSize 0
skinparam style strictuml
skinparam class {
    BackgroundColor PaleGreen
    ArrowColor SeaGreen
    BorderColor SeaGreen
}
hide empty members

class TasksController {
    - readonly ITaskService _taskService
    + TasksController(ITaskService taskService)
    + async Task<IActionResult> GetAllTasks(CancellationToken ct)
    + async Task<IActionResult> GetTaskById(int id, CancellationToken ct)
    + async Task<IActionResult> CreateTask(CreateTaskRequest request, CancellationToken ct)
    + async Task<IActionResult> UpdateTask(int id, UpdateTaskRequest request, CancellationToken ct)
    + async Task<IActionResult> DeleteTask(int id, CancellationToken ct)
}

interface ITaskService {
    + Task<List<TaskDto>> GetAll(CancellationToken ct)
    + Task<TaskDto?> Get(int id, CancellationToken ct)
    + Task<TaskDto> Add(CreateTaskRequest request, CancellationToken ct)
    + Task Update(int id, UpdateTaskRequest request, CancellationToken ct)
    + Task Delete(int id, CancellationToken ct)
}

class TaskService {
    - readonly ITaskRepository _taskRepository
    - readonly ILogger<TaskService> _logger
    + TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
    ' Methods throw NotImplementedException
}

interface ITaskRepository {
    + Task<List<TaskEntity>> GetAllAsync(CancellationToken ct)
    + Task<TaskEntity?> GetByIdAsync(int id, CancellationToken ct)
    + Task<TaskEntity> CreateAsync(TaskEntity entity, CancellationToken ct)
    + Task UpdateAsync(TaskEntity entity, CancellationToken ct)
    + Task DeleteAsync(TaskEntity entity, CancellationToken ct)
}

class TaskRepository {
    - readonly TaskFlowDbContext _dbContext
    + TaskRepository(TaskFlowDbContext dbContext)
    ' Methods throw NotImplementedException
}

class TaskEntity {
    + int Id
    + string Title
    + string? Description
    + int Priority
    + DateTime? DueDate
    + bool IsCompleted
    + int ProjectId
    + ProjectEntity? Project
}

class ProjectEntity {
    + int Id
    + string Name
    + string? Description
    + ICollection<TaskEntity> Tasks
}

class TaskFlowDbContext {
    + DbSet<TaskEntity> Tasks
    + DbSet<ProjectEntity> Projects
}

TasksController --> ITaskService
TaskService .up.|> ITaskService
TaskService --> ITaskRepository
TaskRepository .up.|> ITaskRepository
TaskRepository --> TaskFlowDbContext
TaskRepository -- TaskEntity
TaskFlowDbContext -- TaskEntity
TaskFlowDbContext -- ProjectEntity
TaskEntity -- ProjectEntity
@enduml
```
