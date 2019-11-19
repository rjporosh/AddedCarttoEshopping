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
            _sizeManager = sizeManager; _productManager = productManager;
            _productVariantManager = productVariantManager;
            _stockManager = stockManager;
            _mapper = mapper;   
        }
        private void PopulateDropdownList1(object selectList = null) /*Dropdown List Binding*/
        {
            var product = _productManager.GetAll();
            Product p = new Product
            {
                Id = 0,
                Name = "No Parent"
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
                    Name = "No Stock",
                    StocksId = 0
                }
            };
            // product.Prepend(p);
            //  product.Append(p);
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
            // product.Prepend(p);
            //  product.Append(p);
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
            // product.Prepend(p);
            //  product.Append(p);
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
            PopulateDropdownList1();
            StockPopulateDropdownList();
            ProductVariantsPopulateDropdownList();
            SizePopulateDropdownList();
            //VwBg();
            model.ParentId = 0;
            model.IsActive = true;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,ExpireDate,CategoryId,CategoryList,CategoryName,IsActive,Orders,ImagePath,ProductVariantsId,StocksId,ParentId,ProductVariants,Parent,StockQuantity,ProductVariants.SizeId,Size,ProductVariants.Size,Product.Stocks.Quantity,Product.Stocks.Unit")]ProductVM model, IFormFile Image)
        {
            if(model.ParentId == null || model.ParentId<0)
            {
                model.ParentId = 0;
            }
            if(model.Image == null || model.ImagePath ==null)
            {
                model.ImagePath = "uploads\\img\\2c2f44a937fb4400ac42558d6fb74abb.jpg";
            }
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

                //var sz = _productManager.GetBySzId(Product.ProductVariants.SizeId);
                //_sizeManager.Add(sz);
                //var pv = _productManager.GetByPVId(Product.ProductVariants.SizeId);
                //pv.SizeId = Product.ProductVariants.SizeId;  
                //_productVariantManager.Add(pv);
                if(Product.StocksId==0)
                {

                    Product.StocksId = null;
                }
                bool isAdded = _productManager.Add(Product);
             if(Product.Stocks== null && Product.StocksId== null)
                {
                    Product.Stocks.ProductId = Product.Id;
                    Product.Stocks.Unit = null;

                    _stockManager.Add(Product.Stocks);

                  //  Product.StocksId = St.Id;
                }
               
                //_productManager.Update(Product);
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
            Category p = new Category
            {
                Id = 0,
                Name = "Select a Category"
            };
            // product.Prepend(p);
            //  product.Append(p);
            category.Clear();
            category.Add(p);

            var pp = _productManager.list();
            foreach (var prod in pp)
            {
                category.Add(prod);
            }
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
            if (Product.Parent == null || Product.ParentId != 0)
            {
                PopulateDropdownList1(Product.ParentId);
            }
            if (Product.Stocks == null || Product.StocksId != 0)
            {
                StockPopulateDropdownList(Product.StocksId);
            }
            if (Product.ProductVariants == null || Product.ProductVariantsId > 0)
            {
                if (Product.ProductVariants.Size == null || Product.ProductVariants.SizeId > 0)
                {
                    SizePopulateDropdownList(Product.ProductVariants.SizeId);
                }
                ProductVariantsPopulateDropdownList(Product.ProductVariantsId);
            }

            //VwBg();
            if (Product.ParentId == null)
            {
                Product.ParentId = 0;
            }
            if (Product.Image == null || Product.ImagePath == null)
            {
                Product.ImagePath = "uploads\\img\\2c2f44a937fb4400ac42558d6fb74abb.jpg";
            }

            //ProductVM aProduct = _mapper.Map<ProductVM>(Product);
            Item aProduct = _mapper.Map<Item>(Product);
            PopulateDropdownList(Product.CategoryId);
            if (Product.Parent == null || Product.ParentId != 0)
            {
                PopulateDropdownList1(Product.ParentId);
            }
            if (Product.Stocks == null || Product.StocksId != 0)
            {
                StockPopulateDropdownList(Product.StocksId);
            }
            if (Product.ProductVariants == null || Product.ProductVariantsId > 0)
            {
                if (Product.ProductVariants.Size == null || Product.ProductVariants.SizeId > 0)
                {
                    SizePopulateDropdownList(Product.ProductVariants.SizeId);
                }
                ProductVariantsPopulateDropdownList(Product.ProductVariantsId);
            }
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
           // PopulateDropdownList1(Product.ParentId);
            if (Product.Parent==null || Product.ParentId >=0)
            {
                if(Product.Parent == null)
                {
                    PopulateDropdownList1(0);
                }
                else
                {
                    PopulateDropdownList1(Product.ParentId);
                }
                
            }
             if(Product.Stocks != null && Product.StocksId >=0 || Product.StocksId == null)
            {
                if(Product.StocksId == null)
                {
                    StockPopulateDropdownList(0);
                    Product.StocksId = 0;
                }
                else
                {
                    StockPopulateDropdownList(Product.StocksId);
                }
               
            }
            if (Product.ProductVariants != null && Product.ProductVariantsId >=0 || Product.ProductVariantsId == null)
            {
                if(Product.ProductVariantsId == null)
                {
                    SizePopulateDropdownList();
                    ProductVariantsPopulateDropdownList();
                }
                else
                {
                    if (Product.ProductVariants.Size == null || Product.ProductVariants.SizeId >= 0)
                    {
                        if(Product.ProductVariants.Size == null)
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
            
            //VwBg();
           if(Product.ParentId == null)
            {
                Product.ParentId = 0;
            }
           if(Product.Image == null || Product.ImagePath == null)
            {
                Product.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if(Product.StocksId == 0)
            {
                Product.StocksId = null;
            }
            if(Product.StocksId == null)
            {

            }
            if (Product.StocksId > 0)
            {
                Product.Stocks = _productManager.GetBySId(Product.StocksId);
            }
            Product.IsActive = true;
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
        public async Task<IActionResult> Edit(long Id,[Bind("Id,Name,Price,ExpireDate,CategoryId,ParentId,CategoryList,CategoryName,IsActive,Orders,Image,ImagePath,Parent,Stocks,StockQuantity,StockUnit,ProductVariants,ProductVariantsId,size,ProductVariants.Size,Parent,Category,ProductVariants.SizeId,Stocks.Quantity,StocksId")]ProductVM Product, IFormFile Image)
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
            if (Product.ParentId == null || Product.ParentId < 0)
            {
                Product.ParentId = 0;
            }
            if (Product.Image == null || Product.ImagePath == null)
            {
                Product.ImagePath = "uploads\\img\\NoImageAvailable.jfif";
            }
            if (ModelState.IsValid)
            {
                var aProduct = _mapper.Map<Product>(Product);
                //if (Product.Stocks != null && Product.StocksId>0 )
                //{
                //    var s=_productManager.GetBySId(Product.StocksId);
                //    _stockManager.Update(s);
                //}
                //if (Product.ProductVariants != null && Product.ProductVariantsId > 0)
                //{
                //    if (Product.ProductVariants.Size != null && Product.ProductVariants.SizeId>0)
                //    {
                //        _sizeManager.Update(Product.ProductVariants.Size);
                //        _productVariantManager.Update(Product.ProductVariants);
                //    }

                //    else
                //    {
                //        Product.ProductVariants.SizeId = null;
                //        _productVariantManager.Update(Product.ProductVariants);
                //    }
                //}
                // _productVariantManager(aProduct.ProductVariantsId);
                var sz = _productManager.GetBySzId(Product.ProductVariants.SizeId);
                _sizeManager.Update(sz);
                var st = _productManager.GetBySId(aProduct.StocksId);
                st.Quantity = aProduct.Stocks.Quantity;
                var pv = _productManager.GetByPVId(Product.ProductVariantsId);
                pv.SizeId = Product.ProductVariants.SizeId;
                _productVariantManager.Update(pv);
                bool isUpdated = _productManager.Update(aProduct);
              
                _stockManager.Update(st);
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
                if (Product.Stocks != null)
                {
                    _stockManager.Remove(Product.Stocks);
                }
                if (Product.ProductVariants != null)
                {
                    if(Product.ProductVariants.Size !=null)
                    {
                        _sizeManager.Remove(Product.ProductVariants.Size);
                    }
                   
                    _productVariantManager.Remove(Product.ProductVariants);
                }
               
                
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