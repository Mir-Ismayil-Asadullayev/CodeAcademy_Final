using System;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 35)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [StringLength(maximumLength: 20)]
        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [StringLength(maximumLength: 150)]
        [Required]
        public string HeadTitle { get; set; }
        [StringLength(maximumLength: 350)]
        [Required]
        public string Quote { get; set; }
    }
}
