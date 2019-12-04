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
using Ecommerce.Models.RazorViewModels.Stock;
using Ecommerce.Models.RazorViewModels.ProductOrder;
using Ecommerce.Models.RazorViewModels.Size;
using Ecommerce.Models.RazorViewModels.ProductVariants;

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
            CreateMap<Product, ProductDto>()/*.ForMember(m=>m.CategoryName, map=>map.MapFrom(vm=>vm.Category.Name))*/;
            CreateMap<ProductDto, Product>()/*.ForMember(m=>m.Category.Name, map=>map.MapFrom(vm=>vm.CategoryName))*/;
            CreateMap<Product, Ecommerce.Abstractions.Helper.Item>()/*.ForMember(m=>m.product.Category.Name , map=>map.MapFrom(vm=>vm.CategoryName))*/;
            CreateMap<Ecommerce.Abstractions.Helper.Item, Product>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto,Category>();
            CreateMap<ProductVM, Stock>();
            CreateMap<ProductVM, ProductVariants>();
            CreateMap<ProductVM, Size>();
            CreateMap<ProductVM, Category>();
            CreateMap<Stock, ProductVM>();
            CreateMap<ProductVariants, ProductVM>();
            CreateMap<Size, ProductVM>();
            CreateMap<Category, ProductVM>();
            CreateMap<Stock, StockVM>();
            CreateMap<StockVM, Stock>();
            CreateMap<Order, OrderVM>();
            CreateMap<OrderVM, Order>();
            CreateMap<Size, ProductVM>();
            CreateMap<ProductVM, Size>();
            CreateMap<ProductVariants, ProductVM>();
            CreateMap<ProductVM, ProductVariants>();
            CreateMap<ProductOrder, ProductOrderVM>();
            CreateMap<ProductOrderVM, ProductOrder>();
            CreateMap<Size, SizeVM>();
            CreateMap<SizeVM, Size>();
            CreateMap<ProductVariants, ProductVariantsVM>();
            CreateMap<ProductVariantsVM, ProductVariants>();
            CreateMap<ProductOrder, Item>();
            CreateMap<Item, ProductOrder>();
            CreateMap<ProductVM, Item>();
            CreateMap<Item, ProductVM>();
            CreateMap<ProductOrderVM, Item>();
            CreateMap<Item, ProductOrderVM>();





            //.ForMember(m => m.Category.Name, map => map.MapFrom(vm => vm.CategoryName))
        }
    }
}
