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
        private IOrderManager _orderManager;
        private IProductManager _productManager;
        private IMapper _mapper;

        public ProductOrderController(IProductOrderManager productorderManager, IMapper mapper, IOrderManager orderManager, IProductManager productManager)
        {
            _productOrderManager = productorderManager;
            _productManager = productManager;
            _orderManager = orderManager;
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
           
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind("OrderId,ProductId,Status,Quantity,Unit,Product,Order")]ProductOrderVM model)
        {

            // model.OrderNo = 
            if (ModelState.IsValid)
            {
                var productOrder = _mapper.Map<ProductOrder>(model);
             

                    bool isAdded = _productOrderManager.Add(productOrder);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "ProductOrder Saved Successfully!";
                    }
                
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

           var productorders = _productOrderManager.GetAll();
            return View("Index",productorders);
        }

        //public ActionResult Edit(long id)
        //{
        //    var productOrder = _productOrderManager.GetById((id));
        //    ProductOrderVM model = _mapper.Map<ProductOrderVM>(productOrder);
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(model);
        //}

        //// POST: Category/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind("Id,ProductId,OrdertId,Quantity,Unit,Product,Order")]ProductOrderVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var aStock = _mapper.Map<ProductOrder>(model);
        //        var product = _productManager.GetById(model.ProductId);
        //        var order = _orderManager.GetById(model.OrderId);
        //       // _productOrderManager.Update(aStock);
        //        bool isUpdated = _productOrderManager.Update(aStock);
        //        if (isUpdated)
        //        {
        //            var Stocks = _productOrderManager.GetAll();

        //            ViewBag.SuccessMessage = "Updated Successfully!";
        //            //VwBg();
        //            return View("Index", Stocks);

        //        }
        //        //}
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = "Update Failed!";
        //    }
        //   var gs = _productOrderManager.GetAll();
        //    //VwBg();
        //    //  return View(Product);
        //    return View(gs);
        //}

        //// GET: Category/Delete/5
        //public ActionResult Delete(long id)
        //{
        //    var stock = _productOrderManager.GetById(id);
        //    if (ModelState.IsValid)
        //    {
        //        bool isDeletedWithProduct = _productOrderManager.Remove(stock);
        //        if (isDeletedWithProduct)
        //        {
        //            var stocks = _productOrderManager.GetAll();
        //            ViewBag.SuccessMessage = "Deleted Successfully.!";
        //            //VwBg();
        //            return View("Index", stocks);
        //        }

        //    }

        //    return RedirectToAction(nameof(Index));
        //}
    }
}