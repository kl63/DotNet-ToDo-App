using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index(string? sortOrder = null, string? searchString = null, string? priority = null)
        {
            ViewData["TitleSortParam"] = string.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";
            ViewData["PrioritySortParam"] = sortOrder == "priority" ? "priority_desc" : "priority";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentPriority"] = priority;

            var tasks = from t in _context.Tasks select t;

            // Filter by search string
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString) || 
                                       t.Description.Contains(searchString));
            }
            
            // Filter by priority
            if (!string.IsNullOrEmpty(priority) && priority != "All")
            {
                tasks = tasks.Where(t => t.Priority == priority);
            }

            // Apply sorting
            switch (sortOrder)
            {
                case "title_desc":
                    tasks = tasks.OrderByDescending(t => t.Title);
                    break;
                case "date":
                    tasks = tasks.OrderBy(t => t.DueDate);
                    break;
                case "date_desc":
                    tasks = tasks.OrderByDescending(t => t.DueDate);
                    break;
                case "priority":
                    tasks = tasks.OrderBy(t => t.Priority);
                    break;
                case "priority_desc":
                    tasks = tasks.OrderByDescending(t => t.Priority);
                    break;
                default:
                    tasks = tasks.OrderBy(t => t.Title);
                    break;
            }

            var today = DateTime.Today;
            ViewData["TodaysTasks"] = await _context.Tasks
                .Where(t => t.DueDate.Date == today)
                .CountAsync();

            ViewData["OverdueTasks"] = await _context.Tasks
                .Where(t => t.DueDate.Date < today && !t.IsCompleted)
                .CountAsync();

            return View(await tasks.ToListAsync());
        }

        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["PriorityList"] = new[] { "Low", "Medium", "High" };
            return View();
        }

        // POST: Tasks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,DueDate,Priority,IsCompleted")] TaskItem taskItem)
        {
            if (ModelState.IsValid)
            {
                taskItem.CreatedAt = DateTime.Now;
                _context.Add(taskItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PriorityList"] = new[] { "Low", "Medium", "High" };
            return View(taskItem);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }
            ViewData["PriorityList"] = new[] { "Low", "Medium", "High" };
            return View(taskItem);
        }

        // POST: Tasks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,Priority,IsCompleted,CreatedAt")] TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskItemExists(taskItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["PriorityList"] = new[] { "Low", "Medium", "High" };
            return View(taskItem);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taskItem = await _context.Tasks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskItem == null)
            {
                return NotFound();
            }

            return View(taskItem);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem != null)
            {
                _context.Tasks.Remove(taskItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Tasks/ToggleComplete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var taskItem = await _context.Tasks.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            taskItem.IsCompleted = !taskItem.IsCompleted;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskItemExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
