# Week 20: Code Review & Collaboration

This week, we are focusing on the importance of code review in creating high-quality software, and how it aligns with In Time Tec's emphasis on technical excellence. We will also explore the relationship between code review and the Quality Manifesto's emphasis on clear technical documentation.

## 1. Learning Objectives

- Understand the importance of code review in creating high-quality software.
- Apply best practices for code review and feedback implementation.
- Perform professional peer reviews using provided checklist.
- Provide actionable, respectful feedback with suggested fixes.
- Respond to review comments on your own PR quickly and thoroughly.

## 2. Reading & Resources (40 min)

- **[Google Engineering Practices Documentation: How to Do a Code Review](https://google.github.io/eng-practices/review/)** (15 min) - Core playbook.
- **[Code Review Best Practices](https://www.kevinlondon.com/2015/05/05/code-review-best-practices.html)** (10 min) - Practical checklist.
- **[Code Review Guidelines for C# Programmers](https://www.codeproject.com/Articles/524235/CodeplusreviewplusguidelinesplusforplusC-23pluspr)** (10 min) - Language-specific watchpoints.
- **[30 Proven Code Review Best Practices from Microsoft](https://www.michaelagreiler.com/code-review-best-practices/)** (5 min) - Insights from large teams.

## 3. This Week's Work

- Review **two** sample PRs from `docs/sample-prs/` folder.
- Leave at least three high-quality comments per PR.
- If in cohort with classmates, review their PRs instead.

### Solo Learner Alternative

1. Review sample PRs in `docs/sample-prs/` folder.
2. Create `docs/my-week-20-review.md` with your feedback.
3. Self-review your own Week 17-19 PRs using checklist.

## 4. Files to Modify

- Code changes only if review feedback uncovers issues in your Week 19 branch.
- New: `docs/my-week-20-review.md` (for solo learners).
- This file (`Course-Materials/Weekly-Modules/week-20-code-review-collaboration.md`) — append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

### For Cohort Members (With Classmates)

1. Create branch `week-20-submission` (only for documentation changes).
2. Review two classmates' Week 19 PRs.
3. Focus comments on behavior ("This breaks filtering when priority is empty because...").
4. Provide at least one suggestion comment with code snippet.
5. On your PR, respond to every comment within 24 hours—commit fixes as needed.
6. Document review activity in journal (Section 9).

### For Solo Learners (No Classmates)

1. Create branch `week-20-submission`.
2. Review the **three** sample PRs in `docs/sample-prs/`:
   - `week-19-factory-pattern.md` - Good implementation with subtle issues.
   - `week-17-tdd-example.md` - Has 2-3 bugs to identify.
   - `week-12-ocp-filters.md` - Clean implementation to praise.
3. Create `docs/my-week-20-review.md` documenting your review comments (see Section 11 for template).
4. Use Section 12 "Comment Quality Examples" to structure your feedback.
5. Self-review your own Week 17-19 PRs using the checklist below.
6. Document findings in journal (Section 9).

### Self-Review Checklist

Review your own Week 17-19 PRs and answer:

**Week 17 (Testing):**
- [ ] Tests follow Arrange-Act-Assert pattern.
- [ ] Test names clearly describe scenarios.
- [ ] No skipped/ignored tests without reason.

**Week 18 (Refactoring):**
- [ ] All code smell comments removed.
- [ ] No new smells introduced.
- [ ] Tests still pass after refactoring.

**Week 19 (Factory):**
- [ ] Factory properly injected via DI.
- [ ] No direct entity construction in service.
- [ ] Factory handles all creation logic.

## 6. How to Test

```bash
# If you make code changes from review feedback
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

### For Cohort Members

- Two peer reviews completed with actionable comments.
- All comments on your PR resolved (either code change or explanation).
- Week 20 checkbox ticked in `WEEKLY_PROGRESS.md`.

### For Solo Learners

- Three sample PRs reviewed with detailed feedback in `docs/my-week-20-review.md`.
- At least three high-quality comments per sample PR (minimum 9 total).
- Self-review checklist completed for your Week 17-19 PRs.
- Week 20 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-20-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 20 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *Review Highlights: Summarize the most impactful comment you left on a peer's PR (or sample PR).*
- *Feedback Response: Reflect on the toughest comment received on your PR and how you resolved it (or toughest issue you identified in sample PRs).*
- *Professional Growth: How did practicing code review improve your understanding of clean code principles?*

### Discussion Prep:

- *How did you handle disagreement during review?*
- *Where did asynchronous communication break down, and how could you improve it?*
- *Share examples of the feedback you received and the changes you made to your project.*
- *Discuss the impact of your changes on code quality and maintainability.*

## 10. Time Estimate

- 40 min — Reading.
- 10 min — Prep and checklist review.
- 30 min — Review PR #1.
- 30 min — Review PR #2.
- 30 min — Review PR #3 (solo learners only).
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~2 hours 55 minutes (spread through the week).

## 11. Review Document Template

### For Solo Learners

Create: `docs/my-week-20-review.md`

```markdown
# Week 20 Code Review Practice

## Sample PR 1: week-19-factory-pattern.md

### Comment 1 (Critical - Missing Validation)
**Location:** TaskFactory.cs, line 11
**Severity:** Critical
**Issue:** The CreateNewTask method doesn't validate that request.Title is not null/empty. This will throw NullReferenceException when TaskEntity.Create is called.
**Suggested Fix:** 
```csharp
if (string.IsNullOrWhiteSpace(request.Title))
    throw new ArgumentException("Title cannot be empty", nameof(request));
```

### Comment 2 (Important - Missing DI Registration)
**Location:** Program.cs or ServiceCollectionExtensions.cs
**Severity:** Important
**Issue:** TaskFactory is not registered in DI container. Service will fail at runtime.
**Suggested Fix:** Add `services.AddScoped<TaskFactory>();` to service registration.

### Comment 3 (Praise - Clear Separation)
**Location:** TaskService.cs, line 24
**Type:** Praise
**Comment:** Excellent separation of concerns! Moving creation logic out of the service into the factory makes this much more testable and follows SRP perfectly.

## Sample PR 2: week-17-tdd-example.md

### Comment 1 (Critical - Missing Await)
**Location:** TaskService.cs, line 45
**Severity:** Critical
**Issue:** Missing `await` keyword causes compilation error. You're assigning `Task<TaskEntity>` to `TaskEntity`.
**Suggested Fix:** Change to `var task = await _repository.GetByIdAsync(id);`

[Continue with remaining comments...]

## Sample PR 3: week-12-ocp-filters.md

[Repeat structure...]

## Self-Review Findings

### My Week 17 PR
- **Issue Found:** Tests don't use FakeSystemClock consistently - some tests have flaky time assertions.
- **Action Taken:** Updated all tests to inject FakeSystemClock.

### My Week 18 PR
- **Issue Found:** Removed one code smell comment but didn't actually fix the smell.
- **Action Taken:** Went back and properly refactored the duplicate code.

### My Week 19 PR
- **Improvement Opportunity:** Factory could be made static since it doesn't maintain state.
- **Decision:** Keeping as instance class for easier testing and future extensibility.
```

## 12. Comment Quality Examples

### High-Quality Comment Examples

**❌ BAD:** "Fix this"  
**✅ GOOD:** "This filtering breaks when priority list is empty because line 42 assumes Count > 0. Suggest adding: `if (priorities.Count == 0) return true;`"

**❌ BAD:** "Wrong"  
**✅ GOOD:** "The validation should happen before database call (line 15) to avoid unnecessary query. Consider moving validation to top of method."

**❌ BAD:** "Nice job"  
**✅ GOOD:** "Excellent use of the Factory pattern here! This separation makes it easy to add context-aware defaults in the future (like setting default priority based on user role)."

**❌ BAD:** "This won't work"  
**✅ GOOD:** "Missing `await` keyword on line 23. This will cause a compilation error because you're assigning `Task<TaskEntity>` to `TaskEntity`. Add: `var task = await _repository.GetByIdAsync(id);`"

### What Makes a Comment High-Quality?

1. **Specific Location**: Reference exact file, line number, or method name.
2. **Explains Impact**: Why is this a problem? What breaks?
3. **Actionable**: Provides concrete fix or alternative approach.
4. **Respectful**: Assumes good intent, suggests rather than demands.
5. **Educational**: Helps reviewer learn, not just fix.

### Comment Types

- **Critical**: Bugs, security issues, breaks functionality.
- **Important**: Design flaws, performance issues, violates principles.
- **Suggestion**: Improvements, better patterns, readability.
- **Nit**: Formatting, typos, minor style issues.
- **Praise**: Call out good practices, clever solutions, improvements.

## 13. Additional Resources

### Examples

- **Sample PRs** - Located in `docs/sample-prs/` folder.

### External Resources

- **[How to Review Code Effectively](https://github.com/google/eng-practices/blob/master/review/reviewer/)** - Google's reviewer guide.
- **[Giving Better Code Reviews](https://mtlynch.io/human-code-reviews-1/)** - Human-focused approach.
- **[Code Review Checklist](https://www.evoketechnologies.com/blog/code-review-checklist-perform-effective-code-reviews/)** - Comprehensive checklist.

### Optional Deep Dives

- **[YouTube: How to Do Code Reviews Like a Human](https://www.youtube.com/watch?v=0t4_MfHgb_A)** (10 min) - Human-first approach.
- **[The Art of Code Review](https://www.alexanderlogan.co.uk/blog/2020/the-art-of-code-review)** - Advanced techniques.