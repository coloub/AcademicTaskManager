# Guía de Deployment - Azure App Service

## 🎓 Requisito Previo: Activar Créditos de Estudiante

1. Ve a: **https://azure.microsoft.com/students**
2. Inicia sesión con tu correo estudiantil (@.edu)
3. Verifica tu identidad de estudiante
4. Recibirás **$100 en créditos** (válidos 12 meses)
5. No requiere tarjeta de crédito ✅

---

## 🚀 Deployment Paso a Paso desde VS Code

### Paso 1: Iniciar Sesión en Azure

1. Abre VS Code
2. Presiona `Ctrl+Shift+P` (Windows) o `Cmd+Shift+P` (Mac)
3. Escribe: **"Azure: Sign In"**
4. Selecciona la opción y sigue el proceso de autenticación
5. ✅ Verás tu suscripción en el panel de Azure

### Paso 2: Crear Azure App Service

#### Opción A: Desde la Barra Lateral de Azure

1. Haz clic en el ícono de **Azure** en la barra lateral izquierda (nube con "A")
2. Expande **"RESOURCES"** o **"APP SERVICE"**
3. Haz clic en el botón **"+"** (Create New Web App)
4. Sigue el asistente:

   **Nombre de la aplicación:**
   - Ejemplo: `academictaskmanager-tu-nombre`
   - Debe ser único globalmente
   - URL final: `https://academictaskmanager-tu-nombre.azurewebsites.net`

   **Runtime Stack:**
   - Selecciona: **.NET 8** o **.NET 9** (lo más cercano disponible a .NET 10)
   - Si no hay .NET 10, elige **.NET 8 (LTS)**

   **Region:**
   - Elige la más cercana: `East US`, `West US 2`, o `Central US`

   **Pricing Tier:**
   - Elige: **Free (F1)** o **Basic (B1)** si quieres mejor rendimiento
   - Free: $0/mes, 60 min/día de CPU
   - Basic B1: ~$13/mes (sale de tus créditos)

5. **Espera** a que se cree (1-2 minutos)

#### Opción B: Desde Command Palette

1. `Ctrl+Shift+P` → **"Azure App Service: Create New Web App... (Advanced)"**
2. Sigue los mismos pasos de arriba

### Paso 3: Configurar Base de Datos (Opcional - para empezar usa SQLite)

#### Opción 1: SQLite (Inicio Rápido - RECOMENDADO)

**Ventaja:** No requieres configuración adicional, funciona de inmediato.

**Nota:** Los datos se reinician si el App Service se recicla (plan Free).

**No necesitas hacer nada,** tu aplicación ya está configurada para usar SQLite por defecto.

#### Opción 2: Azure SQL Database (Producción)

Solo si quieres datos permanentes:

1. En Azure Portal: https://portal.azure.com
2. Busca **"SQL databases"** → **"Create"**
3. Configuración:
   - **Database name:** `academictaskmanager-db`
   - **Server:** Create new
     - Server name: `academictaskmanager-server`
     - Admin login: `azureadmin`
     - Password: (crea una contraseña segura)
   - **Compute + Storage:** Basic (5 DTUs, 2GB) = ~$5/mes
4. Clic **"Review + Create"** → **"Create"**
5. Espera 3-5 minutos
6. Ve a **"Connection strings"** y copia la cadena **ADO.NET**

**Ejemplo:**

```
Server=tcp:academictaskmanager-server.database.windows.net,1433;Initial Catalog=academictaskmanager-db;Persist Security Info=False;User ID=azureadmin;Password={tu_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
```

### Paso 4: Configurar Variables de Entorno (Solo si usas Azure SQL)

1. En VS Code, panel de Azure → **App Services**
2. Haz clic derecho en tu aplicación → **"Open in Portal"**
3. En Azure Portal, busca **"Configuration"** en el menú izquierdo
4. Haz clic en **"+ New application setting"**
5. Agrega:
   - **Name:** `ConnectionStrings__DefaultConnection`
   - **Value:** [Pega la cadena de conexión de SQL]
6. **Guarda** los cambios

### Paso 5: Deploy de la Aplicación 🚀

#### Método 1: Deploy Directo desde VS Code

1. Guarda todos tus archivos
2. En el panel de **Azure**, haz clic derecho en tu **App Service**
3. Selecciona **"Deploy to Web App..."**
4. Confirma:
   - **Folder:** Selecciona la carpeta de tu proyecto
   - **¿Deploy?** → **"Deploy"**
   - **¿Sobrescribir?** → **"Deploy"**
5. ✅ Espera 2-5 minutos (primera vez toma más tiempo)
6. VS Code te notificará cuando termine

#### Método 2: Command Palette

1. `Ctrl+Shift+P`
2. Escribe: **"Azure App Service: Deploy to Web App..."**
3. Selecciona tu aplicación
4. Confirma deploy

### Paso 6: Aplicar Migraciones (Solo Primera Vez)

#### Si usas SQLite:

**No necesitas hacer nada.** La aplicación creará la BD automáticamente.

#### Si usas Azure SQL:

1. En VS Code, abre una **Terminal** (`Ctrl+ñ`)
2. Configura la conexión:

```powershell
$env:ConnectionStrings__DefaultConnection="[TU_CADENA_DE_AZURE_SQL]"
dotnet ef database update
```

**O mejor:** Usa Azure Cloud Shell

1. En Azure Portal → Clic en el ícono **">\_"** (Cloud Shell)
2. Elige **PowerShell**
3. Ejecuta:

```bash
# Instalar EF Core Tools (primera vez)
dotnet tool install --global dotnet-ef

# Clonar tu repo
git clone https://github.com/coloub/AcademicTaskManager.git
cd AcademicTaskManager

# Aplicar migraciones
export ConnectionStrings__DefaultConnection="[TU_CADENA]"
dotnet ef database update
```

### Paso 7: Verificar Deployment ✅

1. En VS Code, panel de Azure → Haz clic derecho en tu app
2. Selecciona **"Browse Website"**
3. Tu navegador abrirá: `https://academictaskmanager-tu-nombre.azurewebsites.net`

**Prueba:**

- ✅ La página carga
- ✅ Puedes registrar una cuenta
- ✅ Puedes crear proyectos
- ✅ Puedes crear tareas

---

## 📊 Monitoreo y Logs

### Ver Logs en Tiempo Real

1. En VS Code → Panel de Azure
2. Haz clic derecho en tu App Service
3. Selecciona **"Start Streaming Logs"**
4. Verás los logs en la terminal

### Ver en Azure Portal

1. Abre tu app en el portal
2. Ve a **"Log stream"** en el menú izquierdo
3. O ve a **"Diagnose and solve problems"**

---

## ⚙️ Configuraciones Adicionales

### Habilitar HTTPS Only

1. Azure Portal → Tu App Service
2. Busca **"TLS/SSL settings"**
3. Activa **"HTTPS Only"** → **ON**

### Custom Domain (Opcional)

1. Compra un dominio (ej: GoDaddy, Namecheap)
2. Azure Portal → **"Custom domains"**
3. Sigue el asistente para agregar tu dominio

### Configurar Auto-Scale (Solo con Basic o Superior)

1. Azure Portal → **"Scale up (App Service plan)"**
2. Cambia a **Standard** o **Premium**
3. Ve a **"Scale out (App Service plan)"**
4. Configura reglas de escalado automático

---

## 🔄 Re-Deploy (Actualizaciones)

Para actualizar tu aplicación:

1. Haz cambios en tu código
2. Guarda todo
3. En VS Code → Panel Azure → Clic derecho en tu app
4. **"Deploy to Web App..."**
5. Confirma sobrescribir

**O desde terminal:**

```powershell
# Commit cambios
git add .
git commit -m "Update application"
git push

# Deploy
# (usa el panel de Azure en VS Code)
```

---

## 🆘 Solución de Problemas

### Error: "Application is unavailable"

**Causa:** La aplicación falló al iniciar

**Solución:**

1. Ve a **"Log stream"** en Azure Portal
2. Busca el error específico
3. Verifica que el runtime (.NET 8/9/10) sea correcto

### Error: "Database connection failed"

**Causa:** Cadena de conexión incorrecta

**Solución:**

1. Verifica en **Configuration** que `ConnectionStrings__DefaultConnection` esté correcta
2. Asegúrate que tiene **doble guion bajo** (`__`)
3. Si usas Azure SQL, verifica que el firewall permita conexiones de Azure

### La aplicación es lenta

**Causa:** Plan Free tiene límites de CPU

**Solución:**

- Upgrade a **Basic B1** (mejor rendimiento)
- O activa **Always On** en Basic/Standard (Configuración → General settings)

### Los datos se pierden al reiniciar (SQLite)

**Esperado en plan Free** con SQLite

**Solución:**

- Upgrade a **Basic** con persistent storage
- O usa **Azure SQL Database**

---

## 💰 Costos Estimados (con créditos de estudiante)

| Servicio    | Plan        | Costo Mensual | Duración con $100 |
| ----------- | ----------- | ------------- | ----------------- |
| App Service | Free (F1)   | $0            | ♾️ Ilimitado      |
| App Service | Basic (B1)  | ~$13          | ~7 meses          |
| Azure SQL   | Basic (2GB) | ~$5           | ~20 meses         |
| **Total**   | Basic + SQL | **~$18/mes**  | **~5 meses**      |

**Recomendación:** Usa **Free App Service** + **SQLite** al principio (gratis). Actualiza solo si necesitas más rendimiento.

---

## 📚 Recursos Adicionales

- **Azure for Students:** https://azure.microsoft.com/students
- **Azure App Service Docs:** https://learn.microsoft.com/azure/app-service/
- **Azure SQL Database:** https://learn.microsoft.com/azure/azure-sql/database/
- **VS Code Azure Extension:** https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-azureappservice

---

## ✅ Checklist Final

- [ ] Créditos de estudiante activados
- [ ] Extensión de Azure instalada en VS Code
- [ ] Sesión iniciada en Azure
- [ ] App Service creado
- [ ] Aplicación deployada exitosamente
- [ ] Sitio web accesible públicamente
- [ ] Puede registrar usuarios
- [ ] Puede crear proyectos y tareas
- [ ] (Opcional) Azure SQL configurado
- [ ] (Opcional) Migraciones aplicadas

---

**🎉 ¡Listo! Tu aplicación está en la nube con Azure.**

Cualquier problema, revisa los logs en **"Log stream"** o pregunta.
