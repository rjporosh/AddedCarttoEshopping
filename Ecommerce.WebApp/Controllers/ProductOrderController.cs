using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.ProductOrder;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class ProductOrderController : Controller
    {
        private IProductOrderManager _productOrderManager;
        private IMapper _mapper;

        public ProductOrderController(IProductOrderManager productorderManager, IMapper mapper)
        {
            _productOrderManager = productorderManager;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var po = _productOrderManager.GetAll();
            return View(po);
        }
        public IActionResult Create()
        {
            var orders = _productOrderManager.GetAll();

            var model = new ProductOrderVM();
           
            return View(orders);
        }

        [HttpPost]
        public IActionResult Create([Bind("OrderId,ProductId")]ProductOrderVM model)
        {

            // model.OrderNo = 
            if (ModelState.IsValid)
            {
                var productOrder = _mapper.Map<ProductOrder>(model);
             

                    bool isAdded = _productOrderManager.Add(productOrder);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Order Saved Successfully!";
                    }
                
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

           var orders = _productOrderManager.GetAll();
            return View(orders);
        }
    }
}