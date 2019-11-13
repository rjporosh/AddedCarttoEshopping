using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.BLL
{
    public class SizeManager : Manager<Size>, ISizeManager
    {
        private ISizeRepository _sizeRepository;
        //   private IProductRepository _productRepository;
        public SizeManager(ISizeRepository sizeRepository) : base(sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
    }
}
