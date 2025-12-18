# Week 22: Async Best Practices & Performance Fundamentals

This week is about the kind of "performance work" you will actually do on modern ASP.NET Core teams: making request flows reliably asynchronous, avoiding common async/await footguns, and applying a few simple habits that prevent outages and slowdowns.

We are intentionally **not** doing caching this week, and we are skipping Clean Code's concurrency chapter. Instead, we are using .NET-specific async guidance that directly supports the assignment work you'll do in this repo.

## 1. Learning Objectives

- Identify and fix common async/await anti-patterns in ASP.NET Core (sync-over-async, fire-and-forget, unnecessary `Task.Run`).
- Propagate `CancellationToken` correctly from HTTP request → controller → service/repository calls.
- Understand when to use async patterns and when synchronous code is more appropriate.
- Improve reliability and performance without adding new NuGet packages.

## 2. Reading & Resources (60 min)

- **[Async/Await Best Practices in Asynchronous Programming](https://learn.microsoft.com/en-us/archive/msdn-magazine/2013/march/async-await-best-practices-in-asynchronous-programming)** (20 min) - Stephen Cleary's canonical async/await primer.
- **[ConfigureAwait FAQ](https://devblogs.microsoft.com/dotnet/configureawait-faq/)** (15 min) - When ConfigureAwait(false) helps (and when it's unnecessary).
- **[Performance Best Practices in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/performance/performance-best-practices)** (25 min) - Official Microsoft guidance for async I/O and cancellation.

## 3. This Week's Work

- Fix intentionally-bad async code in `ReportsController` that compiles and "works" but violates best practices.
- Remove sync-over-async patterns (`.Result`, `.Wait()`).
- Fix cancellation token propagation (stop creating empty tokens).
- Remove unnecessary `Task.Run` usage in request handlers.
- Fix fire-and-forget async calls that lose exceptions.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/ReportsController.cs`
- This file (`Course-Materials/Weekly-Modules/week-22-async-best-practices.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-22-submission`.

2. **Read the async articles first (60 min).**
   
   Do not start "fixing code" until you understand what patterns to look for. Pay special attention to:
   - Stephen Cleary's "Async All the Way" principle.
   - When `.Result` and `.Wait()` cause problems.
   - How cancellation tokens flow through async call chains.
   - When `Task.Run` is appropriate (hint: rarely in ASP.NET Core).

3. **Locate the anti-patterns (10 min).**
   
   Open `TaskFlowAPI/Controllers/ReportsController.cs` and find all `// TODO Week 22:` comments.
   
   You should find **4 distinct anti-patterns** to fix:
   - Creating empty cancellation tokens.
   - Using `.Result` to block on async operations.
   - Unnecessary `Task.Run` wrapper.
   - Fire-and-forget async call.

4. **Task A: Fix Cancellation Token Propagation (20 min)**
   
   **Anti-Pattern:** Creating empty tokens instead of using the request token.
   
   ```csharp
   // BAD - ignores request cancellation
   var allTasks = await _taskService.GetAll(new CancellationToken());
   ```
   
   **What's Wrong:**
   - Creates a token that can never be cancelled.
   - Ignores the HTTP request's cancellation token (when user closes browser, etc.).
   - Wastes resources on abandoned requests.
   
   **Fix:**
   - ASP.NET Core automatically binds `CancellationToken cancellationToken` parameter to `HttpContext.RequestAborted`.
   - Replace `new CancellationToken()` with the `cancellationToken` parameter.
   - This allows the operation to be cancelled if the client disconnects.
   
   **Checkpoint:**
   - [ ] No `new CancellationToken()` calls remain in request handlers.
   - [ ] Method parameter `cancellationToken` is passed to all async framework calls.

5. **Task B: Remove Sync-Over-Async Pattern (25 min)**
   
   **Anti-Pattern:** Blocking on async operations with `.Result`.
   
   ```csharp
   // BAD - blocks thread waiting for async I/O
   var allTasksAgain = _taskService.GetAll(new CancellationToken()).Result;
   ```
   
   **What's Wrong:**
   - Blocks a thread pool thread waiting for I/O.
   - Can cause thread pool starvation under load.
   - Makes the async method pointless (you're not actually async anymore).
   - This specific example also duplicates work (calling GetAll twice!).
   
   **Fix:**
   - Remove the entire redundant call.
   - Use the already-awaited `allTasks` variable instead.
   - If you needed this call, you would `await` it instead of using `.Result`.
   
   **Checkpoint:**
   - [ ] No `.Result` calls remain in the method.
   - [ ] No `.Wait()` calls remain.
   - [ ] No `.GetAwaiter().GetResult()` calls remain.

6. **Task C: Fix Unnecessary Task.Run (20 min)**
   
   **Anti-Pattern:** Wrapping synchronous CPU work in `Task.Run` and then blocking on it.
   
   ```csharp
   // BAD - unnecessary Task.Run + blocking with .Result
   var completedTasks = CountCompletedTasksAsync(projectTasks, cancellationToken).Result;
   
   private Task<int> CountCompletedTasksAsync(List<TaskDto> tasks, CancellationToken ct)
   {
       return Task.Run(() => tasks.Count(t => t.IsCompleted), ct);
   }
   ```
   
   **What's Wrong:**
   - `Task.Run` is for offloading work to background threads (useful in UI apps).
   - In ASP.NET Core request handlers, you're already on a thread pool thread!
   - This adds overhead (queuing work, context switching) for no benefit.
   - Then blocking with `.Result` defeats any potential benefit anyway.
   - This is CPU-bound work that's fast — no need for async at all.
   
   **Fix (Recommended):** Make it synchronous
   ```csharp
   var completedTasks = CountCompletedTasks(projectTasks);
   
   private int CountCompletedTasks(List<TaskDto> tasks)
   {
       return tasks.Count(t => t.IsCompleted);
   }
   ```
   
   **Checkpoint:**
   - [ ] No `Task.Run` in request handler code.
   - [ ] CPU-bound work that's fast (< 50ms) is synchronous.
   - [ ] No `.Result` blocking on async methods.

7. **Task D: Fix Fire-and-Forget (20 min)**
   
   **Anti-Pattern:** Discarding async operations without awaiting them.
   
   ```csharp
   // BAD - fire-and-forget loses exceptions
   _ = SimulateAuditLogWriteAsync(projectId, cancellationToken);
   
   return Ok(summary);
   ```
   
   **What's Wrong:**
   - If `SimulateAuditLogWriteAsync` throws an exception, it's lost.
   - No guarantee the audit log is written before response is sent.
   - The `_` discard makes it look intentional, but it's still wrong.
   - The HTTP response might return before the work completes.
   
   **Fix:**
   ```csharp
   await SimulateAuditLogWriteAsync(projectId, cancellationToken);
   
   return Ok(summary);
   ```
   
   **When is fire-and-forget acceptable?**
   - Almost never in request handler code.
   - If you truly need background work, use proper background service patterns.
   - You must have explicit exception handling and lifetime management.
   
   **Checkpoint:**
   - [ ] No discarded tasks (`_ = SomeAsyncCall()`) in request handlers.
   - [ ] All async operations are awaited or explicitly handled.

8. **Verify Your Fixes (10 min)**
   
   After fixing all anti-patterns, use this checklist:
   
   **Code Review Checklist:**
   - [ ] `GenerateProjectSummaryReport` method signature includes `CancellationToken cancellationToken`.
   - [ ] All calls to `_taskService.GetAll()` pass the `cancellationToken` parameter.
   - [ ] No redundant calls to `GetAll` (should only call it once).
   - [ ] No `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` calls.
   - [ ] `CountCompletedTasksAsync` is either synchronous or properly awaited (no `Task.Run`).
   - [ ] `SimulateAuditLogWriteAsync` is awaited, not discarded.
   - [ ] Method compiles with no warnings.

9. Run build and tests:
   
   ```bash
   dotnet build TaskFlowAPI.sln
   dotnet test TaskFlowAPI.sln
   ```
   
   Optional manual verification:
   - Run the API: `dotnet run --project TaskFlowAPI`.
   - Hit the endpoint: `GET https://localhost:5001/api/Reports/project-summary/1`.
   - Verify it returns valid JSON with project statistics.
   - Check logs for any async-related warnings.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Manual testing (optional):
```bash
dotnet run --project TaskFlowAPI

# In another terminal or Postman:
curl https://localhost:5001/api/Reports/project-summary/1
```

Expected: Valid JSON response with project statistics, no errors/warnings in logs.

## 7. Success Criteria

- No `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` in `ReportsController`.
- No `new CancellationToken()` used to "fake" cancellation in request-handling code.
- No `Task.Run` used to wrap normal request logic.
- No fire-and-forget async calls (all async operations are awaited).
- Build and tests succeed with no new warnings.
- Week 22 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-22-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "refactor: fix async anti-patterns in ReportsController"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Sync-over-Async: Which blocking call did you remove, and what failure mode does it prevent in a web server?*
- *Cancellation: Where did your cancellation token originate (hint: ASP.NET Core binds it automatically), and where did you propagate it to?*
- *Task.Run: Why is Task.Run unnecessary in ASP.NET Core request handlers? When WOULD you use it?*
- *Fire-and-Forget: What's the risk of discarding tasks in ASP.NET Core request code? What happens to exceptions?*

### Discussion Prep:

- *If ASP.NET Core has no SynchronizationContext by default, why is sync-over-async still a problem?*
- *What is your team's policy for background work triggered by HTTP requests?*
- *What would you log/measure in production to detect thread pool starvation?*
- *When would you choose to keep CPU-bound work synchronous vs. making it async?*

## 10. Time Estimate

- 60 min — Reading.
- 20 min — Fix cancellation token propagation.
- 25 min — Remove sync-over-async patterns.
- 20 min — Fix unnecessary Task.Run.
- 20 min — Fix fire-and-forget.
- 10 min — Verify fixes with checklist.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~3 hours 10 minutes.

## 11. Additional Resources

### Examples

- **[Async/Await Anti-Patterns](../Examples/AsyncAntiPatterns.md)** - Common mistakes and how to fix them.

### External Resources

- **[Stephen Cleary's Blog](https://blog.stephencleary.com/)** - Deep dives on async/await patterns and pitfalls.
- **[Microsoft Docs: Cancellation in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/request-features)** - How request cancellation works.
- **[David Fowler's ASP.NET Core Performance Tips](https://github.com/davidfowl/AspNetCoreDiagnosticScenarios/blob/master/AsyncGuidance.md)** - Practical async guidance from ASP.NET Core architect.

### Optional Deep Dives

- **[There Is No Thread](https://blog.stephencleary.com/2013/11/there-is-no-thread.html)** - Stephen Cleary's classic explanation of async/await.
- **[ConfigureAwait FAQ](https://devblogs.microsoft.com/dotnet/configureawait-faq/)** - When and why to use ConfigureAwait(false).