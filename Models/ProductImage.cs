using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
