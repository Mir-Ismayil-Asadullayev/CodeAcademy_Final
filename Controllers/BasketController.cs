using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDB _context;
        public BasketController(AppDB context)
        {
            _context = context;
        }
        public IActionResult Index(int id)
        {
            string basket = HttpContext.Request.Cookies["Kuki"];
            BasketCookieVM VM = new BasketCookieVM();
            if (!string.IsNullOrEmpty(basket))
            {
                VM = JsonConvert.DeserializeObject<BasketCookieVM>(basket);
            }
            
            return View(VM);
        }
    } 
}
