# Week 23: Final Polish & Presentation

## 1. Learning Objectives
- Deliver production-ready artifacts (code, docs, tests, demo).
- Produce clear written and video documentation for stakeholders.
- Reflect on end-to-end learning and identify next steps.

## 2. Reading & Resources (15 min)
- Review README, weekly docs, and ensure terminology is consistent (23-week references, TaskFlow naming).
- Skim final checklist below before starting.

## 3. This Week’s Work
- Polish codebase: remove unused TODOs, ensure comments explain “why,” not “what.”
- Finalise documentation: update README quick start, architecture overview, diagrams if needed.
- Ensure tests cover final features and run green.
- Record 5-minute demo video: overview, key features, design decisions, next steps.
- Prepare final retro notes.

## 4. Files to Modify

- This file (`Course-Materials/Weekly-Modules/week-23-final-polish.md`) – append your journal and discussion prompt responses.
- `WEEKLY_PROGRESS.md` (ensure all boxes checked)
- Additional docs: `docs/final-retro.md` (create)

## 5. Step-by-Step Instructions
1. Branch `week-23/<your-name>`.
2. Run `dotnet build` and `dotnet test`—fix any lingering warnings.
3. Review code for lingering smells or TODOs and clean up.
4. Update README with:
   - Architecture diagram or bullet list
   - Setup verification steps (validated)
   - API endpoint summary (link to Swagger)
5. Create `docs/final-retro.md` capturing:
   - Biggest growth areas
   - Remaining technical debt
   - Next steps plan (learning goals)
6. Record demo video (Loom/Teams) walking through API in <5 min. Include link in README + final retro.
7. Ensure `WEEKLY_PROGRESS.md` all checked; include total time spent.
8. Submit final PR and final weekly issue.

## 6. How to Test
```bash
dotnet build TaskFlowAPI.sln
dotnet test TaskFlowAPI.sln
```
- Optional: run integration smoke tests via Swagger or Postman collection.

## 7. Success Criteria
- README is production-ready and up to date.
- All docs reference 23-week program accurately.
- Demo video link accessible and under 5 minutes.
- Tests pass; no warnings/errors on build.
- Final retro completed.

## 8. Submission Process
- Commit `Week 23 – final polish`.
- PR summary includes demo video link and highlights final changes.
- Weekly issue attaches final retro and test/build output.
- Notify mentor in chat that final PR is ready for graduation review.

## 9. Journal and Discussion Prep

(Use this section as a journal of your learning. Answer the questions below after completing the reading and assignment. ALSO record any questions or comments you would like to bring up during this week's discussion.)

Journal:
*Polish Checklist:* Record outstanding TODOs you cleared and why they mattered most.

*Demo Narrative:* Outline the story arc for your 5-minute demo (problem, solution, impact).

Discussion Prep:
- What part of TaskFlow API are you most proud of and why?
- Where would you invest next if given two more weeks?
- How did Clean Code principles change your default coding habits?
- What risks remain for production readiness and how will you communicate them?

## 10. Time Estimate
- 15 min – Reading.
- 20 min – Build/test cleanup.
- 30 min – Documentation + retro.
- 30 min – Record/demo + upload.
**Total:** ~95 minutes.

## 11. Additional Resources

**Video Tutorials:**
- **[Event-Driven Architecture Explained](https://www.youtube.com/watch?v=DtuVN5g_e3k)**
- **[Event-Driven Architecture in the Real World](https://www.youtube.com/watch?v=ksRCq0BJef8)**
- **[Event-Driven Microservices](https://www.youtube.com/watch?v=moCcKZ_eHHs)**

**Technical Documentation:**
- **[Apache Kafka Documentation](https://kafka.apache.org/documentation/)**
- **[RabbitMQ Concepts](https://www.rabbitmq.com/tutorials/amqp-concepts.html)**
- **[AWS Event Bridge Patterns](https://docs.aws.amazon.com/eventbridge/latest/userguide/eb-event-patterns.html)**
- **[Technical Documentation Best Practices](https://www.writethedocs.org/guide/writing/beginners-guide-to-docs/)**
- **[Markdown Cheat Sheet](https://www.markdownguide.org/cheat-sheet/)**
- **[The Art of Writing Good Documentation](https://medium.com/analysts-corner/the-art-of-writing-good-documentation-6e4ce4cd3126)**
- **[YouTube: Writing effective documentation | Beth Aitman](https://www.youtube.com/watch?v=R6zeikbTgVc)**
- **[YouTube: A practical guide to making good documentation | Beth Aitman](https://www.youtube.com/watch?v=8TD-20Mb_7M)**
