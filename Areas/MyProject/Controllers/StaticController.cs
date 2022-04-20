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
    public class StaticController : Controller
    {
        private readonly AppDB _context;
        public StaticController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            Static staticmodel = _context.Statics.Single();
            return View(staticmodel);
        }
        public IActionResult Edit()
        {
            Static model = _context.Statics.Single();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Static stat)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Static exist = _context.Statics.Single();
            if (exist == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            exist.AnimeImage = stat.AnimeImage;
            exist.Article1 = stat.Article1;
            exist.Article2 = stat.Article2;
            exist.BannerImage = stat.BannerImage;
            exist.ContactImage = stat.ContactImage;
            exist.ExOffer1 = stat.ExOffer1;
            exist.ExOffer2 = stat.ExOffer2;
            exist.Facebook = stat.Facebook;
            exist.Instagram = stat.Instagram;
            exist.TelNumber = stat.TelNumber;
            exist.Email = stat.Email;
            exist.Location = stat.Location;
            exist.Logo = stat.Logo;
            exist.SpecialOffer1 = stat.SpecialOffer1;
            exist.SpecialOffer2 = stat.SpecialOffer2;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
