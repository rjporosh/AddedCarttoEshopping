using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.BLL.Base;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class StockManager : Manager<Stock>, IStockManager
    {
        private IStockRepository _stockRepository;
         private IProductRepository _productRepository;
        public StockManager(IStockRepository stockRepository, IProductRepository productRepository) : base(stockRepository)
        {
            _stockRepository = stockRepository;
            _productRepository = productRepository;
        }

        public Product GetByPId(long? Id)
        {
            return _stockRepository.GetByPId(Id);
        }
    }
}
