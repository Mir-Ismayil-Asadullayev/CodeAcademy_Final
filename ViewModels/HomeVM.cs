using MyProject.Models;
using System.Collections.Generic;

namespace MyProject.ViewModels
{
    public class HomeVM
    {
        public Static Static { get; set; }
        public List<HomeSlider> HomeSliders { get; set; }
        public List<Product> Products { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Collection> Collections { get; set; }
        public List<Vendor> Vendors { get; set; }

    }
}
