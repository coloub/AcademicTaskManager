# Deployment Guide - Render.com

This guide provides step-by-step instructions for deploying Academic Task Manager to Render.com.

## Prerequisites

- GitHub account with repository access
- Render.com account (free tier available)
- Repository pushed to GitHub

## Deployment Options

### Option A: SQLite with Persistent Disk (Simple)

Best for: Low-traffic academic projects, single-instance deployments

### Option B: PostgreSQL Managed Database (Recommended for Production)

Best for: Production use, multiple instances, better scalability

---

## Option A: Deployment with SQLite

### Step 1: Create Render Account

1. Navigate to [https://render.com](https://render.com)
2. Click "Get Started" or "Sign Up"
3. Sign up using GitHub (recommended) or email

### Step 2: Connect GitHub Repository

1. From Render Dashboard, click "New +"
2. Select "Web Service"
3. Click "Connect GitHub" if not already connected
4. Authorize Render to access your repositories
5. Select `AcademicTaskManager` repository

### Step 3: Configure Web Service

**Basic Settings:**

- **Name:** `academic-task-manager` (or your preferred name)
- **Region:** Choose closest to your users
- **Branch:** `main`
- **Root Directory:** Leave empty
- **Runtime:** Select `.NET`

**Build & Deploy:**

- **Build Command:**

  ```bash
  dotnet publish -c Release -o out
  ```

- **Start Command:**
  ```bash
  dotnet out/AcademicTaskManager.dll
  ```

**Environment:**

- **Environment:** `.NET 10`

### Step 4: Add Persistent Disk (Required for SQLite)

1. Scroll to "Disk" section
2. Click "Add Disk"
3. **Name:** `data`
4. **Mount Path:** `/var/data`
5. **Size:** 1 GB (free tier)

### Step 5: Configure Environment Variables

Click "Add Environment Variable" for each:

| Key                                    | Value                                      |
| -------------------------------------- | ------------------------------------------ |
| `ASPNETCORE_ENVIRONMENT`               | `Production`                               |
| `ConnectionStrings__DefaultConnection` | `DataSource=/var/data/app.db;Cache=Shared` |
| `ASPNETCORE_URLS`                      | `http://0.0.0.0:5000`                      |

**Note:** Double underscore (`__`) separates nested configuration keys.

### Step 6: Deploy

1. Click "Create Web Service"
2. Render will automatically:
   - Clone your repository
   - Run the build command
   - Start the application
3. Wait for deployment to complete (2-5 minutes)

### Step 7: Run Database Migrations

**Important:** After first deployment, you must initialize the database.

**Method 1: Using Render Shell**

1. Go to your service dashboard
2. Click "Shell" tab
3. Run:
   ```bash
   cd out
   dotnet ef database update --project ../AcademicTaskManager.csproj
   ```

**Method 2: Manual SQL (if EF tools not available)**

1. Download the migration SQL scripts locally
2. Execute through Render Shell using sqlite3

### Step 8: Verify Deployment

1. Click the generated URL (e.g., `https://academic-task-manager.onrender.com`)
2. You should see the home page
3. Test user registration and login
4. Create a test project and task

---

## Option B: Deployment with PostgreSQL (Recommended)

### Step 1-3: Same as Option A

Follow Steps 1-3 from Option A above.

### Step 4: Create PostgreSQL Database

**Before creating the Web Service:**

1. From Render Dashboard, click "New +"
2. Select "PostgreSQL"
3. Configure:
   - **Name:** `academic-task-manager-db`
   - **Database:** `academictaskmanager`
   - **User:** `academictaskmanager` (auto-generated)
   - **Region:** Same as your web service
   - **PostgreSQL Version:** 16
   - **Plan:** Free (or paid for production)
4. Click "Create Database"
5. Wait for database provisioning (~2 minutes)
6. **Copy the Internal Database URL** (starts with `postgresql://`)

### Step 5: Configure Web Service with PostgreSQL

**Build & Deploy:** (Same as Option A)

- **Build Command:** `dotnet publish -c Release -o out`
- **Start Command:** `dotnet out/AcademicTaskManager.dll`

**Environment Variables:**

| Key                                    | Value                           |
| -------------------------------------- | ------------------------------- |
| `ASPNETCORE_ENVIRONMENT`               | `Production`                    |
| `ConnectionStrings__DefaultConnection` | `[PASTE_INTERNAL_DATABASE_URL]` |
| `ASPNETCORE_URLS`                      | `http://0.0.0.0:5000`           |

**Replace `[PASTE_INTERNAL_DATABASE_URL]` with your actual PostgreSQL connection string.**

### Step 6: Deploy

Click "Create Web Service" and wait for deployment.

### Step 7: Run Database Migrations

**Using Render Shell:**

1. Go to your web service dashboard
2. Click "Shell" tab
3. Run:
   ```bash
   cd out
   dotnet ef database update --project ../AcademicTaskManager.csproj
   ```

**Alternative: Local Migration Script**

```bash
# From your local machine
dotnet ef migrations script -o migration.sql
# Then execute migration.sql through PostgreSQL client or Render dashboard
```

### Step 8: Verify Deployment

Same as Option A, Step 8.

---

## Post-Deployment Configuration

### Custom Domain (Optional)

1. In your Render service, go to "Settings"
2. Scroll to "Custom Domain"
3. Click "Add Custom Domain"
4. Follow DNS configuration instructions

### HTTPS

Render automatically provides free SSL certificates via Let's Encrypt. No additional configuration needed.

### Health Checks

Render automatically monitors your service. Configure custom health checks:

1. Go to "Settings"
2. Scroll to "Health Check Path"
3. Set to `/` or create a dedicated health endpoint

---

## Troubleshooting

### Issue: "Connection string not found"

**Solution:** Verify environment variable is named exactly:

```
ConnectionStrings__DefaultConnection
```

(Double underscore, no spaces)

### Issue: "No such table" errors

**Solution:** Database migrations not applied. Run:

```bash
dotnet ef database update
```

from Render Shell.

### Issue: Application crashes on startup

**Solution:** Check logs:

1. Go to your service dashboard
2. Click "Logs" tab
3. Look for error messages
4. Common causes:
   - Missing environment variables
   - Database connection failure
   - Port binding issues (ensure `ASPNETCORE_URLS=http://0.0.0.0:5000`)

### Issue: SQLite "database is locked"

**Solution:** SQLite doesn't scale well with multiple instances. Either:

- Use PostgreSQL instead
- Ensure only 1 instance is running
- Add connection string parameter: `Cache=Shared;Mode=ReadWriteCreate`

### Issue: 502 Bad Gateway

**Solution:**

- Check if application is binding to correct port (5000)
- Verify Start Command is correct
- Check application logs for startup errors

---

## Environment Variables Reference

### Required

| Variable                               | Description         | Example                                         |
| -------------------------------------- | ------------------- | ----------------------------------------------- |
| `ConnectionStrings__DefaultConnection` | Database connection | `DataSource=/var/data/app.db` or PostgreSQL URL |
| `ASPNETCORE_ENVIRONMENT`               | Runtime environment | `Production`                                    |
| `ASPNETCORE_URLS`                      | Bind address        | `http://0.0.0.0:5000`                           |

### Optional

| Variable                     | Description   | Default       |
| ---------------------------- | ------------- | ------------- |
| `Logging__LogLevel__Default` | Logging level | `Information` |
| `AllowedHosts`               | CORS hosts    | `*`           |

---

## Database Backup

### SQLite (Disk-based)

1. In Render dashboard, go to your service
2. Click "Disk" tab
3. Download disk snapshot periodically
4. Store backups securely off-platform

### PostgreSQL

1. In Render database dashboard
2. Click "Backups" tab
3. Manual backup: Click "Create Backup"
4. Automatic backups: Enabled on paid plans
5. Download backups: Click backup → Download

---

## Scaling Considerations

### Free Tier Limitations

- Service spins down after 15 minutes of inactivity
- First request after spin-down takes 30-60 seconds (cold start)
- 750 hours/month free

### Upgrading to Paid

- No cold starts
- Multiple instances for high availability
- Custom resource allocation
- Priority support

---

## Maintenance

### Updating the Application

1. Push changes to GitHub `main` branch
2. Render automatically detects changes
3. Triggers new build and deploy
4. Zero-downtime deployment

### Manual Deploy

1. Go to service dashboard
2. Click "Manual Deploy"
3. Select "Deploy latest commit"

### Running Migrations After Updates

If you add new migrations:

1. Deploy the application
2. Open Render Shell
3. Run: `dotnet ef database update`

---

## Security Checklist

- ✅ HTTPS enabled (automatic with Render)
- ✅ No secrets in source code
- ✅ Environment variables for sensitive data
- ✅ `.gitignore` configured properly
- ✅ Production configuration separate from development
- ✅ Database credentials secured
- ⚠️ Consider enabling authentication rate limiting
- ⚠️ Regular database backups scheduled

---

## Cost Estimate

**Free Tier:**

- Web Service: Free (with limitations)
- PostgreSQL: Free tier available (limited storage)
- Persistent Disk: 1 GB free

**Paid Plans (for production):**

- Web Service: $7/month (Starter)
- PostgreSQL: $7/month (Starter)
- Estimated monthly: ~$14/month

---

## Support Resources

- **Render Documentation:** https://render.com/docs
- **Render Community:** https://community.render.com
- **.NET on Render:** https://render.com/docs/deploy-dotnet
- **Project Repository:** https://github.com/coloub/AcademicTaskManager

---

## Academic Project Notes

This deployment guide is designed for CSC325 course projects. For demonstration purposes, the free tier is sufficient. Production applications should consider:

- Upgrading to paid plans for reliability
- Implementing comprehensive monitoring
- Setting up automated backups
- Configuring custom domains
- Adding CDN for static assets

---

**Last Updated:** February 2026  
**Version:** 1.0
