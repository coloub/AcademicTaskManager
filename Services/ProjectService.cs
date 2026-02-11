using Microsoft.EntityFrameworkCore;
using AcademicTaskManager.Data;

namespace AcademicTaskManager.Services;

/// <summary>
/// Servicio para manejar operaciones CRUD de proyectos
/// </summary>
public class ProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Obtiene todos los proyectos de un usuario específico
    /// </summary>
    public async Task<List<Project>> GetUserProjectsAsync(string userId)
    {
        return await _context.Projects
            .Where(p => p.OwnerUserId == userId)
            .Include(p => p.Tasks)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();
    }

    /// <summary>
    /// Obtiene un proyecto por ID con sus tareas
    /// </summary>
    public async Task<Project?> GetProjectByIdAsync(int projectId)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }

    /// <summary>
    /// Crea un nuevo proyecto
    /// </summary>
    public async Task<Project> CreateProjectAsync(Project project)
    {
        project.CreatedDate = DateTime.Now;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    /// <summary>
    /// Actualiza un proyecto existente
    /// </summary>
    public async Task<bool> UpdateProjectAsync(Project project)
    {
        var existingProject = await _context.Projects.FindAsync(project.Id);
        if (existingProject == null)
            return false;

        existingProject.Title = project.Title;
        existingProject.Description = project.Description;

        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Elimina un proyecto y todas sus tareas asociadas
    /// </summary>
    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Verifica si un usuario es el propietario de un proyecto
    /// </summary>
    public async Task<bool> IsProjectOwnerAsync(int projectId, string userId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        return project?.OwnerUserId == userId;
    }

    /// <summary>
    /// Obtiene estadísticas del proyecto (total de tareas, completadas, etc.)
    /// </summary>
    public async Task<ProjectStatistics> GetProjectStatisticsAsync(int projectId)
    {
        var project = await _context.Projects
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == projectId);

        if (project == null)
            return new ProjectStatistics();

        return new ProjectStatistics
        {
            TotalTasks = project.Tasks.Count,
            CompletedTasks = project.Tasks.Count(t => t.Status == ProjectTaskStatus.Completed),
            PendingTasks = project.Tasks.Count(t => t.Status == ProjectTaskStatus.Pending),
            OverdueTasks = project.Tasks.Count(t => t.Status == ProjectTaskStatus.Pending && t.DueDate < DateTime.Now)
        };
    }
}

/// <summary>
/// Clase para almacenar estadísticas de un proyecto
/// </summary>
public class ProjectStatistics
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int OverdueTasks { get; set; }
    public double CompletionPercentage => TotalTasks > 0 ? (CompletedTasks * 100.0 / TotalTasks) : 0;
}
