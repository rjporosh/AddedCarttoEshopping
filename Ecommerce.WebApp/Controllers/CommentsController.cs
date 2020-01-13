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

namespace Ecommerce.WebApp.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly ICommentsManager _commentManager;


        //class constructor
        public CommentsController(UserManager<ApplicationUser> userManager, ICommentsManager commentManager)
        {
            _userManager = userManager;
            _commentManager = commentManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult LeaveComment(CommentVM model)
        {
            JsonResult result = new JsonResult(model);
            try
            {

                var comment = new Comment();
                comment.Comments = model.Comments;
                comment.ProductId = model.ProductId;
                comment.Date = DateTime.Now;

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                comment.AspNetUserId = _userManager.GetUserId(User);
                var res = _commentManager.Add(comment);
                result.Value = new { Success = res };
                return result;
            }
            catch (Exception ex)
            {
                result.Value = new { Success = false, Message = ex.Message };
                return result;
            }
        }
    }
}