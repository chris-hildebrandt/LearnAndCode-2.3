# Week 22: Performance & Caching

This week, we will focus on the principles of concurrent programming and performance optimization, emphasizing how these concepts align with In Time Tec's commitment to delivering high-quality, scalable solutions.

## 1. Learning Objectives

- Understand the principles of concurrent programming and performance optimization.
- Apply best practices for concurrency and performance optimization in your project.
- Analyze your project for potential concurrency and performance improvements.
- Implement changes to improve concurrency and performance in your project.
- Discuss the relationship between concurrency, performance, and In Time Tec's commitment to delivering high-quality, scalable solutions.
- Apply basic performance improvements (async, caching, response compression).
- Instrument simple metrics/logs to validate impact.
- Ensure caching invalidation flows through repository/service layers.

## 2. Reading & Resources (50 min)

- **Clean Code Chapter 13: Concurrency** – Mindset for safe performance tweaks.
- **Microsoft docs: In-memory caching (`IMemoryCache`)**.
- **Async best practices in ASP.NET Core**.

## 3. This Week’s Work

- Add in-memory caching for `GetAllTasksAsync` results (keyed by filter parameters + pagination).
- Ensure cache invalidated on create/update/delete.
- Confirm all repository/service methods use `async/await` (no `.Result` or `.Wait`).
- Enable response compression middleware if not already active (verify config).

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Tasks/Interfaces` if adding caching abstraction.
- `TaskFlowAPI/Program.cs` (cache configuration).
- Optional: add `TaskFlowAPI/Infrastructure/Caching/ITaskCache.cs`, `MemoryTaskCache.cs`.
- Update tests to account for caching (e.g., verifying invalidation).
- This file (`Course-Materials/Weekly-Modules/week-22-performance-caching.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-22/<your-name>`.
2. Design caching strategy: create abstraction `ITaskCache` to get/set cached `PagedResponse` by filter signature.
3. Implement `MemoryTaskCache` using `IMemoryCache` with TTL (e.g., 60 seconds) and configurable options.
4. Inject cache into `TaskService`; apply when fetching tasks.
5. Invalidate cache on create/update/delete operations.
6. Verify all repository/service methods use `async/await` (no leftover synchronous calls).
7. Confirm `app.UseResponseCompression()` is enabled and configure MIME types if needed.
8. Add logging around cache hits/misses for observability.
9. Run build/tests + manual GET to verify caching (log output confirms hits).

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Manual: call `GET /api/v1/tasks` twice and confirm second call logs cache hit.

## 7. Success Criteria

- Cache abstraction introduced; no direct `IMemoryCache` usage outside infrastructure class.
- Cache invalidated after any write operations.
- Async patterns consistent (no synchronous blocking).
- Build/tests succeed; logs show cache activity.

## 8. Submission Process

- Commit `Week 22 – caching & performance`.
- PR summary includes caching strategy, TTL, and sample logs.
- Weekly issue documents performance observations (stopwatch results optional).

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Cache Strategy:* Outline your cache key structure and invalidation triggers.
- *Async Audit:* Note any synchronous calls you replaced and the impact on readability/performance.

### Discussion Prep:
- *How did caching change the service design?*
- *What risks exist if cache invalidation fails?*
- *What metrics would you add in a production environment?*
- *How will you test caching behaviour in automated builds?*

## 10. Time Estimate

- 50 min – Reading.
- 15 min – Design caching strategy.
- 45 min – Implement cache + async audit.
- 15 min – Testing/log verification + PR/issue.
**Total:** ~125 minutes.

## 11. Caching Examples & Patterns

### Define ITaskCache Interface

```csharp
namespace TaskFlowAPI.Infrastructure.Caching;

/// <summary>
/// Abstraction for caching task query results
/// </summary>
public interface ITaskCache
{
    /// <summary>
    /// Gets cached value by key
    /// </summary>
    Task<PagedResponse<TaskDto>?> GetAsync(string key);
    
    /// <summary>
    /// Sets value in cache with optional TTL (time-to-live)
    /// </summary>
    Task SetAsync(string key, PagedResponse<TaskDto> value, TimeSpan? ttl = null);
    
    /// <summary>
    /// Removes specific key from cache
    /// </summary>
    Task RemoveAsync(string key);
    
    /// <summary>
    /// Clears all cached entries
    /// </summary>
    Task ClearAsync();
}
```

### Implement MemoryTaskCache

```csharp
using Microsoft.Extensions.Caching.Memory;

namespace TaskFlowAPI.Infrastructure.Caching;

public class MemoryTaskCache : ITaskCache
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<MemoryTaskCache> _logger;
    private static readonly TimeSpan DefaultTtl = TimeSpan.FromSeconds(60);
    
    public MemoryTaskCache(IMemoryCache cache, ILogger<MemoryTaskCache> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    
    public Task<PagedResponse<TaskDto>?> GetAsync(string key)
    {
        if (_cache.TryGetValue(key, out PagedResponse<TaskDto>? value))
        {
            _logger.LogInformation("Cache hit for key: {Key}", key);
            return Task.FromResult(value);
        }
        
        _logger.LogInformation("Cache miss for key: {Key}", key);
        return Task.FromResult<PagedResponse<TaskDto>?>(null);
    }
    
    public Task SetAsync(string key, PagedResponse<TaskDto> value, TimeSpan? ttl = null)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = ttl ?? DefaultTtl
        };
        
        _cache.Set(key, value, options);
        _logger.LogInformation("Cached value for key: {Key} with TTL: {Ttl}", 
            key, ttl ?? DefaultTtl);
        
        return Task.CompletedTask;
    }
    
    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        _logger.LogInformation("Removed cache key: {Key}", key);
        return Task.CompletedTask;
    }
    
    public Task ClearAsync()
    {
        // Note: IMemoryCache doesn't have a Clear() method
        // Options: 1) Track keys, 2) Create wrapper, 3) Restart app
        // For this example, we'll log a warning
        _logger.LogWarning("MemoryCache does not support clearing all entries");
        return Task.CompletedTask;
    }
}
```

### Cache Key Generation

```csharp
private string GenerateCacheKey(ITaskFilter? filter, int page, int pageSize)
{
    // Simple approach: combine filter + pagination into unique key
    var filterKey = filter?.GetHashCode().ToString() ?? "all";
    return $"tasks_{filterKey}_p{page}_s{pageSize}";
}

// Alternative: More robust key generation
private string GenerateCacheKey(TaskQueryParams queryParams)
{
    var parts = new List<string>
    {
        "tasks",
        $"p{queryParams.Page}",
        $"s{queryParams.PageSize}"
    };
    
    if (queryParams.Status.HasValue)
        parts.Add($"status{queryParams.Status}");
        
    if (queryParams.Priority.HasValue)
        parts.Add($"priority{queryParams.Priority}");
        
    if (queryParams.DueBefore.HasValue)
        parts.Add($"duebefore{queryParams.DueBefore:yyyyMMdd}");
    
    return string.Join("_", parts);
}
```

### TTL (Time-To-Live) Guidelines

```csharp
public static class CacheTtl
{
    /// <summary>
    /// Short TTL for frequently changing data (tasks)
    /// </summary>
    public static readonly TimeSpan Short = TimeSpan.FromSeconds(30);
    
    /// <summary>
    /// Medium TTL for relatively stable data
    /// </summary>
    public static readonly TimeSpan Medium = TimeSpan.FromMinutes(5);
    
    /// <summary>
    /// Long TTL for rarely changing data
    /// </summary>
    public static readonly TimeSpan Long = TimeSpan.FromHours(1);
}
```

**For tasks:** 60 seconds balances freshness and performance

- Too short (< 10s): Cache benefits minimal
- Too long (> 5 min): Stale data risk
- Sweet spot (30-60s): Good balance for active applications

### Usage in TaskService

```csharp
public class TaskService
{
    private readonly ITaskReader _taskReader;
    private readonly ITaskWriter _taskWriter;
    private readonly ITaskCache _cache;
    private readonly ILogger<TaskService> _logger;
    
    public async Task<PagedResponse<TaskDto>> GetAllTasksAsync(
        ITaskFilter? filter, 
        int page, 
        int pageSize)
    {
        // Generate cache key
        var cacheKey = GenerateCacheKey(filter, page, pageSize);
        
        // Try cache first
        var cached = await _cache.GetAsync(cacheKey);
        if (cached != null)
        {
            return cached;
        }
        
        // Cache miss - fetch from database
        _logger.LogInformation("Fetching tasks from database");
        var tasks = await _taskReader.GetAllAsync();
        
        // Apply filters
        if (filter != null)
        {
            tasks = tasks.Where(t => filter.IsMatch(t)).ToList();
        }
        
        // Apply pagination
        var pagedResult = ApplyPagination(tasks, page, pageSize);
        
        // Store in cache
        await _cache.SetAsync(cacheKey, pagedResult, TimeSpan.FromSeconds(60));
        
        return pagedResult;
    }
}
```

### Cache Invalidation Pattern

```csharp
public async Task<TaskDto> CreateTaskAsync(CreateTaskRequest request)
{
    var entity = _factory.CreateNewTask(request);
    var created = await _taskWriter.CreateAsync(entity);
    
    // Invalidate all cached task lists
    await _cache.ClearAsync();
    
    _logger.LogInformation("Task created, cache invalidated");
    
    return _mapper.ToDto(created);
}

public async Task UpdateTaskAsync(int id, UpdateTaskRequest request)
{
    var existing = await _taskReader.GetByIdAsync(id);
    
    if (existing == null)
        throw new TaskNotFoundException(id);
    
    // Update logic...
    await _taskWriter.UpdateAsync(existing);
    
    // Invalidate cache
    await _cache.ClearAsync();
    
    return _mapper.ToDto(existing);
}

public async Task DeleteTaskAsync(int id)
{
    var existing = await _taskReader.GetByIdAsync(id);
    
    if (existing == null)
        return; // Idempotent delete
    
    await _taskWriter.DeleteAsync(existing);
    
    // Invalidate cache
    await _cache.ClearAsync();
}
```

**Cache Invalidation Strategies:**

1. **Clear All** (simple): Remove all cached entries on any write
   - Pros: Simple, no stale data
   - Cons: Throws away good cache entries

2. **Selective** (advanced): Remove only affected entries
   - Pros: Preserves unrelated cache
   - Cons: Complex key tracking

3. **TTL-based** (passive): Let cache expire naturally
   - Pros: No invalidation code
   - Cons: Stale data risk

**For TaskFlow API:** Start with "Clear All" (simple), optimize later if needed

### DI Registration (Program.cs)

```csharp
// Register memory cache (built-in)
builder.Services.AddMemoryCache();

// Register our cache abstraction
builder.Services.AddScoped<ITaskCache, MemoryTaskCache>();

// Optional: Configure memory cache options
builder.Services.AddMemoryCache(options =>
{
    options.SizeLimit = 1024; // Limit cache size
    options.CompactionPercentage = 0.2; // Compact when 80% full
});
```

### Testing Cache Behavior

```csharp
[Fact]
public async Task GetAllTasks_CachesResult()
{
    // Arrange
    var mockCache = new Mock<ITaskCache>();
    mockCache.Setup(c => c.GetAsync(It.IsAny<string>()))
        .ReturnsAsync((PagedResponse<TaskDto>?)null); // Cache miss
    
    var service = new TaskService(mockReader, mockWriter, mockCache.Object, ...);
    
    // Act
    var result = await service.GetAllTasksAsync(null, 1, 20);
    
    // Assert
    mockCache.Verify(c => c.SetAsync(
        It.IsAny<string>(), 
        It.IsAny<PagedResponse<TaskDto>>(), 
        It.IsAny<TimeSpan?>()), 
        Times.Once);
}

[Fact]
public async Task GetAllTasks_ReturnsCachedResult_WhenAvailable()
{
    // Arrange
    var cachedResult = new PagedResponse<TaskDto> { /* ... */ };
    
    var mockCache = new Mock<ITaskCache>();
    mockCache.Setup(c => c.GetAsync(It.IsAny<string>()))
        .ReturnsAsync(cachedResult); // Cache hit
    
    var mockReader = new Mock<ITaskReader>();
    var service = new TaskService(mockReader.Object, mockWriter, mockCache.Object, ...);
    
    // Act
    var result = await service.GetAllTasksAsync(null, 1, 20);
    
    // Assert
    Assert.Same(cachedResult, result);
    mockReader.Verify(r => r.GetAllAsync(), Times.Never); // Database not called
}
```

### Monitoring Cache Effectiveness

```csharp
// Add metrics/logging to track cache performance
private int _cacheHits = 0;
private int _cacheMisses = 0;

public async Task<PagedResponse<TaskDto>?> GetAsync(string key)
{
    if (_cache.TryGetValue(key, out PagedResponse<TaskDto>? value))
    {
        Interlocked.Increment(ref _cacheHits);
        _logger.LogInformation("Cache hit rate: {HitRate:P}", 
            (double)_cacheHits / (_cacheHits + _cacheMisses));
        return value;
    }
    
    Interlocked.Increment(ref _cacheMisses);
    return null;
}
```

## 12. Additional Resources

- **[Concurrency and Performance Example](../Examples/ConcurrencyAndPerformance.md)**
- **[Introduction to Concurrent Programming: A Beginner's Guide](https://www.toptal.com/software/introduction-to-concurrent-programming)** – Optional concurrency refresher.
- **[MIT: Concurrency](https://web.mit.edu/6.005/www/fa14/classes/17-concurrency/)** – Optional deep dive for those investigating threading.
- **[Concurrency in JavaScript](https://www.honeybadger.io/blog/javascript-concurrency/#:~:text=JavaScript's%20concurrency%20model%20is%20based,promises%2C%20or%20async%2Fawait.)**
- **[Concurrency Part 1](https://www.youtube.com/watch?v=f99zSz5D5RE&list=PL-uROEx3vAxg-yricXrDaOK9xzHGGQk1u&index=13)**
- **[Concurrency Part 2](https://www.youtube.com/watch?v=199dVfVnpMo&list=PL-uROEx3vAxg-yricXrDaOK9xzHGGQk1u&index=14)**
