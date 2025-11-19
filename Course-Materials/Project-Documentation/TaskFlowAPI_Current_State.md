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

## 4. Architecture Diagrams

### 4.1 Current State Class Diagram (Mermaid)

The following Mermaid class diagram illustrates the initial structure of the TaskFlowAPI project.

classDiagram
    %% Controllers Layer
    class TasksController {
        -ITaskService _taskService
        +GetAllTasksAsync()
        +GetTaskByIdAsync()
        +CreateTaskAsync()
        +UpdateTaskAsync()
        +DeleteTaskAsync()
    }
    
    class ReportsController {
        -ITaskService _taskService
        +GenerateProjectSummaryReport()
    }
    
    %% Services Layer
    class ITaskService {
        <<interface>>
        +GetAll()
        +Get()
        +Add()
        +Update()
        +Delete()
    }
    
    class TaskService {
        -ITaskRepository _taskRepository
        -ILogger _logger
        +GetAll()
        +Get()
        +Add()
        +Update()
        +Delete()
        -MapToDto()
        -MapToEntity()
    }
    
    %% Repositories Layer
    class ITaskRepository {
        <<interface>>
        +GetAllAsync()
        +GetByIdAsync()
        +CreateAsync()
        +UpdateAsync()
        +DeleteAsync()
    }
    
    class TaskRepository {
        -TaskFlowDbContext _dbContext
        +GetAllAsync()
        +GetByIdAsync()
        +CreateAsync()
        +UpdateAsync()
        +DeleteAsync()
    }
    
    %% Data Layer
    class TaskFlowDbContext {
        +DbSet~TaskEntity~ Tasks
        +DbSet~ProjectEntity~ Projects
    }
    
    %% Entities
    class TaskEntity {
        +int Id
        +string Title
        +string Description
        +int Priority
        +DateTime? DueDate
        +bool IsCompleted
        +DateTime CreatedAt
        +DateTime? CompletedAt
        +int ProjectId
        +ProjectEntity Project
    }
    
    class ProjectEntity {
        +int Id
        +string Name
        +string Description
        +ICollection~TaskEntity~ Tasks
    }
    
    %% DTOs
    class TaskDto {
        +int Id
        +string Title
        +string Description
        +int Priority
        +DateTime? DueDate
        +bool IsCompleted
        +DateTime CreatedAt
        +DateTime? CompletedAt
        +int ProjectId
        +string ProjectName
    }
    
    class CreateTaskRequest {
        +string Title
        +string Description
        +int Priority
        +DateTime? DueDate
        +int? ProjectId
    }
    
    %% Relationships
    TasksController --> ITaskService : uses
    ReportsController --> ITaskService : uses
    TaskService ..|> ITaskService : implements
    TaskService --> ITaskRepository : uses
    TaskRepository ..|> ITaskRepository : implements
    TaskRepository --> TaskFlowDbContext : uses
    TaskFlowDbContext --> TaskEntity : contains
    TaskFlowDbContext --> ProjectEntity : contains
    TaskEntity --> ProjectEntity : belongs to
    TaskService --> TaskDto : creates
    TaskService --> CreateTaskRequest : accepts
```

### 4.2 Additional Diagrams

For more detailed architecture diagrams including data flow, sequence diagrams, and component diagrams, see:
- **[Complete Architecture Diagrams](../../../docs/architecture-diagrams.md)**
