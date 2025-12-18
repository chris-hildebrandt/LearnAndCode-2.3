// This file defines another custom exception, `TaskNotFoundException`.
// This exception is designed to be thrown when a specific task cannot be found in the database.

// Why is this better than just returning `null` from a method?
// 1.  **Explicit Error Context:** It provides a clear, explicit signal that something went wrong and why.
//     A `null` return can be ambiguous—was the task not found, or did some other error occur?
// 2.  **Centralized Error Handling:** Just like with `DomainValidationException`, we can create a specific
//     "catch" block in our global exception handler. When this exception is caught, we know with certainty
//     that the correct response to send to the client is an HTTP 404 Not Found.
// 3.  **Rich Error Information:** We can add properties to our custom exception (like `TaskId` here)
//     to carry more context about the error, which is very useful for logging and debugging.

namespace TaskFlowAPI.Exceptions;

/// <summary>
/// Represents an error that occurs when a requested task cannot be found.
/// This allows for specific handling of "not found" scenarios in the application, typically resulting in an HTTP 404 response.
/// Week 10: thrown when a task is not found. Provides consistent error handling for the API.
/// </summary>
public class TaskNotFoundException : Exception
{
    /// <summary>
    /// Creates a new instance of the TaskNotFoundException.
    /// </summary>
    /// <param name="taskId">The ID of the task that could not be found.</param>
    public TaskNotFoundException(int taskId)
        // The base constructor is called with a formatted error message.
        // The `$` before the string indicates an "interpolated string", which allows embedding variables directly.
        : base($"Task with id {taskId} was not found.")
    {
        // We store the ID of the missing task in a public property for easy access.
        TaskId = taskId;
    }

    /// <summary>
    /// Gets the ID of the task that was not found.
    /// This is useful for logging or creating detailed error responses.
    /// </summary>
    public int TaskId { get; }
}