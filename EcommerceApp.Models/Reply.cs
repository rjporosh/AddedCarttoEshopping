using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models
{
    public class Reply
    {
        public long? Id { get; set; }
        public long? CustomerId { get; set; }
        public long? CommentId { get; set; }
        public DateTime? Date { get; set; }
        public string? Message { get; set; }
    }
}
