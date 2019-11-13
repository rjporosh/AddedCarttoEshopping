using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.BLL.Base;
using Ecommerce.Models;

namespace Ecommerce.BLL
{
    public class OrderManager:Manager<Order>,IOrderManager
    {
        private IOrderRepository _orderRepository;

        public OrderManager( IOrderRepository orderRepository) : base(orderRepository)
        {
            _orderRepository = orderRepository;
        }

      
        public bool OrderExists(long Id)
        {
            return _orderRepository.OrderExists(Id);
        }
    }
}
