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
            return _db.Products.Include(c => c.Category).Where(c => c.CategoryId == categoryId).ToList();
        }

        public ICollection<Product> GetByPrice(double price)
        {
            return _db.Products.Where(c => ( c.Price>0 && c.Price <= price)).Include(c => c.Category).ToList();
        }

        public ICollection<Product> GetByName(string Name)
        {
            return _db.Products.Where(c => c.Name.Contains(Name)).Include(c => c.Category).ToList();
        }

        public ICollection<Product> GetByCategory(string CategoryName)
        {
            return _db.Products.Where(c => c.Category.Name.Contains(CategoryName)).Include(c => c.Category).ToList();
        }

        public List<Category> list()
        {
            return _db.Categories.ToList();
        }

        public Product Find(long id)
        {
            return _db.Products.Find(id);
        }
        public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria)
        {
            var products = _db.Products.AsQueryable();
            if (criteria != null)
            {
                if (!string.IsNullOrEmpty(criteria.Name))
                {
                    products = products.Where(c => c.Name.ToLower().Contains(criteria.Name.ToLower()));
                }

                if (criteria.FromPrice > 0)
                {
                    products = products.Where(c => c.Price >= criteria.FromPrice);
                }

                if (criteria.ToPrice > 0)
                {
                    products = products.Where(c => c.Price <= criteria.ToPrice);
                }


            }

            return products.ToList();
        }

        object IProductRepository.GetByCriteria(ProductSearchCriteriaVM criteria)
        {
            var products = _db.Products.AsQueryable();
            if (criteria != null)
            {
                if (!string.IsNullOrEmpty(criteria.Name))
                {
                    products = products.Where(c => c.Name.ToLower().Contains(criteria.Name.ToLower()));
                }

                if (criteria.FromPrice > 0)
                {
                    products = products.Where(c => c.Price >= criteria.FromPrice);
                }

                if (criteria.ToPrice > 0)
                {
                    products = products.Where(c => c.Price <= criteria.ToPrice);
                }


            }

            return products.Include(c => c.Category).ToList();
        }
    }
}
