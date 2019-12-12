using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models.RazorViewModels.Login;
using Ecommerce.Models.RazorViewModels.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

   
         [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.Phone };
                var result = await userManager.CreateAsync(user, model.Password).ConfigureAwait(true);
                if(result.Succeeded)
                {
                   await signInManager.SignInAsync(user, isPersistent: false).ConfigureAwait(true);
                    return RedirectToAction("_cardView", "Product");
                }
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(model);
        }
        [AcceptVerbs("Get","Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailInUse(string Email)
        {
            var user = await userManager.FindByEmailAsync(Email).ConfigureAwait(true);
            if(user==null)
            {
                return Json(true); 
            }
            else
            {
                return Json($"Email{Email} is already taken.Plz try another one.");
            }
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync().ConfigureAwait(true);
            return RedirectToAction("_cardView", "Product");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false).ConfigureAwait(true);
                if (result.Succeeded)
                {
                    return RedirectToAction("_cardView", "Product");
                }
                ModelState.AddModelError(string.Empty, "Invalid User Name Or Password");
            }
            return View(model);
        }
       
    }
}