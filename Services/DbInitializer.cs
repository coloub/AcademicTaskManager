using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AcademicTaskManager.Data;

namespace AcademicTaskManager.Services;

/// <summary>
/// Servicio para inicializar la base de datos con datos de prueba
/// </summary>
public static class DbInitializer
{
    /// <summary>
    /// Inicializa la base de datos con un usuario de prueba y datos de ejemplo
    /// </summary>
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Aplicar migraciones pendientes
        await context.Database.MigrateAsync();

        // Verificar si ya existen datos
        if (await context.Users.AnyAsync())
        {
            return; // La BD ya tiene datos
        }

        // Crear usuario de prueba
        var testUser = new ApplicationUser
        {
            UserName = "student@academic.com",
            Email = "student@academic.com",
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(testUser, "Student123!");

        if (result.Succeeded)
        {
            // Crear proyecto de ejemplo
            var sampleProject = new Project
            {
                Title = "Proyecto Final - Sistemas de Base de Datos",
                Description = "Desarrollo de un sistema de gestión académica con funcionalidades CRUD completas. " +
                             "El proyecto incluye diseño de base de datos, implementación de la lógica de negocio, " +
                             "y creación de una interfaz de usuario moderna usando Blazor.",
                CreatedDate = DateTime.Now.AddDays(-15),
                OwnerUserId = testUser.Id
            };

            context.Projects.Add(sampleProject);
            await context.SaveChangesAsync();

            // Crear tareas de ejemplo
            var sampleTasks = new List<ProjectTask>
            {
                new ProjectTask
                {
                    Title = "Diseñar diagrama ER",
                    Description = "Crear el diagrama entidad-relación completo del sistema, identificando entidades, atributos y relaciones.",
                    DueDate = DateTime.Now.AddDays(-7),
                    Status = ProjectTaskStatus.Completed,
                    ProjectId = sampleProject.Id
                },
                new ProjectTask
                {
                    Title = "Implementar modelos de datos",
                    Description = "Crear las clases de modelo en C# usando EF Core con todas las anotaciones necesarias.",
                    DueDate = DateTime.Now.AddDays(-3),
                    Status = ProjectTaskStatus.Completed,
                    ProjectId = sampleProject.Id
                },
                new ProjectTask
                {
                    Title = "Desarrollar servicios CRUD",
                    Description = "Implementar la lógica de negocio para crear, leer, actualizar y eliminar registros.",
                    DueDate = DateTime.Now.AddDays(2),
                    Status = ProjectTaskStatus.Pending,
                    ProjectId = sampleProject.Id
                },
                new ProjectTask
                {
                    Title = "Crear interfaz de usuario",
                    Description = "Diseñar y desarrollar las páginas Blazor con un diseño responsivo y accesible.",
                    DueDate = DateTime.Now.AddDays(5),
                    Status = ProjectTaskStatus.Pending,
                    ProjectId = sampleProject.Id
                },
                new ProjectTask
                {
                    Title = "Realizar pruebas",
                    Description = "Ejecutar pruebas de funcionalidad completas y corregir bugs encontrados.",
                    DueDate = DateTime.Now.AddDays(10),
                    Status = ProjectTaskStatus.Pending,
                    ProjectId = sampleProject.Id
                },
                new ProjectTask
                {
                    Title = "Documentar el proyecto",
                    Description = "Escribir la documentación técnica y el manual de usuario del sistema.",
                    DueDate = DateTime.Now.AddDays(14),
                    Status = ProjectTaskStatus.Pending,
                    ProjectId = sampleProject.Id
                }
            };

            context.Tasks.AddRange(sampleTasks);
            await context.SaveChangesAsync();

            Console.WriteLine("Base de datos inicializada con datos de prueba.");
            Console.WriteLine("Usuario de prueba creado:");
            Console.WriteLine("  Email: student@academic.com");
            Console.WriteLine("  Contraseña: Student123!");
        }
    }
}
