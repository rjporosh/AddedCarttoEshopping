using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.WebApp.Helper
{
    public class Item
    {
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
