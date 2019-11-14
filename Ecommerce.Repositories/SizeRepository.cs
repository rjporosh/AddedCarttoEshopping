using Ecommerce.Abstractions.Repositories;
using Ecommerce.DatabaseContext;
using Ecommerce.Models;
using Ecommerce.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Repositories
{
    public class SizeRepository : EFRepository<Size>, ISizeRepository
    {
        private EcommerceDbContext _db;

        public SizeRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public  Size Find(long? id)
        {
            return _db.Size.Find(id);
        }
    }
}
