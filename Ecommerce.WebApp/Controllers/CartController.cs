﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.Helper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.DatabaseContext;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.WebApp.Controllers
{
    [Authorize]
    [Route("cart")]
    public class CartController : Controller
    {
        private IProductManager _manager;
        private IOrderManager _orderManager;
        private IProductOrderManager _productOrderManager;
        private UserManager<Models.ApplicationUser> _userManager;
        private IMapper _mapper;
        private Models.ProductOrder po = new Models.ProductOrder();
        private EcommerceDbContext _db;
        public CartController(IProductManager productManager, IMapper mapper,IProductOrderManager productOrderManager , IOrderManager orderManager, UserManager<Models.ApplicationUser> userManager)
        {
            _manager = productManager;
            _productOrderManager = productOrderManager;
            _orderManager = orderManager;
            _userManager = userManager;
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
        [Authorize]
        [Route("buy/{Id}")]
        public IActionResult buy(long Id,Item item)
        {
            if(item.product==null)
            {
                item.product = _manager.GetById(Id);
            }
            if (Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                if (item.Quantity==0)
                {
                    item.Quantity = 1;
                }
               // item.user =  _userManager.FindByNameAsync(User.Identity.Name).Result;
                var cart = new List<Item>();
               item.product.Stocks.Quantity -= item.Quantity;
                
                cart.Add(new Item() { product = _manager.Find(Id), Quantity = item.Quantity ,user=item.user });
                _manager.Update(item.product);
              

                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
               
            }
            else
            {
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                if (item.Quantity == 0)
                {
                    item.Quantity = 1;
                }
                
                int index = Exists(cart,Id);
                if(index== -1)
                {
                   // item.user = _userManager.FindByNameAsync(User.Identity.Name).Result;
                    item.product.Stocks.Quantity -= item.Quantity;
                    cart.Add(new Item() { product = _manager.Find(Id), Quantity = item.Quantity ,user=item.user});
                    _manager.Update(item.product);
                    // po.ProductList.Add(item.product);
                    po.Quantity = item.Quantity;
                    po.Product = item.product;
                }
                else
                {
                    cart[index].Quantity+=item.Quantity;
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
            var product = _manager.GetById(Id);
             var i = cart[index];
            product.Stocks.Quantity +=  i.Quantity ;
            _manager.Update(product);
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
           
             //   //foreach (var item in cart)
               // //{

              //  //    var product = item.product;

              //  //    product.Stocks.Quantity += item.Quantity;
                ////    _manager.Update(product);

                ////}
               // //cart.Clear();
                for(int i =0;i<cart.Count;i++)
                {
                    var itm = cart[i];
                    var product =_manager.GetById(itm.product.Id);
                    product.Stocks.Quantity += itm.Quantity;
                    _manager.Update(product);
                   //// cart.RemoveAt(i);
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