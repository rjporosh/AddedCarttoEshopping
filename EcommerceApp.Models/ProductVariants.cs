using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models
{
    public class ProductVariants
    {
        [Key]
        public long? Id { get; set; }
        public string? Color { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        [ForeignKey("SizeId")]
        public long? SizeId { get; set; }
        public virtual Size Size { get; set; }
        public string? Type { get; set; }
     //   public long ProductId { get; set; }
       // [ForeignKey("ProductId")]
        // public virtual Product Product { get; set; }
        // public virtual List<Product> ProductList { get; set; }
    }
}
