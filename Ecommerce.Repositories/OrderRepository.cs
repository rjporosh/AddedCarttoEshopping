using Ecommerce.Abstractions.Repositories;
using Ecommerce.DatabaseContext;
using Ecommerce.Models;
using Ecommerce.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ecommerce.Repositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        private EcommerceDbContext _db;
        public OrderRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override ICollection<Order> GetAll()
        {
            return _db.Orders
                //.Include(c=>c.Customer)
                //.Include(c => c.Products)
                .ToList();
        }
        public bool OrderExists(long Id)
        {
            return _db.Orders.Any(c => c.Id == Id);
        }
    }
}
