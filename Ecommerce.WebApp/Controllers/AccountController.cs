using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Login;
using Ecommerce.Models.RazorViewModels.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
         [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterVM();
            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model, IFormFile Image)
        {
            if (model.Image == null || model.ImagePath == null)
            {
                model.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if (Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    //if(Image.Length<2048)
                    //{
                    model.Image = ms.ToArray();
                    //}
                    var files = HttpContext.Request.Form.Files;
                    foreach (var image in files)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var file = Image;
                            // var root = _appEnvironment.WebRootPath;
                            var root = "wwwroot\\";
                            var uploads = "user\\img";
                            if (file.Length > 0)
                            {
                                // you can change the Guid.NewGuid().ToString().Replace("-", "")
                                // to Guid.NewGuid().ToString("N") it will produce the same result
                                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                                using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream).ConfigureAwait(true);
                                    // This will produce uploads\img\fileName.ext
                                    model.ImagePath = Path.Combine(uploads, fileName);
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                model.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.Phone , Address = model.Address , ShippingAddress = model.ShippingAddress,ImagePath=model.ImagePath,Image = model.Image,Phone =model.Phone };
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