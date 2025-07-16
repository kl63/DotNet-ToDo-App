using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models
{
    public class Note
    {
        public int Id { get; set; }
        
        [Required]
        public string Content { get; set; } = string.Empty;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }
    }
}
