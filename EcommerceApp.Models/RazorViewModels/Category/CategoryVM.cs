using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
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
        public long? ParentId { get; set; }
        //[JsonIgnore]
        //[IgnoreDataMember]
        public virtual Models.Category Parent { get; set; }
        public string ParentCategoryName { get; set; }

        [InverseProperty("Parent")]
        public virtual List<Models.Category> Childs { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        public virtual List<Models.Product> Products { get; set; }
        public virtual List<Models.Category> Categories { get; set; }
    }
}
