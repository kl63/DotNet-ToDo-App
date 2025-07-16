using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var today = DateTime.Today;
        var tomorrow = today.AddDays(1);
        
        // Get today's tasks
        var todaysTasks = _context.Tasks
            .Where(t => t.DueDate.Date == today)
            .OrderBy(t => t.Priority)
            .ToList();
        
        // Get stats for the dashboard
        var stats = new DashboardViewModel
        {
            TodaysTasks = todaysTasks,
            OverdueTasks = _context.Tasks
                .Where(t => t.DueDate.Date < today && !t.IsCompleted)
                .Count(),
            TomorrowTasks = _context.Tasks
                .Where(t => t.DueDate.Date == tomorrow)
                .Count(),
            CompletedToday = _context.Tasks
                .Where(t => t.IsCompleted && t.DueDate.Date == today)
                .Count(),
            RecentNotes = _context.Notes
                .OrderByDescending(n => n.CreatedAt)
                .Take(3)
                .ToList()
        };
        
        return View(stats);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
