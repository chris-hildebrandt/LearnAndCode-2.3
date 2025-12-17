// CODE SMELL: Large Class (Clean Code Ch 17, p. 294)
// This utility class has 12+ methods doing unrelated things:
// - String formatting
// - Date calculations  
// - Validation helpers
// - Mapping utilities
// - Priority formatting
// This violates Single Responsibility Principle - the class has multiple reasons to change.
// Refactor by: Split into focused classes (StringFormatter, DateHelper, ValidationHelper, PriorityFormatter).
namespace TaskFlowAPI.Helpers;

/// <summary>
/// Utility class with various helper methods for tasks.
/// CODE SMELL: This class violates SRP by mixing multiple concerns.
/// </summary>
public static class TaskHelper
{
    // CODE SMELL: Primitive Obsession (Clean Code Ch 17, p. 292)
    // Using int for priority (0-5) instead of a type-safe Priority enum or value object.
    // This allows invalid values and makes code less readable.
    // Refactor by: Create Priority enum or Priority value object class.
    /// <summary>
    /// Formats a priority value as a string label.
    /// </summary>
    public static string FormatPriority(int priority)
    {
        // CODE SMELL: Switch Statements (Clean Code Ch 17, p. 293)
        // Switch statement on priority value suggests polymorphism would be better.
        // Refactor by: Replace with Strategy pattern or Priority value object with behavior.
        return priority switch
        {
            0 => "None",
            1 => "Low",
            2 => "Medium",
            3 => "High",
            4 => "Urgent",
            5 => "Critical",
            _ => "Unknown"
        };
    }

    // CODE SMELL: Long Method (Clean Code Ch 17, p. 288)
    // This method is 25+ lines and does multiple things:
    // 1. Validates input
    // 2. Calculates date
    // 3. Formats output
    // 4. Handles edge cases
    // Refactor by: Extract smaller methods (Extract Method pattern).
    /// <summary>
    /// Calculates the due date based on start date and number of days.
    /// </summary>
    public static DateTime CalculateDueDate(DateTime startDate, int daysToAdd)
    {
        if (daysToAdd < 0)
        {
            throw new ArgumentException("Days to add must be non-negative");
        }

        if (daysToAdd == 0)
        {
            return startDate;
        }

        var calculatedDate = startDate.AddDays(daysToAdd);
        
        // Skip weekends
        while (calculatedDate.DayOfWeek == DayOfWeek.Saturday || calculatedDate.DayOfWeek == DayOfWeek.Sunday)
        {
            calculatedDate = calculatedDate.AddDays(1);
        }

        // Format for display
        var formatted = calculatedDate.ToString("yyyy-MM-dd");
        
        return DateTime.Parse(formatted);
    }

    // CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
    // This validation pattern (null check + throw) appears in 3 other methods in this class.
    // Extract to a shared validation method to follow DRY principle.
    /// <summary>
    /// Validates that a priority value is within acceptable range.
    /// </summary>
    public static bool IsValidPriority(int priority)
    {
        if (priority < 0)
        {
            throw new ArgumentException("Priority cannot be negative");
        }
        
        if (priority > 5)
        {
            throw new ArgumentException("Priority cannot exceed 5");
        }
        
        return priority >= 0 && priority <= 5;
    }

    // CODE SMELL: Long Parameter List (Clean Code Ch 17, p. 290)
    // This method has 6 parameters, violating the "fewer arguments are better" principle.
    // Multiple related parameters suggest they should be grouped into a parameter object.
    // Refactor by: Create TaskQueryParameters class to encapsulate filter parameters.
    /// <summary>
    /// Filters tasks based on multiple criteria.
    /// </summary>
    public static List<TaskDto> FilterTasks(
        List<TaskDto> tasks,
        bool? isCompleted,
        int? minPriority,
        int? maxPriority,
        DateTime? dueBefore,
        DateTime? dueAfter,
        int? projectId)
    {
        var filtered = tasks;

        if (isCompleted.HasValue)
        {
            filtered = filtered.Where(t => t.IsCompleted == isCompleted.Value).ToList();
        }

        if (minPriority.HasValue)
        {
            filtered = filtered.Where(t => t.Priority >= minPriority.Value).ToList();
        }

        if (maxPriority.HasValue)
        {
            filtered = filtered.Where(t => t.Priority <= maxPriority.Value).ToList();
        }

        if (dueBefore.HasValue)
        {
            filtered = filtered.Where(t => t.DueDate.HasValue && t.DueDate.Value < dueBefore.Value).ToList();
        }

        if (dueAfter.HasValue)
        {
            filtered = filtered.Where(t => t.DueDate.HasValue && t.DueDate.Value > dueAfter.Value).ToList();
        }

        if (projectId.HasValue)
        {
            filtered = filtered.Where(t => t.ProjectId == projectId.Value).ToList();
        }

        return filtered;
    }

    // CODE SMELL: Feature Envy (Clean Code Ch 17, p. 291)
    // This method accesses multiple properties of TaskDto to format a message.
    // The formatting logic should be on the DTO itself, not in a helper class.
    // Refactor by: Move formatting logic to TaskDto.FormatSummary() method.
    /// <summary>
    /// Formats a task summary string.
    /// </summary>
    public static string FormatTaskSummary(TaskDto task)
    {
        var status = task.IsCompleted ? "Completed" : "Pending";
        var priority = FormatPriority(task.Priority);
        var dueDate = task.DueDate?.ToString("yyyy-MM-dd") ?? "No due date";
        return $"{task.Title} - {status} - Priority: {priority} - Due: {dueDate}";
    }

    // CODE SMELL: Data Clumps (Clean Code Ch 17, p. 291)
    // These three parameters (startDate, endDate, includeCompleted) are always
    // passed together and represent a single concept: "date range filter".
    // Refactor by: Create DateRangeFilter class to encapsulate these parameters.
    /// <summary>
    /// Filters tasks by date range.
    /// </summary>
    public static List<TaskDto> FilterTasksByDateRange(
        List<TaskDto> tasks,
        DateTime? startDate,
        DateTime? endDate,
        bool includeCompleted)
    {
        var filtered = tasks;

        if (startDate.HasValue)
        {
            filtered = filtered.Where(t => !t.DueDate.HasValue || t.DueDate.Value >= startDate.Value).ToList();
        }

        if (endDate.HasValue)
        {
            filtered = filtered.Where(t => !t.DueDate.HasValue || t.DueDate.Value <= endDate.Value).ToList();
        }

        if (!includeCompleted)
        {
            filtered = filtered.Where(t => !t.IsCompleted).ToList();
        }

        return filtered;
    }

    // CODE SMELL: Dead Code (Clean Code Ch 17, p. 296)
    // This method is never called and serves no purpose.
    // Refactor by: Delete the method (or verify it's truly unused first).
    /// <summary>
    /// Old method for formatting task titles - no longer used.
    /// </summary>
    public static string OldFormatTitle(string title)
    {
        return title.ToUpper().Trim();
    }

    // CODE SMELL: Comments (Clean Code Ch 17, p. 297)
    // This commented-out code is dead code that should be deleted.
    // If you need it later, use version control history.
    // Refactor by: Delete commented code.
    // private static string OldMappingMethod(TaskEntity entity)
    // {
    //     // Old implementation, replaced by TaskMapper
    //     return new TaskDto { ... };
    // }

    // CODE SMELL: Lazy Class (Clean Code Ch 17, p. 295)
    // This class has only one simple method and doesn't justify its own class.
    // Refactor by: Inline the method into the calling class.
    /// <summary>
    /// Trims whitespace from a title string.
    /// </summary>
    public static string TrimTitle(string title)
    {
        return title.Trim();
    }

    // CODE SMELL: Speculative Generality (Clean Code Ch 17, p. 296)
    // This method was created "just in case" but is only used once.
    // The abstraction adds complexity without benefit.
    // Refactor by: Inline the method or remove if truly unused.
    /// <summary>
    /// Generic method for formatting any string - may be useful in the future.
    /// </summary>
    public static string FormatString(string input, string format)
    {
        return string.Format(format, input);
    }
}
