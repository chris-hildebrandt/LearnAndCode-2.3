// This file defines the validator for our `UpdateTaskRequest` DTO.
// Validation for an update operation can sometimes be different or more complex than for a create operation.

// For example, when creating a task, you might require the `Title` to be present.
// However, when updating a task, a client might only send a `Description` to update, omitting the `Title`.
// In this case, you wouldn't want the validation to fail just because the `Title` is null.
// Instead, your rules might state that IF a title is provided, it must not be empty.

// This validator allows us to define these nuanced, update-specific rules.

using FluentValidation; // Imports the core FluentValidation library.
using TaskFlowAPI.DTOs.Requests; // Imports the DTO that this class will validate.

namespace TaskFlowAPI.Validators;

/// <summary>
/// This class defines the validation rules for the `UpdateTaskRequest` DTO.
/// It allows for different logic than the `CreateTaskValidator`, which is often necessary for update operations.
/// Week 10 scaffolding: Students implement update-specific validation rules here.
/// </summary>
// CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
// This validator will likely have similar rule definitions (property validation chains) as CreateTaskValidator.
// For example, both validate Title length, Priority range, and DueDate validity.
// Only the conditions (required vs. optional) differ between create and update.
// Refactor by: Extract common validation rules into a shared base validator or static validation methods.
public class UpdateTaskValidator : AbstractValidator<UpdateTaskRequest>
{
    /// <summary>
    /// The constructor is where you define all the validation rules for the update request.
    /// </summary>
    public UpdateTaskValidator()
    {
        // Rules here might be different from the CreateTaskValidator.
        // For example, you might only validate properties that are not null in the request.
        // e.g., `RuleFor(x => x.Title).NotEmpty().When(x => x.Title != null);`
        // This rule says: "If the title property is included in the request, then it must not be empty."

        // Another common rule for updates is to ensure that the request is not completely empty,
        // meaning the client is at least trying to update *something*.

        // TODO Week 10: Require at least one property to change, validate transitions, etc.
    }
}