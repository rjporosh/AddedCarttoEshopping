using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Stock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
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
            ViewBag.SelectList = new SelectList(product, "Id", "Name", selectList);
        }
        // GET: Stock
        public ActionResult Index()
        {
            var stocks = _stockManager.GetAll();
            var model = new StockVM();
            model.StockList = stocks.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(stocks);

        }

      
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StockVM model)
        {
            if (ModelState.IsValid)
            {
                var stock = _mapper.Map<Stock>(model); //AutoMapper

                bool isAdded = _stockManager.Add(stock);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
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
        public ActionResult Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

          
            var Stock = _stockManager.GetById((id));
            PopulateDropdownList(Stock.ProductId);
            StockVM aStock = _mapper.Map<StockVM>(Stock);
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
        public ActionResult Edit(long id, StockVM model)
        {
            if (ModelState.IsValid)
            {
                var aStock = _mapper.Map<Stock>(model);
                //aCategory.Name = model.Name;
                //aCategory.ParentId = model.ParentId;
               // PopulateDropdownList();
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
        public ActionResult Delete(long id)
        {
            var Product = _stockManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _stockManager.Remove(Product);
                if (isDeleted)
                {
                    var categories = _stockManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    //VwBg();
                    return View("Index", categories);
                }

            }

            return RedirectToAction(nameof(Index));
        }

    }
}