﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers.Api
{
    public class ProductVariantApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}