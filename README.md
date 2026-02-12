# Academic Task Manager

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-10.0-512BD4)](https://docs.microsoft.com/ef/)
[![SQLite](https://img.shields.io/badge/SQLite-3-003B57)](https://www.sqlite.org/)

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

## Deployment Overview (Render)

The application is deployed on Render with the following configuration:

- **Platform:** Render Web Service
- **Build Command:** `dotnet publish -c Release -o out`
- **Start Command:** `dotnet out/AcademicTaskManager.dll`
- **Environment:** .NET 10 runtime
- **Database:** SQLite (persistent volume recommended for production)

**Deployed Application URL:** _(To be added after deployment)_

**Note:** For production deployment, consider migrating from SQLite to PostgreSQL or SQL Server for improved scalability and concurrent user support.

## Project Management

### Repository

- **GitHub:** https://github.com/coloub/AcademicTaskManager
- **Issues:** https://github.com/coloub/AcademicTaskManager/issues

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
