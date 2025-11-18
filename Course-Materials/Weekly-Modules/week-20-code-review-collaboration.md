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

## 3. This Week’s Work
- Review **two** classmates’ Week 19 PRs (or designated practice PRs).
- Leave at least three high-quality comments per PR (nit/praise/question/breakage).
- Respond to all comments on your own Week 19 PR and make necessary updates.

## 4. Files to Modify

- Code changes only if review feedback uncovers issues in your Week 19 branch.
- This file (`Course-Materials/Weekly-Modules/week-20-code-review-collaboration.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions
1. Branch `week-20/<your-name>` (only for documentation changes).
2. Focus comments on behaviour (“This breaks filtering when priority is empty because…”).
3. Provide at least one suggestion comment (`suggestion` block or code snippet).
4. On your PR, respond to every comment within 24 hours—commit fixes as needed.
5. Capture review activity in the Review Log below.
6. If no classmates are available, ask mentored staff for the current sample PR to review.

## 6. How to Test
- Run tests (`dotnet test TaskFlowAPI.sln`) if you make code changes from review feedback.

## 7. Success Criteria
- Two peer reviews completed with actionable comments.
- All comments on your PR resolved (either code change or explanation).

## 8. Submission Process
- Commit documentation update `Week 20 – review log`.
- PR summary includes links to reviews you performed and confirmation your PR comments resolved.
- Weekly issue attaches screenshot of one review comment thread.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Review Highlights:* Summarise the most impactful comment you left on a peer’s PR.

*Feedback Response:* Reflect on the toughest comment received on your PR and how you resolved it.

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
- 10 min – Respond to comments on your PR + documentation.
**Total:** ~120 minutes (spread through the week).

## 11. Additional Resources
