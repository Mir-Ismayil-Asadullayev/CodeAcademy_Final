using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Static
    {
        public int Id { get; set; }
        [Required]
        public string Logo { get; set; }
        [Required]
        public string SpecialOffer1 { get; set; }
        [Required]
        public string SpecialOffer2 { get; set; }
        [StringLength(maximumLength: 350)]
        [Required]
        public string Article1 { get; set; }
        [StringLength(maximumLength: 350)]
        [Required]
        public string Article2 { get; set; }
        [Required]
        public string AnimeImage { get; set; }
        [Required]
        public  string BannerImage { get; set; }
        [Required]
        [StringLength(maximumLength: 50)]
        public string TelNumber { get; set; }
        [StringLength(maximumLength: 50)]
        [Required]
        public string Email { get; set; }
        [StringLength(maximumLength: 30)]
        [Required]
        public string Location { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string Facebook { get; set; }
        [StringLength(maximumLength: 200)]
        [Required]
        public string Instagram { get; set; }
        [StringLength(maximumLength: 150)]
        [Required]
        public string ExOffer1 { get; set; }
        [Required]
        public string ExOffer2 { get; set; }
        [Required]
        public string ContactImage { get; set; }       

    }
}
