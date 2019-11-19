using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Order
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public List<ProductOrder> Products { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
