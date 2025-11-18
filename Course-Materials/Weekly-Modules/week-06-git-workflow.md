# Week 6: Git Workflow & Collaboration

This week, we will introduce version control using Git, aligning with In Time Tec's emphasis on collaboration, communication, and continuous integration and deployment.

## 1. Learning Objectives

- Execute feature branch workflow (branch → commit → PR → review).
- Write meaningful commit messages tied to small scopes.
- Become comfortable with basic and advanced git commands.

## 2. Reading & Resources (45 min)

- **[Git Documentation](https://git-scm.com/doc)** – Skim basics or commands you rarely use.
- **[Atlassian Git Tutorials](https://www.atlassian.com/git/tutorials)** – Review branching/merging best practices.
- **[YouTube: Tools & Concepts for Mastering Version Control with Git](https://www.youtube.com/watch?v=Uszj_k0DGsg)** (optional, 15 min) – Quick video primer.
- Optional deep dive: **[Pro Git Book](https://git-scm.com/book/en/v2)** (selected chapters) & **[Git for Professionals by freeCodeCamp](https://www.freecodecamp.org/news/git-for-professionals/)** article once basics are solid.

## 3. This Week’s Work

- Complete the following Git tutorial: **[https://learngitbranching.js.org/?locale=en_US](https://learngitbranching.js.org/?locale=en_US)**
- Analyze how Git is being used in your project, and perform basic version control tasks such as branching, merging, and conflict resolution.

## 4. Files to Modify

- This file (`Course-Materials/Weekly-Modules/week-06-git-workflow.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-06/<your-name>`.
2. Add a comment in `TasksController` describing future priority filter (`// TODO Week 12: support ?priority=`) and ensure naming matches new conventions.
3. Update `TaskFlowAPI.http` with a `GET` example using `?priority=High`.
4. Commit #1 `chore: document upcoming priority filter`.
5. Create a markdown note in this file under “Review Notes” about what kind of feedback you expect.
6. Commit #2 `docs: capture Git workflow notes`.
7. Push branch, open PR, and add your mentor as reviewer.
8. Share PR link in chat; ask for at least one comment.
9. Respond to review comment (or add a self-review comment if no feedback within 24h). No merge yet.

## 6. How to Test

No testing this week.

## 7. Success Criteria

- Git Tutorial Completed.

## 8. Submission Process

- Link the open PR (even if not merged) in your weekly issue.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Commit Hygiene:* What criteria did you use to decide a commit was “complete”? Capture one example.
- *Review Expectations:* What type of feedback are you hoping to receive on this PR, and why?

### Discussion Prep:
- *What made your commits small and reviewable?*
- *How did you handle feedback or lack thereof?*
- *What automation could enforce this workflow?*
- *Where would advanced Git features (rebase, cherry-pick) help future branches?*

## 10. Time Estimate

- 10 min – Plan Git workflow.
- 25 min – Implement placeholder + commits.
- 15 min – PR + review response.
**Total:** ~50 minutes.

## 11. Additional Resources

- **[Pro Git Book](https://git-scm.com/book/en/v2)**
- **[Git for Professionals by freeCodeCamp](https://www.freecodecamp.org/news/git-for-professionals/)**
