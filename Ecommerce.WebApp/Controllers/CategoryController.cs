using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models.RazorViewModels.Category;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private ICategoryManager _categoryManager;
        private IMapper _mapper;
        public CategoryController(ICategoryManager categoryManager, IMapper mapper)
        {
            _categoryManager = categoryManager;
            _mapper = mapper;
        }
        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var category = _categoryManager.GetAll();
            Category p = new Category
            {
                Id = 0,
                Name = "Select a Category",
                ParentId = 0
            };
            category.Clear();
            category.Add(p);

            var pp = _categoryManager.GetAll();
            foreach (var prod in pp)
            {
                category.Add(prod);
            }

            ViewBag.SelectList = new SelectList(category, "Id", "Name", selectList);
        }
        // GET: Category
        public ActionResult Index()
        {
            var model = _categoryManager.GetAll();
           
            return View(model);
           
        }

        // GET: Category/Details/5
        public ActionResult Details(long id)
        {
            return View();
        }

        [Authorize]
        // GET: Category/Create
        public ActionResult Create()
        {
            var model = new CategoryVM();
            PopulateDropdownList();
            return View();
        }

        // POST: Category/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                  if(model.ParentId == 0)
                {
                    model.ParentId = null;
                }
                var Category = _mapper.Map<Category>(model);
                bool isAdded = _categoryManager.Add(Category);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                    //model.ProductList = _categoryManager.GetAll().ToList();
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

        // GET: Category/Edit/5
        [Authorize]
        public ActionResult Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var Category = _categoryManager.GetById((id));
            PopulateDropdownList(Category.ParentId);
            CategoryVM aCategory = _mapper.Map<CategoryVM>(Category);
            if (aCategory == null)
            {
                return NotFound();
            }

           // aProduct.ProductList = _categoryManager.GetAll().ToList();
            //VwBg();
            return View(aCategory);
        }

        // POST: Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(long id, CategoryVM model)
        {
            if (ModelState.IsValid)
            {
                var aCategory = _mapper.Map<Category>(model);
                //aCategory.Name = model.Name;
                //aCategory.ParentId = model.ParentId;

                bool isUpdated = _categoryManager.Update(aCategory);
                if (isUpdated)
                {
                    var Categories = _categoryManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    //VwBg();
                    return View("Index", Categories);

                }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            model.Categories = _categoryManager.GetAll().ToList();
            //VwBg();
          //  return View(Product);
            return View(model);
        }

        // GET: Category/Delete/5
        [Authorize]
        public ActionResult Delete(long id)
        {
            var Product = _categoryManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _categoryManager.Remove(Product);
                if (isDeleted)
                {
                    var categories = _categoryManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    //VwBg();
                    return View("Index", categories);
                }

            }
           
            return RedirectToAction(nameof(Index));
        }

      
    }
}