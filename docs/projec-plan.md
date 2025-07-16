# 🗓️ Daily Task Manager - Windsurf Project Doc

## 🎯 Project Purpose
Build a simple and effective personal task manager using ASP.NET Core MVC that helps users organize daily tasks, write quick notes, and stay productive. This app will be used for daily planning and task tracking.

---

## 🧱 Tech Stack
- **Frontend:** Razor Views, Bootstrap or Tailwind CSS
- **Backend:** ASP.NET Core MVC, C#
- **Database:** SQLite (or SQL Server)
- **ORM:** Entity Framework Core
- **Auth (Optional):** ASP.NET Core Identity

---

## 📁 Project Structure
```
TaskManager/
├── Controllers/
│   ├── TasksController.cs
│   └── NotesController.cs
│
├── Models/
│   ├── TaskItem.cs
│   └── Note.cs
│
├── Views/
│   ├── Shared/
│   ├── Tasks/
│   │   ├── Index.cshtml
│   │   ├── Create.cshtml
│   │   ├── Edit.cshtml
│   │   └── Details.cshtml
│   └── Notes/
│       ├── Index.cshtml
│       ├── Create.cshtml
│       └── Edit.cshtml
│
├── Data/
│   └── ApplicationDbContext.cs
│
├── wwwroot/
│   └── css/, js/, images/
│
└── appsettings.json
```

---

## ✍️ Models

### TaskItem.cs
```csharp
public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public string Priority { get; set; } // Low, Medium, High
    public bool IsCompleted { get; set; }
}
```

### Note.cs
```csharp
public class Note
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
```

---

## ✅ Core Features
- Add/Edit/Delete tasks with due dates and priorities
- Mark tasks as completed
- View daily/weekly task summary
- Create and edit personal notes
- Optional login/logout (if Identity is enabled)

---

## 🚀 Stretch Goals
- Search and filter tasks
- Task reminders via email
- Dark mode toggle
- Google Calendar integration

---

## 📌 Setup Instructions
1. Create a new ASP.NET Core MVC project.
2. Add models and DbContext.
3. Scaffold controllers and views.
4. Add migrations and update database.
5. Style using Bootstrap or Tailwind.
6. (Optional) Enable authentication using ASP.NET Identity.

---

## 📅 Daily Use Workflow
1. Open the dashboard to see today’s tasks.
2. Create, edit, or complete tasks as needed.
3. Write notes or quick thoughts for the day.
4. End day by reviewing completed tasks.