# User Guide - Academic Task Manager

## Welcome to Academic Task Manager

Academic Task Manager is an application designed to help students organize their academic projects and associated tasks efficiently.

## Table of Contents

1. [Getting Started](#getting-started)
2. [Registration and Login](#registration-and-login)
3. [Project Management](#project-management)
4. [Task Management](#task-management)
5. [Frequently Asked Questions](#frequently-asked-questions)

---

## Getting Started

### Accessing the Application

1. Open your web browser
2. Navigate to the application URL (e.g., `http://localhost:5034`)
3. View the home page with system information

### Home Page

The home page displays:

- **System description** - What you can accomplish with Academic Task Manager
- **Key features** - Project management, task control, visual tracking
- **Action buttons** - Create account or Login

---

## Registration and Login

### Creating a New Account

1. **Click "Create Account"** on the home page
2. **Complete the registration form:**
   - Email: Your email address
   - Password: Minimum 6 characters, must include:
     - At least one uppercase letter
     - At least one lowercase letter
     - At least one number
     - At least one special character
3. **Confirm your password**
4. **Click "Register"**
5. In development mode, your account is automatically confirmed

**Valid password example:** `Test123!`

### Logging In

1. **Click "Login"** on the home page
2. **Enter your credentials:**
   - Email
   - Password
3. **Optionally**, check "Remember me" to maintain an active session
4. **Click "Login"**

### Logging Out

1. Click your username in the navigation menu
2. Select "Logout"

---

## Project Management

### Viewing All Your Projects

1. **After logging in**, click "My Projects" in the navigation menu
2. You will see card-based list of all your projects
3. Each card displays:
   - Project title
   - Description (first 100 characters)
   - Creation date
   - Number of total and completed tasks

### Creating a New Project

1. **On the "My Projects" page**, click "New Project"
2. **Complete the form:**
   - **Title** (required): Descriptive project name
     - Maximum 200 characters
     - Example: "Database Systems Final Project"
   - **Description** (required): Project details
     - Maximum 2000 characters
     - Include objectives, requirements, scope
3. **Click "Create Project"**
4. You will be automatically redirected to the project list

**Tips:**

- Be specific in the title for easy project identification
- In the description, include all relevant information you will need to reference

### Viewing Project Details

1. **In the project list**, click the "View" button on any project
2. The details screen shows:

   **Project Information:**
   - Complete title
   - Full description
   - Creation date

   **Color-Coded Statistics Cards:**
   - 🔵 **Total Tasks** - Total number of tasks
   - 🟢 **Completed** - Finished tasks
   - 🟡 **Pending** - Tasks to be done
   - 🔴 **Overdue** - Pending tasks past their due date

   **Progress Bar:**
   - Visually displays completion percentage
   - Indicates how many tasks have been completed out of total

   **Task List:**
   - Table with all project tasks
   - Columns: Checkbox, Title, Due Date, Status, Actions
   - Overdue tasks are highlighted in red

### Editing a Project

1. **From the project list**: Click the pencil button (Edit)

   **OR**

   **From project details**: Click "Edit"

2. **Modify** the title and/or description
3. **Click "Save Changes"**
4. You will see a confirmation message

**Note:** You can only edit your own projects

### Deleting a Project

⚠️ **WARNING:** This action will also delete ALL project tasks and CANNOT be undone.

1. **In the project list**, click the trash button (Delete)
2. **A confirmation dialog will appear**:
   - Review the project name
   - Read the warning
3. **Click "Delete"** to confirm

   **OR**

   **Click "Cancel"** to abort the operation

---

## Task Management

### Creating a New Task

1. **Navigate to the project details** where you want to create the task
2. **Click "New Task"**
3. **Complete the form:**
   - **Title** (required): Task name
     - Maximum 200 characters
     - Example: "Design ER diagram"
   - **Description** (optional): Additional details
     - Maximum 2000 characters
   - **Due Date** (required): When it should be completed
     - Use the date picker
     - Default: 7 days from today
   - **Status**: Pending or Completed
     - Default: Pending
4. **Click "Create Task"**
5. You will return to the project details page with the new task

**Task Example:**

```
Title: Implement data models
Description: Create model classes in C# using EF Core with all necessary annotations.
Date: 02/15/2026
Status: Pending
```

### Marking a Task as Completed/Pending

In the project task table:

1. **Click the checkbox** at the beginning of the row
2. The status will change automatically:
   - ✅ **Checked** = Completed (green badge)
   - ☐ **Unchecked** = Pending (yellow badge)
3. Project statistics will update automatically

**Quick method to change multiple tasks:**

- Simply click each checkbox you wish to change
- Changes apply immediately

### Editing a Task

1. **In the task table**, click the pencil button (Edit)
2. **Modify** any field:
   - Title
   - Description
   - Due date
   - Status
3. **Click "Save Changes"**
4. **Or click "Return to Project"** to cancel

### Deleting a Task

1. **In the task table**, click the trash button (Delete)
2. **A confirmation dialog will appear**
3. **Click "Delete"** to confirm

   **OR**

   **Click "Cancel"** to abort

### Interpreting Overdue Tasks

Overdue tasks are identified by:

- **Red background row** in the table
- **Red text** in the date
- **Warning icon** ⚠️ with "Overdue"

**What to do with overdue tasks:**

1. If already completed: Check the checkbox to change to Completed
2. If still pending: Consider updating the due date
3. Prioritize these tasks in your work

---

## Frequently Asked Questions

### Can I share a project with other users?

**No.** Currently, each project belongs to a single user. This functionality may be added in future versions.

### How many projects can I create?

**Unlimited.** You can create as many projects as needed to organize your academic work.

### What if I forget my password?

Click "Forgot your password?" on the login page and follow the instructions (requires email configuration in production).

### Can I change my email or password?

Yes. Click your name in the navigation menu to access account management.

### Are data saved automatically?

Yes. All changes are saved immediately to the database when you click buttons like "Create", "Save", or checkbox toggles.

### Can I use the application on my phone?

Yes. The application is fully responsive and works on mobile devices, tablets, and desktop computers.

### What browsers are supported?

The application works in:

- Chrome (recommended)
- Edge
- Firefox
- Safari
- Opera

### Can I export my projects?

This functionality is not currently available, but is planned for future versions.

### How can I better organize my projects?

**Best practices:**

1. Use one project per subject or major assignment
2. Divide the project into small, manageable tasks
3. Be realistic with due dates
4. Review your projects regularly
5. Mark tasks as completed immediately
6. Use the description to document important details

### What do I do if I encounter an error?

If you encounter a problem:

1. Refresh the page (F5)
2. Try logging out and logging back in
3. Contact technical support with error details

---

## Tips for Using Academic Task Manager Effectively

### Project Planning

- **Create the project as soon as you receive the assignment**
- **Read all requirements** before creating tasks
- **Divide large assignments** into small tasks (2-4 hours each)

### Date Management

- **Add time buffer** - Don't use the exact deadline
- **Review upcoming tasks** regularly (every 2-3 days)
- **Prioritize overdue tasks** immediately

### Progress Tracking

- **Mark completed tasks** immediately
- **Review completion percentage** to maintain pace
- **Celebrate small achievements** when completing tasks

### Using Statistics

- **Use the progress bar** as motivation
- **Address overdue tasks** shown in red
- **Maintain a balance** between pending and completed

---

## Support and Help

Need more help?

- Review technical documentation in `IMPLEMENTACION.md`
- Consult developer notes in `DEVELOPER_NOTES.md`
- Contact system administrator

---

**Document version:** 1.0  
**Date:** February 10, 2026  
**Application:** Academic Task Manager v1.0
