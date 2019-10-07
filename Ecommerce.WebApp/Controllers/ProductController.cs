using System;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Ecommerce.Models.APIViewModels;

namespace Ecommerce.WebApp.Controllers
{
    public class ProductController : Controller
    {
        private IProductManager _productManager;
        private IMapper _mapper;
        public ProductController(IProductManager productManager, IMapper mapper)
        {
            _productManager = productManager;
            _mapper = mapper;
        }
        public IActionResult Index(string searchBy, string search ) //Search Facilities
        {
            if (searchBy == "Price")
            {
              //VwBg();
                return View(_productManager.GetByPrice(Convert.ToDouble(search)));
            }
            else if (searchBy == "Name")
            {
              //VwBg();
                return View(_productManager.GetByName(search));
            }
            else if (searchBy == "CategoryName")
            {
              //VwBg();
                return View(_productManager.GetByCategory(search));
            }
           

            var model = _productManager.GetAll();

          //VwBg();
            return View(model);
        }
        //public void VwBg()
        //{
        //    var cart = SessionHelper.GetObjectFromJson<List<Models.Item>>(HttpContext.Session, "cart");
        //    ViewBag.cart = cart;
        //    ViewBag.Total = cart.Sum(i => i.product.Price * i.Quantity);
        //    ViewBag.Count = cart.Sum(i => i.Quantity);
        //}

        public IActionResult show()
        {
            return View();
        }
        public IActionResult Create()
        {
            var products = _productManager.GetAll();
            var model = new ProductVM();
           // model.ProductList = products.ToList();
            //model.CategoryList = _productManager.list();
            PopulateDropdownList();
          //VwBg();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ExpireDate,CategoryId,CategoryList,CategoryName,IsActive,Orders,ImagePath")]ProductVM model, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
               
                var Product = _mapper.Map<Product>(model);

                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    Product.Image = ms.ToArray();
                   // ImageConverter converter = new ImageConverter();
                   // model.Image = (byte[])converter.ConvertTo(Image, typeof(byte[]));
                }
                //test
                var files = HttpContext.Request.Form.Files;
                foreach (var image in files)
                {
                    if (image != null && image.Length > 0)
                    {
                        var file = Image;
                       // var root = _appEnvironment.WebRootPath;
                       var root = "wwwroot\\";
                        var uploads = "uploads\\img";
                        if (file.Length > 0)
                        {
                            // you can change the Guid.NewGuid().ToString().Replace("-", "")
                            // to Guid.NewGuid().ToString("N") it will produce the same result
                            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                            using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                // This will produce uploads\img\fileName.ext
                                Product.ImagePath = Path.Combine(uploads, fileName);
                            }
                        }
                    }
                }
                //test end
                bool isAdded =  _productManager.Add(Product);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Saved Successfully!";
                    //model.ProductList = _productManager.GetAll().ToList();
                      //VwBg();
                        return RedirectToAction(nameof(Index),model.ProductList);
                    }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.ProductList = _productManager.GetAll().ToList();
            model.CategoryList = _productManager.list().ToList();
          //VwBg();
            return View(model);
        }

        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {                                      
            var category = _productManager.list();
            // category.Prepend<Category>(i=>(i.Name = "Select" , i.Id=0));
            Category i = new Category
            {
                Id = 0,
                Name = "Select"
            };
            category.Prepend(i);
            ViewBag.SelectList = new SelectList(category, "Id", "Name", selectList);
        }
        public PartialViewResult ProductListPartial()
        {
            var products = _productManager.GetAll();
            return PartialView("Product/_ProductList", products);
        }
        public IActionResult CardView()
        {
            var products = _productManager.GetAll();
          //VwBg();
            return View("Product/_cardView", products);
        }
        public IActionResult _CardView()
        {
            var products = _productManager.GetAll();
            return View( products);
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }


            var Product = _productManager.GetById((Int64)Id);
            PopulateDropdownList(Product.CategoryId);
            //ProductVM aProduct = _mapper.Map<ProductVM>(Product);
            Item aProduct = _mapper.Map<Item>(Product);
            aProduct.product = _mapper.Map<Product>(Product);
            if (Product == null)
            {
                return NotFound();
            }

            //  aProduct.ProductList = _productManager.GetAll().ToList();
            //VwBg();
            return View(aProduct);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
        

            var Product = _productManager.GetById((Int64)Id);
            PopulateDropdownList(Product.CategoryId);
            ProductVM aProduct = _mapper.Map<ProductVM>(Product);
            if (Product == null)
            {
                return NotFound();
            }

            aProduct.ProductList = _productManager.GetAll().ToList();
          //VwBg();
            return View(aProduct);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long Id,[Bind("Id,Name,Price,ExpireDate,CategoryId,CategoryList,CategoryName,IsActive,Orders,Image,ImagePath")]ProductVM Product, IFormFile Image)
        {
            if (Id != Product.Id)
            {
                return NotFound();
            }
       
            if(Image!=null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    //if(Image.Length<2048)
                    //{
                        Product.Image = ms.ToArray();
                    //}
                    var files = HttpContext.Request.Form.Files;
                    foreach (var image in files)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var file = Image;
                            // var root = _appEnvironment.WebRootPath;
                            var root = "wwwroot\\";
                            var uploads = "uploads\\img";
                            if (file.Length > 0)
                            {
                                // you can change the Guid.NewGuid().ToString().Replace("-", "")
                                // to Guid.NewGuid().ToString("N") it will produce the same result
                                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);

                                using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    // This will produce uploads\img\fileName.ext
                                    Product.ImagePath = Path.Combine(uploads, fileName);
                                }
                            }
                        }
                    }

                    // ImageConverter converter = new ImageConverter();
                    // model.Image = (byte[])converter.ConvertTo(Image, typeof(byte[]));
                }
              
            }
            else
            {
                Product.Image = Product.Image;
                Product.ImagePath = Product.ImagePath;
            }
            if (ModelState.IsValid)
            {
                var aProduct = _mapper.Map<Product>(Product);
                aProduct.Image = Product.Image;
                aProduct.ImagePath = Product.ImagePath;

                bool isUpdated = _productManager.Update(aProduct);
                if (isUpdated)
                {
                    var Products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                  //VwBg();
                    return View("Index", Products);

                }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            Product.ProductList = _productManager.GetAll().ToList();
          //VwBg();
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
        public IActionResult GetProductPartial(long id)
        {
            var product = _productManager.GetById(id);
            if (product == null)
            {
                return null;
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return PartialView("Product/_ProductDetails", productDto);
        }
    }
}