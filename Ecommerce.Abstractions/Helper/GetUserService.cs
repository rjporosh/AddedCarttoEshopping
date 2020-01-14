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
       
        private ApplicationUser user = new ApplicationUser();
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


        public ApplicationUser getUser(string uName)
        {
                //var userId = _userManager.GetUserId(HttpContext.User);
                //user = _userManager.FindByIdAsync(userId).Result;
                user = _userManager.FindByNameAsync(uName).Result;
                return user;
        }
       
        
        public string imgPath(string userName)
        {
           user = _userManager.FindByNameAsync(userName).Result;
            return _userManager.FindByNameAsync(userName).Result.ImagePath;
        }

    }
}
