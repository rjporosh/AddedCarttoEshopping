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

        public OrderManager(IRepository<Order> repository, IOrderRepository orderRepository) : base(repository)
        {
            _orderRepository = orderRepository;
        }

        public override bool Add(Order entity)
        {
            return _orderRepository.Add(entity);
        }

        public override ICollection<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public override Order GetById(long id)
        {
            return _orderRepository.GetById(id);
        }

        public override bool Update(Order entity)
        {
            return _orderRepository.Update(entity);
        }

        public override bool Remove(Order entity)
        {
            return _orderRepository.Remove(entity);
        }
        public bool OrderExists(long Id)
        {
            return _orderRepository.OrderExists(Id);
        }
    }
}
