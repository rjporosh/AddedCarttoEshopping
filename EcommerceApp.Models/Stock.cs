﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ecommerce.Models
{
    public class Stock
    {
        public long Id { get; set; }

        public long? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public  Product Product { get; set; }

        public Decimal? Quantity { get; set; }
        public string? Unit  { get; set; }


    }
}
