using System.Collections.Generic;

namespace TaskManager.Models
{
    public class DashboardViewModel
    {
        public List<TaskItem> TodaysTasks { get; set; } = new List<TaskItem>();
        public int OverdueTasks { get; set; }
        public int TomorrowTasks { get; set; }
        public int CompletedToday { get; set; }
        public List<Note> RecentNotes { get; set; } = new List<Note>();
    }
}
