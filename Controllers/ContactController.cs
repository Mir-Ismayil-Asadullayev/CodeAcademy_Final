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
    public class ContactController : Controller
    {
        private readonly AppDB _context;
        public ContactController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ProductVM contactmodel = new ProductVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.ToList(),
                Vendors = _context.Vendors.ToList()
            };
            return View(contactmodel);
        }
    }
}
