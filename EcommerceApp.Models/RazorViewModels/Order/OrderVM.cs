﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Order
{
    public class OrderVM
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }

        public List<ProductOrder> Products { get; set; }
        public List<Models.Order>  OrderList { get; set; }
    }
}
