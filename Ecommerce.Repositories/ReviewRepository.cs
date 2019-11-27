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
    public class ReviewRepository : EFRepository<Review>, IReviewRepository
    {
        private EcommerceDbContext _db;

        public ReviewRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
    }
}
