using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        //[AllowAnonymous]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        //[AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                  Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole).ConfigureAwait(true);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Administration");
                }
                foreach(IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }
        //[AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var model = roleManager.Roles;
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await roleManager.FindByIdAsync(id).ConfigureAwait(true);
            if(role==null)
            {
                ViewBag.ErrorMessage = $"Role With Id = {id} cn not be found ";
                return View("NotFound");
            }
            var model = new RoleEditVM
            {
                Id = role.Id,
                RoleName = role.Name
            };
            foreach(var user in userManager.Users)
            {
             if( await   userManager.IsInRoleAsync(user,role.Name).ConfigureAwait(true))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit (RoleEditVM model)
        {
            var role = await roleManager.FindByIdAsync(model.Id).ConfigureAwait(true);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id = {model.Id} cn not be found ";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;
                var result = await roleManager.UpdateAsync(role).ConfigureAwait(true);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId).ConfigureAwait(true);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id = {roleId} can not be found ";
                return View("NotFound");
            }
            var model = new List<UserRoleVM>();
            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleVM
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if(await userManager.IsInRoleAsync(user,role.Name).ConfigureAwait(true))
                {
                    userRoleViewModel.isSelected = true;
                }
                else
                {
                    userRoleViewModel.isSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleVM> model,string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId).ConfigureAwait(true);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role With Id = {roleId} can not be found ";
                return View("NotFound");
            }
            for(int i=0;i<model.Count;i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId).ConfigureAwait(true);
                IdentityResult result = null;
                if(model[i].isSelected && !(await userManager.IsInRoleAsync(user,role.Name).ConfigureAwait(true)))
                {
                   result = await userManager.AddToRoleAsync(user,role.Name).ConfigureAwait(true);
                }
                else if(!model[i].isSelected && await userManager.IsInRoleAsync(user, role.Name).ConfigureAwait(true))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name).ConfigureAwait(true);
                }
                else
                {
                    continue;
                }
                if(result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("Edit", new { Id = roleId });

                }
            }
            return RedirectToAction("Edit", new { Id = roleId });
        }
    }
}