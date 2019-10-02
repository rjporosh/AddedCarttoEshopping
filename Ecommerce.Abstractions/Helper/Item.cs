using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Abstractions.Helper
{
    public class Item
    {
        public Product product { get; set; }
        public String ProductCategoryName { get; set; }
        public int Quantity { get; set; }
    }
}
