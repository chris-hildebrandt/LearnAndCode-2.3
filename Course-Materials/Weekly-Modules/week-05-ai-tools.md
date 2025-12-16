# Week 5: AI Tools & Prompt Engineering

This week, we are focusing on how developers can leverage AI tools and prompt engineering techniques to enhance their research and development processes. We will explore core concepts, best practices, and practical applications of AI in software development.

## 1. Learning Objectives

- Understand the strengths and limitations of Large Language Models and Agentic AI in day-to-day engineering.
- Craft effective prompts that accelerate work.
- Understand the ethical and legal problems associated with AI use in the professional setting, and how these relate to the Quality Manifesto and ITT values.
- Critically evaluate AI-generated code quality through hands-on comparison.

## 2. Reading & Resources (45 min)

- **`learn-and-code-ai-use-rules.md`** - Our AI use policy for this course (15 min).

### Understanding LLMs (15 min)

Read at least one of the following:
- **[What are Large Language Models? | A Comprehensive LLMs Guide](https://www.elastic.co/what-is/large-language-models)**
- **[How do large language models work?](https://aws.amazon.com/what-is/large-language-model/)**

### Prompt Engineering (15 min)

Read at least one of the following:
- **[Prompt Engineering Techniques](https://www.promptingguide.ai/)**
- **[Google AI: Prompt Design Best Practices](https://ai.google/static/documents/prompt-design-best-practices.pdf)**
- **[Anthropic's Prompt Engineering Tutorial](https://www.anthropic.com/index/a-guide-to-writing-prompts-for-text-generation-ai)**

## 3. This Week's Work

- Create a test branch from your Week 3 work (before Week 4 changes) for AI assignment.
- Use an AI agent to complete Week 4 assignments on the test branch (refactor `GenerateProjectSummaryReport`, add UPDATE/DELETE endpoints).
- Complete the AI Tools Assignment by filling out `Course-Materials/Examples/Week-05-AI-Assignment.md` as you work.
- Copy completed assignment file to your submission branch.
- Optionally bring AI-generated improvements to your submission branch with proper attribution.

## 4. Files to Modify

**AI changes should be done on the `week-05-ai-test` branch (created from `week-03-submission`)!**

**Files to modify on your `week-05-submission` branch:**
- `Course-Materials/Examples/Week-05-AI-Assignment.md` (copy completed assignment here).
- This file (`Course-Materials/Weekly-Modules/week-05-ai-tools.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`
- (Optional) Any files modified by agentic AI that you would like to bring over to enhance your manual work from last week.

## 5. Step-by-Step Instructions

**Important:** Complete the AI assignment (steps 4-6) BEFORE making any notes in the Journal section (Section 9) of this file. You'll be switching branches during the AI work, and any uncommitted changes to this weekly file may be lost.

1. Create branch `week-05-submission`.
2. Review `learn-and-code-ai-use-rules.md` to understand AI use policy.
3. Complete all reading resources (Section 2).
4. Create AI test branch from Week 3: `git checkout week-03-submission` then `git checkout -b week-05-ai-test`.
5. Open `Course-Materials/Examples/Week-05-AI-Assignment.md` and follow the instructions:
   - Part 1: AI Practical Assessment (40 min) - Fill out answers as you work on the AI test branch.
   - Part 2: AI Ethics Journal (20 min) - Complete all three scenarios.
   - Fill out the assignment file directly on the `week-05-ai-test` branch.
6. Test AI-generated code on your `week-05-ai-test` branch.
7. Switch back to `week-05-submission` branch:
   ```bash
   git checkout week-05-submission
   ```
8. Copy your completed `Week-05-AI-Assignment.md` from the AI test branch with this command:
   ```bash
   git checkout week-05-ai-test -- Course-Materials/Examples/Week-05-AI-Assignment.md
   ```
9. If desired, bring select AI improvements to your `week-05-submission` branch with clear attribution comments.
10. Complete Journal section (Section 9) in this file.
11. Update `WEEKLY_PROGRESS.md`.

## 6. How to Test

Test AI-generated changes on the AI test branch:
```bash
# On week-05-ai-test branch (branched from week-03-submission)
git checkout week-05-ai-test
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
dotnet run --project TaskFlowAPI
```

If you bring any changes to your submission branch:
```bash
# Switch to submission branch
git checkout week-05-submission
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

## 7. Success Criteria

- Created `week-05-ai-test` branch from `week-03-submission` (before Week 4 changes).
- Completed Week 4 assignments using AI assistance on the AI test branch.
- `Course-Materials/Examples/Week-05-AI-Assignment.md` completed with:
  - Code quality analysis and comparison.
  - All three ethics scenarios answered.
  - Personal AI use principles documented.
- Journal and discussion prep completed in this file.
- (Optional) AI-generated improvements integrated with proper attribution.
- Week 5 checkbox ticked in `WEEKLY_PROGRESS.md`.

## 8. Submission Process

> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-05-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week 5 work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:

- *AI Tool Selection: Which AI tool did you choose and why? How did its strengths and limitations compare to your expectations?*
- *Human Oversight: Describe a specific example where AI output was misleading or incorrect. How did you detect it, and what did this teach you about verification?*
- *Core Values: How should ITT values and the Quality Manifesto guide our use of AI tools with partner code? What specific principles apply?*
- *Professional Responsibility: After completing the ethics scenarios, what is the most important lesson you learned about professional responsibility when using AI?*

### Discussion Prep:

- *Share examples of how AI tools enhanced your development process and any unexpected challenges you faced.*
- *Discuss the most effective prompts you created for development tasks and explain your reasoning.*
- *Compare the performance and usefulness of different AI-powered developer tools you experimented with.*
- *Explore the ethical considerations and potential biases in AI-assisted development.*
- *Brainstorm innovative ways to integrate AI tools into your team's development workflow.*
- *What signals tell you to stop iterating with AI and switch to manual investigation?*
- *How will you use AI responsibly in today's highly demanding work environment?*

## 10. Time Estimate

- 45 min – Reading resources.
- 40 min – AI Practical Assessment (see assignment document).
- 20 min – AI Ethics Journal (see assignment document).
- 20 min – Journal and discussion prep.
- 15 min – Test, create PR, and merge.

**Total:** ~2 hours 20 minutes.

## 11. Additional Resources

### ITT Resources

- **[ITT Building with Agents Channel](https://teams.microsoft.com/l/chat/19:meeting_ODU4MDBkOWMtNWRjZi00ZDE1LWFmMzktYTRlZjM0ZDMxMDBk@thread.v2/conversations?context=%7B%22contextType%22%3A%22chat%22%7D)**
- **[Week 5 AI Assignment](../Examples/Week-05-AI-Assignment.md)** - Detailed instructions for practical assessment and ethics journal.

### AI Tools for Developers

- **[GitHub Copilot](https://github.com/features/copilot)** - AI pair programmer for code suggestions.
- **[Cursor](https://www.cursor.com/)** - AI IDE built on VS Code.
- **[Perplexity AI](https://www.perplexity.ai/)** - AI-powered search engine for quick, accurate research.
- **[Pieces for Developers](https://pieces.app/)** - AI-powered code snippet manager and collaborator.
- **[MarsCode](https://www.marscode.com/)** - AI IDE.
- **[PearAI](https://trypear.ai/)** - AI IDE.

### Video Resources

- **[Introduction to Prompt Engineering](https://www.youtube.com/watch?v=dOxUroR57xs)** (1 hour).
- **[ITT Tech Forum with Brad Frazier on the Legal Implications of AI](https://drive.google.com/file/d/1XTgfkLuSGeU3LP4hgDL7u_HM3gbmRI1L/view?usp=sharing)** (1 hour).

### Optional Deep Dives

- **[AI Safety Course Information and Signup](https://www.alignmentforum.org/posts/Zmwkz2BMvuFFR8bi3/agi-safety-fundamentals-curriculum-and-application)** - Third party educational course.
- **[Prompt Engineering Guide](https://www.promptingguide.ai/)** - Comprehensive guide to effective prompting (massive and immersive guide).
- **[AI Code Ethics Discussion](https://www.youtube.com/results?search_query=ai+code+ethics)** - Various perspectives on ethical AI use (YouTube subject search).