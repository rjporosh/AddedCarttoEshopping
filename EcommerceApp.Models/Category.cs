using Newtonsoft.Json;
using System.Collections.Generic;
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
        public long ParentId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual  List<Product> Products { get; set; }

    }
}
