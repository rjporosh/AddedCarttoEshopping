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
    public class CommentsRepository : EFRepository<Comment>, ICommentsRepository
    {
        private EcommerceDbContext _db;

        public CommentsRepository(DbContext db) : base(db)
        {
            _db = db as EcommerceDbContext;
        }
        public override bool Add(Comment entity)
        {
            _db.Comments.Add(entity);
            return _db.SaveChanges()>0;
        }
        public override ICollection<Comment> GetAll()
        {
            return _db.Comments.Include(c=>c.Product).Include(c => c.AspNetUser)/*.Include(c=>c.AspNetUser).Include(c=>c.Reply)*/.ToList();
        }
        public override Comment GetById(long id)
        {
            var comment = _db.Comments.Include(c => c.Product).Include(c => c.AspNetUser).Where(c=>c.Id ==id).FirstOrDefault();
            return comment;
        }
        public override bool Update(Comment entity)
        {
            _db.Entry(entity).State = EntityState.Modified;
            return _db.SaveChanges() > 0;
        }
        public override bool Remove(Comment entity)
        {
            return base.Remove(entity);
        }
    }
}
