using Ecommerce.Abstractions.BLL.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Abstractions.BLL
{
    public interface IStockManager : IManager<Stock>
    {
        Product GetByPId(long? Id);
    }
}
