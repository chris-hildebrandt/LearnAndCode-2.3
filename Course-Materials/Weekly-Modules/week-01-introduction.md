# Week 1: Introduction & Quality Manifesto

Welcome to week 1. This week, we will be introducing you to the importance of clean code and how it aligns with In Time Tec's Quality Manifesto. The Quality Manifesto outlines 9 foundational values that guide software delivery at In Time Tec. These principles define what "done" means in a professional context and will be the focus of every refactoring and discussion throughout this course. Our focus will be on customer-centric design and a writing clean code can contribute to better solutions. It is expected that you will have completed everything on this page prior to our meeting.

## 1. Learning Objectives

- Internalize In Time Tec’s Quality Manifesto principles and how they translate to `TaskFlow` API work.
- Understand the importance of clean code and its relationship to customer-centric design.
- Stand up the repository locally.
- Map the 21-week milestones to the `TaskFlow` API architecture.
- Identify escalation paths when blocked more than 30 minutes.

## 2. Reading & Resources (45 min)

- **`README.md`** (read the entire document).
- **In Time Tec Quality Manifesto** (Review the entire document).
- **Clean Code – Chapter 1: Clean Code (pp. 1-14)**.
  - Focus on the definition of clean code, professional responsibility, and the “Boy Scout Rule.”
  - Summary: Professional developers keep the codebase cleaner with every touch. Quality is non-negotiable, and messy code is a liability to partners.

## 3. This Week’s Work

- Fork and clone the repository.
- Run the `TaskFlow` API locally and explore Swagger.
- Review repository structure, intentional smells, and weekly docs overview.

## 4. Files to Modify

- This file (`Course-Materials/Weekly-Modules/week-01-introduction.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`
- No code changes yet—this week is orientation. But it is recommended that you browse the repo to get familiar with the project layout and folder structure.

## 5. Step-by-Step Instructions

1. Fork the repo on GitHub. Clone your fork locally (`git clone ...`). (Alternatively you may use Github Codespaces for your IDE) ![creating a codespace](image.png)
2. Follow `SETUP.md` exactly. Stop and document any blockers.
3. Run `dotnet run` inside `TaskFlowAPI/` and verify Swagger at `https://localhost:5001/swagger`.
4. Run `dotnet test TaskFlowAPI.sln` from the repo root. Confirm only skipped tests.
5. Read this week's reading assignments.
6. Read and fill out the `Journal` section of this document, make notes from your learning in the `Discussion Prep` section.
7. Check off Week 1 in `WEEKLY_PROGRESS.md` once everything above is done.

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

1. Create branch `week-01/<your-name>`.
2. Commit changes with message `Week 01 – environment ready`.
3. Open PR using `.github/pull_request_template.md`.
4. Create issue using `.github/ISSUE_TEMPLATE/weekly-submission.md` and link your PR.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *The Cost of Mess: Robert C. Martin argues messy code is a professional failure. How does this align with the Manifesto's value of Refactoring?*
- *Customer Impact: How does a lack of clean code (e.g., poor organization, missing tests) directly harm the value of Customer Centric Design? Give one specific example in a task management app.*
- *Your Role: Which Manifesto value do you think will be the most challenging for you to implement in the `TaskFlow` API project and why?*
- *NFRs: Beyond just functionality, what is one Non-Functional Requirement (NFR) that the `TaskFlow` API must meet to be considered "quality" by a partner?*

### Discussion Prep:
- *What did you learn about the curriculum structure?*
- *Where do you expect to struggle?*
- *What’s your plan when blocked longer than 30 minutes?*

## 10. Time Estimate

- 45 min – Read `README.md` + Quality Manifesto + Clean Code chapter.
- 10 min – Fork + clone repo.
- 10 min – Environment setup + build/test verification.
- 15 min – Journal + discussion prep.
- 10 min – Create PR/issue.
**Total:** ~1 hour 30 minutes.

## 11. Getting Help

(please fill in the blanks)
- Team chat `#_____` for quick questions.
- Mentor: `_____`.
- Schedule 1:1 with mentor or L&C leadership if still blocked after chat/office hours.