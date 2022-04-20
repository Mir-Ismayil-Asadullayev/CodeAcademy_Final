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
    public class NotFoundController : Controller
    {
        private readonly AppDB _context;
        public NotFoundController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ProductVM notfoundmodel = new ProductVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.Include(co=>co.CollectionProducts).ThenInclude(co=>co.Product).ToList(),
                Vendors = _context.Vendors.ToList()
            };
            return View(notfoundmodel);
        }
    }
}
