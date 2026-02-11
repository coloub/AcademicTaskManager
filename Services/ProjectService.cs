using Microsoft.EntityFrameworkCore;
using AcademicTaskManager.Data;

namespace AcademicTaskManager.Services;

public class ProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Project>> GetUserProjectsAsync(string userId)
    {
        return await _context.Projects
            .Where(p => p.OwnerUserId == userId)
            .Include(p => p.Tasks)
            .OrderByDescending(p => p.CreatedDate)
            .ToListAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int projectId)
    {
        return await _context.Projects
            .Include(p => p.Tasks)
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == projectId);
    }

    public async Task<Project> CreateProjectAsync(Project project)
    {
        project.CreatedDate = DateTime.Now;
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

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

    public async Task<bool> DeleteProjectAsync(int projectId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        if (project == null)
            return false;

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> IsProjectOwnerAsync(int projectId, string userId)
    {
        var project = await _context.Projects.FindAsync(projectId);
        return project?.OwnerUserId == userId;
    }

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

public class ProjectStatistics
{
    public int TotalTasks { get; set; }
    public int CompletedTasks { get; set; }
    public int PendingTasks { get; set; }
    public int OverdueTasks { get; set; }
    public double CompletionPercentage => TotalTasks > 0 ? (CompletedTasks * 100.0 / TotalTasks) : 0;
}
