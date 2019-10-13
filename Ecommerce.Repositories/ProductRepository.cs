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
            return _db.Products
                .Include(c => c.Stocks)
                .Include(c =>  c.Category)
                //.Include(c=>c.size)
                .Include(c=>c.ProductVariants)
                .ThenInclude(c => c.Size)
                .ToList();
        }
        public override Product GetById(long id)
        {
            
           var product= _db.Products.Where(c => c.Id == id)
                .Include(c => c.Category)
                .Include(c => c.Stocks)
              //  .Include(c => c.size)
                //.Include(c => c.ProductVariants)
                
                .FirstOrDefault();
            //_db.Products.Find(id)
            return product;
        }
        public List<Product> GetByCategory(int categoryId)
        {
            return _db.Products.Where(c => c.CategoryId == categoryId).Include(c => c.Category).ToList();
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
            return _db.Categories.Include(c => c.Products).ToList();
        }

        public Product Find(long id)
        {
            var products = _db.Products.Include(c => c.Category).ToList();
            var product = products.Where(c=>c.Id == id).FirstOrDefault();
            //var product = _db.Products.Where(c => c.Id == id).Include(c => c.Category).FirstOrDefault();
            return product;
        }
        public ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria)
        {
            var products = _db.Products.AsQueryable();
            if (criteria != null)
            {
                if (!string.IsNullOrEmpty(criteria.Name))
                {
                    products = products.Where(c => c.Name.ToLower().Contains(criteria.Name.ToLower())).Include(c => c.Category);
                }

                if (criteria.FromPrice > 0)
                {
                    products = products.Where(c => c.Price >= criteria.FromPrice).Include(c => c.Category);
                }

                if (criteria.ToPrice > 0)
                {
                    products = products.Where(c => c.Price <= criteria.ToPrice).Include(c => c.Category);
                }


            }

            return products.Include(c => c.Category).ToList();
        }

        public ICollection<Product> GetByCatId(long Id)
        {
            return _db.Products.Where(c => c.CategoryId == Id).Include(c => c.Category).ToList();
        }

        //ICollection<Product> IProductRepository.GetByCriteria(ProductSearchCriteriaVM criteria)
        //{
        //    var products = _db.Products.AsQueryable();
        //    if (criteria != null)
        //    {
        //        if (!string.IsNullOrEmpty(criteria.Name))
        //        {
        //            products = products.Where(c => c.Name.ToLower().Contains(criteria.Name.ToLower())).Include(c => c.Category);
        //        }

        //        if (criteria.FromPrice > 0)
        //        {
        //            products = products.Where(c => c.Price >= criteria.FromPrice).Include(c => c.Category);
        //        }

        //        if (criteria.ToPrice > 0)
        //        {
        //            products = products.Where(c => c.Price <= criteria.ToPrice).Include(c => c.Category);
        //        }


        //    }

        //    return products.Include(c => c.Category).ToList();
        //}
    }
}
