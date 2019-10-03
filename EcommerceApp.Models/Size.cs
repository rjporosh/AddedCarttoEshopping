using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models
{
    public class Size
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public long ProductVariantId { get; set; }
        [ForeignKey("ProductVariantId")]
        public virtual List<ProductVariants> ProductVariantsList { get; set; }
    }
}
