﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models
{
    public class Comment
    {
        public long? Id { get; set; }
        public long? ProductId { get; set; }
        public int Rating { get; set; }
        public string? AspNetUserId { get; set; }

        public long? CustomerId { get; set; }
        public DateTime? Date { get; set; }
        public string? Comments { get; set; }
        public Reply? Reply { get; set; }
        public ApplicationUser User { get; set; }
    }
}
