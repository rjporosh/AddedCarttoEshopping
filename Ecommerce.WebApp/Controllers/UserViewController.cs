using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Abstractions.BLL;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class UserViewController : Controller
    {
        private IProductManager _productManager;
        public UserViewController(IProductManager productManager)
        {
            _productManager = productManager;
        }
        public IActionResult Index()
        {
            var model = _productManager.GetAll();
            return View(model);
        }
    }
}