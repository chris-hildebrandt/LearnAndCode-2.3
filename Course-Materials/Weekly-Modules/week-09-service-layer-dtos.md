# Week 9: Service Layer & DTOs

## 1. Learning Objectives
- Implement business logic in the service layer using repository abstractions.
- Map between entities and DTOs with intentional helpers.
- Handle not-found and validation errors consistently (pre-FluentValidation).

## 2. Reading & Resources (60 min)
- **Clean Code Chapter 6: Objects and Data Structures (pp. 89-110)** – Balance data exposure with behaviour.
- **Clean Code Chapter 11 (selected sections)** – Separate policy from implementation.
- **[Data Structures: A Practical Guide](https://www.freecodecamp.org/news/the-top-data-structures-you-should-know-for-your-next-coding-interview-36af0831f5e3/)** – Conceptual grounding.
- Optional: **Microsoft Docs: DTO pattern** – When and how to use DTOs in ASP.NET Core.
- Optional: `MapStruct`/`AutoMapper` docs for ideas on extraction (use judiciously to avoid premature abstraction).

## 3. This Week’s Work
- Implement `TaskService` methods (`GetAll`, `Get`, `Add`) using repository + mapping helpers.
- Add temporary guard clauses for create/update until Week 10 validation lands.
- Return `TaskDto` results to controllers.

## 4. Files to Modify

- `TaskFlowAPI/Services/Tasks/TaskService.cs`
- `TaskFlowAPI/Services/Interfaces/ITaskService.cs` (ensure async naming after Week 2/3 refactor)
- Optional: add/update mapping helpers if you extracted them.
- This file (`Course-Materials/Weekly-Modules/week-09-service-layer-dtos.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md`

## 5. Step-by-Step Instructions
1. Branch `week-09/<your-name>`.
2. Implement `GetAll`:
   - Call `_taskRepository.GetAllAsync`.
   - Map each entity via `MapToDto` helper.
   - Return read-only list (`AsReadOnly()` or `List<TaskDto>` to be converted later).
3. Implement `Get`:
   - Fetch entity by id via repository.
   - Return null if not found (controller returns 404).
4. Implement `Add`:
   - Guard `request.Title` and `request.ProjectId` (temporary until Week 10).
   - Map request to entity via `MapToEntity` helper.
   - Save via repository and return DTO.
5. Add logging for critical paths using `_logger` (`LogInformation` on create, `LogWarning` on not found).
6. Update helper methods if needed to hydrate `ProjectName` (null-safe).
7. Build/tests.

## 6. How to Test
```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
  - Optional: run API and test `GET /api/tasks` and `POST /api/tasks` via Swagger (use sample payload). Expect validation to be basic; more in Week 10.

## 7. Success Criteria
- No remaining `NotImplementedException` in `TaskService`.
- Controller endpoints return data instead of throwing.
- Logging statements added for create + not-found scenarios.
- Build/tests succeed; manual GET returns seeded tasks.

## 8. Submission Process
- Commit `Week 09 – task service implementation`.
- PR summary must include sample JSON payload used for manual testing.
- Weekly issue attaches Swagger screenshot showing GET response.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Mapping Choices:* Note one DTO vs entity mapping decision you made and why it improves clarity.

*Guard Rails:* Which inline validation or logging checks did you leave in place for now, and what is your plan for Week 10?

Discussion Prep:
- How does the service shield controllers from repository details?
- What validation gaps remain before Week 10?
- How will you extract mapper/validator in Week 11 without breaking consumers?
- Where might AutoMapper or manual mappers introduce risk in future refactors?
- What is the relationship between organizing objects and data structures and the Quality Manifesto's emphasis on technical excellence?

## 10. Time Estimate
- 10 min – Reading + code review.
- 35 min – Implement service methods + logging.
- 15 min – Manual testing + PR/issue.
**Total:** ~60 minutes.

## 11. Additional Resources
- **[Objects vs Data Structures](https://medium.com/hackernoon/objects-vs-data-structures-e380b962c1d2)**
- **[Data Structures: A Practical Guide](https://www.freecodecamp.org/news/the-top-data-structures-you-should-know-for-your-next-coding-interview-36af0831f5e3/)**
- **[Stackoverflow: Whats the difference between objects and data structures?](https://stackoverflow.com/questions/23406307/whats-the-difference-between-objects-and-data-structures)**
