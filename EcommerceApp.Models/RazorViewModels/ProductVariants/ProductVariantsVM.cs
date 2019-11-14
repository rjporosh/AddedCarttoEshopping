using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.ProductVariants
{
    public class ProductVariantsVM
    {
      
        public long? Id { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
      
        public long? SizeId { get; set; }
        public virtual Models.Size Size { get; set; }
        public string? Type { get; set; }
        public virtual List<Models.Product> ProductList { get; set; }
    }
}
