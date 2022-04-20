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
    public class WishlistController : Controller
    {
        private readonly AppDB _context;
        public WishlistController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            WishlistVM wishlisttmodel = new WishlistVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.ToList(),
                Vendors = _context.Vendors.ToList()
            };
            return View(wishlisttmodel);
        }
    }
}
