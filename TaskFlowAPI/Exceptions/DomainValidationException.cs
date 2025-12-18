// This file defines a custom exception class.
// A "custom exception" is a special class we create to represent a very specific type of error in our application.
// While .NET provides many built-in exception types (like `ArgumentNullException` or `InvalidOperationException`),
// creating our own allows us to handle certain error scenarios more precisely.

// This `DomainValidationException` is designed to be thrown whenever an operation violates a business rule
// (a "domain" rule). For example, if a user tries to create a task with an empty title, our validation
// logic can throw this specific exception. This is more descriptive than throwing a generic `Exception`.

// Later, we can set up a global exception handler that specifically "catches" `DomainValidationException`
// and returns a user-friendly HTTP 400 Bad Request response to the client.

namespace TaskFlowAPI.Exceptions;

/// <summary>
/// Represents an error that occurs when a business rule or domain logic is violated.
/// Inheriting from the base `Exception` class is the standard way to create a custom exception.
/// Week 10 scaffolding: Throw when business validation fails.
/// Students will wire this into global exception handling middleware.
/// </summary>
public class DomainValidationException : Exception
{
    /// <summary>
    /// Creates a new instance of the DomainValidationException with a specified error message.
    /// The `base(message)` call passes the message to the base `Exception` class.
    /// </summary>
    /// <param name="message">The message that describes the error.</param>
    public DomainValidationException(string message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new instance of the DomainValidationException with a specified error message
    /// and a reference to the inner exception that is the cause of this exception.
    /// This is useful for wrapping a lower-level exception with a more specific, high-level one
    /// without losing the details of the original error.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public DomainValidationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}