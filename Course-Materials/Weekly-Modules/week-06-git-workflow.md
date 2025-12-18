# Week 6: Git Workflow & Collaboration

This week, we will focus on version control using Git, aligning with In Time Tec's emphasis on collaboration, communication, and continuous integration and deployment. This is a lighter week designed to solidify your git skills through practice.

## 1. Learning Objectives

- Execute feature branch workflow (branch → commit → PR → review).
- Write meaningful commit messages tied to small scopes.
- Become comfortable with essential git commands through hands-on practice.
- Practice collaborative code review process.

## 2. Reading & Resources (30 min)

- **[Using Source Control in GitHub Codespaces](https://docs.github.com/en/codespaces/developing-in-a-codespace/using-source-control-in-your-codespace)** – Official guide for using the Codespaces Source Control UI (15 min).
- **[Git Documentation](https://git-scm.com/doc)** – Skim basics or commands you rarely use (15 min).

## 3. This Week's Work

- Complete the interactive Git tutorial: **[Learn Git Branching](https://learngitbranching.js.org/?locale=en_US)** (focus on Main section, Introduction Sequence).
- Practice the feature branch workflow with your journal entries.
- Open a PR and practice the code review process.

## 4. Files to Modify

- This file (`Course-Materials/Weekly-Modules/week-06-git-workflow.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Create branch `week-06-submission`.
2. Complete the Learn Git Branching tutorial (Main → Introduction Sequence: levels 1-8).
3. Add a note in the Journal section of this file (Section 9) reflecting on what you learned from the git tutorial.
4. Stage and commit your journal entry:
   ```bash
   git add Course-Materials/Weekly-Modules/week-06-git-workflow.md
   git commit -m "docs: complete Week 6 journal reflection"
   ```
5. Update `WEEKLY_PROGRESS.md` and commit:
   ```bash
   git add WEEKLY_PROGRESS.md
   git commit -m "chore: mark Week 6 complete"
   ```
6. Push your branch:
   ```bash
   git push origin week-06-submission
   ```
7. Open a Pull Request on GitHub from `week-06-submission` to `main`.
8. Review your own PR, looking for any issues or improvements.
9. If everything looks good, approve and merge your PR.

## 6. How to Test

No code testing required this week. Focus is on git workflow practice.

Verify your commits:
```bash
git log --oneline -5
```

Verify your branch is clean:
```bash
git status
```

## 7. Success Criteria

- Learn Git Branching tutorial completed (Introduction Sequence: levels 1-8).
- At least two separate commits with clear, meaningful commit messages following convention (type: description).
- PR created, reviewed, and merged successfully.
- Journal and discussion prep completed.
- Week 6 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-06-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 6 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Git Tutorial Reflection: What was the most challenging concept in the Learn Git Branching tutorial, and how did you work through it?*
- *Commit Strategy: What criteria did you use to decide when to make a commit? How did you determine the scope of each commit?*
- *Workflow Insights: How does the feature branch workflow (branch → commit → PR → merge) compare to how you've used version control in the past?*

### Discussion Prep:

- *What git commands or concepts do you still find confusing?*
- *How would you explain the difference between merge and rebase to a teammate?*
- *What challenges do you anticipate with git workflows in a team setting?*
- *How does good commit hygiene help with code review and debugging?*

## 10. Time Estimate

- 30 min – Reading.
- 45 min – Learn Git Branching tutorial.
- 15 min – Journal and discussion prep.
- 10 min – Commit workflow practice.
- 10 min – Create PR, review, and merge.

**Total:** ~1 hour 50 minutes.

## 11. Additional Resources

### Git Learning Resources

- **[Learn Git Branching](https://learngitbranching.js.org/?locale=en_US)** - Interactive tutorial (this week's main assignment).
- **[Pro Git Book](https://git-scm.com/book/en/v2)** - Comprehensive git reference.
- **[Dangit, Git!?!](https://dangitgit.com/en)** - Plain English git error recovery.
- **[Atlassian Git Tutorials](https://www.atlassian.com/git/tutorials)** - Branching and merging best practices.

### Video Resources

- **[Git & GitHub Tutorial for Beginners](https://www.youtube.com/watch?v=RGOj5yH7evk)** (1 hour) - Comprehensive intro.
- **[Tools & Concepts for Mastering Version Control with Git](https://www.youtube.com/watch?v=Uszj_k0DGsg)** (15 min) - Quick primer.

### Deep Dives (Optional)

- **[Git for Professionals by freeCodeCamp](https://www.freecodecamp.org/news/git-for-professionals/)** - Advanced techniques.
- **[Atlassian Git Tutorials](https://www.atlassian.com/git/tutorials)** - Topic-specific deep dives.

### Common Git Commands Reference

**Basic Workflow:**
```bash
git status                    # Check current state
git add <file>                # Stage changes
git commit -m "message"       # Commit changes
git push origin <branch>      # Push to remote
git pull origin <branch>      # Pull from remote
```

**Branching:**
```bash
git branch                    # List branches
git checkout -b <branch>      # Create and switch to branch
git checkout <branch>         # Switch to existing branch
git branch -d <branch>        # Delete branch
```

**Viewing History:**
```bash
git log                       # Full commit history
git log --oneline             # Condensed history
git log --oneline -5          # Last 5 commits
git diff                      # Show unstaged changes
git diff --staged             # Show staged changes
```

**Undoing Changes:**
```bash
git checkout -- <file>        # Discard unstaged changes
git reset HEAD <file>         # Unstage file
git commit --amend            # Modify last commit
```