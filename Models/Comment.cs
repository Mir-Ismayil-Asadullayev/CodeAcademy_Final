using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:500)]
        public string Text { get; set; }
        public bool IsVisible { get; set; }
        public DateTime CreatedTime { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
