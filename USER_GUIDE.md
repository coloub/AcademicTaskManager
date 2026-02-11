# Guía del Usuario - Academic Task Manager

## Bienvenido a Academic Task Manager

Academic Task Manager es una aplicación diseñada para ayudar a los estudiantes a organizar sus proyectos académicos y las tareas asociadas de manera eficiente.

## Tabla de Contenidos

1. [Primeros Pasos](#primeros-pasos)
2. [Registro e Inicio de Sesión](#registro-e-inicio-de-sesión)
3. [Gestión de Proyectos](#gestión-de-proyectos)
4. [Gestión de Tareas](#gestión-de-tareas)
5. [Preguntas Frecuentes](#preguntas-frecuentes)

---

## Primeros Pasos

### Acceder a la Aplicación

1. Abra su navegador web
2. Navegue a la URL de la aplicación (ej: `http://localhost:5034`)
3. Ver á la página de inicio con información sobre el sistema

### Página de Inicio

La página de inicio muestra:

- **Descripción del sistema** - Qué puede hacer con Academic Task Manager
- **Características principales** - Gestión de proyectos, control de tareas, seguimiento visual
- **Botones de acción** - Crear cuenta o Iniciar sesión

---

## Registro e Inicio de Sesión

### Crear una Nueva Cuenta

1. **Haga clic en "Crear Cuenta"** en la página de inicio
2. **Complete el formulario de registro:**
   - Email: Su dirección de correo electrónico
   - Contraseña: Mínimo 6 caracteres, debe incluir:
     - Al menos una letra mayúscula
     - Al menos una letra minúscula
     - Al menos un número
     - Al menos un carácter especial
3. **Confirme su contraseña**
4. **Haga clic en "Registrar"**
5. En modo desarrollo, su cuenta se confirma automáticamente

**Ejemplo de contraseña válida:** `Test123!`

### Iniciar Sesión

1. **Haga clic en "Iniciar Sesión"** en la página de inicio
2. **Ingrese sus credenciales:**
   - Email
   - Contraseña
3. **Opcionalmente**, marque "Recordarme" para mantener la sesión activa
4. **Haga clic en "Iniciar Sesión"**

### Cerrar Sesión

1. Haga clic en su nombre de usuario en el menú de navegación
2. Seleccione "Cerrar Sesión"

---

## Gestión de Proyectos

### Ver Todos sus Proyectos

1. **Después de iniciar sesión**, haga clic en "Mis Proyectos" en el menú de navegación
2. Verá una lista de tarjetas con todos sus proyectos
3. Cada tarjeta muestra:
   - Título del proyecto
   - Descripción (primeros 100 caracteres)
   - Fecha de creación
   - Número de tareas totales y completadas

### Crear un Nuevo Proyecto

1. **En la página "Mis Proyectos"**, haga clic en "Nuevo Proyecto"
2. **Complete el formulario:**
   - **Título** (obligatorio): Nombre descriptivo del proyecto
     - Máximo 200 caracteres
     - Ejemplo: "Proyecto Final de Bases de Datos"
   - **Descripción** (obligatoria): Detalles del proyecto
     - Máximo 2000 caracteres
     - Incluya objetivos, requisitos, alcance
3. **Haga clic en "Crear Proyecto"**
4. Será redirigido automáticamente a la lista de proyectos

**Consejos:**

- Sea específico en el título para identificar fácilmente el proyecto
- En la descripción, incluya toda la información relevante que necesitará consultar

### Ver Detalles de un Proyecto

1. **En la lista de proyectos**, haga clic en el botón "Ver" de cualquier proyecto
2. La pantalla de detalles muestra:

   **Información del Proyecto:**
   - Título completo
   - Descripción completa
   - Fecha de creación

   **Estadísticas en Tarjetas de Colores:**
   - 🔵 **Total de Tareas** - Número total de tareas
   - 🟢 **Completadas** - Tareas finalizadas
   - 🟡 **Pendientes** - Tareas por hacer
   - 🔴 **Vencidas** - Tareas pendientes cuya fecha límite ya pasó

   **Barra de Progreso:**
   - Muestra visualmente el porcentaje de completitud
   - Indica cuántas tareas se han completado del total

   **Lista de Tareas:**
   - Tabla con todas las tareas del proyecto
   - Columnas: Checkbox, Título, Fecha Límite, Estado, Acciones
   - Las tareas vencidas se resaltan en rojo

### Editar un Proyecto

1. **Desde la lista de proyectos**: Haga clic en el botón de lápiz (Editar)

   **O**

   **Desde los detalles del proyecto**: Haga clic en "Editar"

2. **Modifique** el título y/o la descripción
3. **Haga clic en "Guardar Cambios"**
4. Verá un mensaje de confirmación

**Nota:** Solo puede editar sus propios proyectos

### Eliminar un Proyecto

⚠️ **ADVERTENCIA:** Esta acción eliminará también TODAS las tareas del proyecto y NO se puede deshacer.

1. **En la lista de proyectos**, haga clic en el botón de papelera (Eliminar)
2. **Aparecerá un cuadro de confirmación**:
   - Revise el nombre del proyecto
   - Lea la advertencia
3. **Haga clic en "Eliminar"** para confirmar

   **O**

   **Haga clic en "Cancelar"** para abortar la operación

---

## Gestión de Tareas

### Crear una Nueva Tarea

1. **Navegue a los detalles del proyecto** donde desea crear la tarea
2. **Haga clic en "Nueva Tarea"**
3. **Complete el formulario:**
   - **Título** (obligatorio): Nombre de la tarea
     - Máximo 200 caracteres
     - Ejemplo: "Diseñar diagrama ER"
   - **Descripción** (opcional): Detalles adicionales
     - Máximo 2000 caracteres
   - **Fecha de Vencimiento** (obligatoria): Cuándo debe estar lista
     - Use el selector de fecha
     - Por defecto: 7 días desde hoy
   - **Estado**: Pendiente o Completada
     - Por defecto: Pendiente
4. **Haga clic en "Crear Tarea"**
5. Volverá a la página de detalles del proyecto con la nueva tarea

**Ejemplo de Tarea:**

```
Título: Implementar modelos de datos
Descripción: Crear las clases de modelo en C# usando EF Core con todas las anotaciones necesarias.
Fecha: 15/02/2026
Estado: Pendiente
```

### Marcar una Tarea como Completada/Pendiente

En la tabla de tareas del proyecto:

1. **Haga clic en el checkbox** al inicio de la fila
2. El estado cambiará automáticamente:
   - ✅ **Marcado** = Completada (badge verde)
   - ☐ **Desmarcado** = Pendiente (badge amarillo)
3. Las estadísticas del proyecto se actualizarán automáticamente

**Método rápido para cambiar múltiples tareas:**

- Simplemente haga clic en cada checkbox que desee cambiar
- Los cambios se aplican inmediatamente

### Editar una Tarea

1. **En la tabla de tareas**, haga clic en el botón de lápiz (Editar)
2. **Modifique** cualquier campo:
   - Título
   - Descripción
   - Fecha de vencimiento
   - Estado
3. **Haga clic en "Guardar Cambios"**
4. **O haga clic en "Volver al Proyecto"** para cancelar

### Eliminar una Tarea

1. **En la tabla de tareas**, haga clic en el botón de papelera (Eliminar)
2. **Aparecerá un cuadro de confirmación**
3. **Haga clic en "Eliminar"** para confirmar

   **O**

   **Haga clic en "Cancelar"** para abortar

### Interpretar Tareas Vencidas

Las tareas vencidas se identifican por:

- **Fila con fondo rojo** en la tabla
- **Texto en rojo** en la fecha
- **Ícono de advertencia** ⚠️ con "Vencida"

**Qué hacer con tareas vencidas:**

1. Si ya la completó: Marque el checkbox para cambiar a Completada
2. Si aún está pendiente: Considere actualizar la fecha límite
3. Priorice estas tareas en su trabajo

---

## Preguntas Frecuentes

### ¿Puedo compartir un proyecto con otros usuarios?

**No.** Actualmente, cada proyecto pertenece a un solo usuario. Esta funcionalidad podría agregarse en futuras versiones.

### ¿Cuántos proyectos puedo crear?

**Sin límite.** Puede crear tantos proyectos como necesite para organizar su trabajo académico.

### ¿Qué pasa si olvido mi contraseña?

Haga clic en "¿Olvidó su contraseña?" en la página de inicio de sesión y siga las instrucciones (requiere configuración de email en producción).

### ¿Puedo cambiar mi email o contraseña?

Sí. Haga clic en su nombre en el menú de navegación para acceder a la gestión de cuenta.

### ¿Los datos se guardan automáticamente?

Sí. Todos los cambios se guardan inmediatamente en la base de datos cuando hace clic en botones como "Crear", "Guardar", o los checkboxes.

### ¿Puedo usar la aplicación en mi teléfono?

Sí. La aplicación es totalmente responsiva y funciona en dispositivos móviles, tablets y computadoras de escritorio.

### ¿Qué navegadores son compatibles?

La aplicación funciona en:

- Chrome (recomendado)
- Edge
- Firefox
- Safari
- Opera

### ¿Puedo exportar mis proyectos?

Esta funcionalidad no está disponible actualmente, pero está planeada para futuras versiones.

### ¿Cómo organizo mejor mis proyectos?

**Mejores prácticas:**

1. Use un proyecto por asignatura o trabajo importante
2. Divida el proyecto en tareas pequeñas y manejables
3. sea realista con las fechas límite
4. Revise sus proyectos regularmente
5. Marque las tareas como completadas inmediatamente
6. Use la descripción para documentar detalles importantes

### ¿Qué hago si encuentro un error?

Si encuentra un problema:

1. Refresque la página (F5)
2. Intente cerrar sesión e iniciar sesión nuevamente
3. Contacte al soporte técnico con detalles del error

---

## Consejos para Usar Academic Task Manager Efectivamente

### 📝 Planificación de Proyectos

- **Cree el proyecto tan pronto como reciba la asignación**
- **Lea todos los requisitos** antes de crear tareas
- **Divida trabajos grandes** en tareas pequeñas (2-4 horas cada una)

### 📅 Gestión de Fechas

- **Agregue buffer de tiempo** - No ponga la fecha exacta de entrega
- **Revise tareas próximas** regularmente (cada 2-3 días)
- **Priorice tareas vencidas** inmediatamente

### ✅ Seguimiento de Progreso

- **Marque tareas completadas** inmediatamente
- **Revise el porcentaje de completitud** para mantener el ritmo
- **Celebre pequeños logros** cuando complete tareas

### 📊 Uso de Estadísticas

- **Use la barra de progreso** como motivación
- **Atienda las tareas vencidas** mostradas en rojo
- **Mantenga un balance** entre pendientes y completadas

---

## Soporte y Ayuda

¿Necesita más ayuda?

- Consulte la documentación técnica en `IMPLEMENTACION.md`
- Consulte las notas del desarrollador en `DEVELOPER_NOTES.md`
- Contacte al administrador del sistema

---

**Versión del documento:** 1.0  
**Fecha:** 10 de Febrero de 2026  
**Aplicación:** Academic Task Manager v1.0
