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
    }
}
