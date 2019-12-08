using System.Collections.Generic;
using System.Linq;
using Ecommerce.Abstractions.Repositories;
using Ecommerce.DatabaseContext;
using Ecommerce.Models;
using Ecommerce.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repositories
{
    public class CategoryRepository:EFRepository<Category>,ICategoryRepository
    {
        private EcommerceDbContext _db;

        public CategoryRepository(DbContext db):base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override ICollection<Category> GetAll()
        {
            return _db.Categories
                .Include(c => c.Childs)
                .Include(c=>c.Parent)
                  .ThenInclude(c=>c.Childs)
                .Include(c => c.Products)
                .Include(c => c.Parent)
                .ToList();
        }
        public override Category GetById(long id)
        {
            var category = _db.Categories
                .Include(c => c.Parent)
                .ThenInclude(c=>c.Childs)
                .Include(c => c.Products).ToList();
            var aCategory = category.Where(c => c.Id == id).FirstOrDefault();
            return aCategory;
        }

        //public void LoadProducts(Category category)
        //{
        //    _db.Entry(category)
        //        .Collection(c=>c.Products)
        //        .Query()
        //        .Where(c=>c.IsActive)
        //        .Load();

        //}

        //public List<Product> productList()
        //{
        //    return _db.Products.Include(i => i.Category).ToList();
        //}
    }
}
