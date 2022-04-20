using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using MyProject.DAL;
using MyProject.Extensions;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Areas.MyProject.Controllers
{
    [Area("MyProject")]
    public class HomeSliderController : Controller
    {
        private readonly AppDB _context;
        private IWebHostEnvironment _env;
        public HomeSliderController(AppDB context, IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
        }
        public IActionResult Index()
        {
            List<HomeSlider> slider = _context.HomeSliders.ToList();
            return View(slider);
        }
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(HomeSlider slider)
        //{
        //    if (!ModelState.IsValid) return View();
        //    if (slider.ImageFile == null)
        //    {
        //        ModelState.AddModelError("ImageFile", "Please choose the picture");
        //        return View();
        //    }
        //    if (!slider.ImageFile.IsLengthMatches(2))
        //    {
        //        ModelState.AddModelError("ImageFile", "The size of picture could be less than 2MB");
        //        return View();
        //    }
        //    if (!slider.ImageFile.IsImage())
        //    {
        //        ModelState.AddModelError("ImageFile", "Please Choose an image file");
        //        return View();
        //    }
        //    _context.HomeSliders.Add(slider);
        //    _context.SaveChanges();

        //    return RedirectToAction(nameof(Index),"HomeSlider");
        //}
        public IActionResult Edit(int id)
        {
            HomeSlider slider = _context.HomeSliders.FirstOrDefault(s => s.Id == id);
            if (slider == null) return NotFound();
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(HomeSlider slider)
        {
            HomeSlider exist = _context.HomeSliders.FirstOrDefault(hs => hs.Id == slider.Id);
            if (!ModelState.IsValid) return View(exist);
            if (exist == null) return NotFound(); exist.Image = slider.Image;
            exist.Offer = slider.Offer;
            exist.SubTitle = slider.SubTitle;
            exist.Headtitle = slider.Headtitle;
            exist.Button = slider.Button;
            #region

            //if (slider.Image != null && slider.Offer != null && slider.SubTitle != null && slider.Headtitle != null && slider.Button != null)
            //{


            //if (slider.Image != null && slider.Offer == null && slider.SubTitle == null && slider.Headtitle == null && slider.Button == null)
            //{
            //    if (!slider.ImageFile.IsLengthMatches(2))
            //    {
            //        ModelState.AddModelError("ImageFile", "The size of picture could be less than 2MB");
            //        return View(exist);
            //    }
            //    if (!slider.ImageFile.IsImage())
            //    {
            //        ModelState.AddModelError("ImageFile", "Please Choose an image file");
            //        return View(exist);
            //    }
            //    exist.Image = slider.Image;
            //}
            //if (slider.Image == null && slider.Offer != null && slider.SubTitle == null && slider.Headtitle == null && slider.Button == null)
            //{
            //    exist.Offer = slider.Offer;
            //}
            //if (slider.Image == null && slider.Offer == null && slider.SubTitle != null && slider.Headtitle == null && slider.Button == null)
            //{
            //    exist.SubTitle = slider.SubTitle;
            //}
            //if (slider.Image == null && slider.Offer == null && slider.SubTitle == null && slider.Headtitle != null && slider.Button == null)
            //{
            //    exist.Headtitle = slider.Headtitle;
            //}
            //if (slider.Image == null && slider.Offer == null && slider.SubTitle == null && slider.Headtitle == null && slider.Button != null)
            //{
            //    exist.Button = slider.Button;
            //}
            #endregion

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));

        }
    }
}
