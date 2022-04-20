using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Extensions;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Areas.MyProject.Controllers
{
    [Area("MyProject")]
    public class ProductController : Controller
    {
        private readonly AppDB _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(AppDB context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            ViewBag.TotalPage = Math.Ceiling((decimal)_context.Products.Count() / 3);
            ViewBag.CurrentPage = page;
            List<Product> product = _context.Products.Include(p => p.ProductImages).Include(p => p.CollectionProducts).ThenInclude(p => p.Collection).Include(p => p.Campaign).Include(p => p.Vendor).Skip((page - 1) * 3).Take(3).ToList();
            return View(product);
        }
        public IActionResult Create()
        {
            ViewBag.Images = _context.ProductImages.ToList();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Collections = _context.Collections.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            ViewBag.Images = _context.ProductImages.ToList();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Collections = _context.Collections.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();

            if (!ModelState.IsValid) return View();
            if (product.CampaignId == 0) { product.CampaignId = null; }

            product.CollectionProducts = new List<CollectionProduct>();
            foreach (int id in product.CollectionIds)
            {
                CollectionProduct colpro = new CollectionProduct
                {
                    Product = product,
                    CollectionId = id
                };
                product.CollectionProducts.Add(colpro);
            }


            product.ProductImages = new List<ProductImage>();
            //foreach (int id in product.CollectionIds)
            //{
            //    CollectionProduct prodd = new CollectionProduct
            //    {
            //        Product = product,
            //        CollectionId = id
            //    };
            //    product.CollectionProducts.Add(prodd);
            //}
           
            if (product.ImageFiles.Count > 6)
            {
                ModelState.AddModelError("ImageFiles", "You can choose only 5 images");
                return View();
            }
            foreach (var image in product.ImageFiles)
            {
                if (!image.IsImage())
                {
                    ModelState.AddModelError("ImageFiles", "Please choose image file");
                    return View();
                }
                if (!image.IsLengthMatches(2))
                {
                    ModelState.AddModelError("ImageFiles", "Image size must be max 2MB");
                    return View();
                }

            }
            foreach (var image in product.ImageFiles)
            {
                ProductImage proimg = new ProductImage
                {
                    Image = image.SaveImg(_env.WebRootPath, "admin/assets/images"),
                    IsMain = product.ProductImages.Count < 1 ? true : false,
                    Product = product
                };
                product.ProductImages.Add(proimg);
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Images = _context.ProductImages.ToList();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Collections = _context.Collections.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();
            Product prop = _context.Products.Include(p => p.ProductImages).Include(p => p.CollectionProducts).ThenInclude(p => p.Collection).FirstOrDefault(pro => pro.Id == id);
            if (prop == null) return NotFound();
            return View(prop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product pr)
        {
            ViewBag.Images = _context.ProductImages.ToList();
            ViewBag.Campaigns = _context.Campaigns.ToList();
            ViewBag.Collections = _context.Collections.ToList();
            ViewBag.Vendors = _context.Vendors.ToList();

            Product exist = _context.Products.Include(p => p.ProductImages).Include(p => p.CollectionProducts).ThenInclude(p => p.Collection).FirstOrDefault(pd => pd.Id == pr.Id);
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please add correct data");
                return View(exist);
            }

            if (exist == null) return NotFound();

            if (pr.ImageFiles != null)
            {
                foreach (var image in pr.ImageFiles)
                {
                    if (!image.IsImage())
                    {
                        ModelState.AddModelError("ImageFiles", "Please select the image file");
                        return View(exist);
                    }
                    if (!image.IsLengthMatches(2))
                    {
                        ModelState.AddModelError("ImageFiles", "You can choose file which size is max 2MB");
                        return View(exist);
                    }
                }

                //List<ProductImage> removableImages = exist.ProductImages.Where(fi => fi.IsMain == false && !pr.ImageIds.Contains(fi.Id)).ToList();

                //exist.ProductImages.RemoveAll(fi => removableImages.Any(ri => ri.Id == fi.Id));

                //foreach (var item in removableImages)
                //{
                //    Helpers.Helper.DeleteImg(_env.WebRootPath, "admin/assets/images", item.Image);
                //}

                foreach (var image in pr.ImageFiles)
                {
                    ProductImage proimage = new ProductImage
                    {
                        Image = image.SaveImg(_env.WebRootPath, "admin/assets/images"),
                        IsMain = false,
                        ProductId = exist.Id
                    };
                    exist.ProductImages.Add(proimage);
                }


                List<CollectionProduct> removableCategories = exist.CollectionProducts.Where(fc => !pr.CategoryIds.Contains(fc.Id)).ToList();

                exist.CollectionProducts.RemoveAll(fc => removableCategories.Any(rc => fc.Id == rc.Id));
                foreach (var categoryId in pr.CategoryIds)
                {
                    CollectionProduct prCategory = exist.CollectionProducts.FirstOrDefault(fc => fc.CollectionId == categoryId);
                    if (prCategory == null)
                    {
                        CollectionProduct pcol = new CollectionProduct
                        {
                            CollectionId = categoryId,
                            ProductId = exist.Id
                        };
                        exist.CollectionProducts.Add(pcol);
                    }
                }
            }
            //List<FlowerCategory> removableCategories = existedFlower.FlowerCategories.Where(fc => !flower.CategoryIds.Contains(fc.Id)).ToList();

            //existedFlower.FlowerCategories.RemoveAll(fc => removableCategories.Any(rc => fc.Id == rc.Id));
            //foreach (var categoryId in flower.CategoryIds)
            //{
            //    FlowerCategory flowerCategory = existedFlower.FlowerCategories.FirstOrDefault(fc => fc.CategoryId == categoryId);
            //    if (flowerCategory == null)
            //    {
            //        FlowerCategory fCategory = new FlowerCategory
            //        {
            //            CategoryId = categoryId,
            //            FlowerId = existedFlower.Id
            //        };
            //        existedFlower.FlowerCategories.Add(fCategory);
            //    }
            //}

            exist.Name = pr.Name;
            exist.Price = pr.Price;
            exist.Description = pr.Description;
            exist.ItemCount = pr.ItemCount;
            exist.Vendor = pr.Vendor;
            exist.InStock = pr.InStock;
            if (pr.CampaignId == 0)
            {
                pr.CampaignId = null;
            }
            exist.Campaign = pr.Campaign;

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Product prod = _context.Products.FirstOrDefault(pro => pro.Id == id);
            if (prod == null) return Json(new { status = 404 });
            _context.Products.Remove(prod);
            _context.SaveChanges();
            return Json(new { status = 200 });
        }
    }
}
