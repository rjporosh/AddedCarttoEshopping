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

namespace Ecommerce.Abstractions.Helper
{
    public class GetUserService : PageModel
    {
        //private readonly IHttpContextAccessor ctxAccessor;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public GetUserService(/*IHttpContextAccessor ctxAccessor,*/ UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            //this.ctxAccessor = ctxAccessor;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
