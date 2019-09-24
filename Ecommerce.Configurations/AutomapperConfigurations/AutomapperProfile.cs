using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Ecommerce.Models;
using Ecommerce.Models.RazorViewModels.Customer;
using Ecommerce.Models.RazorViewModels.Product;
using Ecommerce.Models.RazorViewModels;
using Ecommerce.Models.RazorViewModels.Category;
using Ecommerce.Models.RazorViewModels.Order;

namespace Ecommerce.Configurations.AutomapperConfigurations
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CustomerCreateViewModel, Customer>();
            CreateMap<Customer, CustomerCreateViewModel>();
            CreateMap<Product, ProductVM>().ForMember(vm=>vm.CategoryName, map=>map.MapFrom(m=>m.Category.Name));
            CreateMap<ProductVM, Product>();
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();

            //.ForMember(m => m.Category.Name, map => map.MapFrom(vm => vm.CategoryName))
        }
    }
}
