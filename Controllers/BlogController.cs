using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Models;
using MyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDB _context;
        public BlogController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            BlogVM blogmodel = new BlogVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.ToList(),
                Vendors = _context.Vendors.ToList(),
                Blogs = _context.Blogs.ToList(),
            };
            return View(blogmodel);
        }
        public ActionResult BlogDetail(int id)
        {
            BlogVM blogmodels = new BlogVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.ToList(),
                Vendors = _context.Vendors.ToList(),
                Blog = _context.Blogs.FirstOrDefault(b => b.Id == id),
                Blogs = _context.Blogs.ToList()
            };
            if (id == 0)
            {
                return RedirectToAction("Index","NotFound");
            }
            return View(blogmodels);
        }
    }
}
