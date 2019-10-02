using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
   public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public byte[] Image { get; set; }
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        public long ParentId { get; set; }
        public virtual Product Parent { get; set; }
        [InverseProperty("Parent")]
        public virtual List<Product> Childs { get; set; }
       // public virtual string CategoryName { get; set; }
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public List<ProductOrder> Orders { get; set; }

    }
}
