
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

        // CODE SMELL: Long Method (Clean Code Ch 17, p. 288)
        // This method is 95+ lines and does multiple things:
        // 1. Validates project ID
        // 2. Fetches all tasks
        // 3. Filters tasks by project
        // 4. Calculates statistics
        // 5. Identifies overdue tasks
        // 6. Identifies high priority tasks
        // 7. Finds next deadline
        // 8. Assembles DTO
        // Refactor by: Extract smaller methods for each responsibility (Extract Method pattern).
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

            // CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
            // This filtering pattern (foreach + if + add) is repeated 3 times in this method.
            // Extract to a shared filtering method to follow DRY principle.
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

            // CODE SMELL: Magic Numbers (Clean Code Ch 17, G25)
            // The number 100 is a magic number used for percentage calculation.
            // Refactor by: Extract to named constant: private const int PercentageMultiplier = 100;
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
                percentageComplete = ((decimal)completedTasks / totalTasks) * 100; // Magic number: 100
            }

            // CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
            // This filtering pattern (foreach + if + add) is repeated again.
            // Same pattern as project filtering above.
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

            // CODE SMELL: Magic Numbers (Clean Code Ch 17, G25)
            // The number 1 is a magic number representing "High" priority.
            // Refactor by: Extract to named constant or use Priority enum: private const int HighPriority = 1;
            // CODE SMELL: Duplicate Code (Clean Code Ch 17, p. 289)
            // This filtering pattern (foreach + if + add) is repeated a third time.
            // 5. Identify High Priority Tasks
            var highPriorityTasks = new List<TaskDto>();
            foreach (var task in projectTasks)
            {
                // Priority 1 is considered "High"
                if (task.Priority == 1) // Magic number: 1
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

            // CODE SMELL: Magic Numbers (Clean Code Ch 17, G25)
            // The number 2 is a magic number representing decimal precision.
            // Refactor by: Extract to named constant: private const int DecimalPlaces = 2;
            // 7. Assemble the DTO
            var summary = new ProjectSummaryDto
            {
                Id = projectId,
                Name = $"Project {projectId}", // Placeholder name
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PercentageComplete = Math.Round(percentageComplete, 2), // Magic number: 2
                OverdueTasks = overdueTasks.Count,
                HighPriorityTasks = highPriorityTasks.Count,
                NextUpcomingDeadline = nextDeadline
            };

            return Ok(summary);
        }
    }
}
