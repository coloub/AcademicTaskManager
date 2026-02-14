# Academic Task Manager

## Course Context

**CSC325 – .NET Software Development**  
Spring 2026 Academic Project

## Project Overview

Academic Task Manager is a full-stack web application designed to help university students organize academic projects and track associated tasks. Built with Blazor Server and .NET 10, the system provides complete CRUD functionality, user authentication, real-time updates, and progress tracking. The application demonstrates modern .NET development practices including Entity Framework Core for data persistence, ASP.NET Core Identity for security, and a service layer architecture pattern for maintainability.

This project was developed to provide students with a practical tool for managing coursework while demonstrating proficiency in enterprise-level .NET technologies suitable for production environments.

## Key Features

- Complete project and task CRUD operations with validation
- User authentication and authorization with ASP.NET Core Identity
- Real-time UI updates via SignalR
- Project statistics and completion tracking
- Responsive Bootstrap 5 interface
- Individual user workspaces with data isolation
- Overdue task detection and visual indicators

## Technology Stack

- **Frontend:** Blazor Server (Razor Components)
- **Backend:** ASP.NET Core 10.0
- **Database:** SQLite with Entity Framework Core 10.0
- **Authentication:** ASP.NET Core Identity
- **UI Framework:** Bootstrap 5.3
- **Real-Time:** SignalR

## System Architecture

### Overview

The application follows a three-layer architecture pattern:

1. **Presentation Layer** - Blazor components and pages
2. **Service Layer** - Business logic and data operations
3. **Data Layer** - Entity Framework Core and SQLite

### Authentication & Authorization

ASP.NET Core Identity manages user accounts and authentication. Each user has isolated access to their own projects and tasks. Authorization is enforced at the service layer through user ID validation before any data operation.

**Security Features:**

- Password requirements (min 6 chars, uppercase, lowercase, number, special character)
- User-specific data isolation
- Development mode auto-confirms email for testing

### Database Configuration

**Database:** SQLite (file-based, located at `Data/app.db`)

**Schema:**

- **AspNetUsers** - Identity user accounts
- **Projects** - Academic projects (Id, Title, Description, CreatedDate, OwnerUserId)
- **Tasks** - Project tasks (Id, Title, Description, DueDate, Status, ProjectId)

**Relationships:**

- User (1) → Projects (N) - Cascade delete
- Project (1) → Tasks (N) - Cascade delete

**Migrations:** Code-first approach with Entity Framework Core migrations

## Running the Application Locally

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Code editor (Visual Studio 2022 or VS Code recommended)
- Modern web browser

### Setup Steps

1. **Clone the repository**

```bash
git clone https://github.com/coloub/AcademicTaskManager.git
cd AcademicTaskManager
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Apply database migrations**

```bash
dotnet ef database update
```

4. **Run the application**

```bash
dotnet run
```

Or use the PowerShell script: `.\run-app.ps1`

5. **Access the application**  
   Navigate to: `http://localhost:5034`

### Common Commands

```bash
dotnet build                              # Build project
dotnet ef migrations add MigrationName    # Create migration
dotnet ef database drop --force           # Reset database
```

## Test User Credentials

For testing purposes, create an account with:

- **Email:** `student@academic.com`
- **Password:** `Test123!`

Or register a new account at `/Account/Register`

## Deployment on Render.com (Docker)

The application is production-ready with Docker support and auto-migration for PostgreSQL.

### Prerequisites

1. **Render.com Account**: [Sign up](https://render.com)
2. **PostgreSQL Database**: Create a managed PostgreSQL instance on Render
3. **GitHub Repository**: Code must be in a Git repository

### Step-by-Step Deployment

#### 1. Create PostgreSQL Database

1. In Render Dashboard, click **"New +"** → **"PostgreSQL"**
2. Name: `academictaskmanager-db`
3. Database: `academic_db`
4. User: `academic_user`
5. Region: Choose closest to your users
6. Plan: Free (for testing) or Starter (for production)
7. Click **"Create Database"**
8. **Copy the "Internal Database URL"** (starts with `postgresql://`)

#### 2. Create Web Service (Docker)

1. Click **"New +"** → **"Web Service"**
2. Connect your GitHub repository
3. Configure:
   - **Name**: `academictaskmanager`
   - **Environment**: `Docker`
   - **Region**: Same as database
   - **Branch**: `main`
   - **Dockerfile Path**: `Dockerfile` (auto-detected)
   - **Docker Command**: Leave empty (uses ENTRYPOINT)

#### 3. Configure Environment Variables

In **"Environment"** section, add:

```bash
# Required - PostgreSQL connection from Step 1
ConnectionStrings__DefaultConnection=<PASTE_INTERNAL_DATABASE_URL_HERE>

# Optional - Production environment (auto-set)
ASPNETCORE_ENVIRONMENT=Production
```

**Example:**

```
ConnectionStrings__DefaultConnection=postgresql://academic_user:password@dpg-xxx.oregon-postgres.render.com/academic_db
```

#### 4. Deploy

1. Click **"Create Web Service"**
2. Render will:
   - Build Docker image
   - Apply database migrations automatically ✅
   - Start the application
3. Wait 3-5 minutes for first deployment

### Deployment Status

- **Live URL**: https://academictaskmanager.onrender.com
- **Auto-Migration**: ✅ Enabled (runs on startup)
- **Database**: PostgreSQL (Render managed)
- **Environment**: Production

### Troubleshooting

#### Error: "Format of the initialization string does not conform"

**Cause**: Missing or invalid `ConnectionStrings__DefaultConnection`  
**Fix**:

1. Go to Render Dashboard → Your Web Service → "Environment"
2. Verify the PostgreSQL connection string is set correctly
3. Must start with `postgresql://` or `Host=`
4. Click "Save Changes" and redeploy

#### Error: "relation AspNetUsers does not exist"

**Cause**: Migrations not applied  
**Fix**: Application now auto-applies migrations. If still failing:

1. Check logs for migration errors
2. Verify PostgreSQL database is accessible
3. Ensure connection string has correct permissions

#### Error: "libgssapi_krb5.so.2: cannot open shared object"

**Fix**: Already resolved in updated Dockerfile (installs required libraries)

#### Check Logs

```bash
# In Render Dashboard
Web Service → Logs → View real-time logs
```

### Manual Migration (if needed)

If auto-migration fails, run manually via Render Shell:

1. Go to Web Service → "Shell"
2. Run:

```bash
dotnet ef database update --no-build
```

### Environment Variables Reference

| Variable                               | Required | Description                | Example                          |
| -------------------------------------- | -------- | -------------------------- | -------------------------------- |
| `ConnectionStrings__DefaultConnection` | ✅ Yes   | PostgreSQL URL from Render | `postgresql://user:pass@host/db` |
| `ASPNETCORE_ENVIRONMENT`               | No       | Runtime environment        | `Production` (default)           |
| `PORT`                                 | No       | Auto-set by Render         | `10000`                          |

### Local Testing with Docker

```bash
# Build image
docker build -t academictaskmanager .

# Run with SQLite (development)
docker run -p 5000:5000 \
  -e PORT=5000 \
  -e ConnectionStrings__DefaultConnection="DataSource=app.db" \
  academictaskmanager

# Run with PostgreSQL (production simulation)
docker run -p 5000:5000 \
  -e PORT=5000 \
  -e ASPNETCORE_ENVIRONMENT=Production \
  -e ConnectionStrings__DefaultConnection="Host=localhost;Database=academic;Username=postgres;Password=pass" \
  academictaskmanager
```

### Post-Deployment Checklist

- ✅ Application accessible at Render URL
- ✅ Can register new user account
- ✅ Can create projects and tasks
- ✅ Data persists after server restart
- ✅ No migration errors in logs

---

**📘 Advanced Configuration:** See [DEPLOYMENT_RENDER.md](DEPLOYMENT_RENDER.md) for additional details

## Project Management

### Repository

- **GitHub:** https://github.com/coloub/AcademicTaskManager

### Project Board

- **Trello:** https://trello.com/invite/b/6973b0d61f7cded0464bd5e6/ATTIfb834341ed1a5749b476e7cd704379b033E615EF/academic-task-manager-cse-325

## Project Structure

```
AcademicTaskManager/
├── Components/
│   ├── Account/          # Authentication pages
│   ├── Layout/           # Layouts and navigation
│   └── Pages/            # Application pages
│       ├── Projects/     # Project CRUD
│       └── Tasks/        # Task CRUD
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Project.cs
│   └── ProjectTask.cs
├── Services/
│   ├── ProjectService.cs # Project business logic
│   └── TaskService.cs    # Task business logic
├── Migrations/           # EF Core migrations
└── Program.cs            # Application configuration
```

## Participants

**CSC325 Academic Project - Spring 2026**

- **Jose Mendoza** - Lead Developer
- **Ana Torres** - Developer

## License

This project is educational software developed as an academic project for CSC325 - .NET Software Development.

---

**Version 1.0.0** | February 2026 | Developed with Blazor and .NET 10
