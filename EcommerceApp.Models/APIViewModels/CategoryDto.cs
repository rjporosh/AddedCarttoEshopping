using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.APIViewModels
{
    public class CategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool ProductIsActive { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public DateTime ProductDate { get; set; }
    }
}
