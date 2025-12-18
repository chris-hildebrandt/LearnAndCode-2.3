// The current `GenerateProjectSummaryReport` method is responsible for:
// 1.  Validating the input `projectId`.
// 2.  Fetching all tasks. 
// 3.  Filtering tasks for the specified project.
// 4.  Calculating statistics (total tasks, completed tasks, percentage complete).
// 5.  Identifying overdue tasks.
// 6.  Identifying high-priority tasks.
// 7.  Finding the next upcoming deadline.
// 8.  Assembling the final `ProjectSummaryDto` response.

// This is a classic example of a function that does more than one thing.
// Refactor the `GenerateProjectSummaryReport` method by extracting its various responsibilities into several small, well-named private helper methods. The final `GenerateProjectSummaryReport` method should be short and easy to read, delegating all the work to the helper methods you create.
// You should be able to extract at least **five** distinct helper methods.

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskFlowAPI.DTOs.Responses;
using TaskFlowAPI.Entities;
using TaskFlowAPI.Services.Interfaces;

namespace TaskFlowAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public ReportsController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("project-summary/{projectId}")]
        public async Task<IActionResult> GenerateProjectSummaryReport(int projectId, CancellationToken cancellationToken)
        {

            if (projectId <= 0)
            {
                return BadRequest("Invalid project ID.");
            }

            // TODO Week 22: Fix async best-practices violations in this method.
            //
            // Anti-pattern #1: Ignore request cancellation by creating a brand new token.
            // Anti-pattern #2: Sync-over-async via .Result (thread-blocking in a request path).
            //
            // Fix guidelines:
            // - Use the incoming cancellationToken (ASP.NET binds it to RequestAborted).
            // - Await async calls; do not block.
            var allTasks = await _taskService.GetAll(new CancellationToken());

            // TODO Week 22: Remove this redundant call and sync-over-async.
            // This duplicates work and blocks a thread waiting for async I/O.
            var allTasksAgain = _taskService.GetAll(new CancellationToken()).Result;
            if (allTasks == null || !allTasks.Any())
            {
                return NotFound("No tasks found for the project.");
            }

            var projectTasks = new List<TaskDto>();
            foreach (var task in allTasksAgain)
            {
                if (task.ProjectId == projectId)
                {
                    projectTasks.Add(task);
                }
            }

            if (!projectTasks.Any())
            {
                return NotFound("No tasks found for the specified project.");
            }

            var totalTasks = projectTasks.Count;
            // TODO Week 22: Avoid Task.Run for normal request logic AND avoid .Result.
            // This work is CPU-bound and fast; just compute it synchronously.
            var completedTasks = CountCompletedTasksAsync(projectTasks, cancellationToken).Result;

            decimal percentageComplete = 0;
            if (totalTasks > 0)
            {
                percentageComplete = ((decimal)completedTasks / totalTasks) * 100;
            }

            var overdueTasks = new List<TaskDto>();
            var now = DateTime.UtcNow;
            foreach (var task in projectTasks)
            {
                if (!task.IsCompleted && task.DueDate < now)
                {
                    overdueTasks.Add(task);
                }
            }

            var highPriorityTasks = new List<TaskDto>();
            foreach (var task in projectTasks)
            {

                if (task.Priority == 1)
                {
                    highPriorityTasks.Add(task);
                }
            }
            
            DateTime? nextDeadline = null;
            foreach (var task in projectTasks)
            {
                if (!task.IsCompleted)
                {
                    if (!nextDeadline.HasValue || task.DueDate < nextDeadline.Value)
                    {
                        nextDeadline = task.DueDate;
                    }
                }
            }

            var summary = new ProjectSummaryDto
            {
                Id = projectId,
                Name = $"Project {projectId}",
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PercentageComplete = Math.Round(percentageComplete, 2),
                OverdueTasks = overdueTasks.Count,
                HighPriorityTasks = highPriorityTasks.Count,
                NextUpcomingDeadline = nextDeadline
            };

            // TODO Week 22: Fix fire-and-forget. Discarding tasks can lose exceptions and makes completion non-deterministic.
            _ = SimulateAuditLogWriteAsync(projectId, cancellationToken);

            return Ok(summary);
        }

        // TODO Week 22: Remove Task.Run usage. Prefer synchronous code for synchronous work.
        // If this represented real async I/O, it should be awaited by the caller (no .Result).
        private Task<int> CountCompletedTasksAsync(List<TaskDto> tasks, CancellationToken cancellationToken)
        {
            return Task.Run(() => tasks.Count(t => t.IsCompleted), cancellationToken);
        }

        private async Task SimulateAuditLogWriteAsync(int projectId, CancellationToken cancellationToken)
        {
            await Task.Delay(25, cancellationToken);
            _ = projectId; // Placeholder so the parameter isn't "unused" if refactors move code around.
        }
    }
}