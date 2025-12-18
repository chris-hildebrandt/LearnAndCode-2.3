// This file defines the "Database Context" for our application.
// Think of the DbContext as a session with the database. It's the primary gateway for all database operations.
// It allows us to query data from the database and save changes back to it.
// This class is the heart of our data access layer when using Entity Framework (EF) Core.

using Microsoft.EntityFrameworkCore; // Imports the core functionality of Entity Framework Core.
using TaskFlowAPI.Entities; // Imports our data models (entities) so EF Core knows about them.

namespace TaskFlowAPI.Data;

/// <summary>
/// This is our application's DbContext. It inherits from the base DbContext class provided by EF Core.
/// It is responsible for:
/// 1. Managing the database connection.
/// 2. Exposing database tables as DbSet properties.
/// 3. Tracking changes made to entities.
/// 4. Saving changes back to the database.
/// </summary>
public class TaskFlowDbContext : DbContext
{
    /// <summary>
    /// The constructor for our DbContext. It takes DbContextOptions as a parameter.
    /// These options are configured in `Program.cs` and contain the database connection string
    /// and other configuration details. The `base(options)` call passes these options up to the
    /// base DbContext class.
    /// </summary>
    public TaskFlowDbContext(DbContextOptions<TaskFlowDbContext> options) : base(options)
    {
    }

    // These `DbSet<T>` properties represent the tables in our database.
    // EF Core will use these properties to query and save instances of our entity classes.
    // The name of the DbSet is typically the plural form of the entity name.

    /// <summary>
    /// Represents the collection of all tasks in the database. You will use this to query for tasks.
    /// </summary>
    public DbSet<TaskEntity> Tasks => Set<TaskEntity>();

    /// <summary>
    /// Represents the collection of all projects in the database.
    /// </summary>
    public DbSet<ProjectEntity> Projects => Set<ProjectEntity>();

    /// <summary>
    /// This method is called by EF Core when it is creating the database model for the first time.
    /// It allows us to configure our entity models using the "Fluent API".
    /// The Fluent API gives us more control over the database schema than just using attributes on our entity classes.
    /// </summary>
    /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Here, we are using the Fluent API to configure the 'ProjectEntity'.
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.ToTable("Projects"); // Explicitly set the table name.
            entity.HasKey(project => project.Id); // Define the primary key.
            entity.Property(project => project.Name) // Configure the 'Name' property.
                .HasMaxLength(200) // Set a maximum length.
                .IsRequired(); // Mark it as a required (non-nullable) field.

            entity.Property(project => project.Description)
                .HasMaxLength(1000); // Set a maximum length for the description.
        });

        // And here, we configure the 'TaskEntity'.
        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.ToTable("Tasks"); // Set the table name.
            entity.HasKey(task => task.Id); // Define the primary key.
            entity.Property(task => task.Title)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(task => task.Description)
                .HasMaxLength(2000);

            entity.Property(task => task.Priority)
                .HasDefaultValue(0); // Set a default value for the Priority field.

            entity.Property(task => task.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Use a SQL function to set the default value.

            // This configures the relationship between TaskEntity and ProjectEntity.
            entity.HasOne(task => task.Project) // A task has one project.
                .WithMany(project => project.Tasks) // A project has many tasks.
                .HasForeignKey(task => task.ProjectId) // The foreign key is ProjectId on the TaskEntity.
                .OnDelete(DeleteBehavior.Cascade); // If a project is deleted, delete all its associated tasks.

            // These lines create database indexes on certain columns.
            // Indexes can significantly speed up query performance for columns that are frequently searched or sorted.
            entity.HasIndex(task => task.Title);
            entity.HasIndex(task => task.Priority);
        });

        // "Data Seeding" is the process of populating the database with initial data.
        // This is extremely useful for development and testing, as it ensures that the database
        // has some consistent data to work with right from the start.
        // Seed baseline data to make local testing easier.
        modelBuilder.Entity<ProjectEntity>().HasData(
            new ProjectEntity
            {
                Id = 1,
                Name = "Sample Project",
                Description = "Seed data used during the course. Feel free to replace after Week 6."
            }
        );

        modelBuilder.Entity<TaskEntity>().HasData(
            new TaskEntity
            {
                Id = 1,
                Title = "Wireframe onboarding flow",
                Description = "Starter task included for Week 2 naming refactor.",
                Priority = 1,
                ProjectId = 1,
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new TaskEntity
            {
                Id = 2,
                Title = "Interview stakeholders",
                Description = "Use this to practice refactoring and testing.",
                Priority = 2,
                ProjectId = 1,
                CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, DateTimeKind.Utc)
            }
        );
    }
}