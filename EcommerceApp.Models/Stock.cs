using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ecommerce.Models
{
    public class Stock
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public virtual Product Product { get; set; }

        public Double Quantity { get; set; }
        public string Unit  { get; set; }


    }
}
