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
    public class ProductOrderRepository : EFRepository<ProductOrder>, IProductOrderRepository
    {
        private EcommerceDbContext _db;

        public ProductOrderRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override ICollection<ProductOrder> GetAll()
        {
            return _db.ProductOrder
               .Include(c => c.Product)
               .Include(c => c.Order)
               //.ThenInclude(c=>c.Customer)
               .ToList();
        }
    }
}
