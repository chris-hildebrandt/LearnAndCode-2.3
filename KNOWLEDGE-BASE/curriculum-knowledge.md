# Curriculum Knowledge Base

**Purpose:** Comprehensive documentation of all curriculum content, learning principles, and their application to TaskFlowAPI  
**Last Updated:** 2025-01-18  
**Status:** Living document - reference when designing exercises

---

## Course Materials Summary

### Course Structure Overview

**Total Duration:** 23 weeks  
**Target Audience:** Junior developers joining a staffing company  
**Primary Text:** Clean Code by Robert C. Martin  
**Supporting Materials:** In Time Tec Quality Manifesto, supplementary readings

**Course Phases:**
- **Phase 1: Foundation (Weeks 1-5)** – Quality Manifesto, meaningful names, comments, functions, AI tools
- **Phase 2: Architecture (Weeks 6-10)** – Git workflow, encapsulation, repositories, services, validation & error handling
- **Phase 3: SOLID (Weeks 11-15)** – SRP, OCP, LSP, ISP, DIP applied to TaskFlow code
- **Phase 4: Quality & Patterns (Weeks 16-20)** – File organization, unit testing/TDD, code smells, design patterns, peer review
- **Phase 5: Production Ready (Weeks 21-23)** – API design & documentation, performance & caching, final polish + demo

### Week-by-Week Breakdown

#### Week 1: Introduction & Quality Manifesto
**Topics:**
- In Time Tec Quality Manifesto (9 foundational values)
- Clean Code Chapter 1: Clean Code definition, professional responsibility, Boy Scout Rule
- Customer-centric design mindset
- Repository setup and exploration

**Key Concepts:**
- Professional responsibility for code quality
- Quality is non-negotiable
- Messy code is a liability
- Code Smell Scavenger Hunt (NEW) - active exploration

**Learning Outcomes:**
- Understand quality standards
- Identify escalation paths
- Map curriculum to API architecture
- Build mental inventory of code smells

---

#### Week 2: Meaningful Names
**Topics:**
- Clean Code Chapter 2: Meaningful Names
- Reveal intent, avoid encodings/abbreviations, use pronounceable names
- Batch renaming with Git-friendly commits

**Key Concepts:**
- Names should reveal intention
- Avoid mental mapping (abbreviations)
- Use searchable names
- Method names are verb phrases
- Class names are nouns

**Learning Outcomes:**
- Refactor bad names to reveal intent
- Understand cascading rename impacts
- Apply naming conventions consistently

**Exercise Framework:**
- Name Analysis Worksheet (pre-refactoring)
- Name Quality Metrics (post-refactoring)
- Rename Impact Map

---

#### Week 3: Comments & Documentation
**Topics:**
- Clean Code Chapter 4: Comments
- "Explain yourself in code" principle
- Good vs. bad comments
- Warning comments and TODOs

**Key Concepts:**
- Comments are failures to express in code
- Delete comments that repeat what code does
- Keep comments that explain "why"
- Public API documentation is valuable

**Learning Outcomes:**
- Distinguish between "what" and "why" comments
- Replace comments with expressive code
- Document public APIs appropriately

---

#### Week 4: Functions
**Topics:**
- Clean Code Chapter 3: Functions
- Small functions, descriptive names, minimizing arguments
- Stepdown Rule
- Single responsibility within functions

**Key Concepts:**
- Functions should be small
- Do one thing
- One level of abstraction per function
- Stepdown Rule: high-level summary, details in called functions
- Fewer arguments are better

**Learning Outcomes:**
- Extract methods systematically
- Apply Stepdown Rule
- Design small, intention-revealing functions

**Exercise Framework:**
- Method Extraction Decision Framework (scoring system)
- Extraction Order guidance (prevent compiler errors)
- Example Refactor Comparison (self-assessment)

---

#### Week 5: AI Tools & Prompt Engineering
**Topics:**
- AI tool usage and prompt engineering
- When to use AI vs. when to think independently
- Quality standards regardless of tool
- AI ethics in professional settings

**Key Concepts:**
- AI as a tool, not a replacement for thinking
- Prompt engineering techniques
- Verification and validation of AI output
- Professional responsibility with AI-generated code
- Ethical considerations (IP, disclosure, learning)

**Learning Outcomes:**
- Craft effective prompts
- Evaluate AI output critically
- Understand ethical boundaries
- Use AI responsibly in professional context

**Exercise Framework:**
- AI-Assisted Refactoring Challenge (structured experimentation)
- AI Ethics in Practice scenarios

---

#### Week 6: Git Workflow & Collaboration
**Topics:**
- Git branching strategies
- Commit message best practices
- Pull request workflow
- Code review process
- Git recovery techniques

**Key Concepts:**
- Feature branch workflow
- Meaningful commit messages
- Small, reviewable commits
- Git recovery (amend, cherry-pick, reset)
- Conflict resolution

**Learning Outcomes:**
- Execute feature branch workflow
- Write meaningful commit messages
- Recover from git mistakes
- Resolve merge conflicts

---

#### Week 7: Classes & Encapsulation
**Topics:**
- Clean Code Chapter 10: Classes
- Cohesion, encapsulation, hiding implementation details
- Domain behaviors vs. data structures

**Key Concepts:**
- Classes should be small
- Single Responsibility Principle (preview)
- Encapsulation protects invariants
- Rich domain models vs. anemic models
- EF Core encapsulation patterns

**Learning Outcomes:**
- Transform anemic entities to rich domain models
- Encapsulate properties with validation
- Add domain behaviors (Complete, Reopen, etc.)
- Work with EF Core encapsulation constraints

**Exercise Framework:**
- Encapsulation Decision Framework
- EF Core Encapsulation Survival Guide
- Two-phase approach (core + optional extension)

---

#### Week 8: Repository Pattern
**Topics:**
- Clean Code Chapter 11: Systems
- Repository pattern implementation
- EF Core best practices
- Async patterns

**Key Concepts:**
- Separate construction from use
- Keep boundaries clean
- Repository isolates data access
- AsNoTracking for read operations
- Include for eager loading
- Async/await patterns

**Learning Outcomes:**
- Implement repository pattern
- Use EF Core effectively
- Apply async patterns consistently
- Test repositories with in-memory database

**Exercise Framework:**
- Study Example Implementation (working pattern)
- Self-check checklists
- In-memory testing examples

---

#### Week 9: Service Layer & DTOs
**Topics:**
- Clean Code Chapter 6: Objects and Data Structures
- Clean Code Chapter 11: Separate policy from implementation
- DTO pattern
- Service layer orchestration

**Key Concepts:**
- Objects vs. data structures
- DTOs separate API contracts from domain models
- Service layer coordinates business logic
- Mapping between layers
- Guard clauses and validation

**Learning Outcomes:**
- Implement service layer methods
- Map between entities and DTOs
- Handle not-found scenarios
- Understand DTO vs. Entity distinction

**Exercise Framework:**
- DTO vs Entity Decision Framework
- Guided implementation with checkpoints
- Step-by-step instructions with reflection questions

---

#### Week 10: Error Handling & Validation
**Topics:**
- Clean Code Chapter 7: Error Handling
- FluentValidation framework
- Custom exception types
- Global exception handling

**Key Concepts:**
- Prefer exceptions to error codes
- Keep error-handling code separate
- Use checked exceptions judiciously
- Provide context with exceptions
- Define exception classes in terms of caller's needs

**Learning Outcomes:**
- Implement FluentValidation rules
- Create domain-specific exceptions
- Configure global exception handling
- Map exceptions to HTTP responses

---

#### Week 11: Single Responsibility Principle (SRP)
**Topics:**
- Clean Code Chapter 3 (Functions) - "do one thing" mantra
- Single Responsibility Principle
- Extracting responsibilities into focused classes

**Key Concepts:**
- A class should have only one reason to change
- Extract mapping logic
- Extract business rules
- Extract validation logic
- Dependency injection for new collaborators

**Learning Outcomes:**
- Identify SRP violations
- Extract responsibilities systematically
- Update dependency injection
- Maintain behavior while refactoring

**Exercise Framework:**
- Phased extraction approach
- SRP Smell Detector (mentioned, needs detail)

---

#### Week 12: Open/Closed Principle (OCP)
**Topics:**
- Clean Code Chapter 11: Systems - keep policies decoupled
- Open/Closed Principle
- Strategy pattern for extensibility

**Key Concepts:**
- Open for extension, closed for modification
- Strategy pattern
- Composition over modification
- When NOT to use OCP (avoid premature abstraction)

**Learning Outcomes:**
- Implement strategy pattern
- Extend functionality without modifying existing code
- Recognize when OCP is appropriate

**Exercise Framework:**
- "Before OCP" Anti-Pattern (shows the problem)
- "When NOT To Use OCP" decision matrix

---

#### Week 13: Liskov Substitution Principle (LSP)
**Topics:**
- Liskov Substitution Principle
- Contract inheritance
- Behavioral subtyping

**Key Concepts:**
- Subtypes must be behaviorally substitutable
- Contracts must be honored
- Behavioral contracts vs. type contracts
- Red flags: strengthened preconditions, weakened postconditions, changed behavior

**Learning Outcomes:**
- Understand LSP through discovery learning
- Create contract tests
- Ensure test fakes match production behavior
- Document behavioral contracts

**Exercise Framework:**
- Three-stage approach (generic lab → domain application → reflection)
- Discovery learning (run failing test first)
- LSP Red Flags comparison table

---

#### Week 14: Interface Segregation Principle (ISP)
**Topics:**
- Interface Segregation Principle
- Fat interface problems
- Client-specific interfaces

**Key Concepts:**
- Clients should not depend on methods they don't use
- Split fat interfaces into focused contracts
- One class can implement multiple interfaces
- DI registration patterns for multiple interfaces

**Learning Outcomes:**
- Identify fat interfaces
- Split interfaces appropriately
- Update dependencies to use minimal interfaces
- Register one class as multiple interfaces

---

#### Week 15: Dependency Inversion Principle (DIP)
**Topics:**
- Dependency Inversion Principle
- High-level modules depend on abstractions
- Infrastructure abstractions

**Key Concepts:**
- Depend on abstractions, not concretions
- Invert dependencies on infrastructure
- Abstract time, caching, external services
- Enable testability through abstraction

**Learning Outcomes:**
- Create infrastructure abstractions
- Invert dependencies on concrete types
- Use abstractions for testability
- Audit constructors for concrete dependencies

**Exercise Framework:**
- Clear before/after comparison
- System clock abstraction example
- Fake clock for testing

---

#### Week 16: File Organization & Module Structure
**Topics:**
- Clean Code Chapter 5: Formatting
- Clean Code Chapter 8: Boundaries
- Namespace organization
- File structure best practices

**Key Concepts:**
- Organize files by feature/module
- Namespaces match folder structure
- Consolidate DI registrations
- Remove unused code

**Learning Outcomes:**
- Organize codebase logically
- Establish namespace conventions
- Consolidate configuration
- Maintain clean structure

---

#### Week 17: Unit Testing & TDD
**Topics:**
- Clean Code Chapter 9: Unit Tests
- Test-Driven Development (TDD)
- F.I.R.S.T. principles
- Arrange-Act-Assert pattern

**Key Concepts:**
- Tests are first-class citizens
- TDD: Red-Green-Refactor cycle
- Tests should be fast, independent, repeatable, self-validating, timely
- Mock vs. Fake decision guide
- Coverage anti-patterns

**Learning Outcomes:**
- Write focused unit tests
- Practice TDD workflow
- Achieve 80%+ coverage on business logic
- Choose appropriate test doubles

**Exercise Framework:**
- Fix broken tests first (builds confidence)
- Mock vs Fake decision guide
- Coverage anti-patterns section

---

#### Week 18: Code Smells & Refactoring
**Topics:**
- Clean Code Chapter 17: Smells and Heuristics
- Common code smells
- Refactoring techniques

**Key Concepts:**
- Long method, duplicate code, shotgun surgery
- Data clumps, primitive obsession
- Refactoring without changing behavior
- Regression safeguards

**Learning Outcomes:**
- Identify code smells
- Apply appropriate refactorings
- Ensure behavior unchanged
- Document refactoring impact

---

#### Week 19: Essential Design Patterns
**Topics:**
- Design patterns overview
- Factory pattern
- Strategy pattern (review)
- Repository pattern (review)

**Key Concepts:**
- When to apply patterns
- Factory for object creation
- Strategy for algorithms
- Pattern selection criteria

**Learning Outcomes:**
- Implement Factory pattern
- Review existing pattern usage
- Document pattern rationale
- Avoid over-patterning

**Exercise Framework:**
- Clear explanation of Factory vs. Mapper
- Before/after comparison
- When to use Factory decision guide

---

#### Week 20: Code Review & Collaboration
**Topics:**
- Code review best practices
- Collaborative development
- Feedback techniques

**Key Concepts:**
- Review for behavior, not style
- Provide actionable feedback
- Respond to reviews promptly
- Comment quality examples

**Learning Outcomes:**
- Perform professional code reviews
- Provide constructive feedback
- Respond to review comments
- Use review as learning opportunity

**Exercise Framework:**
- Comment quality examples
- Review checklist
- Sample PR reviews

---

#### Week 21: API Design & Documentation
**Topics:**
- RESTful API design
- API versioning
- Swagger/OpenAPI documentation
- Pagination

**Key Concepts:**
- REST principles
- Resource naming conventions
- HTTP status codes
- API versioning strategies
- Pagination patterns

**Learning Outcomes:**
- Design RESTful APIs
- Implement pagination
- Version APIs appropriately
- Document APIs with Swagger

**Exercise Framework:**
- Configuration examples
- Pagination templates
- Swagger setup guide

---

#### Week 22: Performance & Caching
**Topics:**
- Clean Code Chapter 13: Concurrency
- Performance optimization
- Caching strategies
- Async patterns

**Key Concepts:**
- In-memory caching
- Cache invalidation strategies
- Async/await best practices
- Response compression
- Performance monitoring

**Learning Outcomes:**
- Implement caching abstraction
- Design cache invalidation
- Audit async patterns
- Monitor cache effectiveness

**Exercise Framework:**
- Caching examples and patterns
- Cache key generation
- TTL guidelines
- Testing cache behavior

---

#### Week 23: Final Polish & Presentation
**Topics:**
- Production readiness
- Documentation completion
- Demo preparation
- Reflection

**Key Concepts:**
- Production-ready checklist
- Demo script structure
- Final retrospective
- Next steps planning

**Learning Outcomes:**
- Deliver production-ready artifacts
- Create clear documentation
- Present work effectively
- Reflect on learning journey

**Exercise Framework:**
- Demo script template
- Final retro template
- Production-ready checklist

---

## Clean Code Principles

### Chapter 1: Clean Code

**Core Principle:** Professional developers keep the codebase cleaner with every touch. Quality is non-negotiable, and messy code is a liability.

**Key Techniques:**
- Boy Scout Rule: Leave code cleaner than you found it
- Professional responsibility for code quality
- Customer-centric design

**Common Anti-Patterns:**
- Accepting messy code
- "I'll clean it up later"
- Blaming others for bad code

**TaskFlowAPI Application Opportunities:**
- Week 1: Establish quality mindset
- Throughout: Apply Boy Scout Rule in every exercise
- Week 23: Final polish demonstrates professional responsibility

**Maps to Curriculum Week:** Week 1

**How this advances the API toward portfolio quality:** Establishes foundation for all subsequent improvements

---

### Chapter 2: Meaningful Names

**Core Principle:** Names should reveal intention. Avoid mental mapping.

**Key Techniques:**
- Use intention-revealing names
- Avoid disinformation
- Make meaningful distinctions
- Use pronounceable names
- Use searchable names
- Avoid encodings
- Avoid abbreviations
- Class names: nouns
- Method names: verb phrases

**Common Anti-Patterns:**
- Single-letter variables (except loop iterators)
- Abbreviations (svc, dt, req)
- Noise words (Manager, Helper, Handler)
- Misleading names

**TaskFlowAPI Application Opportunities:**
- Week 2: Refactor TasksController bad names (svc, s, t, dt, req)
- Week 2: Refactor ITaskService bad names (Get, GetOne, Add)
- Throughout: Apply naming principles in all new code

**Maps to Curriculum Week:** Week 2

**How this advances the API toward portfolio quality:** Self-documenting code reduces need for comments, improves maintainability

---

### Chapter 3: Functions

**Core Principle:** Functions should be small and do one thing.

**Key Techniques:**
- Small functions
- Do one thing
- One level of abstraction per function
- Stepdown Rule
- Use descriptive names
- Function arguments: fewer is better
- Have no side effects
- Command Query Separation
- Prefer exceptions to error codes
- Don't repeat yourself

**Common Anti-Patterns:**
- Long methods (100+ lines)
- Multiple responsibilities
- Mixed abstraction levels
- Too many arguments
- Flag arguments
- Output arguments

**TaskFlowAPI Application Opportunities:**
- Week 4: Refactor ReportsController.GenerateProjectSummaryReport (100+ lines)
- Week 11: Extract methods from TaskService (SRP)
- Throughout: Keep functions small and focused

**Maps to Curriculum Week:** Week 4

**How this advances the API toward portfolio quality:** Small functions are easier to test, understand, and maintain

---

### Chapter 4: Comments

**Core Principle:** Comments are failures to express in code. Explain yourself in code.

**Key Techniques:**
- Delete comments that repeat code
- Keep comments that explain "why"
- Good comments: legal, informative, explanation of intent, warning, TODO, amplification
- Bad comments: mumbling, redundant, misleading, mandated, journal, noise, closing brace, attributions, commented-out code

**Common Anti-Patterns:**
- Comments explaining "what" code does
- Outdated comments
- Commented-out code
- Noise comments

**TaskFlowAPI Application Opportunities:**
- Week 3: Remove redundant comments from Program.cs
- Week 3: Keep only "why" comments
- Throughout: Replace comments with expressive code

**Maps to Curriculum Week:** Week 3

**How this advances the API toward portfolio quality:** Self-documenting code is more maintainable than commented code

---

### Chapter 6: Objects and Data Structures

**Core Principle:** Balance data exposure with behavior. Objects hide data and expose functions. Data structures expose data and have no meaningful functions.

**Key Techniques:**
- DTOs are data structures (expose data)
- Entities are objects (hide data, expose behavior)
- Law of Demeter
- Data Transfer Objects

**Common Anti-Patterns:**
- Anemic domain models
- Exposing internal structure
- Hybrid objects/data structures

**TaskFlowAPI Application Opportunities:**
- Week 7: Transform TaskEntity from anemic to rich domain model
- Week 9: Understand DTO vs Entity distinction
- Throughout: Keep DTOs as data structures, entities as objects

**Maps to Curriculum Week:** Week 7, Week 9

**How this advances the API toward portfolio quality:** Rich domain models encapsulate business logic, DTOs provide clean API contracts

---

### Chapter 7: Error Handling

**Core Principle:** Prefer exceptions to error codes. Keep error-handling code separate from happy-path logic.

**Key Techniques:**
- Use exceptions rather than return codes
- Write try-catch-finally first
- Use unchecked exceptions
- Provide context with exceptions
- Define exception classes in terms of caller's needs
- Don't return null
- Don't pass null

**Common Anti-Patterns:**
- Error codes
- Returning null
- Swallowing exceptions
- Generic exceptions

**TaskFlowAPI Application Opportunities:**
- Week 10: Implement custom exceptions (DomainValidationException, TaskNotFoundException)
- Week 10: Configure global exception handling
- Throughout: Use exceptions appropriately

**Maps to Curriculum Week:** Week 10

**How this advances the API toward portfolio quality:** Robust error handling improves reliability and user experience

---

### Chapter 9: Unit Tests

**Core Principle:** Tests are first-class citizens. They enable clean code.

**Key Techniques:**
- F.I.R.S.T. principles (Fast, Independent, Repeatable, Self-validating, Timely)
- One assert per test
- Test one concept per test
- Arrange-Act-Assert pattern
- Clean tests are readable

**Common Anti-Patterns:**
- Slow tests
- Dependent tests
- Non-deterministic tests
- Multiple assertions testing different things
- Test code that's hard to read

**TaskFlowAPI Application Opportunities:**
- Week 17: Write unit tests for TaskService
- Week 17: Practice TDD workflow
- Throughout: Maintain test quality

**Maps to Curriculum Week:** Week 17

**How this advances the API toward portfolio quality:** Comprehensive tests provide confidence for refactoring and prevent regressions

---

### Chapter 10: Classes

**Core Principle:** Classes should be small and have a single responsibility.

**Key Techniques:**
- Single Responsibility Principle
- Cohesion
- Organizing for change
- Classes should be small
- Encapsulation

**Common Anti-Patterns:**
- God classes
- Low cohesion
- Multiple responsibilities
- Public fields

**TaskFlowAPI Application Opportunities:**
- Week 7: Encapsulate TaskEntity properties
- Week 11: Extract responsibilities from TaskService
- Week 16: Organize classes into focused modules

**Maps to Curriculum Week:** Week 7, Week 11, Week 16

**How this advances the API toward portfolio quality:** Small, focused classes are easier to test, understand, and maintain

---

### Chapter 11: Systems

**Core Principle:** Separate construction from use. Keep boundaries clean.

**Key Techniques:**
- Separate construction from use
- Dependency Injection
- Cross-cutting concerns
- Keep boundaries clean
- Use factories for construction

**Common Anti-Patterns:**
- Construction mixed with use
- Direct instantiation
- Tight coupling
- Unclear boundaries

**TaskFlowAPI Application Opportunities:**
- Week 8: Implement repository pattern (isolate data access)
- Week 9: Implement service layer (separate policy from implementation)
- Week 15: Apply dependency inversion
- Week 19: Use factory pattern

**Maps to Curriculum Week:** Week 8, Week 9, Week 15, Week 19

**How this advances the API toward portfolio quality:** Clean boundaries enable testability and maintainability

---

### Chapter 13: Concurrency

**Core Principle:** Concurrency is a decoupling strategy. It helps structure what and when.

**Key Techniques:**
- Keep concurrency-related code separate
- Limit access to shared data
- Use data copies
- Threads should be as independent as possible
- Know your library
- Know your execution model
- Beware dependencies between synchronized methods

**Common Anti-Patterns:**
- Shared mutable state
- Race conditions
- Deadlocks
- Synchronization in wrong places

**TaskFlowAPI Application Opportunities:**
- Week 22: Apply async patterns correctly
- Week 22: Implement caching (concurrency considerations)
- Throughout: Use async/await properly

**Maps to Curriculum Week:** Week 22

**How this advances the API toward portfolio quality:** Proper async patterns improve performance and scalability

---

### Chapter 17: Smells and Heuristics

**Core Principle:** Code smells indicate problems. Heuristics guide refactoring.

**Key Techniques:**
- Long method, large class
- Long parameter list
- Data clumps
- Primitive obsession
- Long chains
- Comments
- Duplicate code
- Dead code

**Common Anti-Patterns:**
- All the code smells listed in the chapter
- Ignoring smells
- Not refactoring

**TaskFlowAPI Application Opportunities:**
- Week 1: Identify smells in scavenger hunt
- Week 18: Find and fix remaining smells
- Throughout: Apply Boy Scout Rule

**Maps to Curriculum Week:** Week 1, Week 18

**How this advances the API toward portfolio quality:** Removing smells improves code quality and maintainability

---

## Supplementary Readings

### In Time Tec Quality Manifesto

**Key Principles (9 foundational values):**
1. Customer-Centric Design
2. Technical Excellence
3. Continuous Improvement
4. Clear Communication
5. Professional Responsibility
6. Collaboration
7. Quality Assurance
8. Domain Knowledge
9. [Additional principles as documented]

**Application Throughout Curriculum:**
- Week 1: Introduction to manifesto
- All weeks: Connect exercises to manifesto values
- Journal prompts: Link principles to manifesto

**TaskFlowAPI Application:**
- Every exercise should demonstrate how clean code serves customer needs
- Quality is non-negotiable
- Professional responsibility for code quality

---

## Learning Progression & Dependencies

### Foundation Dependencies

**Week 1 → Week 2:**
- Scavenger hunt finds bad names → Week 2 fixes them
- Understanding codebase structure → Refactoring with confidence

**Week 2 → Week 3:**
- Good names reduce need for comments
- Self-documenting code → Fewer comments needed

**Week 3 → Week 4:**
- Clean code → Easier to extract functions
- Understanding structure → Better function design

**Week 4 → Week 7:**
- Function extraction skills → Method extraction in entities
- Understanding responsibilities → Encapsulation decisions

### Architecture Dependencies

**Week 7 → Week 8:**
- Encapsulated entities → Repository works with rich domain models
- Domain behaviors → Repository uses entity methods

**Week 8 → Week 9:**
- Repository implemented → Service uses repository
- Data access patterns → Service orchestration

**Week 9 → Week 10:**
- Service methods implemented → Add validation
- DTOs in place → Validation on DTOs

**Week 10 → Week 11:**
- Service has multiple responsibilities → Extract them
- Validation in place → Extract validators

### SOLID Dependencies

**Week 11 → Week 12:**
- SRP applied → Service is focused
- Focused service → Easier to extend (OCP)

**Week 12 → Week 13:**
- Filter strategies → Need contract compliance (LSP)
- Interfaces in place → Test with fakes (LSP)

**Week 13 → Week 14:**
- Contract understanding → Interface design (ISP)
- Test fakes → Interface segregation benefits

**Week 14 → Week 15:**
- Interface design → Dependency inversion
- Abstractions in place → Invert infrastructure dependencies

### Quality & Patterns Dependencies

**Week 15 → Week 16:**
- Abstractions created → Organize into modules
- Dependencies inverted → Clean file organization

**Week 16 → Week 17:**
- Organized code → Easier to test
- Abstractions in place → Testable code

**Week 17 → Week 18:**
- Tests in place → Refactor with confidence
- Test coverage → Identify remaining smells

**Week 18 → Week 19:**
- Clean code → Apply patterns appropriately
- Smells removed → Pattern application clearer

**Week 19 → Week 20:**
- Patterns applied → Review pattern usage
- Clean code → Review quality

**Week 20 → Week 21:**
- Reviewed code → Document API
- Clean architecture → Professional API design

**Week 21 → Week 22:**
- API designed → Optimize performance
- Documentation in place → Performance considerations

**Week 22 → Week 23:**
- Optimized API → Final polish
- All features complete → Production readiness

---

## Stated Learning Outcomes

### By End of Course, Students Should:

1. **Write Clean Code:**
   - Meaningful names throughout
   - Small, focused functions
   - Self-documenting code (minimal comments)
   - Encapsulated classes with clear responsibilities

2. **Apply SOLID Principles:**
   - Single Responsibility Principle
   - Open/Closed Principle
   - Liskov Substitution Principle
   - Interface Segregation Principle
   - Dependency Inversion Principle

3. **Use Design Patterns:**
   - Repository pattern
   - Strategy pattern
   - Factory pattern
   - Appropriate pattern selection

4. **Write Quality Tests:**
   - Unit tests with 80%+ coverage
   - TDD workflow
   - Appropriate test doubles (mocks/fakes)
   - Test organization and readability

5. **Design Professional APIs:**
   - RESTful design
   - API versioning
   - Comprehensive documentation
   - Error handling

6. **Work Professionally:**
   - Git workflow mastery
   - Code review skills
   - Professional communication
   - Quality mindset

---

## How Each Week Builds on Previous Weeks

### Progressive Complexity

**Weeks 1-4:** Foundation skills (naming, functions, comments)
- Each week refines code quality
- Skills compound (good names → fewer comments → better functions)

**Weeks 5-9:** Architecture implementation
- Build on foundation to create working system
- Each layer depends on previous layer

**Weeks 10-15:** SOLID principles
- Refactor existing code applying principles
- Each principle builds on previous understanding

**Weeks 16-20:** Quality and patterns
- Organize and improve existing code
- Apply advanced concepts to mature codebase

**Weeks 21-23:** Production readiness
- Polish complete system
- Professional presentation

### Skill Transfer

**Naming (Week 2) → Functions (Week 4):**
- Good names make function extraction easier
- Naming skills apply to extracted methods

**Functions (Week 4) → Encapsulation (Week 7):**
- Function extraction skills → Method design in classes
- Understanding responsibilities → Encapsulation decisions

**Encapsulation (Week 7) → Repository (Week 8):**
- Rich domain models → Repository uses entity methods
- Encapsulation patterns → Data access patterns

**Repository (Week 8) → Service (Week 9):**
- Data access patterns → Business logic orchestration
- Async patterns → Service async patterns

**Service (Week 9) → SOLID (Weeks 11-15):**
- Service has multiple responsibilities → Extract (SRP)
- Service needs extension → Strategy pattern (OCP)
- Service uses repository → Contract compliance (LSP)
- Service dependencies → Interface segregation (ISP)
- Service uses concrete types → Dependency inversion (DIP)

---

## Notes for Exercise Design

### When Designing Exercises, Consider:

1. **What exists at this point?**
   - Reference `taskflowapi-architecture.md` for current API state
   - Understand what students have built so far

2. **What principle is being taught?**
   - Explicitly name the Clean Code principle or learning objective
   - Connect to specific chapter/section

3. **How does this build on previous weeks?**
   - Reference previous week's work
   - Show progression

4. **How does this advance the API?**
   - Explain portfolio value
   - Show improvement in code quality

5. **Is this appropriate for junior developers?**
   - Can be completed in ≤2 hours?
   - Clear instructions?
   - Appropriate complexity?

6. **Does this create a teachable moment?**
   - Theory meets practice?
   - Clear before/after?
   - Measurable outcome?

---

## Maintenance Notes

**When updating this document:**
- Update week-by-week breakdown when curriculum changes
- Add new Clean Code principles as they're introduced
- Document new supplementary readings
- Update learning progression as dependencies change
- Keep stated learning outcomes aligned with actual curriculum

**This document should be referenced when:**
- Designing new exercises
- Verifying principle-to-exercise coupling
- Understanding curriculum dependencies
- Planning curriculum improvements
