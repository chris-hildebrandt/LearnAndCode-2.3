// This file defines a generic Data Transfer Object (DTO) for sending paginated data back to a client.
// "Pagination" is the process of dividing a large set of data into smaller, discrete pages.
// It's essential for performance and user experience when dealing with long lists of items.
// Instead of sending all 10,000 tasks at once, we might send page 1 with 20 tasks.

// This `PagedResponse<T>` is "generic", which means it can wrap a list of any type of object.
// The `<T>` is a placeholder for a specific type that will be defined when the class is used.
// For example, we could have a `PagedResponse<TaskDto>` or a `PagedResponse<ProjectSummaryDto>`.
// This makes the class highly reusable throughout our application.

namespace TaskFlowAPI.DTOs.Responses;

/// <summary>
/// A generic wrapper for sending paginated API responses.
/// It includes not only the data for the current page but also metadata about the pagination itself.
/// Generic response for paginated data. Will be implemented in Week 13.
/// </summary>
/// <typeparam name="T">The type of the data items being paginated.</typeparam>
public class PagedResponse<T>
{
    /// <summary>
    /// The current page number.
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// The number of records requested per page.
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    // The total number of pages available.
    /// </summary>
    public int TotalPages { get; set; }

    /// <summary>
    /// The total number of records in the entire collection.
    /// </summary>
    public int TotalRecords { get; set; }

    /// <summary>
    /// The actual list of data for the current page.
    /// The `new()` initializes the list to an empty list to prevent it from ever being null.
    /// </summary>
    public List<T> Data { get; set; } = new();
}