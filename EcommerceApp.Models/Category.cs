using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

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
        public long? ParentId { get; set; }
        public Category Parent { get; set; }

        [InverseProperty("Parent")]
        public List<Category> Childs { get; set; }
        public virtual List<Product> Products { get; set; }
       

    }
}
