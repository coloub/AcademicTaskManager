# Notas del Desarrollador - Academic Task Manager

## Información del Proyecto

**Proyecto:** Academic Task Manager  
**Stack Tecnológico:** Blazor Server + .NET 10 + Entity Framework Core + SQLite  
**Patrón de Arquitectura:** Service Layer Pattern  
**Autenticación:** ASP.NET Core Identity

---

## Arquitectura de la Aplicación

### Diagrama de Capas

```
┌─────────────────────────────────────┐
│     Presentation Layer              │
│  (Blazor Components & Pages)        │
│  - Home, Projects, Tasks pages      │
│  - Layouts, Navigation              │
└─────────────────────────────────────┘
            ↓ ↑
┌─────────────────────────────────────┐
│     Service Layer                   │
│  (Business Logic)                   │
│  - ProjectService                   │
│  - TaskService                      │
│  - DbInitializer                    │
└─────────────────────────────────────┘
            ↓ ↑
┌─────────────────────────────────────┐
│     Data Access Layer               │
│  (Entity Framework Core)            │
│  - ApplicationDbContext             │
│  - Repositories (built-in EF)       │
└─────────────────────────────────────┘
            ↓ ↑
┌─────────────────────────────────────┐
│     Database Layer                  │
│  (SQLite)                           │
│  - AspNetUsers, Projects, Tasks     │
└─────────────────────────────────────┘
```

### Flujo de Datos

1. **Usuario interactúa** con componente Blazor
2. **Componente llama** método del servicio
3. **Servicio ejecuta** lógica de negocio
4. **Servicio usa** DbContext para acceder a datos
5. **EF Core traduce** a consultas SQL
6. **SQLite ejecuta** consulta y retorna datos
7. **Datos fluyen de vuelta** hasta el componente
8. **Blazor re-renderiza** la UI automáticamente

---

## Estructura de Carpetas Detallada

```
AcademicTaskManager/
│
├── Components/
│   ├── Account/                    # Sistema de autenticación Identity
│   │   ├── Pages/                  # Páginas de Identity (Login, Register, etc.)
│   │   │   ├── Login.razor
│   │   │   ├── Register.razor
│   │   │   ├── ForgotPassword.razor
│   │   │   └── Manage/             # Gestión de cuenta
│   │   └── Shared/                 # Componentes compartidos de Identity
│   │
│   ├── Layout/                     # Layouts de la aplicación
│   │   ├── MainLayout.razor        # Layout principal
│   │   ├── MainLayout.razor.css    # Estilos del layout
│   │   ├── NavMenu.razor           # Menú de navegación
│   │   ├── NavMenu.razor.css       # Estilos del menú
│   │   └── ReconnectModal.razor    # Modal de reconexión
│   │
│   ├── Pages/                      # Páginas de la aplicación
│   │   ├── Projects/               # Módulo de proyectos
│   │   │   ├── Index.razor         # Lista de proyectos
│   │   │   ├── Create.razor        # Crear proyecto
│   │   │   ├── Edit.razor          # Editar proyecto
│   │   │   └── Details.razor       # Detalles del proyecto
│   │   │
│   │   ├── Tasks/                  # Módulo de tareas
│   │   │   ├── Create.razor        # Crear tarea
│   │   │   └── Edit.razor          # Editar tarea
│   │   │
│   │   ├── Home.razor              # Página de inicio
│   │   ├── Error.razor             # Página de error
│   │   └── NotFound.razor          # Página 404
│   │
│   ├── _Imports.razor              # Imports globales
│   ├── App.razor                   # Componente raíz
│   └── Routes.razor                # Configuración de rutas
│
├── Data/                           # Capa de datos
│   ├── Migrations/                 # Historial de migraciones EF
│   │   ├── 20260210225235_InitialCreate.cs
│   │   ├── 20260210225235_InitialCreate.Designer.cs
│   │   └── ApplicationDbContextModelSnapshot.cs
│   │
│   ├── ApplicationDbContext.cs     # Contexto de EF Core
│   ├── ApplicationUser.cs          # Modelo de usuario extendido
│   ├── Project.cs                  # Modelo de proyecto
│   └── ProjectTask.cs              # Modelo de tarea
│
├── Services/                       # Capa de lógica de negocio
│   ├── DbInitializer.cs            # Inicializador de BD
│   ├── ProjectService.cs           # Lógica de proyectos
│   └── TaskService.cs              # Lógica de tareas
│
├── Properties/
│   └── launchSettings.json         # Configuración de inicio
│
├── wwwroot/                        # Archivos estáticos
│   ├── app.css                     # Estilos personalizados
│   ├── bootstrap/                  # Bootstrap CSS/JS
│   └── lib/                        # Librerías de terceros
│
├── appsettings.json                # Configuración de la app
├── appsettings.Development.json    # Config de desarrollo
├── Program.cs                      # Punto de entrada
├── AcademicTaskManager.csproj      # Archivo de proyecto
├── run-app.ps1                     # Script de ejecución
│
└── Documentación/
    ├── IMPLEMENTACION.md           # Reporte de implementación
    ├── USER_GUIDE.md               # Guía del usuario
    └── DEVELOPER_NOTES.md          # Este archivo
```

---

## Modelos de Datos

### Project.cs

```csharp
public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }           // Max 200 chars
    public string Description { get; set; }     // Max 2000 chars
    public DateTime CreatedDate { get; set; }
    public string OwnerUserId { get; set; }     // FK a ApplicationUser

    // Navigation properties
    public ApplicationUser? Owner { get; set; }
    public ICollection<ProjectTask> Tasks { get; set; }
}
```

**Validaciones:**

- Title: Required, MaxLength(200)
- Description: Required, MaxLength(2000)
- OwnerUserId: Required

**Relaciones:**

- N:1 con ApplicationUser (muchos proyectos por usuario)
- 1:N con ProjectTask (un proyecto tiene muchas tareas)

### ProjectTask.cs

```csharp
public class ProjectTask
{
    public int Id { get; set; }
    public string Title { get; set; }           // Max 200 chars
    public string Description { get; set; }     // Max 2000 chars
    public DateTime DueDate { get; set; }
    public ProjectTaskStatus Status { get; set; }
    public int ProjectId { get; set; }          // FK a Project

    // Navigation property
    public Project? Project { get; set; }
}

public enum ProjectTaskStatus
{
    Pending = 0,
    Completed = 1
}
```

**Nota importante:** Se usa `ProjectTaskStatus` en lugar de `TaskStatus` para evitar conflicto con `System.Threading.Tasks.TaskStatus`.

**Validaciones:**

- Title: Required, MaxLength(200)
- Description: MaxLength(2000)
- DueDate: Required
- ProjectId: Required

**Relaciones:**

- N:1 con Project (muchas tareas por proyecto)

### ApplicationDbContext.cs

```csharp
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Project -> User (Cascade delete)
        builder.Entity<Project>()
            .HasOne(p => p.Owner)
            .WithMany()
            .HasForeignKey(p => p.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Task -> Project (Cascade delete)
        builder.Entity<ProjectTask>()
            .HasOne(t => t.Project)
            .WithMany(p => p.Tasks)
            .HasForeignKey(t => t.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Índices para performance
        builder.Entity<Project>()
            .HasIndex(p => p.OwnerUserId);

        builder.Entity<ProjectTask>()
            .HasIndex(t => t.ProjectId);
    }
}
```

**Comportamiento de Cascade Delete:**

- Al eliminar un usuario → se eliminan todos sus proyectos
- Al eliminar un proyecto → se eliminan todas sus tareas

---

## Servicios (Business Logic Layer)

### ProjectService.cs

**Responsabilidades:**

- CRUD completo de proyectos
- Filtrado por usuario
- Cálculo de estadísticas
- Validación de propiedad

**Métodos principales:**

```csharp
Task<List<Project>> GetUserProjectsAsync(string userId)
Task<Project?> GetProjectByIdAsync(int projectId)
Task<Project> CreateProjectAsync(Project project)
Task<bool> UpdateProjectAsync(Project project)
Task<bool> DeleteProjectAsync(int projectId)
Task<bool> IsProjectOwnerAsync(int projectId, string userId)
Task<ProjectStatistics> GetProjectStatisticsAsync(int projectId)
```

**ProjectStatistics:**

```csharp
public class ProjectStatistics
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int OverdueTasks { get; set; }
    public double CompletionPercentage { get; } // Calculado
}
```

### TaskService.cs

**Responsabilidades:**

- CRUD completo de tareas
- Cambio de estado (Pending/Completed)
- Filtrado por proyecto
- Obtención de tareas vencidas y próximas

**Métodos principales:**

```csharp
Task<List<ProjectTask>> GetProjectTasksAsync(int projectId)
Task<ProjectTask?> GetTaskByIdAsync(int taskId)
Task<ProjectTask> CreateTaskAsync(ProjectTask task)
Task<bool> UpdateTaskAsync(ProjectTask task)
Task<bool> DeleteTaskAsync(int taskId)
Task<bool> MarkTaskAsCompletedAsync(int taskId)
Task<bool> MarkTaskAsPendingAsync(int taskId)
Task<bool> TaskBelongsToProjectAsync(int taskId, int projectId)
Task<List<ProjectTask>> GetUpcomingTasksAsync(int projectId, int days = 7)
Task<List<ProjectTask>> GetOverdueTasksAsync(int projectId)
```

**Nota:** Todos los métodos son asíncronos para mejorar la escalabilidad.

---

## Componentes Blazor

### Estructura de un Componente Típico

```razor
@page "/ruta"
@using Namespace
@attribute [Authorize]
@inject Servicio servicio
@inject NavigationManager Navigation
@rendermode InteractiveServer

<PageTitle>Título</PageTitle>

<!-- Markup HTML con Razor syntax -->

@code {
    // Parámetros, propiedades, métodos

    protected override async Task OnInitializedAsync()
    {
        // Inicialización al cargar el componente
    }
}
```

### Ciclo de Vida de Componentes Blazor

1. **Constructor** - Se crea la instancia
2. **SetParametersAsync** - Se establecen los parámetros
3. **OnInitialized / OnInitializedAsync** - Inicialización (se ejecuta UNA vez)
4. **OnParametersSet / OnParametersSetAsync** - Después de cada renderizado
5. **OnAfterRender / OnAfterRenderAsync** - Después de renderizado en el navegador
6. **Dispose** - Limpieza de recursos

### Directivas Importantes

- `@page` - Define la ruta
- `@attribute [Authorize]` - Requiere autenticación
- `@inject` - Inyecta dependencias
- `@rendermode` - Modo de renderizado (InteractiveServer para SignalR)
- `@code` - Bloque de código C#
- `@bind` - Data binding bidireccional
- `@onclick` - Event handler
- `@if` - Condicional
- `@foreach` - Bucle

---

## Dependency Injection

### Registro de Servicios (Program.cs)

```csharp
// Framework services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Identity
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthentication(/* ... */);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Custom services
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskService>();
```

**Lifetimes:**

- `AddScoped` - Una instancia por solicitud (ideal para servicios con DbContext)
- `AddSingleton` - Una instancia para toda la aplicación
- `AddTransient` - Nueva instancia cada vez que se inyecta

### Uso en Componentes

```razor
@inject ProjectService ProjectService
@inject AuthenticationStateProvider AuthenticationStateProvider

@code {
    private async Task CargarDatos()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var userId = authState.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        projects = await ProjectService.GetUserProjectsAsync(userId);
    }
}
```

---

## Autenticación y Autorización

### ASP.NET Core Identity

**Configuración:**

```csharp
builder.Services.AddIdentityCore<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;

    // Política de contraseña
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddSignInManager()
.AddDefaultTokenProviders();
```

### Protección de Rutas

**Método 1: Atributo en componente**

```razor
@attribute [Authorize]
```

**Método 2: Condicional en markup**

```razor
<AuthorizeView>
    <Authorized>
        <!-- Contenido para usuarios autenticados -->
    </Authorized>
    <NotAuthorized>
        <!-- Contenido para usuarios no autenticados -->
    </NotAuthorized>
</AuthorizeView>
```

### Obtener Usuario Actual

```csharp
var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
var user = authState.User;

if (user.Identity?.IsAuthenticated ?? false)
{
    var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var email = user.FindFirst(ClaimTypes.Email)?.Value;
    var username = user.Identity.Name;
}
```

---

## Base de Datos y Migraciones

### SQLite Connection String

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=Data/app.db"
  }
}
```

### Comandos de Entity Framework

**Crear migración:**

```bash
dotnet ef migrations add NombreMigration
```

**Aplicar migraciones:**

```bash
dotnet ef database update
```

**Revertir migración:**

```bash
dotnet ef database update NombreMigracionAnterior
```

**Eliminar última migración:**

```bash
dotnet ef migrations remove
```

**Eliminar base de datos:**

```bash
dotnet ef database drop --force
```

**Recrear base de datos (desarrollo):**

```bash
dotnet ef database drop --force
dotnet ef database update
```

### Mejores Prácticas con Migraciones

1. **Una migración por cambio lógico** - No mezclar cambios no relacionados
2. **Nombres descriptivos** - `AddProjectTaskRelationship` en lugar de `Migration1`
3. **Revisar el código generado** - Asegurarse de que hace lo esperado
4. **No modificar migraciones aplicadas** - Crear una nueva para corregir
5. **Backup antes de aplicar en producción** - Siempre

---

## Blazor Server Specifics

### Render Modes

```razor
@rendermode InteractiveServer
```

**Interactive Server:**

- Usa SignalR para comunicación en tiempo real
- El estado se mantiene en el servidor
- Menor tamaño de descarga
- Requiere conexión constante

**Static:**

- Se renderiza en el servidor, sin interactividad
- Mejor para SEO
- No requiere SignalR

### State Management

**Problema:** En Blazor Server, el estado se pierde si se pierde la conexión SignalR.

**Soluciones:**

1. **Recargar datos en OnInitializedAsync**
2. **Usar servicios Scoped para compartir estado**
3. **Persistent storage** (LocalStorage, SessionStorage)
4. **Circuit handlers** para detectar reconexiones

### Event Handlers

```razor
<!-- Async (recomendado) -->
<button @onclick="HandleClickAsync">Click</button>

<!-- Sync -->
<button @onclick="HandleClick">Click</button>

<!-- Con parámetros -->
<button @onclick="() => HandleClickWithParam(id)">Click</button>

<!-- Prevenir default -->
<form @onsubmit="HandleSubmit" @onsubmit:preventDefault="true">
```

---

## Performance y Optimización

### Consultas EF Core

**✅ Buenas prácticas:**

```csharp
// Usar Include para eager loading
var project = await _context.Projects
    .Include(p => p.Tasks)
    .FirstOrDefaultAsync(p => p.Id == id);

// Proyectar solo campos necesarios
var stats = await _context.Projects
    .Where(p => p.Id == id)
    .Select(p => new {
        TotalTasks = p.Tasks.Count,
        CompletedTasks = p.Tasks.Count(t => t.Status == ProjectTaskStatus.Completed)
    })
    .FirstOrDefaultAsync();

// Usar AsNoTracking para consultas read-only
var projects = await _context.Projects
    .AsNoTracking()
    .ToListAsync();
```

**❌ Evitar:**

```csharp
// N+1 queries
var projects = await _context.Projects.ToListAsync();
foreach (var project in projects)
{
    // Esto hace una query por cada proyecto
    var tasks = await _context.Tasks
        .Where(t => t.ProjectId == project.Id)
        .ToListAsync();
}

// Cargar todo innecesariamente
var allData = await _context.Projects
    .Include(p => p.Tasks)
    .Include(p => p.Owner)
    .ToListAsync();
```

### Blazor Component Optimization

```csharp
// Usar ShouldRender para control fino
protected override bool ShouldRender()
{
    return isDataLoaded; // Solo re-renderizar cuando sea necesario
}

// Virtualization para listas largas
<Virtualize Items="@longList" Context="item">
    <div>@item.Name</div>
</Virtualize>
```

---

## Testing

### Unit Testing de Servicios

```csharp
[Fact]
public async Task CreateProjectAsync_ShouldAddProject()
{
    // Arrange
    var options = new DbContextOptionsBuilder<ApplicationDbContext>()
        .UseInMemoryDatabase(databaseName: "TestDb")
        .Options;

    using var context = new ApplicationDbContext(options);
    var service = new ProjectService(context);

    var project = new Project
    {
        Title = "Test Project",
        Description = "Test Description",
        OwnerUserId = "user123"
    };

    // Act
    var result = await service.CreateProjectAsync(project);

    // Assert
    Assert.NotNull(result);
    Assert.True(result.Id > 0);
    Assert.Equal("Test Project", result.Title);
}
```

### Integration Testing con bUnit

```csharp
[Fact]
public void ProjectsIndex_RendersCorrectly()
{
    // Arrange
    using var ctx = new TestContext();
    ctx.Services.AddScoped<ProjectService>(/* mock */);
    ctx.Services.AddScoped<AuthenticationStateProvider>(/* mock */);

    // Act
    var cut = ctx.RenderComponent<ProjectsIndex>();

    // Assert
    cut.Find("h1").TextContent.Should().Contain("Mis Proyectos");
}
```

---

## Deployment

### Preparación para Producción

**1. Actualizar appsettings.Production.json:**

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=your-server;Database=AcademicTaskManager;..."
  },
  "AllowedHosts": "your-domain.com"
}
```

**2. Cambiar a SQL Server (opcional):**

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

```csharp
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
```

**3. Configurar Email para Identity:**

```csharp
builder.Services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
```

**4. Publicar la aplicación:**

```bash
dotnet publish -c Release -o ./publish
```

### Azure Deployment

**Opción 1: Azure App Service**

1. Crear App Service en Azure Portal
2. Configurar connection string en Application Settings
3. Publicar desde Visual Studio o Azure CLI

**Opción 2: Azure Container Instances**

1. Crear Dockerfile
2. Build image: `docker build -t academictaskmanager .`
3. Push a Azure Container Registry
4. Deploy a Container Instance

### Docker

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["AcademicTaskManager.csproj", "./"]
RUN dotnet restore "AcademicTaskManager.csproj"
COPY . .
RUN dotnet build "AcademicTaskManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AcademicTaskManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcademicTaskManager.dll"]
```

---

## Troubleshooting

### Problemas Comunes

**1. "no such table: AspNetUsers"**

- **Causa:** Migraciones no aplicadas
- **Solución:** `dotnet ef database update`

**2. "TaskStatus is ambiguous"**

- **Causa:** Conflicto con System.Threading.Tasks.TaskStatus
- **Solución:** Usar `ProjectTaskStatus` o `Data.TaskStatus` con namespace completo

**3. SignalR connection errors**

- **Causa:** Configuración de HTTPs incorrecta o firewall
- **Solución:** Verificar configuración de puertos y certificados

**4. "Cannot access a disposed object"**

- **Causa:** DbContext usado fuera de su scope
- **Solución:** Asegurarse de que los servicios son Scoped, no Singleton

**5. Datos no se actualizan en UI**

- **Causa:** Component no se re-renderiza
- **Solución:** Llamar `StateHasChanged()` manualmente si necesario

### Debugging Tips

```csharp
// Logging
_logger.LogInformation("Creating project: {Title}", project.Title);
_logger.LogError(ex, "Error creating project");

// Breakpoints condicionales
// En Visual Studio: Click derecho en breakpoint > Conditions

// Ver SQL generado por EF
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString)
           .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information));
```

---

## Mejoras Futuras Técnicas

### Backend

- [ ] Implementar Repository Pattern
- [ ] Agregar Unit of Work
- [ ] Caching con IMemoryCache o Redis
- [ ] Rate limiting
- [ ] API REST endpoints para mobile app
- [ ] Background jobs con Hangfire
- [ ] Audit logging

### Frontend

- [ ] Progressive Web App (PWA)
- [ ] Offline support
- [ ] Push notifications
- [ ] Internacionalización (i18n)
- [ ] Tema oscuro
- [ ] Drag & drop para reordenar tareas
- [ ] Gráficos con Chart.js

### Seguridad

- [ ] Two-factor authentication (2FA)
- [ ] CAPTCHA en registro
- [ ] Rate limiting en login
- [ ] Content Security Policy headers
- [ ] XSS protection
- [ ] CSRF token validation

### Testing

- [ ] Unit tests completos (>80% coverage)
- [ ] Integration tests
- [ ] End-to-end tests con Playwright
- [ ] Load testing
- [ ] Security testing

---

## Referencias y Recursos

### Documentación Oficial

- [Blazor Documentation](https://learn.microsoft.com/aspnet/core/blazor)
- [Entity Framework Core](https://learn.microsoft.com/ef/core)
- [ASP.NET Core Identity](https://learn.microsoft.com/aspnet/core/security/authentication/identity)

### Libros Recomendados

- "Blazor in Action" - Chris Sainty
- "Entity Framework Core in Action" - Jon P Smith
- "Pro ASP.NET Core Identity" - Adam Freeman

### Comunidades

- [Blazor Discord](https://discord.gg/blazor)
- [Stack Overflow - blazor tag](https://stackoverflow.com/questions/tagged/blazor)
- [Reddit r/Blazor](https://reddit.com/r/Blazor)

---

**Documento creado por:** Equipo de Desarrollo  
**Última actualización:** 10 de Febrero de 2026  
**Versión:** 1.0
