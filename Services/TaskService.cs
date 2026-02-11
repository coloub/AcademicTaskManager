using Microsoft.EntityFrameworkCore;
using AcademicTaskManager.Data;

namespace AcademicTaskManager.Services;

public class TaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProjectTask>> GetProjectTasksAsync(int projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<ProjectTask?> GetTaskByIdAsync(int taskId)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == taskId);
    }

    public async Task<ProjectTask> CreateTaskAsync(ProjectTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<bool> UpdateTaskAsync(ProjectTask task)
    {
        var existingTask = await _context.Tasks.FindAsync(task.Id);
        if (existingTask == null)
            return false;

        existingTask.Title = task.Title;
        existingTask.Description = task.Description;
        existingTask.DueDate = task.DueDate;
        existingTask.Status = task.Status;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteTaskAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkTaskAsCompletedAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null)
            return false;

        task.Status = ProjectTaskStatus.Completed;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> MarkTaskAsPendingAsync(int taskId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        if (task == null)
            return false;

        task.Status = ProjectTaskStatus.Pending;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> TaskBelongsToProjectAsync(int taskId, int projectId)
    {
        var task = await _context.Tasks.FindAsync(taskId);
        return task?.ProjectId == projectId;
    }

    public async Task<List<ProjectTask>> GetUpcomingTasksAsync(int projectId, int days = 7)
    {
        var deadline = DateTime.Now.AddDays(days);
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId &&
                       t.Status == ProjectTaskStatus.Pending &&
                       t.DueDate <= deadline &&
                       t.DueDate >= DateTime.Now)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }

    public async Task<List<ProjectTask>> GetOverdueTasksAsync(int projectId)
    {
        return await _context.Tasks
            .Where(t => t.ProjectId == projectId &&
                       t.Status == ProjectTaskStatus.Pending &&
                       t.DueDate < DateTime.Now)
            .OrderBy(t => t.DueDate)
            .ToListAsync();
    }
}
