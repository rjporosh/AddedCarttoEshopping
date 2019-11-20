using System;
using System.Collections.Generic;
using System.Text;

namespace Ecommerce.Models.RazorViewModels.Product
{
    public class ProductVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string? CategoryName { get; set; }

        public byte[]? Image { get; set; }
        public string? ImagePath { get; set; }

        public bool IsActive { get; set; }

        public long CategoryId { get; set; }
        public  Models.Category Category { get; set; }
        public virtual List<Models.Category> CategoryList { get; set; }

      //  public virtual Stock Stocks { get; set; }
      //  public long StocksId { get; set; }
      // public virtual ProductVariants ProductVariants { get; set; }
       // public long ProductVariantsId { get; set; }
   //     public long SizeId { get; set; }
      //  public virtual Size Size { get; set; }
        public long ProductVariantsSizesId { get; set; }
        public Models.Size ProductVariantsSize { get; set; }
        public Decimal? StocksQuantity { get; set; }
        public string? StocksUnit { get; set; }
       


        public long? StocksId { get; set; }
       
        public  Models.Stock Stocks { get; set; }
      
        public long? ParentId { get; set; }
      
        public long? ProductVariantsId { get; set; }
        //[InverseProperty("ProductVariantsId")]

        public  Models.ProductVariants ProductVariants { get; set; }
        public string? ProductVariantsColor { get; set; }
        public string? ProductVariantsBrand { get; set; }
        public string? ProductVariantsName { get; set; }

        public string? ProductVariantsSizeCode { get; set; }
        public string? ProductVariantsSizeName { get; set; }

        public long? ProductVariantsSizeId { get; set; }
        public  Models.Size Size { get; set; }
        public string? ProductVariantsType { get; set; }
      
        //public long? SizeId { get; set; }
        //[InverseProperty("SizeId")]
       // public virtual Models.Size size { get; set; }
        public  Models.Product Parent { get; set; }
      
        public  List<Models.Product> Childs { get; set; }



        public List<Models.Product> ProductList { get; set; }
        public List<Models.ProductOrder> Orders { get; set; }
    }
}
