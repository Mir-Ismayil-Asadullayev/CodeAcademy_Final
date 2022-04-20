using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Models;
using MyProject.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDB _context;
        public HomeController(AppDB context)
        {
            _context = context;               
        }

        public IActionResult Index()
        {
            HomeVM homemodel = new HomeVM
            {
                Static = _context.Statics.Single(),
                HomeSliders = _context.HomeSliders.ToList(),
                Products = _context.Products.Include(p=>p.Campaign).Include(p=>p.ProductImages).ToList(),
                Blogs = _context.Blogs.ToList(),
                Collections = _context.Collections.ToList(),
                Vendors =_context.Vendors.ToList()
            };
            return View(homemodel);
        }
    }
}
