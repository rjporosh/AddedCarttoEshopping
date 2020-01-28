using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Stock;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private IStockManager _stockManager;
        private IProductManager _productManager;
        private IMapper _mapper;
        public StockController(IStockManager stockManager, IMapper mapper, IProductManager productManager)
        {
            _stockManager = stockManager;
            _mapper = mapper;
            _productManager = productManager; //Dropdown List
        }

        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _productManager.GetAll();
            Product p = new Product
            {
                Id = 0,
                //StocksId = null,
                Name = "No Product"
            };
            // product.Prepend(p);
            //  product.Append(p);
            product.Clear();
            product.Add(p);

            var pp = _productManager.GetAll();
            foreach (var prod in pp)
            {
                product.Add(prod);
            }
            ViewBag.SelectList = new SelectList(product, "Id", "Name", selectList);
        }
        [Authorize]
        // GET: Stock
        public ActionResult Index()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockVM();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(stocks);

        }

        [Authorize(Roles = "Admin")]
        // GET: Category/Create
        public ActionResult Create()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockVM();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        // POST: Category/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ProductId,Product,Quantity,Unit")]StockVM model)
        {
            if (ModelState.IsValid)
            {
                var stock = _mapper.Map<Stock>(model); //AutoMapper

                var product = _stockManager.GetByPId(stock.ProductId);
                //if(product.StocksId == null)
                //{
                    bool isAdded = _stockManager.Add(stock);
                    //product.StocksId = stock.Id;
                    _productManager.Update(product);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Saved Successfully!";
                    }
               // }
                else
                {
                    ViewBag.SuccessMessage = "Stocks Already Exist!";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.StockList = _stockManager.GetAll().ToList();
            PopulateDropdownList(model.ProductId); /*Dropdown List Binding*/
            return View(model);
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long id)
        {
         
          
            var Stock = _stockManager.GetById((id));
           
            
            
            StockVM aStock = _mapper.Map<StockVM>(Stock);
            PopulateDropdownList(aStock.ProductId);
            var product = _stockManager.GetByPId(aStock.ProductId);
            if (aStock == null)
            {
                return NotFound();
            }

            // aProduct.ProductList = _categoryManager.GetAll().ToList();
            //VwBg();
           
            return View(aStock);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(long id, [Bind("Id,ProductId,Product,Quantity,Unit")]StockVM model)
        {
            if (ModelState.IsValid)
            {
                var aStock = _mapper.Map<Stock>(model);
               //// var product = _stockManager.GetByPId(aStock.ProductId);
               //// //product.StocksId = aStock.Id;
               //// _productManager.Update(product);
               //// //aCategory.Name = model.Name;
               //// //aCategory.ParentId = model.ParentId;
               ////// PopulateDropdownList();
                bool isUpdated = _stockManager.Update(aStock);
                if (isUpdated)
                {
                    var Stocks = _stockManager.GetAll();
                  
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    //VwBg();
                    return View("Index", Stocks);

                }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
          model.StockList = _stockManager.GetAll().ToList();
            //VwBg();
            //  return View(Product);
            return View(model);
        }

        // GET: Category/Delete/5
       
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(long id)
        {
            var stock = _stockManager.GetById(id);
            //if (ModelState.IsValid)
            //{
                //var product = _productManager.GetById((long)stock.ProductId);
                bool isDeleted = _stockManager.Remove(stock);
              //  bool isDeletedWithProduct = _productManager.Remove(product);
                if (isDeleted)
                {
                    var stocks = _stockManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    //VwBg();
                    return View("Index", stock);
                }

            //}

            return RedirectToAction(nameof(Index));
        }

    }
}