# Week 20: Code Review & Collaboration
This week, we are focusing on the importance of code review in creating high-quality software, and how it aligns with In Time Tec's emphasis on technical excellence. We will also explore the relationship between code review and the Quality Manifesto's emphasis on clear technical documentation.

## 1. Learning Objectives
- Understand the importance of code review in creating high-quality software
- Apply best practices for code review and feedback implementation
- Perform professional peer reviews using provided checklist.
- Provide actionable, respectful feedback with suggested fixes.
- Respond to review comments on your own PR quickly and thoroughly.
- Discuss the relationship between code review and the Quality Manifesto's emphasis on clear technical documentation

## 2. Reading & Resources (40 min)
- **[Google Engineering Practices Documentation: How to Do a Code Review](https://google.github.io/eng-practices/review/)** – Core playbook.
- **[Code Review Best Practices](https://www.kevinlondon.com/2015/05/05/code-review-best-practices.html)** – Practical checklist.
- **[Code Review Guidelines for C# Programmers](https://www.codeproject.com/Articles/524235/CodeplusreviewplusguidelinesplusforplusC-23pluspr)** – Language-specific watchpoints.
- **[30 Proven Code Review Best Practices from Microsoft](https://www.michaelagreiler.com/code-review-best-practices/)** – Insights from large teams.
- **[Youtube: How to Do Code Reviews Like a Human](https://www.youtube.com/watch?v=0t4_MfHgb_A)** (optional, 10 min) – Human-first approach.

## 3. This Week's Work
- Review **two** sample PRs from `docs/sample-prs/` folder
- Leave at least three high-quality comments per PR
- If in cohort with classmates, review their PRs instead

### Solo Learner Alternative:
1. Review sample PRs in `docs/sample-prs/` folder
2. Create `docs/my-week-20-review.md` with your feedback
3. Self-review your own Week 17-19 PRs using checklist

## 4. Files to Modify

- Code changes only if review feedback uncovers issues in your Week 19 branch.
- `docs/my-week-20-review.md` (new, for solo learners)
- This file (`Course-Materials/Weekly-Modules/week-20-code-review-collaboration.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### For Cohort Members (With Classmates):
1. Branch `week-20/<your-name>` (only for documentation changes).
2. Review two classmates' Week 19 PRs.
3. Focus comments on behavior ("This breaks filtering when priority is empty because…").
4. Provide at least one suggestion comment (`suggestion` block or code snippet).
5. On your PR, respond to every comment within 24 hours—commit fixes as needed.
6. Capture review activity in the Review Log below.

### For Solo Learners (No Classmates):
1. Branch `week-20/<your-name>`.
2. Review the **three** sample PRs in `docs/sample-prs/`:
   - `week-19-factory-pattern.md` - Good implementation with subtle issues
   - `week-17-tdd-example.md` - Has 2-3 bugs to identify
   - `week-12-ocp-filters.md` - Clean implementation to praise
3. Create `docs/my-week-20-review.md` documenting your review comments (see template below).
4. Use the **Comment Quality Examples** section below to structure your feedback.
5. Self-review your own Week 17-19 PRs using the checklist in section 9.

### Sample Review Document Template (`docs/my-week-20-review.md`):

```markdown
# Week 20 Code Review Practice

## Sample PR 1: week-19-factory-pattern.md

### Comment 1 (Issue - Missing Validation):
**Location:** TaskFactory.cs, line 11
**Severity:** Medium
**Issue:** The CreateNewTask method doesn't validate that request.Title is not null/empty...
**Suggested Fix:** Add validation at the start of the method...

### Comment 2 (Issue - Missing DI):
[Your comment here]

### Comment 3 (Praise):
[Your positive feedback here]

## Sample PR 2: week-17-tdd-example.md

[Repeat structure]

## Sample PR 3: week-12-ocp-filters.md

[Repeat structure]

## Self-Review Checklist

### My Week 17 PR:
- [ ] Tests follow Arrange-Act-Assert pattern
- [ ] Test names clearly describe scenarios
- [ ] No skipped/ignored tests without reason
- [Your notes...]

### My Week 19 PR:
- [ ] Factory properly injected via DI
- [ ] No direct entity construction in service
- [Your notes...]
```

## 6. How to Test
- Run tests (`dotnet test TaskFlowAPI.sln`) if you make code changes from review feedback.

## 7. Success Criteria

### For Cohort Members:
- Two peer reviews completed with actionable comments.
- All comments on your PR resolved (either code change or explanation).

### For Solo Learners:
- Three sample PRs reviewed with detailed feedback in `docs/my-week-20-review.md`.
- At least three high-quality comments per sample PR (minimum 9 total).
- Self-review checklist completed for your Week 17-19 PRs.

## 8. Submission Process

1.  Create a new branch for your weekly work (e.g., `git checkout -b week-20-submission`).
2.  Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 20 work"`).
3.  Push the branch to your forked repository on GitHub.
4.  On GitHub, create a Pull Request from your new branch to your `main` branch.
5.  Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Review Highlights:* Summarize the most impactful comment you left on a peer's PR (or sample PR).

*Feedback Response:* Reflect on the toughest comment received on your PR and how you resolved it (or toughest issue you identified in sample PRs).

Discussion Prep:
- How did you handle disagreement during review?
- Where did asynchronous communication break down, and how could you improve it?
- Share examples of the feedback you received during your code review and the changes you made to your project.
- Discuss the impact of your changes on code quality and maintainability.
- Share challenges you faced while implementing the feedback and how you overcame them.
- Discuss the relationship between code review and the Quality Manifesto's emphasis on clear technical documentation.

## 10. Time Estimate
- 40 min – Reading.
- 10 min – Prep + checklist review.
- 30 min – Review PR #1.
- 30 min – Review PR #2.
- 30 min – Review PR #3 (solo learners).
- 20 min - Journal + discussion prep.
- 15 min – Test => PR => Review => Merge.
**Total:** ~120-150 minutes (spread through the week).

## 11. Comment Quality Examples

### High-Quality Comment Examples:

**BAD:** "Fix this"  
**GOOD:** "This filtering breaks when priority list is empty because line 42 assumes Count > 0. Suggest adding: `if (priorities.Count == 0) return true;`"

**BAD:** "Wrong"  
**GOOD:** "The validation should happen before database call (line 15) to avoid unnecessary query. Consider moving validation to top of method."

**BAD:** "Nice job"  
**GOOD:** "Excellent use of the Factory pattern here! This separation makes it easy to add context-aware defaults in the future (like setting default priority based on user role)."

**BAD:** "This won't work"  
**GOOD:** "Missing `await` keyword on line 23. This will cause a compilation error because you're assigning `Task<TaskEntity>` to `TaskEntity`. Add: `var task = await _reader.GetByIdAsync(id);`"

### What Makes a Comment High-Quality?

1. **Specific Location**: Reference exact file, line number, or method name
2. **Explains Impact**: Why is this a problem? What breaks?
3. **Actionable**: Provides concrete fix or alternative approach
4. **Respectful**: Assumes good intent, suggests rather than demands
5. **Educational**: Helps reviewer learn, not just fix

### Comment Types:

- **Critical**: Bugs, security issues, breaks functionality
- **Important**: Design flaws, performance issues, violates principles
- **Suggestion**: Improvements, better patterns, readability
- **Nit**: Formatting, typos, minor style issues
- **Praise**: Call out good practices, clever solutions, improvements

## 12. Additional Resources
