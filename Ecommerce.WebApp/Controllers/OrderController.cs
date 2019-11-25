﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Order;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.WebApp.Controllers
{
    public class OrderController : Controller
    {
        private IOrderManager _orderManager;
        private IProductOrderManager _productOrderManager;
        private IMapper _mapper;

        public OrderController(IOrderManager orderManager, IMapper mapper, IProductOrderManager productOrderManager)
        {
            _orderManager = orderManager;
            _productOrderManager = productOrderManager;
            _mapper = mapper;
        }
        private void PopulateDropdownList(object selectList = null) /*Dropdown List Binding*/
        {
            // var category = _productManager.GetAll();
            //  ViewBag.SelectList= new SelectList(category, "Id", "Name", selectList);
            List<string> option = new List<string>();
            option.Add("Pending");
            option.Add("Accepted");
            option.Add("Packed");
            option.Add("On The Way");
            option.Add("Delivered");
            ViewBag.SelectList = new SelectList(option, selectList);
               
        }
        public IActionResult Index() //Search Facilities
        {
          

            var model = _orderManager.GetAll().ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            var orders = _orderManager.GetAll();
            PopulateDropdownList();
            var model = new OrderVM();
            model.OrderDate = DateTime.Now;
            model.CustomerId = 1;
            model.OrderNo = "100000"+model.CustomerId;
            model.OrderList = orders.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create([Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status")]OrderVM model)
        {
           
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
                    if (isAdded)
                    {
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

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var order = _orderManager.GetById((Int64)Id);
            PopulateDropdownList();
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
        public IActionResult Edit(int Id, [Bind("Id,CustomerId,OrderNo,OrderDate,Products,Customer,Status")]OrderVM order)
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