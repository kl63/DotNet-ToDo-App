using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        public string Description { get; set; } = string.Empty;
        
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }
        
        [Required]
        public string Priority { get; set; } = "Medium"; // Low, Medium, High
        
        public bool IsCompleted { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
