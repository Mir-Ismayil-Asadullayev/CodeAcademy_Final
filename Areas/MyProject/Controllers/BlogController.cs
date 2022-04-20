using Microsoft.AspNetCore.Mvc;
using MyProject.DAL;
using MyProject.Models;
using MyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Areas.MyProject.Controllers
{
    [Area("MyProject")]
    public class BlogController : Controller
    {
        private readonly AppDB _context;
        public BlogController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Blogs.Count() / 2);
            ViewBag.CurrentPage = page;
            List<Blog> blogs = _context.Blogs.Skip((page - 1) * 2).Take(2).ToList();
            return View(blogs);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(blo => blo.Id == id);
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Blog existblo = _context.Blogs.FirstOrDefault(blo => blo.Id == blog.Id);
            if (existblo == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            Blog clone = _context.Blogs.FirstOrDefault(blo => blo.Name.ToLower().Trim() == blog.Name.ToLower().Trim());

            if (clone != null)
            {
                ModelState.AddModelError("", "The given name is already existed");
                return View();
            }
            existblo.Image = blog.Image;
            existblo.Name = blog.Name;
            existblo.Author = blog.Author;
            existblo.Date = blog.Date;
            existblo.HeadTitle = blog.HeadTitle;
            existblo.Image = blog.Image;
            existblo.Quote = blog.Quote;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Blog blog = _context.Blogs.FirstOrDefault(blo => blo.Id == id);
            if (blog == null) return Json(new { status = 404 });
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
