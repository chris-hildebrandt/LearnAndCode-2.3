# Week 5: AI Tools & Prompt Engineering

This week, we are focusing on how developers can leverage AI tools and prompt engineering techniques to enhance their research and development processes. We will explore core concepts, best practices, and practical applications of AI in software development.

## 1. Learning Objectives

- Understand the strengths and limitations of Large Language Models in day-to-day engineering.
- Craft effective prompts that accelerate work.
- Understand the ethical and legal problems associated with AI use in the professional setting, and how these relate to the Quality Manifesto and ITT values.

## 2. Reading & Resources (60-120 min)

- **`learn-and-code-ai-use-rules.md`** - Our AI use policy for this course.

### AI Safety and Ethics:
- **[AI Safety Considerations](https://www.alignmentforum.org/posts/HdWKEWfifaEcXAj4j/ai-safety-fundamentals-curriculum)**

### Understanding LLMs:
(read at least one of the following:)
- **[What are Large Language Models? | A Comprehensive LLMs Guide](https://www.elastic.co/what-is/large-language-models)**
- **[How do large language models work?](https://aws.amazon.com/what-is/large-language-model/)**

### Prompt Engineering:
(read at least one of the following:)
- **[Prompt Engineering Techniques](https://www.promptingguide.ai/)**
- **[Google AI: Prompt Design Best Practices](https://ai.google/static/documents/prompt-design-best-practices.pdf)**
- **[Anthropic's Prompt Engineering Tutorial](https://www.anthropic.com/index/a-guide-to-writing-prompts-for-text-generation-ai)**

## 3. This Week’s Work

**REVISED:** Structured AI experimentation with concrete TaskFlowAPI challenges

- **Part A:** Use AI to identify code smells in TaskFlowAPI (30 min)
- **Part B:** Compare AI-generated refactoring suggestions to your Week 4 solution (30 min)
- **Part C:** Critical analysis of AI-generated code quality (30 min)
- **Part D:** AI Ethics in Practice scenarios (20 min)

## 4. Files to Modify

- `docs/week-05-ai-comparison.md` (NEW - AI tool comparison and analysis)
- This file (`Course-Materials/Weekly-Modules/week-05-ai-tools.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-05/<your-name>`.
2. Review `learn-and-code-ai-use-rules.md` to understand AI use policy.
3. **NEW:** Complete AI-Assisted Refactoring Challenge (see Section 11 below) - 90 minutes
4. **NEW:** Complete AI Ethics in Practice scenarios (see Section 12 below) - 20 minutes
5. Summarise outcomes in the Journal section of this doc.
6. Summarise outcomes in the Journal section of this doc (add a new bullet list under “Learner Notes”).

## 6. How to Test

No testing this week.

## 7. Success Criteria

- ✅ Tested ≥ 3 AI tools on same code smell identification task
- ✅ Documented at least ONE hallucination or error from AI
- ✅ Identified at least ONE scenario where AI explanation was superior to documentation
- ✅ Critical analysis of AI-generated code quality completed
- ✅ All three ethics scenarios answered with your position
- ✅ `docs/week-05-ai-comparison.md` completed with all sections
- ✅ Journal and Discussion prep completed

## 8. Submission Process

1. Commit `Week 05 – AI prompt engineering`.
2. PR summary links to journal bullets and attaches screenshots/logs as needed.
3. Weekly issue includes a brief description of the AI tool(s) evaluated.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
(add responses under **Learner Notes** at the end of this file):
- *Prompt Evolution: Which prompt iteration delivered the best balance of accuracy and brevity?*
- *Human Oversight: Describe a time AI output was misleading. How did you detect and correct it?*
- *Core values: How do you believe ITT values and the Quality Manifesto should reflect in our use of AI tools with partner code?*

### Discussion Prep:
- *Share examples of how AI tools enhanced your development process and any unexpected challenges you faced.*
- *Discuss the most effective prompts you created for development tasks and explain your reasoning.*
- *Compare the performance and usefulness of different AI-powered developer tools you experimented with.*
- *Explore the ethical considerations and potential biases in AI-assisted development.*
- *Brainstorm innovative ways to integrate AI tools into your team's development workflow.*
- *What signals tell you to stop iterating with AI and switch to manual investigation?*
- *How will you use AI responsibly in today's highly demanding work environment?*

## 10. Time Estimate

- 60 min – Reading + tool setup (unchanged).
- **30 min – Part A: AI Code Smell Identification (NEW)**
- **30 min – Part B: Compare AI Refactoring to Your Solution (NEW)**
- **30 min – Part C: Critical Analysis of AI-Generated Code (NEW)**
- **20 min – Part D: AI Ethics Scenarios (NEW)**
- 15 min – PR/issue + sharing artefacts.
**Total:** ~3 hours 5 minutes (increased, but structured and concrete).

## 11. AI-Assisted Refactoring Challenge (NEW - Structured Experimentation)

**Goal:** Experience AI strengths AND weaknesses through hands-on TaskFlowAPI analysis

**Time:** 90 minutes

### Part A: Test AI Understanding (30 minutes)

**Task:** Use AI to identify code smells

1. **Locate the "before" code:**
   - Open your **Week 4 branch** (before refactoring)
   - Copy the original 100+ line `GenerateProjectSummaryReport` method from `ReportsController.cs`

2. **Select 3 AI tools** from this list:
   - GitHub Copilot Chat
   - Claude (claude.ai)
   - ChatGPT (chat.openai.com)
   - Cursor Chat
   - Perplexity AI
   - Any other LLM-based coding assistant

3. **Use this prompt for ALL 3 tools:**
   ```
   This C# method violates Clean Code principles. 
   Identify 3-5 specific code smells and explain why each is problematic.
   Include the principle violated (e.g., SRP, DRY, magic numbers).

   [paste your 100-line GenerateProjectSummaryReport method here]
   ```

4. **Create `docs/week-05-ai-comparison.md` and document:**

```markdown
# Week 5: AI Tool Comparison Analysis

## Part A: Code Smell Identification

### Tool 1: [Name]
**Smells Identified:**
1. [smell name] - [explanation]
2. [smell name] - [explanation]
...

**Quality Assessment:**
- Clear explanations? (Yes/No)
- Identified principle violated? (Yes/No)
- Missed any obvious issues? (List them)
- Hallucinated non-existent problems? (List them)

### Tool 2: [Name]
[Same format]

### Tool 3: [Name]
[Same format]

### Comparison Summary

| Aspect | Tool 1 | Tool 2 | Tool 3 | Winner |
|--------|--------|--------|--------|--------|
| Number of smells found | ___ | ___ | ___ | ___ |
| Accuracy (correct smells) | ___% | ___% | ___% | ___ |
| Explanation quality (1-5) | ___ | ___ | ___ | ___ |
| Referenced Clean Code principles? | Yes/No | Yes/No | Yes/No | ___ |
| Hallucinations found | ___ | ___ | ___ | Lowest wins |

**Best Tool for Code Smell Detection:** ___________  
**Why:** [50 words]
```

---

### Part B: Test AI Code Generation (30 minutes)

**Task:** Compare AI refactoring to your Week 4 solution

5. **Prompt ALL 3 tools with:**
   ```
   Refactor the GenerateProjectSummaryReport method by extracting helper methods.
   Show ONLY the method signatures (not full implementations).
   List the extracted method names and their parameters.

   [paste same 100-line method]
   ```

6. **Open your Week 4 refactored code** side-by-side

7. **Add to your `week-05-ai-comparison.md`:**

```markdown
## Part B: Refactoring Comparison

### Your Week 4 Solution
**Methods you extracted:**
1. `MethodName(params)` - [what it does]
2. `MethodName(params)` - [what it does]
...

**Total methods extracted:** ___

### Tool 1 Suggestions
**Methods AI suggested:**
1. `MethodName(params)` - [what it does]
2. `MethodName(params)` - [what it does]
...

**Comparison:**
- Methods in common: [list]
- AI extracted but you didn't: [list] - Why did AI extract these?
- You extracted but AI didn't: [list] - Why did you extract these?
- **Name quality:** AI names clearer (Yes/No) - Examples: ___

### Tool 2 Suggestions
[Same format]

### Tool 3 Suggestions
[Same format]

### Analysis Questions

1. **Boundary Decisions:**
   Did AI extract same logical boundaries as you, or different chunking?
   - Your answer: [100 words]

2. **Naming Comparison:**
   Whose names are more intention-revealing: yours or AI's?
   - Example where AI was better: ___
   - Example where you were better: ___

3. **Surprising Extraction:**
   Did AI suggest extracting something you didn't think of? Was it a good idea?
   - Your answer: [50 words]
```

---

### Part C: Critical Thinking - Don't Trust, Verify (30 minutes)

**Task:** Analyze AI-generated implementation quality

8. **Pick ONE extracted method** from your Week 4 refactor (e.g., `FilterTasksByProjectId`)

9. **Prompt ONE tool:**
   ```
   Generate complete C# implementation of this method:

   private List<TaskDto> FilterTasksByProjectId(List<TaskDto> tasks, int projectId)
   {
       // TODO: implementation
   }

   Requirements:
   - Filter tasks where ProjectId matches parameter
   - Handle null input
   - Handle empty list
   ```

10. **DON'T COPY IT YET!** First analyze on paper:

**Add to `week-05-ai-comparison.md`:**

```markdown
## Part C: Implementation Quality Analysis

### Generated Code
**Tool used:** ___________

```csharp
[paste AI-generated implementation here]
```

### Static Analysis (Before Running)

**Syntax Check:**
- Does it compile? (Yes/No)
- Any missing using statements? (List them)
- Type mismatches? (List them)

**Logic Review:**
- Handles null `tasks` parameter? (Yes/No/Doesn't check)
- Handles empty list? (Yes/No/Unnecessary check)
- Handles projectId <= 0? (Yes/No/Doesn't check)
- Returns correct type? (Yes/No)
- Uses LINQ or loops? (Which one?)

**Code Quality:**
- Method name matches behavior? (Yes/No)
- Follows C# naming conventions? (Yes/No)
- Efficient approach? (Yes/No) - Why/why not: ___

### Dynamic Testing (After Running)

11. **Now copy into a test branch:**
   ```bash
   git checkout -b week-05-ai-test
   # Add method to ReportsController.cs
   dotnet build
   ```

**Build Result:**
- ✅ Compiles without errors
- ⚠️ Compiles with warnings: [list warnings]
- ❌ Fails to compile: [error message]

12. **If it compiles, test it:**
   - Manual test via Swagger with projectId=1
   - Does it return expected results? (Yes/No)
   - Any runtime errors? (List them)

**Final Verdict:**
- [ ] Ready to use as-is
- [ ] Needs minor tweaks: [list changes needed]
- [ ] Completely broken: [explain why]

### Lessons Learned

**When should I trust AI-generated code?** (Based on YOUR experiment)

[Write 100-150 words reflecting on:
- What types of code did AI handle well?
- What types of code did AI struggle with?
- What checks should you ALWAYS do before using AI code?
- How does this relate to professional responsibility (Week 1)?]
```

---

### Deliverable for Part A-C

Completed `docs/week-05-ai-comparison.md` with:
- ✅ 3 tools tested on same smell identification task
- ✅ At least ONE hallucination or error documented
- ✅ Refactoring comparison (AI vs your Week 4 solution)
- ✅ Implementation quality analysis with build/test results
- ✅ "When to trust AI code" reflection

**Why This Matters:**
- Hands-on with YOUR actual code (not abstract examples)
- Experience AI failures directly (hallucinations, logic errors)
- Builds healthy skepticism ("trust but verify")
- Comparison teaches evaluation skills
- Prepares you for real workplace AI use

---

## 12. AI Ethics in Practice (NEW - Professional Responsibility)

**Goal:** Articulate YOUR ethical stance on AI use in professional development

**Time:** 20 minutes

### Scenario 1: AI-Generated Code in PRs

**Situation:**
You used GitHub Copilot to generate a `CalculateCompletionPercentage` method for Week 4. It works perfectly. The PR is ready to merge.

**Questions:**

1. **Disclosure:** Should you disclose to your team that AI wrote this code? Why or why not?

2. **Responsibility:** If the code has a subtle bug discovered 6 months later (e.g., division by zero when no tasks exist), who is responsible?
   - [ ] You (the developer who merged it)
   - [ ] GitHub (the AI provider)
   - [ ] The team (for not catching it in code review)
   - [ ] Shared responsibility - explain: ___

3. **Professional Context:** If this was partner code (client paying $200/hour for your expertise), does your answer change?

**Your Position (100 words):**
_____________

---

### Scenario 2: AI-Assisted Learning

**Situation:**
It's Week 8. Your `TaskRepository.GetByIdAsync` implementation returns an error you don't understand:
```
System.InvalidOperationException: Sequence contains no matching element
```

You ask AI: "Why am I getting this error?" AI immediately shows you the fix: change `Single()` to `SingleOrDefault()`.

**Questions:**

1. **Learning vs Copying:** If you copy the fix without understanding WHY, did you actually learn anything?

2. **The Line:** What's the difference between "AI helping you learn" vs. "AI doing your homework"?
   - Acceptable: ___
   - Unacceptable: ___

3. **Your Policy:** For this Learn & Code course, where's YOUR personal line?
   - I will use AI for: ___
   - I will NOT use AI for: ___
   - I will disclose AI usage when: ___

**Your Policy for This Course (50-75 words):**
_____________

---

### Scenario 3: Intellectual Property & Training Data

**Situation:**
GitHub Copilot was trained on millions of public GitHub repositories, including some with restrictive licenses. When you ask it to generate a "task priority algorithm," it produces code that looks suspiciously similar to code you've seen in an open-source project.

**Questions:**

1. **Originality:** Is AI-generated code "original" if the AI was trained on existing code?
   - Your stance: ___

2. **Ownership:** If AI produces code similar to someone's copyrighted work (but you didn't know), who owns the code you ship?
   - [ ] You (you wrote the prompt)
   - [ ] The original author (AI copied their pattern)
   - [ ] The AI company (their model generated it)
   - [ ] Public domain (can't prove direct copying)

3. **Partner Risk:** Should you use AI in partner codebases? What risks exist?
   - Legal risks: ___
   - Reputation risks: ___
   - Mitigation strategies: ___

**Your Answer (100 words):**
_____________

---

### Deliverable for Ethics Scenarios

**Add to end of `docs/week-05-ai-comparison.md`:**

```markdown
## Part D: AI Ethics in Practice

### Scenario 1: AI-Generated Code in PRs
**Your position:**
[Your 100-word answer]

**Key principle:** [One sentence - what ITT Quality Manifesto value applies here?]

### Scenario 2: AI-Assisted Learning
**Your policy for this course:**
[Your 50-75 word answer]

**Key principle:** [How does this align with "professional responsibility" from Week 1?]

### Scenario 3: Intellectual Property
**Your answer:**
[Your 100-word answer]

**Key principle:** [What does "Customer-Centric Design" mean in context of AI-generated code?]

### Reflection: My AI Use Principles

Based on these scenarios, I commit to:

1. **Disclosure:** I will disclose AI usage when ___
2. **Verification:** I will always verify AI code by ___
3. **Learning:** I will use AI to enhance (not replace) learning by ___
4. **Professional Integrity:** In partner projects, I will ___

**Signature:** [Your name], Week 5, [Date]
```

**Why This Matters:**
- Makes abstract ethics CONCRETE (you have to take a stance)
- Prepares you for real workplace scenarios (clients WILL ask about AI)
- Aligns with ITT professional responsibility values
- Forces you to articulate YOUR boundaries (not just follow rules)
- Creates accountability (you've stated your principles in writing)

---

## 13. Additional Resources

- **[ITT Building with Agents Channel](https://teams.microsoft.com/l/chat/19:meeting_ODU4MDBkOWMtNWRjZi00ZDE1LWFmMzktYTRlZjM0ZDMxMDBk@thread.v2/conversations?context=%7B%22contextType%22%3A%22chat%22%7D)**

### AI Tools for Developers:
- **[Perplexity AI](https://www.perplexity.ai/)** - An AI-powered search engine for quick, accurate research.
- **[GitHub Copilot](https://github.com/features/copilot)** - AI pair programmer for code suggestions.
- **[Cursor](https://www.cursor.com/)** - AI IDE built on VS Code.
- **[Pieces for Developers](https://pieces.app/)** - AI-powered code snippet manager and collaborator.
- **[MarsCode](https://www.marscode.com/)** - AI IDE.
- **[PearAI](https://trypear.ai/)** - AI IDE.

### Video Resources:
- **[Introduction to Prompt Engineering](https://www.youtube.com/watch?v=dOxUroR57xs)**
- **[AI Code Generation Ethics](https://www.youtube.com/results?search_query=ai+code+ethics)**
