# TaskFlowAPI Architecture Knowledge Base

**Purpose:** Single source of truth for understanding API state at any point in the curriculum  
**Last Updated:** 2025-01-18  
**Status:** Living document - update as curriculum evolves

---

## TaskFlowAPI Overview

**Purpose:** Task and workflow management REST API  
**Domain:** Task management, project tracking, reporting  
**Target User:** Project managers, team leads, developers managing work items  
**Technology Stack:**
- Framework: ASP.NET Core (Web API)
- Database: SQLite with Entity Framework Core
- Testing: xUnit, Moq, FluentAssertions
- Validation: FluentValidation
- Documentation: Swagger/OpenAPI
- Logging: Serilog

**Portfolio Vision:** By Week 23, this API should be production-ready, demonstrating:
- Clean architecture (Controller → Service → Repository)
- SOLID principles applied throughout
- 80%+ unit test coverage
- Comprehensive error handling
- API versioning and documentation
- Performance optimizations (caching, etc.)
- Professional code quality suitable for showcasing to employers

---

## Current Architecture

### Controllers Layer

**Location:** `TaskFlowAPI/Controllers/`

#### TasksController.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Contains intentionally bad names for Week 2 exercise
- **Endpoints:**
  - `GET /api/tasks` - Get all tasks (Week 2: rename from `Get()`)
  - `GET /api/tasks/{id}` - Get task by ID (Week 2: rename from `GetOne()`)
  - `POST /api/tasks` - Create task (Week 2: rename from `Add()`)
  - `PUT /api/tasks/{id}` - Update task (Week 4: optional extension)
  - `DELETE /api/tasks/{id}` - Delete task (Week 4: optional extension)
- **Dependencies:** `ITaskService`
- **Bad Code Examples (Intentional):**
  - `svc` field name (should be `_taskService`)
  - `s` parameter name (should be `taskService`)
  - `t`, `dt`, `req` variable names (should be descriptive)
  - Method names: `Get()`, `GetOne()`, `Add()` (should be `GetAllTasksAsync()`, etc.)

#### ReportsController.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Contains long method for Week 4 function extraction
- **Endpoints:**
  - `GET /api/reports/project-summary/{projectId}` - Generate project summary report
- **Bad Code Example (Intentional):**
  - `GenerateProjectSummaryReport` method: 100+ lines, multiple responsibilities
  - Students will extract 5+ helper methods in Week 4
  - Violates Single Responsibility Principle (preview of Week 11)

### Services Layer

**Location:** `TaskFlowAPI/Services/`

#### TaskService.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Contains multiple responsibilities for Week 11 SRP exercise
- **Current State:**
  - Methods throw `NotImplementedException` (students implement in Week 9)
  - Contains mapping methods (`MapToDto`, `MapToEntity`) - will be extracted in Week 11
  - Contains business rule logic - will be extracted in Week 11
- **Dependencies:** `ITaskRepository`, `ILogger<TaskService>`
- **Methods:**
  - `GetAllAsync()` - Returns all tasks as DTOs
  - `GetAsync(int id)` - Returns single task by ID
  - `AddAsync(CreateTaskRequest)` - Creates new task
  - `UpdateAsync(int id, UpdateTaskRequest)` - Updates existing task (Week 4 optional)
  - `DeleteAsync(int id)` - Deletes task (Week 4 optional)
- **Future Extractions (Week 11):**
  - `TaskMapper` - Entity ↔ DTO mapping
  - `TaskBusinessRules` - Business rule validation

#### Services/Tasks/Filters/
- **Status:** [PEDAGOGICAL - DO NOT FIX] Exists for Week 12 OCP exercise
- **Files:**
  - `ITaskFilter.cs` - Filter interface
  - `StatusTaskFilter.cs` - Filters by completion status
  - `PriorityTaskFilter.cs` - Filters by priority
  - `DueDateTaskFilter.cs` - Filters by due date
  - `CompositeTaskFilter.cs` - Combines multiple filters
- **Purpose:** Demonstrates Open/Closed Principle - extend filtering without modifying service

### Repositories Layer

**Location:** `TaskFlowAPI/Repositories/`

#### TaskRepository.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Methods throw `NotImplementedException` (students implement in Week 8)
- **Dependencies:** `TaskFlowDbContext`
- **Methods (to be implemented in Week 8):**
  - `GetAllAsync()` - Returns all tasks with Project navigation
  - `GetByIdAsync(int id)` - Returns single task or null
  - `CreateAsync(TaskEntity)` - Creates and saves task
  - `UpdateAsync(TaskEntity)` - Updates existing task
  - `DeleteAsync(TaskEntity)` - Deletes task (idempotent)
- **EF Core Patterns to Learn:**
  - `AsNoTracking()` for read operations
  - `Include()` for eager loading navigation properties
  - `SaveChangesAsync()` for persistence
  - Async/await patterns with `CancellationToken`

#### ITaskRepository.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Will be split in Week 14 (ISP)
- **Future Changes:**
  - Week 14: Split into `ITaskReader` and `ITaskWriter`
  - Week 13: Contract documentation added for LSP compliance

### Entities Layer

**Location:** `TaskFlowAPI/Entities/`

#### TaskEntity.cs
- **Status:** [PEDAGOGICAL - DO NOT FIX] Intentionally anemic for Week 7 encapsulation exercise
- **Current State:**
  - All properties have public setters
  - No domain behaviors
  - No validation
- **Week 7 Transformation:**
  - Encapsulate Title property (immutable after creation)
  - Add `Complete()` method with validation
  - Add static factory `TaskEntity.Create()`
  - Optional: Add `Reopen()`, `UpdateDetails()`, `ChangePriority()`
- **Properties:**
  - `Id` (int) - Primary key
  - `Title` (string) - Required, will be encapsulated
  - `Description` (string?) - Optional
  - `Priority` (int) - 0-5 range, will be validated
  - `DueDate` (DateTime?) - Optional
  - `IsCompleted` (bool) - Managed via `Complete()`/`Reopen()`
  - `CompletedAt` (DateTime?) - Set automatically on completion
  - `CreatedAt` (DateTime) - Set on creation
  - `ProjectId` (int) - Foreign key
  - `Project` (ProjectEntity?) - Navigation property

#### ProjectEntity.cs
- **Status:** Standard entity, may need encapsulation in future
- **Properties:**
  - `Id` (int) - Primary key
  - `Name` (string) - Required
  - `Description` (string?) - Optional
  - `Tasks` (ICollection<TaskEntity>) - Navigation property

### DTOs Layer

**Location:** `TaskFlowAPI/DTOs/`

#### Requests/
- **CreateTaskRequest.cs** - Input for creating tasks
  - Properties: `Title?`, `Description?`, `Priority?`, `DueDate?`, `ProjectId?`
  - All nullable for flexible validation (Week 10)
- **UpdateTaskRequest.cs** - Input for updating tasks
  - Properties: All nullable for partial updates
  - Includes `IsCompleted?` for status changes

#### Responses/
- **TaskDto.cs** - Output for task data
  - Flattened structure (ProjectId + ProjectName instead of nested Project)
  - All properties from TaskEntity except navigation properties
- **ProjectSummaryDto.cs** - Output for project reports
  - Used by ReportsController
  - Contains aggregated statistics
- **PagedResponse.cs** - For pagination (future)

### Validators Layer

**Location:** `TaskFlowAPI/Validators/`

#### CreateTaskValidator.cs
- **Status:** Scaffolded, students implement rules in Week 10
- **Rules to implement:**
  - Title: required, 3-100 characters
  - Priority: 0-5 range
  - DueDate: must be future or today
  - ProjectId: must be positive

#### UpdateTaskValidator.cs
- **Status:** Scaffolded, students implement rules in Week 10
- **Rules to implement:**
  - At least one field must be provided
  - Same validation rules as CreateTaskValidator for provided fields

### Data Layer

**Location:** `TaskFlowAPI/Data/`

#### TaskFlowDbContext.cs
- **Purpose:** EF Core DbContext for database operations
- **DbSets:**
  - `Tasks` - TaskEntity collection
  - `Projects` - ProjectEntity collection
- **Configuration:**
  - Fluent API configuration in `OnModelCreating()`
  - Seed data for development/testing
  - Indexes on Title and Priority
  - Cascade delete: deleting project deletes all tasks

### Infrastructure

**Location:** `TaskFlowAPI/Infrastructure/` (created in Week 15)

#### Time/
- **ISystemClock.cs** - Abstraction for system time (Week 15)
- **UtcSystemClock.cs** - Production implementation
- **Purpose:** Enable deterministic testing of time-based logic

### Extensions

**Location:** `TaskFlowAPI/Extensions/`

#### ExceptionMiddlewareExtensions.cs
- **Purpose:** Global exception handling middleware
- **Status:** Students enhance in Week 10
- **Current:** Basic implementation
- **Week 10:** Map custom exceptions to ProblemDetails responses

### Program.cs

**Purpose:** Application entry point and configuration
- **Services Registered:**
  - Controllers, Swagger, EF Core, Serilog
  - Repository: `ITaskRepository` → `TaskRepository` (Week 8)
  - Service: `ITaskService` → `TaskService` (Week 9)
  - Validators: FluentValidation (Week 10)
  - Mapper, BusinessRules: (Week 11)
  - Filters: (Week 12)
  - Clock: `ISystemClock` → `UtcSystemClock` (Week 15)
- **Middleware Pipeline:**
  - Swagger (Development only)
  - Exception handler
  - Request logging
  - Response compression
  - HTTPS redirection
  - Authorization
  - Controllers

---

## API Progression Vision (Week by Week)

### Weeks 1-4: Foundation & Code Quality

**Week 1:** Setup and exploration
- API runs locally
- Swagger accessible
- Students explore codebase, identify smells
- **API State:** Minimal, intentionally messy

**Week 2:** Meaningful Names
- Refactor `TasksController` bad names
- Refactor `ITaskService` bad names
- **API State:** Same functionality, better names

**Week 3:** Comments & Documentation
- Remove redundant comments
- Keep only "why" comments
- **API State:** Self-documenting code

**Week 4:** Functions
- Refactor `ReportsController.GenerateProjectSummaryReport`
- Extract 5+ helper methods
- Optional: Add UPDATE/DELETE endpoints
- **API State:** Smaller, focused functions

### Weeks 5-9: Core Implementation

**Week 5:** AI Tools
- No code changes (exploration week)
- **API State:** Unchanged

**Week 6:** Git Workflow
- Minor refactoring (validation extraction)
- **API State:** Slight improvement

**Week 7:** Classes & Encapsulation
- Transform `TaskEntity` from anemic to rich domain model
- Add `Complete()`, factory method, encapsulation
- **API State:** Domain model with behaviors

**Week 8:** Repository Pattern
- Implement `TaskRepository` methods
- Learn EF Core patterns
- **API State:** Data access layer functional

**Week 9:** Service Layer & DTOs
- Implement `TaskService` methods
- Map entities to DTOs
- **API State:** Full CRUD operations working

### Weeks 10-15: SOLID Principles

**Week 10:** Error Handling & Validation
- Implement FluentValidation rules
- Add custom exceptions
- Enhance exception middleware
- **API State:** Robust error handling

**Week 11:** Single Responsibility Principle
- Extract `TaskMapper` from `TaskService`
- Extract `TaskBusinessRules` from `TaskService`
- **API State:** Focused, single-responsibility classes

**Week 12:** Open/Closed Principle
- Implement filter strategies
- Extend filtering without modifying service
- **API State:** Extensible filtering system

**Week 13:** Liskov Substitution Principle
- Create `FakeTaskRepository` for tests
- Add contract tests
- Ensure substitutability
- **API State:** Testable with fakes

**Week 14:** Interface Segregation Principle
- Split `ITaskRepository` into `ITaskReader` and `ITaskWriter`
- Update dependencies
- **API State:** Clients depend only on what they use

**Week 15:** Dependency Inversion Principle
- Create `ISystemClock` abstraction
- Inject clock into services
- **API State:** Testable time-based logic

### Weeks 16-20: Quality & Patterns

**Week 16:** File Organization
- Reorganize folder structure
- Consolidate DI registrations
- **API State:** Well-organized codebase

**Week 17:** Unit Testing & TDD
- Write tests for `TaskService`
- Achieve 80%+ coverage
- **API State:** Well-tested business logic

**Week 18:** Code Smells & Refactoring
- Identify and fix remaining smells
- **API State:** Cleaner code

**Week 19:** Design Patterns
- Apply appropriate patterns
- **API State:** Pattern-based architecture

**Week 20:** Code Review & Collaboration
- Peer review process
- **API State:** Reviewed and improved

### Weeks 21-23: Production Ready

**Week 21:** API Design & Documentation
- Enhance Swagger documentation
- API versioning
- **API State:** Well-documented API

**Week 22:** Performance & Caching
- Add caching strategies
- Performance optimizations
- **API State:** Optimized for performance

**Week 23:** Final Polish
- Final code review
- Documentation completion
- Demo preparation
- **API State:** Production-ready portfolio piece

---

## Integration Points for Learning

### Clean Code Principles → API Locations

**Chapter 2: Meaningful Names**
- `TasksController.cs` - Bad names (Week 2)
- `ITaskService.cs` - Bad names (Week 2)

**Chapter 3: Functions**
- `ReportsController.GenerateProjectSummaryReport` - Long method (Week 4)
- Service methods - Function extraction (Week 11)

**Chapter 4: Comments**
- `Program.cs` - Extensive "what" comments (Week 3)
- All files - Comment cleanup (Week 3)

**Chapter 6: Objects and Data Structures**
- DTOs vs Entities - Separation (Week 9)
- Domain model design (Week 7)

**Chapter 7: Error Handling**
- Exception middleware (Week 10)
- Custom exceptions (Week 10)

**Chapter 9: Unit Tests**
- Test infrastructure (Week 17)
- TDD exercise (Week 17)

**Chapter 10: Classes**
- TaskEntity encapsulation (Week 7)
- File organization (Week 16)

**Chapter 11: Systems**
- Repository pattern (Week 8)
- Service layer (Week 9)
- Boundaries (Week 16)

### SOLID Principles → API Locations

**Single Responsibility Principle (Week 11)**
- `TaskService` - Multiple responsibilities
- Extract: `TaskMapper`, `TaskBusinessRules`

**Open/Closed Principle (Week 12)**
- Task filtering - Strategy pattern
- Extend without modifying service

**Liskov Substitution Principle (Week 13)**
- `ITaskRepository` - Contract compliance
- `FakeTaskRepository` vs `TaskRepository`

**Interface Segregation Principle (Week 14)**
- `ITaskRepository` - Fat interface
- Split into `ITaskReader` and `ITaskWriter`

**Dependency Inversion Principle (Week 15)**
- `DateTime.UtcNow` - Direct dependency
- Abstract to `ISystemClock`

---

## Portfolio Value Assessment

### Current State (Week 1)
**Showcase-Ready:** ❌ No
**What's Missing:**
- Poor code quality (intentional, for learning)
- No business logic implemented
- No error handling
- No tests
- Incomplete functionality

### By End of Course (Week 23)
**Showcase-Ready:** ✅ Yes
**What Makes It Portfolio-Worthy:**
- Clean architecture demonstrating SOLID principles
- Comprehensive error handling
- 80%+ test coverage
- Well-documented API (Swagger)
- Performance optimizations
- Professional code quality
- Real-world problem solved (task management)
- Demonstrates progression from messy to clean code

**Portfolio Talking Points:**
- "I refactored a legacy codebase applying Clean Code principles"
- "Implemented SOLID principles throughout the architecture"
- "Built a production-ready API with comprehensive testing"
- "Applied design patterns appropriately"
- "Demonstrates understanding of professional software development practices"

---

## Curriculum-Driven API Evolution

This section maps each curriculum week to the API state, showing how exercises build toward the final portfolio piece.

### Week 1: Introduction
**Principles Taught:** Quality Manifesto, Clean Code mindset  
**API State at Start:** Minimal, intentionally messy  
**API State at End:** Same, but students understand what needs fixing  
**Key Files Involved:** All files (exploration)  
**Portfolio Impact:** Foundation for understanding code quality

### Week 2: Meaningful Names
**Principles Taught:** Clean Code Chapter 2  
**API State at Start:** Bad names throughout  
**API State at End:** All names reveal intent  
**Key Files Involved:** `TasksController.cs`, `ITaskService.cs`, `TaskService.cs`  
**Portfolio Impact:** Code is self-documenting

### Week 3: Comments
**Principles Taught:** Clean Code Chapter 4  
**API State at Start:** Redundant comments  
**API State at End:** Only "why" comments remain  
**Key Files Involved:** All files  
**Portfolio Impact:** Code communicates intent without comments

### Week 4: Functions
**Principles Taught:** Clean Code Chapter 3  
**API State at Start:** Long method in ReportsController  
**API State at End:** Small, focused functions  
**Key Files Involved:** `ReportsController.cs`  
**Portfolio Impact:** Demonstrates function extraction skills

### Week 7: Classes & Encapsulation
**Principles Taught:** Clean Code Chapter 10  
**API State at Start:** Anemic domain model  
**API State at End:** Rich domain model with behaviors  
**Key Files Involved:** `TaskEntity.cs`  
**Portfolio Impact:** Shows domain modeling skills

### Week 8: Repository Pattern
**Principles Taught:** Clean Code Chapter 11 (Systems)  
**API State at Start:** Empty repository methods  
**API State at End:** Functional data access layer  
**Key Files Involved:** `TaskRepository.cs`  
**Portfolio Impact:** Demonstrates data access patterns

### Week 9: Service Layer & DTOs
**Principles Taught:** Clean Code Chapters 6, 11  
**API State at Start:** Empty service methods  
**API State at End:** Full CRUD operations  
**Key Files Involved:** `TaskService.cs`, DTOs  
**Portfolio Impact:** Shows layered architecture understanding

### Week 10: Error Handling
**Principles Taught:** Clean Code Chapter 7  
**API State at Start:** Basic error handling  
**API State at End:** Comprehensive validation and exception handling  
**Key Files Involved:** Validators, Exception middleware  
**Portfolio Impact:** Production-ready error handling

### Week 11: Single Responsibility Principle
**Principles Taught:** SOLID - SRP  
**API State at Start:** TaskService does too much  
**API State at End:** Focused, single-responsibility classes  
**Key Files Involved:** `TaskService.cs`, new mapper/rules classes  
**Portfolio Impact:** Demonstrates SOLID application

### Week 12: Open/Closed Principle
**Principles Taught:** SOLID - OCP  
**API State at Start:** Filtering logic in service  
**API State at End:** Extensible filter strategies  
**Key Files Involved:** Filter classes, `TaskService.cs`  
**Portfolio Impact:** Shows extensibility design

### Week 13: Liskov Substitution Principle
**Principles Taught:** SOLID - LSP  
**API State at Start:** Repository contract unclear  
**API State at End:** Contract tests ensure substitutability  
**Key Files Involved:** `ITaskRepository.cs`, `FakeTaskRepository.cs`  
**Portfolio Impact:** Demonstrates contract design

### Week 14: Interface Segregation Principle
**Principles Taught:** SOLID - ISP  
**API State at Start:** Fat ITaskRepository interface  
**API State at End:** Segregated reader/writer interfaces  
**Key Files Involved:** `ITaskRepository.cs`, new interfaces  
**Portfolio Impact:** Shows interface design skills

### Week 15: Dependency Inversion Principle
**Principles Taught:** SOLID - DIP  
**API State at Start:** Direct DateTime.UtcNow usage  
**API State at End:** Abstracted time dependency  
**Key Files Involved:** New infrastructure abstractions  
**Portfolio Impact:** Demonstrates dependency management

### Week 16: File Organization
**Principles Taught:** Clean Code Chapter 5, 8  
**API State at Start:** Disorganized structure  
**API State at End:** Well-organized modules  
**Key Files Involved:** All files (reorganization)  
**Portfolio Impact:** Professional code organization

### Week 17: Unit Testing & TDD
**Principles Taught:** Clean Code Chapter 9  
**API State at Start:** Minimal tests  
**API State at End:** 80%+ coverage  
**Key Files Involved:** Test project  
**Portfolio Impact:** Demonstrates testing expertise

### Week 18: Code Smells & Refactoring
**Principles Taught:** Clean Code Chapter 17  
**API State at Start:** Remaining smells  
**API State at End:** Clean code  
**Key Files Involved:** Various  
**Portfolio Impact:** Shows refactoring skills

### Week 19: Design Patterns
**Principles Taught:** Common design patterns  
**API State at Start:** Basic patterns  
**API State at End:** Pattern-based architecture  
**Key Files Involved:** Various  
**Portfolio Impact:** Demonstrates pattern knowledge

### Week 20: Code Review
**Principles Taught:** Collaboration practices  
**API State at Start:** Individual work  
**API State at End:** Reviewed and improved  
**Key Files Involved:** All files  
**Portfolio Impact:** Shows collaboration skills

### Week 21: API Design & Documentation
**Principles Taught:** RESTful API design  
**API State at Start:** Basic API  
**API State at End:** Well-documented, versioned API  
**Key Files Involved:** Controllers, Swagger config  
**Portfolio Impact:** Professional API design

### Week 22: Performance & Caching
**Principles Taught:** Performance optimization  
**API State at Start:** Basic performance  
**API State at End:** Optimized with caching  
**Key Files Involved:** Caching infrastructure  
**Portfolio Impact:** Shows performance awareness

### Week 23: Final Polish
**Principles Taught:** Production readiness  
**API State at Start:** Good code  
**API State at End:** Production-ready portfolio piece  
**Key Files Involved:** All files  
**Portfolio Impact:** Complete, showcase-ready project

---

## Notes for Curriculum Designers

### Intentionally Bad Code Locations

**DO NOT FIX these - they are teaching tools:**

1. **TasksController.cs** - Bad names (Week 2)
2. **ReportsController.cs** - Long method (Week 4)
3. **TaskEntity.cs** - Anemic model (Week 7)
4. **TaskService.cs** - Multiple responsibilities (Week 11)
5. **TaskRepository.cs** - Empty methods (Week 8)
6. **Program.cs** - Extensive comments (Week 3)

### Empty Implementations

**DO NOT IMPLEMENT these - students will:**

1. **TaskRepository methods** - Week 8
2. **TaskService methods** - Week 9
3. **Validators** - Week 10
4. **Mapper/BusinessRules** - Week 11

### Code That Should Exist But Doesn't

**These need to be created by students:**

1. **FakeTaskRepository** - Week 13 (LSP)
2. **ISystemClock infrastructure** - Week 15 (DIP)
3. **ITaskReader/ITaskWriter** - Week 14 (ISP)
4. **Test files** - Week 17

---

## Maintenance Notes

**When updating this document:**
- Update "Current Architecture" section when codebase changes
- Update "API Progression Vision" when curriculum changes
- Update "Portfolio Value Assessment" as API improves
- Keep "Intentionally Bad Code" section accurate
- Document any new integration points

**This document should be referenced when:**
- Designing new exercises
- Verifying exercise-to-principle coupling
- Understanding API state at any week
- Planning portfolio improvements
