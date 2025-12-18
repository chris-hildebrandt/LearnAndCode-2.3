# Weekly Module Continuity Checklist (REVISED)

## Purpose
Standard checklist for reviewing weekly module files to ensure consistency, quality, and proper formatting across all weeks.

---

## 1. Agent Meta-Commentary Check

**❌ NEVER INCLUDE:**
- "NEW" tags
- "REVISED" tags
- "UPDATED" tags
- Strikethrough text
- Version commentary ("we've added...", "previously...")
- References to what changed

**✅ CORRECT STYLE:**
- Present tense, authoritative
- Direct instructions
- Clean, as if it's always been this way

**How to Check:**
- Search document for: `NEW`, `REVISED`, `UPDATED`, `~~`
- Look for phrases like "we've changed" or "now includes"

---

## 2. Branch Naming Consistency

**Standard Format:** `week-XX-submission` (where XX is week number)

**Check These Locations:**
- Section 5, Step 1 (first instruction)
- Section 8 (Submission Process)

**Common Errors:**
- `week-XX/<your-name>` (old pattern)
- `week-XX-work` (wrong suffix)
- Inconsistent between Section 5 and Section 8

---

## 3. Section Numbering & Structure

**Standard Structure (11 sections):**
1. Learning Objectives
2. Reading & Resources
3. This Week's Work
4. Files to Modify
5. Step-by-Step Instructions
6. How to Test
7. Success Criteria
8. Submission Process
9. Journal and Discussion Prep
10. Time Estimate
11. Additional Resources

**Exceptions:**
- Week 1: 14 sections (orientation week - acceptable)

**How to Check:**
- Count section headers (`##`)
- Verify numbering is sequential
- Check for missing or extra sections

---

## 4. Time Estimate Accuracy

**Check:**
1. **Inline estimates** in section headers match breakdown
   - Example: "Reading & Resources (45 min)" must appear in Section 10

2. **Math adds up** in Section 10
   - List all time items
   - Calculate total
   - Verify total matches stated total

3. **Consistent formatting:**
   - Use `—` (em dash) not `-` (hyphen) or `–` (en dash)
   - Format: `XX min — Description.`
   - Total format: `**Total:** ~X hours XX minutes.`

**Example Section 10:**
```markdown
- 45 min — Reading.
- 30 min — Code refactoring.
- 20 min — Journal and discussion prep.
- 15 min — Test, create PR, and merge.

**Total:** ~1 hour 50 minutes.
```

**Math check:** 45 + 30 + 20 + 15 = 110 minutes = 1 hour 50 minutes ✅

---

## 5. Punctuation Consistency

**Rules:**
- ALL bullet points end with periods
- ALL list items use proper punctuation
- Section headers: No periods
- Subsection headers: No periods

**How to Check:**
- Scan all bullet lists in:
  - Learning Objectives
  - This Week's Work
  - Files to Modify
  - Success Criteria
- Verify each ends with period

---

## 6. Journal & Discussion Prep Quality

**Purpose:** Questions should cement learning and spark reflection, NOT be academic busywork.

**Standard Format:** `*Label: Question text?*`

**Quality Criteria:**

### Journal Questions Should:
- ✅ Connect directly to THIS WEEK'S code changes
- ✅ Be answerable from hands-on experience (not theory)
- ✅ Spark reflection on "why" decisions were made
- ✅ Feel practical and relevant to real development work
- ✅ Cement principles learned in the module

### Journal Questions Should NOT:
- ❌ Require external research or reading beyond the assignment
- ❌ Feel like academic essay prompts
- ❌ Be answerable without doing the work
- ❌ Use overly formal or theoretical language
- ❌ Ask "What is X?" (definitions) - ask "How did X affect your work?"

### Discussion Prep Should:
- ✅ Prepare students to share concrete examples from their code
- ✅ Identify decision points where developers might disagree
- ✅ Surface "gray areas" or judgment calls
- ✅ Encourage bringing actual code snippets to discuss

**Good Examples (from Weeks 2-7):**

✅ **Week 2:** *"Which type of bad name (abbreviations, noise words, or mental mapping) is the most difficult for a new teammate to understand, and why does this slow team velocity?"*
- Connects to their refactoring work
- Practical, not theoretical
- Answerable from experience

✅ **Week 4:** *"Now that you've extracted logic like CalculatePercentage into private methods inside the Controller, how would you write a unit test for JUST that calculation? Is it easier or harder than testing the original function?"*
- Directly references their code changes
- Sparks critical thinking about architecture
- Sets up future learning (Week 9)

✅ **Week 6:** *"What criteria did you use to decide when to make a commit? How did you determine the scope of each commit?"*
- Reflection on decisions they just made
- No "right answer" but valuable to articulate
- Practical workflow question

**Bad Examples (What to Avoid):**

❌ *"Describe the theoretical foundations of dependency inversion and how it relates to SOLID principles in enterprise architecture."*
- Too academic
- Not tied to their code
- Feels like homework, not reflection

❌ *"What is the difference between an object and a data structure?"*
- Definition question
- Answerable without doing the work
- Should be: "Look at TaskDto.cs and TaskEntity.cs. Which is an object? Which is a data structure? Why?"

❌ *"Discuss the philosophical implications of encapsulation in object-oriented design."*
- Too theoretical
- Not grounded in their experience
- Lacks concrete connection to TaskFlowAPI

### How to Review Journal Prompts:

**Ask yourself:**
1. Can this be answered WITHOUT doing the assignment? (If yes → rewrite)
2. Does it reference specific files/methods they modified? (Should → yes)
3. Would a student roll their eyes at this? (If yes → too academic)
4. Does it help cement the "why" behind the code change? (Should → yes)
5. Could this be answered in 2-3 sentences based on hands-on experience? (Should → yes)

**Tone Benchmark:**
- Use Weeks 2-7 as the gold standard
- Conversational, practical, reflective
- "You just did X. How did Y affect your decision?" not "Explain the theory of Z."

---

## 7. Step-by-Step Quality

**Requirements:**
- Numbered steps (1, 2, 3...)
- Each step is clear and actionable
- Includes exact git commands where applicable
- Safe for least experienced developers

**Red Flags:**
- Vague instructions ("do the thing")
- Missing git commands
- Assumes knowledge not taught yet
- Overly complex nested steps

---

## 8. Reading Time Estimates

**Check:**
- Inline time estimate in Section 2 header
- Matches time in Section 10 breakdown
- Realistic for actual content

**Video Resources:**
- If videos listed, time should reflect watching them
- Clearly mark videos as optional if not required

**Example:**
```markdown
## 2. Reading & Resources (45 min)

- **Clean Code Chapter 10** (30 min).
- **[Article Title](link)** (15 min).
```

Section 10 must include: `45 min — Reading.`

---

## 9. Files to Modify Alignment

**Check:**
- Files listed in Section 4 match files mentioned in Section 5 (Step-by-Step)
- No "ghost files" (mentioned but never actually modified)
- No missing files (modified but not listed)

**Example Check:**
- Section 4 lists: `TaskEntity.cs`, `TaskFlowDbContext.cs`
- Section 5 should mention modifying both
- If Section 5 only modifies TaskEntity.cs → Section 4 error

---

## 10. Success Criteria Alignment

**Check:**
- Success criteria (Section 7) match what Step-by-Step (Section 5) actually does
- No criteria for work not assigned
- No missing criteria for assigned work

**Example:**
- Section 5 says "Add Complete() method"
- Section 7 must include "Complete() method implemented"

---

## 11. Additional Resources Organization

**Standard Subsections:**
- Examples (internal course files)
- External Resources (articles, documentation)
- Video Resources (if applicable)
- Deep Dives / Optional (clearly marked)

**How to Check:**
- Section 11 should have clear subsections using `###`
- Internal examples listed first
- External resources grouped logically
- Optional content clearly labeled

---

## 12. Submission Process Standardization

**Section 8 must be identical across all weeks:**

```markdown
> **Reminder:** For your weekly submission, please follow the fork-and-PR workflow outlined in the `SETUP.MD` guide. You will create a Pull Request on your **own fork**, not the main curriculum repository, and then approve and merge it yourself.

1. Create a new branch for your weekly work (e.g., `git checkout -b week-XX-submission`).
2. Commit your changes to this branch (e.g., `git commit -m "feat: Complete Week XX work"`).
3. Push the branch to your forked repository on GitHub.
4. On GitHub, create a Pull Request from your new branch to your `main` branch.
5. Review, approve, and merge your own Pull Request.
```

**Only XX changes** - rest is copy/paste

---

## 13. Assignment-Chapter Alignment

**Critical Check:**
- Reading assignment (Section 2) matches work assignment (Sections 3-5)
- No busywork - every task teaches a principle from the reading
- Assignment is relevant to TaskFlowAPI's actual needs

**Red Flags:**
- Reading about X, assignment does Y (unrelated)
- Assignment feels artificial or forced
- Work doesn't fit naturally into project structure

---

## Quick Scan Checklist

Use this for rapid review:

- [ ] No "NEW", "REVISED", "UPDATED" tags anywhere
- [ ] Branch naming: `week-XX-submission` consistently
- [ ] 11 sections (or 14 for Week 1)
- [ ] Time math adds up correctly
- [ ] All bullets end with periods
- [ ] Journal questions use `*Label: Question?*` format
- [ ] **Journal questions are practical, not theoretical**
- [ ] **Journal questions reference specific code changes**
- [ ] **Discussion prep asks for concrete examples**
- [ ] Files to Modify = Files mentioned in Step-by-Step
- [ ] Success Criteria = Work actually assigned
- [ ] Step-by-Step has clear numbered steps
- [ ] Section 8 (Submission) is standard template
- [ ] Assignment teaches principles from reading
- [ ] Additional Resources has subsections

---

## When to Use This Checklist

**Use for:**
- ✅ New weekly modules created
- ✅ Existing modules being updated
- ✅ After AI generates content (always verify)
- ✅ Before marking a week "complete"
- ✅ When standardizing multiple weeks at once

**Don't need for:**
- ❌ Quick typo fixes
- ❌ Journal entry updates (student-facing)
- ❌ Example files (different format)

---

## Journal Quality Examples Reference

### ✅ GOOD (Weeks 2-7 Standard):

**Week 2:**
*"Customer-Centric Design: How does a vague name like `svc` or `dt` in a critical service method ultimately increase customer risk (e.g., through delayed bug fixes or new feature implementation)?"*

**Week 4:**
*"Function Size: Describe the most significant function you extracted. Why did you choose to separate it? What naming strategy did you use to make its purpose clear?"*

**Week 7:**
*"Objects vs. Data Structures: Look at TaskDto.cs and your new TaskEntity.cs. Which one is a Data Structure? Which one is an Object? Why?"*

### ❌ BAD (Too Academic/Theoretical):

❌ *"Explain the theoretical foundations of the Dependency Inversion Principle and its relationship to other SOLID principles."*

❌ *"Discuss the philosophical implications of abstraction in software design."*

❌ *"What are the academic definitions of high-level and low-level modules in dependency management?"*

### ✅ HOW TO FIX:

**Before:** *"Explain the theoretical foundations of DIP."*

**After:** *"Describe one concrete dependency you inverted this week and the runtime risk it mitigates."*

**Why it's better:**
- References their actual work
- Asks for specific example
- Practical, not theoretical
- Answerable in 2-3 sentences