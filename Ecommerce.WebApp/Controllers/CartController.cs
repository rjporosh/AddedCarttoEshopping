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
        private IMapper _mapper;
        private EcommerceDbContext _db;
        public CartController(IProductManager productManager, IMapper mapper)
        {
            _manager = productManager;
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
        public IActionResult buy(long Id,Item item)
        {
            
            if (Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                if (item.Quantity==0)
                {
                    item.Quantity = 1;
                }
                var cart = new List<Item>();
                cart.Add(new Item() { product = _manager.Find(Id), Quantity = item.Quantity });
                Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
               
            }
            else
            {
                var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart,Id);
                if(index== -1)
                {
                    cart.Add(new Item() { product = _manager.Find(Id), Quantity = 1 });
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
                cart.Count();

            Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            

            return RedirectToAction("Index",cart);
        }
       // [Route("Clear")]
        public IActionResult Clear()
        {
            var cart = Ecommerce.Abstractions.Helper.SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            if(cart == null)
            {
                return RedirectToAction("Index");
            }
            cart.Clear();
            cart.Count();
            Ecommerce.Abstractions.Helper.SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", cart);
        }
        private int Exists(List<Item> cart,long Id)
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