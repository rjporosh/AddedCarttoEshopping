using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.Helper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.DatabaseContext;

using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private IProductManager _manager;
        private IOrderManager _orderManager;
        private IProductOrderManager _productOrderManager;
        private ICustomerManager _customerManager;
        private IMapper _mapper;
        private Models.ProductOrder po = new Models.ProductOrder();
        private EcommerceDbContext _db;
        public CartController(IProductManager productManager, IMapper mapper,IProductOrderManager productOrderManager , IOrderManager orderManager, ICustomerManager customerManager)
        {
            _manager = productManager;
            _productOrderManager = productOrderManager;
            _orderManager = orderManager;
            _customerManager = customerManager;
            _mapper = mapper;
        }
        [Route("Index")]
        public IActionResult Index()
        {
            // List<Item> cart = new List<Item>();
               
            var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            
            if (cart == null)
            {
                ViewBag.cart = new List<Item>();
                ViewBag.Count = 0;
                ViewBag.Total = 0;
            }
            else
            {
                ViewBag.cart = cart;
                ViewBag.Total = cart.Sum(i => i.product.Price * i.Quantity);
                ViewBag.Count = cart.Sum(i => i.Quantity);
            }
           
          
            return View();
        }

        [Route("buy/{Id}")]
        public IActionResult buy(long Id,[Bind("Product,Quantity")]Item item)
        {
            
            if (Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                if (item.Quantity==0)
                {
                    item.Quantity = 1;
                }
              
                var cart = new List<Item>();
                cart.Add(new Item() { product = _manager.Find(Id), Quantity = item.Quantity });
               // po.ProductList.Add(item.product);
                po.Quantity = item.Quantity;
                po.Product = item.product;

                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
               
            }
            else
            {
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart,Id);
                if(index== -1)
                {
                    cart.Add(new Item() { product = _manager.Find(Id), Quantity = 1 });
                   // po.ProductList.Add(item.product);
                    po.Quantity = item.Quantity;
                    po.Product = item.product;
                }
                else
                {
                    cart[index].Quantity++;
                  //  cart.Count();
                }
                // cart.Count();
                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

           return RedirectToAction("Index");
        }

        [Route("Remove/{Id}")]
        public IActionResult Remove(long Id)
        {
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart, Id);
                cart.RemoveAt(index);
            Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index",cart);
        }
        
        //public IActionResult Checkout()
        //{
        //    return View();
        //}

      //  [Route("Edit/{Id}")]
      //  public IActionResult Edit( long Id)
      //  {
      //      var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
      //      int index = Exists(cart, Id);
      //      var model = cart[index];
            
            
      //      //  Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      //      return View(model);
      //  }

      ////  [Route("Edit/{Id}")]
      //  public IActionResult Edit (long Id,[Bind("Product,Quantity,cart")]Item item)
      //  {
      //      var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
      //      int index = Exists(cart, Id);
      //      cart[index]= item;
      //      Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
      //      return RedirectToAction("Index", cart);
      //  }
        // [Route("Clear")]
        public IActionResult Clear()
        {
            var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if(cart == null)
            {
                return RedirectToAction("Index");
            }
            cart.Clear();
            Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", cart);
        }

        //private Item toEdit(List<Item> cart, long Id)
        //{
        //     var item = new Item();
        //    for (int i = 0; i < cart.Count; i++)
        //    {
        //        if (cart[i].product.Id == Id)
        //        {
        //            return item = cart[i];
        //        }
        //    }
        //    return item;
        //}
        private  int Exists(List<Item> cart,long Id)
        {
            for(int i=0;i<cart.Count;i++)
            {
                if (cart[i].product.Id==Id)
                {
                    return i;
                }
            }
            return -1;
        }
        //public IActionResult redirect(List<Item> cart)
        //{
        //    int Item = ViewBag.count = cart.Sum(i => i.Quantity);
        //   // SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", totalItem);
        //    return RedirectToAction("_cardView", "Product", Item);
        //}
    }
}