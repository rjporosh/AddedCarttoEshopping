using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private IOrderManager _orderManager;
        private IProductOrderManager _productOrderManager;
        private IProductManager _productManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private IStockManager _stockManager;
        private IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper, IProductOrderManager productOrderManager, IStockManager stockManager, IProductManager productManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _orderManager = orderManager;
            _productOrderManager = productOrderManager;
            _stockManager = stockManager;
            _productManager = productManager;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _mapper = mapper;
        }
        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            // var category = _productManager.GetAll();
            //  ViewBag.SelectList= new SelectList(category, "Id", "Name", selectList);
            List<string> option = new List<string>();
            option.Add("Pending");
            option.Add("Rejected");
            option.Add("Placed");
            option.Add("Cancelled");
            option.Add("Accepted");
            option.Add("Packed");
            option.Add("On The Way");      
            option.Add("Delivered");
            ViewBag.SelectList = new SelectList(option, selectList);
               
        }
        private void PaymentMethodPopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            // var category = _productManager.GetAll();
            //  ViewBag.SelectList= new SelectList(category, "Id", "Name", selectList);
            List<string> option = new List<string>();
            option.Add("Bkash");
            option.Add("Rocket");
            option.Add("Paypal");
            option.Add("Master/Visa Card");
            option.Add("On Delivery");
            ViewBag.PaymentMethodSelectList = new SelectList(option, selectList);

        }
        [Authorize]
        public IActionResult Index() //Search Facilities
        {
          

            var model = _orderManager.GetAll().ToList();
            return View(model);
        }
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            var orders = _orderManager.GetAll();
            PopulateDropdownList();
            PaymentMethodPopulateDropdownList();
           
            var model = new OrderVM();
            model.OrderDate = DateTime.Now;
          //  model.CustomerId = 1;
            // var user = userManager.FindByNameAsync(User.Identity.Name);
            // model.AspNetUsersId = user.Id.ToString();
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            model.AspNetUserId = userManager.GetUserId(User); // Get user id:
           // model.AspNetUsersId = User.Identity.Name;
            model.OrderNo = DateTime.Now.ToBinary().ToString()+model.AspNetUserId;
           // model.AspNetUser = await userManager.FindByIdAsync(model.AspNetUserId).ConfigureAwait(true);
            model.ProductList = cart;
            model.OrderList = orders.ToList();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status,ShippingAddress,PaymentMethod,ProductList,Phone,AspNetUserId,AspNetUser")]OrderVM model)
        {
            //var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            //if(model.ProductList == null)
            //{
            //    model.ProductList = cart;

            //}
            if (model.OrderNo == null || model.OrderDate == null || model.AspNetUserId == null)
            {
                model.OrderDate = DateTime.Now;
               // model.CustomerId = 1;
                System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                model.AspNetUserId = userManager.GetUserId(User);
                //var id = userManager.GetUserId(User);
                //model.AspNetUser = await userManager.FindByIdAsync(id).ConfigureAwait(true);
                model.OrderNo = DateTime.Now.ToString() + model.AspNetUserId;
            }
           // model.OrderNo = 
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(model);
                if (_orderManager.OrderExists(model.Id))
                {
                    ViewBag.ErrorMessage = "Order Exists Already";                        
                }
                else
                {
                    order.Status = "Pending";

                    bool isAdded = _orderManager.Add(order);
                    long id = order.Id;

                    if (isAdded)
                    {
                        ProductOrder po = new ProductOrder();
                        Order o = _orderManager.GetById(id);
                        var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                    
                        foreach(var item in cart)
                        {
                            po.ProductId = item.product.Id;
                            long pid = item.product.Id;
                            po.Quantity = item.Quantity;
                            int qty = item.Quantity;
                            po.OrderId = id;
                            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;
                            po.AspNetUserId = userManager.GetUserId(User);
                            //po.AspNetUser = await userManager.FindByIdAsync(po.AspNetUserId).ConfigureAwait(true);
                            //po.Customer = o.Customer;
                            //po.CustomerId = o.CustomerId;
                            po.Status = "Pending";
                            _productOrderManager.Add(po);
                            var stock = _stockManager.check(pid);
                            stock.Quantity = stock.Quantity- qty;
                            _stockManager.Update(stock);
                        }
                        cart.Clear();
                        Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                        ViewBag.SuccessMessage = "Order Saved Successfully!";
                        //return nameof()
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }

            model.OrderList = _orderManager.GetAll().ToList(); ;
            return View(model);
        }


        public PartialViewResult OrderListPartial()
        {
            var orders = _orderManager.GetAll();
            return PartialView("Order/_OrderList", orders);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetById((Int64)Id);
            PopulateDropdownList();
            PaymentMethodPopulateDropdownList();
            OrderVM aOrder = _mapper.Map<OrderVM>(order);
            if (order == null)
            {
                return NotFound();
            }

            aOrder.OrderList = _orderManager.GetAll().ToList();
            return View(aOrder);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status,ShippingAddress,PaymentMethod,Phone,Products")]OrderVM order)
        {
            if (Id != order.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                ProductOrder po = new ProductOrder();
                Order o = _orderManager.GetById(Id);
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                foreach (var item in cart)
                {
                    po.ProductId = item.product.Id;
                    long pid = item.product.Id;
                    po.Quantity = item.Quantity;
                    int qty = item.Quantity;
                    po.OrderId = Id;
                    //po.AspNetUserId =
                    //po.Customer = o.Customer;
                    //po.CustomerId = o.CustomerId;
                    po.Status = "Pending";
                    _productOrderManager.Add(po);
                    var stock = _stockManager.check(pid);
                    stock.Quantity = stock.Quantity - qty;
                    _stockManager.Update(stock);
                }
                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                var aOrder = _mapper.Map<Order>(order);
                bool isUpdated = _orderManager.Update(aOrder);
                if (isUpdated)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Order Updated Successfully!";
                    return View("Index", orders);

                }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            order.OrderList = _orderManager.GetAll().ToList();
            return View(order);

        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(long id)
        {
            var customer = _orderManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _orderManager.Remove(customer);
                ProductOrder po = new ProductOrder();
                Order o = _orderManager.GetById(id);
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");

                foreach (var item in cart)
                {
                    po.ProductId = item.product.Id;
                    long pid = item.product.Id;
                    po.Quantity = item.Quantity;
                    int qty = item.Quantity;
                    po.OrderId = id;
                    //po.Customer = o.Customer;
                    //po.CustomerId = o.CustomerId;
                    _productOrderManager.Remove(po);
                    var stock = _stockManager.check(pid);
                    stock.Quantity = stock.Quantity + qty;
                    _stockManager.Update(stock);
                }
                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
                if (isDeleted)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Order Deleted Successfully.!";
                    return View("Index", orders);
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}