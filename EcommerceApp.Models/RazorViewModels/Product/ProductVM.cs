using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Product
{
    public class ProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public byte[] Image { get; set; }
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }

        public long CategoryId { get; set; }
        public virtual Models.Category Category { get; set; }
        public virtual List<Models.Category> CategoryList { get; set; }

        public string CategoryName { get; set; }

        public List<Models.Product> ProductList { get; set; }
        public List<ProductOrder> Orders { get; set; }
    }
}
