﻿using System;
using System.Collections.Generic;
using System.Text;
using Ecommerce.Abstractions.Repositories.Base;
using Ecommerce.Models;

namespace Ecommerce.Abstractions.Repositories
{
    public interface IOrderRepository:IRepository<Order>
    {
        bool OrderExists(long Id);
    }
}
