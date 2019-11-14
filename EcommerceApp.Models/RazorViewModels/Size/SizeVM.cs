using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Size
{
    public class SizeVM
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public virtual List<Models.Product> ProductList { get; set; }
    }
}
