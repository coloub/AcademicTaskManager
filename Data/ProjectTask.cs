using System.ComponentModel.DataAnnotations;

namespace AcademicTaskManager.Data;

/// <summary>
/// Representa una tarea dentro de un proyecto
/// </summary>
public class ProjectTask
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es requerido")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    public string Title { get; set; } = string.Empty;

    [StringLength(2000, ErrorMessage = "La descripción no puede exceder 2000 caracteres")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "La fecha de vencimiento es requerida")]
    public DateTime DueDate { get; set; } = DateTime.Now.AddDays(7);

    public ProjectTaskStatus Status { get; set; } = ProjectTaskStatus.Pending;

    [Required]
    public int ProjectId { get; set; }

    // Relación con Project
    public Project? Project { get; set; }
}

/// <summary>
/// Estados posibles para una tarea
/// </summary>
public enum ProjectTaskStatus
{
    Pending = 0,
    Completed = 1
}
