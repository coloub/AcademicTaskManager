# 📚 Academic Task Manager

> Sistema de gestión de proyectos académicos y tareas para estudiantes

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-Server-512BD4)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![Entity Framework](https://img.shields.io/badge/EF%20Core-10.0-512BD4)](https://docs.microsoft.com/ef/)
[![SQLite](https://img.shields.io/badge/SQLite-3-003B57)](https://www.sqlite.org/)

## 🚀 Características Principales

- ✅ **Gestión de Proyectos Académicos** - Crea, edita y elimina proyectos
- ✅ **Control de Tareas** - Administra tareas con fechas límite y estados
- ✅ **Seguimiento de Progreso** - Visualiza estadísticas y porcentaje de completitud
- ✅ **Autenticación Segura** - Sistema de usuarios con ASP.NET Core Identity
- ✅ **Interfaz Responsiva** - Funciona en desktop, tablet y móvil
- ✅ **Tiempo Real** - Actualizaciones instantáneas con SignalR

## 📸 Screenshots

### Página de Inicio

![Home Page](docs/screenshots/home.png)

### Lista de Proyectos

![Projects List](docs/screenshots/projects.png)

### Detalles del Proyecto con Estadísticas

![Project Details](docs/screenshots/details.png)

## 🛠️ Stack Tecnológico

- **Frontend:** Blazor Server (Razor Components)
- **Backend:** ASP.NET Core 10.0
- **Base de Datos:** SQLite con Entity Framework Core
- **Autenticación:** ASP.NET Core Identity
- **UI Framework:** Bootstrap 5
- **Icons:** Bootstrap Icons

## 📋 Requisitos Previos

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) o superior
- Editor de código (recomendado: [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/))
- Navegador web moderno (Chrome, Edge, Firefox)

## 🔧 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone https://github.com/coloub/AcademicTaskManager.git
cd AcademicTaskManager
```

### 2. Restaurar Dependencias

```bash
dotnet restore
```

### 3. Aplicar Migraciones de Base de Datos

```bash
dotnet ef database update
```

### 4. Ejecutar la Aplicación

**Opción A: Usando PowerShell Script**

```powershell
.\run-app.ps1
```

**Opción B: Comando dotnet**

```bash
dotnet run
```

**Opción C: Visual Studio**

- Abrir `Project.sln`
- Presionar F5

### 5. Acceder a la Aplicación

Abrir el navegador en: **http://localhost:5034**

## 👤 Usuario de Prueba

Para probar la aplicación, crea un usuario con:

- **Email:** `estudiante@academic.com`
- **Contraseña:** `Test123!`

O registra tu propio usuario en `/Account/Register`

## 📚 Documentación

- **[Reporte de Implementación](IMPLEMENTACION.md)** - Detalles técnicos completos de la implementación
- **[Guía del Usuario](USER_GUIDE.md)** - Manual de uso de la aplicación
- **[Notas del Desarrollador](DEVELOPER_NOTES.md)** - Arquitectura y guías de desarrollo

## 🏗️ Estructura del Proyecto

```
AcademicTaskManager/
├── Components/
│   ├── Account/          # Sistema de autenticación
│   ├── Layout/           # Layouts y navegación
│   └── Pages/            # Páginas de la aplicación
│       ├── Projects/     # Gestión de proyectos
│       └── Tasks/        # Gestión de tareas
├── Data/
│   ├── ApplicationDbContext.cs
│   ├── Project.cs        # Modelo de proyecto
│   └── ProjectTask.cs    # Modelo de tarea
├── Services/
│   ├── ProjectService.cs # Lógica de negocio de proyectos
│   └── TaskService.cs    # Lógica de negocio de tareas
├── Migrations/           # Migraciones de EF Core
├── wwwroot/              # Archivos estáticos
└── Program.cs            # Configuración de la aplicación
```

## 🧪 Ejecutar Tests

```bash
dotnet test
```

## 🚢 Deployment

### Azure App Service

```bash
# Publicar la aplicación
dotnet publish -c Release -o ./publish

# Desplegar a Azure (requiere Azure CLI)
az webapp up --name tu-app-name --resource-group tu-resource-group
```

### Docker

```bash
# Build image
docker build -t academic-task-manager .

# Run container
docker run -p 8080:80 academic-task-manager
```

Ver [DEVELOPER_NOTES.md](DEVELOPER_NOTES.md) para más opciones de deployment.

## 🤝 Contribuir

Las contribuciones son bienvenidas. Por favor:

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Changelog

### Versión 1.0.0 (10 de Febrero de 2026)

- ✨ Release inicial
- ✅ CRUD completo de proyectos y tareas
- ✅ Sistema de autenticación
- ✅ Estadísticas de proyectos
- ✅ UI responsiva

## 🐛 Reportar Bugs

Si encuentras un bug, por favor abre un [issue](https://github.com/coloub/AcademicTaskManager/issues) con:

- Descripción detallada del problema
- Pasos para reproducir
- Comportamiento esperado vs actual
- Screenshots si aplica
- Información del entorno (OS, navegador, versión .NET)

## 📄 Licencia

Este proyecto es software educativo desarrollado como proyecto académico.

## 👥 Autores

**Proyecto Académico CSC325 - Spring 2026**

- Jose Mendoza - Lead Developer
- Ana Torres - Developer

**Enlaces del Proyecto:**

- GitHub: https://github.com/coloub/AcademicTaskManager
- Trello: https://trello.com/b/6973b0d61f7cded0464bd5e6/academic-task-manager-cse-325

## 🙏 Agradecimientos

- [Blazor](https://blazor.net/) por el framework
- [Bootstrap](https://getbootstrap.com/) por los estilos
- [Bootstrap Icons](https://icons.getbootstrap.com/) por los iconos
- Microsoft por la documentación excelente

## 📞 Soporte

¿Necesitas ayuda?

- 📖 Lee la [Guía del Usuario](USER_GUIDE.md)
- 💻 Consulta las [Notas del Desarrollador](DEVELOPER_NOTES.md)
- 🐛 Reporta un [issue](https://github.com/coloub/AcademicTaskManager/issues)

---

**Desarrollado con ❤️ usando Blazor y .NET**
