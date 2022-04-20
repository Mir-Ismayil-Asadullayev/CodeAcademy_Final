using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class HomeSlider
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        [StringLength(maximumLength: 30)]
        public string Offer { get; set; }
        [StringLength(maximumLength: 30)]
        [Required]
        public string Headtitle { get; set; }
        [StringLength(maximumLength: 130)]
        [Required]
        public string SubTitle { get; set; }
        [StringLength(maximumLength: 10)]
        [Required]
        public string Button { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
