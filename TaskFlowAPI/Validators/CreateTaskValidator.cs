// This file defines a "Validator" for our `CreateTaskRequest` DTO.
// The purpose of a validator is to check if the data sent by a client is valid and complete
// before we try to process it. This is a crucial part of building a robust and secure API.

// We are using a popular third-party library called `FluentValidation`.
// FluentValidation provides a clean, readable, and powerful way to define validation rules using a "fluent" syntax.
// This is generally preferred over using data annotation attributes (like `[Required]`) directly on DTOs
// because it keeps the validation logic separate from the data-carrying object and allows for more complex rules.

using FluentValidation; // Imports the core FluentValidation library.
using TaskFlowAPI.DTOs.Requests; // Imports the DTO that this class will validate.

namespace TaskFlowAPI.Validators;

/// <summary>
/// This class defines the validation rules for the `CreateTaskRequest` DTO.
/// It inherits from `AbstractValidator<T>`, where `T` is the type of the object to be validated.
/// FluentValidation will automatically discover this validator and apply it when a `CreateTaskRequest` is received by a controller.
/// Week 10 scaffolding: implement validation rules with FluentValidation.
/// </summary>
public class CreateTaskValidator : AbstractValidator<CreateTaskRequest>
{
    // CODE SMELL: Comments (Clean Code Ch 17, p. 297)
    // This comment explains what code should do, but the code doesn't exist yet.
    // Once validation rules are implemented, this comment becomes redundant.
    // Refactor by: Remove comment once code is implemented (code should be self-explanatory).
    /// <summary>
    /// The constructor is where you define all the validation rules for the object.
    /// </summary>
    public CreateTaskValidator()
    {
        // The validation rules are defined here using a fluent, chainable syntax.
        // For example, a rule for the Title property might look like this:
        // RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required.");
        // RuleFor(x => x.Title).MaximumLength(200).WithMessage("Title cannot be longer than 200 characters.");

        // TODO Week 10: Add real rules (Title required, Priority range, DueDate validations, etc.).
        // Leave empty for now so Week 2-9 assignments can run without validation blocking progress.
    }
}