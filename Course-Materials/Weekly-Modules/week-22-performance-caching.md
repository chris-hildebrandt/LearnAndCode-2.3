# Week 22: Async Best Practices & Performance Fundamentals

This week is about the kind of “performance work” you will actually do on modern ASP.NET Core teams: making request flows reliably asynchronous, avoiding common async/await footguns, and applying a few simple habits that prevent outages and slowdowns.

We are intentionally **not** doing caching this week, and we are skipping Clean Code’s concurrency chapter. Instead, we are using .NET-specific async guidance that directly supports the assignment work you’ll do in this repo.

## 1. Learning Objectives

- Identify and fix common async/await anti-patterns in ASP.NET Core (sync-over-async, fire-and-forget, unnecessary `Task.Run`, etc.).
- Propagate `CancellationToken` correctly from HTTP request → controller → service/repository calls.
- Understand when **NOT** to use `ConfigureAwait(false)` in ASP.NET Core applications (and when it *does* make sense in library code).
- Improve reliability and performance without adding new NuGet packages.

## 2. Reading & Resources (60 min)

- **Async/Await Best Practices in Asynchronous Programming** by Stephen Cleary (20 min)
  - The canonical async/await best-practices primer.
  - Covers “async all the way”, avoiding `async void`, and common deadlock/perf footguns.
- **ConfigureAwait FAQ** by Stephen Toub (15 min)
  - Explains context capturing and when `ConfigureAwait(false)` helps (and when it’s unnecessary).
- **Performance Best Practices in ASP.NET Core** (25 min)
  - Official Microsoft guidance for high-signal performance decisions in web apps.
  - Emphasizes async I/O, cancellation, and avoiding thread pool starvation.

## 3. This Week’s Work

You will fix intentionally-bad async code that compiles and “works”, but violates best practices and creates performance/reliability risk.

**Deliverable:** Resolve every `// TODO Week 22:` comment you find in the codebase and ensure no new async anti-patterns remain.

## 4. Files to Modify

- `TaskFlowAPI/Controllers/ReportsController.cs`
- `TaskFlowAPI/Extensions/ExceptionMiddlewareExtensions.cs`
- Any other files impacted by your changes (use the compiler + find-all-references to guide you).
- This file (`Course-Materials/Weekly-Modules/week-22-performance-caching.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-22/<your-name>`.

2. **Read the async articles first (60 min).**
   - Do not start “fixing code” until you understand what patterns to look for.

3. **Find the Week 22 targets (5 min).**
   - Search the codebase for:
     - `TODO Week 22`
     - `.Result`
     - `.Wait(`
     - `Task.Run(`
     - `new CancellationToken()`
     - `ConfigureAwait(`

4. **Task A — Fix sync-over-async (25–35 min)**
   - Remove blocking calls like `.Result` / `.Wait()` / `.GetAwaiter().GetResult()`.
   - Replace them with `await` **all the way down**.
   - Rule: In ASP.NET Core request-handling code, you almost never want to block a thread waiting for async work.

5. **Task B — Fix cancellation token propagation (20–30 min)**
   - Do not create tokens with `new CancellationToken()` in a request path.
   - Use the provided `CancellationToken` (ASP.NET Core will bind it to the request abort token).
   - Ensure any async framework calls that accept a token are passed the token.

6. **Task C — Fix fire-and-forget (15–25 min)**
   - Identify any “discarded task” usage (`_ = SomeAsyncCall();`).
   - Decide the correct behavior:
     - Most often: `await` it.
     - If you truly must background work: you must have a plan for exceptions + lifetime (this is rare in request code).

7. **ConfigureAwait exercise (10 min)**
   - If you see `ConfigureAwait(false)` inside the web app code, decide whether it is helpful.
   - Default rule for this course: **Do not use `ConfigureAwait(false)` in ASP.NET Core app code unless you can justify it in writing.**

8. Run build and tests:

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

Optional manual verification:
- Run the API and hit the Reports endpoint twice; confirm it still returns a valid response and no warnings/errors appear in logs.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- No `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` in request-handling code.
- No `new CancellationToken()` used to “fake” cancellation in request-handling code.
- No `Task.Run` used to wrap normal request logic.
- No fire-and-forget async calls in request-handling code (or they are explicitly justified and safely handled).
- Build and tests succeed.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-22-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "refactor: fix async anti-patterns"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Sync-over-Async:* Which blocking call did you remove, and what failure mode does it prevent in a web server?
- *Cancellation:* Where did your cancellation token originate, and where did you propagate it to?
- *Fire-and-Forget:* What’s the risk of discarding tasks in ASP.NET Core request code?
- *ConfigureAwait:* In your own words, what does `ConfigureAwait(false)` change, and why is it usually unnecessary in ASP.NET Core apps?

### Discussion Prep:

- *If ASP.NET Core has no SynchronizationContext by default, why is sync-over-async still a problem?*
- *What is your team’s policy for background work triggered by HTTP requests?*
- *What would you log/measure in production to detect thread pool starvation?*

## 10. Time Estimate

- 60 min – Reading.
- 60–90 min – Async audit + fixes.
- 20 min – Journal and discussion prep.
- 15 min – Test => PR => Review => Merge.

**Total:** ~2 hours 35 minutes.

## 11. Additional Resources

- **[Microsoft docs: Cancellation in ASP.NET Core](https://learn.microsoft.com/aspnet/core/fundamentals/request-features?view=aspnetcore-8.0)** (focus: request aborts / cancellation tokens)
- **[Stephen Cleary’s blog](https://blog.stephencleary.com/)** (optional deep dives)
