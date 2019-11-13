﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL;
using Ecommerce.DatabaseContext;
using Ecommerce.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Configurations
{
    public static class ServicesConfigurationExtension
    {
        public static void ConfigureServicesForEcommerce(this IServiceCollection services)
        {
            services.AddTransient<ICustomerManager, CustomerManager>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IProductManager, ProductManager>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ICategoryManager, CategoryManager>();
           services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IOrderManager, OrderManager>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IStockManager, StockManager>();
            services.AddTransient<IStockRepository, StockRepository>();

            services.AddTransient<DbContext, EcommerceDbContext>();
        }
    }
}
