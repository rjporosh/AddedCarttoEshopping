using System;

namespace Ecommerce.Models
{
    public class ProductOrder
    {
        public ProductOrder()
        {

        }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public Decimal? Quantity { get; set; }
        public string? Status { get; set; }
        public string? AspNetUserId { get; set; }
        public virtual ApplicationUser AspNetUser { get; set; }
        //public Customer Customer { get; set; }
        //public long CustomerId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
        public int? DiscountPercentage { get; set; }
        public Decimal? UnitPrice { get; set; }

    }
}
