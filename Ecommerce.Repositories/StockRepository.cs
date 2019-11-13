using System.Collections.Generic;
using System.Linq;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.DatabaseContext;
using Ecommerce.Models;
using Ecommerce.Models.APIViewModels;
using Ecommerce.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class StockRepository : EFRepository<Stock>, IStockRepository
    {
        private EcommerceDbContext _db;

        public StockRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override ICollection<Stock> GetAll()
        {
            return _db.Stocks
               .Include(c => c.Product)
               .ToList();
        }
        public override Stock GetById(long id)
        {
            return _db.Stocks
              .Include(c => c.Product)
              .Where(c=>c.Id == id)
              .FirstOrDefault();
        }
        public override bool Remove(Stock entity)
        {
          
            _db.Stocks.Remove(entity);
            return _db.SaveChanges() > 0;
        }
    }
}
