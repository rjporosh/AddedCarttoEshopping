using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Abstractions.BLL;
using Ecommerce.BLL;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Customer;
using Ecommerce.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.WebApp.Controllers
{
    public class CustomerController:Controller
    {
        private ICustomerManager _customerManager;
        private IMapper _mapper;

        public CustomerController(ICustomerManager customerManager,IMapper mapper)
        {
            _customerManager = customerManager;
            _mapper = mapper;
        }
        public IActionResult Index(string searchBy, string search) //Search Facilities
        {
            if (searchBy == "Address")
            {
                return View(_customerManager.GetByAddress(search));
            }
            else if (searchBy == "Name")
            {
                return View(_customerManager.GetByName(search));
            }

            var model = _customerManager.GetAll();
            return View(model);
        }

        public IActionResult Create()
        {
            var customers = _customerManager.GetAll();
            var model  = new CustomerCreateViewModel();
            model.CustomerList = customers.ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CustomerCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _mapper.Map<Customer>(model);
                if (_customerManager.CustomerExists(model.Name))
                {
                    ViewBag.ErrorMessage = "Customer Exists Already";
                }
                else
                {

                    bool isAdded = _customerManager.Add(customer);
                    if (isAdded)
                    {
                        ViewBag.SuccessMessage = "Saved Successfully!";
                    }
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Operation Failed!";
            }
            
            model.CustomerList = _customerManager.GetAll().ToList(); ;
            return View(model);
        }


        public PartialViewResult CustomerListPartial()
        {
            var customers = _customerManager.GetAll();
            return PartialView("Customer/_CustomerList", customers);
        }

        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var customer = _customerManager.GetById((Int64)Id);
            CustomerCreateViewModel aCustomer = _mapper.Map<CustomerCreateViewModel>(customer);
            if (customer == null)
            {
                return NotFound();
            }

            aCustomer.CustomerList = _customerManager.GetAll().ToList();
            return View(aCustomer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, [Bind("Id,Name,Address,LoyaltyPoint")]CustomerCreateViewModel customer)
        {
            if (Id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var aCustomer = _mapper.Map<Customer>(customer);
                //if (_customerManager.CustomerExists(customer.Name))
                //{
                //    ViewBag.ErrorMessage = "Customer Exists Already";
                //}
                //else
                //{
                    bool isUpdated = _customerManager.Update(aCustomer);
                    if (isUpdated)
                    {
                        var customers = _customerManager.GetAll();
                        ViewBag.SuccessMessage = "Updated Successfully!";
                        return View("Index", customers);

                    }
                //}
            }
            else
            {
                ViewBag.ErrorMessage = "Update Failed!";
            }
            customer.CustomerList = _customerManager.GetAll().ToList();
            return View(customer);

        }


        public IActionResult Delete(long id)
        {
            var customer = _customerManager.GetById(id);
            if (ModelState.IsValid)
            {
                bool isDeleted = _customerManager.Remove(customer);
                if (isDeleted)
                {
                    var products = _customerManager.GetAll();
                    ViewBag.SuccessMessage = "Deleted Successfully.!";
                    return View("Index", products);
                }

            }
            return RedirectToAction(nameof(Index));
        }
    }
}
