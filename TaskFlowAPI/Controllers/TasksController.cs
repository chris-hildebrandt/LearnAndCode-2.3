// This file defines the "Controller" for our tasks.
// In an ASP.NET Core Web API, a controller is like a traffic cop for incoming web requests.
// It receives requests from clients (like a web browser or a mobile app), figures out what the client wants to do,
// interacts with the necessary services to get the job done, and then sends back a response.
// This particular controller handles everything related to "Tasks".

// These 'using' statements are like importing libraries or modules in other languages.
// They allow us to use classes and functionalities defined elsewhere in our project or in the .NET framework.
using Microsoft.AspNetCore.Mvc; // Imports the core ASP.NET Core MVC framework features, like ControllerBase, [ApiController], etc.
using TaskFlowAPI.DTOs.Requests; // Imports the request objects (Data Transfer Objects) we'll use, like CreateTaskRequest.
using TaskFlowAPI.Services.Interfaces; // Imports the ITaskService interface, which defines the contract for our task-related business logic.

// A namespace is a way to organize code and prevent naming conflicts.
// All the code within this 'TaskFlowAPI.Controllers' namespace is grouped together.
namespace TaskFlowAPI.Controllers;

// WEEK 2 ASSIGNMENT: Meaningful Names
//
// CODE SMELL: Abbreviations & Unclear Names (Clean Code Ch 2, pp. 17-48)
// This controller WORKS but has TERRIBLE names that violate every principle of meaningful naming.
// Problems to identify and fix:
// 1. Abbreviations that obscure intent: svc (service), s (service?), t (task), dt (data/datetime?), req (request, but confusing)
// 2. Single-letter variables: 't' (ambiguous), 's' (ambiguous), 'dt' (is this DateTime or DataTable?  Confusing!)
// 3. Method names that don't reveal intention: Get() (get what?), GetOne() (one what?), Add() (add what?)
// 4. Inconsistent naming style: some use abbreviations, some don't
// 5. Disinformation: abbreviations can mean multiple things (dt = DateTime OR DataTransfer?)
//
// Refactor by: Apply Clean Code Chapter 2 principles:
// - Replace abbreviations with full names (svc → taskService, t → taskDto, dt → ???)
// - Use searchable names (can find 'svc' everywhere, which is noise)
// - Make distinctions meaningful (what's the difference between 't' and 'taskDto'?)
// - Use pronounceable names (spell out 'taskRepository' so people can discuss it)
// - Method names should be verb phrases (GetAllTasks, not Get; CreateTask, not Add)
// - Parameter names should clarify intent (taskId instead of id, createRequest instead of req)
//
// Success criteria:
// - Every name reveals intention without needing a comment
// - No abbreviations except standard conventions (HTTP, API, ID, DTO)
// - Method names are action verbs + object (Get → GetAllTasks, GetOne → GetTaskById)
// - All variable/parameter names are searchable and pronounceable
// - No single-letter variables (except loop iterators like 'i')
// - Code should be readable aloud to another developer
//
// Time estimate: 50 minutes
// - Identify all bad names: 10 min
// - Refactor controller: 30 min
// - Refactor service interface: 10 min
//
// When done: Run tests (dotnet test) - they should still pass!

// [ApiController] is an attribute that enables several helpful API-specific features,
// like automatic model validation and more descriptive error responses.
[ApiController]
// [Route("api/[controller]")] defines the URL pattern for this controller.
// "api/" is a literal prefix.
// "[controller]" is a placeholder that gets replaced with the controller's name (in this case, "Tasks").
// So, the base URL for this controller will be "https://your-api-url.com/api/Tasks".
[Route("api/[controller]")]
public class TasksController : ControllerBase // Our class 'TasksController' inherits from 'ControllerBase', the standard base class for API controllers.
{
    // This is a private field to hold a reference to our task service.
    // The 'readonly' keyword means it can only be assigned a value when it's declared or in the constructor.
    // This is a common pattern for "Dependency Injection".
    private readonly ITaskService svc; // TODO: Fix this name!

    // This is the constructor for the TasksController. It's called when a new instance of the controller is created.
    // The 'ITaskService s' parameter is an example of DEPENDENCY INJECTION.
    // Instead of the controller creating its own TaskService, the ASP.NET Core framework "injects" (provides) one for us.
    // This makes our code more modular and easier to test.
    public TasksController(ITaskService s) // TODO: Fix this name!
    {
        // We assign the injected service to our private field so we can use it in our methods below.
        svc = s;
    }

    // This method is called an "Action Method". It handles HTTP GET requests.
    // The [HttpGet] attribute tells ASP.NET that this method should handle GET requests to the controller's base URL (e.g., "api/Tasks").
    // TODO: Get what? All tasks? Be specific!
    [HttpGet]
    // It's an 'async' method that returns a 'Task<IActionResult>'.
    // 'async' and 'await' allow for non-blocking operations, which is crucial for web server performance.
    // 'IActionResult' is an interface that represents the result of an action method (e.g., Ok, NotFound, BadRequest).
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        // We call the service layer to get the data. The controller's job is not to know HOW to get the tasks,
        // but to know WHO to ask. In this case, it asks the ITaskService.
        var t = await svc.GetAll(ct); // TODO: Fix the variable and method names.
        
        // We wrap the result in an 'Ok' response, which sends the data back to the client with an HTTP 200 OK status code.
        return Ok(t);
    }

    // This action method also handles HTTP GET requests, but for a specific task.
    // The [HttpGet("{id}")] attribute means it handles requests to URLs like "api/Tasks/1", "api/Tasks/2", etc.
    // The "{id}" part is a route parameter that gets mapped to the 'id' parameter in the method.
    // TODO: 'GetOne' one what?
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id, CancellationToken ct)
    {
        // We ask the service for a single task by its ID.
        var t = await svc.Get(id, ct);

        // It's important to handle cases where the requested item doesn't exist.
        if (t == null)
        {
            // If the service returns null, we send back an HTTP 404 Not Found response.
            return NotFound();
        }

        // If the task is found, we send it back with an HTTP 200 OK status code.
        return Ok(t);
    }

    // This action method handles HTTP POST requests, which are typically used for creating new resources.
    // The [HttpPost] attribute maps this method to POST requests sent to "api/Tasks".
    [HttpPost]
    // The [FromBody] attribute tells ASP.NET to get the 'CreateTaskRequest' data from the body of the incoming HTTP request.
    // The client will send this data as JSON.
    public async Task<IActionResult> Add([FromBody] CreateTaskRequest req, CancellationToken ct)
    {
        // Basic validation to ensure the client sent something.
        if (req == null)
        {
            // If the request body is empty, we send back an HTTP 400 Bad Request response.
            return BadRequest("request body required");
        }

        // We call the service to create the new task, passing the data we received from the client.
        var dt = await svc.Add(req, ct);

        // A best practice for POST requests is to return an HTTP 201 Created status code.
        // 'CreatedAtAction' does this and also includes a "Location" header in the response,
        // which points to the URL of the newly created resource (using the 'GetOne' action).
        return CreatedAtAction(nameof(GetOne), new { id = dt.Id }, dt);
    }

    // WEEK 4 TODO: Add UpdateTask and DeleteTask action methods
    //
    // UpdateTask:
    //   - Attribute: [HttpPut("{id}")]
    //   - Method name: UpdateTask
    //   - Parameters: int id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken
    //   - Call: await _taskService.UpdateTaskAsync(id, request, cancellationToken)
    //   - Return: NoContent() for HTTP 204 status
    //
    // DeleteTask:
    //   - Attribute: [HttpDelete("{id}")]
    //   - Method name: DeleteTask
    //   - Parameters: int id, CancellationToken cancellationToken
    //   - Call: await _taskService.DeleteTaskAsync(id, cancellationToken)
    //   - Return: NoContent() for HTTP 204 status (idempotent operation)
    //
    // Both methods should follow the same async/await pattern as the Create method above.
    // After adding, check Swagger UI - you should see PUT /api/tasks/{id} and DELETE /api/tasks/{id} endpoints.
}