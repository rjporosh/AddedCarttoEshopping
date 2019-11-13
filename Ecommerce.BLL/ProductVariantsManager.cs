using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class ProductVariantsManager : Manager<ProductVariants>, IProductVariantsManager
    {
        private IProductVariantsRepository _productVariantsRepository;
        //   private IProductRepository _productRepository;
        public ProductVariantsManager(IProductVariantsRepository productVariantsRepository) : base(productVariantsRepository)
        {
            _productVariantsRepository = productVariantsRepository;
        }
    }
}
