﻿using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Abstractions.Helper
{
    public class GetUserService : Controller /*PageModel*/
    {
        private readonly IHttpContextAccessor _ctxAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private ApplicationUser user;
        private string imagePath;

        public GetUserService(IHttpContextAccessor ctxAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _ctxAccessor = ctxAccessor;
            _userManager = userManager;
            _signInManager = signInManager;
        
        }

        public async Task<ApplicationUser> getUser()
        {
            var AspNetUserId = _userManager.GetUserId(User);
            var uid = User.Identity.Name;
            //  System.Security.Claims.ClaimsPrincipal currentUser = User;
            var AspNetUser = await _userManager.FindByNameAsync(uid).ConfigureAwait(true);
            user = AspNetUser;
            imagePath = user.ImagePath;
            return AspNetUser;
        }
        public  string ImagePath()

        {
            
            return imagePath;
        }

        //public Task<ApplicationUser> UserCurrent
        //{
        //    get
        //    {
        //        System.Security.Claims.ClaimsPrincipal currentUser = User;

        //        return User;
        //        //ApplicationUser user = userManager.GetUserAsync(claimsPrincipal);
        //        //string firstName = user.ImagePath;
        //        //// // // var uid = signInManager.IsSignedIn(User);
        //        //var userid = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        //        //////  //var user =  userManager.GetUserAsync(HttpContext.User);
        //        //var user = userManager.FindByIdAsync(userid);
        //        //return user;
        //        //var ImagePath = user.ImagePath;
        //        //if(ImagePath==null)
        //        //{
        //        //    return "\\user\\img\\default.jpg";
        //        //}
        //        //else
        //        //{
        //        //    return ImagePath;
        //        //}
        //    }
        //}
        //
    }
}
