using System.Collections.Generic;
using System.Linq;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.DatabaseContext;
using Ecommerce.Models;
using Ecommerce.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class ProductRepository:EFRepository<Product>,IProductRepository
    {
        private EcommerceDbContext _db;

        public ProductRepository(DbContext db):base(db)
        {
            _db = db as EcommerceDbContext;
        }

        public override ICollection<Product> GetAll()
        {
            return _db.Products.Include(c=>c.Category).ToList();
        }
        public List<Product> GetByCategory(int categoryId)
        {
            return _db.Products.Where(c => c.CategoryId == categoryId).ToList();
        }

        public ICollection<Product> GetByPrice(double price)
        {
            return _db.Products.Where(c => c.Price <= price).ToList();
        }

        public ICollection<Product> GetByName(string Name)
        {
            return _db.Products.Where(c => c.Name.Contains(Name)).ToList();
        }

        public ICollection<Product> GetByCategory(string CategoryName)
        {
            return _db.Products.Where(c => c.Category.Name == CategoryName).ToList();
        }

        public List<Category> list()
        {
            return _db.Categories.ToList();
        }

        public Product Find(long id)
        {
            return _db.Products.Find(id);
        }
    }
}
