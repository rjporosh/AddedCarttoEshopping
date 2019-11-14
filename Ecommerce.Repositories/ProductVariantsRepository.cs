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
    public class ProductVariantsRepository : EFRepository<ProductVariants>, IProductVariantsRepository
    {
        private EcommerceDbContext _db;

        public ProductVariantsRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override ICollection<ProductVariants> GetAll()
        {
            return _db.ProductVariants
                .Include(c => c.Size)
                .ToList();
        }
        public override ProductVariants GetById(long id)
        {
            return _db.ProductVariants.Include(c => c.Size).FirstOrDefault(c => c.Id == id);
        }
        public override bool Remove(ProductVariants entity)
        {
            //var size = _db.Size.Find(entity.SizeId);
            //_db.Size.Remove(size);
            _db.ProductVariants.Remove(entity);
            

            return _db.SaveChanges() > 0;
        }
        public override bool Add(ProductVariants entity)
        {
            _db.ProductVariants.Add(entity);
         //   _db.Size.Add(entity.Size);
            return _db.SaveChanges() > 0;
        }
        public override bool Update(ProductVariants entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
           // _db.Entry(entity.Size).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }
    }
}
