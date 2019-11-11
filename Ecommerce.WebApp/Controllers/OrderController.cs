using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager _orderManager;
        private IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper)
        {
            _orderManager = orderManager;
            _mapper = mapper;
        }
        public IActionResult Index(string searchBy, string search) //Search Facilities
        {
          

            var model = _orderManager.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            var orders = _orderManager.GetAll();
            var model = new OrderVM();
            model.OrderList = orders.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(OrderVM model)
        {
            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(model);
                if (_orderManager.OrderExists(model.Id))
                {
                    ViewBag.ErrorMessage = "Order Exists Already";
                }
                else
                {

                    bool isAdded = _orderManager.Add(order);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Order Saved Successfully!";
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

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetById((Int64)Id);
            OrderVM aOrder = _mapper.Map<OrderVM>(order);
            if (order == null)
            {
                return NotFound();
            }

            aOrder.OrderList = _orderManager.GetAll().ToList();
            return View(aOrder);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id,Name,Address,LoyaltyPoint")]OrderVM order)
        {
            if (Id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var aOrder = _mapper.Map<Order>(order);
                //if (_customerManager.CustomerExists(customer.Name))
                //{
                //    ViewBag.ErrorMessage = "Customer Exists Already";
                //}
                //else
                //{
                bool isUpdated = _orderManager.Update(aOrder);
                if (isUpdated)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Updated Successfully!";
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


        public IActionResult Delete(long id)
        {
            var customer = _orderManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _orderManager.Remove(customer);
                if (isDeleted)
                {
                    var orders = _orderManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    return View("Index", orders);
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}