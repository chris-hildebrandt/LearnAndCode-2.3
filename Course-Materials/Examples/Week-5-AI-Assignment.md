# Week 5: AI Tools Assignment

**Complete this assignment file as you work, then copy it to your main fork's `Course-Materials/Examples/` folder for submission.**

This assignment has two main components:
1. **AI Practical Assessment** - Compare AI-generated code to your manual Week 4 work
2. **AI Ethics Journal** - Reflect on professional responsibility and AI use

**Estimated Time:** 60 minutes total (40 min practical + 20 min ethics)

---

## Part 1: AI Practical Assessment (40 minutes)

### Task Overview
Use an AI agent to complete the same assignments you did manually in Week 4, then critically analyze the differences.

### Step-by-Step Instructions

#### 1. Select Your AI Tool (2 min)

**Recommended: GitHub Copilot Chat** (built-in to Codespaces, has best access to your code, requires no additional setup)

*Alternative options if Copilot is unavailable:*
- Cursor (corporate license available via [Freshrealm Cursor Subscription Request](https://intimetec.freshservice.com/support/catalog/items/118))
- Other LLM-based coding assistant such as:
   - Perplexity AI
   - Claude (claude.ai)
   - ChatGPT (chat.openai.com)
   - Gemini
*Note: Alternative tools will require you to upload files or grant access to your GitHub fork.*

**AI Tool I Used:** ___________________

#### 2. Create AI Test Branch from Week 3 (1 min)

**Important:** We need AI to work on the ORIGINAL messy code (before your Week 4 changes) so you can fairly compare AI's approach to your manual work.

```bash
# Start from your Week 3 submission (before Week 4 refactoring)
git checkout week-03-submission
git checkout -b week-05-ai-test
```

**Verify:** Open `ReportsController.cs` - the `GenerateProjectSummaryReport` method should still be 100+ lines (messy). If it's already refactored, you're on the wrong branch.

**Note:** This keeps your Week 4 work safe while giving AI the same starting point you had.

#### 3. AI-Assisted Refactoring (20 min)

Use your chosen AI tool to complete the Week 4 assignments:
- Refactor `GenerateProjectSummaryReport` method in `ReportsController.cs`
- Add `UpdateTaskAsync` and `DeleteTaskAsync` method signatures to `ITaskService`
- Implement controller actions for `PUT /api/tasks/{id}` and `DELETE /api/tasks/{id}`

**Example prompt for GitHub Copilot Chat:**
```
I need to refactor the GenerateProjectSummaryReport method in ReportsController.cs 
according to Clean Code principles. The method should:
- Be no more than 15 lines
- Extract logical blocks into private helper methods with clear names
- Follow the Stepdown Rule

[Paste or reference the current GenerateProjectSummaryReport method]

Please provide the refactored code with extracted helper methods.
```

**Prompts I used:**
1. ___________________
2. ___________________
3. ___________________

#### 4. Test AI-Generated Code (5 min)

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```

If it compiles, test manually:
```bash
dotnet run --project TaskFlowAPI
```

- Test refactored endpoint: `GET https://localhost:5001/api/Reports/project-summary/1`
- Verify new endpoints in Swagger: `PUT /api/tasks/{id}` and `DELETE /api/tasks/{id}`
- Note: New endpoints will return 500 errors (NotImplementedException) - this is expected

**Test Results:**
- Did it compile? ___________________
- Did it run correctly? ___________________
- Any errors or warnings? ___________________

**Final Verdict:**

- [ ] Ready to use as-is

- [ ] Needs minor tweaks: [list changes needed]

- [ ] Completely broken: [explain why]

#### 5. Document Your Comparison Analysis (11 min)

Compare your Week 4 manual refactoring to the AI-generated version:

**What did AI do better than you?**
___________________

**What did AI do worse than you?**
___________________

**What surprised you most about the AI's approach?**
___________________

**Specific Examples - Give at least one concrete example comparing your code to AI's:**

*Method extraction decisions:*
___________________

*Naming quality:*
___________________

*Code organization:*
___________________

### Trust and Verification

**Would you trust this code in production?**
- [ ] Yes
- [ ] No
- [ ] Partially

**Why?** ___________________

**What checks did you perform before deciding?**
1. ___________________
2. ___________________
3. ___________________

### Agent Quality Assessment

**Did the AI agent:**
- Provide clear explanations? [ ] Yes [ ] No - Notes: ___________________
- Miss any obvious issues? ___________________
- Hallucinate non-existent problems? ___________________
- Did the AI try to use a library (like AutoMapper or a different JSON serializer) that isn't installed in our project? ___________________

**Key Takeaway:**

What's the most important lesson about using AI for code generation, and how does this relate to professional responsibility from Week 1?

___________________

---

## Part 2: AI Ethics Journal (20 minutes)

Reflect on these three professional scenarios involving AI use.

### Scenario 1: Disclosure and Responsibility (~7 min)

**Situation:**
You used GitHub Copilot to generate a `CalculateCompletionPercentage` method for Week 4. It works perfectly. The PR is ready to merge.

**Core Question:**
Should you disclose to your team that AI wrote this code? If the code has a bug discovered 6 months later (e.g., division by zero when no tasks exist), who is responsible? Does your answer change if this is partner code where the client is paying $200/hour for your expertise?

**Your Position:**

___________________

---

### Scenario 2: Learning vs. Copying (~7 min)

**Situation:**
It's Week 8. Your `TaskRepository.GetByIdAsync` implementation returns an error:
```
System.InvalidOperationException: Sequence contains no matching element
```

You ask AI: "Why am I getting this error?" AI immediately shows you the fix: change `Single()` to `SingleOrDefault()`.

**Core Question:**
What's the difference between "AI helping you learn" vs. "AI doing your homework"? Where is YOUR personal line for using AI in this course?

**Your Reflection:**

*I WILL use AI for:*
___________________

*I will NOT use AI for:*
___________________

*I will disclose AI usage when:*
___________________

---

### Scenario 3: Intellectual Property (~6 min)

**Situation:**
GitHub Copilot was trained on millions of public GitHub repositories, including some with restrictive licenses. When you ask it to generate a "task priority algorithm," it produces code that looks suspiciously similar to code you've seen in an open-source project.

**Core Question:**
Is AI-generated code "original"? Should you use AI in partner codebases? What legal, reputation, and quality risks exist?

**Your Answer:**

___________________

---

### My AI Use Principles

Based on these scenarios and your practical experience this week:

**1. Disclosure:**
I will disclose AI usage when ___________________

**2. Verification:**
I will always verify AI code by ___________________

**3. Learning:**
I will use AI to enhance (not replace) learning by ___________________

**4. Professional Integrity:**
In partner projects, I will ___________________

**5. Quality Manifesto Alignment:**
The ITT Quality Manifesto value that most applies to responsible AI use is ___________________ because ___________________

---

**Signature:** _________________, Week 5, _________________

---

## Submission Instructions

**Once you've completed this assignment:**

1. Switch back to your submission branch:
   ```bash
   git checkout week-05-submission
   ```

2. Copy this completed assignment file to your submission branch:
   ```bash
   git checkout week-05-ai-test -- Course-Materials/Examples/Week-05-AI-Assignment.md
   git add Course-Materials/Examples/Week-05-AI-Assignment.md
   git commit -m "feat: Complete Week 5 AI assignment"
   ```

3. Continue with the remaining steps in the Week 5 module (Journal section, etc.).

---

## Why This Assignment Matters

**Practical Skills:**
- Experience AI's real capabilities and limitations with YOUR code.
- Learn to critically evaluate AI output (not just accept it).
- Build "trust but verify" habits for professional work.

**Professional Ethics:**
- Articulate YOUR boundaries for AI use (not just follow rules).
- Prepare for real client conversations about AI-generated code.
- Align AI use with ITT values and professional responsibility.