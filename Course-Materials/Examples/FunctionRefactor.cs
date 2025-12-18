// Functions - Refactoring Exercise
//
// Learning Objectives:
// 1. Functions should be small (ideally 5-10 lines)
// 2. Functions should do ONE thing
// 3. Functions should have ONE level of abstraction
// 4. Function names should clearly describe what they do
//
// Instructions:
// For each example below, refactor the large function into smaller, 
// well-named functions that each do one thing.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Week04_FunctionsExercise
{
    // --------------------------------------------------------------------------------
    // Example 1: WARM-UP - Calculate Order Total
    // Problem: One function doing calculation AND formatting
    // --------------------------------------------------------------------------------

    public class OrderCalculatorBefore
    {
        /// <summary>
        /// This function calculates the order total but ALSO formats the output.
        /// That's two responsibilities!
        /// 
        /// Refactoring Task:
        /// 1. Extract the calculation logic into CalculateSubtotal()
        /// 2. Extract the tax calculation into CalculateTax()
        /// 3. Keep formatting separate in GetFormattedTotal()
        /// </summary>
        public string GetOrderSummary(List<decimal> itemPrices, decimal taxRate)
        {
            // Calculate subtotal
            decimal subtotal = 0;
            foreach (var price in itemPrices)
            {
                subtotal += price;
            }

            // Calculate tax
            decimal tax = subtotal * taxRate;

            // Calculate total
            decimal total = subtotal + tax;

            // Format output
            return $"Subtotal: ${subtotal:F2}\nTax: ${tax:F2}\nTotal: ${total:F2}";
        }
    }

    // SOLUTION HINT:
    public class OrderCalculatorAfter
    {
        public string GetOrderSummary(List<decimal> itemPrices, decimal taxRate)
        {
            var subtotal = CalculateSubtotal(itemPrices);
            var tax = CalculateTax(subtotal, taxRate);
            var total = subtotal + tax;
            return FormatOrderSummary(subtotal, tax, total);
        }

        private decimal CalculateSubtotal(List<decimal> itemPrices)
        {
            return itemPrices.Sum();
        }

        private decimal CalculateTax(decimal subtotal, decimal taxRate)
        {
            return subtotal * taxRate;
        }

        private string FormatOrderSummary(decimal subtotal, decimal tax, decimal total)
        {
            return $"Subtotal: ${subtotal:F2}\nTax: ${tax:F2}\nTotal: ${total:F2}";
        }
    }

    // --------------------------------------------------------------------------------
    // Example 2: MEDIUM - Process Task Creation
    // Problem: One function with multiple levels of abstraction
    // --------------------------------------------------------------------------------

    public class TaskServiceBefore
    {
        /// <summary>
        /// This function creates a task, but mixes high-level orchestration
        /// with low-level validation and formatting details.
        /// 
        /// Refactoring Task:
        /// Break this into smaller functions:
        /// 1. ValidateTaskInput() - handles all validation
        /// 2. CreateTaskFromInput() - creates the task object
        /// 3. AssignDefaultValues() - sets defaults
        /// 4. SaveTask() - persistence logic
        /// </summary>
        public void CreateTask(string title, string description, string priority, DateTime? dueDate)
        {
            // Validation mixed with business logic
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title cannot be empty");
            }
            if (title.Length > 100)
            {
                throw new ArgumentException("Title too long");
            }

            // More validation
            if (priority != "Low" && priority != "Medium" && priority != "High")
            {
                priority = "Medium"; // default
            }

            // Create task object
            var task = new Task
            {
                Title = title.Trim(),
                Description = description?.Trim() ?? "",
                Priority = priority,
                DueDate = dueDate ?? DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };

            // Save to database (simulated)
            Console.WriteLine($"Saving task: {task.Title}");
            // database.SaveTask(task);
        }
    }

    // SOLUTION HINT:
    public class TaskServiceAfter
    {
        public void CreateTask(string title, string description, string priority, DateTime? dueDate)
        {
            ValidateTaskInput(title);
            var normalizedPriority = NormalizePriority(priority);
            var task = BuildTask(title, description, normalizedPriority, dueDate);
            SaveTask(task);
        }

        private void ValidateTaskInput(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");
            
            if (title.Length > 100)
                throw new ArgumentException("Title too long");
        }

        private string NormalizePriority(string priority)
        {
            var validPriorities = new[] { "Low", "Medium", "High" };
            return validPriorities.Contains(priority) ? priority : "Medium";
        }

        private Task BuildTask(string title, string description, string priority, DateTime? dueDate)
        {
            return new Task
            {
                Title = title.Trim(),
                Description = description?.Trim() ?? "",
                Priority = priority,
                DueDate = dueDate ?? DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now,
                IsCompleted = false
            };
        }

        private void SaveTask(Task task)
        {
            Console.WriteLine($"Saving task: {task.Title}");
            // database.SaveTask(task);
        }
    }

    // --------------------------------------------------------------------------------
    // Example 3: CHALLENGE - Generate Report
    // Problem: Giant function doing everything
    // --------------------------------------------------------------------------------

    public class ReportGeneratorBefore
    {
        /// <summary>
        /// This function is doing WAY too much in one place.
        /// It's hard to read, hard to test, and hard to maintain.
        /// 
        /// Refactoring Task:
        /// Break this monster into logical, well-named functions:
        /// 1. GetCompletedTasks() - data retrieval
        /// 2. CalculateCompletionStats() - business logic
        /// 3. FormatReportHeader() - formatting
        /// 4. FormatTaskList() - formatting
        /// 5. GenerateReport() - orchestration (calls the others)
        /// </summary>
        public string GenerateTaskReport(List<Task> allTasks)
        {
            // Filter completed tasks
            var completedTasks = new List<Task>();
            foreach (var task in allTasks)
            {
                if (task.IsCompleted)
                {
                    completedTasks.Add(task);
                }
            }

            // Calculate statistics
            int totalTasks = allTasks.Count;
            int completedCount = completedTasks.Count;
            double completionRate = totalTasks > 0 ? (double)completedCount / totalTasks * 100 : 0;

            // Count by priority
            int highPriorityCompleted = 0;
            int mediumPriorityCompleted = 0;
            int lowPriorityCompleted = 0;
            
            foreach (var task in completedTasks)
            {
                if (task.Priority == "High") highPriorityCompleted++;
                else if (task.Priority == "Medium") mediumPriorityCompleted++;
                else lowPriorityCompleted++;
            }

            // Build report string
            var report = "=== TASK COMPLETION REPORT ===\n";
            report += $"Generated: {DateTime.Now:yyyy-MM-dd HH:mm}\n\n";
            report += $"Total Tasks: {totalTasks}\n";
            report += $"Completed: {completedCount}\n";
            report += $"Completion Rate: {completionRate:F1}%\n\n";
            report += "Completed by Priority:\n";
            report += $"  High: {highPriorityCompleted}\n";
            report += $"  Medium: {mediumPriorityCompleted}\n";
            report += $"  Low: {lowPriorityCompleted}\n\n";
            report += "Completed Tasks:\n";
            
            foreach (var task in completedTasks.OrderBy(t => t.CompletedAt))
            {
                report += $"  [{task.Priority}] {task.Title} - Completed: {task.CompletedAt:yyyy-MM-dd}\n";
            }

            return report;
        }
    }

    // YOUR TURN: Refactor this into multiple small, well-named functions!
    public class ReportGeneratorAfter
    {
        public string GenerateTaskReport(List<Task> allTasks)
        {
            // TODO: Break this down into smaller functions
            // Hint: Start by identifying the major "sections" of work
            throw new NotImplementedException("Your refactoring goes here!");
        }
    }

    // --------------------------------------------------------------------------------
    // Supporting Classes
    // --------------------------------------------------------------------------------

    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsCompleted { get; set; }
    }
}