using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.ProductOrder
{
    public class ProductOrderVM
    {
        public long ProductId { get; set; }
        public long OrderId { get; set; }
      //  public long CustomerId { get; set; }
        public Models.Order Order { get; set; }
        public Models.Product Product { get; set; }
    }
}
