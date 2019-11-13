using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class ProductOrderManager : Manager<ProductOrder>, IProductOrderManager
    {
        private IProductOrderRepository _productOrderRepository;
        //   private IProductRepository _productRepository;
        public ProductOrderManager(IProductOrderRepository productOrderRepository) : base(productOrderRepository)
        {
            _productOrderRepository = productOrderRepository;
        }
    }
}
