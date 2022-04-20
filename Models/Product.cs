using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public bool InStock { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ushort ItemCount { get; set; }
        public int? CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<CollectionProduct> CollectionProducts { get; set; }
        public List<Comment> Comments { get; set; }
        [NotMapped]
        public List<int> CollectionIds { get; set; }
        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public List<int> ImageIds { get; set; }
        [NotMapped]
        public List<int> CategoryIds { get; set; }
    }
}
