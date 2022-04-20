using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class CollectionController : Controller
    {
        private readonly AppDB _context;
        public CollectionController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            CollectionVM collection = new CollectionVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.Include(col=>col.CollectionProducts).ThenInclude(colp=>colp.Product).ToList(),
                Vendors = _context.Vendors.ToList()
            };
            return View(collection);
        }
    }
}
