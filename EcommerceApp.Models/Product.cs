﻿using System;
using System.Collections.Generic;

namespace Ecommerce.Models
{
   public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }

        public byte[] Image { get; set; }
        public string ImagePath { get; set; }

        public bool IsActive { get; set; }  

        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public List<ProductOrder> Orders { get; set; }

    }
}