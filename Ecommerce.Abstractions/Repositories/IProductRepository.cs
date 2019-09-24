using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;

namespace Ecommerce.Abstractions.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        ICollection<Product> GetByPrice(double price);
        ICollection<Product> GetByName(string Name);
        ICollection<Product> GetByCategory(string CategoryName);
        List<Category> list();
        Product Find(long id);
    }
}
