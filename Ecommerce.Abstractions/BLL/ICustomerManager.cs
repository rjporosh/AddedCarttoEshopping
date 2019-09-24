using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.BLL.Base;
using Ecommerce.Models;

namespace Ecommerce.Abstractions.BLL
{
    public interface ICustomerManager:IManager<Customer>
    {
        ICollection<Customer> GetByAddress(string address);
        ICollection<Customer> GetByName(string Name);
        bool CustomerExists(string Name);
    }
}
