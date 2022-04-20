using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Vendor
    {
        public int Id { get; set; }
        [StringLength(maximumLength:50)]
        [Required]
        public string Name { get; set; }
        public List<Product> Products { get; set; }

    }
}
