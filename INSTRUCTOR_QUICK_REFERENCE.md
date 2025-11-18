# INSTRUCTOR QUICK REFERENCE - Weeks 1-2 Simulation Results

## TL;DR

**Week 1: BROKEN - Fix immediately before next cohort** ⚠️  
**Week 2: EXCELLENT - Use as template for all weeks** ✓✓✓

---

## Week 1 Critical Fixes Needed (4 hours total work)

### Fix #1: Migration Files (1 hour) - HIGHEST PRIORITY
**Problem:** Pre-existing migration doesn't match DbContext model  
**Impact:** 100% of students will be blocked  
**Fix:**
```bash
cd TaskFlowAPI
dotnet ef migrations remove --force
dotnet ef migrations add InitialCreate
dotnet ef database update
# Verify it works, commit the corrected migration
```

### Fix #2: Setup Automation (2 hours)
**Problem:** Environment not configured, multiple manual steps fail  
**Options:**

**Option A - Codespaces devcontainer (RECOMMENDED):**
```json
// .devcontainer/devcontainer.json
{
  "name": "TaskFlow Dev Environment",
  "image": "mcr.microsoft.com/devcontainers/dotnet:8.0",
  "postCreateCommand": "dotnet tool install --global dotnet-ef --version 8.0.11 && dotnet restore TaskFlowAPI.sln && dotnet ef database update --project TaskFlowAPI",
  "customizations": {
    "vscode": {
      "extensions": ["ms-dotnettools.csharp"]
    }
  }
}
```

**Option B - Setup script:**
```bash
#!/bin/bash
# setup.sh

# Install .NET 8 if not present
if ! command -v dotnet &> /dev/null; then
    wget https://dot.net/v1/dotnet-install.sh
    chmod +x dotnet-install.sh
    ./dotnet-install.sh --channel 8.0
fi

# Set environment
export DOTNET_ROOT=$HOME/.dotnet
export PATH="$DOTNET_ROOT:$DOTNET_ROOT/tools:$PATH"

# Install EF tools
dotnet tool install --global dotnet-ef --version 8.0.11

# Restore and setup
dotnet restore TaskFlowAPI.sln
cd TaskFlowAPI
dotnet ef database update

echo "Setup complete! Run 'dotnet run' from TaskFlowAPI/ to start."
```

### Fix #3: Update Documentation (1 hour)
**Update SETUP.md:**
- Add environment prerequisites section
- Update time estimate: "10 min (Codespaces) or 30 min (local)"
- Link to setup.sh script if using Option B
- Add health check step: `dotnet --version && dotnet ef --version && dotnet test`

---

## Week 2 - Keep These Elements

**What Makes It Work:**
- ✓ Intentionally bad code with clear TODO markers
- ✓ Compiler-guided refactoring
- ✓ Realistic scope (2-3 files)
- ✓ Immediate feedback (build/test)
- ✓ Thoughtful journal questions connecting to business impact
- ✓ Accurate time estimates

**Template for Future Weeks:**
1. Clear, focused objective
2. Measurable success criteria
3. Realistic, manageable scope
4. Immediate feedback mechanism
5. Accurate time estimates
6. Journal questions that connect to Quality Manifesto

---

## Student Experience Summary

### Week 1 (Current):
```
Setup Time: 50 min (estimate was 30 min)
Blockers: 4 critical
Mentor Help: Required 3x
Emotional State: Frustrated → Exhausted
Risk: High dropout
```

### Week 1 (With Fixes):
```
Setup Time: <15 min (automated)
Blockers: 0
Mentor Help: Rarely needed
Emotional State: Ready to learn
Risk: Low dropout
```

### Week 2 (Current - Excellent):
```
Time: 1h 20min (estimate was 1h 30min)
Blockers: 0
Mentor Help: Not needed
Emotional State: Accomplished → Confident
Risk: None, builds engagement
```

---

## Before Next Cohort Checklist

- [ ] Fix migration files in repo
- [ ] Add devcontainer.json OR setup.sh script
- [ ] Test setup on fresh environment (both Codespaces and local)
- [ ] Update SETUP.md with new instructions
- [ ] Update Week 1 time estimates
- [ ] Add health check verification step
- [ ] Test Week 1 with fresh eyes (have someone else try it)

**Time Required:** 4-5 hours  
**Risk if not done:** 50% student drop rate

---

## Red Flags to Watch For

### In Student Questions/Feedback:
- "dotnet command not found"
- "migration has pending changes"
- "Spent hours on setup"
- "Should I skip Week 1?"
- "Is this course broken?"

If you see these → Week 1 fixes haven't been applied yet.

### In Submissions:
- Migration files with different timestamps than repo
- dotnet-install.sh in their commits
- Setup-related issues in PRs
- Took >2 hours for Week 1

---

## Success Indicators

### Week 1 (After Fixes):
- ✓ 95%+ students complete setup in <15 min
- ✓ <5% require mentor help with setup
- ✓ No "environment broken" reports
- ✓ Students reach reading/journal quickly

### Week 2 (Maintain Current):
- ✓ 100% completion rate
- ✓ Students finish under time estimate
- ✓ High confidence/satisfaction
- ✓ Code quality improves measurably

---

## Resources Created

1. **`/workspace/CURRICULUM_ISSUES.md`**
   - Detailed log of all issues found
   - Week-by-week breakdown
   - Patterns observed

2. **`/workspace/WEEK_1_REPORT.md`**
   - Complete Week 1 assessment
   - All blockers documented
   - Recommended fixes

3. **`/workspace/WEEK_2_REPORT.md`**
   - Week 2 success analysis
   - What to replicate
   - Minor enhancements

4. **`/workspace/WEEKS_1-2_SUMMARY.md`**
   - Comparative analysis
   - Overall recommendations
   - Risk assessment

5. **`/workspace/INSTRUCTOR_QUICK_REFERENCE.md`**
   - This document
   - Fast action items
   - Checklists

---

## Questions for Course Leadership

1. **Environment:** Will students use Codespaces, local setup, or both?
   - Answer determines which fix to prioritize

2. **Timeline:** When is next cohort starting?
   - Determines urgency of fixes

3. **Resources:** Who can test the fixes?
   - Need fresh eyes to verify

4. **Communication:** How to handle current cohort's Week 1 struggles?
   - May need make-up session or time extension

---

## One-Page Action Plan

```
PRIORITY 1 (DO TODAY):
□ Fix migrations in repo (1 hour)
□ Test migration fix (30 min)
□ Commit corrected migrations

PRIORITY 2 (DO THIS WEEK):
□ Choose: Codespaces devcontainer OR setup script
□ Implement chosen solution (2 hours)
□ Test on 3 different environments (1 hour)
□ Update documentation (1 hour)

PRIORITY 3 (BEFORE NEXT COHORT):
□ Dry run with volunteer "student" (1 hour)
□ Fix any remaining issues (buffer time)
□ Announce to cohort that Week 1 is fixed

TOTAL TIME: 6-7 hours
IMPACT: Course goes from "broken" to "excellent"
```

---

## Contact for Questions

Refer to detailed reports for:
- Specific error messages and solutions
- Student journey analysis
- Portfolio readiness assessment
- Recommendations for Weeks 3+

---

## Bottom Line

**Week 1 is the gateway.** Fix it and students will thrive in the excellent content (proven by Week 2).  
**Leave it broken** and students will never see Week 2.

**ROI of fixes:** 6 hours of work → saves 33 min × every student + prevents dropouts

**Recommendation:** Fix immediately. Course has great potential.

---

**Last Updated:** 2025-11-17  
**Simulation:** Complete for Weeks 1-2  
**Next Steps:** Await decision on fixes and/or continuation to Weeks 3-4
