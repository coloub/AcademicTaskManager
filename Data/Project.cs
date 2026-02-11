using System.ComponentModel.DataAnnotations;

namespace AcademicTaskManager.Data;

public class Project
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El título es requerido")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "La descripción es requerida")]
    [StringLength(2000, ErrorMessage = "La descripción no puede exceder 2000 caracteres")]
    public string Description { get; set; } = string.Empty;

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public string OwnerUserId { get; set; } = string.Empty;

    public ApplicationUser? Owner { get; set; }
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();
}
