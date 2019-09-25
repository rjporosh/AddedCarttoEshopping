using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.DatabaseContext;
using Ecommerce.WebApp.Controllers.Helper;
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
            var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
            ViewBag.cart = cart;
            ViewBag.Total = cart.Sum(i => i.product.Price * i.Quantity);
            ViewBag.Count = cart.Sum(i=>i.Quantity);
            if (ViewBag.Total == null && ViewBag.Count == 0)
            {
                ViewBag.Total = 0;
            }
            return View();
        }

        [Route("buy/{Id}")]
        public IActionResult buy(long Id)
        {
            if (SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>();
                cart.Add(new Item() { product = _manager.Find(Id), Quantity = 1 });
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
               
            }
            else
            {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
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
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

           return RedirectToAction("Index");
        }

        [Route("Remove/{Id}")]
        public IActionResult Remove(long Id)
        {
                var cart = SessionHelper.GetObjectFromJson<List<Item>>(HttpContext.Session, "cart");
                int index = Exists(cart, Id);
                cart.RemoveAt(index);
                cart.Count();
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            

            return RedirectToAction("Index",cart);
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
    }
}