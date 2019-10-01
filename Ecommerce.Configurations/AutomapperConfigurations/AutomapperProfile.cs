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
using Ecommerce.Models.APIViewModels;

namespace Ecommerce.Configurations.AutomapperConfigurations
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            CreateMap<CustomerCreateViewModel, Customer>();
            CreateMap<Customer, CustomerCreateViewModel>();
            CreateMap<Product, ProductVM>()/*.ForMember(m => m.CategoryName, map => map.MapFrom(vm => vm.Category.Name))*/;
            CreateMap<ProductVM, Product>()/*.ForMember(m => m.Category.Name, map => map.MapFrom(vm => vm.CategoryName))*/;
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
            CreateMap<Product, Item>();
            CreateMap<Item, Product>();

            //.ForMember(m => m.Category.Name, map => map.MapFrom(vm => vm.CategoryName))
        }
    }
}
