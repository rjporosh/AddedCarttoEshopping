using Ecommerce.Models;
using System;
using System.Collections.Generic;     
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public String ProductCategoryName { get; set; }
        public string user { get; set; }
        public int Quantity { get; set; }
    }
}
