using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryManager _productManager;
        private IMapper _mapper;
        public CategoryController(ICategoryManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }
        public IActionResult Index(string searchBy, string search) //Search Facilities
        {
          


            var model = _productManager.GetAll();

        
            return View(model);
        }
      

      
        public IActionResult Create()
        {
            var products = _productManager.GetAll();
            var model = new CategoryVM();
          
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryVM model)
        {
            if (ModelState.IsValid)
            {

                var Product = _mapper.Map<Category>(model);

              
                bool isAdded = _productManager.Add(Product);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                   // model. Products = _productManager.GetAll().ToList();
                    //VwBg();
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

           
            return View(model);
        }

        public PartialViewResult CategoryListPartial()
        {
            var products = _productManager.GetAll();
            return PartialView("Category/_CategoryList", products);
        }
      
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }


            var Product = _productManager.GetById((Int64)Id);
         
            CategoryVM aProduct = _mapper.Map<CategoryVM>(Product);
            if (Product == null)
            {
                return NotFound();
            }

           
            return View(Product);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }


            var Product = _productManager.GetById((Int64)Id);
         
            CategoryVM aProduct = _mapper.Map<CategoryVM>(Product);
            if (Product == null)
            {
                return NotFound();
            }

          // aProduct.Products = _productManager.GetAll().ToList();
            //VwBg();
            return View(Product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long Id,CategoryVM Product)
        {
            if (Id != Product.Id)
            {
                return NotFound();
            }

          
            if (ModelState.IsValid)
            {
                var aProduct = _mapper.Map<Category>(Product);
              
                bool isUpdated = _productManager.Update(aProduct);
                if (isUpdated)
                {
                    var Products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                  
                    return View("Index", Products);

                }
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
           
            return View(Product);

        }


        public IActionResult Delete(long id)
        {
            var Product = _productManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _productManager.Remove(Product);
                if (isDeleted)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    //VwBg();
                    return View("Index", products);
                }

            }
            //VwBg();
            return RedirectToAction(nameof(Index));
        }
     
    }
}