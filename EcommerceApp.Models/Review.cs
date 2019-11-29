using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models
{
    public class Review
    {
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public long? CustomerId { get; set; }
        public int? Rating { get; set; }
       
        public Comment? Comments { get; set; }
    }
}
