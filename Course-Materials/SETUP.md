# Environment Setup Guide

## Required Software

1. **.NET 8 SDK** – [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **IDE** – Visual Studio 2022 (17.8+) or VS Code with the official C# Dev Kit
3. **Git** – [Download](https://git-scm.com/downloads)
4. **Database** – SQLite (bundled with .NET provider) or SQL Server Express if your team standardises on it
5. **API Client** – Postman, Insomnia, or VS Code REST Client extension

## Verification Checklist

```bash
# 1. Confirm .NET 8 installation (can run from anywhere)
dotnet --version
# Expected output: 8.0.xxx

# 2. Install Entity Framework Core tools globally (can run from anywhere)
dotnet tool install --global dotnet-ef
# This installs the database migration tool you'll need later

# 3. Clone your fork (run from wherever you keep your projects)
git clone https://github.com/YOUR-USERNAME/LearnAndCode-TaskFlow.git
cd LearnAndCode-TaskFlow
# You are now in: LearnAndCode-TaskFlow/ (root directory)

# 4. Restore NuGet packages (run from root directory)
dotnet restore TaskFlowAPI.sln
# This downloads all C# dependencies for the project

# 5. Navigate to the API project directory
cd TaskFlowAPI
# You are now in: LearnAndCode-TaskFlow/TaskFlowAPI/

# 6. Apply database migrations (MUST run from TaskFlowAPI/ directory)
dotnet ef database update
# This creates taskflow.db with tables and seed data
# Note: After restarting your terminal, you can use 'dotnet ef' instead of the full path

# 7. Run the API (from TaskFlowAPI/ directory)
dotnet run
# Keep this terminal running - the API will be live at https://localhost:5001

# 8. In a NEW terminal, navigate back to root and run tests
cd ..
# You should now be in: LearnAndCode-TaskFlow/ (root directory)
dotnet test TaskFlowAPI.sln
```

You should see Swagger running on `https://localhost:5001/swagger` and the test suite reporting two skipped placeholder tests (no failures).

## Troubleshooting

- **`dotnet` not found** – Ensure the SDK is installed and the terminal has been restarted after installation.
- **SQLite locking errors** – Close any DB browser windows and delete `TaskFlowAPI/taskflow.db` before re-running migrations.
- **`dotnet-ef` command missing** – Install the tool with `dotnet tool install --global dotnet-ef` and use `~/.dotnet/tools/dotnet-ef ...` the first time.
- **SSL certificate warnings** – Run `dotnet dev-certs https --trust` and restart your browser.

## Getting Help

1. Ask in the team chat with the full command and error output.
2. Reach out directly to your mentor or a member of the Learn and Code leadership team.

Do not remain blocked. Setup should take less than 30 minutes end-to-end.