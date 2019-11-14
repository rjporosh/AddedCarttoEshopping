using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.ProductVariants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
    public class ProductVariantsController : Controller
    {
        private ISizeManager _sizeManager;
        private IProductManager _productManager;
        private IProductVariantsManager _productVariantsManager;
        private IMapper _mapper;
        public ProductVariantsController(ISizeManager sizeManager, IMapper mapper, IProductManager productManager,IProductVariantsManager productVariantManager)
        {
            _sizeManager = sizeManager;
            _mapper = mapper;
            _productManager = productManager; //Dropdown List
            _productVariantsManager = productVariantManager;
        }

        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _sizeManager.GetAll();
            ViewBag.SelectList = new SelectList(product, "Id", "Name", selectList);
        }
        // GET: Stock
        public ActionResult Index()
        {
            var variant = _productVariantsManager.GetAll();
            var products = _productManager.GetAll();
            var model = new ProductVariantsVM();
            model.ProductList = products.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(variant);

        }


        // GET: Category/Create
        public ActionResult Create()
        {
            var size = _sizeManager.GetAll();
            var products = _productManager.GetAll();
            var model = new ProductVariantsVM();
            model.ProductList = products.ToList();
            PopulateDropdownList(); /*Dropdown List Binding*/
            return View(model);
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductVariantsVM model)
        {
            if (ModelState.IsValid)
            {
                var size = _mapper.Map<ProductVariants>(model); //AutoMapper
                //_sizeManager.Add(model.Size);
                bool isAdded = _productVariantsManager.Add(size);
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


            var Stock = _productVariantsManager.GetById((id));
             PopulateDropdownList(Stock.SizeId);
            var aStock = _mapper.Map<ProductVariantsVM>(Stock);
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
        public ActionResult Edit(long id, ProductVariantsVM model)
        {
            if (ModelState.IsValid)
            {
                var aStock = _mapper.Map<ProductVariants>(model);
                //aCategory.Name = model.Name;
                //aCategory.ParentId = model.ParentId;
                 PopulateDropdownList(aStock.SizeId);
               // _sizeManager.Update(aStock.Size);
                bool isUpdated = _productVariantsManager.Update(aStock);

                if (isUpdated)
                {
                    var Stocks = _productVariantsManager.GetAll();
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
           
            var productvariants = _productVariantsManager.GetById(id);
            if(productvariants.SizeId>0 && productvariants.SizeId!=null)
            {
               var size = _sizeManager.Find(productvariants.SizeId);
                _sizeManager.Remove(size);
            }
           
            if (ModelState.IsValid)
            {
                //var product = _productManager.GetById((long)stock.ProductId);
               
                bool isDeleted = _productVariantsManager.Remove(productvariants);
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