using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ecommerce.Abstractions.Repositories.Base;

namespace Ecommerce.Abstractions.Repositories
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        ICollection<Customer> GetByAddress(string address);
        ICollection<Customer> GetByName(string name);
       bool CustomerExists(string name);
    }
}
