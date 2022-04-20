using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.DAL
{
    public class AppDB : IdentityDbContext<AppUser>
    {
        public AppDB(DbContextOptions<AppDB> options) : base(options) { }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<CollectionProduct> CollectionProducts { get; set; } 
        public DbSet<FAQAnswer> FAQAnswers { get; set; }
        public DbSet<FAQQuestion> FAQQuestions { get; set; }
        public DbSet<HomeSlider> HomeSliders { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Static> Statics { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
