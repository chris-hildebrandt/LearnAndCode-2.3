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

## 11. Additional Resources

- **[Concurrency and Performance Example](../Examples/ConcurrencyAndPerformance.md)**
- **[Introduction to Concurrent Programming: A Beginner's Guide](https://www.toptal.com/software/introduction-to-concurrent-programming)** – Optional concurrency refresher.
- **[MIT: Concurrency](https://web.mit.edu/6.005/www/fa14/classes/17-concurrency/)** – Optional deep dive for those investigating threading.
- **[Concurrency in JavaScript](https://www.honeybadger.io/blog/javascript-concurrency/#:~:text=JavaScript's%20concurrency%20model%20is%20based,promises%2C%20or%20async%2Fawait.)**
- **[Concurrency Part 1](https://www.youtube.com/watch?v=f99zSz5D5RE&list=PL-uROEx3vAxg-yricXrDaOK9xzHGGQk1u&index=13)**
- **[Concurrency Part 2](https://www.youtube.com/watch?v=199dVfVnpMo&list=PL-uROEx3vAxg-yricXrDaOK9xzHGGQk1u&index=14)**
