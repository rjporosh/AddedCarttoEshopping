using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;
using Ecommerce.Models.APIViewModels;

namespace Ecommerce.Abstractions.Repositories
{
    public interface IProductRepository:IRepository<Product>
    {
        ICollection<Product> GetByPrice(double price);
        ICollection<Product> GetByName(string Name);
        ICollection<Product> GetByCategory(string CategoryName);
        List<Category> list();
        ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria);
        ICollection<Product> GetByCatId(long Id);
        Product Find(long id);
    }
}
