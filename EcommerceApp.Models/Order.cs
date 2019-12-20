using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Order
    {
        public long Id { get; set; }
        //public long CustomerId { get; set; }
        [Unique]
        public string OrderNo { get; set; }
        public string Phone { get; set; }
        //public Decimal? DiscountPercentage { get; set; }
        public string? AspNetUserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        public DateTime OrderDate { get; set; }
        public string? Status { get; set; }
        public string? ShippingAddress { get; set; }
        public string PaymentMethod { get; set; }

        public List<ProductOrder> Products { get; set; }
        public List<Item> ProductList { get; set; }
        //public virtual Customer Customer { get; set; }
    }
}
