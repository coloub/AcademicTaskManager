# Quick Start Guide - Academic Task Manager

## Para Desarrolladores

### Ejecutar la Aplicación (Método Rápido)

```powershell
cd "D:\CSE 325 MyProjects\Project\AcademicTaskManager"
.\run-app.ps1
```

La aplicación se abrirá en: **http://localhost:5034**

### Credenciales de Prueba

Para crear un usuario de prueba:

1. Ir a `/Account/Register`
2. Email: `estudiante@academic.com`
3. Password: `Test123!`

## Comandos Útiles

```bash
# Compilar
dotnet build

# Ejecutar
dotnet run

# Crear migración
dotnet ef migrations add NombreMigration

# Aplicar migraciones
dotnet ef database update

# Eliminar BD (desarrollo)
dotnet ef database drop --force

# Recrear BD completa
dotnet ef database drop --force && dotnet ef database update
```

## Estructura de URLs

| Ruta                        | Descripción           |
| --------------------------- | --------------------- |
| `/`                         | Página de inicio      |
| `/Account/Register`         | Registro de usuario   |
| `/Account/Login`            | Inicio de sesión      |
| `/projects`                 | Lista de proyectos    |
| `/projects/create`          | Crear proyecto        |
| `/projects/{id}`            | Detalles del proyecto |
| `/projects/edit/{id}`       | Editar proyecto       |
| `/tasks/create/{projectId}` | Crear tarea           |
| `/tasks/edit/{id}`          | Editar tarea          |

## Archivos Clave

```
Program.cs                    # Configuración de servicios y middleware
appsettings.json              # Configuración (connection string)
Components/Pages/Projects/    # Páginas de proyectos
Components/Pages/Tasks/       # Páginas de tareas
Data/ApplicationDbContext.cs  # Contexto de EF Core
Services/ProjectService.cs    # Lógica de proyectos
Services/TaskService.cs       # Lógica de tareas
```

## Solución de Problemas Rápida

**Error: "no such table: AspNetUsers"**

```bash
dotnet ef database update
```

**Error: Migraciones pendientes**

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

**Resetear BD completa**

```bash
Remove-Item Data\app.db* -Force
dotnet ef database update
```

**La aplicación no inicia**

- Verificar que el puerto 5034 no esté en uso
- Compilar primero: `dotnet build`
- Ver errores: `dotnet run --verbosity detailed`

## Testing Rápido

1. ✅ Registrar usuario
2. ✅ Crear proyecto
3. ✅ Crear 2-3 tareas
4. ✅ Marcar una tarea como completada
5. ✅ Verificar estadísticas
6. ✅ Editar proyecto
7. ✅ Eliminar tarea
8. ✅ Cerrar sesión

## Documentación Completa

- [README.md](README.md) - Vista general
- [IMPLEMENTACION.md](IMPLEMENTACION.md) - Reporte técnico completo
- [USER_GUIDE.md](USER_GUIDE.md) - Guía del usuario
- [DEVELOPER_NOTES.md](DEVELOPER_NOTES.md) - Arquitectura y desarrollo

---

**¿Necesitas más ayuda?** Consulta la documentación completa en los archivos .md
