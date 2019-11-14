using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Abstractions.Repositories
{
    public interface ISizeRepository : IRepository<Size>
    {
        Size Find(long? Id);
    }
}
