# Quick Start Guide - Academic Task Manager

## For Developers

### Run the Application (Fast Method)

```powershell
cd "D:\CSE 325 MyProjects\Project\AcademicTaskManager"
.\run-app.ps1
```

Application will open at: **http://localhost:5034**

### Test Credentials

To create a test user:

1. Navigate to `/Account/Register`
2. Email: `student@academic.com`
3. Password: `Test123!`

## Useful Commands

```bash
# Build
dotnet build

# Run
dotnet run

# Create migration
dotnet ef migrations add MigrationName

# Apply migrations
dotnet ef database update

# Drop database (development)
dotnet ef database drop --force

# Recreate complete database
dotnet ef database drop --force && dotnet ef database update
```

## URL Structure

| Route                       | Description       |
| --------------------------- | ----------------- |
| `/`                         | Home page         |
| `/Account/Register`         | User registration |
| `/Account/Login`            | User login        |
| `/projects`                 | Project list      |
| `/projects/create`          | Create project    |
| `/projects/{id}`            | Project details   |
| `/projects/edit/{id}`       | Edit project      |
| `/tasks/create/{projectId}` | Create task       |
| `/tasks/edit/{id}`          | Edit task         |

## Key Files

```
Program.cs                    # Service and middleware configuration
appsettings.json              # Configuration (connection string)
Components/Pages/Projects/    # Project pages
Components/Pages/Tasks/       # Task pages
Data/ApplicationDbContext.cs  # EF Core context
Services/ProjectService.cs    # Project logic
Services/TaskService.cs       # Task logic
```

## Quick Troubleshooting

**Error: "no such table: AspNetUsers"**

```bash
dotnet ef database update
```

**Error: Pending migrations**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Reset complete database**

```bash
Remove-Item Data\app.db* -Force
dotnet ef database update
```

**Application won't start**

- Verify port 5034 is not in use
- Build first: `dotnet build`
- View errors: `dotnet run --verbosity detailed`

## Quick Testing

1. Register user
2. Create project
3. Create 2-3 tasks
4. Mark one task as completed
5. Verify statistics
6. Edit project
7. Delete task
8. Logout

## Complete Documentation

- [README.md](README.md) - Overview
- [IMPLEMENTACION.md](IMPLEMENTACION.md) - Complete technical report
- [USER_GUIDE.md](USER_GUIDE.md) - User guide
- [DEVELOPER_NOTES.md](DEVELOPER_NOTES.md) - Architecture and development

---

**Need more help?** Consult the complete documentation in .md files
