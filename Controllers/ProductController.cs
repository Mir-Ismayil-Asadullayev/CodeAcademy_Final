using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Models;
using MyProject.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Controllers
{

    public class ProductController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDB _context;
        public ProductController(AppDB context, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public ActionResult Index(int id, int page = 1)
        {
            ViewBag.Produkt = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).Skip((page - 1) * 8).Take(8).ToList();
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Products.Count() / 8);
            ViewBag.CurrentPage = page;

            ProductVM productmodel = new ProductVM
            {
                Static = _context.Statics.Single(),
                Collections = _context.Collections.Include(copl => copl.CollectionProducts).ThenInclude(copl => copl.Product).ToList(),
                Vendors = _context.Vendors.ToList(),
                Collection = _context.Collections.FirstOrDefault(co => co.Id == id),
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList()
            };
            return View(productmodel);

        }
        public ActionResult ProductDetail(int id)
        {
            ProductDetailVM productdetail = new ProductDetailVM
            {
                Product = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).Include(p => p.Vendor).Include(p => p.CollectionProducts).ThenInclude(p => p.Collection).FirstOrDefault(p => p.Id == id),
                Static = _context.Statics.Single(),
                Collections = _context.Collections.Include(cop => cop.CollectionProducts).ThenInclude(cop => cop.Product).ToList(),
                Vendors = _context.Vendors.ToList(),
                Products = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList()
            };
            if (id == 0)
            {
                return RedirectToAction("Index", "NotFound");
            }
            return View(productdetail);
        }
        public IActionResult AddBasket(int id)
        {
            Product product = _context.Products.Include(p => p.ProductImages).FirstOrDefault(p => p.Id == id);
            string basket = HttpContext.Request.Cookies["Kuki"];

            if (basket == null)
            {
                BasketCookieVM basketVM = new BasketCookieVM
                {
                    BasketItems = new List<BasketItemVM>(),
                    TotalPrice = product.Price,
                    Count = 1
                };
                BasketItemVM basketItemVM = new BasketItemVM
                {
                    Product = product,
                    Count = 1
                };
                basketVM.BasketItems.Add(basketItemVM);
                string basketStr = JsonConvert.SerializeObject(basketVM);
                HttpContext.Response.Cookies.Append("Kuki", basketStr);

            }
            else
            {
                BasketCookieVM basketVM = JsonConvert.DeserializeObject<BasketCookieVM>(basket);
                BasketItemVM basketItemVM = basketVM.BasketItems.FirstOrDefault(b => b.Product.Id == product.Id);
                if (basketItemVM == null)
                {
                    basketItemVM = new BasketItemVM
                    {
                        Product = product,
                        Count = 1
                    };
                    basketVM.Count++;
                    basketVM.BasketItems.Add(basketItemVM);
                }
                else
                {
                    basketItemVM.Count++;
                }

                basketVM.TotalPrice += product.Price;
                Math.Round(basketVM.TotalPrice, 2);
                string basketStr = JsonConvert.SerializeObject(basketVM);
                HttpContext.Response.Cookies.Append("Kuki", basketStr);
            }


            return RedirectToAction("Index", "Basket");
        }
        public IActionResult ShowBasket()
        {
            string basketpro = HttpContext.Request.Cookies["Kuki"];

            if (basketpro == null)
            {
                return RedirectToAction("Index", "NotFound");
            }
            else
            {
                BasketCookieVM products = JsonConvert.DeserializeObject<BasketCookieVM>(basketpro);
                return Json(products);
            }
        }
        public async Task<IActionResult> AddComment(Comment comment)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (!ModelState.IsValid) return RedirectToAction("ProductDetail", "Product", new { id = comment.ProductId });
            if (!_context.Products.Any(f => f.Id == comment.Product.Id)) return NotFound();
            Comment cmt = new Comment
            {
                Text = comment.Text,
                ProductId = comment.ProductId,
                CreatedTime = DateTime.Now,

            };
            return RedirectToAction();
        }
    }
}
