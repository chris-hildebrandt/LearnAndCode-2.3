# Environment Setup Guide (for GitHub Codespaces)

Welcome to the project! This guide will walk you through setting up your development environment in GitHub Codespaces.

## What is GitHub Codespaces?

Think of Codespaces as a computer in the cloud that is perfectly set up for this project. You don't need to install anything on your own machine. You'll write code and run commands in a version of VS Code that runs right in your web browser.

**The main parts of the VS Code window you'll use are:**
- **The Explorer (Left Side):** This shows all the project files, just like on a desktop computer.
- **The Editor (Center):** This is where you'll view and write code.
- **The Terminal (Bottom):** This is a command-line interface where you'll run commands to build, test, and run the application.

---

## Part 0: Getting Your Copy of the Project

Before you can set up anything, you need your own copy of the project code.

### Step 1: Fork the Repository

"Forking" creates your own personal copy of the project on GitHub.

1. Log into your github account
2. Go to the main project repository: ([Learn and Code 2.1](https://github.com/In-Time-Tec/LearnAndCode-2.1))
3. Click the **"Fork"** button in the top-right corner of the page
4. GitHub will create a copy under your account: `github.com/YOUR-USERNAME/LearnAndCode-2.1`

### Step 2: Open Your Fork in Codespaces

1. Go to YOUR forked repository page (`github.com/YOUR-USERNAME/LearnAndCode-2.1`)
2. Click the green **"Code"** button
3. Click the **"Codespaces"** tab
4. Click **"Create codespace on main"**
5. Wait 1-2 minutes while your cloud development environment loads

You'll see a VS Code interface open in your browser. You're now ready to start!

---

## Part 1: Initial Project Setup

You only need to do this setup once per Codespace.

### Step 1: Open the Terminal

First, you need to open the terminal panel where you'll type commands.

- **How to open it:** Look at the top menu bar in VS Code and click `Terminal` → `New Terminal` (or press 'CTRL + `')
- A panel should appear at the bottom of the screen
- You'll see a blinking cursor with a prompt like `@username ➜ /workspaces/LearnAndCode-2.1 $`

**This is where you'll type all the commands below.**

### Step 2: Check Where You Are

Let's verify you're in the correct starting location (the "root" directory of the project).

```bash
pwd
```

> **Expected Output:** You should see something like `/workspaces/LearnAndCode-2.1`

If you see something different, type:
```bash
cd /workspaces/LearnAndCode-2.1
```

### Step 3: Install the Database Tools

This project uses a database to store data, and we need a special tool called "Entity Framework Core" (or "EF Core") to manage it.

**Copy and paste this entire block into the terminal and press Enter:**

```bash
# Install the EF Core tool globally
dotnet tool install --global dotnet-ef

# Tell the terminal where to find the tool
export PATH="$PATH:$HOME/.dotnet/tools"

# Save this setting so it's remembered in future terminals
echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
```

**Verify it worked:**

```bash
dotnet ef --version
```

> **Expected Output:** You should see a version number like `8.0.11`  
> **If you see "command not found":** See the troubleshooting section at the end of this guide

### Step 4: Prepare the Database

Now we'll create the database for the application.

**First, navigate into the API project folder:**

```bash
cd TaskFlowAPI
```

> **How to tell where you are:** Your terminal prompt should now show `TaskFlowAPI` in the path, like:  
> `@username ➜ /workspaces/LearnAndCode-2.1/TaskFlowAPI $`

**Next, create the database:**

```bash
dotnet ef database update
```

> **What this does:** This command reads your project's code and creates a database file called `taskflow.db` with all the necessary tables and some sample data.

> **Expected Output:** You'll see messages like "Applying migration '20240101000000_InitialCreate'..." followed by "Done."

### Step 5: Run the Application

Now you're ready to start the API server!

**Make sure you're still in the `TaskFlowAPI` directory.** If you're not sure, run:

```bash
pwd
```

If you're not in the TaskFlowAPI directory, run `cd TaskFlowAPI` again.

**Start the application:**

```bash
dotnet run
```

> **What to expect:** You'll see text scrolling in the terminal as the project builds and starts. After a few seconds, you'll see:
> ```
> Now listening on: https://localhost:5001
> ```

**Opening the application in your browser:**

**Option 1 (Recommended):** A pop-up notification will appear in the bottom-right corner saying "Your application running on port 5001 is available."
- Click the **"Open in Browser"** button
- A new browser tab will open showing your API

**Option 2 (If no pop-up appears):**
- Click the **"PORTS"** tab in the bottom panel (next to "TERMINAL")
- Find port 5001 in the list
- Click the globe icon (🌐) next to it
- Or right-click and select "Open in Browser"

**To see the interactive API documentation:**
- Add `/swagger` to the end of the URL
- Example: `https://friendly-space-giggle-xxxxx-5001.app.github.dev/swagger`

> **Success!** You should see the Swagger UI with a list of API endpoints like "Tasks" and "Projects"

---

## Part 2: Running the Tests

Tests verify that your code works correctly. Let's make sure everything is set up properly.

### Step 1: Stop the Application

Go to the terminal where the API is running and press `Ctrl+C` to stop it.

### Step 2: Navigate to the Root Directory

We need to go back to the main project folder where the test solution file lives.

```bash
cd ..
```

> **Verify where you are:** Run `pwd` - you should see `/workspaces/LearnAndCode-2.1` (not `.../TaskFlowAPI`)

### Step 3: Run the Tests

```bash
dotnet test TaskFlowAPI.sln
```

> **Expected Output:** You should see:
> ```
> Passed!  - Failed:     0, Passed:     0, Skipped:     2, Total:     2
> ```
> 
> This means there are 2 placeholder tests that are intentionally skipped. **This is the correct starting state!**

---

## Part 3: Weekly Workflow (Saving and Submitting Your Work)

Each week, you'll save your work using Git. Think of Git like a "save game" system that tracks every change you make to the code.

**The analogy:** Creating a branch is like creating a separate save file so you can experiment without breaking your main game. When you're happy with your changes, you merge them back.

### The Process (Do this every week):

#### 1. Create a New Branch

A "branch" is your personal workspace for this week's assignment.

```bash
# Make sure you're on the main branch and it's up to date
git checkout main
git pull origin main

# Create a new branch with a descriptive name
git checkout -b week-01-introduction
```

> **Branch naming:** Use `week-XX-topic` format (e.g., `week-01-introduction`, `week-02-naming`, etc.)

#### 2. Do Your Work

Make changes to the code files as required for the week's assignment.

#### 3. Save Your Changes (Stage and Commit)

When you've completed your work, you need to "commit" it (take a snapshot of your changes).

```bash
# See what files you changed
git status

# Stage all your changes (tell Git which files to include in the snapshot)
git add .

# Commit your changes with a descriptive message
git commit -m "feat: Complete Week 1 introduction and setup"
```

> **Commit message tips:**
> - Start with a type: `feat:` (new feature), `fix:` (bug fix), `refactor:` (code improvement), `docs:` (documentation)
> - Be descriptive: "feat: Add input validation to Task creation" not just "changes"

#### 4. Push Your Branch to GitHub

This uploads your branch with all its commits to your personal fork on GitHub.

```bash
# Push to your fork (the -u flag remembers this branch for future pushes)
git push -u origin week-01-introduction
```

> **What happens:** Git uploads your code to GitHub. You'll see progress in the terminal.

#### 5. Create a Pull Request (PR)

A Pull Request is how you propose merging your changes back into the main branch.

1. Go to your repository on GitHub: `github.com/YOUR-USERNAME/LearnAndCode-2.1`
2. You'll see a yellow banner saying "week-01-introduction had recent pushes" with a green **"Compare & pull request"** button
3. Click that button
4. **CRITICAL CHECK:** Make sure the "base repository" dropdown shows **your fork** (`YOUR-USERNAME/LearnAndCode-2.1`), NOT the original repo
5. The base branch should be `main` and the compare branch should be your week branch
6. Review your changes in the "Files changed" tab
7. Write a description of what you did (optional but recommended)
8. Click **"Create pull request"**

#### 6. Review and Merge Your PR

For this course, you approve and merge your own PRs to practice the full development cycle.

1. On the PR page, click the **"Files changed"** tab and review your work one more time
2. Go back to the **"Conversation"** tab
3. Scroll down and click **"Merge pull request"**
4. Click **"Confirm merge"**
5. Optionally, click **"Delete branch"** to clean up (you can always recreate it if needed)

**🎉 Done!** Your changes are now merged into your main branch.

---

## Common Problems and How to Fix Them

### Problem: `dotnet ef: command not found`

**Why it happens:** The terminal doesn't know where to find the `dotnet-ef` tool you installed.

**How to fix:**

```bash
# Temporarily add the tool to your PATH
export PATH="$PATH:$HOME/.dotnet/tools"

# Try your command again
dotnet ef --version
```

If this works, make it permanent by running:

```bash
echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
```

Then close and reopen your terminal.

---

### Problem: `The model for context has pending changes`

**Why it happens:** You changed the database structure in code, but the database file doesn't match yet.

**How to fix:**

```bash
# Navigate to the API project folder
cd TaskFlowAPI

# Remove the old migration
dotnet ef migrations remove --force

# Create a new migration
dotnet ef migrations add InitialCreate

# Apply it to the database
dotnet ef database update
```

---

### Problem: `SQLite Error: database is locked`

**Why it happens:** Another program or VS Code extension is viewing the database file.

**How to fix:**

1. Close any tabs or extensions viewing `taskflow.db` (like SQLite Viewer)
2. If that doesn't work, delete and recreate the database:

```bash
# From the root directory
rm TaskFlowAPI/taskflow.db

# Navigate to API folder
cd TaskFlowAPI

# Recreate the database
dotnet ef database update
```

---

### Problem: Browser shows "Your connection is not private" warning

**Why it happens:** The application uses a "self-signed" SSL certificate for security, which browsers don't automatically trust.

**How to fix:** This is safe to ignore in development.

- Click **"Advanced"**
- Click **"Proceed to [URL]"** or **"Accept the risk"**

---

### Problem: `Port 5001 is already in use`

**Why it happens:** The application is already running in another terminal, or it didn't shut down properly.

**How to fix:**

**Option 1 (Easiest):**
1. Click the **"PORTS"** tab at the bottom of VS Code
2. Find port 5001
3. Right-click it and select **"Stop Forwarding Port"** or **"Stop Process"**

**Option 2:**
1. Find the terminal where `dotnet run` is still running
2. Click on that terminal
3. Press `Ctrl+C` to stop it

---

### Problem: `error: Your local changes would be overwritten by checkout`

**Why it happens:** You have uncommitted changes and you're trying to switch branches.

**How to fix:**

**Option 1 (Save your changes first):**
```bash
# Save your current work
git add .
git commit -m "wip: saving current progress"

# Now you can switch branches
git checkout main
```

**Option 2 (Discard your changes - be careful!):**
```bash
# This will DELETE all your uncommitted changes
git checkout -f main
```

---

### Problem: `The current .NET SDK does not support targeting .NET 8.0`

**Why it happens:** This should NOT happen in Codespaces, but if it does, the environment needs to be rebuilt.

**How to fix:**
1. Press `F1` (or `Cmd+Shift+P` on Mac)
2. Type "Codespaces: Rebuild Container"
3. Select it and wait for the rebuild (2-3 minutes)

---

## Getting Help

### The 30-Minute Rule

**If you're stuck for more than 30 minutes, ask for help immediately.** Professional developers escalate blockers quickly - it's a sign of maturity, not weakness.

### Where to Get Help

1. **Check this troubleshooting section** - Your issue might already be documented
2. **Ask in the team chat** - Include:
   - The exact command you ran
   - The full error message (copy/paste, don't screenshot if possible)
   - What you've already tried
3. **Reach out to your mentor** - They're here to help!
4. **Schedule a 1:1** - For longer issues, book time with your mentor or L&C leadership

### Tips for Asking Good Questions

- ✅ "I ran `dotnet ef database update` and got error: 'No executable found matching command dotnet-ef'. I tried adding it to PATH but still getting the error."
- ❌ "It doesn't work"

The more specific you are, the faster someone can help you!

---

## Quick Reference

### Common Commands

```bash
# Check where you are
pwd

# Go to root directory
cd /workspaces/LearnAndCode-2.1

# Go to API directory
cd TaskFlowAPI

# Go up one level
cd ..

# See what changed
git status

# Run the application (from TaskFlowAPI/)
dotnet run

# Run tests (from root)
dotnet test TaskFlowAPI.sln

# Build the project
dotnet build TaskFlowAPI.sln

# Update database (from TaskFlowAPI/)
dotnet ef database update
```

### Weekly Workflow Cheatsheet

```bash
# Start of week
git checkout main
git pull origin main
git checkout -b week-XX-topic

# During the week (as you work)
git add .
git commit -m "feat: descriptive message"

# End of week
git push -u origin week-XX-topic
# Then create PR on GitHub and merge it
```

---

## What's Next?

Once you've completed this setup:

1. ✅ You can run the application
2. ✅ You can run the tests  
3. ✅ You understand the Git workflow

**You're ready to start Week 1!** Go back to `Course-Materials/Weekly-Modules/week-01-introduction.md` and continue with the learning materials.

---

**Questions?** Don't wait - ask in the team chat or reach out to your mentor. Setup should take less than 30 minutes total. If you're taking longer, you might be stuck on something that's easy to fix with help!