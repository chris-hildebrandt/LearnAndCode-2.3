# Week 3: Comments & Documentation

This week, we are focusing on the importance of comments in code, and how they contribute to clean code principles. We will explore best practices for writing comments that are clear, concise, and necessary. This module is based on **Clean Code Chapter 4: Comments**.

## 1. Learning Objectives

- Audit existing comments and documentation for clarity and necessity.
- Replace redundant comments with expressive names introduced in Week 2.
- Document “why” with comments only when the code cannot express intent.

## 2. Reading & Resources (35 min)

- **Clean Code – Chapter 4: Comments (pp. 59-76)**.
  - Focus on “explain yourself in code,” good vs. bad comments, and warning comments.
- **In Time Tec Quality Manifesto – Documentation sections**.
  - Revisit expectations for professional communication.

## 3. This Week’s Work

- Review `TasksController`, `ITaskService`, and `TaskService` for lingering comments and XML docs.
- Delete comments that repeat what the code now communicates; rewrite ones that capture intent or trade-offs.
- Add/upsert doc comments only where the reader cannot deduce intent (e.g., public API contracts, exceptional behaviour).
- Throughout the `TaskFlowAPI` files there are MANY comments describing the files and code. Each week you will encounter more of these files and comments. Starting today you will DELETE these comments when you have read them and understand the function of the code (the 'what') You may leave any comments necessary for explaining the 'why'.
- Update `README.md` “What You Will Ship” section with one paragraph summarising the documentation mindset (optional stretch).

## 4. Files to Modify

- `TaskFlowAPI/Controllers/TasksController.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs`
- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- This file (`Course-Materials/Weekly-Modules/week-03-comments.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions

1. Branch `week-03/<your-name>`.
2. Skim the targeted files and list every comment/XML doc.
3. For each comment decide: delete, reword to communicate intent, or move content into code (rename method, extract variable, etc.).
4. Add “why” comments where side effects, performance workarounds, or partner-facing constraints are not obvious.
5. Run `dotnet build TaskFlowAPI.sln` to ensure no removed XML docs were required for compiler directives.
6. Update `README.md` (optional) with a short note on documentation principles adopted this week.

## 6. How to Test

```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Optional: run `dotnet format --verify-no-changes` to ensure no whitespace regressions after deletions.

## 7. Success Criteria

- Every remaining comment explains intent, warns of side effects, or references an external constraint.
- No `TODOs` or redundant “what the code does” comments linger in the targeted files.
- Build/tests succeed with no new warnings.

## 8. Submission Process

1. Commit `Week 03 – comments and documentation cleanup`.
2. Weekly submission issue links to PR and highlights one before/after example.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

### Journal:
- *Comment Debt: Which comment deletions felt risky, and how did you mitigate the loss of context?*
- *Documentation Mindset: Capture one scenario where written documentation (`README.md`, journal, ticket) communicates intent better than inline comments.*

### Discussion Prep:
- *Share an example where you transformed a noisy comment into expressive code.*
- *When are warning comments acceptable in production code?*
- *How will you keep documentation updated as the `TaskFlowAPI` changes?*
- *Which areas still need better naming before more comments can be removed?*

## 10. Time Estimate

- 35 min – Reading & comment inventory.
- 25 min – Code/document updates.
- 15 min – Testing + PR/issue.
**Total:** ~75 minutes.

## 11. Comment Examples

### BEFORE (Delete These):

```csharp
// This method adds two numbers together ❌ Obvious from code
public int Add(int a, int b) { return a + b; }

// Initializes the variable to zero ❌ What, not why
int count = 0;

// Loop through tasks ❌ Redundant
foreach (var task in tasks)
{
    // Process each task ❌ Obvious
    ProcessTask(task);
}

// Get the user ❌ Repeats method name
var user = GetUser(userId);
```

### AFTER (Keep These):

```csharp
// Fallback to 0 when no user preference exists ✓ Explains why
int defaultPriority = userPreference ?? 0;

/// <summary>
/// Validates task before persistence. ✓ API documentation
/// </summary>
public void ValidateTask(TaskEntity task) { ... }

// TODO Week 12: support ?priority= query parameter ✓ Future work
// See issue #42 for requirements

// Performance: Batch size tuned for 95th percentile response time ✓ Design decision
const int BatchSize = 100;

// HACK: API returns inconsistent date format; parsing as string first ✓ Workaround with context
// Remove when API v2 is deployed (ETA: Q2 2025)
var dateString = response.ToString();
```

### When to Keep Comments:

1. **Legal/Copyright Headers**: Required by organization
2. **Public API Documentation**: XML docs for consumers
3. **Warning Comments**: Alert about consequences
4. **TODO Comments**: Track future work with context
5. **Explanation of Intent**: When code alone cannot express "why"
6. **Workarounds**: Temporary fixes with removal plan

### When to Delete Comments:

1. **Obvious "What"**: Comment repeats what code does
2. **Noise Words**: "Get", "Set", "Process" without value
3. **Commented-Out Code**: Use version control instead
4. **Outdated Comments**: No longer match the code
5. **Redundant Headers**: "Constructor", "Properties" sections
6. **Journal Comments**: Change history (use git log)

## 12. Additional Resources
