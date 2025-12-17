// This file defines an "Extension Method" to set up our custom global exception handling.

// An "Extension Method" is a special kind of static method that allows you to "add" new methods
// to existing types without creating a new derived type. Here, we are adding a new method called
// `UseTaskFlowExceptionHandler` to the `WebApplication` class.

// This is part of the ASP.NET Core "middleware" pipeline.
// Middleware components are pieces of software that are assembled into a pipeline to handle requests and responses.
// This particular middleware is designed to be a global error handler. It will catch any unhandled exceptions
// that occur during a request, log them, and format a standardized, user-friendly error response.

using Microsoft.AspNetCore.Diagnostics; // Provides the IExceptionHandlerFeature to access the exception.
using Microsoft.AspNetCore.Mvc; // Provides the ProblemDetails class for standardized error responses.
using TaskFlowAPI.Exceptions; // Imports our custom exception types.

namespace TaskFlowAPI.Extensions;

/// <summary>
/// A static class that holds our custom extension methods.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// This is the extension method. It adds a custom exception handler to the ASP.NET Core request pipeline.
    /// The `this WebApplication app` syntax is what makes it an extension method for the `WebApplication` class.
    /// Week 10: Students enable global error handling using this extension method.
    /// For now it simply surfaces the default developer exception page.
    /// </summary>
    /// <param name="app">The WebApplication instance to configure.</param>
    public static void UseTaskFlowExceptionHandler(this WebApplication app)
    {
        // TODO Week 10: Replace with custom exception handler middleware that translates domain exceptions into ProblemDetails responses.
        
        // `app.UseExceptionHandler` registers a middleware that will catch unhandled exceptions.
        app.UseExceptionHandler(exceptionApp =>
        {
            // The `Run` method defines the terminal action for this middleware branch.
            exceptionApp.Run(async context =>
            {
                // We first try to get information about the exception that occurred.
                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                var exception = exceptionHandlerFeature?.Error;

                // We use the `ProblemDetails` class to create a standardized, machine-readable error response.
                // This follows RFC 7807 (https://tools.ietf.org/html/rfc7807) and is a modern best practice for APIs.
                var problemDetails = new ProblemDetails
                {
                    Title = "An unexpected error occurred.",
                    Detail = exception?.Message,
                    Status = StatusCodes.Status500InternalServerError,
                    Type = "https://httpstatuses.com/500"
                };

                // Here, we check if the exception is one of our specific custom exceptions.
                // This allows us to provide more specific, meaningful error responses.
                if (exception is TaskNotFoundException notFound)
                {
                    // If the task wasn't found, we change the response to a 404 Not Found.
                    problemDetails.Title = "Task not found";
                    problemDetails.Detail = notFound.Message;
                    problemDetails.Status = StatusCodes.Status404NotFound;
                    problemDetails.Type = "https://httpstatuses.com/404";
                }
                // In a real application, you would add more `if` or `switch` blocks here
                // to handle other custom exceptions, like DomainValidationException (returning a 400 Bad Request).

                // We set the response content type and status code based on the problem details.
                context.Response.ContentType = "application/problem+json";
                context.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
                
                // Finally, we write the `ProblemDetails` object to the response body as JSON.
                //
                // TODO Week 22: Async best practices & performance fundamentals
                // - Propagate cancellation (RequestAborted) into async framework calls when available.
                // - Decide whether ConfigureAwait(false) is appropriate here (ASP.NET Core apps typically do not need it).
                await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken: context.RequestAborted)
                    .ConfigureAwait(false);
            });
        });
    }
}