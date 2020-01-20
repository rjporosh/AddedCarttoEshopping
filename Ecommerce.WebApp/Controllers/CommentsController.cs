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
            //var userId = _userManager.GetUserId(HttpContext.User);
            //ApplicationUser user = _userManager.FindByIdAsync(userId).Result;
            var model = _commentManager.GetAll();
            return View(model);
        }
        [HttpPost]
        public  JsonResult LeaveComment(CommentVM model)
        {
            JsonResult result = new JsonResult(model);
            try
            {
                var comment = new Comment();
                comment.Comments = model.Comments;
                comment.ProductId = model.ProductId;
                comment.Rating = model.Rating;
                comment.Date = DateTime.Now;
                

                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                comment.AspNetUserId = _userManager.GetUserId(User);
                var userId = _userManager.GetUserId(HttpContext.User);
                ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

               // comment.User = _userManager.FindByIdAsync(userId).Result;
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
        [Authorize]
        //[Route("Add/{Id}")]
        [HttpGet]
        public IActionResult Add()
        {
           var comment = new Comment();
           // comment.ProductId = Id;
            return View();
        }
        //[Route("Add/{Id}")]
        [Authorize]
        [HttpPost]
        public IActionResult Add( CommentVM model)
        {
            var comment = new Comment();
            comment.Comments = model.Comments;
            comment.ProductId = model.ProductId;
            comment.Rating = model.Rating;
            comment.Date = DateTime.Now;


            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            comment.AspNetUserId = _userManager.GetUserId(User);
            var userId = _userManager.GetUserId(HttpContext.User);
            ApplicationUser user = _userManager.FindByIdAsync(userId).Result;

           comment.AspNetUser = user;
            var res = _commentManager.Add(comment);
           if(res== true)
            {
                return View("_cardView", "Product");
            }
           else
            {
                return View("Add","Comments");
            }
            
        }
        [HttpGet]
        public IActionResult Edit (long Id)
        {
            var model = _commentManager.GetById(Id);
            var comment = new CommentVM();
            comment.Id = model.Id;
            comment.AspNetUserId = model.AspNetUserId;
            comment.AspNetUser = model.AspNetUser;
            comment.Comments = model.Comments;
            comment.Reply = model.Reply;
            comment.Rating = model.Rating;
            // ViewBag.model = model;
            //var pid = model.ProductId;
            //return View("Details?id=${pid}#Review", "Product");
            return View(comment);
        }
        [HttpPut]
        public IActionResult Edit(CommentVM model)
        {
            var comment = new Comment();
            comment.Id = model.Id;
            comment.AspNetUserId = model.AspNetUserId;
            comment.AspNetUser = model.AspNetUser;
            comment.Comments = model.Comments;
            comment.Reply = model.Reply;
            comment.Rating = model.Rating;
            bool updated =_commentManager.Update(comment);
            if(updated)
            {
                ViewBag.message = "Successfully Updated";
                return RedirectToAction("_cardView","Product");
            }
            else
            {
                ViewBag.message = "Failed";
            }
            return View();
        }
        [HttpPut]
        public IActionResult Update(CommentVM model)
        {
            var comment = new Comment();
            comment.Id = model.Id;
            comment.ProductId = model.ProductId;
            comment.Product = model.Product;
            comment.AspNetUserId = model.AspNetUserId;
            comment.AspNetUser = model.AspNetUser;
            comment.Comments = model.Comments;
            comment.Reply = model.Reply;
            comment.Rating = model.Rating;
            bool updated = _commentManager.Update(comment);
            if (updated)
            {
                ViewBag.message = "Successfully Updated";
                return RedirectToAction("Details/{model.ProductId}#Comments","Product");
            }
            else
            {
                ViewBag.message = "Failed";
            }
            return RedirectToAction("Index", "Product");
        }
        [HttpDelete]
        public IActionResult Remove(long id)
        {
            var comment = _commentManager.GetById(id);
            _commentManager.Remove(comment);
            return RedirectToAction("_cardView", "Product");
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}