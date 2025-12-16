# Week 1: Introduction & Quality Manifesto

Welcome to week 1. This week, we will be introducing you to the importance of clean code and how it aligns with In Time Tec's Quality Manifesto. The Quality Manifesto outlines 9 foundational values that guide software delivery at In Time Tec. These principles define what "done" means in a professional context and will be the focus of every assignment and discussion throughout this course. Our focus will be on customer-centric design and how writing clean code creates abundance and ensures our partners are delighted with their ROI. It is expected that you will have completed everything on this page prior to our next meeting.

## 1. Learning Objectives

- Internalize In Time Tec's Quality Manifesto principles and how they translate to `TaskFlow` API work.
- Understand the importance of clean code and its relationship to customer-centric design.
- Stand up the repository locally.
- Map the 23-week milestones to the `TaskFlow` API architecture.
- Identify escalation paths when blocked more than 30 minutes.

## 2. Reading & Resources (45 min)

- **`README.md`** (read the entire document).
- **In Time Tec Quality Manifesto** (review the entire document).
- **Clean Code – Chapter 1: Clean Code (pp. 1-14)**.
  - Focus on the definition of clean code, professional responsibility, and the "Boy Scout Rule."
  - Summary: Professional developers keep the codebase cleaner with every touch. Quality is non-negotiable, and messy code is a liability to partners.

## 3. This Week's Work

- Fork and clone the repository.
- Run the `TaskFlow` API locally and explore Swagger.
- Review repository structure, intentional smells, and weekly docs overview.

## 4. Files to Modify

- This file (`Course-Materials/Weekly-Modules/week-01-introduction.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`
- No code changes yet—this week is orientation. But it is recommended that you browse the repo to get familiar with the project layout and folder structure.

## 5. Step-by-Step Instructions

1. Fork the repo on GitHub. Use Github Codespaces for your IDE [creating a codespace](image.png) (Alternatively you may clone your fork locally (`git clone ...`)).
2. Follow `SETUP.md` exactly. Stop and document any blockers.
3. Run `dotnet run` inside `TaskFlowAPI/` and verify Swagger at `https://localhost:5001/swagger`.
4. Run `dotnet test TaskFlowAPI.sln` from the repo root. Confirm only skipped tests.
5. Read this week's reading assignments.
6. Complete Code Smell Scavenger Hunt (see Section 11 below).
7. Read and fill out the `Journal` section of this document, make notes from your learning in the `Discussion Prep` section.
8. Check off Week 1 in `WEEKLY_PROGRESS.md` once everything above is done.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- Repository forks successfully and builds locally.
- Swagger UI reachable and shows `TaskFlow` endpoints.
- `dotnet test` executes with two skipped tests and zero failures.
- Escalation plan documented in this file.
- Week 1 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-01-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 1 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

**After completing Code Smell Scavenger Hunt:**

1. **Customer Impact Example:** 
   You found the abbreviation `svc` in TasksController. Imagine a new teammate needs to add a feature to create bulk tasks. Walk through their confusion:
   - What questions will they ask about `svc`?
   - How many minutes might they waste?
   - How does this delay = increased cost to customer?

2. **Bug Risk Example:**
   The `GenerateProjectSummaryReport` method has 100+ lines. 
   - If a bug exists in "calculate percentage" logic, how hard is it to find?
   - How many OTHER things might break when you fix it?

3. **Manifesto Connection:**
   - *The Cost of Mess: Robert C. Martin argues messy code is a professional failure. How does this align with the Manifesto's value of Refactoring?*
   - *Customer Impact: How does a lack of clean code (e.g., poor organization, missing tests) directly harm the value of Customer Centric Design? Give one specific example in a task management app.*
   - *Your Role: Which Manifesto value do you think will be the most challenging for you to implement in the `TaskFlow` API project and why?*
   - *NFRs: Beyond just functionality, what is one Non-Functional Requirement (NFR) that the `TaskFlow` API must meet to be considered "quality" by a partner?*

### Discussion Prep:

- *What did you learn about the curriculum structure?*
- *Where do you expect to struggle?*
- *What's your plan when blocked longer than 30 minutes?*

## 10. Time Estimate

- 45 min – Reading (`README.md` + Quality Manifesto + Clean Code chapter).
- 15 min – Architecture overview (Section 11).
- 10 min – Fork and clone repository.
- 10 min – Environment setup and build/test verification.
- 30 min – Code Smell Scavenger Hunt (Section 12).
- 20 min – Journal and discussion prep.
- 10 min – Create PR and merge.

**Total:** ~2 hours 20 minutes.

## 11. Architecture Overview

**Goal:** Understand the TaskFlowAPI structure before diving into code.

**Time:** 15 minutes.

### Visual Architecture Reference

Before exploring the codebase, review these architecture diagrams to understand the big picture:

1. **Current State Class Diagram** - Shows the initial structure (Weeks 1-8).
   - See: `docs/architecture-diagrams.md` (Section 1 - Simplified Diagram for Week 1)
   - Or: `Course-Materials/About TaskFlowAPI/TaskFlowAPI_Current_State.md` for detailed explanation.

2. **Future State Class Diagram** - Shows the target architecture (Week 23).
   - See: `docs/architecture-diagrams.md` (Section 2)
   - Or: `Course-Materials/About TaskFlowAPI/TaskFlowAPI_Future_State.md` for detailed explanation.

3. **Data Flow Diagram** - Shows how requests flow through the system.
   - See: `docs/architecture-diagrams.md` (Section 3)

4. **Component Diagram** - Shows high-level components and dependencies.
   - See: `docs/architecture-diagrams.md` (Section 5)

5. **Architecture Evolution Timeline** - Shows how the architecture evolves over 23 weeks.
   - See: `docs/architecture-diagrams.md` (Section 6)

### Key Architecture Concepts

**Layered Architecture:**
- **Controllers** - Handle HTTP requests/responses.
- **Services** - Business logic and orchestration.
- **Repositories** - Data access abstraction.
- **Entities** - Domain models (database tables).

**Design Patterns You'll Learn:**
- Repository Pattern (Week 8).
- Strategy Pattern (Week 12 - Filters).
- Factory Pattern (Week 19).
- CQRS Pattern (Weeks 14 & 22 - Reader/Writer separation and caching/read-write split).

**SOLID Principles:**
- Single Responsibility (Week 11).
- Open/Closed (Week 12).
- Liskov Substitution (Week 13).
- Interface Segregation (Week 14).
- Dependency Inversion (Week 15).

### Your Task

1. Open `docs/architecture-diagrams.md`.
2. Review the Current State Class Diagram.
3. Compare it to the Future State Class Diagram.
4. Note the differences - these are what you'll build over 23 weeks!

**Questions to Consider:**
- What patterns do you recognize?
- What's missing in the current state?
- How does the architecture support testability?

## 12. Code Smell Scavenger Hunt

**Goal:** Build awareness of code patterns you'll improve over 23 weeks.

**Time:** 30 minutes.

**Instructions:**

1. Create `docs/week-01-codebase-inventory.md` in your fork.
2. Browse these files and identify code smells:
   - `TaskFlowAPI/Controllers/TasksController.cs`
   - `TaskFlowAPI/Controllers/ReportsController.cs`
   - `TaskFlowAPI/Entities/TaskEntity.cs`
   - `TaskFlowAPI/Services/Tasks/TaskService.cs`

3. Document your findings in a simple table:

| Smell Type | File | Location |
|-----------|------|----------|
| Abbreviation | TasksController.cs | Line ~15 - `svc` |
| Long Method | ReportsController.cs | Line ~24 - `GenerateProjectSummaryReport()` |
| (add your findings) | | |

**Smell types to look for:**
- Abbreviations (e.g., `svc`, `dt`, `rpt`).
- Single-letter variables (outside loop iterators).
- Unclear method names.
- Long methods (100+ lines).
- Public setters on properties.
- Classes doing multiple things.

**Why This Matters:**
- Builds pattern recognition before coding refactorings.
- Creates your own roadmap of improvements.
- You'll reference these in future weeks.

## 13. Additional Resources

- **[Architecture Diagrams](../../../docs/architecture-diagrams.md)** - Complete visual reference for TaskFlowAPI architecture.
- **[TaskFlowAPI Current State](../About TaskFlowAPI/TaskFlowAPI_Current_State.md)** - Detailed current state analysis.
- **[TaskFlowAPI Future State](../About TaskFlowAPI/TaskFlowAPI_Future_State.md)** - Target architecture vision.

## 14. Getting Help

(Please fill in the blanks.)

- Team chat `#_____` for quick questions.
- Mentor: `_____`.
- Schedule 1:1 with mentor or L&C leadership if still blocked after chat/office hours.