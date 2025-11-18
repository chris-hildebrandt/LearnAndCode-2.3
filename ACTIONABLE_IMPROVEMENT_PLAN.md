# ACTIONABLE IMPROVEMENT PLAN
## TaskFlow API Clean Code Curriculum - Priority Fixes

**Created:** 2025-11-17  
**Based on:** 23-week junior developer simulation  
**Current Grade:** B- (73%)  
**Target Grade:** A- (90%+)

---

## IMPLEMENTATION STRATEGY

This plan is organized by **priority tiers** with **specific, actionable tasks** for each fix.

Each tier includes:
- **Tasks:** Concrete changes to make
- **Effort:** Estimated hours to implement
- **Impact:** Expected improvement in completion/understanding
- **Success Metrics:** How to validate the fix

---

## TIER 1: CRITICAL FIXES (Must Do - 8-12 hours)

### These fixes unblock students and eliminate major frustration points.

---

### 1.1 FIX WEEK 20: Make Self-Contained

**Current Problem:** Week 20 requires classmates for PR review - 0% of solo learners can complete

**Tasks:**
1. Create `/docs/sample-prs/` folder with 2-3 example PRs:
   - `week-19-factory-pattern.md` - Good implementation with questions
   - `week-17-tdd-example.md` - Has 2-3 issues to identify
   - `week-12-ocp-filters.md` - Clean implementation to praise

2. Each sample PR should include:
   - File diffs (before/after code)
   - Commit message
   - 2-3 issues for student to identify
   - 1-2 positive aspects to praise

3. Update `week-20-code-review-collaboration.md`:
   ```markdown
   ## 3. This Week's Work
   - Review **two** sample PRs from `docs/sample-prs/` folder
   - Leave at least three high-quality comments per PR
   - If in cohort with classmates, review their PRs instead
   
   ### Solo Learner Alternative:
   1. Review sample PRs in `docs/sample-prs/` folder
   2. Create `docs/my-week-20-review.md` with your feedback
   3. Self-review your own Week 17-19 PRs using checklist
   ```

4. Add comment quality examples:
   ```markdown
   ### High-Quality Comment Examples:
   
   **BAD:** "Fix this"
   **GOOD:** "This filtering breaks when priority list is empty because line 42 
   assumes Count > 0. Suggest adding: if (priorities.Count == 0) return true;"
   
   **BAD:** "Wrong"
   **GOOD:** "The validation should happen before database call (line 15) 
   to avoid unnecessary query. Consider moving validation to top of method."
   ```

**Effort:** 3-4 hours  
**Impact:** Unblocks 100% of solo learners  
**Success Metric:** Solo learners can complete Week 20 independently

---

### 1.2 FIX WEEK 1: Complete Setup Guide

**Current Problem:** 4 critical blockers in setup (NET not installed, dotnet-ef issues, DOTNET_ROOT, migration sync)

**Tasks:**
1. Update `SETUP.md` with complete installation section:
   ```markdown
   ## 1. Install .NET 8 SDK
   
   ### Windows:
   1. Download from https://dotnet.microsoft.com/download/dotnet/8.0
   2. Run installer
   3. Verify: `dotnet --version` (should show 8.0.x)
   
   ### macOS:
   ```bash
   brew install dotnet@8
   export DOTNET_ROOT=/usr/local/share/dotnet
   echo 'export DOTNET_ROOT=/usr/local/share/dotnet' >> ~/.zshrc
   dotnet --version
   ```
   
   ### Linux (Ubuntu/Debian):
   ```bash
   wget https://dot.net/v1/dotnet-install.sh
   chmod +x dotnet-install.sh
   ./dotnet-install.sh --channel 8.0
   export DOTNET_ROOT=$HOME/.dotnet
   export PATH=$PATH:$DOTNET_ROOT
   echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
   echo 'export PATH=$PATH:$DOTNET_ROOT' >> ~/.bashrc
   dotnet --version
   ```
   ```

2. Add EF Core tools installation:
   ```markdown
   ## 2. Install EF Core Tools
   
   ```bash
   dotnet tool install --global dotnet-ef --version 8.0.11
   dotnet ef --version
   ```
   
   **If `dotnet ef` command fails:** Run `export PATH="$PATH:$HOME/.dotnet/tools"`
   ```

3. Add verification checklist:
   ```markdown
   ## 3. Verify Setup
   
   Run these commands and verify output:
   ☐ `dotnet --version` → Shows 8.0.x
   ☐ `dotnet ef --version` → Shows 8.0.x
   ☐ `cd TaskFlowAPI && dotnet build` → Build succeeded
   ☐ `dotnet ef database update` → Applying migration...
   ☐ `dotnet test ../TaskFlowAPI.sln` → Tests passed
   
   All ✓? You're ready for Week 1!
   ```

4. Add troubleshooting section:
   ```markdown
   ## Troubleshooting
   
   **"You must install .NET to run this application"**
   - Set DOTNET_ROOT environment variable (see instructions above)
   - Restart terminal after setting
   
   **"The model for context has pending changes"**
   - Run: `dotnet ef migrations remove --force`
   - Run: `dotnet ef migrations add InitialCreate`
   - Run: `dotnet ef database update`
   
   **dotnet-ef not found**
   - Add to PATH: `export PATH="$PATH:$HOME/.dotnet/tools"`
   - Or install locally: `dotnet tool install dotnet-ef`
   ```

**Effort:** 2-3 hours  
**Impact:** Reduces Week 1 setup time from 2+ hours to 30 min  
**Success Metric:** 95% complete Week 1 without blockers

---

### 1.3 FIX WEEKS 14-15: Add Scaffolding

**Current Problem:** Zero TODOs/templates, DI factory pattern unexplained, forced design from scratch

**Tasks:**

#### Week 14 (ISP):
1. Add TODOs to `ITaskRepository.cs`:
   ```csharp
   namespace TaskFlowAPI.Repositories.Interfaces;
   
   // TODO Week 14: This interface violates ISP - it forces clients to depend on methods they don't use
   // TODO: Create ITaskReader interface with read-only methods (GetAllAsync, GetByIdAsync)
   // TODO: Create ITaskWriter interface with write-only methods (CreateAsync, UpdateAsync, DeleteAsync)
   // TODO: Update TaskRepository to implement both ITaskReader and ITaskWriter
   
   public interface ITaskRepository { ... }
   ```

2. Create `/docs/Examples/InterfaceSegregation.cs` with template:
   ```csharp
   // BEFORE (Fat Interface - ISP Violation):
   public interface ITaskRepository
   {
       Task<List<TaskEntity>> GetAllAsync();        // Read
       Task<TaskEntity?> GetByIdAsync(int id);      // Read
       Task<TaskEntity> CreateAsync(TaskEntity e);  // Write
       Task UpdateAsync(TaskEntity entity);         // Write
       Task DeleteAsync(TaskEntity entity);         // Write
   }
   
   // AFTER (Segregated Interfaces - ISP Compliant):
   public interface ITaskReader
   {
       Task<List<TaskEntity>> GetAllAsync();
       Task<TaskEntity?> GetByIdAsync(int id);
   }
   
   public interface ITaskWriter
   {
       Task<TaskEntity> CreateAsync(TaskEntity entity);
       Task UpdateAsync(TaskEntity entity);
       Task DeleteAsync(TaskEntity entity);
   }
   
   // Implementation (one class, two interfaces):
   public class TaskRepository : ITaskReader, ITaskWriter
   {
       // ... same implementation, now satisfies two interfaces
   }
   ```

3. Add DI pattern explanation to `week-14-interface-segregation.md`:
   ```markdown
   ### Registering One Class as Two Interfaces
   
   In `ServiceCollectionExtensions.cs`:
   
   ```csharp
   // Register concrete class once
   services.AddScoped<TaskRepository>();
   
   // Register as ITaskReader (factory delegates to same instance)
   services.AddScoped<ITaskReader>(sp => 
       sp.GetRequiredService<TaskRepository>());
   
   // Register as ITaskWriter (factory delegates to same instance)
   services.AddScoped<ITaskWriter>(sp => 
       sp.GetRequiredService<TaskRepository>());
   ```
   
   **Why this pattern?**
   - One concrete instance (efficient)
   - Two interface registrations (flexibility)
   - Clients get segregated interfaces they need
   ```

#### Week 15 (DIP):
1. Add "WHY Abstract System Time?" section to `week-15-dependency-inversion.md`:
   ```markdown
   ### Why Abstract System Time?
   
   **Problem with `DateTime.UtcNow`:**
   - Cannot control time in tests
   - Tests break if time-sensitive logic fails
   - Cannot test "created 5 minutes ago" scenarios
   
   **Solution: Invert the dependency**
   ```csharp
   // BEFORE (Direct Dependency - Hard to Test):
   var entity = new TaskEntity {
       CreatedAt = DateTime.UtcNow  // ❌ Can't control in tests
   };
   
   // AFTER (Abstraction - Easy to Test):
   var entity = new TaskEntity {
       CreatedAt = _clock.UtcNow  // ✅ Can inject FakeClock in tests
   };
   ```
   
   **Benefits:**
   - Deterministic tests (always returns same time)
   - Can test time-based logic
   - Can advance time in tests (`fakeClock.Advance(TimeSpan.FromDays(1))`)
   ```

2. Add template files:
   - `/TaskFlowAPI/Infrastructure/Time/ISystemClock.cs` (with TODO comments)
   - `/TaskFlowAPI/Infrastructure/Time/UtcSystemClock.cs` (with TODO comments)
   - `/TaskFlowAPI.Tests/Unit/FakeSystemClock.cs` (with TODO comments)

**Effort:** 3-4 hours  
**Impact:** Increases Weeks 14-15 completion from 50% to 85%  
**Success Metric:** Students complete without googling DI patterns

---

## TIER 2: HIGH PRIORITY (Should Do - 6-8 hours)

### These fixes significantly improve learning and reduce frustration.

---

### 2.1 ADD EXAMPLES TO WEEK 18: Code Smells

**Current Problem:** No examples of what smells look like IN this codebase

**Tasks:**
1. Add "Example Smells in TaskFlowAPI" section to `week-18-code-smells-refactoring.md`:
   ```markdown
   ### Example Code Smells to Refactor
   
   **Smell #1: Long Method (TaskService.UpdateTaskAsync)**
   - **Location:** Services/Tasks/TaskService.cs, lines 124-176
   - **Problem:** 53 lines (exceeds 20-line guideline)
   - **Refactor:** Extract Method
   - **Target:** Break into smaller methods:
     - ValidateUpdateRequest()
     - GetExistingTaskOrThrow()
     - ApplyUpdatesToTask()
     - HandleCompletionStatusUpdate()
   
   **Smell #2: Duplicate Code (Validation Pattern)**
   - **Location:** CreateTaskAsync and UpdateTaskAsync
   - **Problem:** Same validation-log-throw pattern repeated
   - **Refactor:** Extract Method
   - **Target:** Create reusable validation methods
   
   **Smell #3: Long Parameter List (Controller.GetAllTasksAsync)**
   - **Location:** Controllers/TasksController.cs, line 34
   - **Problem:** 7 parameters (exceeds 3-parameter guideline)
   - **Refactor:** Parameter Object
   - **Target:** Create TaskQueryParameters class
   ```

2. Add smell checklist:
   ```markdown
   ### Code Smell Checklist
   
   Scan your code for:
   ☐ **Long Method:** Methods >20 lines
   ☐ **Long Parameter List:** Methods with >3 parameters
   ☐ **Duplicate Code:** Same code block (>5 lines) in multiple places
   ☐ **Complex Conditionals:** Nested if/else >3 levels deep
   ☐ **Large Class:** Class >200 lines
   ☐ **Primitive Obsession:** Using primitives instead of objects
   ```

**Effort:** 2 hours  
**Impact:** Reduces smell identification time by 50%  
**Success Metric:** Students identify all 3 smells in <15 minutes

---

### 2.2 EXPLAIN FACTORY PURPOSE (Week 19)

**Current Problem:** Factory purpose unclear, feels like busywork

**Tasks:**
1. Add "WHY Factory Pattern?" section to `week-19-design-patterns.md`:
   ```markdown
   ### Why Factory Pattern?
   
   **Current Problem:**
   - TaskMapper creates entities (mixing concerns)
   - No place for context-aware logic
   - Creation logic scattered
   
   **Factory Solution:**
   - **Separation of Concerns:** Factory = creation, Mapper = conversion
   - **Extensibility:** Easy to add context-aware defaults
   - **Testability:** Creation logic tested separately
   
   **Example Use Cases:**
   - Set default priority based on user role
   - Set default project based on user context
   - Apply team-specific creation rules
   
   ### Factory vs Mapper
   
   ```csharp
   // TaskMapper: Converts BETWEEN representations
   public TaskDto ToDto(TaskEntity entity) { ... }      // Entity → DTO
   public TaskEntity ToEntity(CreateTaskRequest req);  // ❌ This is creation, not conversion!
   
   // TaskFactory: Creates NEW entities
   public TaskEntity CreateNewTask(CreateTaskRequest req) { ... } // ✓ Clear responsibility
   
   // Both exist; different purposes:
   // - Factory: Request → Entity (domain creation)
   // - Mapper: Entity ↔ DTO (presentation layer)
   ```
   ```

2. Add before/after comparison:
   ```markdown
   ### Before Factory (Week 11-18):
   ```csharp
   var entity = _mapper.ToEntity(request);  // Mapper doing creation
   var createdEntity = await _taskWriter.CreateAsync(entity);
   return _mapper.ToDto(createdEntity);     // Mapper doing conversion
   ```
   
   ### After Factory (Week 19):
   ```csharp
   var entity = _factory.CreateNewTask(request);  // Factory creates
   var createdEntity = await _taskWriter.CreateAsync(entity);
   return _mapper.ToDto(createdEntity);            // Mapper converts
   ```
   ```

**Effort:** 1 hour  
**Impact:** Increases understanding from 40% to 75%  
**Success Metric:** Students can explain factory vs mapper difference

---

### 2.3 ADD CONFIG EXAMPLES TO WEEK 21: API Design

**Current Problem:** No configuration code examples (API versioning, Swagger XML)

**Tasks:**
1. Add configuration snippets to `week-19-api-design-documentation.md`:
   ```markdown
   ### Step 3: Configure API Versioning
   
   In `Program.cs`, add after `AddControllers()`:
   
   ```csharp
   builder.Services.AddApiVersioning(options =>
   {
       options.DefaultApiVersion = new ApiVersion(1, 0);
       options.AssumeDefaultVersionWhenUnspecified = true;
       options.ReportApiVersions = true;
   });
   ```
   
   ### Step 7: Configure Swagger for XML Comments
   
   Update `AddSwaggerGen()` in `Program.cs`:
   
   ```csharp
   builder.Services.AddSwaggerGen(c =>
   {
       var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
       var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
       c.IncludeXmlComments(xmlPath);
   });
   ```
   ```

2. Add pagination defaults guidance:
   ```markdown
   ### Pagination Defaults (Industry Standards)
   
   - **Default page size:** 20 (balances data vs requests)
   - **Maximum page size:** 100 (prevents abuse)
   - **Default page:** 1 (first page)
   - **Validation:** page >= 1, pageSize >= 1 && pageSize <= max
   ```

**Effort:** 1 hour  
**Impact:** Reduces googling from 30 min to 5 min  
**Success Metric:** Students copy-paste config without googling

---

### 2.4 ADD CACHE EXAMPLES TO WEEK 22: Performance

**Current Problem:** Cache key generation, interface definition, TTL concept all unclear

**Tasks:**
1. Define `ITaskCache` interface in instructions:
   ```csharp
   public interface ITaskCache
   {
       Task<PagedResponse<TaskDto>?> GetAsync(string key);
       Task SetAsync(string key, PagedResponse<TaskDto> value, TimeSpan? ttl = null);
       Task RemoveAsync(string key);
       Task ClearAsync();
   }
   ```

2. Show cache key generation:
   ```csharp
   private string GenerateCacheKey(ITaskFilter? filter, int page, int pageSize)
   {
       // Simple approach: hash filter + pagination
       var filterKey = filter?.GetHashCode().ToString() ?? "all";
       return $"tasks_{filterKey}_p{page}_s{pageSize}";
   }
   ```

3. Add TTL guidance:
   ```markdown
   ### TTL (Time-To-Live) Guidelines
   
   - **Short (5-60 sec):** Frequently changing data (tasks)
   - **Medium (5-15 min):** Relatively stable data
   - **Long (1+ hour):** Rarely changing data
   
   **For tasks:** 60 seconds balances freshness and performance
   ```

4. Show invalidation pattern:
   ```csharp
   public async Task CreateTaskAsync(...)
   {
       // ... creation logic
       await _cache.ClearAsync();  // Invalidate all cached lists
       return result;
   }
   ```

**Effort:** 2 hours  
**Impact:** Reduces Week 22 googling from 60 min to 15 min  
**Success Metric:** Students implement caching in <90 minutes

---

## TIER 3: MEDIUM PRIORITY (Nice to Have - 4-6 hours)

### These fixes improve polish and consistency.

---

### 3.1 FIX LSP TESTING LIMITATION (Week 13)

**Tasks:**
1. Add note acknowledging limitation:
   ```markdown
   ### Note on Testing Real Repository
   
   In theory, we should test BOTH FakeTaskRepository AND TaskRepository
   against the same contract tests to verify LSP compliance.
   
   **Limitation:** TaskRepository requires DbContext, making it impractical
   to test in unit tests without integration test infrastructure.
   
   **Pragmatic Approach:**
   - Test FakeTaskRepository (easy to instantiate)
   - Use FakeTaskRepository in service tests
   - Trust that TaskRepository follows contracts (verified manually)
   
   **Production Recommendation:**
   - Add integration tests for TaskRepository
   - Test both implementations once infrastructure exists
   ```

2. Provide fake repository template in `docs/Examples/LiskovSubstitution.cs`

**Effort:** 1 hour  
**Impact:** Reduces confusion and frustration  
**Success Metric:** Students understand pragmatic trade-off

---

### 3.2 ADD DEMO VIDEO TEMPLATE (Week 23)

**Tasks:**
1. Add demo video outline to `week-21-final-polish.md`:
   ```markdown
   ### Demo Video Outline (5 minutes)
   
   **Section 1: Introduction (30 sec)**
   - Your name + project name
   - "I built TaskFlow API to learn Clean Code principles"
   
   **Section 2: Architecture Overview (1 min)**
   - Show README diagram
   - Explain Controller → Service → Repository
   - Mention SOLID principles applied
   
   **Section 3: API Demo (2 min)**
   - Open Swagger UI
   - Create a task (POST)
   - Get all tasks with pagination (GET)
   - Filter by status (GET)
   - Update task (PUT)
   - Show validation error handling
   
   **Section 4: Code Highlights (1 min)**
   - Show one clean code principle (e.g., SRP, OCP)
   - Show one design pattern (e.g., Repository, Factory)
   
   **Section 5: Next Steps (30 sec)**
   - What you'd improve (caching, more tests)
   - What you learned
   ```

**Effort:** 30 minutes  
**Impact:** Reduces video creation anxiety  
**Success Metric:** Students record video in <45 minutes

---

### 3.3 ADD COMMENT EXAMPLES (Week 3)

**Tasks:**
1. Add before/after examples:
   ```csharp
   // BEFORE (Delete These):
   // This method adds two numbers together ❌ Obvious from code
   public int Add(int a, int b) { return a + b; }
   
   // Initializes the variable to zero ❌ What, not why
   int count = 0;
   
   // AFTER (Keep These):
   // Fallback to 0 when no user preference exists ✓ Explains why
   int defaultPriority = userPreference ?? 0;
   
   /// <summary>
   /// Validates task before persistence. ✓ API documentation
   /// </summary>
   public void ValidateTask(TaskEntity task) { ... }
   ```

**Effort:** 30 minutes  
**Impact:** Minor clarity improvement  
**Success Metric:** Students confidently delete redundant comments

---

### 3.4 INCREASE TIME ESTIMATES (+30-50%)

**Tasks:**
1. Add "Junior vs Experienced" time estimates to all weeks:
   ```markdown
   ## 10. Time Estimate
   - 40 min – Reading
   - 60 min – Implementation
   - 20 min – Testing
   
   **Experienced Developer:** ~120 minutes
   **Junior Developer (First Time):** ~160-180 minutes (+33-50%)
   
   **Note:** Taking longer is NORMAL when learning. Focus on understanding, not speed.
   ```

2. Add time management note:
   ```markdown
   ### Time Management Tips
   
   - Don't rush to meet estimates
   - Learning takes time (that's OK!)
   - Ask for help if blocked >30 minutes
   - Quality > Speed
   ```

**Effort:** 1 hour  
**Impact:** Reduces time pressure stress  
**Success Metric:** Students feel less rushed

---

## TIER 4: LOW PRIORITY (Polish - 2-3 hours)

### 4.1 Add Architecture Diagrams

**Tasks:**
- Create Mermaid diagram for README
- Add to `docs/Examples/`

**Effort:** 1 hour

---

### 4.2 Add Production README Criteria

**Tasks:**
- Define "production-ready README" checklist
- Add template

**Effort:** 30 minutes

---

### 4.3 Add Final Retro Template

**Tasks:**
- Create `docs/final-retro-template.md`
- Include reflection prompts

**Effort:** 30 minutes

---

## IMPLEMENTATION TIMELINE

### Sprint 1 (Week 1): Critical Fixes
- **Duration:** 2 days
- **Tasks:** 1.1 (Week 20), 1.2 (Week 1), 1.3 (Weeks 14-15)
- **Outcome:** Unblock 100% of solo learners, reduce Week 1 failures by 80%

### Sprint 2 (Week 2): High Priority
- **Duration:** 2 days
- **Tasks:** 2.1 (Week 18), 2.2 (Week 19), 2.3 (Week 21), 2.4 (Week 22)
- **Outcome:** Reduce googling time by 60%, increase understanding by 30%

### Sprint 3 (Week 3): Medium Priority
- **Duration:** 1 day
- **Tasks:** 3.1 (Week 13), 3.2 (Week 23), 3.3 (Week 3), 3.4 (Time estimates)
- **Outcome:** Polish and consistency improvements

### Sprint 4 (Optional): Low Priority
- **Duration:** 1 day
- **Tasks:** 4.1-4.3 (Templates and diagrams)
- **Outcome:** Final polish

---

## SUCCESS METRICS

### Overall Goals:
- **Completion Rate:** 90%+ (vs current 82.6%)
- **Solo Learner Success:** 100% (vs current 0% for Week 20)
- **Time Accuracy:** Estimates within 20% of actual (vs current 40% overrun)
- **Understanding:** 80%+ can explain concepts (vs current 50-60% on SOLID)

### Per-Week Targets:
- Week 1: 95%+ complete without blockers
- Week 14-15: 85%+ complete without googling
- Week 18: 90%+ identify smells in <15 min
- Week 19: 75%+ understand factory purpose
- Week 20: 100% solo learners can complete
- Week 22: 80%+ implement caching in <90 min

---

## VALIDATION PLAN

### Before Implementation:
1. Review plan with curriculum team
2. Prioritize based on resources
3. Assign tasks to team members

### During Implementation:
1. Test each fix with 1-2 junior developers
2. Iterate based on feedback
3. Document unexpected issues

### After Implementation:
1. Run full curriculum with 3-5 new juniors
2. Collect completion metrics
3. Gather satisfaction surveys
4. Iterate on remaining issues

---

## COST-BENEFIT ANALYSIS

### Tier 1 (Critical): 8-12 hours
- **Cost:** ~2 days work
- **Benefit:** Unblock 100% of students, eliminate 3 major blockers
- **ROI:** High - prevents curriculum abandonment

### Tier 2 (High Priority): 6-8 hours
- **Cost:** ~1.5 days work
- **Benefit:** Reduce frustration, increase understanding by 30%
- **ROI:** High - significantly improves learning

### Tier 3 (Medium): 4-6 hours
- **Cost:** ~1 day work
- **Benefit:** Polish and consistency
- **ROI:** Medium - nice to have

### Tier 4 (Low): 2-3 hours
- **Cost:** ~0.5 days work
- **Benefit:** Final polish
- **ROI:** Low - cosmetic improvements

**Total Effort:** 20-29 hours (~3-4 days work)  
**Expected Grade Improvement:** B- (73%) → A- (90%+)

---

## CONCLUSION

Implementing **Tiers 1-2 only** (14-20 hours) will achieve **85-90% effectiveness** and eliminate all critical blockers.

**Highest ROI Fixes:**
1. Week 20 (unblocks 100% of solo learners)
2. Week 1 (prevents early abandonment)
3. Weeks 14-15 (fixes steepest difficulty cliff)
4. Week 18 examples (most common confusion point)

**Quick Wins (< 1 hour each):**
- Week 21 config snippets
- Week 19 factory explanation
- Week 3 comment examples

**Start with:** Week 20 fix (highest impact, self-contained task)

---

## APPENDIX: ISSUE REFERENCE

**See `/workspace/CURRICULUM_ISSUES.md` for complete issue tracking (Issues #1-#38+)**

**Key Issues by Week:**
- Week 1: Issues #1-#4 (setup blockers)
- Week 13: Issue #15 (LSP testing limitation)
- Week 14-15: Issues #16, #11-#14 (ISP/DIP scaffolding)
- Week 18: Issues #17-#18, #33-#34 (smell identification)
- Week 19: Issues #19-#21, #35-#37 (factory purpose)
- Week 20: Issue #22 (not self-contained) 🚨
- Week 21: Issues #26-#32 (config examples)
- Week 22: Issues #39-#48 (caching complexity)

