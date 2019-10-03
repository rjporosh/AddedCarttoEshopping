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
        public long Id { get; set; }
        public string Color { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        
        public long SizeId { get; set; }
        [ForeignKey("SizeId")]
        public Size Size { get; set; }
        public string Type { get; set; }
        public long ProductsId { get; set; }
        [ForeignKey("ProductVariantsId")]
        public Product Product { get; set; }
        public virtual List<Product> ProductList { get; set; }
    }
}
