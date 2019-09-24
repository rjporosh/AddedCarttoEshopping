using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Category
{
    public class CategoryVM
    {
        public CategoryVM()
        {
            Products = new List<Models.Product>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual List<Models.Product> Products { get; set; }
    }
}
