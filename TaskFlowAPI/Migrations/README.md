## Database Migrations

This folder contains the database migrations for the project, which are managed by **Entity Framework (EF) Core**.

### What are Migrations?

Migrations are C# code files that represent incremental changes to the database schema. When we modify our data models (the `Entity` classes), we create a new migration to apply those changes (like adding a table or a column) to the database.

**Key Points:**

- **These files are auto-generated.** You should not manually edit the files in this folder. They are created using the `dotnet ef migrations add` command.
- **They keep the database in sync with the code.** Migrations ensure that every developer's local database has the same structure as what the application expects.
- **`TaskFlowDbContextModelSnapshot.cs`** is a snapshot of the current database model. EF Core uses it to determine what has changed when creating a new migration.

You can safely ignore this folder for day-to-day feature development. You will only interact with it indirectly when you need to update the database schema after changing an `Entity` file.