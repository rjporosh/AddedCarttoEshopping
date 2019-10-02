using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL.Base;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;
using Ecommerce.Models.APIViewModels;

namespace Ecommerce.Abstractions.BLL
{
    public interface IProductManager:IManager<Product>
    {
        ICollection<Product> GetByPrice(double price);
        ICollection<Product> GetByName(string Name);
        ICollection<Product> GetByCategory(string CategoryName);
        Product Find(long Id);

        List<Category> list();
        ICollection<Product> GetByCriteria(ProductSearchCriteriaVM criteria);
        ICollection<Product> GetByCatId(long Id);
    }
}
