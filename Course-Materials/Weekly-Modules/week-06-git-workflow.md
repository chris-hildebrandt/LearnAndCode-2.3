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
- **REVISED:** Make a meaningful code refactor (validation extraction) instead of TODO comment
- **NEW:** Practice git recovery techniques (amend, cherry-pick, conflict resolution)
- Document your git "oops" moments and how you recovered

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs` (NEW - extract validation method)
- `docs/week-06-git-recovery-log.md` (NEW - document recovery scenarios)
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

- ✅ Git Tutorial completed (learngitbranching.js.org)
- ✅ Made meaningful code improvement (validation extraction, not TODO)
- ✅ Demonstrated 3 git recovery techniques (amend, cherry-pick, reset)
- ✅ Resolved at least one merge conflict manually
- ✅ Wrote clear commit message following convention (verb: description format)
- ✅ `docs/week-06-git-recovery-log.md` documents all "oops" scenarios and solutions
- ✅ PR open with at least one review comment (self-review acceptable)

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-06-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 6 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

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

- 60 min – Complete Git tutorial (if not already done).
- **20 min – Part A: Meaningful code change (validation extraction) - NEW**
- **15 min – Part B: Git recovery scenarios (amend, cherry-pick) - NEW**
- **15 min – Part C: Conflict resolution practice - NEW**
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~70 minutes (increased, but teaches real git skills).

## 11. Git Recovery Commands Reference (NEW)

**Common "Oops" Scenarios:**

### Amend Last Commit
```bash
# Change commit message
git commit --amend -m "new message"

# Add forgotten file without changing message
git add forgotten-file.cs
git commit --amend --no-edit
```

### Wrong Branch
```bash
# Save commit hash
git log -1  # copy hash

# Move to correct branch
git checkout -b correct-branch main
git cherry-pick <hash>

# Clean up wrong branch
git checkout wrong-branch
git reset --hard HEAD~1
git branch -D wrong-branch
```

### Undo Last Commit (Keep Changes)
```bash
git reset --soft HEAD~1  # Undo commit, keep changes staged
git reset HEAD~1         # Undo commit, keep changes unstaged
git reset --hard HEAD~1  # Undo commit, discard changes (DANGEROUS!)
```

### Resolve Merge Conflict
```bash
git merge feature-branch
# Conflict! Edit files to resolve
git add resolved-file.cs
git commit -m "merge: resolve conflicts"
```

### View What Changed
```bash
git diff                 # Unstaged changes
git diff --staged        # Staged changes
git diff HEAD~1          # Last commit changes
git log --oneline -5     # Last 5 commits
```

**When to Use:**
- `--amend`: Fix typo or add forgotten file (before pushing)
- `cherry-pick`: Move commit to correct branch
- `reset --soft`: Undo commit but keep work
- `reset --hard`: Start over (use cautiously!)

**Golden Rule:** Never rewrite history AFTER pushing (unless you know what you're doing)

---

## 12. Additional Resources

- **[Pro Git Book](https://git-scm.com/book/en/v2)**
- **[Git for Professionals by freeCodeCamp](https://www.freecodecamp.org/news/git-for-professionals/)**
- **[Oh Shit, Git!?!](https://ohshitgit.com/)** - Plain English git error recovery
- **[Atlassian Git Tutorials](https://www.atlassian.com/git/tutorials)** - Interactive lessons
