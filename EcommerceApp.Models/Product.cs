﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double? DiscountPrice { get; set; }
        public double? DiscountPercent { get; set; }
        public double BuyCost { get; set; }
        public string? Description { get; set; }
        public string? ProductCode { get; set; }
        public DateTime? ExpireDate { get; set; }

        public byte[]? Image { get; set; }
    public string? ImagePath { get; set; }

    public bool IsActive { get; set; }
        public Review Review { get; set; }   
        //public long? StocksId { get; set; }
        //[ForeignKey("StocksId")]
        public Stock Stocks { get; set; }
    [ForeignKey("ParentId")]
    public long? ParentId { get; set; }
    [ForeignKey("ProductVariantsId")]
    public long? ProductVariantsId { get; set; }
    //[InverseProperty("ProductVariantsId")]

    public  ProductVariants ProductVariants { get; set; }
   // [ForeignKey("SizeId")]
   // public long? SizeId { get; set; }
    //[InverseProperty("SizeId")]
    //public virtual Size size { get; set; }
    public  Product Parent { get; set; }
    [InverseProperty("Parent")]
    public  List<Product> Childs { get; set; }
    //public string CategoryName { get; set; }
    public long CategoryId { get; set; }
    public Models.Category Category { get; set; }

        //   [NotMapped]
        //[ForeignKey("ProductVariantId")]
        //public virtual List<ProductVariants>? ProductVariantList { get; set; }
        [NotMapped]
        public  List<Category> Categories { get; set; }
        public virtual List<ProductOrder> Orders { get; set; }

}
}
  

