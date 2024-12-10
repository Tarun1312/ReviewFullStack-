using System;
using System.ComponentModel.DataAnnotations;

namespace ReviewHubApp.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; } // Primary Key

        [Required]
        public string Category { get; set; } // Match with table column

        [Required]
        public string Content { get; set; } // Match with table column

        [Required]
        public int UserId { get; set; } // Foreign key, match with table column

        public DateTime CreatedAt { get; set; } // Match with table column

        [Required]
        public int Rating { get; set; } // Match with table column
    }
}
