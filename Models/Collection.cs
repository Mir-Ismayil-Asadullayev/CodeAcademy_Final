using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Collection
    {
        public int Id { get; set; }
        [StringLength(maximumLength: 20)]
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Image { get; set; }
        public List<CollectionProduct> CollectionProducts { get; set; }
    }
}
