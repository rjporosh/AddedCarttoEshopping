using System.Collections.Generic;

namespace Ecommerce.Models
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual  List<Product> Products { get; set; }

    }
}
