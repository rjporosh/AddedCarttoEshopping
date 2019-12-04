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
        private IStockManager _stockManager;
        private IProductVariantsManager _productVariantManager;
        private ISizeManager _sizeManager;
        private IMapper _mapper;
        public ProductController(IProductManager productManager, IMapper mapper, IStockManager stockManager, IProductVariantsManager productVariantManager, ISizeManager sizeManager)
        {
            _sizeManager = sizeManager;
            _productManager = productManager;
            _productVariantManager = productVariantManager;
            _stockManager = stockManager;
            _mapper = mapper;   
        }
        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var category = _productManager.list();
            Category p = new Category
            {
                Id = 0,
                Name = "Select a Category"
            };
            category.Clear();
            category.Add(p);

            var pp = _productManager.list();
            foreach (var prod in pp)
            {
                category.Add(prod);
            }
            ViewBag.SelectList = new SelectList(category, "Id", "Name", selectList);
        }
        private void PopulateDropdownList1(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _productManager.GetAll();
            Product p = new Product
            {
                Id = 0,
                Name = "No Parent"
            };
            product.Clear();
            product.Add(p);

            var pp = _productManager.GetAll();
            foreach (var prod in pp)
            {
                product.Add(prod);
            }

            ViewBag.SelectList1 = new SelectList(product, "Id", "Name", selectList);
        }
        private void StockPopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var stock = _stockManager.GetAll();
            Stock p = new Stock
            {
                Id = 0,
                ProductId = null,
                Quantity = (Decimal)0.00,
                Product = new Product
                {
                    Name = "No Stock"
                }
            };
            stock.Clear();
            stock.Add(p);

            var pp = _stockManager.GetAll();
            foreach (var prod in pp)
            {
                stock.Add(prod);
            }
            ViewBag.StockSelectList = new SelectList(stock, "Id", "Product.Name", selectList);
        }
        private void ProductVariantsPopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var ProductVariants = _productVariantManager.GetAll();
            ProductVariants p = new ProductVariants
            {
                Id = null,
                Name = "No Variant"
            };
            ProductVariants.Clear();
            ProductVariants.Add(p);
            var pp = _productVariantManager.GetAll();
            foreach (var prod in pp)
            {
                ProductVariants.Add(prod);
            }

            ViewBag.ProductVariantsSelectList = new SelectList(ProductVariants, "Id", "Name", selectList);
        }
        private void SizePopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            var size = _sizeManager.GetAll();

            Models.Size p = new Models.Size
            {
                Id = null,
                Name = "No Size"
            };
            size.Clear();
            size.Add(p);

            var pp = _sizeManager.GetAll();
            foreach (var prod in pp)
            {
                size.Add(prod);
            }
            ViewBag.SizeSelectList = new SelectList(size, "Id", "Name", selectList);
        }
        public IActionResult Index(string searchBy, string search ) //Search Facilities
        {
            if (searchBy == "Price")
            {
                return View(_productManager.GetByPrice(Convert.ToDouble(search)));
            }
            else if (searchBy == "Name")
            {
                return View(_productManager.GetByName(search));
            }
            else if (searchBy == "CategoryName")
            {
              return View(_productManager.GetByCategory(search));
            }
            var model = _productManager.GetAll();
            return View(model);
        }
      
        public IActionResult show()
        {
            return View();
        }
        public IActionResult Create()
        {
            var products = _productManager.GetAll();
            var model = new ProductVM();
            model.ProductList = products.ToList();
            model.CategoryList = _productManager.list();
            PopulateDropdownList();
            PopulateDropdownList1();
            StockPopulateDropdownList();
            ProductVariantsPopulateDropdownList();
            SizePopulateDropdownList();
            model.Stocks = new Stock();
            model.Stocks.Quantity = 0;
            model.Stocks.Unit = "nothing";
            model.ParentId = 0;
            model.ExpireDate = DateTime.Now;
            model.IsActive = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ExpireDate,Description,BuyCost,ProductCode,CategoryId,CategoryList,CategoryName,IsActive,Orders,DiscountPrice,Image,ImagePath,ProductVariantsId,StocksId,ParentId,ProductVariants,Parent,StocksQuantity,ProductVariantsSizeId,Size,ProductVariantsSize,StocksQuantity,StocksUnit,Stocks,DiscountPercent")]ProductVM model, IFormFile Image)
        {
            if (model.Stocks.Quantity == null)
            {
                model.Stocks.Quantity = 0;
            }
            if (model.Stocks.Unit == null)
            {
                model.Stocks.Unit = "Nothing";
            }
            if (Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    //if(Image.Length<2048)
                    //{
                    model.Image = ms.ToArray();
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
                                    await file.CopyToAsync(fileStream).ConfigureAwait(true);
                                    // This will produce uploads\img\fileName.ext
                                    model.ImagePath = Path.Combine(uploads, fileName);
                                }
                            }
                        }
                    }
                }

            }
            else
            {
                model.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if (model.ParentId == null || model.ParentId < 0)
            {
                model.ParentId = 0;
            }
            if (model.Image == null || model.ImagePath == null)
            {
                model.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if (ModelState.IsValid)
            {

                var Product = _mapper.Map<Product>(model);
                bool isAdded = _productManager.Add(Product);
                long id = Product.Id;
                var p = _productManager.GetById(id);
                string pc = p.ParentId.ToString();
                pc = pc + (p.CategoryId)+(p.ProductVariantsId)+(p.Id);
                p.ProductCode = pc;
                _productManager.Update(p);
                if (isAdded)
                {
                    ViewBag.SuccessMessage = "Saved Successfully!";
                    model.ProductList = _productManager.GetAll().ToList();
                    return RedirectToAction(nameof(Index), model.ProductList);
                }
                else
                {
                    ViewBag.ErrorMessage = "Operation Failed!";
                }

                model.ProductList = _productManager.GetAll().ToList();
                model.CategoryList = _productManager.list().ToList();
            }
            return View(model);
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

                return View(products);
            }
            public IActionResult Details(long Id)
            {
                           var Product = _productManager.Find(Id);
                           Ecommerce.Abstractions.Helper.Item Item = new Ecommerce.Abstractions.Helper.Item
                                      {
                                         product = Product,
                                         ProductCategoryName = Product.Category.Name,
                                         Quantity = 2
                                       };
                           return View(Item);
            }
        

        public IActionResult Edit(int? Id)
           {
                if (Id == null)
                {
                    return NotFound();
                }


                var Product = _productManager.Find((Int64)Id);
                PopulateDropdownList(Product.CategoryId);
                PopulateDropdownList1(Product.ParentId);
                if (Product.Parent == null || Product.ParentId >= 0)
                {
                    if (Product.Parent == null)
                    {
                        PopulateDropdownList1();
                    }
                    else
                    {
                        PopulateDropdownList1(Product.ParentId);
                    }

                }
                if (Product.ProductVariants != null && Product.ProductVariantsId >= 0 || Product.ProductVariantsId == null)
                {
                    if (Product.ProductVariantsId == null)
                    {
                        SizePopulateDropdownList();
                        ProductVariantsPopulateDropdownList();
                    }
                    else
                    {
                        if (Product.ProductVariants.Size == null || Product.ProductVariants.SizeId >= 0)
                        {
                            if (Product.ProductVariants.Size == null)
                            {
                                SizePopulateDropdownList();
                            }
                            else
                            {
                                SizePopulateDropdownList(Product.ProductVariants.SizeId);
                                Product.ProductVariants.Size = _productManager.GetBySzId(Product.ProductVariants.SizeId);
                            }

                        }
                        ProductVariantsPopulateDropdownList(Product.ProductVariantsId);
                        Product.ProductVariants = _productManager.GetByPVId(Product.ProductVariantsId);
                    }

                }
                if (Product.Stocks == null || Product.Stocks.Id >= 0)
                {
                    if (Product.Stocks == null)
                    {
                        StockPopulateDropdownList();
                    }
                    else
                    {
                        Product.Stocks = _productManager.GetBySId(Product.Stocks.Id);
                    }
                }
                if (Product.ParentId == null)
                {
                    Product.ParentId = 0;
                }
                if (Product.Image == null || Product.ImagePath == null)
                {
                    Product.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
                }

                Product.IsActive = true;
                ProductVM aProduct = _mapper.Map<ProductVM>(Product);
                 aProduct.Image = Product.Image;
                if (Product == null)
                {
                    return NotFound();
                }

                aProduct.ProductList = _productManager.GetAll().ToList();
                return View(aProduct);
            }
        

      [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long Id, [Bind("Id,ImagePath,Image,Name,Description,BuyCost,DiscountPrice,ProductCode,Price,ExpireDate,CategoryId,ParentId,CategoryList,CategoryName,IsActive,Orders,Parent,Stocks,StocksQuantity,StocksUnit,ProductVariants,ProductVariantsId,ProductVariantsSize,Parent,Category,ProductVariantsSizeId,StocksQuantity,DiscountPercent")]ProductVM aProduct, IFormFile Image)
        {
          //  var Product = _mapper.Map<Product>(aProduct);
            if (Id != aProduct.Id)
            {
                return NotFound();
            }
            if (Image!=null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    //if(Image.Length<2048)
                    //{
                        aProduct.Image = ms.ToArray();
                    //}
                    var files = HttpContext.Request.Form.Files;
                    foreach (var image in files)
                    {
                        if (image != null && image.Length > 0)
                        {
                            var file = Image;
                            var root = "wwwroot\\";
                            var uploads = "uploads\\img";
                            if (file.Length > 0)
                            {
                                var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(file.FileName);
                                using (var fileStream = new FileStream(Path.Combine(root, uploads, fileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream).ConfigureAwait(true);
                                    aProduct.ImagePath = Path.Combine(uploads, fileName);
                                }
                            }
                        }
                    }

                }
              
            }
            else
            {
              
                if (aProduct.Image == null && aProduct.ImagePath == null)
                {
                    aProduct.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
                }
            }
            if (aProduct.ParentId == null || aProduct.ParentId < 0)
            {
                aProduct.ParentId = 0;
            }
            if (aProduct.ImagePath == null)
            {
                aProduct.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
           
            if (ModelState.IsValid)
            {
               var  Product = _mapper.Map<Product>(aProduct);
                bool isUpdated = _productManager.Update(Product);
                var pv = _productManager.GetByPVId(aProduct.ProductVariantsId);
                pv.Brand = aProduct.ProductVariants.Brand;
                pv.Color = aProduct.ProductVariants.Color;
                pv.Name = aProduct.ProductVariants.Name;
                pv.Type = aProduct.ProductVariants.Type;
                pv.SizeId = aProduct.ProductVariants.SizeId;
                pv.Size.Name = aProduct.ProductVariants.Size.Name;
                pv.Size.Code = aProduct.ProductVariants.Size.Code;
                _productVariantManager.Update(pv);
                var a = _productManager.GetById(Id);
                long? i = pv.SizeId;
                var sz = _sizeManager.Find(i);
                sz.Name = aProduct.ProductVariants.Size.Name;
                sz.Code = aProduct.ProductVariants.Size.Code;
                _sizeManager.Update(sz);
                var ast = _stockManager.check(Id);
                ast.Quantity = aProduct.Stocks.Quantity;
                ast.Unit = aProduct.Stocks.Unit;
                _stockManager.Update(ast);
                if (isUpdated)
                {
                    var Products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
                    return RedirectToAction(nameof(Index), Products);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            aProduct.ProductList = _productManager.GetAll().ToList();
            return View(aProduct);

        }
        public IActionResult Delete(long id)
        {
            var Product = _productManager.GetById(id);
            var variantsize = new Models.Size();
            var variant = new Models.ProductVariants();
            var stock = new Models.Stock();
            if(Product.Stocks != null)
            {
                stock = _productManager.GetBySId(Product.Stocks.Id);
                //_stockManager.Remove(stock);
            }
            if (Product.ProductVariantsId != null)
            {
                if (Product.ProductVariants.SizeId != null)
                {
                    variantsize = _productManager.GetBySzId(Product.ProductVariants.SizeId);
                }
                variant = _productManager.GetByPVId(Product.ProductVariantsId);
                variant.SizeId = null;
                _productVariantManager.Update(variant);
                variant = _productManager.GetByPVId(Product.ProductVariantsId);
              //  _productVariantManager.Remove(variant);
               // _sizeManager.Remove(variantsize);
            }
            if (ModelState.IsValid)
            {
               // _sizeManager.Remove(variantsize);
                
                bool isDeleted = _productManager.Remove(Product);
                _productVariantManager.Remove(variant);
                _stockManager.Remove(stock);
                 _sizeManager.Remove(variantsize);

                if (isDeleted)
                {
                    var products = _productManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    return View("Index", products);
                }

            }
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

