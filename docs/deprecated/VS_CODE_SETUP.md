# ***DEPRECATED***
**we are now using github codespaces for the project environment, to setup github codespaces (which is much simpler) please follow `Course-Materials/SETUP.md`. If you wish to use VS Code instead for some reason, you may follow this guide, but it is not recommended as you may face errors or issues that the course leadership will be unable to assist with. Proceed with caution, and at your own risk!**

# Environment Setup Guide

## Required Software

1. **.NET 8 SDK** – [Download](https://dotnet.microsoft.com/download/dotnet/8.0)
2. **IDE** – Visual Studio 2022 (17.8+) or VS Code with the official C# Dev Kit
3. **Git** – [Download](https://git-scm.com/downloads)
4. **Database** – SQLite (bundled with .NET provider) or SQL Server Express if your team standardizes on it
5. **API Client** – Postman, Insomnia, or VS Code REST Client extension

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

## 2. Install EF Core Tools

```bash
dotnet tool install --global dotnet-ef --version 8.0.11
dotnet ef --version
```

**If dotnet ef command fails:** Run `export PATH="$PATH:$HOME/.dotnet/tools"` (add to `~/.bashrc` or `~/.zshrc` to persist)

## 3. Verify Setup

Run these commands and verify output:
- ☐ `dotnet --version` → Shows 8.0.x
- ☐ `dotnet ef --version` → Shows 8.0.x
- ☐ `cd TaskFlowAPI && dotnet build` → Build succeeded
- ☐ `dotnet ef database update` → Applying migration...
- ☐ `dotnet test ../TaskFlowAPI.sln` → Tests passed

All ✓? You're ready for Week 1!

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

## Weekly Submissions and Pull Requests

This course uses a fork-based workflow for weekly submissions. You will not be pushing code directly to the main curriculum repository. Instead, you will create Pull Requests (PRs) on your own forked repository. This allows you to track your progress and allows instructors to review your work.

### The Workflow

1.  **Create a Branch:** Each week, create a new branch for your work (e.g., `git checkout -b week-01-submission`).
2.  **Commit Your Changes:** As you complete the tasks, commit your changes to this branch.
3.  **Push to Your Fork:** Push the branch to your forked repository on GitHub (e.g., `git push -u origin week-01-submission`).
4.  **Create a Pull Request:**
    *   Go to your forked repository's page on GitHub.
    *   You will see a prompt to create a Pull Request from your new branch.
    *   **IMPORTANT:** Ensure the PR is targeting the `main` branch of **your own fork**, not the original `LearnAndCode-TaskFlow` repository. The base repository should be `YOUR-USERNAME/LearnAndCode-TaskFlow`.
5.  **Approve and Merge Your Own PR:** For the purposes of this course, you are responsible for approving and merging your own PRs. This simulates a complete development cycle.
    *   Open the PR on GitHub.
    *   Review your changes.
    *   Click "Merge pull request" and confirm the merge.

This process will be repeated for each weekly submission.

## Troubleshooting

### "You must install .NET to run this application"
- Set DOTNET_ROOT environment variable (see instructions above)
- Restart terminal after setting
- On Windows, restart your IDE as well

### "The model for context has pending changes"
- Run: `dotnet ef migrations remove --force`
- Run: `dotnet ef migrations add InitialCreate`
- Run: `dotnet ef database update`

### dotnet-ef not found
- Add to PATH: `export PATH="$PATH:$HOME/.dotnet/tools"` (Linux/macOS)
- Or use full path: `~/.dotnet/tools/dotnet-ef` (Linux/macOS)
- Or use full path: `%USERPROFILE%\.dotnet\tools\dotnet-ef` (Windows)
- Or install locally: `dotnet tool install dotnet-ef`

### SQLite locking errors
- Close any DB browser windows and delete `TaskFlowAPI/taskflow.db` before re-running migrations.

### SSL certificate warnings
- Run `dotnet dev-certs https --trust` and restart your browser.

### Build errors about missing packages
- Delete `bin/` and `obj/` folders
- Run `dotnet restore TaskFlowAPI.sln`
- Run `dotnet build TaskFlowAPI.sln`

### Port already in use (5001)
- Kill process using port: 
  - Windows: `netstat -ano | findstr :5001` then `taskkill /PID <PID> /F`
  - Linux/macOS: `lsof -ti:5001 | xargs kill -9`
- Or change port in `Properties/launchSettings.json`

## Getting Help

1. Ask in the team chat with the full command and error output.
2. Reach out directly to your mentor or a member of the Learn and Code leadership team.

Do not remain blocked. Setup should take less than 30 minutes end-to-end.
