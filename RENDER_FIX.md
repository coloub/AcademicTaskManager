# 🔧 SOLUCIÓN RÁPIDA - Errores de Deployment en Render

## Problema Actual

Tu aplicación desplegó en Render pero falla con estos errores:

1. ❌ `Format of the initialization string does not conform to specification`
2. ❌ `relation "AspNetUsers" does not exist`
3. ⚠️ `Cannot load library libgssapi_krb5.so.2`

## ✅ Solución Aplicada

Se han realizado los siguientes cambios en el código:

### 1. Auto-Migration al Iniciar (Program.cs)

- ✅ Las migraciones ahora se aplican automáticamente en producción
- ✅ No necesitas ejecutar `dotnet ef database update` manualmente
- ✅ La base de datos se crea al arrancar la aplicación

### 2. Librerías PostgreSQL (Dockerfile)

- ✅ Se agregó `libgssapi-krb5-2` al contenedor Docker
- ✅ Elimina el error "Cannot load library"

### 3. Validación de Connection String (Program.cs)

- ✅ Mensaje de error claro si falta la variable de entorno
- ✅ Indica exactamente qué variable configurar

## 📋 Pasos para Resolver (Render Dashboard)

### Paso 1: Verifica la Variable de Entorno

1. Ve a **Render Dashboard** → Tu Web Service → **"Environment"**
2. Busca esta variable:

   ```
   ConnectionStrings__DefaultConnection
   ```

3. **SI NO EXISTE o ESTÁ VACÍA:**
   - Click **"Add Environment Variable"**
   - **Key:** `ConnectionStrings__DefaultConnection`
   - **Value:** Copia el **Internal Database URL** de tu PostgreSQL

   Ejemplo:

   ```
   postgresql://academic_user:Yy8sNNhfEr5x...@dpg-xxx.oregon-postgres.render.com/academic_db
   ```

4. Click **"Save Changes"**

### Paso 2: Obtener el PostgreSQL Connection String

1. Ve a **Render Dashboard** → Tu PostgreSQL Database
2. Busca **"Connections"** o **"Internal Database URL"**
3. Copia el valor completo que empieza con `postgresql://`
4. Pégalo en la variable de entorno del Paso 1

### Paso 3: Re-desplegar

Opción A - **Despliegue Automático:**

1. Haz `git push` de los cambios del código
2. Render detectará automáticamente y redesplegará

Opción B - **Despliegue Manual:**

1. Ve a tu Web Service en Render
2. Click **"Manual Deploy"** → **"Clear build cache & deploy"**

### Paso 4: Verificar Logs

1. Ve a **Render Dashboard** → Tu Web Service → **"Logs"**
2. Busca estos mensajes de éxito:

   ```
   ✅ Applying database migrations...
   ✅ Database migrations applied successfully.
   ✅ Now listening on: http://0.0.0.0:10000
   ✅ Application started.
   ```

3. Si ves errores, verifica:
   - Connection string correcto
   - Base de datos PostgreSQL activa
   - Variables de entorno guardadas

## ⚡ Comandos Git para Actualizar

```bash
# Desde tu carpeta del proyecto
git add .
git commit -m "fix: Add auto-migration and PostgreSQL libs for Render deployment"
git push origin main
```

## 🎯 Checklist Final

- [ ] Variable `ConnectionStrings__DefaultConnection` configurada en Render
- [ ] Valor contiene el Internal Database URL de PostgreSQL
- [ ] Código actualizado pusheado a GitHub
- [ ] Render ha redesplegado (check "Events" tab)
- [ ] Logs muestran "Database migrations applied successfully"
- [ ] Aplicación accesible en https://academictaskmanager.onrender.com
- [ ] Puedes registrar nuevo usuario y crear proyectos

## 🆘 Si Aún Falla

### Error: "User does not have CONNECT privilege"

**Solución:** El usuario de la base de datos necesita permisos. En Render Shell de PostgreSQL:

```sql
GRANT ALL PRIVILEGES ON DATABASE academic_db TO academic_user;
```

### Error: "Timeout reading from connection"

**Solución:**

- Verifica que el Internal Database URL (no External)
- Debe estar en el formato: `postgresql://user:pass@dpg-xxx.region-postgres.render.com/dbname`

### Aplicación inicia pero da error al cargar

**Solución:**

1. Ve a Render Web Service → **"Shell"**
2. Ejecuta manualmente:
   ```bash
   dotnet ef database update --no-build
   ```

## 📞 Información de Soporte

Si los errores persisten, comparte en los logs:

1. Últimas 50 líneas del log de despliegue
2. Screenshot de tus Environment Variables (oculta las contraseñas)
3. El error exacto que aparece

---

**Última actualización:** Febrero 2026  
**Archivos modificados:** Program.cs, Dockerfile, README.md
