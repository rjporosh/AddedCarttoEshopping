using Ecommerce.Models;
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

namespace Ecommerce.WebApp.Controllers
{
    public class GetUserService : Controller
    {
        private readonly IHttpContextAccessor ctxAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private ApplicationUser nowuser;
        private string imagePath;

        public GetUserService(IHttpContextAccessor ctxAccessor, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.ctxAccessor = ctxAccessor;
            this.userManager = userManager;
            this.signInManager = signInManager;
        
        }
        public GetUserService(ApplicationUser user, string imgPath)
        {
            nowuser = user;
            imagePath = imgPath;
        }

        public ApplicationUser getUser()
        {
            //var AspNetUserId = userManager.GetUserId(User);
            //var uid = User.Identity.Name;
            ////  System.Security.Claims.ClaimsPrincipal currentUser = User;
            //var AspNetUser = await userManager.FindByNameAsync(uid).ConfigureAwait(true);
            //user = user;
            //imagePath = imgPath;
            return nowuser;
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
