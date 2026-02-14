using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AcademicTaskManager.Components;
using AcademicTaskManager.Components.Account;
using AcademicTaskManager.Data;
using AcademicTaskManager.Services;
using ASP.D_.CSE_325_MyProjects.Project.AcademicTaskManager.Components;

var builder = WebApplication.CreateBuilder(args);

// Configure web host for cloud deployment (Render, Azure, etc.)
// Azure App Service configures ports automatically
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
{
    // Render.com or similar services that use PORT env variable
    var isDevelopmentEnv = builder.Environment.IsDevelopment();
    var host = isDevelopmentEnv ? "localhost" : "0.0.0.0";
    builder.WebHost.UseUrls($"http://{host}:{port}");
}
// Azure App Service: No need to set URL, it's configured automatically

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Use SQLite as default if no connection string is provided (Azure App Service default)
if (string.IsNullOrWhiteSpace(connectionString))
{
    connectionString = "DataSource=Data/app.db;Cache=Shared";
    builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;
}

// Detect database provider based on environment or connection string
var isDevelopment = builder.Environment.IsDevelopment();
var usePostgreSql = connectionString.Contains("Host=") || connectionString.StartsWith("postgresql://") || connectionString.Contains("Username=");
var useSqlServer = connectionString.Contains("Server=") && !connectionString.Contains("Host=");

if (usePostgreSql)
{
    // Production: PostgreSQL (Render managed database)
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else if (useSqlServer)
{
    // Production: SQL Server (alternative)
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString));
}
else
{
    // Development: SQLite (default)
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(connectionString));
}

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<TaskService>();

var app = builder.Build();

// Auto-apply migrations in production (Azure, Render, etc.)
if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            var logger = services.GetRequiredService<ILogger<Program>>();

            // Ensure Data directory exists for SQLite
            var dataDir = Path.Combine(app.Environment.ContentRootPath, "Data");
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
                logger.LogInformation("Created Data directory for SQLite database.");
            }

            logger.LogInformation("Applying database migrations...");
            context.Database.Migrate();
            logger.LogInformation("Database migrations applied successfully.");
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
            throw;
        }
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapAdditionalIdentityEndpoints();

app.Run();
