using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Abstractions.Helper
{
    public class GetUserService : Controller /*PageModel*/
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
       
        //private ApplicationUser user;
        private IHttpContextAccessor contextAccessor;
        private readonly SignInManager<ApplicationUser> signinManager;

        //private string userId;
        //private string ImagePath;


        public GetUserService( UserManager<ApplicationUser> userManager, IHttpContextAccessor ctxAccessor,SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            contextAccessor = ctxAccessor;
            this.signinManager = signinManager;
        }


        public ApplicationUser getUser
        {
            get
            {
                var userId = _userManager.GetUserId(HttpContext.User);
               ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

                return user;
            }
        }
       
        public  string ImagePath
        {
            get
            {
                
                ApplicationUser user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                var path = user.ImagePath;
                return path;
            }
        }
        public string imgPath(string userName)
        {
            ApplicationUser user = _userManager.FindByNameAsync(userName).Result;
            return _userManager.FindByNameAsync(userName).Result.ImagePath;
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
