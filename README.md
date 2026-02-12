# Academic Task Manager

> Academic project and task management system for university students

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-10.0-512BD4)](https://docs.microsoft.com/ef/)
[![SQLite](https://img.shields.io/badge/SQLite-3-003B57)](https://www.sqlite.org/)

## Course Context

**CSC325 – .NET Software Development**  
Spring 2026 Academic Project

## Project Description

Academic Task Manager is a full-stack web application built with Blazor Server and .NET 10 that enables students to organize academic projects and track associated tasks. The system implements complete CRUD operations, authentication with ASP.NET Core Identity, and real-time updates through SignalR. The application follows a service layer architecture pattern with Entity Framework Core for data persistence in SQLite.

## Key Features

- **Academic Project Management** - Create, edit, and delete projects with validation
- **Task Control** - Manage tasks with due dates, statuses, and completion tracking
- **Progress Monitoring** - View statistics and completion percentages
- **Secure Authentication** - User system with ASP.NET Core Identity
- **Responsive Interface** - Functional across desktop, tablet, and mobile devices
- **Real-Time Updates** - Instant UI updates via SignalR

## Screenshots

### Home Page

![Home Page](docs/screenshots/home.png)

### Projects List

![Projects List](docs/screenshots/projects.png)

### Project Details with Statistics

![Project Details](docs/screenshots/details.png)

## Technology Stack

- **Frontend:** Blazor Server (Razor Components)
- **Backend:** ASP.NET Core 10.0
- **Database:** SQLite with Entity Framework Core 10.0
- **Authentication:** ASP.NET Core Identity
- **UI Framework:** Bootstrap 5.3
- **Icons:** Bootstrap Icons

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or higher
- Code editor (recommended: [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/))
- Modern web browser (Chrome, Edge, Firefox)

## Application Architecture

### Authentication System

The application uses ASP.NET Core Identity for user management. In development mode, email confirmation is disabled to facilitate testing. Each user has isolated access to their own projects and tasks, enforced through user ID validation in service methods.

### Database Design

SQLite database with three main entities:

- **AspNetUsers** - User accounts managed by Identity
- **Projects** - Academic projects with title, description, and creation date
- **Tasks** - Individual tasks linked to projects with due dates and status

Cascade delete is configured: deleting a user removes all projects, deleting a project removes all tasks.

### Service Layer Pattern

Business logic is separated into service classes:

- **ProjectService** - Project CRUD operations and statistics calculation
- **TaskService** - Task CRUD operations and status management

All data access is performed through Entity Framework Core with async operations.

## Installation and Setup

### 1. Clone the Repository

```bash
git clone https://github.com/coloub/AcademicTaskManager.git
cd AcademicTaskManager
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Apply Database Migrations

```bash
dotnet ef database update
```

### 4. Run the Application

**Option A: Using PowerShell Script**

```powershell
.\run-app.ps1
```

**Option B: dotnet Command**

```bash
dotnet run
```

**Option C: Visual Studio**

- Open `AcademicTaskManager.sln`
- Press F5

### 5. Access the Application

Open browser at: **http://localhost:5034**

## Test Credentials

To test the application, create a user with:

- **Email:** `student@academic.com`
- **Password:** `Test123!`

Or register your own user at `/Account/Register`

Password requirements: minimum 6 characters, must include uppercase, lowercase, number, and special character.

## Documentation

- **[Implementation Report](IMPLEMENTACION.md)** - Complete technical implementation details
- **[User Guide](USER_GUIDE.md)** - Application usage manual
- **[Developer Notes](DEVELOPER_NOTES.md)** - Architecture and development guidelines
- **[Quick Start](QUICKSTART.md)** - Rapid setup guide for developers

## Project Structure

```
AcademicTaskManager/
├── Components/
│   ├── Account/          # Authentication system
│   ├── Layout/           # Layouts and navigation
│   └── Pages/            # Application pages
│       ├── Projects/     # Project management
│       └── Tasks/        # Task management
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Project.cs        # Project model
│   └── ProjectTask.cs    # Task model
├── Services/
│   ├── ProjectService.cs # Project business logic
│   └── TaskService.cs    # Task business logic
├── Migrations/           # EF Core migrations
├── wwwroot/              # Static files
└── Program.cs            # Application configuration
```

## Testing

```bash
dotnet test
```

## Deployment

### Azure App Service

```bash
# Publish application
dotnet publish -c Release -o ./publish

# Deploy to Azure (requires Azure CLI)
az webapp up --name your-app-name --resource-group your-resource-group
```

### Docker

```bash
# Build image
docker build -t academic-task-manager .

# Run container
docker run -p 8080:80 academic-task-manager
```

See [DEVELOPER_NOTES.md](DEVELOPER_NOTES.md) for additional deployment options.

## Contributing

Contributions are welcome. Please:

1. Fork the project
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## Changelog

### Version 1.0.0 (February 10, 2026)

- Initial release
- Complete CRUD for projects and tasks
- Authentication system
- Project statistics
- Responsive UI

## Bug Reporting

If you encounter a bug, please open an [issue](https://github.com/coloub/AcademicTaskManager/issues) with:

- Detailed problem description
- Steps to reproduce
- Expected vs actual behavior
- Screenshots if applicable
- Environment information (OS, browser, .NET version)

## License

This project is educational software developed as an academic project for CSC325.

## Authors

**CSC325 Academic Project - Spring 2026**

- Jose Mendoza - Lead Developer
- Ana Torres - Developer

**Project Links:**

- GitHub Repository: https://github.com/coloub/AcademicTaskManager
- Trello Board: https://trello.com/b/6973b0d61f7cded0464bd5e6/academic-task-manager-cse-325

## Acknowledgments

- Blazor team for the framework
- Bootstrap for styling components
- Bootstrap Icons for iconography
- Microsoft for comprehensive.NET documentation

## Support

Need help?

- Read the [User Guide](USER_GUIDE.md)
- Consult [Developer Notes](DEVELOPER_NOTES.md)
- Report an [issue](https://github.com/coloub/AcademicTaskManager/issues)

---

**Developed with Blazor and .NET 10**
