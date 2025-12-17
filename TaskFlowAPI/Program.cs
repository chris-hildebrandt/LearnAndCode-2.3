// This is the main entry point for our entire web application.
// In modern .NET applications, `Program.cs` is where the application is configured and launched.
// It sets up all the necessary services, configures the HTTP request pipeline, and starts the web server.
// This file uses the "minimal hosting model" introduced in .NET 6, which simplifies the startup process.

using FluentValidation.AspNetCore; // Integrates FluentValidation with ASP.NET Core.
using Microsoft.EntityFrameworkCore; // Provides Entity Framework Core functionality.
using Serilog; // Imports the Serilog logging library.
using TaskFlowAPI.Data; // Imports our DbContext.
using TaskFlowAPI.Extensions; // Imports our custom extension methods (like the exception handler).
using TaskFlowAPI.Repositories; // Imports our repository implementation.
using TaskFlowAPI.Repositories.Interfaces; // Imports the repository interface.
using TaskFlowAPI.Services.Interfaces; // Imports the service interface.
using TaskFlowAPI.Services.Tasks; // Imports our service implementation.

// The `WebApplication.CreateBuilder(args)` creates a new web application builder.
// The `builder` object is used to configure everything about our application before it runs.
var builder = WebApplication.CreateBuilder(args);

// --- Configure Logging ---
// Here, we are setting up Serilog as our logging provider.
// Serilog is a powerful and popular logging library for .NET that provides structured logging.
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Reads configuration from appsettings.json.
    .Enrich.FromLogContext() // Adds contextual information to log events.
    .WriteTo.Console() // Configures logs to be written to the console.
    .CreateLogger();

// This line tells the application host to use Serilog for all its internal logging.
builder.Host.UseSerilog();

// --- Configure Services (Dependency Injection) ---
// This is the "service container" or "Dependency Injection (DI) container".
// Here, we register all the services that our application will need.
// When a part of our application (like a controller) asks for a service (like ITaskService),
// the DI container knows how to create and provide it.

builder.Services.AddRouting(options => options.LowercaseUrls = true); // Configures all URLs to be lowercase.
builder.Services.AddControllers(); // Adds the services required for controllers.

// These two lines set up Swagger, a tool for generating interactive API documentation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Here we register our `TaskFlowDbContext` with the DI container.
// We configure it to use SQLite as the database provider and get the connection string from our configuration file.
// CODE SMELL: Magic Strings (Clean Code Ch 17, G25)
// The string "DefaultConnection" is a magic string used as a configuration key.
// If this key name changes in appsettings.json, the code breaks silently or throws at runtime.
// Magic strings are hard to find with refactoring tools and easy to misspell.
// Refactor by: Extract configuration keys to named constants (private const string DefaultConnectionKey = "DefaultConnection").
builder.Services.AddDbContext<TaskFlowDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// We register our repository and service layers here.
// `AddScoped` means that a new instance of the service will be created once per client request.

// Repository Layer (Week 8)
builder.Services.AddScoped<ITaskRepository, TaskRepository>();

// Service Layer (Week 9)
builder.Services.AddScoped<ITaskService, TaskService>();

// This registers FluentValidation services, allowing it to automatically find and run our validators.
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddMemoryCache(); // Adds a default in-memory cache service.
builder.Services.AddResponseCompression(); // Adds services for compressing HTTP responses.

// TODO Week 11: Register TaskMapper and validators once extracted
// TODO Week 12: Register task filter strategies and composite filter

// --- Build the Application ---
// The `builder.Build()` method creates the actual web application instance (`app`)
// using all the services and configuration we defined above.
var app = builder.Build();

// --- Configure the HTTP Request Pipeline (Middleware) ---
// This section defines the "middleware pipeline".
// Each `app.Use...` call adds a middleware component that processes incoming HTTP requests.
// The order of middleware is very important.

// This checks if the application is running in the "Development" environment.
if (app.Environment.IsDevelopment())
{
    // If so, we enable the Swagger UI, which provides a helpful, interactive page for testing the API.
    app.UseSwagger();
    app.UseSwaggerUI();
}

// This is our custom global exception handler, defined in the `ExceptionMiddlewareExtensions` file.
app.UseTaskFlowExceptionHandler();

app.UseSerilogRequestLogging(); // Adds a middleware that logs details about each incoming request.
app.UseResponseCompression(); // Compresses responses to save bandwidth.
app.UseHttpsRedirection(); // Redirects HTTP requests to HTTPS.
app.UseAuthorization(); // Applies authorization policies.

// This middleware is responsible for routing incoming requests to the correct controller action.
app.MapControllers();

// --- Run the Application ---
// This final line starts the web server and begins listening for incoming HTTP requests.
// The application will continue to run until it is stopped.
app.Run();