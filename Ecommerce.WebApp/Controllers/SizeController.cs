using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Size;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
    public class SizeController : Controller
    {
        private ISizeManager _sizeManager;
        private IProductManager _productManager;
        private IMapper _mapper;
        public SizeController(ISizeManager sizeManager, IMapper mapper, IProductManager productManager)
        {
            _sizeManager = sizeManager;
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
            var size = _sizeManager.GetAll();
            var products = _productManager.GetAll();
            var model = new SizeVM();
            model.ProductList = products.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(size);

        }


        // GET: Category/Create
        public ActionResult Create()
        {
            var size = _sizeManager.GetAll();
            var products = _productManager.GetAll();
            var model = new SizeVM();
            model.ProductList = products.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SizeVM model)
        {
            if (ModelState.IsValid)
            {
                var size = _mapper.Map<Size>(model); //AutoMapper

                bool isAdded = _sizeManager.Add(size);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                }


            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.ProductList = _productManager.GetAll().ToList();
           // PopulateDropdownList(model.ProductId); /*Dropdown List Binding*/
            return View(model);
        }

        // GET: Category/Edit/5
        public ActionResult Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var Stock = _sizeManager.GetById((id));
           // PopulateDropdownList(Stock.ProductId);
            SizeVM aStock = _mapper.Map<SizeVM>(Stock);
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
        public ActionResult Edit(long id, SizeVM model)
        {
            if (ModelState.IsValid)
            {
                var aStock = _mapper.Map<Size>(model);
                //aCategory.Name = model.Name;
                //aCategory.ParentId = model.ParentId;
                // PopulateDropdownList();
                bool isUpdated = _sizeManager.Update(aStock);
                if (isUpdated)
                {
                    var Stocks = _sizeManager.GetAll();
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
            model.ProductList = _productManager.GetAll().ToList();
            //VwBg();
            //  return View(Product);
            return View(model);
        }

        // GET: Category/Delete/5
        public ActionResult Delete(long id)
        {
            var stock = _sizeManager.GetById(id);
            if (ModelState.IsValid)
            {
                //var product = _productManager.GetById((long)stock.ProductId);
                bool isDeleted = _sizeManager.Remove(stock);
               // bool isDeletedWithProduct = _productManager.Remove(product);
                if (isDeleted)
                {
                    var categories = _sizeManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    //VwBg();
                    return View("Index", categories);
                }

            }

            return RedirectToAction(nameof(Index));
        }

    }
}