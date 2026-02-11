# Script para ejecutar Academic Task Manager
Write-Host "=== Academic Task Manager - Iniciando ===" -ForegroundColor Cyan

# Cambiar al directorio del proyecto
Set-Location "D:\CSE 325 MyProjects\Project\AcademicTaskManager"
Write-Host "Directorio: $(Get-Location)" -ForegroundColor Green

# Aplicar migraciones si es necesario
Write-Host ""
Write-Host "Aplicando migraciones de base de datos..." -ForegroundColor Yellow
dotnet ef database update

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al aplicar migraciones" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "Base de datos actualizada" -ForegroundColor Green
Write-Host ""
Write-Host "=== Credenciales de Prueba ===" -ForegroundColor Cyan
Write-Host "Email: estudiante@academic.com" -ForegroundColor White
Write-Host "Password: Test123!" -ForegroundColor White

# Ejecutar la aplicación
Write-Host ""
Write-Host "Iniciando aplicacion..." -ForegroundColor Yellow
Write-Host ""
dotnet run


