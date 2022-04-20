using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.DAL;
using MyProject.Models;
using MyProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDB _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, AppDB context)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            ViewBag.RegisterProducts = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList();
            ViewBag.RegisterStatics = _context.Statics.Single();
            ViewBag.RegisterCollections = _context.Collections.ToList();
            ViewBag.RegisterVendors = _context.Vendors.ToList();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            ViewBag.RegisterProducts = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList();
            ViewBag.RegisterStatics = _context.Statics.Single();
            ViewBag.RegisterCollections = _context.Collections.ToList();
            ViewBag.RegisterVendors = _context.Vendors.ToList();
            if (!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                FullName = register.Fullname,
                UserName = register.UserName,
                Email = register.Email
            };
            if (register.Password.Contains(register.UserName) || register.Password.Contains(register.Fullname))
            {
                ModelState.AddModelError("", "This Username and email could not be the same digits");
                return View();
            }

            IdentityResult result = await _userManager.CreateAsync(user, register.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Member");
                return RedirectToAction("Login", "Account");
            }



        }
        public IActionResult Login()
        {
            ViewBag.RegisterProducts = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList();
            ViewBag.RegisterStatics = _context.Statics.Single();
            ViewBag.RegisterCollections = _context.Collections.ToList();
            ViewBag.RegisterVendors = _context.Vendors.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM login)
        {
            ViewBag.RegisterProducts = _context.Products.Include(p => p.ProductImages).Include(p => p.Campaign).ToList();
            ViewBag.RegisterStatics = _context.Statics.Single();
            ViewBag.RegisterCollections = _context.Collections.ToList();
            ViewBag.RegisterVendors = _context.Vendors.ToList();
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or Password is not correct");
                return View();
            }
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account has been locked for 3 minutes due to overtrying to access");
                    return View();
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Edit()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditVM editedUser = new UserEditVM
            {
                UserName = user.UserName,
                Email = user.Email,
                Fullname = user.FullName
            };

            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editedUser)
        {
            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM eUser = new UserEditVM
            {
                UserName = user.UserName,
                Email = user.Email,
                Fullname = user.FullName
            };

            if (user.UserName != editedUser.UserName && await _userManager.FindByNameAsync(editedUser.UserName) != null)
            {
                ModelState.AddModelError("", $"{editedUser.UserName} has already taken");
                return View(eUser);
            }

            if (string.IsNullOrWhiteSpace(editedUser.CurrentPassword))
            {
                user.UserName = editedUser.UserName;
                user.Email = editedUser.Email;
                user.FullName = editedUser.Fullname;
                await _userManager.UpdateAsync(user);
            }
            else
            {
                user.UserName = editedUser.UserName;
                user.Email = editedUser.Email;
                user.FullName = editedUser.Fullname;

                IdentityResult result = await _userManager.ChangePasswordAsync(user, editedUser.CurrentPassword, editedUser.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return View(eUser);
                }
            }
            await _signInManager.PasswordSignInAsync(user, editedUser.Password, true, true);

            return RedirectToAction("index", "home");

        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            if (user == null) return BadRequest();
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();

            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
    }
}
