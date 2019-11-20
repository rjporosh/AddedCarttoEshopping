using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Abstractions.Repositories
{
    public interface IStockRepository : IRepository<Stock>
    {
        Product GetByPId(long? Id);
        Stock check(long? Id);
    }
}
