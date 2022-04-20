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
    public class InformationController : Controller
    {
        private readonly AppDB _context;
        public InformationController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            InformationVM infomodel = new InformationVM
            {
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList(),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.ToList(),
                Vendors = _context.Vendors.ToList(),
                Informations =_context.Informations.ToList()
            };
            return View(infomodel);
        }
    }
}
