// This file provides an example of a "Unit Test".
// A unit test is a piece of code written to verify that a small, isolated piece of our application (a "unit")
// behaves as expected. In this case, the "unit" we are testing is the `TaskService`.

// Unit tests are fundamental to building reliable and maintainable software. They act as a safety net,
// allowing us to make changes and refactor our code with confidence, knowing that our tests will tell us if we broke something.

// We are using the xUnit testing framework, along with Moq for creating "mocks" and FluentAssertions for writing readable assertions.

using FluentAssertions; // Imports the FluentAssertions library for expressive and readable test assertions.
using Microsoft.Extensions.Logging; // Imports the logging interfaces.
using Moq; // Imports the Moq library for creating mock objects.
using TaskFlowAPI.DTOs.Requests; // Imports the request DTOs.
using TaskFlowAPI.Entities; // Imports the entity classes.
using TaskFlowAPI.Repositories.Interfaces; // Imports the repository interface that we will be mocking.
using TaskFlowAPI.Services.Tasks; // Imports the service class that we are testing.

namespace TaskFlowAPI.Tests.Examples;

/// <summary>
/// This class contains example unit tests for the `TaskService`.
/// Test class names often end with "Tests".
/// </summary>
public class TaskServiceTestsExample
{
    // The `[Fact]` attribute from xUnit marks this method as a test. The test runner will discover and execute it.
    // The `Skip` property tells the test runner to ignore this test. This is useful for example tests or tests that are temporarily broken.
    // CODE SMELL: Obscure Test (Clean Code Ch 17, p. 299)
    // This test name doesn't clearly describe what scenario is being tested.
    // The test body is also unclear about expected behavior.
    // Refactor by: Use descriptive test name following "MethodName_Scenario_ExpectedBehavior" pattern.
    [Fact(Skip = "Example only - Week 17 students will create their own passing tests.")]
    // Test method names should be descriptive and clearly state what they are testing and the expected outcome.
    // A common pattern is `MethodName_ExpectedBehavior_WhenCondition`.
    public async Task CreateTask_AddsTask_WhenRequestIsValid()
    {
        // Tests are typically structured in three parts: Arrange, Act, and Assert (AAA).

        // CODE SMELL: Comments (Clean Code Ch 17, p. 297)
        // These comments explain what the code does, which should be obvious from reading the code.
        // Comments like "ARRANGE: In this section..." are redundant.
        // Refactor by: Remove explanatory comments, keep only "why" comments if needed.
        // ARRANGE: In this section, we set up everything needed for the test.
        // This includes creating mock objects for dependencies, setting up input data, and configuring the expected behavior of the mocks.
        
        // We create a "mock" of the `ITaskRepository`. A mock is a fake object that we can control.
        // We don't want to use a real database in a unit test, so we mock the repository to simulate its behavior.
        var repositoryMock = new Mock<ITaskRepository>();
        var loggerMock = new Mock<ILogger<TaskService>>(); // We also mock the logger.

        // CODE SMELL: Magic Numbers (Clean Code Ch 17, G25)
        // The numbers 2 and 1 are magic numbers without clear meaning.
        // Refactor by: Extract to named constants: private const int MediumPriority = 2; private const int DefaultProjectId = 1;
        // This is the input data for our test.
        var request = new CreateTaskRequest
        {
            Title = "Document scaffolding",
            Description = "Demonstration of Arrange/Act/Assert",
            Priority = 2, // Magic number: 2
            ProjectId = 1 // Magic number: 1
        };

        // Here, we configure the behavior of our mock repository.
        // `Setup` tells Moq what to do when a specific method on the mock is called.
        repositoryMock
            .Setup(repo => repo.CreateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>())) // When `CreateAsync` is called with any TaskEntity...
            .ReturnsAsync((TaskEntity entity, CancellationToken _) => // ...immediately return a Task that resolves to the following:
            {
                // CODE SMELL: Magic Numbers (Clean Code Ch 17, G25)
                // The number 42 is a magic number (arbitrary test ID).
                // Refactor by: Extract to named constant: private const int TestTaskId = 42;
                entity.Id = 42; // Magic number: 42 - Simulate the database generating an ID for the new entity.
                return entity;
            });

        // We create an instance of the `TaskService`, the class we are actually testing.
        // We pass it our mock objects instead of real ones.
        var service = new TaskService(repositoryMock.Object, loggerMock.Object);

        // ACT: In this section, we execute the method we want to test.
        // (Week 9 students will remove exception by implementing Add; Week 17 tests will make it pass)
        var result = await service.Add(request);

        // ASSERT: In this section, we verify that the outcome of the `Act` step was correct.
        
        // We use `FluentAssertions` to make our assertions clear and readable.
        // `result.Title.Should().Be(...)` reads almost like a natural sentence.
        result.Title.Should().Be("Document scaffolding");
        result.Priority.Should().Be(2);

        // We can also use Moq to verify that certain methods on our mocks were called.
        // This assertion checks that the `CreateAsync` method on our repository mock was called exactly one time.
        // This confirms that our service is correctly interacting with its dependencies.
        repositoryMock.Verify(repo => repo.CreateAsync(It.IsAny<TaskEntity>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}