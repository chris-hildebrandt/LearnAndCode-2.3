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
        public async Task<IActionResult> GenerateProjectSummaryReport(int projectId)
        {

            if (projectId <= 0)
            {
                return BadRequest("Invalid project ID.");
            }

            var allTasks = await _taskService.GetAll(new CancellationToken());
            if (allTasks == null || !allTasks.Any())
            {
                return NotFound("No tasks found for the project.");
            }

            var projectTasks = new List<TaskDto>();
            foreach (var task in allTasks)
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
            var completedTasks = 0;
            foreach (var task in projectTasks)
            {
                if (task.IsCompleted)
                {
                    completedTasks++;
                }
            }

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

            return Ok(summary);
        }
    }
}
