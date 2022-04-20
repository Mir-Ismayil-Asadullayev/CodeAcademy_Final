using Microsoft.AspNetCore.Mvc;
using MyProject.DAL;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Areas.MyProject.Controllers
{
    [Area("MyProject")]
    public class InformationController : Controller
    {
        private readonly AppDB _context;
        public InformationController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Information> aboutus = _context.Informations.ToList();
            return View(aboutus);
        }
        public IActionResult Edit(int id)
        {
            Information information = _context.Informations.FirstOrDefault(info => info.Id == id);
            return View(information);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Information information)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Information exist = _context.Informations.FirstOrDefault(info => info.Id == information.Id);
            if (exist == null)
            {
                return RedirectToAction("Index", "NotFound");
            }

            exist.Image = information.Image;
            exist.Title = information.Title;
            exist.Subtitle = information.Subtitle;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
