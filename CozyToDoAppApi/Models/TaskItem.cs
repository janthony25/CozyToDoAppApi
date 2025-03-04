﻿using System.ComponentModel.DataAnnotations;

namespace CozyToDoAppApi.Models
{
    public class TaskItem
    {
        [Key]
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public bool IsCompleted { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
