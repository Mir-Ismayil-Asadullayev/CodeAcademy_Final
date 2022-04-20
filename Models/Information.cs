using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Information
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Title { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string Subtitle { get; set; }
    }
}
