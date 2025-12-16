# TaskFlowAPI Project Overview

## 1. Introduction

TaskFlowAPI is a .NET Core web API project designed as a practical learning tool for a 23-week clean code and software design curriculum. The project starts as a minimal, non-functional API and evolves into a well-structured, feature-rich application as students apply the principles learned each week.

The primary goal of the TaskFlowAPI project is to provide a hands-on experience in building and refactoring a real-world application, reinforcing concepts such as SOLID principles, design patterns, testing, and API design.

## 2. Technology Stack

*   **Backend:** C# with ASP.NET Core
*   **Database:** SQLite with Entity Framework Core
*   **Testing:** xUnit, Moq, FluentAssertions
*   **API Documentation:** Swagger (OpenAPI)
*   **Validation:** FluentValidation

## 3. Core Functionality

The TaskFlowAPI is a simple task management system that allows users to perform CRUD (Create, Read, Update, Delete) operations on tasks. Each task has properties such as a title, description, priority, due date, and completion status. Tasks are also associated with projects.

## 4. Learning Progression

The project is structured to align with the weekly curriculum, with specific assignments and refactoring tasks for each topic. The progression can be summarized as follows:

*   **Weeks 1-4: Foundations:** Environment setup, meaningful naming, and effective use of comments.
*   **Weeks 5-9: Core Implementation:** Building the repository and service layers, implementing business logic, and using DTOs.
*   **Weeks 10-15: SOLID Principles:** Applying SRP, OCP, LSP, ISP, and DIP to improve the application's architecture.
*   **Weeks 16-23: Advanced Topics:** File organization, TDD, code refactoring, design patterns, API design, and performance optimization.

By the end of the course, the TaskFlowAPI project will be a robust, maintainable, and well-documented application that serves as a testament to the student's learning journey.
