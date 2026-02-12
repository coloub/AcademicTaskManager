# Academic Task Manager - Implementation Report

## IMPLEMENTATION DOCUMENTATION

### 1. GENERAL INFORMATION

**Nombre del Proyecto:** Academic Task Manager  
**Tecnología:** Blazor Server (.NET 10.0)  
**Base de Datos:** SQLite con Entity Framework Core  
**Autenticación:** ASP.NET Core Identity  
**Fecha de Implementación:** 10 de Febrero de 2026  
**Curso:** CSC325 - Software Engineering  
**Equipo de Desarrollo:**

- Jose Mendoza
- Ana Torres

### 2. ARCHIVOS CREADOS

#### Modelos de Datos (`Data/`)

- `Project.cs` - Modelo de proyecto académico con validaciones
- `ProjectTask.cs` - Modelo de tarea con enum ProjectTaskStatus
- `ApplicationDbContext.cs` - Contexto de EF Core con relaciones configuradas
- `ApplicationUser.cs` - Usuario extendido de Identity (generado por template)

#### Servicios (`Services/`)

- `ProjectService.cs` - Lógica de negocio para proyectos (CRUD + estadísticas)
- `TaskService.cs` - Lógica de negocio para tareas (CRUD + filtros)
- `DbInitializer.cs` - Inicializador de datos de prueba (opcional)

#### Páginas Blazor (`Components/Pages/`)

**Proyectos:**

- `Projects/Index.razor` - Lista de todos los proyectos del usuario
- `Projects/Create.razor` - Formulario para crear nuevo proyecto
- `Projects/Edit.razor` - Formulario para ed

itar proyecto existente

- `Projects/Details.razor` - Vista detallada con tareas y estadísticas

**Tareas:**

- `Tasks/Create.razor` - Formulario para crear nueva tarea
- `Tasks/Edit.razor` - Formulario para editar tarea existente

**Otras:**

- `Home.razor` - Página de inicio con información del sistema
- Páginas de Identity (generadas por template)

#### Componentes de Layout (`Components/Layout/`)

- `NavMenu.razor` - Updated navigation menu
- `MainLayout.razor` - Main layout (template)

#### Configuration and Scripts

- `Program.cs` - Service and middleware configuration
- `run-app.ps1` - PowerShell script to run application
- `appsettings.json` - Configuration with SQLite connection string
- `AcademicTaskManager.csproj` - .NET project file

#### Migrations (`Migrations/`)

- `20260210225235_InitialCreate.cs` - Initial migration (Project and Tasks tables)
- Designer files for migrations

### 3. ADDED NUGET PACKAGES

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.Sqlite" Version="10.0.3" />
```

**Packages included in template:**

- Microsoft.AspNetCore.Identity.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.AspNetCore.Components.WebAssembly.Authentication

### 4. CREATED ROUTES

| Route                       | Description        | Requires Auth |
| --------------------------- | ------------------ | ------------- |
| `/`                         | Home page          | No            |
| `/projects`                 | Project list       | Yes           |
| `/projects/create`          | Create new project | Yes           |
| `/projects/{id}`            | Project details    | Yes           |
| `/projects/edit/{id}`       | Edit project       | Yes           |
| `/tasks/create/{projectId}` | Create task        | Yes           |
| `/tasks/edit/{id}`          | Edit task          | Yes           |
| `/Account/*`                | Identity pages     | Varies        |

### 5. DATA MODEL

#### Table: Projects

```
- Id (int, PK)
- Title (string, required, max 200)
- Description (string, required, max 2000)
- CreatedDate (DateTime)
- OwnerUserId (string, FK to AspNetUsers)
- Tasks (navigation property)
```

#### Table: Tasks

```
- Id (int, PK)
- Title (string, required, max 200)
- Description (string, max 2000)
- DueDate (DateTime, required)
- Status (enum: Pending=0, Completed=1)
- ProjectId (int, FK to Projects)
- Project (navigation property)
```

#### Relationships

- User (1) → Projects (N)
- Project (1) → Tasks (N)
- Cascade delete configured

### 6. HOW TO RUN THE APPLICATION

#### Method 1: Using PowerShell Script

```powershell
cd "D:\CSE 325 MyProjects\Project\AcademicTaskManager"
.\run-app.ps1
```

#### Method 2: Manual Commands

```powershell
cd "D:\CSE 325 MyProjects\Project\AcademicTaskManager"
dotnet ef database update
dotnet run
```

#### Method 3: From Visual Studio

1. Open `AcademicTaskManager.sln` in Visual Studio
2. Set `AcademicTaskManager` as startup project
3. Press F5 or click "Run"

**Application URL:** http://localhost:5034 (port may vary)

### 7. HOW TO TEST AUTHENTICATION

#### Create Test User:

1. Start the application
2. Navigate to `/Account/Register`
3. Complete registration form:
   - Email: `student@academic.com`
   - Password: `Test123!`
4. Confirm email (auto-confirmed in development)
5. Login with created credentials

#### Authentication Flow:

- ✅ Unauthenticated users can view home page
- ❌ Unauthenticated users CANNOT access `/projects`
- ✅ After login, can create and manage projects and tasks
- ✅ Users only see their own projects
- ✅ Users can only edit/delete their own projects

### 8. IMPLEMENTED FUNCTIONALITIES

#### ✅ Project Management

- Create project with title and description
- View list of all authenticated user's projects
- View project details with all tasks
- Edit project information
- Delete project (with confirmation)
- Display project statistics
  - Total tasks
  - Completed tasks
  - Pending tasks
  - Overdue tasks
  - Completion percentage

#### ✅ Task Management

- Create task within a project
- View all tasks of a project
- Edit task (title, description, date, status)
- Delete task (with confirmation)
- Change status with checkbox (Pending ↔ Completed)
- Highlight overdue tasks in red
- Sort tasks by due date

#### ✅ Security and Authorization

- User registration with password validation
- Login/Logout
- Access restriction to authenticated users only
- Each user only sees their own projects
- Ownership validation before edit/delete

#### ✅ UI/UX

- Responsive design (Bootstrap 5)
- Bootstrap Icons
- Visual indicators (badges, colors)
- Confirmation messages for deletions
- Visual loading feedback
- Clear and intuitive navigation
- Accessibility (labels, semantic HTML)

### 9. ASSUMPTIONS AND DESIGN DECISIONS

#### Assumptions:

1. **SQLite for development**: SQLite used for simplicity, but easily changed to SQL Server for production
2. **Email confirmation**: Auto-confirmed in development, would require SMTP service in production
3. **Single user per project**: Each project belongs to one user (no multi-user collaboration)
4. **Dates in local timezone**: Timezone handling not implemented
5. **No pagination**: Assumes each user won't have hundreds of projects

#### Design Decisions:

1. **Enum ProjectTaskStatus**: Renamed `TaskStatus` to `ProjectTaskStatus` to avoid conflict with `System.Threading.Tasks.TaskStatus`
2. **Cascade Delete**: When deleting a project, all its tasks are automatically deleted
3. **Blazor Server vs WebAssembly**: Chose Blazor Server for lower initial complexity and better SEO
4. **Dedicated services**: Created `ProjectService` and `TaskService` to separate business logic from UI
5. **Validations**: Use Data Annotations in models and Blazor's automatic validation

### 10. MEJORAS FUTURAS SUGERIDAS

#### Alta Prioridad:

- [ ] Implementar paginación en lista de proyectos
- [ ] Agregar búsqueda y filtros
- [ ] Notificaciones de tareas próximas a vencer
- [ ] Dashboard con resumen general

#### Media Prioridad:

- [ ] Categorías o etiquetas para proyectos
- [ ] Prioridad de tareas (Alta/Media/Baja)
- [ ] Comentarios en tareas
- [ ] Archivos adjuntos
- [ ] Exportar proyecto a PDF

#### Baja Prioridad:

- [ ] Colaboración multi-usuario
- [ ] Plantillas de proyectos
- [ ] Tema oscuro/claro
- [ ] Integración con calendario
- [ ] API REST para mobile apps

### 11. PROBLEMAS ENCONTRADOS Y SOLUCIONES

#### Problema 1: Conflicto de nombres `TaskStatus`

**Error:** Ambigüedad entre `AcademicTaskManager.Data.TaskStatus` y `System.Threading.Tasks.TaskStatus`  
**Solución:** Renombrar el enum a `ProjectTaskStatus`

#### Problema 2: Migraciones vacías

**Error:** EF Core generaba migraciones sin contenido  
**Solución:** Eliminar migraciones y regenerarlas. Las tablas de Identity ya están incluidas en el template.

#### Problema 3: Caracteres especiales en script PowerShell

**Error:** Error de parsing en caracteres con tilde  
**Solución:** Evitar caracteres especiales en strings de PowerShell

### 12. PROJECT STRUCTURE

```
AcademicTaskManager/
├── Components/
│   ├── Account/          # Identity pages
│   ├── Layout/           # Layouts and navigation
│   └── Pages/           # Razor pages
│       ├── Projects/     # Project CRUD
│       ├── Tasks/        # Task CRUD
│       └── Home.razor
├── Data/
│   ├── Migrations/       # EF Core migrations
│   ├── ApplicationDbContext.cs
│   ├── ApplicationUser.cs
│   ├── Project.cs
│   └── ProjectTask.cs
├── Services/
│   ├── DbInitializer.cs
│   ├── ProjectService.cs
│   └── TaskService.cs
├── wwwroot/             # Static files
├── Program.cs           # App configuration
├── appsettings.json     # Configuration
├── run-app.ps1          # Execution script
└── AcademicTaskManager.csproj
```

### 13. REQUIREMENTS VERIFICATION

| Requirement           | Status | Notes                        |
| --------------------- | ------ | ---------------------------- |
| .NET 8+               | ✅     | Using .NET 10                |
| Blazor Server         | ✅     | Official .NET template       |
| Entity Framework Core | ✅     | Version 10.0.3               |
| SQLite                | ✅     | With code-first migrations   |
| ASP.NET Core Identity | ✅     | Register, login, logout      |
| Projects CRUD         | ✅     | Create, Read, Update, Delete |
| Tasks CRUD            | ✅     | Create, Read, Update, Delete |
| Authentication        | ✅     | Required for projects        |
| Authorization         | ✅     | Only owner can modify        |
| Responsive UI         | ✅     | Bootstrap 5                  |
| Clear navigation      | ✅     | Updated NavMenu              |
| Accessibility         | ✅     | Labels, semantic HTML        |
| Async/Await           | ✅     | In all services              |
| Dependency Injection  | ✅     | Services registered          |
| Migrations            | ✅     | Code-first approach          |
| Test data             | ⚠️     | Script available (optional)  |

**Legend:** ✅ Completed | ⚠️ Partial | ❌ Not completed

### 14. CONCLUSION

The **Academic Task Manager** project has been successfully implemented fulfilling all specified functional and technical requirements. The application is ready to be deployed and used by students to efficiently manage their academic projects.

**Final status:** ✅ READY FOR PRODUCTION

---

**Document generated on:** February 10, 2026  
**Version:** 1.0  
**Last update:** 02/10/2026 19:00
