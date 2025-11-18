
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
            // 1. Initial Setup & Validation
            if (projectId <= 0)
            {
                return BadRequest("Invalid project ID.");
            }

            var allTasks = await _taskService.GetAll(new CancellationToken());
            if (allTasks == null || !allTasks.Any())
            {
                return NotFound("No tasks found for the project.");
            }

            // 2. Filter Tasks for the specific project
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

            // 3. Calculate Core Statistics
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

            // 4. Identify Overdue Tasks
            var overdueTasks = new List<TaskDto>();
            var now = DateTime.UtcNow;
            foreach (var task in projectTasks)
            {
                if (!task.IsCompleted && task.DueDate < now)
                {
                    overdueTasks.Add(task);
                }
            }

            // 5. Identify High Priority Tasks
            var highPriorityTasks = new List<TaskDto>();
            foreach (var task in projectTasks)
            {
                // Priority 1 is considered "High"
                if (task.Priority == 1)
                {
                    highPriorityTasks.Add(task);
                }
            }
            
            // 6. Find the next upcoming deadline
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

            // 7. Assemble the DTO
            var summary = new ProjectSummaryDto
            {
                Id = projectId,
                Name = $"Project {projectId}", // Placeholder name
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
