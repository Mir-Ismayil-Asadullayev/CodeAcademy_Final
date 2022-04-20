using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Areas.MyProject.Controllers
{
    [Area("MyProject")]
    public class CollectionController : Controller
    {
        private readonly AppDB _context;
        public CollectionController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.Collection = _context.Collections.Skip((page - 1) * 2).Take(2).ToList();
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Collections.Count() / 2);
            ViewBag.CurrentPage = page;

            List<Collection> collections = _context.Collections.Include(c => c.CollectionProducts).ThenInclude(c => c.Product).Skip((page - 1) * 2).Take(2).ToList();
            return View(collections);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Collection collection)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Collections.Add(collection);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Collection coll = _context.Collections.Include(c => c.CollectionProducts).ThenInclude(c => c.Product).FirstOrDefault(col => col.Id == id);
            return View(coll);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Collection collect)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Collection existcol = _context.Collections.FirstOrDefault(col => col.Id == collect.Id);
            if (existcol == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            Collection clone = _context.Collections.FirstOrDefault(col => col.Name.ToLower().Trim() == collect.Name.ToLower().Trim());
            Collection clone1 = _context.Collections.FirstOrDefault(col => col.Image == collect.Image);

            //if (clone != null)
            //{
            //    ModelState.AddModelError("", "The given name is already existed");
            //    return View();
            //}
            //if (clone1 == null)
            //{
            //    ModelState.AddModelError("", "The given name is already existed");
            //    return View();
            //}
            existcol.Image = collect.Image;
            existcol.Name = collect.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Collection collection = _context.Collections.FirstOrDefault(col => col.Id == id);
            if (collection == null) return Json(new { status = 404 });
            _context.Collections.Remove(collection);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
