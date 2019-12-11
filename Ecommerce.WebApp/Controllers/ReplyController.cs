using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize]
    public class ReplyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}