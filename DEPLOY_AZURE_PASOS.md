# 🚀 PASOS EXACTOS PARA DEPLOY EN AZURE - ¡SIGUE ESTO!

## ✅ Pre-requisito: Ya tienes la extensión instalada

---

## PASO 1: INICIAR SESIÓN EN AZURE (2 minutos)

### Opción A: Comando de Teclado (MÁS RÁPIDO)

1. Presiona: **`Ctrl + Shift + P`**
2. Escribe: **`Azure: Sign In`**
3. Presiona **Enter**
4. Se abrirá tu navegador
5. Inicia sesión con tu correo de estudiante
6. Autoriza VS Code
7. ✅ Cierra el navegador cuando diga "You can close this page"

### Opción B: Desde la Barra Lateral

1. Haz clic en el ícono de **Azure** (☁️) en la barra izquierda
2. Haz clic en **"Sign in to Azure..."**
3. Sigue los pasos en el navegador

---

## PASO 2: CREAR TU WEB APP (3 minutos)

### Después de iniciar sesión:

1. En la barra lateral, busca el panel de **RESOURCES** o **APP SERVICE**
2. Haz clic en el icono **"+"** (Create Resource)
3. Selecciona **"Create App Service Web App..."**

### Responde las preguntas:

**Pregunta 1: Nombre de la aplicación**

- Escribe: `academictaskmanager-tunombre` (reemplaza "tunombre")
- Ejemplo: `academictaskmanager-jose`
- Nota: Debe ser único en todo Azure

**Pregunta 2: Select a runtime stack**

- Selecciona: **.NET 8 (LTS)** o la versión más cercana disponible

**Pregunta 3: Select a pricing tier**

- Selecciona: **Free (F1)** → Gratis
- O si tienes créditos y quieres más poder: **Basic (B1)**

**Pregunta 4: Select a location**

- Selecciona: **East US** o la más cercana a ti

**Pregunta 5: Create new resource group?**

- Di **Yes**
- Nombre: `AcademicTaskManager-RG`

⏳ **Espera 1-2 minutos** mientras Azure crea tu aplicación...

✅ Cuando termine, verás tu app en la lista de **APP SERVICE**

---

## PASO 3: DEPLOY TU APLICACIÓN (5 minutos)

### Desde VS Code:

1. En el panel de **Azure** (barra lateral), expande **APP SERVICE**
2. Expande tu **suscripción**
3. Verás tu aplicación: `academictaskmanager-tunombre`
4. **Haz clic derecho** en ella
5. Selecciona **"Deploy to Web App..."**

### Confirmaciones:

**Primera pregunta: "Select the folder to deploy"**

- Selecciona: **`AcademicTaskManager`** (tu carpeta actual)
- Presiona **Enter**

**Segunda pregunta: "Always deploy the workspace..."**

- Selecciona: **"Yes"** (para futuros deploys más rápidos)

**Tercera advertencia: "This will overwrite..."**

- Haz clic en **"Deploy"**

⏳ **Espera 2-5 minutos...**

Verás el progreso en la esquina inferior derecha de VS Code:

- "Building..."
- "Publishing..."
- "Deploying..."

✅ Cuando termine dirá: **"Deployment to academictaskmanager-tunombre completed"**

---

## PASO 4: ABRIR TU SITIO WEB (30 segundos)

### Después del deploy:

1. Aparecerá una notificación en la esquina inferior derecha
2. Haz clic en **"Browse Website"**

### O manualmente:

1. Haz clic derecho en tu app en el panel de Azure
2. Selecciona **"Browse Website"**

### Tu URL será:

```
https://academictaskmanager-tunombre.azurewebsites.net
```

---

## PASO 5: VERIFICAR QUE TODO FUNCIONE ✅

En tu navegador:

- [ ] La página carga correctamente
- [ ] Haz clic en **"Register"**
- [ ] Crea una cuenta de prueba:
  - Email: `test@academic.com`
  - Password: `Test123!`
- [ ] Inicia sesión
- [ ] Crea un proyecto de prueba
- [ ] Crea una tarea en ese proyecto

✅ **Si todo funciona, ¡LISTO! Tu app está en línea.**

---

## 🆘 SI TIENES PROBLEMAS:

### Ver logs en tiempo real:

1. Haz clic derecho en tu app (panel de Azure)
2. Selecciona **"Start Streaming Logs"**
3. Los logs aparecerán en la terminal de VS Code

### O ve al portal web:

1. Haz clic derecho en tu app
2. Selecciona **"Open in Portal"**
3. En el portal, ve a **"Log stream"**

---

## 🔄 PARA ACTUALIZAR TU APP DESPUÉS:

Cuando hagas cambios en tu código:

1. Guarda todos los archivos
2. Haz clic derecho en tu app (panel de Azure)
3. Selecciona **"Deploy to Web App..."**
4. Confirma **"Deploy"**
5. ✅ Listo en 2-3 minutos

---

## 💡 TIPS IMPORTANTES:

- **Primera carga lenta:** La primera vez que alguien accede puede tardar 10-20 segundos (plan Free)
- **Los datos persisten:** Con SQLite, los datos se guardan pero pueden perderse si Azure reinicia el servidor (plan Free)
- **Siempre disponible:** Tu sitio está 24/7 en línea
- **HTTPS automático:** Azure te da HTTPS gratis

---

## 📊 MONITOREO:

Para ver cuántos créditos usas:

1. Ve a: https://portal.azure.com
2. Busca **"Cost Management + Billing"**
3. Ve a **"Cost analysis"**
4. Con plan **Free** = **$0/mes** ✅

---

## ¿NECESITAS AYUDA?

Si ves algún error o algo no funciona:

1. **Copia el mensaje de error exacto**
2. Ve a los logs (**"Start Streaming Logs"**)
3. Busca líneas rojas con "ERROR"
4. **Dime qué dice el error**

---

**🎉 ¡Éxito! Tu aplicación está en Azure.**

Ahora puedes compartir tu URL con cualquiera:
`https://academictaskmanager-tunombre.azurewebsites.net`
