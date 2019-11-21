using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.ProductOrder
{
    public class ProductOrderVM
    {
       // public long Id { get; set; }
        public long ProductId { get; set; }
        public long OrderId { get; set; }
        public Decimal? Quantity  { get; set; }
        public string Status { get; set; }
       // public Models.Customer Customer { get; set; }
        //  public long CustomerId { get; set; }
        public virtual Models.Order Order { get; set; }
        public virtual Models.Product Product { get; set; }
    }
}
