using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Stock
{
    public class StockVM
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Models.Product Product { get; set; }

        public Decimal Quantity { get; set; }
        public string Unit { get; set; }
        public ICollection<Models.Stock> StockList { get; set; }
    }
}
